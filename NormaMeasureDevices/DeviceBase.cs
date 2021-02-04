using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using NormaMeasure.Devices.XmlObjects;
using NormaMeasure.Devices.Teraohmmeter;
using System.Diagnostics;


namespace NormaMeasure.Devices
{
    public class DeviceWorkStatusEventArgs : EventArgs
    {
        public DeviceWorkStatus StatusWas;
        public DeviceWorkStatus StatusNew;
        public DeviceWorkStatusEventArgs(DeviceWorkStatus sts_was, DeviceWorkStatus sts_new)
        {
            StatusWas = sts_was;
            StatusNew = sts_new;
        }
    }

    public class MeasureResultEventArgs : EventArgs
    {
        public double RawValue;
        public double ConvertedValue;
        public uint RangeId;
        public uint MeasureStatus;
        public MeasureResultEventArgs(TeraMeasureResultStruct result)
        {
            RawValue = result.ConvertedValue;
            ConvertedValue = result.ConvertedByModeValue;
            RangeId = result.Range;
            MeasureStatus = result.MeasureStatus;
        }
    }
    public delegate void ConnectionTheadDelegate();
    public class DeviceBase : IDisposable
    {
        public EventHandler OnDisconnected;
        public EventHandler OnPCModeFlagChanged;
        public EventHandler OnWorkStatusChanged;
        public EventHandler OnGetMeasureResult;
        public EventHandler OnMeasureCycleFlagChanged;

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
        public bool IsOnPCMode
        {
            get
            {
                return pc_mode_flag;
            }
            protected set
            {
                if (value != pc_mode_flag)
                {
                    pc_mode_flag = value;
                    if (!pc_mode_flag) ClientId = -1;
                    if (xmlState != null) xmlState.IsOnPCMode = pc_mode_flag;
                    OnPCModeFlagChanged?.Invoke(this, new EventArgs());
                }
            }
        }

        private bool is_on_measure_cycle = false;
        public bool IsOnMeasureCycle
        {
            get
            {
                return is_on_measure_cycle;
            }set
            {
                lock(locker)
                {
                    is_on_measure_cycle = value;
                    if (value != is_on_measure_cycle)
                    {
                        is_on_measure_cycle = value;
                        if (xmlState != null) xmlState.IsOnMeasureCycle = is_on_measure_cycle;
                        OnMeasureCycleFlagChanged?.Invoke(this, new EventArgs());
                    }
                }
            }
        }

        private bool is_connected = false;

        public bool IsConnected
        {
            get
            {
                return is_connected;
            }
            protected set
            {
                lock(locker)
                {
                    if (value != is_connected)
                    {
                        is_connected = value;
                        if (is_connected)
                        {
                            /*When flag is true*/
                            InitConnectionCheckThread();
                        }
                        else
                        {
                            threadIsActive = false;
                            WorkStatus = DeviceWorkStatus.DISCONNECTED;
                            OnDisconnected?.Invoke(this, new EventArgs());
                        }
                    }
                }
               
            }
        }

        public string WorkStatusText
        {
            get
            {
                switch(work_status)
                {
                    case DeviceWorkStatus.DISCONNECTED:
                        return "Отключен";
                    case DeviceWorkStatus.IDLE:
                        return "Ожидает";
                    case DeviceWorkStatus.CALIBRATION:
                        return "Калибровка";
                    case DeviceWorkStatus.ENTERING_PARAMETERS:
                        return "Ввод параметров";
                    case DeviceWorkStatus.DEPOLARIZATION:
                        return "Разряд";
                    case DeviceWorkStatus.POLARIZATION:
                        return "Поляризация";
                    case DeviceWorkStatus.MEASURE:
                        return "Измерение";
                    case DeviceWorkStatus.LOST_CIRCUIT_CORRECTION:
                        return "Корректировка токов утечки";
                    default:
                        return "Неизвестный статус";
                }
            }
        }

