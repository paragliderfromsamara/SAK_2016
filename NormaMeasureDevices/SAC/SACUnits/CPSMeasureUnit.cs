using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NormaMeasure.DBControl.Tables;
using System.Threading;
using System.Diagnostics;

namespace NormaMeasure.Devices.SAC.SACUnits
{
    public class CPSMeasureUnit : SACUnit
    {
        protected bool IsEtalonMeasure = false;
        protected LeadCommutationType LeadCommType = LeadCommutationType.AB;
        protected uint[] allowedMeasuredParameters;
        private MeasuredParameterType curParameterType;
        protected int unitSerialNumber;
        protected byte MeasureMode_CMD = 0x00;
        /// <summary>
        /// Дополнительные настройки команды запуска измерения
        /// </summary>
        protected byte MeasureMode_CMD_Addiction = 0x00;
        /// <summary>
        /// Серийный номер узла установленного в систему
        /// </summary>
        public int UnitSerialNumber => unitSerialNumber;
        public byte ReadResult_CMDHeader => (byte)(SACCPS.Read_cmd | SACCPS.TwoBytes_cmd | unitCMD_Address | SACCPS.ReadResult);
        public byte SetMode_CMDHeader => (byte)(unitCMD_Address | SACCPS.ReadResult);

        protected int ChangeRangeCounterMax = 3;
        /// <summary>
        /// Количество циклов после установки режима для подавления переходных процессов
        /// </summary>
        protected int ChangeRangeCounter;
        /// <summary>
        /// Задаёт режим измерения
        /// </summary>
        /// <returns></returns>
        public virtual void SetMeasureMode()
        {
            byte header = (byte)(unitCMD_Address | SACCPS.ReadResult);
            byte mode = (byte)(MeasureMode_CMD | MeasureMode_CMD_Addiction);
            if (HasRange) mode |= currentRanges[selectedRangeIdx].RangeCommand;
            cps.WriteBytes(new byte[] { header, mode });
            Debug.WriteLine($"CPSMeasureUnit.SetMeasureMode(): Узел {unitName} {UnitId}; Команда {header.ToString("X")} {mode.ToString("X")}");
            Thread.Sleep(AFTER_SET_MODE_DELAY);
        }

        /// <summary>
        /// Устанавливает эталон для данного типа измеряемого параметра
        /// </summary>
        protected virtual void SetEtalon()
        {
            throw new NotImplementedException($"Не реализовано в дочернем классе {this.GetType().Name}");
        }


        /// <summary>
        /// Установка начального диапазона измерения
        /// </summary>
        protected virtual void SelectDefaultRange(int defaultIdx = 0)
        {
            if (HasMeasureRanges && !HasSelectedRange) selectedRangeIdx = defaultIdx;
        }

        /// <summary>
        /// Считывает результат с узла
        /// </summary>
        /// <returns></returns>
        protected double ReadUnitResult()
        {
            byte header = (byte)(SACCPS.Read_cmd | SACCPS.TwoBytes_cmd | unitCMD_Address | SACCPS.ReadResult);
            byte[] result = new byte[] { 0x00, 0x00 };
            double r = 0;
            cps.WriteCmdAndReadBytesArr(new byte[] { header }, result);
            //Thread.Sleep(BETWEEN_ADC_TIME);
            //cps.DevicePort.DiscardInBuffer();
            r = result[0] * 256 + result[1];
            return r;
        }

        /// <summary>
        /// Применяет коэффициенты для результата
        /// </summary>
        /// <param name="result"></param>
        protected void ApplyCoeffs(ref double result)
        {
            Debug.WriteLine($"ApplyCoeffs перед проверкой HasRange; HasMeasureRanges {HasMeasureRanges}; HasSelectedRange {HasSelectedRange};");
            if (HasRange)
            {
                UnitMeasureRange range = currentRanges[selectedRangeIdx];
                if (range.NeedConvertResult)
                {
                    Debug.WriteLine("ApplyCoeffs перед конвертацией после проверки HasRange");
                    ConvertResult(ref result, range);
                }
            }
        }

        /// <summary>
        /// Конвертация результата в соответствии с коэффициентами
        /// </summary>
        /// <param name="result"></param>
        /// <param name="range"></param>
        protected virtual void ConvertResult(ref double result, UnitMeasureRange range)
        {
            result = (result / range.KK) + range.BV;
        }

