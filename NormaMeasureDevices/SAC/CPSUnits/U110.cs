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
            base.SetUnitInfo();
            unitName = "U110";
            unitTitle = "Узел произвонки и измерения Rжил";
        }


    }
}
