﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TeraMicroMeasure.XmlObjects;
using NormaLib.SocketControl;
using NormaLib.SocketControl.TCPControlLib;
using System.Diagnostics;
using System.Threading;
using NormaLib.Devices.XmlObjects;
using NormaLib.Devices;
using NormaLib.ProtocolBuilders;

namespace TeraMicroMeasure.CommandProcessors
{
    class ClientListChangedEventArgs : EventArgs
    {
        public ServerXmlState ServerState;
        public ClientXmlState OnFocusClientState;

        public ClientListChangedEventArgs(ServerXmlState server_state, ClientXmlState on_focus_client_state)
        {
            ServerState = server_state;
            OnFocusClientState = on_focus_client_state;
        }
    }
    class ClientIDChangedEventArgs : EventArgs
    {
        public int IdWas, IdNew;
        
        public ClientIDChangedEventArgs(int id_was, int id_new) : base()
        {
            IdNew = id_new;
            IdWas = id_was;
        } 
    }

    class MeasureSettingsChangedEventArgs : EventArgs
    {
        public MeasureXMLState MeasureState_Was;
        public MeasureXMLState MeasureState_New;
        public int ClientId;

        public MeasureSettingsChangedEventArgs(MeasureXMLState was, MeasureXMLState next, int client_id) : base()
        {
            MeasureState_Was = was;
            MeasureState_New = next;
            ClientId = client_id;
        } 
    }

    public class ServerCommandDispatcher : IDisposable
    {
        TCPServerClientsControl clientsControl;
        //private List<string> WillDisconnectedIPList;
        private ServerXmlState _currentServerState;
        private NormaTCPClient currentTCPClient;
        public int ClientsConnected => currentServerState.Clients.Count;
        private ServerXmlState currentServerState
        {
            get
            {
                return _currentServerState;
            }
            set
            {
                bool f;
                try
                {
                    f = _currentServerState.StateId != value.StateId;
                }
                catch (NullReferenceException)
                {
                    f = true;
                }
                if (f)
                {
                    _currentServerState = value;
                    clientsControl.Answer = _currentServerState.InnerXml;
                }
            }
        }

        private EventHandler OnClientIDChanged;
        private EventHandler OnClientNeasureSettingsChanged;
        private EventHandler OnMeasureStartByClient;
        private EventHandler OnMeasureStopByClient;
        private EventHandler OnClientConnected;
        private EventHandler OnClientDisconnected;
        private EventHandler OnDeviceTryCapture;
        private EventHandler OnDeviceReleased;

        #region lockers
        private static object locker1 = new object();
        private static object locker2 = new object();
        private static object locker3 = new object();
        private static object locker4 = new object();
        private static object locker5 = new object();
        private static object locker6 = new object();
        private static object locker7 = new object();
        private static object locker8 = new object();
        private static object locker9 = new object();
        private static object locker10 = new object();
        #endregion

        public ServerCommandDispatcher(TCPServerClientsControl _clients_control, ServerXmlState server_state, EventHandler on_client_id_changed, EventHandler on_client_measure_settings_changed, EventHandler on_measure_start_by_client, EventHandler on_measure_stop_by_client, EventHandler on_client_connected, EventHandler on_client_disconnected, EventHandler on_device_try_capture, EventHandler on_device_released)
        {
            clientsControl = _clients_control;
            currentServerState = new ServerXmlState(server_state.InnerXml);
            clientsControl.OnClientMessageReceived += OnClientMessageReceived_Handler;
            clientsControl.OnClientDetected += OnClientDetected_Handler;
            clientsControl.OnClientDisconnected += OnClientDisconnected_Handler;

            OnClientIDChanged += on_client_id_changed;
            OnClientNeasureSettingsChanged += on_client_measure_settings_changed;
            OnMeasureStartByClient += on_measure_start_by_client;
            OnMeasureStopByClient += on_measure_stop_by_client;
            OnClientConnected += on_client_connected;
            OnClientDisconnected += on_client_disconnected;
            OnDeviceTryCapture += on_device_try_capture;
            OnDeviceReleased += on_device_released;

            ProtocolSettings.OnCommonSettingsChanged += RefreshProtocolSettingsOnServerState;

        }

        private void RefreshProtocolSettingsOnServerState(object sender, EventArgs e)
        {
            lock(locker10)
            {
                currentServerState.ProtocolSettingsState = sender as ProtocolSettingsXMLState;
                RefreshCurrentServerStateOnClientControl();
            }
        }

        internal void RemoveDeviceFromServerState(DeviceType typeId, string serial)
        {
            lock (locker1)
            {
                currentServerState.RemoveDevice((int)typeId, serial);
                RefreshCurrentServerStateOnClientControl();
            }
        }

        internal void AddDeviceToServerState(DeviceXMLState deviceXMLState)
        {
            DeviceXMLState ds = new DeviceXMLState(deviceXMLState.InnerXml);
            lock(locker2)
            {
                currentServerState.AddOrReplaceDevice(ds);
                RefreshCurrentServerStateOnClientControl();
                Debug.WriteLine(ds.InnerXml);
            }
        }

        internal void ReplaceDeviceOnServerState(DeviceXMLState xml_device)
        {
            DeviceXMLState ds = new DeviceXMLState(xml_device.InnerXml);
            lock (locker3)
            {
                currentServerState.AddOrReplaceDevice(ds);
                RefreshCurrentServerStateOnClientControl();
            }
        }