        /// <summary>
        /// Производит одиночное измерение на узле
        /// </summary>
        /// <returns></returns>
        public virtual void MakeMeasure(ref SACMeasurePoint point)
        {
            double result = (double)CPSMeasureUnit_Status.InProcess;
            //cps.OpenPort();
            set_mode_again:
            int waitingForAUnitTimes = 20;
            SetMeasureMode();
            repeat:
            do
            {
                Thread.Sleep(BETWEEN_ADC_TIME);
                result = ReadUnitResult();
                try
                {
                    CheckResult(result);
                    if (result == (double)CPSMeasureUnit_Status.NOT_USED)
                    {
                        Debug.WriteLine($"CPSMeasureUnit.MakeMeasure(): status = CPSMeasureUnit_Status.NOT_USED");
                        //SetMeasureMode();
                        goto set_mode_again;
                    }
                }
                catch(SACMeasureUnit_Exception ex)
                {
                    if (ex.ErrStatus == CPSMeasureUnit_Status.NotAnswer && waitingForAUnitTimes > 0) 
                    {
                        waitingForAUnitTimes--;
                        Debug.WriteLine($"CPSMeasureUnit.MakeMeasure(): status = CPSMeasureUnit_Status.NotAnswer {waitingForAUnitTimes}");
                        //Thread.Sleep(BETWEEN_ADC_TIME);
                        goto repeat;
                    }else
                    {
                        throw new SACMeasureUnit_Exception(ex.ErrStatus, ex.Message);
                    }
                }


            } while (result == (double)CPSMeasureUnit_Status.InProcess);
            if (ChangeRangeCounter-- > 0)
            {
                //Thread.Sleep(BETWEEN_ADC_TIME);
                Debug.WriteLine($"CPSMeasureUnit.MakeMeasure(): counter = {ChangeRangeCounter}");
                goto repeat;
            }
            if (CheckRange(result))
            {
                ChangeRangeCounter = ChangeRangeCounterMax;
                // SetMeasureMode();
                goto set_mode_again;
            }

            //cps.ClosePort();
            Debug.WriteLine($"CPSMeasureUnit.ExecuteElementaryMeasure(): Range {selectedRangeIdx} result {result}");
            point.RawResult = result;
            ApplyCoeffs(ref result);
            if (IsEtalonMeasure) CorrectEtalonValue(ref result);
            point.ConvertedResult = result;
        }

        protected void CorrectEtalonValue(ref double r)
        {
            if (Etalon.HasValue)
            {
                double measuredVal = cps.sac.SettingsFile.GetEtalonValue(Etalon.Value);
                r += (Etalon.Value.TrueValue - measuredVal);
            }
        }

        protected void CheckResult(double r)
        {

            if (r == (double)CPSMeasureUnit_Status.GTVLineInNull) throw new SACMeasureUnit_Exception(CPSMeasureUnit_Status.GTVLineInNull, "Измерительный узел не подключен, либо не исправен");
            else if (r == (double)CPSMeasureUnit_Status.NotAnswer)  throw new SACMeasureUnit_Exception(CPSMeasureUnit_Status.NotAnswer, "Измерительный узел не подключен, либо не исправен");
            else if (r == (double)CPSMeasureUnit_Status.UnloadReleRizolNotWork) throw new SACMeasureUnit_Exception(CPSMeasureUnit_Status.UnloadReleRizolNotWork, "Не сработало реле разряда узла 130");
        }


        protected MeasuredParameterType CurrentParameterType
        {
            get
            {
                return curParameterType;
            }
            set
            {
                curParameterType = value; 
                SetMeasureModeCMDByParameterType(); //Установка команды задания режима
                SetMeasureRangesForParameterType(); //Инициализация диапазонов
                SetEtalon(); //Установка эталона для данного типа параметра
            }
        }

        public CPSMeasureUnit(SACCPS _cps) : base(_cps)
        { 
            SetAllowedMeasuredParameters(); //Устанавливаем все доступные для узла параметры
        }

        /// <summary>
        /// Берёт номер установленного на данный момент узла из файла SACSettings
        /// </summary>
        protected virtual void GetMeasureUnitNumber_FromSACSettingsFile()
        {
            unitSerialNumber = cps.sac.SettingsFile.GetOrSetUnitNumber(this.UnitName);
        }

        protected override void InitUnit()
        {
            base.InitUnit();
            SetAllowedMeasuredParameters();
        }

        protected override void SetUnitInfo()
        {
            base.SetUnitInfo();
            GetMeasureUnitNumber_FromSACSettingsFile();
        }

        /// <summary>
        /// Алгоритм для измерения
        /// </summary>
        /// <param name="result"></param>
        /// <param name="pType"></param>
        /// <param name="isEtalonMeasure">true - измеряется эталон, false - измеряется стол</param>
        /// <param name="leadCommType"> Тип пожильного измерения</param>
        /// <returns></returns>
        public virtual void SetUnitStateByMeasurePoint(SACMeasurePoint point)
        {

            //if (IsAllowedParameter(pType.ParameterTypeId)) return false;
            CurrentParameterType = point.ParameterType;
            LeadCommType = point.LeadCommType;
            IsEtalonMeasure = point.CommutationType == SACCommutationType.Etalon;
            //PrepareCommutator(isEtalonMeasure);
            // result = 
        }


        /// <summary>
        /// Поддерживает ли модуль параметр с указанным id
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public bool IsAllowedParameter(uint id)
        {
            return allowedMeasuredParameters.Contains(id);
        }

        /// <summary>
        /// Установка доступных ихмеряемых параметров
        /// </summary>
        protected virtual void SetAllowedMeasuredParameters() { allowedMeasuredParameters = new uint[] { }; }

