using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.IO.Ports;
using Modbus.Device;
using System.Diagnostics;

namespace NormaLib.Devices
{
    public class DeviceCommandProtocolException : Exception
    {
        public DeviceCommandProtocolException(string message, Exception inner) : base(message, inner)
        {

        }
    } 
    public class DeviceCommandProtocol : IDisposable
    {
        const int RETRY_SENDING_TIMES = 50;

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
        protected ushort MeasureRangeIdAddr;


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

        public ushort MeasureRangeId
        {
            get
            {
                return ReadSingleHolding(MeasureRangeIdAddr);
            }set
            {
                WriteSingleHolding(MeasureRangeIdAddr, value);
            }
        }

        public int WorkStatus
        {
            get
            {
                int ws = ReadSingleHolding(DeviceWorkStatusAddr);
                return ws; 
            }
        }

        public bool MeasureStartFlag
        {
            get
            {
                return ReadBoolValue(MeasureStartFlagAddr);
            }
            set
            {
                WriteBoolValue(MeasureStartFlagAddr, value);
            }
        }

        //public virtual bool GetPCModeFlag()
        //{
        //    return ReadBoolValue(PCModeFlagAddr);
        //}

        //public virtual short GetMeasureLineNumber()
        //{
        //    return (short)ReadSingleHolding(MeasureLineNumberAddr);
        //}

        public short MeasureLineNumber
        {
            get
            {
                return (short)ReadSingleHolding(MeasureLineNumberAddr);
            }
            set
            {
                WriteSingleHolding(MeasureLineNumberAddr, (ushort)value);
            }
        }

        public bool PCModeFlag
        {
            get
            {
                return ReadBoolValue(PCModeFlagAddr);
            }
            set
            {
                WriteBoolValue(PCModeFlagAddr, value);
            }
        }
        //public virtual void SetPCModeFlag(bool value)
        //{
        //    ushort val = (value) ? (ushort)1 : (ushort)0;
        //    WriteBoolValue(PCModeFlagAddr, value);
        //}

        //public virtual void SetMeasureLineNumber(int value)
        //{
        //    WriteSingleHolding(MeasureLineNumberAddr, (ushort)value);
        //}

        protected bool ReadBoolValue(ushort addr)
        {
            ushort value = ReadSingleHolding(addr);
            return (value > 0);
        }

        protected void WriteBoolValue(ushort addr, bool value)
        {
            ushort val = (ushort)((value) ? 1 : 0);
            WriteSingleHolding(addr, val);
        }

        protected ushort ReadSingleHolding(ushort addr)
        {
            ushort[] value = ReadHoldings(addr, addr);
            return ((value.Length > 0) ? value[0] : (ushort)0);
        }

        protected double ReadFloatValue(ushort addr)
        {
            ushort[] arr = ReadHoldings(addr, (ushort)(addr+1));
            return GetFloatFromUSHORT(arr);
        }

        protected void WriteFloatValue(ushort addr, float value)
        {
            byte[] arr = BitConverter.GetBytes(value);
            ushort[] uArr = { 0, 0 };
            uArr[0] = (ushort)(((ushort)arr[3] << (ushort)8) + (ushort)arr[2]);
            uArr[1] = (ushort)(arr[1] << 8);
            WriteMultipleHoldings(addr, uArr);
        }

        protected ushort[] ReadHoldings(ushort addr_start, ushort addr_end)
        {
            int times = RETRY_SENDING_TIMES;
            SerialPort port = null;
            ushort length = (ushort)(addr_end - addr_start+1);
            ushort[] value;
            retry:
            try
            {
                port = InitPort(comPortName);
                port.Open();
                ModbusSerialMaster m = ModbusSerialMaster.CreateRtu(port);
                value = m.ReadHoldingRegisters(0, addr_start, length);
                port.Close();
            }
            catch(Exception ex)
            {
                value = new ushort[] { };
                if (port != null) port.Dispose();
                if (times-- > 0) goto retry;
                throw new DeviceCommandProtocolException($"Ошибка операции ReadHoldings для диапазона адресов {addr_start.ToString("X")}-{addr_end.ToString("X")};", ex);
            }
            return value;
        }

        protected void WriteMultipleHoldings(ushort address, ushort[] data)
        {
            int times = RETRY_SENDING_TIMES;
            SerialPort port = null;
            retry:
            try
            {
                port = InitPort(comPortName);
                port.Open();
                ModbusSerialMaster m = ModbusSerialMaster.CreateRtu(port);
                m.WriteMultipleRegisters(0, address, data);
                port.Close();
            }
            catch (Exception ex)
            {
                if (port != null) port.Dispose();
                if (times-- > 0) goto retry;
                throw new DeviceCommandProtocolException($"Ошибка операции WriteMultipleHoldings для адреса {address.ToString("X")};", ex);
            }
        }

        protected void WriteSingleHolding(ushort address, ushort value)
        {
            int times = RETRY_SENDING_TIMES;
            SerialPort port = null;
            retry:
            try
            {
                port = InitPort(comPortName);
                port.Open();
                ModbusSerialMaster m = ModbusSerialMaster.CreateRtu(port);
                m.WriteSingleRegister(0, address, value);
                port.Close();
            }
            catch (Exception ex)
            {
                if (port != null) port.Dispose();
                if (times-- > 0) goto retry;
                throw new DeviceCommandProtocolException($"Ошибка операции WriteSingleHolding для адреса {address.ToString("X")};", ex);
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
            MeasureStatusAddr = 0x0083;
            MeasureRangeIdAddr = 0x0047;
        }

        public float GetFloatFromUSHORT(ushort[] valArr)
        {
            return GetFloatFromUSHORT(valArr[1], valArr[0]);
        }

        public float GetFloatFromUSHORT(ushort hight, ushort low)
        {
            byte[] bytes = new byte[4];
            bytes[0] = 0;
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
