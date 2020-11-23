using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TeraMicroMeasure.XmlObjects;
using NormaMeasure.SocketControl;
using System.Windows.Forms;
using System.Diagnostics;

namespace TeraMicroMeasure
{
    /*
    class ServerTCPControl
    {
        static object locker = new object();
        public EventHandler OnServerConnectionException;
        public EventHandler OnClientStateReceived;
        public EventHandler OnServerStatusChanged;
        public EventHandler OnClientListChanged;

        private ServerXmlState currentState;
        private NormaServerDeprecated server;
        public ServerTCPControl(ServerXmlState state)
        {
            this.currentState = state;
        }

        private void initServer()
        {
            server = new NormaServerDeprecated(currentState.IPAddress, currentState.Port);
            server.ProcessOnServerConnectionException += onServerException;
            server.OnClientConnected += onClientConnected;
            server.OnClientDisconnected += onClientDisconnected;
            server.OnStatusChanged += onServerStatusChanged;
        }

        private void onServerStatusChanged(object o, EventArgs a)
        {
            OnServerStatusChanged?.Invoke(o, a);
      
        }

        public void SendState(ServerXmlState state_to_send)
        {
            currentState = new ServerXmlState(state_to_send.InnerXml);
            string inner = state_to_send.InnerXml;
            foreach (NormaTCPClient cl in server.ServerClients.Values)
            {
                cl.MessageToSend = inner;
            }
        }

        public void Start()
        {
            if (server == null) initServer();
            server.Start();
            OnServerStatusChanged?.Invoke(server, new EventArgs());
        }

        public void Stop()
        {
            if (server != null)
            {
                server.Stop();
                server = null;

            }
        }
        private void onServerException(Exception ex)
        {
            OnServerConnectionException?.Invoke(ex, new EventArgs());
        }

        private void onClientConnected(NormaTCPClient client)
        { 
            client.OnMessageReceived += onClientStateReceived;
        }

        private void onClientDisconnected(NormaTCPClient client)
        {
            if (currentState.Clients.ContainsKey(client.RemoteIP))
            {
                currentState.RemoveClient(currentState.Clients[client.RemoteIP]);
                OnClientListChanged?.Invoke(currentState, new EventArgs());
            }
        }

        private void onClientStateReceived(string raw_state, NormaTCPClient client)
        {

            lock (locker)
            {
                try
                {
                    ClientXmlState clState = new ClientXmlState(raw_state);
                    if (clState.IsValid) OnClientStateReceived?.Invoke(clState, new EventArgs());
                    //client.MessageToSend = currentState.InnerXml;
                }
                catch(System.Xml.XmlException)
                {
                    return;
                }finally
                {
                    //client.Close();
                }
            }
        }
    }
    */
}
