﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NormaMeasure.Devices.Teraohmmeter
{
    public class TOhmM_01_v1_CommandProtocol : DeviceCommandProtocol
    {
        public TOhmM_01_v1_CommandProtocol(string port_name) : base(port_name)
        {

        }

        protected override void InitAddressMap()
        {
            base.InitAddressMap();
            PCModeFlagAddr = 0x0092;
            MeasureLineNumberAddr = 0x0093;
        }
    }
}
