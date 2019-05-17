using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NormaMeasure.DBControl.Tables;
using System.Diagnostics;

namespace NormaMeasure.Devices.SAC.SACUnits
{
    public class U120 : CPSMeasureUnit
    {
        private bool _isCEKMinus;
        protected bool IsCEKMinus
        {
            get
            {
                return _isCEKMinus;
            }set
            {
                _isCEKMinus = value;
                MeasureMode_CMD_Addiction = (_isCEKMinus) ? (byte)0x08 : (byte)0x00;
            }
        }

        protected override void ConvertResult(ref double result, UnitMeasureRange range)
        {
            base.ConvertResult(ref result, range);
            if (IsCEKMinus)
            {
                switch(CurrentParameterType.ParameterTypeId)
                {
                    case MeasuredParameterType.K1:
                    case MeasuredParameterType.K2:
                    case MeasuredParameterType.K3:
                    case MeasuredParameterType.K23:
                    case MeasuredParameterType.Ea:
                        result = -result;
                        break;
                }
            }
            Debug.WriteLine($"U120.MakeMeasure():result {result}");
        }


        public U120(SACCPS _cps) : base(_cps)
        {
        }

        protected override bool CheckRange(double result)
        {
            if (CurrentParameterType.IsEK()) 
            {
                {
                    if (result < 2 && !IsCEKMinus)
                    {
                        IsCEKMinus = true;
                        return true;
                    }
                    else return false;
                }

            }else
            {
                if (selectedRangeIdx == 2)
                {
                    if (result <= 4800)
                    {
                        selectedRangeIdx--;
                        if (result < 480) selectedRangeIdx--;
                        Debug.WriteLine($"U120.CheckRange(): NewRange = {selectedRangeIdx}");
                        return true;
                    }
                }
            }
            return false;
            //return base.CheckRange(result);
        }

        public override void MakeMeasure(ref SACMeasurePoint point)
        {
            if (!CurrentParameterType.IsEK())
            {
                selectedRangeIdx = 2;
            }else
            {
                selectedRangeIdx = 0;
            }
            base.MakeMeasure(ref point);
        }

        public override void SetUnitStateByMeasurePoint(SACMeasurePoint point)
        {
            base.SetUnitStateByMeasurePoint(point);
            int defaultRangeIdx = CurrentParameterType.IsEK() ? 0 : 2;
            BETWEEN_ADC_TIME = 25;
            AFTER_SET_MODE_DELAY = 25;
            ChangeRangeCounter = ChangeRangeCounterMax = CurrentParameterType.IsEK() ? 0 : 3;
            Debug.WriteLine($"U120.MakeMeasure():defaultRangeIdx {defaultRangeIdx}");
            //SelectDefaultRange(defaultRangeIdx);
        }

        protected override void SetMeasureModeCMDByParameterType()
        {
            base.SetMeasureModeCMDByParameterType();
            IsCEKMinus = (LeadCommType == LeadCommutationType.B) && (CurrentParameterType.ParameterTypeId == MeasuredParameterType.Co);
            switch (CurrentParameterType.ParameterTypeId)
            {
                case MeasuredParameterType.Cp:
                    MeasureMode_CMD = 0x00;
                    break;
                case MeasuredParameterType.Ea:
                    MeasureMode_CMD = 0x40;
                    break;
                case MeasuredParameterType.Co:
                    MeasureMode_CMD = LeadCommType == LeadCommutationType.B ? (byte)0x44 : (byte)0x04; //true - C20, false-C10
                    break;
                case MeasuredParameterType.K1:
                    MeasureMode_CMD = 0x80;
                    break;
                case MeasuredParameterType.K2:
                case MeasuredParameterType.K3:
                case MeasuredParameterType.K23:
                case MeasuredParameterType.K9:
                case MeasuredParameterType.K10:
                case MeasuredParameterType.K11:
                case MeasuredParameterType.K12:
                case MeasuredParameterType.K9_12:
                    MeasureMode_CMD = 0xC0;
                    break;

            }
        }

        protected override UnitMeasureRange[] GetDefaultRanges()
        {
            UnitMeasureRange[] ranges = new UnitMeasureRange[] { };
            switch (CurrentParameterType.ParameterTypeId)
            {
                case MeasuredParameterType.Cp:
                case MeasuredParameterType.Co:
                    ranges = new UnitMeasureRange[] { CpCo_MeasureRange_1, CpCo_MeasureRange_2, CpCo_MeasureRange_3};
                    break;
                case MeasuredParameterType.Ea:
                case MeasuredParameterType.K1:
                case MeasuredParameterType.K2:
                case MeasuredParameterType.K3:
                case MeasuredParameterType.K23:
                case MeasuredParameterType.K9:
                case MeasuredParameterType.K10:
                case MeasuredParameterType.K11:
                case MeasuredParameterType.K12:
                case MeasuredParameterType.K9_12:
                    ranges = new UnitMeasureRange[] { EK_MeasureRange_1 };
                    break;
            }
            return ranges;
        }


        protected override void SetAllowedMeasuredParameters()
        {
            base.SetAllowedMeasuredParameters();
            allowedMeasuredParameters = new uint[] 
            {
                MeasuredParameterType.Co,
                MeasuredParameterType.Cp,
                MeasuredParameterType.dCp,
                MeasuredParameterType.Ea,
                MeasuredParameterType.K1,
                MeasuredParameterType.K2,
                MeasuredParameterType.K3,
                MeasuredParameterType.K9,
                MeasuredParameterType.K10,
                MeasuredParameterType.K11,
                MeasuredParameterType.K12,
                MeasuredParameterType.K23,
                MeasuredParameterType.K9_12
            };
        }

        protected override void SetUnitAddress()
        {
            unitCMD_Address = 0x02;
        }

