using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TeraMicroMeasure.XmlObjects;
using NormaMeasure.SocketControl.TCPServerControllLib;
using NormaMeasure.SocketControl;

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
            if (CurrentClientState.ClientID == 0)
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
                    ServerState.ReplaceClient(CurrentClientState);
                    ExtractedClientState = CurrentClientState;
                }
                else return false;
            }
            else
            {
                ServerState.AddClient(CurrentClientState);
            }
           
            return true;
        }

    }

    class ServerCommandDispatcher : IDisposable
    {
        TCPServerClientsControl clientsControl;
        private ServerXmlState _currentServerState;
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
            currentServerState = server_state;
            clientsControl.OnClientMessageReceived += OnClientMessageReceived_Handler;
            
            OnServerStateChangedByClient += on_server_state_changed_by_client_handler;

        }


        private void OnClientMessageReceived_Handler(object sender, EventArgs e)
        {
            lock(locker)
            {
                NormaTCPClientEventArgs a = e as NormaTCPClientEventArgs;
                ClientXmlState cs = new ClientXmlState(a.Message);
                if (cs.IsValid)
                {
                    ServerStateUpdater ssu = new ServerStateUpdater(
                                     new ClientDetector(currentServerState, cs), OnServerStateUpdated_Handler
                                    );
                }

            }
        }


        private void OnServerStateUpdated_Handler(object sender, EventArgs e)
        {
            OnServerStateChangedByClient?.Invoke(this, e);
        }

        public void Dispose()
        {
            clientsControl.Dispose();
        }
    }
}
