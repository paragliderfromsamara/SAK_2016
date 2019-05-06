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
           // Init_DeviceFounderTimer();
            deviceTypeName = "DeviceBase";
        }

        protected virtual void FillDeviceCommands() { }

        


        /// <summary>
        /// Отправка команды с открытием/закрытием порта
        /// </summary>
        /// <param name="command">Передаваемая команда</param>
        /// <returns></returns>
        public void WriteBytes(byte[] command)
        {
            //repeat:
            OpenPort();
            try
            {
                device_port.Write(command, 0, command.Length);
            }catch(System.IO.IOException)
            {
                //goto repeat;
            }
            catch(System.InvalidOperationException)
            {
               // goto repeat;
            }

        }

        public bool OpenPort()
        {
            bool flag = false;
            try
            {
                if (!IsOpen)
                {
                    device_port.Open();
                    flag = true;
                }
            }
            catch(System.InvalidOperationException ex)
            {
                connected = false;
                Debug.WriteLine($"DeviceBase.OpenPort(): InvalidOperationException.ErrCode = {ex.HResult}");
            }
            catch (System.UnauthorizedAccessException)
            {
                //connected = false;
            }
            catch (System.IO.IOException)
            {
                connected = false;
            }
            return flag;
        }

        private void ClosePort()
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
            }catch(System.IO.IOException)
            {
                f = false;
                //goto repeat;
            }
            catch(System.InvalidOperationException)
            {
                f = false;
                //goto repeat;
            }
            catch (TimeoutException)
            {
                f = false;
            }
            _RxFlag = false;
            return f;
        }

        public bool ReadByte(ref byte dataByte)
        {
            try
            {
                OpenPort();
                if (DevicePort.BytesToRead > 0)
                {
                    dataByte = (byte)device_port.ReadByte();
                    return true;
                }
                else return false;
            }
            catch (TimeoutException)
            {
                return false;
            }catch(System.IO.IOException)
            {
                return false;
            }catch(System.InvalidOperationException)
            {
                return false;
            }
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
            WriteBytes(cmd);
            ReadBytes(buffer);
        }

        private bool AttemptToConnection()
        {
            string[] port_list;
            bool flag = false;
            string portNameWas = PortName;
            isOnFinding = true;
            repeat:
            port_list = SerialPort.GetPortNames();
            if (port_list.Length == 0)
            {
                System.Windows.Forms.DialogResult r =  System.Windows.Forms.MessageBox.Show("На компьютере отсутствуют доступные COM порты!", "Ошибка связи", System.Windows.Forms.MessageBoxButtons.RetryCancel, System.Windows.Forms.MessageBoxIcon.Information);
                if (r == System.Windows.Forms.DialogResult.Retry) goto repeat;
            } else
            {
                if (port_list.Contains(PortName)) flag = TryConnectToPort(PortName);
                if (!flag)
                {
                    foreach (string s in port_list)
                    {
                        if (s == portNameWas) continue;
                        if (flag = TryConnectToPort(s)) break;
                    }
                }

            }
            isOnFinding = false;
             
            Debug.WriteLine($"{DeviceType} status {flag}");
            return flag;
        }

        private bool TryConnectToPort(string port_name)
        {
            bool flag = false;
            try
            {
                ClosePort();
                device_port.PortName = port_name;
                if (OpenPort())
                {
                    if (CheckConnection())
                    {
                        flag = true;
                    }
                    else
                    {
                        ClosePort();
                    }
                }
            }
            catch (System.UnauthorizedAccessException)
            {
                ClosePort();
            }
            catch (System.IO.IOException)
            {
                ClosePort();
            }
            catch (TimeoutException)
            {
                ClosePort();
            }
            return flag;
        }

        /// <summary>
        /// Поиск устройства 
        /// </summary>
        public virtual void Find()
        {
            if (!IsConnected && !IsOnFinding)
            {
                connected = AttemptToConnection();
            }
        }


        /// <summary>
        /// Проверка соединения
        /// </summary>
        /// <returns></returns>
        protected virtual bool CheckConnection()
        {
            byte[] buffer = new byte[] { 0x00, 0x00 };
            WriteCmdAndReadBytesArr(FindDevice_cmd, buffer);
            return CheckConnectionResult(buffer);
        }

        protected virtual bool CheckConnectionResult(byte[] result)
        {
            return true;
        }

        /// <summary>
        /// Проверка текущего соединения
        /// </summary>
        /// <returns></returns>
        public void CheckCurrentConnection(object obj)
        {
            if (remainConnectionAttempting-- > 0)
            {
                Device_Finding?.Invoke(this);
                if (AttemptToConnection())
                {
                    connected = true;
                    Debug.WriteLine($"DeviceBase.CheckCurrentConnection: подключено {DeviceType} {deviceId}");
                }
                else
                {
                    if (remainConnectionAttempting == 0) Device_NotFound?.Invoke(this);
                    Debug.WriteLine($"DeviceBase.CheckCurrentConnection: не удалось найти {DeviceType} {deviceId}");
                }
            }
        } 

        private void Init_DeviceFounderTimer()
        {
            TimerCallback tm = new TimerCallback(CheckCurrentConnection);
            //overallMeasureTime = 0;
            remainConnectionAttempting = TimesAttemptingForConnection;
            DeviceFinderTimer = new Timer(tm, 0, 0, 1000);
        }

        private void Dispose_DeviceFounderTimer()
        {
            DeviceFinderTimer.Dispose();
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
            components?.Dispose();
            DeviceFinderTimer?.Dispose();

        }


        private System.ComponentModel.IContainer components = null;

        public string PortName => device_port.PortName;
        public bool IsOpen => device_port.IsOpen;
        private SerialPort device_port;
        public SerialPort DevicePort => device_port;

        private bool findingStatus = false;
        public bool IsOnFinding => isOnFinding;
        private bool isOnFinding
        {
            set
            {
                bool was = findingStatus;
                findingStatus = value;
                if (was && !findingStatus)
                {
                    if (!IsConnected) Device_NotFound?.Invoke(this);
                }
                else if (!was && findingStatus)
                {
                    Device_Finding?.Invoke(this);
                }
            }get
            {
                return findingStatus;
            }
        }

        public bool IsConnected => isConnected;
        private bool isConnected = false;
        private bool connected
        {
            set
            {
                bool valWas = isConnected;
                isConnected = value;

                if (valWas && !value)
                {
                    //Init_DeviceFounderTimer();
                    Device_LostConnection?.Invoke(this);
                }
                else if (!valWas && value)
                {
                    //Dispose_DeviceFounderTimer();
                    Device_Connected?.Invoke(this);
                    ClosePort();
                }
            }
        }

        const int CHECK_LINK_INTERVAL = 10; 
        int NotUsingTimer = 0;

        protected string deviceId = string.Empty;
        protected string deviceTypeName;
        public string DeviceId => deviceId;
        public string DeviceType => deviceTypeName;

        /// <summary>
        /// Количество попыток установить соединение
        /// </summary>
        private const int TimesAttemptingForConnection = 3;

        /// <summary>
        /// Остаток попыток установки соединения
        /// </summary>
        private int remainConnectionAttempting;

        protected bool _RxFlag = false;

        protected byte[] FindDevice_cmd = new byte[] {0x00 };


        private Timer DeviceFinderTimer;
        public event Device_Handler Device_Connected;
        public event Device_Handler Device_LostConnection;
        public event Device_Handler Device_NotFound;
        public event Device_Handler Device_Finding;
        public event Device_HandlerExceptions OnFindingException;
        public event Device_Handler OnDataReceive;
       

    }


}
