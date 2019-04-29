using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NormaMeasure.DBControl.Tables;
using System.Threading;

namespace NormaMeasure.Devices.SAC.CPSUnits
{
    public class CPSMeasureUnit : CPSUnit
    {
        protected uint[] allowedMeasuredParameters;
        private MeasuredParameterType curParameterType;
        protected int unitSerialNumber;
        protected byte MeasureMode_CMD = 0x00;
        /// <summary>
        /// Серийный номер узла установленного в систему
        /// </summary>
        public int UnitSerialNumber => unitSerialNumber;
        public byte ReadResult_CMDHeader => (byte)(SACCPS.Read_cmd | SACCPS.TwoBytes_cmd | unitCMD_Address | SACCPS.ReadResult);
        public byte SetMode_CMDHeader => (byte)(unitCMD_Address | SACCPS.ReadResult);

        /// <summary>
        /// Задаёт режим измерения
        /// </summary>
        /// <returns></returns>
        protected void SetMeasureMode()
        {
            byte header = (byte)(unitCMD_Address | SACCPS.ReadResult);
            byte mode = (byte)(MeasureMode_CMD);
            if (HasRange) mode |= currentRanges[currentRangeId].RangeCommand;
            cps.WriteBytes(new byte[] { header, mode });
            //System.Windows.Forms.MessageBox.Show($"header - {header.ToString("X")}; mode-{mode.ToString("X")} ");
            Thread.Sleep(250);
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
            r = result[0] * 256 + result[1];

            return r;
        }


        /// <summary>
        /// Производит одиночное измерение на узле
        /// </summary>
        /// <returns></returns>
        protected virtual double ExecuteElementaryMeasure()
        {
            double result = (double)CPSMeasureUnit_Status.InProcess;
            cps.OpenPort();
            set_mode_again:
            SetMeasureMode();
            do
            {
                result = ReadUnitResult();
                CheckResult(result);
                if (result == (double)CPSMeasureUnit_Status.NOT_USED) goto set_mode_again;
                Thread.Sleep(20);
            } while (result == (double)CPSMeasureUnit_Status.InProcess);
            cps.ClosePort();
            return result;
        }

        protected void CheckResult(double r)
        {

            if (r == (double)CPSMeasureUnit_Status.GTVLineInNull) throw new SACMeasureUnit_Exception(CPSMeasureUnit_Status.GTVLineInNull, "Измерительный узел не подключен, либо не исправен");
            else if (r == (double)CPSMeasureUnit_Status.NotAnswer) throw new SACMeasureUnit_Exception(CPSMeasureUnit_Status.NotAnswer, "Измерительный узел не подключен, либо не исправен");
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
                SetMeasureModeCMDParameterType(curParameterType.ParameterTypeId); //Установка команды задания режима
                SetMeasureRangesForParameterType(curParameterType.ParameterTypeId); //Инициализация диапазонов
            }
        }






        protected int currentRangeId;

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
        /// <returns></returns>
        public virtual bool MakeMeasure(ref double result, MeasuredParameterType pType)
        {

            //if (IsAllowedParameter(pType.ParameterTypeId)) return false;
            CurrentParameterType = pType;
            
            //PrepareCommutator(isEtalonMeasure);
           // result = 
            return true;
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

        protected virtual void SetMeasureModeCMDParameterType(uint parameterTypeId)
        {
            MeasureMode_CMD = 0x00;
        }



        protected bool CheckRange(double value)
        {
            bool wasChanged = false;
            if (value > currentRanges[selectedRangeIdx].MaxValue && (selectedRangeIdx + 1) < currentRanges.Length)
            {
                selectedRangeIdx++;
                wasChanged = true;
            } else if (value < currentRanges[selectedRangeIdx].MinValue && (selectedRangeIdx - 1) >= 0)
            {
                selectedRangeIdx--;
                wasChanged = true;
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
        protected bool HasSelectedRange => selectedRangeIdx >= 0;

        /// <summary>
        /// Установка диапазонов измерений для текущего типа параметра
        /// </summary>
        /// <param name="pTypeId"></param>
        private void SetMeasureRangesForParameterType(uint pTypeId)
        {
            UnitMeasureRange[] defaultRanges = GetDefaultRanges(pTypeId);
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
        protected virtual UnitMeasureRange[] GetDefaultRanges(uint pTypeId)
        {
            return new UnitMeasureRange[0];
        }

        #endregion

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
        public float MinValue;
        public float MaxValue;
        public float KK;
        public float BV;
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
}
