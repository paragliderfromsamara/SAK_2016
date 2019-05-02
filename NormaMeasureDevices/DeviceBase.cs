using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO.Ports;
using System.Threading;
using System.Diagnostics;

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
            ConfigureDevicePort(); //Конфигурируем COM порт устройства
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
        /// Отправка команды с открытием/закрытием порта
        /// </summary>
        /// <param name="command">Передаваемая команда</param>
        /// <returns></returns>
        public void WriteBytes(byte[] command, bool needToClose = false)
        {
            OpenPort();
            device_port.Write(command, 0, command.Length);
            if (needToClose) ClosePort();
        }

        public void OpenPort()
        {
            if (!IsOpen) device_port.Open();
        }

        public void ClosePort()
        {
            if (IsOpen) device_port.Close();
        }

        /// <summary>
        /// Принимаем массив данных 
        /// </summary>
        /// <param name="bufferSize">Размер буфера</param>
        /// <returns></returns>
        public bool ReadBytes(byte[] buffer, bool needToClose = false)
        {
            bool f = true;
            device_port.ReadTimeout = 500;
            OpenPort();
            try
            {
                device_port.Read(buffer, 0, buffer.Length);
            }
            catch (TimeoutException)
            {
                f = false;
            }
            if (!needToClose) ClosePort();
            _RxFlag = false;
            return f;

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
        public virtual void WriteCmdAndReadBytesArr(byte[] cmd, byte[] buffer)
        {
            OpenPort();
            WriteBytes(cmd);
            ReadBytes(buffer);
            ClosePort();
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
                ClosePort();
                device_port.PortName = s;
                try
                {
                    if (CheckConnection())
                    {
                        f = true;
                        break;
                    }
                }
                catch (TimeoutException)
                {
                    ClosePort();
                    continue;
                }catch(System.IO.IOException)
                {
                    ClosePort();
                    continue;
                }catch(System.UnauthorizedAccessException)
                {
                    ClosePort();
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
                ClosePort();
                WriteCmdAndReadBytesArr(FindDevice_cmd, buffer);
                f = CheckConnectionResult(buffer);
            }
            catch (TimeoutException)
            {
                ClosePort();
            }
            catch(System.IO.IOException)
            {
                ClosePort();
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
            DevicePort.DataReceived += DevicePort_DataReceived;
        }

        protected virtual void DevicePort_DataReceived(object sender, SerialDataReceivedEventArgs e)
        {
            
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

        public event Device_Handler Device_Connected;
        public event Device_Handler Device_LostConnection;
        public event Device_Handler Device_NotFound;
        public event Device_Handler Device_Finding;
        public event Device_HandlerExceptions OnFindingException;
        public event Device_Handler OnDataReceive;

    }
}
