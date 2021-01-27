using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.IO.Ports;
using Modbus.Device;
using System.Diagnostics;

namespace NormaMeasure.Devices
{
    public class DeviceCommandProtocol : IDisposable
    {
        int MemorySize;
        ushort DeviceTypeAddr = 0x0000;
        ushort DeviceSerialYearAddr = 0x0001;
        ushort DeviceSerialNumAddr = 0x0002;
        ushort DeviceModelVersionAddr = 0x0003;
        ushort DeviceWorkStatusAddr = 0x0004;

        string comPortName;

        public DeviceCommandProtocol()
        {

        }

        public DeviceCommandProtocol(string port_name) : this()
        {
            comPortName = port_name;
        }

        public virtual DeviceBase GetDeviceOnCOM(string port_name)
        {
            DeviceBase device = null;
            DeviceInfo info;
            comPortName = port_name;
            try
            {
                info = GetDeviceInfo();
                if (info.type != DeviceType.Unknown) device = DeviceBase.CreateFromDeviceInfo(info);
                Debug.WriteLine($"DeviceType on {port_name}: {info.type} {info.SerialYear}-{info.SerialNumber} v.{info.ModelVersion}");
            }
            catch(Exception ex)
            {
                Debug.WriteLine(ex.Message);
            }
            return device;
        }

        private SerialPort InitPort(string port_name)
        {
            SerialPort port = new SerialPort(port_name);
            port.BaudRate = 57600;
            port.StopBits = StopBits.One;
            port.Parity = Parity.None;
            port.DataBits = 8;
            port.WriteTimeout = 20;
            port.ReadTimeout = 20;
            return port;
        }

        public virtual DeviceInfo GetDeviceInfo()
        {
            ushort[] data = ReadHoldings(DeviceTypeAddr, DeviceModelVersionAddr);
            DeviceInfo info = new DeviceInfo();
            info.PortName = comPortName;
            if (data.Length == 4)
            {
                info.type = DeviceBase.IsAllowedDeviceType((byte)data[0]) ? (DeviceType)data[0] : DeviceType.Unknown;
                info.SerialYear = data[1];
                info.SerialNumber = (byte)data[2];
                info.ModelVersion = (byte)data[3];
            }else
            {
                info.type = DeviceType.Unknown;
            }
            return info;
        }

        protected ushort[] ReadHoldings(ushort addr_start, ushort addr_end)
        {
           
            SerialPort port = InitPort(comPortName);
            ushort length = (ushort)(addr_end - addr_start+1);
            ushort[] value;
            try
            {
                port.Open();
                ModbusSerialMaster m = ModbusSerialMaster.CreateRtu(port);
                value = m.ReadHoldingRegisters(0, addr_start, length);
                port.Close();
            }
            catch(Exception ex)
            {
                value = new ushort[] { };
                port.Dispose();
                throw ex;
            }
            return value;
        }

        protected void WriteMultipleHoldings(ushort address, ushort[] data)
        {
            SerialPort port = InitPort(comPortName);
            try
            {
                port.Open();
                ModbusSerialMaster m = ModbusSerialMaster.CreateRtu(port);
                m.WriteMultipleRegisters(0, address, data);
                port.Close();
            }
            catch (Exception ex)
            {
                port.Dispose();
                throw ex;
            }
        }

        protected void WriteSingleHolding(ushort address, ushort value)
        {
            SerialPort port = InitPort(comPortName);
            try
            {
                port.Open();
                ModbusSerialMaster m = ModbusSerialMaster.CreateRtu(port);
                m.WriteSingleRegister(0, address, value);
                port.Close();
            }
            catch (Exception ex)
            {
                port.Dispose();
                throw ex;
            }
        }


        public void Dispose()
        {
           
        }

        ~DeviceCommandProtocol()
        {
            Dispose();
        }
    }
}
