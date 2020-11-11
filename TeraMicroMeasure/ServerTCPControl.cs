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
    class ServerTCPControl
    {
        static object locker = new object();
        public EventHandler OnServerConnectionException;
        public EventHandler OnClientStateReceived;
        public EventHandler OnServerStatusChanged;
        public EventHandler OnClientListChanged;

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
            server.OnClientDisconnected += onClientDisconnected;
            server.OnStatusChanged += onServerStatusChanged;
        }

        private void onServerStatusChanged(object o, EventArgs a)
        {
            OnServerStatusChanged?.Invoke(o, a);
      
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
            //OnClientAccepted?.Invoke(client, new EventArgs());
            client.OnMessageReceived += onClientStateReceived;
        }

        private void onClientDisconnected(NormaTCPClient client)
        {
         
            if (currentState.Clients.ContainsKey(client.RemoteIP))
            {
                currentState.RemoveClient(currentState.Clients[client.RemoteIP]);
                OnClientListChanged?.Invoke(currentState.Clients.Values.ToArray(), new EventArgs());
            }
        }

        private void processReceivedClientState(ClientXmlState clState)
        {

        }



        private void onClientStateReceived(string raw_state, NormaTCPClient client)
        {

            lock (locker)
            {
                try
                {
                    ClientXmlState clState = new ClientXmlState(raw_state);
                    ServerXmlState nextState = new ServerXmlState(currentState.InnerXml);

                    if (!clState.IsValid)
                    {
                        //client.Close();
                        return;
                    }
                    if (nextState.Clients.ContainsKey(clState.ClientIP))
                    {
                         if (clState.StateId == nextState.Clients[clState.ClientIP].StateId) nextState.ReplaceClient(clState);
                    }
                    else
                    {
                        SynchronizeClientStateOnConnection(clState);
                        nextState.AddClient(clState);
                        OnClientListChanged?.Invoke(nextState.Clients.Values.ToArray().Clone(), new EventArgs());
                        Debug.WriteLine($"onClientStateReceived client_id = {clState.ClientID}"); 
                    }
                    OnClientStateReceived?.Invoke(clState, new EventArgs());
                    if (this.currentState.StateId == nextState.StateId) this.currentState = nextState;
                    client.MessageToSend = currentState.InnerXml;

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

        private void SynchronizeClientStateOnConnection(ClientXmlState clState)
        {
            Debug.WriteLine($"SynchronizeClientStateOnConnection was_client_id = {clState.ClientID}");
            Debug.WriteLine($"SynchronizeClientStateOnConnection client_ip = {clState.ClientIP}");
            if (clState.ClientID == 0) clState.ClientID = SettingsControl.GetClientIdByIp(clState.ClientIP);
            Debug.WriteLine($"SynchronizeClientStateOnConnection next_client_id = {clState.ClientID}");
            SettingsControl.SetClientIP(clState.ClientID, clState.ClientIP);
        }
    }
}
