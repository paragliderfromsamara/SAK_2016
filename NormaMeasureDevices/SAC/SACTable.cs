using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO.Ports;
using System.Diagnostics;
using System.Threading;
using NormaMeasure.Devices.SAC.SACUnits;
using NormaMeasure.Utils;

namespace NormaMeasure.Devices.SAC
{
    public delegate void SACTable_Handler(SACTable table);
    public delegate void TableReceiverControl_Thread_Handler(byte[] receivedInfo);
    public class SACTable : DeviceBaseOld
    {
        public int TableNumber = 1;

        SAC_Device sac;
        TableReceiverControl_Thread Receiver;
        public SACTableSelfCorrFile SelfCorrFile;


        public SACTable(int table_number, SAC_Device _sac) : base()
        {
            sac = _sac;
            deviceTypeName = "Стол";
            TableNumber = table_number;
            SelfCorrFile = new SACTableSelfCorrFile(this);
            deviceId = table_number.ToString();
            PairCommutator = new PairCommutator(this);
            USICommutator = new USICommutator(this);
            DDSGenerator = new FrequencyGenerator(this);
            this.Device_Connected += SACTable_Device_Connected;
        }

        protected override string GetLastConnectedPortName()
        {
            if (sac == null) return "COM1";
            else return sac.SettingsFile.GetTablePortName(TableNumber);
        }

        private void SACTable_Device_Connected(DeviceBaseOld device)
        {
            sac.SettingsFile.SetTablePortName(this.TableNumber, PortName);
        }

        public bool SetTableForMeasurePoint(SACMeasurePoint current_point)
        {
            bool flag = true;
            flag &= USICommutator.SetUSICommutatorState(current_point);
            flag &= PairCommutator.SetCableCommutatorState_ForCurrentPoint(current_point);
            return flag;
        }

        public bool ClearCableCommutator()
        {
            return PairCommutator.SetAllPairTo(ComTablePairConncectionState.spNONE);
        }

        public override void Find()
        {
            TableNumberIsValid = false;
            base.Find();
        }

        protected override bool CheckConnection()
        {
            int timesForChecking = 10;
            if (!SendCommand(0x00)) return false;
            do
            {
                Thread.Sleep(100);
                if (TableNumberIsValid) break;
            } while (timesForChecking-- > 0);
            Debug.WriteLine($"SACTable.CheckConnection { TableNumberIsValid} {timesForChecking}");
            return TableNumberIsValid;
        }


        protected override void ConfigureDevicePort()
        {
            base.ConfigureDevicePort();
            DevicePort.BaudRate = 115200;
            DevicePort.DataBits = 8;
            DevicePort.ReadTimeout = 1000;
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
                    if (!TableNumberIsValid) TableNumberIsValid = (int)receivedInfo[2] == TableNumber;
     
                        
                    Debug.WriteLine($"SACTable.Receiver_NewCommandReceived: принят номер стола {receivedInfo[2]}");
                    break;
                //Приём информации по ВСВИ
                case VSVI_INPUT_CMD:
                    OnVSVIInfo_Received?.Invoke(this);
                    if (receivedInfo[2] == 0x11) System.Windows.Forms.MessageBox.Show("Система в режиме высоковольтных испытаний!!!", "Высоковольтные испытания", System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Exclamation);
                    s = String.Empty;
                    //System.Windows.Forms.MessageBox.Show("Для ВСВИ");
                    for (int i = 0; i < dataLength; i++) s += $"{receivedInfo[i+2].ToString("X")} ";
                    Debug.WriteLine($"SACTable.Receiver_NewCommandReceived: принята информация для ВСВИ: {s}");
                    break;
            }
        }

        private bool WriteBytes(byte[] buildedCmd, byte checkSum)
        {
            bool cmdWasSent = false;
            byte[] tmpRx = new byte[] { 0x00 }; 
            int RepeatSendingTimes = 10;

            Receiver.TxFlag = true; //Чтобы случайно ничего не принять во время отправки
            while (Receiver.RxFlag) ;

            do
            {
                base.WriteCmdAndReadBytesArr(buildedCmd, tmpRx);
                cmdWasSent = tmpRx[0] == checkSum;
                Thread.Sleep(200);
            } while (RepeatSendingTimes-- > 0 && !cmdWasSent);

            Receiver.TxFlag = false;
            return cmdWasSent;
        }


        /// <summary>
        /// Отправляет данные в стол с возможностью получения ответной информации
        /// </summary>
        /// <param name="cmd"></param>
        /// <param name="data"></param>
        /// <returns></returns>
        public bool SendCommand(byte cmd, byte[] data)
        {
            byte[] toSend = new byte[] { };
            byte checkSum = BuildCommand(cmd, data, ref toSend);
            return WriteBytes(toSend, checkSum);
        }

        public bool SendCommand(byte cmd, byte data)
        {
            return SendCommand(cmd, new byte[] { data });
        }

        public bool SendCommand(byte cmd)
        {
            return SendCommand(cmd, 0x00);
        }

        protected byte BuildCommand(byte cmdAddr, byte[] data, ref byte[] TxBuffer)
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

        public PairCommutator PairCommutator;
        public USICommutator USICommutator;
        public FrequencyGenerator DDSGenerator;

        private bool TableNumberIsValid = false;
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

    public class SACTableSelfCorrFile
    {
        private SACTable table;
        private IniFile file;
        public SACTableSelfCorrFile(SACTable _table)
        {
            table = _table;
            InitFile();
        }

        public double GetInfluenceCoeff(int int_freq, int wave_res, double defaultValue = 45)
        {
            string section = $"al<{wave_res.ToString()}>";
            string key = $"N{int_freq}";
            double val = defaultValue;
            string strVal = String.Empty;
            bool flag = false;
            if (file.KeyExists(key, section))
            {
                strVal = file.Read(key, section);
                flag = double.TryParse(strVal, out val);
            }
            if (!flag) file.Write(key, val.ToString(), section);
            return val;
        }

        private void InitFile()
        {
            file = new IniFile($"SelfCorr(N{table.TableNumber}).ini");
        }
    }

}
