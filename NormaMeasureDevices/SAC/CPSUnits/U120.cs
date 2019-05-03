﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NormaMeasure.DBControl.Tables;
using System.Diagnostics;

namespace NormaMeasure.Devices.SAC.CPSUnits
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
            if (IsCEKMinus) result = -result;
            Debug.WriteLine($"U120.MakeMeasure():result {result}");
        }


        public U120(SACCPS _cps) : base(_cps)
        {
        }

        protected override bool CheckRange(double result)
        {
            if (result<2 && (CurrentParameterType.IsEK()) && !IsCEKMinus )
            {
                IsCEKMinus = true;
                return false;
            }
            return base.CheckRange(result);
        }

        public override bool MakeMeasure(ref SACMeasurePoint point)
        {
            base.MakeMeasure(ref point);
            int defaultRangeIdx = CurrentParameterType.IsEK() ? 0 : 2;
            Debug.WriteLine($"U120.MakeMeasure():defaultRangeIdx {defaultRangeIdx}");
            SelectDefaultRange(defaultRangeIdx);
            ExecuteElementaryMeasure(ref point);
            return true;
        }

        protected override void SetMeasureModeCMDByParameterType()
        {
            base.SetMeasureModeCMDByParameterType();
            IsCEKMinus = (LeadCommType == LeadCommutationType.B);
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
                range.MinValue = 0;
                range.MaxValue = 480;
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
                range.MinValue = 480;
                range.MaxValue = 4800;
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
                range.MinValue = 4800;
                range.MaxValue = 65500;
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
                range.MinValue = 0;
                range.MaxValue = 65500;
                range.RangeTitle = "Диапазон 1";
                range.RangeCommand = 0x20;//, 0x60
                range.NeedConvertResult = true;
                return range;
            }
        }

        #endregion


    }
}