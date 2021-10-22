using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Modbus.Device;
using System.Threading;
using System.IO.Ports;
using System.Diagnostics;
using NormaLib.Devices.XmlObjects;
using NormaLib.DBControl.Tables;

namespace NormaLib.Devices
{

    public class DevicesDispatcher : IDisposable
    {
        public const bool USE_EMULATED_DEVICES = false;
         
        private static object locker = new object();
        public EventHandler OnDeviceFound;
        private DeviceCommandProtocol commandProtocol;
        public Dictionary<string, DeviceBase> DeviceList;
        public int ConnectedCount => DeviceList.Count;
        //private List<string> deviceComList;
        private Thread FindThread;
        private bool NeedStop = false;
        public DevicesDispatcher(DeviceCommandProtocol command_protocol, EventHandler on_device_found)
        {
            DeviceList = new Dictionary<string, DeviceBase>();
            commandProtocol = command_protocol;
            OnDeviceFound = on_device_found;
            InitFindThread();
        }
        public DeviceXMLState CaptureDeviceAndGetDeviceXmlState(int type_id, string serial, int client_id)
        {
            foreach(var d in DeviceList.Values)
            {
                if ((int)d.TypeId == type_id && serial == d.Serial)
                {
                    if (d.ClientId == -1 && d.WorkStatus == DeviceWorkStatus.IDLE)
                    {
                        d.AssignToClient(client_id);
                        return d.GetXMLState();
                    }else if (d.ClientId == client_id)
                    {
                        return d.GetXMLState();
                    }
                    else return null;
                }
            }
            return null;
        }

        public DeviceXMLState ReleaseDeviceAndGetDeviceXmlState(int client_id)
        {
            foreach (var d in DeviceList.Values)
            {
                if (d.ClientId == client_id)
                {
                    d.ReleaseDeviceFromClient();
                    return d.GetXMLState();
                }
            }
            return null;
        }

        public void InitFindThread()
        {
            NeedStop = false;
            FindThread = new Thread(new ThreadStart(findThreadFunction));
            FindThread.Start();
        }

        public void StopFind()
        {
            NeedStop = true;
        }

        private string[] GetAvailablePortNames()
        {
            string[] port_list = SerialPort.GetPortNames();
            List<string> portList = new List<string>();
            string ports = String.Empty;
            foreach (string port_name in port_list)
            {
                ports += $"{port_name}; ";
                if (!DeviceList.ContainsKey(port_name))
                {
                    portList.Add(port_name);
                }
            }

            return portList.ToArray();
        }

        private void findThreadFunction()
        {
            if (USE_EMULATED_DEVICES)
            {
                List<DeviceBase> devices = new List<DeviceBase>();
                Teraohmmeter.TeraohmetrTOmM_01_Emulator tera = new Teraohmmeter.TeraohmetrTOmM_01_Emulator(new DeviceInfo() { ModelVersion = 1, PortName = "VCOM_1", SerialNumber = 1, SerialYear = 2021, WorkStatus = DeviceWorkStatus.IDLE, type = DeviceType.Teraohmmeter });
                devices.Add(tera);
                foreach(var d in devices)
                {
                    d.OnDisconnected += OnDeviceDisconnected_Handler;
                    DeviceList.Add(d.PortName, d);
                    d.InitConnection();
                }
                OnDeviceFound?.Invoke(tera, new EventArgs());

            } else
            {
                while (!NeedStop)
                {
                    string[] port_list = GetAvailablePortNames();
                    if (port_list.Length > 0)
                    {
                        foreach (string port_name in port_list)
                        {
                            DeviceBase device = commandProtocol.GetDeviceOnCOM(port_name);
                            Debug.WriteLine($"Сканируем порт {port_name}");
                            if (device != null)
                            {
                                Debug.WriteLine($"Найдено устройство на {port_name}");
                                SyncDeviceWithDB(device);
                                OnDeviceFound?.Invoke(device, new EventArgs());
                                device.OnDisconnected += OnDeviceDisconnected_Handler;
                                DeviceList.Add(port_name, device);
                                device.InitConnection();
                            }
                        }
                    }
                    for (int i = 0; i < 2000; i++)
                    {
                        Thread.Sleep(1);
                        if (NeedStop) break;
                    }
                }
            }
           
        }

        private void SyncDeviceWithDB(DeviceBase device)
        {
            device.DeviceIDOnDB = MeasureDevice.TryGetDeviceIdByDevice(device);
            Debug.WriteLine($"ID в базе данных {device.DeviceIDOnDB }");
        }

        public DeviceBase GetDeviceByTypeAndSerial(int typeId, string serial)
        {
            foreach(var d in DeviceList.Values)
            {
                if (d.Serial == serial && (int)d.TypeId == typeId)
                {
                    return d;
                }
            }
            return null;
        }

        private void OnDeviceDisconnected_Handler(object sender, EventArgs e)
        {
            lock (locker)
            {
                DeviceBase d = sender as DeviceBase;
                DeviceList.Remove(d.PortName);
                d.Dispose();
            }

        }

        public void Dispose()
        {
            StopFind();
            if (DeviceList.Count > 0)
            {
                foreach(DeviceBase d in DeviceList.Values)
                {
                    d.Dispose();
                }
            }
        }
    }
}
