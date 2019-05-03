using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO.Ports;
using System.Diagnostics;
using System.Threading;

namespace NormaMeasure.Devices.SAC
{
    public delegate void SACTable_Handler(SACTable table);
    public delegate void TableReceiverControl_Thread_Handler(byte[] receivedInfo);
    public class SACTable : DeviceBase
    {
        int tableNumber = 1;
        TableReceiverControl_Thread Receiver;
        public SACTable(int table_number) : base()
        {
            deviceTypeName = "Стол";
            tableNumber = table_number;
            deviceId = table_number.ToString();
        }

        private void DevicePort_ErrorReceived(object sender, SerialErrorReceivedEventArgs e)
        {
            Debug.WriteLine($"SACTable.DevicePort_DataReceived1: DevicePort_ErrorReceived");
        }

       
        protected override bool CheckConnection()
        {
            byte[] RxData = new byte[5];
            if (!SendCommand(0x00, RxData)) return false;
            if (RxData[2] != TABLE_NUM_INPUT_CMD) return false;
            return (RxData[3] == RxData[4] && RxData[3] == tableNumber);
        }


        protected override void ConfigureDevicePort()
        {
            base.ConfigureDevicePort();
            DevicePort.BaudRate = 115200;
            DevicePort.DataBits = 8;
            DevicePort.PortName = "COM2";
            DevicePort.ReadTimeout = 300;
            Receiver = new TableReceiverControl_Thread(this);
            Receiver.NewCommandReceived += Receiver_NewCommandReceived;
        }

        private void Receiver_NewCommandReceived(byte[] receivedInfo)
        {
            byte cmd = receivedInfo[1];
            byte dataLength = receivedInfo[0];
            string s = string.Empty;
            foreach (byte b in receivedInfo) s += $"{b.ToString("X")} ";
            Debug.WriteLine($"SACTable.Receiver_NewCommandReceived: BeforeSwitch. CMD = {cmd.ToString("X")}");
            switch (cmd)
            {
                //Прием серийного номера
                case TABLE_NUM_INPUT_CMD:
                    OnTableNumber_Received?.Invoke(this);
                    System.Windows.Forms.MessageBox.Show("Серийный номер");
                    Debug.WriteLine($"SACTable.Receiver_NewCommandReceived: принят номер стола {receivedInfo[2]}");
                    break;
                //Приём информации по ВСВИ
                case VSVI_INPUT_CMD:
                    OnVSVIInfo_Received?.Invoke(this);
                    System.Windows.Forms.MessageBox.Show("Для ВСВИ");
                    Debug.WriteLine($"SACTable.Receiver_NewCommandReceived: принята информация для ВСВИ: {receivedInfo[2]}");
                    break;
            }
        }


        public bool SendCommand(byte cmd, byte[] RxData = null)
        {
            byte[] data = new byte[] { 0x00 };
            return SendCommand(cmd, data, RxData);
        }

        private bool WriteBytes(byte[] buildedCmd, byte checkSum, byte[] RxBuffer = null)
        {
            bool cmdWasSent = false;
            int RepeadSendingTimes = 10;
            while (Receiver.RxFlag) ;

            if (RxBuffer == null) RxBuffer = new byte[] { 0x00 }; //так как нам всё равно надо читать команду, создаем массив если он не задан в параметре

            Receiver.TxFlag = true; //Чтобы случайно ничего не принять во время отправки
            do
            {
                base.WriteCmdAndReadBytesArr(buildedCmd, RxBuffer);
                cmdWasSent = RxBuffer[0] == checkSum;
            } while (RepeadSendingTimes-- > 0 && !cmdWasSent);
            Receiver.TxFlag = false;

            return cmdWasSent;
        }

        /// <summary>
        /// Оправляет и считывает данные 
        /// </summary>
        /// <param name="buildedCmd">Подготовленная для отправки в стол команда</param>
        /// <param name="checkSum">Проверочная сумма</param>
        /// <param name="RxBuffer">Считываемые данные</param>
        /// <returns></returns>
        private bool WriteCmdAndReadBytesArr(byte[] buildedCmd, byte checkSum, byte[] RxBuffer)
        {
            return WriteBytes(buildedCmd, checkSum, RxBuffer);
        }

        /// <summary>
        /// Отправляет данные в стол с возможностью получения ответной информации
        /// </summary>
        /// <param name="cmd"></param>
        /// <param name="data"></param>
        /// <param name="RxBuffer"></param>
        /// <returns></returns>
        public bool SendCommand(byte cmd, byte[] data, byte[] RxBuffer = null)
        {
            byte[] toSend = new byte[] { };
            byte checkSum = BuildCommand(cmd, data, toSend);
            if (RxBuffer == null)
            {
                return WriteBytes(toSend, checkSum);
            }else
            {
                return WriteCmdAndReadBytesArr(toSend, checkSum, RxBuffer);
            }

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


        public override void Dispose()
        {
            base.Dispose();
            Receiver.Terminated = true;
        }

        public event SACTable_Handler OnTableNumber_Received;
        public event SACTable_Handler OnVSVIInfo_Received;

        public const byte TABLE_NUM_INPUT_CMD = 0x14;
        public const byte VSVI_INPUT_CMD = 0x10;
    }

    /// <summary>
    /// Поток постоянной проверки принимаемой от стола информации
    /// </summary>
    class TableReceiverControl_Thread : IDisposable
    {
        public bool Terminated = false;
        public bool RxFlag = false;
        public bool TxFlag = false;
        public int RxBufferCurPoint = 0;
        private byte[] RxBuffer = new byte[512];



        public TableReceiverControl_Thread(SACTable _table)
        {
            table = _table;
            thread = new Thread(LinkThreadFunction);
            thread.Start();
        }

        private void LinkThreadFunction()
        { 
            do
            {
                RxFlag = false;
                while (TxFlag);
                RxFlag = true;
                if (ReadBytes())
                {
                    if (CheckReceivedAndSendAnswer())
                    {
                        NewCommandReceived?.Invoke((byte[])RxBuffer.Clone());
                    }
                }
            } while (!Terminated);
        }


        private bool ReadBytes()
        {
            byte dByte = 0;
            RxBufferCurPoint = 0;
            int cmdSize = 512;
            while (table.ReadByte(ref dByte) && --cmdSize>0)
            {
                if (RxBufferCurPoint == 0) cmdSize = dByte+2;
                RxBuffer[RxBufferCurPoint++] = dByte;
            }
            return (RxBufferCurPoint>0);
        }

        /// <summary>
        /// Проверяем то, что получили и отправляем ответ столу
        /// </summary>
        /// <returns></returns>
        private bool CheckReceivedAndSendAnswer()
        {
            byte msgCS = RxBuffer[RxBufferCurPoint - 1];
            byte calcCS = 0;
            byte length = RxBuffer[0];
            for (int i = 0; i < length; i++) calcCS += RxBuffer[i + 2];
            table.WriteBytes(new byte[] { calcCS });
            Debug.WriteLine($"TableReceiverControl_Thread.CheckReceivedAndSendAnswer: Сумма принята: {msgCS.ToString("X")}; Посчитана: {calcCS.ToString("X")}");
            return (calcCS == msgCS);
        }

        public void Dispose()
        {
            Terminated = true;
        }

        ~TableReceiverControl_Thread()
        {
            thread.Abort();

        }
        private Thread thread;
        private SACTable table;
        public event TableReceiverControl_Thread_Handler NewCommandReceived;
    }
}
