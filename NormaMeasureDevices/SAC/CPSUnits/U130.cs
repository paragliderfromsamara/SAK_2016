using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NormaMeasure.DBControl.Tables;

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

        protected override void SetUnitAddress()
        {
            unitCMD_Address = (byte)(0x03 + unitNumber);
        }

        protected override void SetUnitInfo()
        {
            unitName = "U130";
            unitTitle = "Узел измерения сопротивления изоляции";
            base.SetUnitInfo();
        }

        public byte RizolUnitNumber => unitNumber;
        private byte unitNumber;
    }


}
