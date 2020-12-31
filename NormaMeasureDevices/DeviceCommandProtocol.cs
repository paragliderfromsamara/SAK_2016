using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.IO.Ports;
using Modbus.Device;

namespace NormaMeasure.Devices
{
    public class DeviceCommandProtocol : IDisposable
    {
        int MemorySize;
        ushort DeviceProdYear_Addr = 0x00FC;
        ushort DeviceTypeAndProdNumber_Addr = 0x00FE;

        string comPortName;
        SerialPort port;
        public DeviceCommandProtocol(string port_name)
        {
            InitPort(port_name);
        }

        public DeviceBase GetDeviceOnCOM(string port_name)
        {

        }

        private SerialPort InitPort(string port_name)
        {
            SerialPort port = new SerialPort(port_name);
            port.BaudRate = 9600;
            port.StopBits = StopBits.One;
            port.Parity = Parity.None;
            port.DataBits = 8;
            return port;
        }

        private DeviceInfo GetDeviceInfo()
        {
            ushort[] data = ReadHoldings(DeviceProdYear_Addr, DeviceTypeAndProdNumber_Addr);
            
        }

        private ushort[] ReadHoldings(ushort addr_start, ushort addr_end)
        {
            ushort length = (ushort)(addr_end - addr_start+1);
            ushort[] value;
            port.Open();
            ModbusSerialMaster m = ModbusSerialMaster.CreateRtu(port);
            value = m.ReadHoldingRegisters(0, addr_start, length);
            port.Close();
            return value;
        }

        public void Dispose()
        {
            port.Dispose();
        }

        ~DeviceCommandProtocol()
        {
            Dispose();
        }
    }
}
