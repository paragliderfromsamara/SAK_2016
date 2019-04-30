using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NormaMeasure.Devices.SAC
{
    public class SACTable : DeviceBase
    {
        int tableNumber;
        public SACTable(int table_number) : base()
        {
            deviceTypeName = "Стол";
            tableNumber = table_number;
            deviceId = table_number.ToString();
        }

        protected override void ConfigureDevicePort()
        {
            base.ConfigureDevicePort();
            DevicePort.StopBits = System.IO.Ports.StopBits.One;
            DevicePort.Parity = System.IO.Ports.Parity.None;
            DevicePort.BaudRate = 115200;
            DevicePort.DataBits = 8;
            DevicePort.PortName = "COM1";
        }
    }
}
