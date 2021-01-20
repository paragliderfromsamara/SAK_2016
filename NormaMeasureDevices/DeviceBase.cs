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
    public class DeviceBase : IDisposable
    {
        private static object locker = new object();
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
        private int client_id = -1;
        public int ClientId
        {
            get
            {
                return client_id;
            }
        }
        public DeviceStatus WorkStatus => work_status;


   

        public DeviceType TypeId => type_id;

        protected DeviceType type_id;
        protected DeviceStatus _work_status = DeviceStatus.UNDEFINED;
        Thread CheckConnectionThead;
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
                            OnDisconnected?.Invoke(this, new EventArgs());
                            break;
                    }
                    _work_status = value;
                }
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
        protected virtual void AssignToClient(int cl_id)
        {
            this.client_id = cl_id;
        }

        protected virtual void CheckDeviceConnectionThreadFunc()
        {
            int tryTimes = 50;
            DeviceCommandProtocol p = null;
            retry: 
            try
            {
                p = new DeviceCommandProtocol(port_name);
                while (work_status == DeviceStatus.WAITING_FOR_COMMAND)
                {
                    DeviceInfo info = p.GetDeviceInfo();
                    if (info.type != this.TypeId || info.SerialNumber != this.SerialNumber || info.SerialYear != this.SerialYear || info.ModelVersion != this.ModelVersion)
                    {
                        work_status = DeviceStatus.DISCONNECTED;
                    }
                    Debug.WriteLine($"Попыток на отправку: {tryTimes};");
                    tryTimes = 50;
                }
                p.Dispose();
            }catch
            {
                if (p != null) p.Dispose();
                if (tryTimes-- > 0) goto retry;
                work_status = DeviceStatus.DISCONNECTED;
            }
        }

        internal void GoToWaitingForCommand()
        {
            work_status = DeviceStatus.WAITING_FOR_COMMAND;
        }

        public DeviceBase()
        {

        }

        private void InitConnectionCheckThread()
        {
            CheckConnectionThead = new Thread(new ThreadStart(CheckDeviceConnectionThreadFunc));
            CheckConnectionThead.Start();
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
            return new DeviceXMLState(this);
        }

        public void Dispose()
        {
            OnDisconnected = null;
            if (work_status == DeviceStatus.WAITING_FOR_COMMAND) work_status = DeviceStatus.WILL_DISCONNECT;

        }



    }

    public struct DeviceInfo
    {
        public DeviceType type;
        public uint SerialYear;
        public byte SerialNumber;
        public byte ModelVersion;
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


    public class NormaDeviceException : Exception
    {
        public NormaDeviceException(string message) : base(message)
        {

        }
    }
}
