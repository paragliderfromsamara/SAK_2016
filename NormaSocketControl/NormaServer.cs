using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Threading;
using System.Net;
using System.Net.Sockets;
using System.Diagnostics;

namespace NormaMeasure.SocketControl
{
    public delegate void ProcessConnectionException(Exception ex);
    public class NormaServer
    {
        public ProcessConnectionException ProcessOnServerConnectionException;
        public NormaTCPClientDelegate OnClientConnected;
        public NormaTCPClientDelegate OnClientDisconnected;
        public EventHandler OnStatusChanged;
        private TcpListener listener;
        private Thread serverThread;
        private string ipAddress;
        private int port;
        public Dictionary<string, NormaTCPClient> ServerClients;
      

        public static bool IsValidIPString(string ip)
        {
            try
            {
                IPAddress.Parse(ip);
                return true;
            }catch
            {
                return false;
            }
        }


        public static string[] GetAvailableIpAddressList()
        {
            var host = Dns.GetHostEntry(Dns.GetHostName());
            List<string> addrs = new List<string>();
            foreach (var ip in host.AddressList)
            {
                if (ip.AddressFamily == AddressFamily.InterNetwork)
                {
                    addrs.Add(ip.ToString());
                }
            }
            return addrs.ToArray();
        }

        public static bool IncludesIpOnList(string ip)
        {
            string[] ipList = GetAvailableIpAddressList();
            if (ipList.Length > 0)
            {
                if (ipList[0] == "127.0.0.1" && ipList.Length == 1) return false;
                foreach(var s in ipList)
                {
                    if (s == ip) return true;
                }
            }
            return false;
        }

        public string IpAddress => ipAddress;
        public int Port => port;
        public NormaServer(string _ip, int _port)
        {
            this.ServerClients = new Dictionary<string, NormaTCPClient>();
            this.port = _port;
            this.ipAddress = _ip;
        }   

        /// <summary>
        /// Запуск сервера с проверкой предварительного
        /// </summary>
        public void Start()
        {
            if (serverThread == null)
            {
                serverThread = new Thread(serverThreadProcess);
                serverThread.Start();
            }
        }

        public void Stop()
        {
            if (listener != null )listener.Stop();
            foreach(NormaTCPClient cl in ServerClients.Values)
            {
                cl.Close();
            }
        }

        /// <summary>
        /// Запуск сервера
        /// </summary>
        private void serverThreadProcess()
        {
            try
            {
                listener = new TcpListener(IPAddress.Parse(ipAddress), port);
                listener.Start();
                OnStatusChanged?.Invoke($"IP aдрес: {ipAddress}; порт: {port}", new EventArgs());
                while (true)
                {
                    TcpClient client = listener.AcceptTcpClient();
                    NormaTCPClient clientObject = new NormaTCPClient(client);
                    processClient(clientObject);
                }
            }
            catch (Exception ex)
            {
                ProcessOnServerConnectionException(ex);
            }
            finally
            {
                if (listener != null) listener.Stop();
                OnStatusChanged?.Invoke($"Сервер не активен", new EventArgs());

            }
        }


        private void processClient(NormaTCPClient cl)
        {
            if (!ServerClients.ContainsKey(cl.RemoteIP))
            {
                cl.ClientReceiveMessageException += disposeClient;
                ServerClients.Add(cl.RemoteIP, cl);
                OnClientConnected?.Invoke(cl);
                cl.InitReceiveThread();
            }

            Debug.WriteLine("Найден клиент... " + cl.RemoteIP);
        }


        private void disposeClient(string ip, Exception ex)
        {
            NormaTCPClient cl = ServerClients[ip];
            OnClientDisconnected?.Invoke(cl);
            ServerClients.Remove(ip);
        }
    }
}
