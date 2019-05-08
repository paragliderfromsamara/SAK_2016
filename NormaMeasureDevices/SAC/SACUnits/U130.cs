using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NormaMeasure.DBControl.Tables;
using System.Diagnostics;
using System.Threading;


namespace NormaMeasure.Devices.SAC.SACUnits
{
    public class U130 : CPSMeasureUnit
    {
        protected byte[] SwitchOffSupplyCMD;
        public U130(SACCPS _cps, byte unit_number) : base(_cps)
        {
            unitNumber = unit_number;
            SetUnitAddress();
        }

        /// <summary>
        /// Устанавливает напряжение источника
        /// </summary>
        public void SwitchOffVoltageSource()
        {
            cps.WriteBytes(new byte[] { unitCMD_Address, 0x00 });
            cps.SwitchOnOffLed(unitNumber);
        }

        protected override void SetAllowedMeasuredParameters()
        {
            base.SetAllowedMeasuredParameters();
            allowedMeasuredParameters = new uint[]
            {
                MeasuredParameterType.Risol1,
                MeasuredParameterType.Risol2,
                MeasuredParameterType.Risol3,
                MeasuredParameterType.Risol4
            };
        }

        public override void SetUnitStateByMeasurePoint(SACMeasurePoint point)
        {
            base.SetUnitStateByMeasurePoint(point);
            BETWEEN_ADC_TIME = 1500;
            AFTER_SET_MODE_DELAY = 2000;
            ChangeRangeCounterMax = 1;
            SelectDefaultRange(0);
            cps.SwitchOnOffLed(unitNumber, true);
        }

        protected override void SetUnitAddress()
        {
            unitCMD_Address = (byte)(0x03 + unitNumber);
        }

        protected override void SetMeasureModeCMDByParameterType()
        {
            MeasureMode_CMD = 0x04;
        }

        protected override UnitMeasureRange[] GetDefaultRanges()
        {
            return new UnitMeasureRange[] { Rizol_MeasureRange_1, Rizol_MeasureRange_2, Rizol_MeasureRange_3 };
        }

        protected override void SetUnitInfo()
        {
            unitName = "U130";
            unitTitle = "Узел измерения сопротивления изоляции";
            base.SetUnitInfo();
        }

        protected override void ConvertResult(ref double result, UnitMeasureRange range)
        {
            result = result == 0 ? 9999999 : (range.KK / result) + range.BV;
        }

        protected override bool CheckRange(double result)
        {
            bool wasChanged = false;
            Debug.WriteLine($"U130.CheckRange: result = {result}");
            switch(selectedRangeIdx)
            {
                case 0:
                    if (result <= 300)
                    {
                        Debug.WriteLine("U130.CheckRange: Смена диапазона вверх");
                        selectedRangeIdx++;
                        wasChanged = true;
                    }
                    break;
                case 1:
                    if (result<821)
                    {
                        Debug.WriteLine("U130.CheckRange: Смена диапазона вверх");
                        selectedRangeIdx++;
                        wasChanged = true;
                    }
                    else if (result >= 45000)
                    {
                        Debug.WriteLine("U130.CheckRange: Смена диапазона вниз");
                        selectedRangeIdx--;
                        wasChanged = true;
                    }
                    break;
                case 2:
                    if (result >= 32768)
                    {
                        Debug.WriteLine("U130.CheckRange: Смена диапазона вниз");
                        selectedRangeIdx--;
                        wasChanged = true;
                    }
                    break;
            }
            return wasChanged;
        }

        protected UnitMeasureRange Rizol_MeasureRange_1
        {
            get
            {
                UnitMeasureRange range = new UnitMeasureRange();
                range.RangeId = "0";
                range.parameterTypeId = CurrentParameterType.ParameterTypeId;
                range.KK = 163840;
                range.BV = 0;
                range.MinValue = 300;
                range.MaxValue = 45000;
                range.RangeTitle = "Диапазон 1";
                range.RangeCommand = 0xA4;
                range.UnitId = UnitSerialNumber.ToString();
                range.NeedConvertResult = true;
                return range;
            }
        }
        protected UnitMeasureRange Rizol_MeasureRange_2
        {
            get
            {
                UnitMeasureRange range = new UnitMeasureRange();
                range.UnitId = UnitSerialNumber.ToString();
                range.RangeId = "1";
                range.parameterTypeId = CurrentParameterType.ParameterTypeId;
                range.KK = 16384000;
                range.BV = 0;
                range.MinValue = 820;
                range.MaxValue = 45000;
                range.RangeTitle = "Диапазон 2";
                range.RangeCommand = 0x94;
                range.NeedConvertResult = true;
                return range;
            }
        }

        protected UnitMeasureRange Rizol_MeasureRange_3
        {
            get
            {
                UnitMeasureRange range = new UnitMeasureRange();
                range.UnitId = UnitSerialNumber.ToString();
                range.RangeId = "2";
                range.parameterTypeId = CurrentParameterType.ParameterTypeId;
                range.KK = 65536000;
                range.BV = 0;
                range.MinValue = 0;
                range.MaxValue = 32768;
                range.RangeTitle = "Диапазон 3";
                range.RangeCommand = 0x84;
                range.NeedConvertResult = true;
                return range;
            }
        }


        public byte RizolUnitNumber => unitNumber;
        private byte unitNumber;
    }


}
