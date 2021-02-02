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
        protected int MemorySize;
        protected ushort DeviceTypeAddr;
        protected ushort DeviceSerialYearAddr;
        protected ushort DeviceSerialNumAddr;
        protected ushort DeviceModelVersionAddr;
        protected ushort DeviceWorkStatusAddr;
        protected ushort PCModeFlagAddr;
        protected ushort MeasureLineNumberAddr;
        protected ushort MeasureStartFlagAddr;
        protected ushort MeasureStatusAddr;


        string comPortName;

        public DeviceCommandProtocol()
        {
            InitAddressMap();
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
                //Debug.WriteLine($"DeviceType on {port_name}: {info.type} {info.SerialYear}-{info.SerialNumber} v.{info.ModelVersion}");
            }
            catch(Exception ex)
            {
               // Debug.WriteLine(ex.Message);
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

        public DeviceInfo GetDeviceInfo()
        {
            ushort[] data = ReadHoldings(DeviceTypeAddr, DeviceWorkStatusAddr);
            DeviceInfo info = new DeviceInfo();
            info.PortName = comPortName;
            if (data.Length == 5)
            {
                info.type = DeviceBase.IsAllowedDeviceType((byte)data[0]) ? (DeviceType)data[0] : DeviceType.Unknown;
                info.SerialYear = data[1];
                info.SerialNumber = (byte)data[2];
                info.ModelVersion = (byte)data[3];
                info.WorkStatus = (DeviceWorkStatus)(data[4]);
            }else
            {
                info.type = DeviceType.Unknown;
            }
            return info;
        }

        public ushort MeasureStatus
        {
            get
            {
                return ReadSingleHolding(MeasureStartFlagAddr);
            }
        }

        public virtual bool GetPCModeFlag()
        {
            return ReadBoolValue(PCModeFlagAddr);
        }

        public virtual short GetMeasureLineNumber()
        {
            return (short)ReadSingleHolding(MeasureLineNumberAddr);
        }

        public virtual void SetPCModeFlag(bool value)
        {
            ushort val = (value) ? (ushort)1 : (ushort)0;
            WriteSingleHolding(PCModeFlagAddr, val);
        }

        public virtual void SetMeasureLineNumber(int value)
        {
            WriteSingleHolding(MeasureLineNumberAddr, (ushort)value);
        }

        protected bool ReadBoolValue(ushort addr)
        {
            ushort value = ReadSingleHolding(addr);
            return (value > 0);
        }

        protected ushort ReadSingleHolding(ushort addr)
        {
            ushort[] value = ReadHoldings(addr, addr);
            return ((value.Length > 0) ? value[0] : (ushort)0);
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

        protected virtual void InitAddressMap()
        {
            DeviceTypeAddr = 0x0000;
            DeviceSerialYearAddr = 0x0001;
            DeviceSerialNumAddr = 0x0002;
            DeviceModelVersionAddr = 0x0003;
            DeviceWorkStatusAddr = 0x0004;
            PCModeFlagAddr = 0x0080;
            MeasureLineNumberAddr = 0x0081;
            MeasureStartFlagAddr = 0x0082;
        }

        public float GetFloatFromUSHORT(ushort hight, ushort low)
        {
            byte[] bytes = new byte[4];
            bytes[0] = (byte)(hight & 0xFF);
            bytes[1] = (byte)(hight >> 8);
            bytes[2] = (byte)(low & 0xFF);
            bytes[3] = (byte)(low >> 8);
            float value = BitConverter.ToSingle(bytes, 0);
            return value;
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