        protected DeviceXMLState xmlState = null;

        private double raw_result;
        public double RawResult
        {
            get
            {
                return raw_result;
            }protected set
            {
                raw_result = value;
                if (xmlState != null) xmlState.RawResult = value;
            }
        }
        private double converted_result;
        public double ConvertedResult
        {
            get
            {
                return converted_result;
            }
            protected set
            {
                converted_result = value;
                if (xmlState != null) xmlState.ConvertedResult = value;
            }
        }
        private uint measure_status_id;
        public uint MeasureStatusId
        {
            get
            {
                return measure_status_id;
            }
            protected set
            {
                measure_status_id = value;
                if (xmlState != null) xmlState.MeasureStatusId = value;
            }
        }


        public uint SerialYear => serial_year;
        public uint SerialNumber => serial_number;
        public uint ModelVersion => model_version;
        public string PortName => port_name;
        public string Serial => (serial_number < 10) ? $"{serial_year}-0{serial_number}" : $"{serial_year}-{serial_number}";

        public string SerialWithFullName => $"{type_name_full} зав.№ {Serial}";
        public string SerialWithShortName => $"{type_name_short} зав.№ {Serial}";
        public string TypeNameFull => type_name_full;
        public string TypeNameShort => type_name_short;
        private int client_id = -1;
        public int ClientId
        {
            get
            {
                return client_id;
            }
            protected set
            {
                client_id = value;
                if (xmlState != null) xmlState.ClientId = value;
            }
        }
        // public DeviceStatus WorkStatus => work_status;
        private DeviceWorkStatus work_status = DeviceWorkStatus.DISCONNECTED;
        public DeviceWorkStatus WorkStatus
        {
            get
            {
                return work_status;
            }protected set
            {
                if (work_status != value)
                {
                    DeviceWorkStatus was = work_status;
                    work_status = value;
                    if (xmlState != null) xmlState.WorkStatusId = (int)value;
                    OnWorkStatusChanged?.Invoke(this, new DeviceWorkStatusEventArgs(was, work_status));
                }
            }
        }
        

   

        public DeviceType TypeId => type_id;
        protected DeviceType type_id;
        Thread ConnectionThread;
        protected ConnectionTheadDelegate OnThreadWillFinish = null;
        protected ThreadStart ThreadExecFunction = null;

        internal void ReleaseDeviceFromClient()
        {
            ClientId = -1;
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
            ClientId = cl_id;
            InitPCMode();
        }

        protected virtual void CheckDeviceConnectionThreadFunc()
        {
            int tryTimes = 50;
            DeviceCommandProtocol p = null;
            retry:
            try
            {
                p = GetDeviceCommandProtocol();
                while (threadIsActive)
                {
                    Debug.WriteLine("------------------------SIMPLE_CONNECTION_CHECK---------------");
                    DeviceInfo info = p.GetDeviceInfo();
                    if (IsOnPCMode)
                    {
                        p.PCModeFlag = IsOnPCMode = false;
                    }
                    if (info.type != this.TypeId || info.SerialNumber != this.SerialNumber || info.SerialYear != this.SerialYear || info.ModelVersion != this.ModelVersion)
                    {
                        //work_status = DeviceStatus.DISCONNECTED;
                        IsConnected = false;
                    }
                    else
                    {
                        WorkStatus = info.WorkStatus;
                    }
                    Thread.Sleep(800);
                    tryTimes = 50;
                }
                p.Dispose();
                OnThreadWillFinish?.Invoke();
            }
            catch(DeviceCommandProtocolException ex)
            {
                Debug.WriteLine("----------------------------M E S S A G E--------------------");
                Debug.WriteLine(ex.Message);
                Debug.WriteLine(ex.InnerException.Message);
                Debug.WriteLine("----------------------------M E S S A G E--E N D--------------");
                if (p != null) p.Dispose();
                IsConnected = false;
            }
        }

