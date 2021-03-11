using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net;
using System.Net.Sockets;
using System.Diagnostics;

namespace NormaLib.SocketControl.TCPControlLib
{
    public class TCPServerClientsControl : IDisposable
    {
        public EventHandler OnClientMessageReceived;
        public EventHandler OnClientDetected;
        public EventHandler OnClientDisconnected;

        private Dictionary<string, NormaTCPClient> ServerClients;

        private TCPServer server;

        public string Answer
        {
            set
            {
                if (!string.Equals(answer, value))
                {
                    answer = value;
                    RefreshAnswerOnClients();
                }
            }
            get
            {
                return answer;
            }
        }

        private void RefreshAnswerOnClients()
        {
            foreach(var cs in ServerClients.Values)
            {
                cs.MessageToSend = Answer;
            }
        }

        private string answer = string.Empty;
         
        public TCPServerClientsControl(TCPServer _server)
        {
            ServerClients = new Dictionary<string, NormaTCPClient>();
            server = _server;
            server.OnClientDetected += OnClientDetected_Handler;
            server.OnServerStatusChanged += OnServerStatusChanged_Handler;
            server.Start();
        }

        private void OnClientDetected_Handler(object s, EventArgs e)
        {
            NormaTCPClient cl = s as NormaTCPClient;
            TCPServerClientEventArgs a = e as TCPServerClientEventArgs;
            if (!ServerClients.ContainsKey(cl.RemoteIP))
            {
                
                cl.OnClientDisconnectedWithException += disposeClient;
                cl.OnMessageReceived += OnClientMessageReceived_Handler;
                ServerClients.Add(cl.RemoteIP, cl);
                Debug.WriteLine($"OnClientDetected_Handler on TCPServerClientsControl {cl.RemoteIP} connection № {a.ClientConnectionNumber}");
                OnClientDetected?.Invoke(cl, e);
                cl.MessageToSend = answer;
                cl.InitReceiveThread();
            }
        }



        private void disposeClient(object sender, EventArgs e)
        {
            NormaTCPClient cl = sender as NormaTCPClient;
            OnClientDisconnected?.Invoke(cl, e);
            ServerClients.Remove(cl.RemoteIP);
        }

        private void disposeClients()
        {
            foreach(var cl in ServerClients.Values)
            {
                cl.Close();
            }
        }

        private void OnServerStatusChanged_Handler(object sender, EventArgs e)
        {
            if (server.Status == NORMA_SERVER_STATUS.STOPPED)
            {
                disposeClients();
                server.Stop();
            } 
        }

        private void OnClientMessageReceived_Handler(object sender, EventArgs e)
        {
            OnClientMessageReceived?.Invoke(sender, e);
        }

        public void Dispose()
        {
            disposeClients();
            server.Dispose();
            ServerClients = null;
        }
    }
}
