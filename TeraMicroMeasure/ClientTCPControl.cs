using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NormaMeasure.SocketControl;

using TeraMicroMeasure.XmlObjects;
namespace TeraMicroMeasure
{
    class ClientTCPControl
    {
        public EventHandler OnStateWasChangedByServer;
        public EventHandler OnServerStateChanged;
        public EventHandler OnServerConnected;
        public EventHandler StateReceived;
        public EventHandler OnConnectionException;
        private ClientXmlState currentState;
        private ClientXmlState stateFromServer;
        private ServerXmlState currentServerState;
        private NormaTCPClient client;
        private object locker = new object();
        private int ServerTryFileLimit = 10;
        private bool firstTransaction = false;
        public ClientTCPControl(ClientXmlState cur_state)
        {
            stateFromServer = currentState = cur_state;
            initTCPClient();
            currentServerState = new ServerXmlState();
        }

        private void initTCPClient()
        {
            client = new NormaTCPClient(currentState.ClientIP, currentState.ServerIP, currentState.ClientPort, currentState.ServerPort);
           // client.OnAnswerReceived += serverStateReceived;
           // client.ClientSendMessageException += connectionException;
        }

        public void Start()
        {
            sendState();
        }

        public void Stop()
        {
            if (client != null)
            {
                client.Close();
                client = null;
            }
        }

        public void SendState(ClientXmlState s)
        {
            if (s.StateId != currentState.StateId)
            {
                currentState = new ClientXmlState(s.InnerXml);
                sendState();
            }
        }

        private void sendState()
        {
            client.Send(currentState.InnerXml);
        }

        private void serverStateReceived(string raw_server_state, NormaTCPClient cl)
        {
            try
            {
                ServerXmlState serverState = new ServerXmlState(raw_server_state);
                if (serverState.IsValid) OnServerStateChanged?.Invoke(serverState, new EventArgs());
                if (!firstTransaction)
                {
                    firstTransaction = true;
                    OnServerConnected?.Invoke(currentServerState, new EventArgs());
                }
                ServerTryFileLimit = 10;
            }
            catch (System.Xml.XmlException e)
            {
               
                if (--ServerTryFileLimit == 0)
                {
                    OnConnectionException?.Invoke(e, new EventArgs());
                }
            }
        }

        private void onServerStateChanged()
        {
            ClientXmlState clientStateFromServer = currentServerState.Clients[currentState.ClientIP];
            OnServerStateChanged?.Invoke(currentServerState, new EventArgs());
            if (clientStateFromServer == null) return;
            if (clientStateFromServer.StateId != currentState.StateId) SynchronizeWithCurrentState(clientStateFromServer);
            
        }

        private void SynchronizeWithCurrentState(ClientXmlState clientStateFromServer)
        {
            //if (clientStateFromServer.StateId == currentState.StateId) return;
            if (currentState.ClientID != clientStateFromServer.ClientID)
            {
                currentState.ClientID = clientStateFromServer.ClientID;
                SettingsControl.SetClientId(currentState.ClientID);
            }

            OnStateWasChangedByServer?.Invoke(currentState, new EventArgs());
           
        }

        private bool IsEqualWithCurrentState(ClientXmlState clientStateFromServer)
        {
            bool f = true;
            f &= clientStateFromServer.ClientID == currentState.ClientID;
            return f;
        }

        private void connectionException(string ip_addr, Exception ex)
        {
            OnConnectionException?.Invoke(ex, new EventArgs());
        }

        private void SynchronizeState()
        {

        }
        
    }

}
