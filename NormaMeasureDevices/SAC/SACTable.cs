using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NormaMeasure.Devices.SAC
{
    public class SACTable : DeviceBase
    {
        public SACTable() : base()
        {
            deviceTypeName = "Table";
        }

        protected override void ConfigureDevicePort()
        {
            base.ConfigureDevicePort();
            DevicePort.StopBits = System.IO.Ports.StopBits.One;
            DevicePort.Parity = System.IO.Ports.Parity.None;
            DevicePort.BaudRate = 9600;
            DevicePort.DataBits = 8;
            DevicePort.PortName = "COM1";
        }
    }
}
