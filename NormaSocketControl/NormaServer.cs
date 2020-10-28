using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Threading;
using System.Net;
using System.Net.Sockets;
using System.Diagnostics;

namespace NormaSocketControl
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
        private bool isAbort = false;

        public string IpAddress => ipAddress;
        public NormaServer(int _port)
        {
            this.port = _port;
            this.ipAddress = getLocalIpAddress();
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
            var host = Dns.GetHostEntry(Dns.GetHostName());
            foreach (var ip in host.AddressList)
            {
                if (ip.AddressFamily == AddressFamily.InterNetwork)
                {
                    return ip.ToString();
                }
            }
            return "";
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
