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

        protected override bool CheckConnection()
        {
            byte[] buffer = new byte[] { 0x00, 0x00 };
            bool f = false;
            try
            {
                ClosePort();
                WriteCmdAndReadBytesArr(FindDevice_cmd, buffer);
                f = CheckConnectionResult(buffer);
            }
            catch (TimeoutException)
            {
                ClosePort();
            }
            catch (System.IO.IOException)
            {
                ClosePort();
            }
            return f;
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

        public bool SendCommand(byte cmd, byte[] data)
        {
            byte[] toSend = new byte[] { };
            byte checkSum = BuildCommand(cmd, data, toSend);
            byte[] RxBuffer = new byte[] { 0x00 };
            int RepeadSendingTimes = 10;
            bool cmdWasSent = false;
            do
            {
                WriteCmdAndReadBytesArr(toSend, RxBuffer);
                cmdWasSent = RxBuffer[0] == checkSum;
            } while (RepeadSendingTimes-- > 0 && !cmdWasSent);
            return cmdWasSent;

        }

        protected byte BuildCommand(byte cmdAddr, byte[] data, byte[] TxBuffer)
        {
            byte checkSum = 0x00;
            List<byte> cmd = new List<byte>();
            byte dataLength;
            //Если длина отправляемой последовательности не умещается в один байт то отправляем 0 (Так интерпретирует стол)
            if (data.Length > 255) dataLength = 0;
            //Если ничего не отправляем - выставляем длину равую 
            else if (data.Length == 0) dataLength = 1;
            else dataLength = (byte)data.Length;

            cmd.Add(dataLength);
            cmd.Add(cmdAddr);
            foreach(byte b in data)
            {
                cmd.Add(b);
                checkSum += b; 
            }
            cmd.Add(checkSum);
            TxBuffer = cmd.ToArray();
            return checkSum;
        }
    }
}