        public Dictionary<string, DeviceXMLState> GetServerDevices()
        {
            lock(locker9)
            {
                return currentServerState.Devices;
            }
        }
        

        private void OnClientDetected_Handler(object sender, EventArgs e)
        {
            RefreshCurrentServerStateOnClientControl();
        }

        private void RefreshCurrentServerStateOnClientControl()
        {
            lock (locker4)
            {
                clientsControl.Answer = currentServerState.InnerXml;
            }
        }

        public void RefreshCurrentServerState(ServerXmlState state)
        {
            currentServerState = new ServerXmlState(state.InnerXml);
            RefreshCurrentServerStateOnClientControl();
        }

        private void OnClientMessageReceived_Handler(object sender, EventArgs e)
        {
            lock (locker5)
            {

                NormaTCPClientEventArgs a = e as NormaTCPClientEventArgs;
                if (!string.IsNullOrWhiteSpace(a.Message))
                {
                    ClientXmlState cs = new ClientXmlState(a.Message);
                    currentTCPClient = sender as NormaTCPClient;
                    if (cs.IsValid)
                    {
                        ServerXmlState newState = new ServerXmlState(currentServerState.InnerXml);
                        if (currentServerState.Clients.ContainsKey(cs.ClientIP))
                        {
                            ClientXmlState last_cs = currentServerState.Clients[cs.ClientIP];
                            if (last_cs.StateId != cs.StateId)
                            {
                                processClientAsAlreadyConnected(cs, last_cs);
                                currentServerState.ReplaceClient(cs);
                            }
                        }else
                        {
                            processClientAsFirstConnect(cs);
                            currentServerState.AddClient(cs);
                            OnClientConnected?.Invoke(this, new ClientListChangedEventArgs(currentServerState, cs));
                        }
                        if (currentServerState.WasChanged) RefreshCurrentServerStateOnClientControl();
                    }
                }


            }
        }

        private void processClientAsFirstConnect(ClientXmlState cs)
        {
            GiveClientId(cs);
        }

        private void GiveClientId(ClientXmlState cs)
        {
            int clientId = cs.ClientID;
            if (clientId > 0)
            {
                if (currentServerState.GetClientStateByClientID(clientId) != null) clientId = -1;
            }
            clientId = SettingsControl.GetClientID(clientId, cs.ClientIP);
            if (cs.ClientID != clientId) cs.ClientID = clientId;
        }

        private void processClientAsAlreadyConnected(ClientXmlState cs, ClientXmlState last_cs)
        {
            if (last_cs.MeasureState.StateId != cs.MeasureState.StateId)
            {
                OnClientNeasureSettingsChanged?.Invoke(this, new MeasureSettingsChangedEventArgs(last_cs.MeasureState, cs.MeasureState, cs.ClientID));
            }
            if (String.IsNullOrWhiteSpace(last_cs.MeasureState.CapturedDeviceSerial) && !String.IsNullOrWhiteSpace(cs.MeasureState.CapturedDeviceSerial))
            {
                OnDeviceTryCapture?.Invoke(cs, new EventArgs());
            }else if (!String.IsNullOrWhiteSpace(last_cs.MeasureState.CapturedDeviceSerial) && String.IsNullOrWhiteSpace(cs.MeasureState.CapturedDeviceSerial))
            {
                OnDeviceReleased?.Invoke(cs, new EventArgs());
            }
            if (!last_cs.MeasureState.MeasureStartFlag && cs.MeasureState.MeasureStartFlag)
            {
                Debug.WriteLine("Measure On \n" + cs.MeasureState.MeasureStartFlag.ToString());
                OnMeasureStartByClient?.Invoke(cs, new EventArgs());
            }else if (last_cs.MeasureState.MeasureStartFlag && !cs.MeasureState.MeasureStartFlag)
            {
                Debug.WriteLine("Measure Off \n" + cs.MeasureState.MeasureStartFlag.ToString());
                OnMeasureStopByClient?.Invoke(cs, new EventArgs());
            }
        }

        public void RefreshServerMeasureXMLState(MeasureXMLState new_state)
        {
            lock(locker8)
            {
                if (currentServerState.ServerClientState.MeasureState.StateId != new_state.StateId)
                {
                    ClientXmlState cs = new ClientXmlState(currentServerState.ServerClientState.InnerXml);
                    cs.MeasureState = new MeasureXMLState(new_state.InnerXml);
                    processClientAsAlreadyConnected(cs, currentServerState.ServerClientState);                    
                    currentServerState.ServerClientState = cs;
                } 
            }
        }

        private void OnClientDisconnected_Handler(object client, EventArgs e)
        {
            lock (locker6)
            {
                NormaTCPClient cl = client as NormaTCPClient;
                string ip = cl.RemoteIP as string;
                if (currentServerState.Clients.ContainsKey(ip))
                {
                    ClientXmlState cs = currentServerState.Clients[ip];
                    currentServerState.RemoveClient(ip);
                    RefreshCurrentServerStateOnClientControl();
                    OnClientDisconnected?.Invoke(this, new ClientListChangedEventArgs(currentServerState, cs));
                }
            }
        }

        public ClientXmlState GetClientStateByClientID(int client_id)
        {
            return currentServerState.GetClientStateByClientID(client_id);
        }

        public bool HasClientWithId(int client_id)
        {
            return currentServerState.HasClientWithID(client_id);
        }

        public void Dispose()
        {
            clientsControl.Dispose();
        }
    }
}
