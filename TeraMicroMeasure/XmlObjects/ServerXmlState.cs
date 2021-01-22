using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NormaMeasure.Utils;
using NormaMeasure.Devices.XmlObjects;
namespace TeraMicroMeasure.XmlObjects
{
    public class ServerXmlStateEventArgs : EventArgs
    {
        public ClientXmlState ClientState;
        public ServerXmlState ServerState;
        public ServerXmlStateEventArgs(ServerXmlState _server_state, ClientXmlState _changer_client_state)
        {
            ServerState = _server_state;
            ClientState = _changer_client_state;
        }
    }

    public class ServerXmlState : NormaXmlObject
    {
        const string ClientsList_TagName = "ClientsList";
        const string ClientElement_TagName = "ClientXmlState";
        const string IPAddress_TagName = "ip";
        const string Port_TagName = "port";
        const string RequestPeriod_TagName = "RequestPeriod";
        const string DevicesList_TagName = "ConnectedDevices";
        const string DeviceElement_TagName = "DeviceXMLState";


        public ServerXmlState() : base()
        {

        }

        public ServerXmlState(string inner_xml) : base(inner_xml)
        {

        }


        string ip_address;
        /// <summary>
        /// IP адрес сервера
        /// </summary>
        public string IPAddress
        {
            get
            {
                return ip_address;
            }
            set
            {
                bool f = ip_address != value;
                if (f)
                {
                    ip_address = value;
                    setChangedFlag(f);
                }
            }
        }

        int port;
        /// <summary>
        /// TCP порт на котором работает сервер
        /// </summary>
        public int Port
        {
            get
            {
                return port;
            }
            set
            {
                bool f = port != value;
                if (f)
                {
                    port = value;
                    setChangedFlag(f);
                }
            }
        }

        int request_period;
        /// <summary>
        /// Периодичность запросов клиентов к серверу
        /// </summary>
        public int RequestPeriod
        {
            get
            {
                return request_period;
            }
            set
            {
                bool f = request_period != value;
                if (f)
                {
                    request_period = value;
                    setChangedFlag(f);
                }
            }
        }


        Dictionary<string,DeviceXMLState> devices = new Dictionary<string, DeviceXMLState>();

        public Dictionary<string, DeviceXMLState> Devices
        {
            get
            {
                return devices;
            }
        }

        public DeviceXMLState GetXMLDeviceState(int type_id, string serial)
        {
            string key = $"{type_id}-{serial}";
            if (devices.ContainsKey(key))
            {
                return devices[key];
            }
            else return null;
        }

        public void AddOrReplaceDevice(DeviceXMLState device_state)
        {
            string key = $"{device_state.TypeId}-{device_state.Serial}";
            DeviceXMLState onList = GetXMLDeviceState(device_state.TypeId, device_state.Serial);
            bool willAdd = false;
            if (onList == null)
            {
                willAdd = true;
            }else
            {
                if (onList.StateId != device_state.StateId)
                {
                    willAdd = true;
                    devices.Remove(key);
                }
            }
            if (willAdd)
            {
                devices.Add(key, device_state);
                setChangedFlag(true);
            }
        }

        public void RemoveDevice(int type_id, string serial)
        {
            string key = $"{type_id}-{serial}";
            if (devices.ContainsKey(key))
            {
                devices.Remove(key);
                setChangedFlag(true);
            }
        }

        public bool ContainsDevice(int type_id, string serial)
        {
            return devices.ContainsKey($"{type_id}-{serial}");
        }

        Dictionary<string, ClientXmlState> clients = new Dictionary<string, ClientXmlState>();
        /// <summary>
        /// Список подключенных клиентов по IP адресам
        /// </summary>
        public Dictionary<string, ClientXmlState> Clients
        {
            get
            {
                if (clients == null)
                {
                    Dictionary<string, string> raw = GetNodesInnerXmlFromContainer(ClientsList_TagName, ClientElement_TagName);
                    clients = new Dictionary<string, ClientXmlState>();
                    foreach (string ip in raw.Keys)
                    {
                        clients.Add(ip, new ClientXmlState(raw[ip]));
                    }
                }
                return clients;
                //xm
            }
        }

        protected override void buildFromXML()
        {
            base.buildFromXML();
            fillIPAddressFromXML();
            fillPortFormXML();
            fillRequestPeriodFromXML();
            fillClientsFromXML();
            fillDevicesFromXML();
        }

        private void fillDevicesFromXML()
        {
            devices.Clear();
            Dictionary<string, string> raw = GetNodesInnerXmlFromContainer(DevicesList_TagName, DeviceElement_TagName);
            foreach(var key in raw.Keys)
            {
                devices.Add(key, new DeviceXMLState(raw[key]));
            }
        }

        private void fillClientsFromXML()
        {
            clients.Clear();
            Dictionary<string, string> raw = GetNodesInnerXmlFromContainer(ClientsList_TagName, ClientElement_TagName);
            foreach (string ip in raw.Keys)
            {
                clients.Add(ip, new ClientXmlState(raw[ip]));
            }
        }

        private void fillRequestPeriodFromXML()
        {
            int v = 500;
            tryGetIntXmlProp(RequestPeriod_TagName, out v);
            if (v == 0) v = 500;
            request_period = v;
        }

        private void fillPortFormXML()
        {
            int v = 8000;
            tryGetIntXmlProp(Port_TagName, out v);
            port = v;
        }

        protected override void fillXMLDocument()
        {
            base.fillXMLDocument();
            setXmlProp(IPAddress_TagName, ip_address);
            setXmlProp(Port_TagName, port.ToString());
            setXmlProp(RequestPeriod_TagName, request_period.ToString());
            addClientsToXML();
            addDevicesToXML();
        }

        private void addDevicesToXML()
        {
            foreach(var d in devices.Values)
            {
                AddElementToContainer(DevicesList_TagName, d.InnerXml);
            }
        }

        private void addClientsToXML()
        {
            foreach(var cl in clients.Values)
            {
                AddElementToContainer(ClientsList_TagName, cl.InnerXml);
            }
        }

        private void fillIPAddressFromXML()
        {
            ip_address = getXmlProp(IPAddress_TagName);
        }

        /// <summary>
        /// Добавление клиента в списко подключенных
        /// </summary>
        /// <param name="ip"></param>
        /// <param name="cl"></param>
        public void AddClient(ClientXmlState cl)
        {
            if (!clients.ContainsKey(cl.ClientIP))
            {
                clients.Add(cl.ClientIP, cl);
                setChangedFlag(true);
            }else
            {
                throw new Exception($"Клиент с IP {cl.ClientIP} уже добавлен в список");
            }
        }

        public void RemoveClient(string ip)
        {
            if (clients.ContainsKey(ip))
            {
                clients.Remove(ip);
                setChangedFlag(true);
            }
        }

        public void RemoveClient(ClientXmlState cs)
        {
            RemoveClient(cs.ClientIP);
        }

        public void ReplaceClient(ClientXmlState cl)
        {
            if (clients.ContainsKey(cl.ClientIP))
            {
                RemoveClient(cl);
            }
            AddClient(cl);
        }


        public ClientXmlState GetClientStateByClientID(int client_id)
        {
            foreach(var cs in Clients.Values)
            {
                if (cs.ClientID == client_id) return cs;
            }
            return null;
        }

        public bool HasClientWithID(int client_id)
        {
            return GetClientStateByClientID(client_id) != null;
        }
    }
}
