using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NormaMeasure.DBControl.Tables;

namespace NormaMeasure.Devices.SAC.CPSUnits
{
    public class U160 : CPSMeasureUnit
    {
        public U160(SACCPS _cps) : base(_cps)
        {
        }

        protected override void SetUnitAddress()
        {
            unitCMD_Address = 0x03;
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
            base.SetUnitInfo();
            unitName = "U160";
            unitTitle = "Узел параметров влияния";
        }
    }
}