        protected virtual bool DeviceInfoIsValid(DeviceInfo info)
        {
           return (info.type != this.TypeId || info.SerialNumber != this.SerialNumber || info.SerialYear != this.SerialYear || info.ModelVersion != this.ModelVersion);
        }

        internal void InitConnection()
        {
            //work_status = DeviceStatus.WAITING_FOR_COMMAND;
            IsConnected = true;
        }

        public DeviceBase()
        {

        }

        public void InitPCMode()
        {
            if (!IsOnPCMode && WorkStatus == DeviceWorkStatus.IDLE)
            {
                InitPCModeCheckThread();
            }
        }

        public void InitMeasureFromXMLState(MeasureXMLState measure_state)
        {
            SetMeasureParametersFromMeasureXMLState(measure_state);
            //IsOnMeasureCycle = true;
            InitPCModeMeasureThread();
        }

        public void InitPCModeMeasureThread()
        {
            if (threadIsActive)
            {
                SetDeviceConnectionThreadFunction(new ThreadStart(PCModeMeasureThread), InitPCModeMeasureThread);
            }
            else
            {
                SetDeviceConnectionThreadFunction(new ThreadStart(PCModeMeasureThread), InitPCModeCheckThread);
            }
        }

        protected virtual void PCModeMeasureThread()
        {
        }

