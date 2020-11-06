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
        public EventHandler OnStateWasUpdated;
        public EventHandler OnServerStateChanged;
        public EventHandler StateReceived;
        public EventHandler OnConnectionException;
        private ClientXmlState currentState;
        private ServerXmlState currentServerState;
        private NormaTCPClient client;
        private object locker = new object();
        public ClientTCPControl(ClientXmlState cur_state)
        {
            currentState = cur_state;
            initTCPClient();
            currentServerState = new ServerXmlState();
        }

        private void initTCPClient()
        {
            client = new NormaTCPClient(currentState.ClientIP, currentState.ServerIP, currentState.ClientPort, currentState.ServerPort);
            client.OnAnswerReceived += serverStateReceived;
            client.ClientSendMessageException += connectionException;

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
        private void sendState()
        {
            client.Send(currentState.InnerXml);
        }

        private void serverStateReceived(string raw_server_state, NormaTCPClient cl)
        {
            lock(locker)
            {
                ServerXmlState serverState = new ServerXmlState(raw_server_state);
                if (!serverState.IsValid) return;
                if (currentServerState.InnerXml != serverState.InnerXml)
                {
                    currentServerState = serverState;
                }
                OnServerStateChanged?.Invoke(currentServerState, new EventArgs());
            }
        }

        private void connectionException(string ip_addr, Exception ex)
        {
            OnConnectionException?.Invoke(ex, new EventArgs());
        }
        
    }

}
