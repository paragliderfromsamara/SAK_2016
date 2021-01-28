using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using NormaMeasure.Devices.XmlObjects;
using System.Diagnostics;


namespace NormaMeasure.Devices
{
    public delegate void ConnectionTheadDelegate();
    public class DeviceBase : IDisposable
    {
        private static object locker = new object();
        private bool thread_is_active = false;

        protected bool threadIsActive
        {
            get
            {
                return thread_is_active;
            }set
            {
                lock(locker)
                {
                    thread_is_active = value;
                }
            }
        }
        

        private bool pc_mode_flag = false;
        protected bool pcModeFlag
        {
            get
            {
                return pc_mode_flag;
            }set
            {
                if (value != pc_mode_flag)
                {
                    pc_mode_flag = value;
                    if (!pc_mode_flag) client_id = -1;
                    OnPCModeFlagChanged?.Invoke(this, new EventArgs());
                }
            }
        }
        public EventHandler OnPCModeFlagChanged;
        public bool IsOnPCMode => pcModeFlag;
        protected DeviceXMLState xmlState = null;

        public EventHandler OnDisconnected;
        public uint SerialYear => serial_year;
        public uint SerialNumber => serial_number;
        public uint ModelVersion => model_version;
        public string PortName => port_name;
        public string Serial => (serial_number < 10) ? $"{serial_year}-0{serial_number}" : $"{serial_year}-{serial_number}";

        public string SerialWithFullName => $"{type_name_full} зав.№ {Serial}";
        public string SerialWithShortName => $"{type_name_short} зав.№ {Serial}";
        public string TypeNameFull => type_name_full;
        public string TypeNameShort => type_name_short;
        private int _client_id = -1;
        private int client_id
        {
            set
            {
                _client_id = value;
                if (xmlState != null) xmlState.ClientId = value;
            }
        }
        public int ClientId
        {
            get
            {
                return _client_id;
            }
        }
        public DeviceStatus WorkStatus => work_status;


   

        public DeviceType TypeId => type_id;

        protected DeviceType type_id;
        protected DeviceStatus _work_status = DeviceStatus.UNDEFINED;
        Thread ConnectionThread;
        protected ConnectionTheadDelegate OnThreadWillFinish = null;
        protected DeviceStatus work_status
        {
            get
            {
                return _work_status;
            }
            set
            {
                lock(locker)
                {
                    DeviceStatus sts = _work_status;
                    if (sts == value) return;
                    switch (value)
                    {
                        case DeviceStatus.WAITING_FOR_COMMAND:
                            InitConnectionCheckThread();
                            break;
                        case DeviceStatus.DISCONNECTED:
                            threadIsActive = false;
                            OnDisconnected?.Invoke(this, new EventArgs());
                            break;
                    }
                    _work_status = value;
                }
            }
        }

        internal void ReleaseDeviceFromClient()
        {
            client_id = -1;
        }

        private uint serial_year = 2000;
        private uint serial_number = 0;
        private uint model_version = 0;
        private string port_name = "COM1";
        protected string type_name_short;
        protected string type_name_full;


        public static DeviceBase CreateFromDeviceInfo(DeviceInfo info)
        {
            switch(info.type)
            {
                case DeviceType.Teraohmmeter:
                    return new Teraohmmeter.TeraohmmeterTOmM_01(info);
                case DeviceType.Microohmmeter:
                    return new Microohmmeter.Microohmmeter_uOmM_01m(info);
                default:
                    return new DeviceBase(info);
            }
        }
        
        /// <summary>
        /// Привязка прибора к измерительной линии
        /// </summary>
        /// <param name="cl_id"></param>
        public virtual void AssignToClient(int cl_id)
        {
            this.client_id = cl_id;
            InitPCMode();
        }

        protected virtual void CheckDeviceConnectionThreadFunc()
        {
            int tryTimes = 50;
            DeviceCommandProtocol p = null;
            retry: 
            try
            {
                p = new DeviceCommandProtocol(port_name);
                while (threadIsActive)
                {
                    DeviceInfo info = p.GetDeviceInfo();
                    if (DeviceInfoIsValid(info))
                    {
                        work_status = DeviceStatus.DISCONNECTED;
                        break;
                    }
                    Debug.WriteLine($"Попыток на отправку: {tryTimes};");
                    tryTimes = 50;
                    if (threadIsActive) break;
                }
                p.Dispose();
                OnThreadWillFinish?.Invoke();
                Debug.WriteLine($"-----------CheckDeviceConnectionThreadFunc STOPPED--------------");
            }
            catch
            {
                if (p != null) p.Dispose();
                if (tryTimes-- > 0) goto retry;
                work_status = DeviceStatus.DISCONNECTED;
            }
        }

        protected virtual bool DeviceInfoIsValid(DeviceInfo info)
        {
           return (info.type != this.TypeId || info.SerialNumber != this.SerialNumber || info.SerialYear != this.SerialYear || info.ModelVersion != this.ModelVersion);
        }

        internal void GoToWaitingForCommand()
        {
            work_status = DeviceStatus.WAITING_FOR_COMMAND;
        }

