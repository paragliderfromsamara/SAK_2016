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
        public NormaServerClientDelegate OnClientConnected;
        public NormaServerClientDelegate OnClientDisconnected;
        private TcpListener listener;
        private Thread serverThread;
        private string ipAddress;
        private int port;

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

        public static NormaServerClient GetToServerConnectionForClient(string localIp, int localPort, string serverIp, int serverPort)
        {
            IPEndPoint localPoint = new IPEndPoint(IPAddress.Parse(localIp), localPort);
            IPEndPoint serverPoint = new IPEndPoint(IPAddress.Parse(serverIp), serverPort);
            TcpClient cl = new TcpClient(localPoint);
            cl.Connect(serverPoint);
            return new NormaServerClient(cl);
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
            listener.Stop();
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
                //Console.WriteLine("Ожидание подключений...");
                //Debug.WriteLine("Ожидание подключений...");
                while (true)
                {
                    TcpClient client = listener.AcceptTcpClient();
                    NormaServerClient clientObject = new NormaServerClient(client);
                    addClientToList(clientObject);
                    // создаем новый поток для обслуживания нового клиента
                    // Thread clientThread = new Thread(new ThreadStart(clientObject.Process));
                    // clientThread.Start();
                    //Debug.WriteLine("Найден клиент...");
                }
            }
            catch (Exception ex)
            {
                // MessageBox.Show(ex.Message, "Ошибка соединения");
                ProcessOnServerConnectionException(ex);
            }
            finally
            {
                if (listener != null) listener.Stop();
            }
        }

        private string getLocalIpAddress()
        {
            string[] addrs = GetAvailableIpAddressList();
            if (addrs.Length > 0) return addrs[0];
            else return "";
        }

        private void addClientToList(NormaServerClient cl)
        {
            OnClientConnected(cl);
            cl.StartThread();
            Debug.WriteLine("Найден клиент... " + cl.IpAddress);
        }

       // public void Dispose()
       // {
       //     if (listener != null)
       //     {
       //         listener.Server.Dispose();
       //         listener = null;
       //         
       //     }
       // }
    }
}
