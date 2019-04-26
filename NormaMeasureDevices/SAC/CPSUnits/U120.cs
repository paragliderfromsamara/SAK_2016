using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NormaMeasure.DBControl.Tables;

namespace NormaMeasure.Devices.SAC.CPSUnits
{
    public class U120 : CPSMeasureUnit
    {
        public U120(SACCPS _cps) : base(_cps)
        {
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
            base.SetUnitInfo();
            unitName = "U120";
            unitTitle = "Узел емкостных параметров CEK";
        }
    }
}
