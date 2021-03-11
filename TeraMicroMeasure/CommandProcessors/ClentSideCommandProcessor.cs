using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NormaLib.Utils;
using TeraMicroMeasure.XmlObjects;
using NormaLib.SocketControl.TCPControlLib;
using NormaLib.SocketControl;
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

    

    

    public enum FROM_SERVER_COMMAND_TYPE
    {
        CHANGE_CLIENT_ID, //Изменить ClientID
        REFRESH_MEASURE_RESULT_FIELD, //Обновить поле результата
        MEASURE_ABORTED // Отказ в измерении от сервера

    }

}