        #region Управление диапазонами измерений

        protected int selectedRangeIdx = -1;

        protected virtual void SetMeasureModeCMDByParameterType()
        {
            MeasureMode_CMD = 0x00;
            MeasureMode_CMD_Addiction = 0x00;
        }



        /// <summary>
        /// Проверка и изменение текущего значения диапазона измерения
        /// </summary>
        /// <param name="result"></param>
        /// <returns></returns>
        protected virtual bool CheckRange(double result)
        {
            bool wasChanged = false;
            Debug.WriteLine($"CPSMeasureUnit.CheckRange: result = {result}");
            if (result > currentRanges[selectedRangeIdx].MaxValue && (selectedRangeIdx + 1) < currentRanges.Length)
            {
                selectedRangeIdx++;
                wasChanged = true;
                Debug.WriteLine("CPSMeasureUnit.CheckRange: Смена диапазона вверх");
            } else if (result < currentRanges[selectedRangeIdx].MinValue && (selectedRangeIdx - 1) >= 0)
            {
                selectedRangeIdx--;
                wasChanged = true;
                Debug.WriteLine("CPSMeasureUnit.CheckRange: Смена диапазона вниз");
            }
            return wasChanged;
        }

        protected UnitMeasureRange[] currentRanges;
      
        /// <summary>
        /// Содержит диапазоны и выбран один из них
        /// </summary>
        protected bool HasRange => HasMeasureRanges && HasSelectedRange;
        /// <summary>
        /// Содержит диапазоны
        /// </summary>
        protected bool HasMeasureRanges => currentRanges.Length > 0;
        /// <summary>
        /// Выбран диапазон
        /// </summary>
        protected bool HasSelectedRange => selectedRangeIdx >= 0 && currentRanges.Length > selectedRangeIdx;

        /// <summary>
        /// Установка диапазонов измерений для текущего типа параметра
        /// </summary>
        /// <param name="pTypeId"></param>
        private void SetMeasureRangesForParameterType()
        {
            UnitMeasureRange[] defaultRanges = GetDefaultRanges();
            if (defaultRanges.Length>0)
            {
                for(int i=0; i< defaultRanges.Length; i++)
                {
                    cps.sac.SettingsFile.GetOrSet_UnitMeasureParameterRange(ref defaultRanges[i]);
                }
            }
            currentRanges = defaultRanges;
        }

        /// <summary>
        /// Создает диапазоны по умолчанию
        /// </summary>
        /// <param name="pTypeId"></param>
        /// <returns></returns>
        protected virtual UnitMeasureRange[] GetDefaultRanges()
        {
            throw new NotImplementedException($"Не реализовано в дочернем классе {this.GetType().Name}");
           // return new UnitMeasureRange[0];
        }


        /// <summary>
        /// Время между измерениями
        /// </summary>
        public int BETWEEN_ADC_TIME = 450;
        /// <summary>
        /// Время после смены диапазона/установки режима
        /// </summary>
        public int AFTER_SET_MODE_DELAY = 450;
        #endregion


        public MeasureUnitEtalon ? Etalon;

    }

    public enum CPSMeasureUnit_Status : Int32
    {
        UnloadReleRizolNotWork = 0xFFFC,
        NotAnswer = 0xFFFD,
        InProcess = 0xFFFE,
        GTVLineInNull = 0xFFFF,
        NOT_USED = 0xFFFB
    }

    public struct UnitMeasureRange
    {
        public string UnitId;
        public string RangeId;
        public string RangeTitle;
        public UnitMeasureRangeType RangeType;
        public double MinValue;
        public double MaxValue;
        public double KK;
        public double BV;
        public byte RangeCommand;
        public uint parameterTypeId;
        public bool NeedConvertResult;
    }

    public enum UnitMeasureRangeType
    {
        ByValueRange, ByFreqRange, ByVoltageRange
    }

    public class SACMeasureUnit_Exception : Exception
    {
        CPSMeasureUnit_Status errStatus;
        public CPSMeasureUnit_Status ErrStatus => errStatus;
        public SACMeasureUnit_Exception(string mess) : base(mess)
        {

        }
        public SACMeasureUnit_Exception(CPSMeasureUnit_Status unitStatus, string message) : base(message)
        {
            errStatus = unitStatus;
        }
    }

    public struct MeasureUnitEtalon
    {
        /// <summary>
        /// Заголовок эталона
        /// </summary>
        public string EtalonTitle;
        /// <summary>
        /// Значение которое должно быть
        /// </summary>
        public double TrueValue;
        /// <summary>
        /// Измеренное значение
        /// </summary>
        public double MeasuredValue;

        /// <summary>
        /// Максимальная допустимое отклонение в процентах
        /// </summary>
        public double MaxErrorPercent;
        /// <summary>
        /// Максимальное допустимое отклонение аддиктивное
        /// </summary>
        public double MaxErrorAddictive;
        
    }
}
