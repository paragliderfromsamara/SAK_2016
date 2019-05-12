using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Threading;
using NormaMeasure.DBControl.Tables;
using System.Diagnostics;

namespace NormaMeasure.Devices.SAC.SACUnits
{


    public class U160 : CPSMeasureUnit
    {
        public U160(SACCPS _cps) : base(_cps)
        {
        }

        public override void SetMeasureMode()
        {
            byte header = (byte)(unitCMD_Address | SACCPS.ReadResult | SACCPS.TwoBytes_cmd);
            byte mode = (byte)(MeasureMode_CMD | currentRanges[selectedRangeIdx].RangeCommand);
            cps.WriteBytes(new byte[] { header, FreqCode, mode });
            Debug.WriteLine($"U160.SetMeasureMode(): Узел {unitName} {UnitId}; Команда {header.ToString("X")} {FreqCode.ToString("X")} {mode.ToString("X")}");
            Thread.Sleep(AFTER_SET_MODE_DELAY);
        }

        protected override void SetUnitAddress()
        {
            unitCMD_Address = 0x03;
        }

        protected override void SetMeasureModeCMDByParameterType()
        {
            base.SetMeasureModeCMDByParameterType();
            switch(CurrentParameterType.ParameterTypeId)
            {
                case MeasuredParameterType.al:
                    MeasureMode_CMD = 0x20;
                    break;
                case MeasuredParameterType.Ao:
                case MeasuredParameterType.Az:
                    MeasureMode_CMD = 0x00;
                    break;
            }
        }

        protected override UnitMeasureRange[] GetDefaultRanges()
        {
            if (CurrentParameterType.ParameterTypeId == MeasuredParameterType.al)
            {
                return new UnitMeasureRange[] { al_LF, al_10_40, al_40_2000 };
            }else
            {
                return new UnitMeasureRange[] { Ao_Az_LF, Ao_Az_10_40, Ao_Az_40_2000 };
            }
        }

        public override void SetUnitStateByMeasurePoint(SACMeasurePoint point)
        {
            base.SetUnitStateByMeasurePoint(point);
            CurrentFrequency = point.CurrentFrequency;
            SelectDefaultRange();
        }

        protected override void SetAllowedMeasuredParameters()
        {
            base.SetAllowedMeasuredParameters();
            allowedMeasuredParameters = new uint[]
            {
                MeasuredParameterType.al,
                MeasuredParameterType.Ao,
                MeasuredParameterType.Az
            };
        }

        protected override void SetUnitInfo()
        {
            unitName = "U160";
            unitTitle = "Узел измерения параметров влияния";
            base.SetUnitInfo();
        }

        /// <summary>
        /// Чтобы не пытался искать диапазон по результату - здесь он по частоте
        /// </summary>
        /// <param name="result"></param>
        /// <returns></returns>
        protected override bool CheckRange(double result)
        {
            return false;
        }

        protected UnitMeasureRange al_LF
        {
            get
            {
                UnitMeasureRange range = new UnitMeasureRange();
                range.RangeId = "LF";
                range.parameterTypeId = CurrentParameterType.ParameterTypeId;
                range.KK = 11.77;
                range.BV = 0;
                range.MinValue = 0.8;
                range.MaxValue = 0.8;
                range.RangeTitle = "al 0.8 кГц";
                range.RangeCommand = 0x08;
                range.UnitId = UnitSerialNumber.ToString();
                range.NeedConvertResult = true;
                return range;
            }
        }

        protected UnitMeasureRange al_10_40
        {
            get
            {
                UnitMeasureRange range = new UnitMeasureRange();
                range.RangeId = "HF_10_40";
                range.parameterTypeId = CurrentParameterType.ParameterTypeId;
                range.KK = 11.8;
                range.BV = 0;
                range.MinValue = 10.0;
                range.MaxValue = 40.0;
                range.RangeTitle = "al 10-40 кГц";
                range.RangeCommand = 0x10;
                range.UnitId = UnitSerialNumber.ToString();
                range.NeedConvertResult = true;
                return range;
            }
        }

        protected UnitMeasureRange al_40_2000
        {
            get
            {
                UnitMeasureRange range = new UnitMeasureRange();
                range.RangeId = "HF_40_2000";
                range.parameterTypeId = CurrentParameterType.ParameterTypeId;
                range.KK = 11.8;
                range.BV = 0;
                range.MinValue = 40.0;
                range.MaxValue = 2000.0;
                range.RangeTitle = "al 40-2000 кГц";
                range.RangeCommand = 0x00;
                range.UnitId = UnitSerialNumber.ToString();
                range.NeedConvertResult = true;
                return range;
            }
        }

        protected UnitMeasureRange Ao_Az_LF
        {
            get
            {
                UnitMeasureRange range = new UnitMeasureRange();
                range.RangeId = "LF";
                range.parameterTypeId = CurrentParameterType.ParameterTypeId;
                range.KK = 11.77;
                range.BV = 0;
                range.MinValue = 0.8;
                range.MaxValue = 0.8;
                range.RangeTitle = "Ao, Az 0.8 кГц";
                range.RangeCommand = 0x08;
                range.UnitId = UnitSerialNumber.ToString();
                range.NeedConvertResult = true;
                return range;
            }
        }

        protected UnitMeasureRange Ao_Az_10_40
        {
            get
            {
                UnitMeasureRange range = new UnitMeasureRange();
                range.RangeId = "HF_10_40";
                range.parameterTypeId = CurrentParameterType.ParameterTypeId;
                range.KK = 11.8;
                range.BV = 0;
                range.MinValue = 10.0;
                range.MaxValue = 40.0;
                range.RangeTitle = "Ao, Az 10-40 кГц";
                range.RangeCommand = 0x10;
                range.UnitId = UnitSerialNumber.ToString();
                range.NeedConvertResult = true;
                return range;
            }
        }

        protected UnitMeasureRange Ao_Az_40_2000
        {
            get
            {
                UnitMeasureRange range = new UnitMeasureRange();
                range.RangeId = "HF_40_2000";
                range.parameterTypeId = CurrentParameterType.ParameterTypeId;
                range.KK = 11.8;
                range.BV = 0;
                range.MinValue = 40.0;
                range.MaxValue = 2000.0;
                range.RangeTitle = "Ao, Az 40-2000 кГц";
                range.RangeCommand = 0x00;
                range.UnitId = UnitSerialNumber.ToString();
                range.NeedConvertResult = true;
                return range;
            }
        }

        /// <summary>
        /// Текущая частота измерителя. 
        /// Когда задается значение частоты, выбирается диапазон и вычисляется код частоты
        /// </summary>
        public float CurrentFrequency
        {
            get
            {
                return CurFrequency;
            }set
            {
                CurFrequency = value;
                if (CurFrequency == 0.8)
                {
                    FreqCode = 0x05;
                    selectedRangeIdx = 0;
                }
                else if (CurFrequency >= 40)
                {
                    FreqCode = (byte)(CurFrequency / 8); // 40-2000
                    selectedRangeIdx = 2;
                }
                else
                {
                    selectedRangeIdx = 1;
                    FreqCode = (byte)(CurFrequency / 2); // 10-40
                }
            }
        }


        private byte FreqCode = 0x00;
        private float CurFrequency;

    }
}
