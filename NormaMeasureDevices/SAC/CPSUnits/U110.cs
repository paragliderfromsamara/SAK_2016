using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NormaMeasure.DBControl.Tables;

namespace NormaMeasure.Devices.SAC.CPSUnits
{
    public class U110 : CPSMeasureUnit
    {
        public U110(SACCPS _cps) : base(_cps)
        {
        }

        protected override void SetAllowedMeasuredParameters()
        {
            base.SetAllowedMeasuredParameters();
            allowedMeasuredParameters = new uint[] { MeasuredParameterType.Calling, MeasuredParameterType.Rleads};
        }
        protected override void SetUnitAddress()
        {
            unitCMD_Address = 0x01;
        }

        protected override void SetUnitInfo()
        {
            unitName = "U110";
            unitTitle = "Узел произвонки и измерения Rжил";
            base.SetUnitInfo();
        }

        protected override UnitMeasureRange[] GetDefaultRanges(uint pTypeId)
        {
            if (pTypeId == MeasuredParameterType.Rleads)
            {
                return new UnitMeasureRange[] { Rleads_MeasureRange_1, Rleads_MeasureRange_2 };
            }else
            {
                return new UnitMeasureRange[] { };
            }

        }

        #region Список диапазонов
        protected UnitMeasureRange Rleads_MeasureRange_1
        {
            get
            {
                UnitMeasureRange range = new UnitMeasureRange();
                range.RangeId = "0";
                range.parameterTypeId = MeasuredParameterType.Rleads;
                range.KK = 409.6f;
                range.BV = 0;
                range.MinValue = 0;
                range.MaxValue = 90;
                range.RangeTitle = "Диапазон 1";
                range.RangeCommand = new byte[] { 0x40, 0x00};//, 0x60
                range.UnitId = UnitSerialNumber.ToString();
                return range;
            }
        }

        protected UnitMeasureRange Rleads_MeasureRange_2
        {
            get
            {
                UnitMeasureRange range = new UnitMeasureRange();
                range.UnitId = UnitSerialNumber.ToString();
                range.RangeId = "1";
                range.parameterTypeId = MeasuredParameterType.Rleads;
                range.KK = 40.96f;
                range.BV = 0;
                range.MinValue = 80;
                range.MaxValue = 1300;
                range.RangeTitle = "Диапазон 2";
                range.RangeCommand = new byte[] { 0x60, 0x00 };//, 0x60
                return range;
            }
        }
        #endregion
    }
}
