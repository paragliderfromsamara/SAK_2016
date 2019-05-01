using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NormaMeasure.DBControl.Tables;
using System.Diagnostics;

namespace NormaMeasure.Devices.SAC.CPSUnits
{
    public class U130 : CPSMeasureUnit
    {
        public U130(SACCPS _cps, byte unit_number) : base(_cps)
        {
            unitNumber = unit_number;
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

        public override bool MakeMeasure(ref SACMeasurePoint point)
        {
            SelectDefaultRange();
            ExecuteElementaryMeasure(ref point);
            return true;
        }

        protected override void SetUnitAddress()
        {
            unitCMD_Address = (byte)(0x03 + unitNumber);
        }

        protected override void SetMeasureModeCMDByParameterType()
        {
            MeasureMode_CMD = 0x00;
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
            if (result > currentRanges[selectedRangeIdx].MaxValue && (selectedRangeIdx - 1) > 0)
            {
                selectedRangeIdx--;
                wasChanged = true;
                Debug.WriteLine("U130.CheckRange: Смена диапазона вниз");
            }
            else if (result < currentRanges[selectedRangeIdx].MinValue && (selectedRangeIdx + 1) < currentRanges.Length)
            {
                selectedRangeIdx++;
                wasChanged = true;
                Debug.WriteLine("U130.CheckRange: Смена диапазона вверх");
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
                range.KK = 163840f;
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
                range.KK = 16384000f;
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
                range.KK = 65536000f;
                range.BV = 0;
                range.MinValue = 0;
                range.MaxValue = 32768;
                range.RangeTitle = "Диапазон 3";
                range.RangeCommand = 0x84;//, 0x60
                range.NeedConvertResult = true;
                return range;
            }
        }


        public byte RizolUnitNumber => unitNumber;
        private byte unitNumber;
    }


}
