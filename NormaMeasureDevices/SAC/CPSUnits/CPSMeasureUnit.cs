using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NormaMeasure.DBControl.Tables;

namespace NormaMeasure.Devices.SAC.CPSUnits
{
    public class CPSMeasureUnit : CPSUnit
    {
        protected uint[] allowedMeasuredParameters;
        private MeasuredParameterType curParameterType;
        protected int unitSerialNumber;
        /// <summary>
        /// Серийный номер узла установленного в систему
        /// </summary>
        public int UnitSerialNumber => unitSerialNumber;
        public byte GetResultCMD => (byte)(SACCPS.Read_cmd | SACCPS.TwoBytes_cmd | unitCMD_Address);
        public byte SetRangeCMD => (byte)(unitCMD_Address);

        protected MeasuredParameterType CurrentParameterType
        {
            get
            {
                return curParameterType;
            }
            set
            {
                curParameterType = value;
                SetMeasureRangesForParameterType(curParameterType.ParameterTypeId);
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

        protected virtual double GetUnitResult()
        {
            return 0; 
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

        protected UnitMeasureRange[] currentRanges;

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
        GTVLineInNull = 0xFFFF
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
        public byte[] RangeCommand;
        public uint parameterTypeId;
    }

    public enum UnitMeasureRangeType
    {
        ByValueRange, ByFreqRange, ByVoltageRange
    }
}
