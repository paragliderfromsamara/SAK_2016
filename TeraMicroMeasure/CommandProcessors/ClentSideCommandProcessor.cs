using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NormaMeasure.Utils;
using TeraMicroMeasure.XmlObjects;
using NormaMeasure.SocketControl.TCPControlLib;
using NormaMeasure.SocketControl;
using System.Diagnostics;


namespace TeraMicroMeasure.CommandProcessors
{

    class TeraMicroStateProcessor
    {
        public ClientXmlState[] AnotherClientStates;
        public ClientXmlState CurrentClientState = null;
        public ClientXmlState ExtractedClientState = null;
        public ServerXmlState ServerState;
        public bool HasCurrentClientState => CurrentClientState != null;
        public bool HasExtractedClientState => ExtractedClientState != null;
        public bool IncludesAnotherClientsState => AnotherClientStates.Length > 0;

        public TeraMicroStateProcessor(TeraMicroStateProcessor pr)
        {
            ServerState = pr.ServerState;
            CurrentClientState = pr.CurrentClientState;
            ExtractedClientState = pr.ExtractedClientState;
            AnotherClientStates = pr.AnotherClientStates;
        }

        public TeraMicroStateProcessor(ServerXmlState server_state_of_extraction, ClientXmlState current_client_state)
        {
            ServerState = new ServerXmlState(server_state_of_extraction.InnerXml);
            CurrentClientState = new ClientXmlState(current_client_state.InnerXml);
            ExtractedClientState = extractState();
            AnotherClientStates = extractAnotherStates();
        }

        private ClientXmlState extractState()
        {
            if (ServerState.Clients.ContainsKey(CurrentClientState.ClientIP)) return ServerState.Clients[CurrentClientState.ClientIP];
            return null;
        }

        private ClientXmlState[] extractAnotherStates()
        {
            List<ClientXmlState> list = new List<ClientXmlState>();
            foreach (ClientXmlState cl in ServerState.Clients.Values)
            {
                if (HasCurrentClientState)
                {
                    if (cl.ClientIP == CurrentClientState.ClientIP) continue;
                }
                list.Add(cl);
            }
            return list.ToArray();
        }
    }

    class ClientIDProcessor : TeraMicroStateProcessor
    {
        public ClientIDProcessor(TeraMicroStateProcessor pr, EventHandler clientIdChanged) : base(pr)
        {
            if (HasCurrentClientState && HasExtractedClientState)
            {
                if (CurrentClientState.ClientID != ExtractedClientState.ClientID)
                {
                    CurrentClientState.ClientID = ExtractedClientState.ClientID;
                    clientIdChanged.Invoke(CurrentClientState.ClientID, new EventArgs());
                }
            }

        }
    }

    class ClientDetector : TeraMicroStateProcessor
    {
        public ClientDetector(ServerXmlState serverState, ClientXmlState receivedClientState) : base(serverState, receivedClientState)
        {
            getOrSetClientID();
        }

        void getOrSetClientID()
        {
            if (CurrentClientState.ClientID == -1)
            {
                CurrentClientState.ClientID = SettingsControl.GetClientIdByIp(CurrentClientState.ClientIP);
                SettingsControl.SetClientIP(CurrentClientState.ClientID, CurrentClientState.ClientIP);
            }
        }
    }

    class ServerStateUpdater : TeraMicroStateProcessor
    {
        private bool ServerStateChanged = false;
        public ServerStateUpdater(TeraMicroStateProcessor pr, EventHandler OnServerStateChanged) : base(pr)
        {
            ServerStateChanged |= refreshCurrentClientOnServerSettings();
            if (ServerStateChanged) OnServerStateChanged?.Invoke(ServerState, new ServerXmlStateEventArgs(ServerState, CurrentClientState));
        }

        bool refreshCurrentClientOnServerSettings()
        {
            if (HasExtractedClientState)
            {
                if (ExtractedClientState.StateId != CurrentClientState.StateId)
                {
                    Debug.WriteLine("REPLACE CLIENT ON SERVER STATE UPDATER");
                    ServerState.ReplaceClient(CurrentClientState);
                    ExtractedClientState = CurrentClientState;
                }
                else
                {
                    Debug.WriteLine("NOTHING DO ON SERVER STATE UPDATER");
                    return false;
                }
            }
            else
            {
                Debug.WriteLine("ADD CLIENT ON SERVER STATE UPDATER");
                ServerState.AddClient(CurrentClientState);
            }
           
            return true;
        }

    }

    class ServerCommandDispatcher : IDisposable
    {
        TCPServerClientsControl clientsControl;
        private ServerXmlState _currentServerState;
        private NormaTCPClient currentTCPClient;
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
                    f =  _currentServerState.StateId != value.StateId;
                }catch(NullReferenceException)
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
        private EventHandler OnServerStateChangedByClient;
        private static object locker = new object();

