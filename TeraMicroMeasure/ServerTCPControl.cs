using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TeraMicroMeasure.XmlObjects;
using NormaMeasure.SocketControl;

namespace TeraMicroMeasure
{
    class ServerTCPControl
    {
        public EventHandler OnServerStarted;
        public EventHandler OnClientConnected;
        public EventHandler OnServerConnectionException;
        public EventHandler OnServerStop;

        private ServerXmlState currentState;
        private NormaServer server;
        public ServerTCPControl(ServerXmlState state)
        {
            this.currentState = state;
            

        }

        private void initServer()
        {
            server = new NormaServer(currentState.IPAddress, currentState.Port);
            server.ProcessOnServerConnectionException += onServerException;
            server.OnClientConnected += onClientConnected;
        }

        private void Start()
        {
            if (server == null) initServer();
            server.Start();
            OnServerStarted?.Invoke(server, new EventArgs());
        }

        private void Stop()
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
            
            OnClientConnected?.Invoke(client, new EventArgs());
        }
    }
}
