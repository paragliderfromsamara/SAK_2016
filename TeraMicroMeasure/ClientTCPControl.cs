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
        public EventHandler OnServerStateReceived;
        public EventHandler StateReceived;
        public EventHandler OnConnectionException;
        private ClientXmlState currentState;
        private ServerXmlState currentServerState;
        private NormaTCPClient client;

        public ClientTCPControl(ClientXmlState cur_state)
        {
            currentState = cur_state;
            initTCPClient();
            
        }

        private void initTCPClient()
        {
            client = new NormaTCPClient(currentState.ClientIP, currentState.ServerIP, currentState.ClientPort, currentState.ServerPort);
            client.OnMessageReceived += serverStateReceived;
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

        private void serverStateReceived(string server_state, NormaTCPClient cl)
        {
            ServerXmlState serverState = new ServerXmlState(server_state);
            OnServerStateReceived?.Invoke(server_state, new EventArgs());

        }

        private void connectionException(string ip_addr, Exception ex)
        {
            OnConnectionException?.Invoke(ex, new EventArgs());
        }
        
    }

}