        protected virtual void SetMeasureParametersFromMeasureXMLState(MeasureXMLState measure_state)
        {
            //Инициализируется в дочерних классах
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

        protected void ThreadLoop()
        {
            reload:
            while(threadIsActive)
            {
               ThreadExecFunction?.Invoke();
            }
            OnThreadWillFinish?.Invoke();
            if (threadIsActive) goto reload;
        }


        protected void PCModeThreadFunction()
        {
            bool isInited = false;
            DeviceCommandProtocol p = null;
            try
            {
                p = GetDeviceCommandProtocol();
                if (!isInited)
                {
                    if (!IsOnPCMode)p.PCModeFlag = true;
                    IsOnPCMode = true;
                    p.MeasureLineNumber = (short)ClientId;
                    isInited = true; //чтоб больше этого не делать, устанавливаем флажок
                }
                while (threadIsActive)
                {
                    DeviceInfo info = p.GetDeviceInfo();
                    if (DeviceInfoIsValid(info))
                    {
                        IsConnected = false;
                        break;
                    }
                    else
                    {
                        WorkStatus = info.WorkStatus;
                    }
                    if (p.MeasureLineNumber != ClientId) p.MeasureLineNumber = (short)ClientId;
                    if (threadIsActive)
                    {
                        IsOnPCMode = p.PCModeFlag;
                        if (!IsOnPCMode) threadIsActive = false;
                    }
                    /*
                    if (!IsOnMeasureCycle && IsOnPCMode)
                    {
                        //Debug.WriteLine("ВЫКЛЮЧЕНИЕ ИЗМЕРИТЕЛЯ");
                        if (isMeasureCycle)
                        {
                            p.MeasureStartFlag = false;
                            //Thread.Sleep(18);
                            if (CheckDeviceIsOnMeasureCycle(p)) continue;
                            isMeasureCycle = false;
                        }
                    }
                    else if (IsOnMeasureCycle && IsOnPCMode)
                    {
                        if (!isMeasureCycle)
                        {
                            //Debug.WriteLine("ОТПРАВКА ПАРАМЕТРОВ");
                            SendMeasureParamsToDevice(p);
                            //Debug.WriteLine("ВКЛЮЧЕНИЕ ИЗМЕРИТЕЛЯ");
                            p.MeasureStartFlag = true;
                            //Thread.Sleep(20);
                            //if (!CheckDeviceIsOnMeasureCycle(p)) continue;
                            isMeasureCycle = true;
                        }

                    }

                    if (idx == 0)
                    {
                        //Debug.WriteLine("ЧТЕНИЕ ОСНОВНОЙ ИНФОРМАЦИИ");
                        DeviceInfo info = p.GetDeviceInfo();
                        if (DeviceInfoIsValid(info))
                        {
                            IsConnected = false;
                            break;
                        }
                        else
                        {
                            WorkStatus = info.WorkStatus;
                        }
                        idx++;
                    }else if (idx == 1)
                    {
                        //Debug.WriteLine("ПРОВЕРКА MEASURE LINE NUMBER");
                        if (p.MeasureLineNumber != ClientId) p.MeasureLineNumber = (short)ClientId;
                        idx++;
                    }else if (idx == 2)
                    {
                        //Debug.WriteLine("ПРОВЕРКА PC MODE FLAG");
                        if (threadIsActive) threadIsActive = IsOnPCMode = p.PCModeFlag;
                        if (!threadIsActive) break;
                        idx++;
                    }else if (idx == 3)
                    {
                        if (isMeasureCycle && IsOnMeasureCycle)
                        {
                            //Debug.WriteLine("ЧТЕНИЕ РЕЗУЛЬТАТА");
                            ReadMeasureResult(p);
                        }
                        idx++;
                    }else if (idx == 4)
                    {
                        if (isMeasureCycle && IsOnMeasureCycle)
                        {
                            //Debug.WriteLine("ЧТЕНИЕ START MEASURE FLAG");
                            IsOnMeasureCycle = isMeasureCycle = p.MeasureStartFlag;
                        }
                        idx++;
                    }
                    if (idx > 4) idx = 0;
                    */
                    //if (isMeasureCycle) Thread.Sleep(20);
                    //tryTimes = 50;
                }
               
                p.Dispose();
                //OnThreadWillFinish?.Invoke();
            }
            catch(DeviceCommandProtocolException ex)
            {
                Debug.WriteLine("----------------------------M E S S A G E--------------------");
                Debug.WriteLine(ex.Message);
                Debug.WriteLine(ex.InnerException.Message);
                Debug.WriteLine("----------------------------M E S S A G E--E N D--------------");
                if (p != null) p.Dispose();
                IsOnPCMode = false;
                IsOnMeasureCycle = false;
                //work_status = DeviceStatus.DISCONNECTED;
                IsConnected = false;

            }
        }

        protected virtual void ReadMeasureResult(DeviceCommandProtocol p)
        {
            
        }

        protected virtual bool CheckDeviceIsOnMeasureCycle(DeviceCommandProtocol p)
        {
            return false;
        }

        protected virtual void SendMeasureParamsToDevice(DeviceCommandProtocol p)
        {
            
        }

        protected virtual DeviceCommandProtocol GetDeviceCommandProtocol()
        {
            return new DeviceCommandProtocol(PortName);
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

        private void InitMeasureStopThread()
        {
            if (threadIsActive)
            {
                SetDeviceConnectionThreadFunction(new ThreadStart(MeasureStopThreadFunc), InitMeasureStopThread);
            }
            else
            {
                SetDeviceConnectionThreadFunction(new ThreadStart(MeasureStopThreadFunc), InitPCModeCheckThread);
            }
        }

        protected virtual void MeasureStopThreadFunc()
        {

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
                ThreadExecFunction = threadFunction;
                threadIsActive = true;
                if (ConnectionThread == null)
                {
                    ConnectionThread = new Thread(new ThreadStart(ThreadLoop));
                    ConnectionThread.Start();
                }
            }

            /*
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
       */
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
            OnMeasureCycleFlagChanged = null;
            OnWorkStatusChanged = null;
            OnThreadWillFinish = null;
            threadIsActive = false;
            //if (work_status == DeviceStatus.WAITING_FOR_COMMAND) work_status = DeviceStatus.WILL_DISCONNECT;

        }

        public void StopMeasure()
        {
            InitMeasureStopThread();
            //IsOnMeasureCycle = false;
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


    public enum DeviceWorkStatus : int
    {
        DISCONNECTED = -1,
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
