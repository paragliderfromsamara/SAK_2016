using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO.Ports;

namespace NormaMeasure.Devices
{
    public delegate void Device_Handler(DeviceBase device);

    public delegate void Device_HandlerExceptions(DeviceBase device, Exception ex);

    public class DeviceBase : IDisposable
    {

        public DeviceBase() : base()
        {
            components = new System.ComponentModel.Container();
            device_port = new SerialPort(components);
            FillDeviceCommands(); //Заполняем команды устройства
            ConfigureDevicePort(); //Конфигурируем устройство
            deviceTypeName = "DeviceBase";
            device_port.DataReceived += Device_port_DataReceived;
        }

        private void Device_port_DataReceived(object sender, SerialDataReceivedEventArgs e)
        {
            
        }

        protected virtual void FillDeviceCommands()
        {
           // throw new NotImplementedException();
        }

        /// <summary>
        /// Отправляет команду в серийный порт
        /// </summary>
        /// <param name="command">Отправляемая команда</param>
        /// <param name="needToCheckConnection">Необходима ли проверка подключения перед отправкой</param>
        /// <param name="needToClosePort">Нужно ли закрывать порт после отправки</param>
        public bool Write(byte[] command, bool needToCheckConnection = true, bool needToClosePort = true)
        {
            bool flag = false;
            try
            {
                if (needToCheckConnection) flag = CheckCurrentConnection();
                if (flag)
                {
                    if (!IsOpen) device_port.Open();

                    device_port.Write(command, 0, command.Length);

                    if (needToClosePort) device_port.Close();
                }
                return flag;
            }catch(Exception)
            {
                return false;
            }
        }

        /// <summary>
        /// Принимаем массив данных 
        /// </summary>
        /// <param name="bufferSize">Размер буфера</param>
        /// <returns></returns>
        public int ReadBytes(byte[] buffer, int offset, int count, bool needToClosePort = true)
        {
            int i=0;
            if (!IsOpen) device_port.Open();
            try
            {
                i = device_port.Read(buffer, offset, count);
            }
            catch(TimeoutException)
            {
                connected = false;
            }
            if (needToClosePort && IsOpen) device_port.Close();
            return i;
        }

        /// <summary>
        /// Отправляет и принимает данные с порта
        /// </summary>
        /// <param name="cmd">Команда</param>
        /// <param name="needToCheckConnection">Флаг необходимости проверки соединения</param>
        /// <param name="buffer">Заполняемый массив</param>
        /// <param name="offset">Отступ в массиве</param>
        /// <param name="count">Принимаемое количество байт</param>
        /// <returns></returns>
        public int WriteCmdAndReadBytesArr(byte[] cmd, byte[] buffer, int offset, int count, bool needToCheckConnection = true)
        {
            Write(cmd, needToCheckConnection, false);
            return ReadBytes(buffer, offset, count);
        }

        

        /// <summary>
        /// Побайтный прием
        /// </summary>
        /// <returns></returns>
        public byte ReadByte()
        {
            return (byte)device_port.ReadByte();
        } 

        /// <summary>
        /// Поиск устройства 
        /// </summary>
        public void Find()
        {
            string[] port_list = SerialPort.GetPortNames();
            bool f = false;
            Device_Finding?.Invoke(this);
            foreach (string s in port_list)
            {
                device_port.PortName = s;
                try
                {
                    if (IsOpen) device_port.Close();
                    device_port.Open();
                    device_port.Write(FindDevice_cmd, 0, FindDevice_cmd.Length);

                    if (CheckConnection())
                    {
                        f = true;
                        break;
                    }
                    device_port.Close();
                }
                catch (TimeoutException)
                {
                    if (IsOpen) device_port.Close();
                    continue;
                }catch(System.IO.IOException)
                {
                    if (IsOpen) device_port.Close();
                    continue;
                }catch(System.UnauthorizedAccessException)
                {
                    if (IsOpen) device_port.Close();
                    continue;
                }
            }
            if (!f) Device_NotFound?.Invoke(this);
            connected = f;
        }


        /// <summary>
        /// Проверка соединения
        /// </summary>
        /// <returns></returns>
        protected virtual bool CheckConnection()
        {
            byte[] buffer = new byte[] { 0x00, 0x00 };
            bool f = false;
            try
            {
                if (IsOpen) device_port.Close();
                device_port.Open();
                device_port.Write(FindDevice_cmd, 0, FindDevice_cmd.Length);
                device_port.Read(buffer, 0, buffer.Length);
                f = CheckConnectionResult(buffer);
                device_port.Close();
            }
            catch (TimeoutException)
            {
                if (IsOpen) device_port.Close();
            }catch(System.IO.IOException)
            {
                if (IsOpen) device_port.Close();
            }
            return f;
        }

        protected virtual bool CheckConnectionResult(byte[] result)
        {
            return true;
        }

        /// <summary>
        /// Проверка текущего соединения
        /// </summary>
        /// <returns></returns>
        public bool CheckCurrentConnection()
        {
            connected = CheckConnection();
            return IsConnected;
        } 

        /// <summary>
        /// Установка характеристик порта - скорость, четность и прочее
        /// </summary>
        protected virtual void ConfigureDevicePort()
        {
            DevicePort.StopBits = StopBits.One;
            DevicePort.Parity = Parity.None;
            DevicePort.DataBits = 8;
            DevicePort.PortName = "COM1";
        }

        ~DeviceBase()
        {
            Dispose();
        }

        //protected override Dis

        public virtual void Dispose()
        {
            if (components != null) components.Dispose();
        }


        private System.ComponentModel.IContainer components = null;

        public string PortName => device_port.PortName;
        public bool IsOpen => device_port.IsOpen;
        private SerialPort device_port;
        protected SerialPort DevicePort => device_port;

        public bool IsConnected => isConnected;
        private bool isConnected = false;
        private bool connected
        {
            set
            {
                bool valWas = isConnected;
                isConnected = value;

                if (valWas && !value) Device_LostConnection?.Invoke(this);
                else if (!valWas && value) Device_Connected?.Invoke(this);
            }
        }

        protected string deviceId = string.Empty;
        protected string deviceTypeName;
        public string DeviceId => deviceId;
        public string DeviceType => deviceTypeName;

        protected byte[] FindDevice_cmd = new byte[] {0x00 };

        private byte[] receiverBuffer = new byte[64];

        public event Device_Handler Device_Connected;
        public event Device_Handler Device_LostConnection;
        public event Device_Handler Device_NotFound;
        public event Device_Handler Device_Finding;
        public event Device_HandlerExceptions OnFindingException;
        public event Device_Handler OnDataReceive;
    }
}
