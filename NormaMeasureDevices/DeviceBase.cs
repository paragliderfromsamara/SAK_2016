using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using NormaMeasure.Devices.XmlObjects;
using NormaMeasure.Devices.Teraohmmeter;
using NormaMeasure.Devices.Microohmmeter;
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

        public MeasureResultEventArgs(MicroMeasureResultStruct result)
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
       // public EventHandler OnPCModeFlagChanged;
        public EventHandler OnWorkStatusChanged;
        public EventHandler OnGetMeasureResult;
        public EventHandler OnMeasureCycleFlagChanged;
        public EventHandler OnXMLStateChanged;
        protected DeviceCommandProtocol CommandProtocol;

        private static object locker = new object();
        //private bool thread_is_active = false;

        //protected bool threadIsActive
        //{
        //    get
        //    {
         //       return thread_is_active;
         //   }set
         //   {
          //      lock(locker)
         //       {
        //            thread_is_active = value;
         //       }
        //    }
       // }
        

        private bool is_on_pc_mode = false;
        public bool IsOnPCMode
        {
            get
            {
                return is_on_pc_mode;
            }
            protected set
            {
                if (value != is_on_pc_mode)
                {
                    is_on_pc_mode = value;
                    if (!is_on_pc_mode) ClientId = -1;
                    if (xmlState != null) xmlState.IsOnPCMode = is_on_pc_mode;
                    OnXMLStateChanged?.Invoke(this, new EventArgs());//OnPCModeFlagChanged?.Invoke(this, new EventArgs());
                }
            }
        }

        protected bool pc_mode_flag = false;
        protected bool measure_cycle_flag = false;

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
                            InitConnectionThread();
                        }
                        else
                        {
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

        public string MeasureStatusText
        {
            get
            {
                switch(measure_status_id)
                {
                    case (uint)DeviceMeasureStatus.SUCCESS:
                        return "Успешно";
                    case (uint)DeviceMeasureStatus.INTEGRATOR_IS_ON_NEGATIVE:
                        return "Цепь под напряжением";
                    case (uint)DeviceMeasureStatus.IN_WORK:
                        return "В процессе";
                    case (uint)DeviceMeasureStatus.RANGE_DOWN:
                        return "Требуется диапазон ниже";
                    case (uint)DeviceMeasureStatus.RANGE_UP:
                        return "Требуется диапазон выше";
                    case (uint)DeviceMeasureStatus.SHORT_CIRCUIT:
                        return "Короткое замыкание";
                    case (uint)DeviceMeasureStatus.BRAKE_CURRENT_LINE:
                        return "Обрыв токовой цепи";
                    case (uint)DeviceMeasureStatus.BRAKE_POTENTIAL_LINE:
                        return "Обрыв потенциальной цепи";
                    case (uint)DeviceMeasureStatus.CALCULATE_ERROR:
                        return "Ошибка преобразования";
                    default:
                        return $"Измерение прервано. Код прерывания:{measure_status_id}.";
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
                if (xmlState != null)
                {
                    xmlState.MeasureStatusId = value;
                    xmlState.MeasureStatusText = MeasureStatusText;    
                }

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
                OnXMLStateChanged?.Invoke(this, new EventArgs());
            }
        }
        protected int next_client_id = -1;
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
            lock(locker)
            {
                next_client_id = -1;
            }
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
            lock (locker)
            {
                next_client_id = cl_id;//ClientId = cl_id;
                InitPCMode();
            }
        }

        protected virtual void CheckDeviceConnectionThreadFunc()
        {
            Debug.WriteLine("------------------------SIMPLE_CONNECTION_CHECK---------------");
            DeviceInfo info = CommandProtocol.GetDeviceInfo();
            if (!DeviceInfoIsValid(info))
            {
                IsConnected = false;
            }
            else
            {
                WorkStatus = info.WorkStatus;
            }
            Thread.Sleep(800);
        }

        protected virtual bool DeviceInfoIsValid(DeviceInfo info)
        {
           return (info.type == this.TypeId && info.SerialNumber == this.SerialNumber && info.SerialYear == this.SerialYear && info.ModelVersion == this.ModelVersion);
        }

        internal void InitConnection()
        {
            IsConnected = true;
        }

        public DeviceBase()
        {

        }

        public void InitPCMode()
        {
            lock (locker)
            {
                if (!IsOnPCMode && WorkStatus == DeviceWorkStatus.IDLE)
                {
                    pc_mode_flag = true;
                }
            }
        }

        public void InitMeasureFromXMLState(MeasureXMLState measure_state)
        {
            SetMeasureParametersFromMeasureXMLState(measure_state);
            InitPCModeMeasureThread();
        }

        protected virtual void InitPCModeMeasureThread()
        {
            lock (locker)
            {
                measure_cycle_flag = true;
            }
        }

        protected virtual void PCModeMeasureThreadFunction()
        {
            //Инициализируется в дочерних классах
        }

        protected virtual void SetMeasureParametersFromMeasureXMLState(MeasureXMLState measure_state)
        {
            //Инициализируется в дочерних классах
        }

        public void StopPCMode()
        {
            // if (IsOnPCMode) InitConnectionCheckThread();
            lock(locker)
            {
                pc_mode_flag = false;
            }
        }

        protected void ThreadLoop()
        {
            try
            {
                CommandProtocol = GetDeviceCommandProtocol();
                while (IsConnected)
                {
                    if (IsOnPCMode)
                    {
                        if (IsOnMeasureCycle)
                        {
                            if (!measure_cycle_flag)
                            {
                                MeasureStopThreadFunc();
                            }else
                            {
                                PCModeMeasureThreadFunction();
                            }
                        }
                        else
                        {
                            if (measure_cycle_flag)
                            {
                                InitMeasureCycleOnDevice();
                            }else
                            {
                                PCModeIdleThreadFunction();
                            }
                        }
                    }
                    else
                    {
                        CheckDeviceConnectionThreadFunc();
                        if (pc_mode_flag)
                        {
                            Debug.WriteLine("----------------------------InitPCModeOnDevice--------------------");
                            InitPCModeOnDevice();
                        }
                        
                    }
                    //ThreadExecFunction?.Invoke();
                }
                OnThreadWillFinish?.Invoke();
                if (CommandProtocol != null) CommandProtocol.Dispose();
            }
            catch (DeviceCommandProtocolException ex)
            {
                Debug.WriteLine("----------------------------DeviceThreadFinished--------------------");
                Debug.WriteLine(ex.Message);
                Debug.WriteLine(ex.InnerException.Message);
                Debug.WriteLine("----------------------------DeviceThreadFinished END----------------");
                if (CommandProtocol != null) CommandProtocol.Dispose();
                IsConnected = false;
            }
        }

        protected virtual void InitMeasureCycleOnDevice()
        {
            
        }

        protected virtual void InitPCModeOnDevice()
        {
            CommandProtocol.PCModeFlag = true;
            Thread.Sleep(250);
            CommandProtocol.MeasureLineNumber = (short)ClientId;
            Thread.Sleep(250);
            IsOnPCMode = CommandProtocol.PCModeFlag;
            Thread.Sleep(250);
        }

        protected void PCModeIdleThreadFunction()
        {
            DeviceInfo info = CommandProtocol.GetDeviceInfo();
            Debug.WriteLine("------------------------PC_MODE_CONNECTION_CHECK---------------");
            if (!DeviceInfoIsValid(info))
            {
                IsOnPCMode = pc_mode_flag = false;
                Debug.WriteLine("Escape FROM CheckDeviceInfo");
            }
            else
            {
                WorkStatus = info.WorkStatus;
                if (CommandProtocol.MeasureLineNumber != next_client_id)
                {
                    CommandProtocol.MeasureLineNumber = (short)next_client_id;
                    //Thread.Sleep(250);
                    ClientId = CommandProtocol.MeasureLineNumber;
                }
                IsOnPCMode = pc_mode_flag = CommandProtocol.PCModeFlag;
                Debug.WriteLine($"Escape FROM Set PC MODE FLAG {pc_mode_flag}");
            }
            // проверяем актуальность ID клиента, 
        }

        protected virtual void ReadMeasureResult(DeviceCommandProtocol p)
        {
            
        }

        protected virtual bool CheckDeviceIsOnMeasureCycle(DeviceCommandProtocol p)
        {
            return false;
        }

        protected virtual void SendMeasureParamsToDevice()
        {
            
        }

        protected virtual DeviceCommandProtocol GetDeviceCommandProtocol()
        {
            return new DeviceCommandProtocol(PortName);
        }

        protected void InitConnectionThread()
        {
            if (ConnectionThread == null)
            {
                ConnectionThread = new Thread(new ThreadStart(ThreadLoop));
                ConnectionThread.Start();
            }
        }

        protected virtual void MeasureStopThreadFunc()
        {

        }

        public DeviceBase(DeviceInfo info)
        {
            serial_year = info.SerialYear;
            serial_number = info.SerialNumber;
            model_version = info.ModelVersion;
            port_name = info.PortName;
            work_status = info.WorkStatus;
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
            //OnPCModeFlagChanged = null;
            OnMeasureCycleFlagChanged = null;
            OnWorkStatusChanged = null;
            OnThreadWillFinish = null;
            //threadIsActive = false;
            OnXMLStateChanged = null;
            IsConnected = false;
            //if (work_status == DeviceStatus.WAITING_FOR_COMMAND) work_status = DeviceStatus.WILL_DISCONNECT;

        }

        public void StopMeasure()
        {
            lock(locker)
            {
                if (IsOnMeasureCycle) measure_cycle_flag = false;
            }
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

    //public enum DeviceMeasureResultStatus : uint
    ///{
    //    SUCCESS = 100,
    //    IN_WORK = 105,   //
    //    NEED_TO_REPEAT = 106,
    //    RANGE_UP = 101,	//увеличить диапазон
    //    RANGE_DOWN = 102,	//понизить диапазон
    //    SHORT_CIRCUIT = 103, //короткое замыкание
    //    INTEGRATOR_IS_ON_NEGATIVE = 104 //Интегратор в отрицательной области	
    //}


    public class NormaDeviceException : Exception
    {
        public NormaDeviceException(string message) : base(message)
        {

        }
    }

    public enum DeviceMeasureStatus : ushort
    {
        SUCCESS = 100,
        RANGE_DOWN = 101,
        RANGE_UP = 102,
        SHORT_CIRCUIT = 103,
        INTEGRATOR_IS_ON_NEGATIVE = 104,
        IN_WORK = 105,
        BRAKE_CURRENT_LINE = 107,
        BRAKE_POTENTIAL_LINE = 108,
        CALCULATE_NO_ERROR = 109,
        CALCULATE_ERROR = 110,
        INTERRUPTED = 111,
        PHASE_REVERSE = 112
     }
}
