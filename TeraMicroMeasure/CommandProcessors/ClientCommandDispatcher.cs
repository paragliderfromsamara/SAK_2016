using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NormaLib.SocketControl;
using NormaLib.SocketControl.TCPControlLib;
using TeraMicroMeasure.XmlObjects;
using System.Windows.Forms;
using NormaLib.Devices.XmlObjects;

namespace TeraMicroMeasure.CommandProcessors
{
    public class ClientCommandDispatcher : IDisposable
    {
        private static object locker = new object();
        TCPClientConnectionControl connectionControl;
        EventHandler OnClientIDChanged;
        EventHandler OnMeasureStatusChanged;
        EventHandler OnServerStateReceived;
        ClientXmlState currentState;

        public ClientCommandDispatcher(TCPClientConnectionControl _conn_cntrl, ClientXmlState _state, EventHandler on_client_id_changed, EventHandler on_measure_status_changed, EventHandler on_server_state_received)
        {
            OnClientIDChanged += on_client_id_changed;
            OnMeasureStatusChanged += on_measure_status_changed;
            OnServerStateReceived += on_server_state_received;
            connectionControl = _conn_cntrl;
            connectionControl.OnServerAnswerReceived += OnServerStateReceived_Handler;
            _state.ClientIP = connectionControl.LocalIP;
            _state.ClientPort = connectionControl.LocalPort;
            _state.ServerIP = connectionControl.RemoteIP;
            _state.ServerPort = connectionControl.RemotePort;
            currentState = _state;
            RefreshCurrentStateOnConnectionControl();
            connectionControl.InitSending();
            
        }

        private void OnServerStateReceived_Handler(object sender, EventArgs e)
        {
            try
            {
                NormaTCPClientEventArgs a = e as NormaTCPClientEventArgs;
                NormaTCPClient cl = sender as NormaTCPClient;
                ServerXmlState s = new ServerXmlState(a.Message);
                if (s.Clients.ContainsKey(cl.LocalIP))
                {
                    ClientXmlState fromServerState = s.Clients[cl.LocalIP];
                    OnServerStateReceived?.Invoke(s, new EventArgs());
                    if (fromServerState.StateId != currentState.StateId)
                    {
                        CheckClientIDChanged(fromServerState);
                        RefreshCurrentStateOnConnectionControl();
                    }
                }
            }
            catch(System.Xml.XmlException)
            {

            }catch(NullReferenceException)
            {

            }
        }



        private void CheckClientIDChanged(ClientXmlState fromServerState)
        {
            if (currentState.ClientID != fromServerState.ClientID)
            {
                int was = currentState.ClientID;
                currentState.ClientID = fromServerState.ClientID;
                SettingsControl.SetClientId(fromServerState.ClientID);
                OnClientIDChanged?.Invoke(this, new ClientIDChangedEventArgs(was, currentState.ClientID));
            }
        }

        public void RefreshMeasureState(MeasureXMLState m_state)
        {
            currentState.MeasureState = m_state;
            RefreshCurrentStateOnConnectionControl();
        }
        public void RefreshCurrentStateOnConnectionControl()
        {
            lock (locker)
            {
                connectionControl.MessageTo = currentState.InnerXml;
            }
        }

        public void Dispose()
        {
            connectionControl.Dispose();
        }
    }
}