        protected override void SetUnitInfo()
        {
            unitName = "U120";
            unitTitle = "Узел емкостных параметров CEK";
            base.SetUnitInfo();
        }



        #region Список диапазонов Cp и Сo
        protected UnitMeasureRange CpCo_MeasureRange_1
        {
            get
            {
                UnitMeasureRange range = new UnitMeasureRange();
                range.RangeId = "0";
                range.parameterTypeId = CurrentParameterType.ParameterTypeId;
                range.KK = 4000f;
                range.BV = 0;
                range.MinValue = 0;//не используется для выбора диапазона
                range.MaxValue = 58000;//не используется для выбора диапазона
                range.RangeTitle = "Диапазон 1";
                range.RangeCommand = 0x20;//, 0x60
                range.UnitId = UnitSerialNumber.ToString();
                range.NeedConvertResult = true;
                return range;
            }
        }
        protected UnitMeasureRange CpCo_MeasureRange_2
        {
            get
            {
                UnitMeasureRange range = new UnitMeasureRange();
                range.UnitId = UnitSerialNumber.ToString();
                range.RangeId = "1";
                range.parameterTypeId = CurrentParameterType.ParameterTypeId;
                range.KK = 400f;
                range.BV = 0;
                range.MinValue = 4800; //не используется для выбора диапазона
                range.MaxValue = 53000;//не используется для выбора диапазона
                range.RangeTitle = "Диапазон 2";
                range.RangeCommand = 0x10;//, 0x60
                range.NeedConvertResult = true;
                return range;
            }
        }

        protected UnitMeasureRange CpCo_MeasureRange_3
        {
            get
            {
                UnitMeasureRange range = new UnitMeasureRange();
                range.UnitId = UnitSerialNumber.ToString();
                range.RangeId = "2";
                range.parameterTypeId = CurrentParameterType.ParameterTypeId;
                range.KK = 40f;
                range.BV = 0;
                range.MinValue = 4800;//не используется для выбора диапазона
                range.MaxValue = 65500;//не используется для выбора диапазона
                range.RangeTitle = "Диапазон 3";
                range.RangeCommand = 0x00;//, 0x60
                range.NeedConvertResult = true;
                return range;
            }
        }

        #endregion

        #region Диапазоны Ea, K1, K2, K3, K9-K12

        protected UnitMeasureRange EK_MeasureRange_1
        {
            get
            {
                UnitMeasureRange range = new UnitMeasureRange();
                range.UnitId = UnitSerialNumber.ToString();
                range.RangeId = "0";
                range.parameterTypeId = CurrentParameterType.ParameterTypeId;
                range.KK = 4f;
                range.BV = 0;
                range.MinValue = 0;//не используется для выбора диапазона
                range.MaxValue = 65500;//не используется для выбора диапазона
                range.RangeTitle = "Диапазон 1";
                range.RangeCommand = 0x20;//, 0x60
                range.NeedConvertResult = true;
                return range;
            }
        }

        #endregion

        protected override void SetEtalon()
        {
            switch (CurrentParameterType.ParameterTypeId)
            {
                case MeasuredParameterType.Cp:
                    Etalon = Etalon_Cp;
                    break;
                case MeasuredParameterType.Ea:
                    Etalon = Etalon_Ea;
                    break;
                case MeasuredParameterType.Co:
                    Etalon = Etalon_Co;
                    break;
                case MeasuredParameterType.K1:
                    Etalon = Etalon_K1;
                    break;
                case MeasuredParameterType.K2:
                case MeasuredParameterType.K3:
                case MeasuredParameterType.K23:
                    Etalon = Etalon_K23;
                    break;
                case MeasuredParameterType.K9:
                case MeasuredParameterType.K10:
                case MeasuredParameterType.K11:
                case MeasuredParameterType.K12:
                case MeasuredParameterType.K9_12:
                    Etalon = Etalon_K9_12;
                    break;

            }
        }
        private MeasureUnitEtalon Etalon_Cp
        {
            get
            {
                MeasureUnitEtalon e = new MeasureUnitEtalon() { TrueValue = 10, MaxErrorPercent = 0.5, MaxErrorAddictive = 0, EtalonTitle = "Cp" };
                return e;
            }
        }

        private MeasureUnitEtalon Etalon_Co
        {
            get
            {
                MeasureUnitEtalon e = new MeasureUnitEtalon() { TrueValue = 10, MaxErrorPercent = 0.5, MaxErrorAddictive = 0, EtalonTitle = "Co" };
                return e;
            }
        }

        private MeasureUnitEtalon Etalon_Ea
        {
            get
            {
                MeasureUnitEtalon e = new MeasureUnitEtalon() { TrueValue = 10000, MaxErrorPercent = 5.0, MaxErrorAddictive = 0, EtalonTitle = "Ea" };
                return e;
            }
        }

        private MeasureUnitEtalon Etalon_K1
        {
            get
            {
                MeasureUnitEtalon e = new MeasureUnitEtalon() { TrueValue = 10000, MaxErrorPercent = 5.0, MaxErrorAddictive = 0, EtalonTitle = "K1" };
                return e;
            }
        }

        private MeasureUnitEtalon Etalon_K23
        {
            get
            {
                MeasureUnitEtalon e = new MeasureUnitEtalon() { TrueValue = 10000, MaxErrorPercent = 5.0, MaxErrorAddictive = 0, EtalonTitle = "K2,K3" };
                return e;
            }
        }

        private MeasureUnitEtalon Etalon_K9_12
        {
            get
            {
                MeasureUnitEtalon e = new MeasureUnitEtalon() { TrueValue = 10000, MaxErrorPercent = 5, MaxErrorAddictive = 0, EtalonTitle = "K9-K12" };
                return e;
            }
        }
    }
}
