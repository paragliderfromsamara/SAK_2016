using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Modbus.Device;

namespace NormaMeasure.Devices
{

    public class DevicesDispatcher
    {
        KeyValuePair<string, DeviceBase> connectedDevices;

        public DevicesDispatcher()
        {
            connectedDevices = new KeyValuePair<string, DeviceBase>();
        }

        private void FindDevices()
        {
            string[] port_list =
            port_list = SerialPort.GetPortNames();
        }

        
    }
}
