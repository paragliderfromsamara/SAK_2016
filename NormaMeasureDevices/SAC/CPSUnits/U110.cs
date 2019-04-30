using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NormaMeasure.DBControl.Tables;
using System.Threading;
using System.Diagnostics;



namespace NormaMeasure.Devices.SAC.CPSUnits
{
    public class U110 : CPSMeasureUnit
    {
        public U110(SACCPS _cps) : base(_cps)
        {
        }

        protected override void SetMeasureModeCMDParameterType(uint parameterTypeId)
        {
            base.SetMeasureModeCMDParameterType(parameterTypeId);
            if (parameterTypeId == MeasuredParameterType.Rleads) MeasureMode_CMD = 0x00;
            else if (parameterTypeId == MeasuredParameterType.Calling) MeasureMode_CMD = 0x20;
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

        public override bool MakeMeasure(ref double result, MeasuredParameterType pType)
        {
            base.MakeMeasure(ref result, pType);
            if (pType.ParameterTypeId == MeasuredParameterType.Rleads)
            {
                result = RleadsMeasure();
            }
            return true;
        }

        /// <summary>
        /// Измерение Rжил
        /// </summary>
        /// <returns></returns>
        private double RleadsMeasure()
        {
            double result = 0;
            SelectDefaultRange(0);
            result = ExecuteElementaryMeasure();
            return result;
        }

        private void SetRange(int rangeId)
        {
            UnitMeasureRange curRange = currentRanges[rangeId];
            byte[] cmd = new byte[] { (byte)SetMode_CMDHeader, 0x00, curRange.RangeCommand };
            cps.WriteBytes(cmd);
            Thread.Sleep(20);
        }

        private double GetResultByRangeId(int rangeId)
        {
            double r = 0;
            byte[] rsltArr = new byte[] { 0x00, 0x00};
            //SetRange(rangeId);
            cps.OpenPort();
            cps.WriteBytes(new byte[] { 0x21, 0x40});
            Thread.Sleep(200);
            repeat:
            cps.WriteCmdAndReadBytesArr(new byte[] { 0xE1 } , rsltArr);
            Thread.Sleep(100);

            r = rsltArr[1] * 256 + rsltArr[0];
            if (r == 0xfffe) goto repeat;
            cps.ClosePort();
            r = r/currentRanges[rangeId].KK + currentRanges[rangeId].BV;
            return r;
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
                range.MaxValue = 49152;
                range.RangeTitle = "Диапазон 1";
                range.RangeCommand = 0x20;//, 0x60
                range.UnitId = UnitSerialNumber.ToString();
                range.NeedConvertResult = true;
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
                range.MinValue = 2600;
                range.MaxValue = 0;
                range.RangeTitle = "Диапазон 2";
                range.RangeCommand = 0x00;//, 0x60
                range.NeedConvertResult = true;
                return range;
            }
        }


        #endregion
    }
}