        public ServerCommandDispatcher(TCPServerClientsControl _clients_control, ServerXmlState server_state, EventHandler on_server_state_changed_by_client_handler)
        {
            clientsControl = _clients_control;
            currentServerState = new ServerXmlState(server_state.InnerXml);
            clientsControl.OnClientMessageReceived += OnClientMessageReceived_Handler;
            clientsControl.OnClientDetected += OnClientDetected_Handler;
            clientsControl.OnClientDisconnected += OnClientDisconnected_Handler;


            OnServerStateChangedByClient += on_server_state_changed_by_client_handler;

        }

        private void OnClientDetected_Handler(object sender, EventArgs e)
        {
            Debug.WriteLine("OnClientDetected_Handler on ServerCommandDispatcher");
            clientsControl.Answer = currentServerState.InnerXml;
            //NormaTCPClient cl = sender as NormaTCPClient;
            //cl.MessageToSend = currentServerState.InnerXml;
        }

        private void RefreshCurrentServerStateOnClientControl()
        {
            clientsControl.Answer = currentServerState.InnerXml;
        }

        public void RefreshCurrentServerState(ServerXmlState state)
        {
            currentServerState = new ServerXmlState(state.InnerXml);
            RefreshCurrentServerStateOnClientControl();
            Debug.WriteLine($"RefreshCurrentServerState {state.InnerXml}");
        }

        private void OnClientMessageReceived_Handler(object sender, EventArgs e)
        {
            lock(locker)
            {
                
                NormaTCPClientEventArgs a = e as NormaTCPClientEventArgs;
                if (!string.IsNullOrWhiteSpace(a.Message))
                {
                    ClientXmlState cs = new ClientXmlState(a.Message);
                    Debug.WriteLine(cs.InnerXml);
                    currentTCPClient = sender as NormaTCPClient;
                    if (cs.IsValid)
                    {
                        ServerStateUpdater ssu = new ServerStateUpdater(
                                         new ClientDetector(currentServerState, cs), OnServerStateUpdated_Handler
                                        );

                    }
                }


            }
        }

        private void OnClientDisconnected_Handler(object client, EventArgs e)
        {
            lock(locker)
            {
                NormaTCPClient cl = client as NormaTCPClient;
                string ip = cl.RemoteIP as string;
                if (currentServerState.Clients.ContainsKey(ip))
                {
                    currentServerState.RemoveClient(ip);
                    RefreshCurrentServerStateOnClientControl();
                    Debug.WriteLine("OnClientDisconnected_Handler: 1");
                }else Debug.WriteLine("OnClientDisconnected_Handler: 0");
            }
        }

        private void OnServerStateUpdated_Handler(object sender, EventArgs e)
        {
            ServerXmlStateEventArgs a = e as ServerXmlStateEventArgs;
            OnServerStateChangedByClient?.Invoke(this, e);
           // RefreshCurrentServerState(a.ServerState);
            Debug.WriteLine("OnServerStateUpdated_Handler: ");
            Debug.WriteLine(clientsControl.Answer);

        }

        public void Dispose()
        {
            clientsControl.Dispose();
        }
    }

    public class ClientCommandDispatcher : IDisposable
    {
        private static object locker = new object();
        TCPClientConnectionControl connectionControl;
        EventHandler OnClientStateChangedByServer;
        ClientXmlState currentState;
        public ClientCommandDispatcher(TCPClientConnectionControl _conn_cntrl, ClientXmlState _state, EventHandler on_client_state_changed_by_server)
        {
            //currentState = _state;
            OnClientStateChangedByServer = on_client_state_changed_by_server;
            connectionControl = _conn_cntrl;
            connectionControl.OnServerAnswerReceived += OnServerStateReceived_Handler;
            _state.ClientIP = connectionControl.LocalIP;
            _state.ClientPort = connectionControl.LocalPort;
            _state.ServerIP = connectionControl.RemoteIP;
            _state.ServerPort = connectionControl.RemotePort;
            RefreshCurrentState(_state);
            //connectionControl.MessageTo = currentState.InnerXml;
            connectionControl.InitSending();
        }

        private void OnServerStateReceived_Handler(object sender, EventArgs e)
        {
            NormaTCPClientEventArgs a = e as NormaTCPClientEventArgs;
            NormaTCPClient cl = sender as NormaTCPClient;
            ServerXmlState s = new ServerXmlState(a.Message);
            if (s.Clients.ContainsKey(cl.LocalIP))
            {
                if (s.Clients[cl.LocalIP].StateId != currentState.StateId)
                {
                    ServerXmlStateEventArgs args = new ServerXmlStateEventArgs(s, s.Clients[cl.LocalIP]);
                    OnClientStateChangedByServer?.Invoke(s, args);
                }
            }
        }

        public void RefreshCurrentState(ClientXmlState state)
        {
            lock(locker)
            {
                string inner = state.InnerXml;
                currentState = new ClientXmlState(inner);
                connectionControl.MessageTo = inner;

            }
        }

        public void Dispose()
        {
            connectionControl.Dispose();
        }
    }

    public enum FROM_SERVER_COMMAND_TYPE
    {
        CHANGE_CLIENT_ID, //Изменить ClientID
        REFRESH_MEASURE_RESULT_FIELD, //Обновить поле результата
        MEASURE_ABORTED // Отказ в измерении от сервера

    }

}