        public DeviceBase()
        {

        }

        public void InitPCMode()
        {
            if (!IsOnPCMode)
            {
                InitPCModeCheckThread();
            }
        }

        public void StopPCMode()
        {
            if (IsOnPCMode) InitConnectionCheckThread();
        }

        protected virtual void InitPCModeCheckThread()
        {
            if (threadIsActive)
            {
                SetDeviceConnectionThreadFunction(new ThreadStart(PCModeThreadFunction), InitPCModeCheckThread);
            }
            else
            {
                SetDeviceConnectionThreadFunction(new ThreadStart(PCModeThreadFunction), InitConnectionCheckThread);
            }
        }

        protected virtual void PCModeThreadFunction()
        {
            int tryTimes = 50;
            bool isInited = false;
            DeviceCommandProtocol p = null;
            retry:
            try
            {
                p = new DeviceCommandProtocol(port_name);
                if (!isInited)
                {
                    p.SetPCModeFlag(true);
                    pcModeFlag = p.GetPCModeFlag();
                    p.SetMeasureLineNumber(ClientId);
                    isInited = true; //чтоб больше этого не делать, устанавливаем флажок
                }
                while (threadIsActive)
                {
                    DeviceInfo info = p.GetDeviceInfo();
                    if (DeviceInfoIsValid(info))
                    {
                        work_status = DeviceStatus.DISCONNECTED;
                        break;
                    }
                    if (p.GetMeasureLineNumber() != ClientId) p.SetMeasureLineNumber(ClientId);
                    tryTimes = 50;
                    if (threadIsActive) threadIsActive = p.GetPCModeFlag();
                }
                if (pcModeFlag)
                {
                    p.SetPCModeFlag(false);
                    pcModeFlag = false;
                }
                p.Dispose();
                OnThreadWillFinish?.Invoke();
            }
            catch
            {
                if (p != null) p.Dispose();
                if (tryTimes-- > 0) goto retry;
                pcModeFlag = false;
                work_status = DeviceStatus.DISCONNECTED;
            }
        }

        protected void InitConnectionCheckThread()
        {
            if (threadIsActive)
            {
                SetDeviceConnectionThreadFunction(new ThreadStart(CheckDeviceConnectionThreadFunc), InitConnectionCheckThread);
            }
            else
            {
                SetDeviceConnectionThreadFunction(new ThreadStart(CheckDeviceConnectionThreadFunc));
            }
        }



        protected void SetDeviceConnectionThreadFunction(ThreadStart threadFunction, ConnectionTheadDelegate onThreadWillFinish = null)
        {
            if (threadIsActive)
            {
                if (onThreadWillFinish != null) OnThreadWillFinish = onThreadWillFinish;
                threadIsActive = false;

            }
            else
            {
                OnThreadWillFinish = (onThreadWillFinish != null) ? onThreadWillFinish : null;
                ConnectionThread = new Thread(threadFunction);
                threadIsActive = true;
                ConnectionThread.Start();
            }
        }

        public DeviceBase(DeviceInfo info)
        {
            serial_year = info.SerialYear;
            serial_number = info.SerialNumber;
            model_version = info.ModelVersion;
            port_name = info.PortName;
        }

        public static bool IsAllowedDeviceType(byte typeId)
        {
            return (typeId > 0 && typeId <= (byte)DeviceType.AVU_4);
        }

        public virtual DeviceXMLState GetXMLState()
        {
            if (xmlState == null) xmlState = new DeviceXMLState(this);
            return xmlState;
        }

        public void Dispose()
        {
            OnDisconnected = null;
            OnPCModeFlagChanged = null;
            threadIsActive = false;
            //if (work_status == DeviceStatus.WAITING_FOR_COMMAND) work_status = DeviceStatus.WILL_DISCONNECT;

        }



    }

    public struct DeviceInfo
    {
        public DeviceType type;
        public uint SerialYear;
        public byte SerialNumber;
        public byte ModelVersion;
        public DeviceWorkStatus WorkStatus;
        public string PortName;
    }

    public enum DeviceType : byte
    {
        Unknown = 0,
        Teraohmmeter = 1,
        Microohmmeter = 2,
        PICA_X = 3,
        SAC_TVCH_MEASURE_UNIT = 4,
        SAC_TVCH_COMM_TABLE = 5,
        AVU_4 = 6
    }

    public enum DeviceStatus : byte
    {
        UNDEFINED,
        WAITING_FOR_COMMAND,
        CHANGE_STATUS,
        IS_ON_MEASURE,
        IS_ON_POLARISATION,
        IS_ON_DEPOLARISATION,
        DISCONNECTED, 
        WILL_DISCONNECT
    }

    public enum DeviceWorkStatus : byte
    {
        IDLE = 0, 
        MEASURE = 1,
        CALIBRATION = 2, 
        ENTERING_PARAMETERS = 3,
        POLARIZATION = 4,
        DEPOLARIZATION = 5,
        LOST_CIRCUIT_CORRECTION = 6
    }



    public class NormaDeviceException : Exception
    {
        public NormaDeviceException(string message) : base(message)
        {

        }
    }
}
