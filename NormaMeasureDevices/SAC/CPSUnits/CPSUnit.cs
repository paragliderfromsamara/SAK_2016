using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NormaMeasure.Devices.SAC.CPSUnits
{
    public class CPSUnit
    {
        public CPSUnit(SACCPS _cps)
        {
            cps = _cps;
            SetUnitAddress();
        }
        /// <summary>
        /// Установка адреса узла
        /// </summary>
        protected virtual void SetUnitAddress() { }
        protected byte unitCMD_Address;
        protected SACCPS cps;
    }
}
