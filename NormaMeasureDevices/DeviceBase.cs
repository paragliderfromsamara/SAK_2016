﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using NormaMeasure.Devices.XmlObjects;
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
    public delegate void ConnectionTheadDelegate();
    public class DeviceBase : IDisposable
    {
        public EventHandler OnDisconnected;
        public EventHandler OnPCModeFlagChanged;
        public EventHandler OnWorkStatusChanged;

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
                p = new DeviceCommandProtocol(PortName);
                while (threadIsActive)
                {
                    DeviceInfo info = p.GetDeviceInfo();
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
            catch
            {
                if (p != null) p.Dispose();
                if (tryTimes-- > 0)
                {
                    goto retry;
                }
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

        public void InitMeasure()
        {

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
                    IsOnPCMode = p.GetPCModeFlag();
                    p.SetMeasureLineNumber(ClientId);
                    isInited = true; //чтоб больше этого не делать, устанавливаем флажок
                }
                while (threadIsActive)
                {
                    DeviceInfo info = p.GetDeviceInfo();
                    if (DeviceInfoIsValid(info))
                    {
                        //work_status = DeviceStatus.DISCONNECTED;
                        IsConnected = false;
                        break;
                    }else
                    {
                        WorkStatus = info.WorkStatus;
                    }
                    if (p.GetMeasureLineNumber() != ClientId) p.SetMeasureLineNumber(ClientId);
                    tryTimes = 50;
                    if (threadIsActive) threadIsActive = p.GetPCModeFlag();
                }
                if (IsOnPCMode)
                {
                    p.SetPCModeFlag(false);
                    IsOnPCMode = false;
                }
                p.Dispose();
                OnThreadWillFinish?.Invoke();
            }
            catch
            {
                if (p != null) p.Dispose();
                if (tryTimes-- > 0) goto retry;
                IsOnPCMode = false;
                //work_status = DeviceStatus.DISCONNECTED;
                IsConnected = false;
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
            OnWorkStatusChanged = null;
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
