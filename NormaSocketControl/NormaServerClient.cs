using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Threading;
using System.Net;
using System.Net.Sockets;

namespace NormaSocketControl
{
    public delegate string NormaServerClientMessageDelegate(string message, NormaServerClient client);
    public delegate void NormaServerClientDelegate(NormaServerClient client);
    public delegate void NormaServerClientExceptionDelegate(string ipAddr, Exception ex);
    public class NormaServerClient
    {
        public NormaServerClientExceptionDelegate ClientConnectionException;
        public NormaServerClientMessageDelegate OnMessageReceived;
        private TcpClient tcpClient;
        private Thread clientThread;
        private string ip_address;
        public string IpAddress
        {
            get
            {
                return ip_address;
            }
        }

        public NormaServerClient(TcpClient client)
        {
            this.tcpClient = client;
            this.ip_address = tcpClient.Client.RemoteEndPoint.ToString();
        }

        public void StartThread()
        {
            if (clientThread == null && tcpClient.Connected) initClientThread();
        }

        public void Close()
        {
            if (tcpClient != null) tcpClient.Close();
            if (clientThread != null)
            {
                clientThread.Abort();
            }
        }

        private void initClientThread()
        {
            clientThread = new Thread(clientThreadProcess);
            clientThread.Start();
        }


        private void clientThreadProcess()
        {
            NetworkStream stream = null;
            try
            {
                stream = tcpClient.GetStream();
                byte[] data = new byte[64]; // буфер для получаемых данных
                while (true)
                {
                    // получаем сообщение
                    StringBuilder builder = new StringBuilder();
                    int bytes = 0;
                    do
                    {
                        bytes = stream.Read(data, 0, data.Length);
                        builder.Append(Encoding.Default.GetString(data, 0, bytes));
                    }
                    while (stream.DataAvailable);
                    string message = builder.ToString();
                    message = OnMessageReceived(message, this);
                    data = Encoding.Default.GetBytes(message);
                    stream.Write(data, 0, data.Length);
                }
            }
            catch (Exception ex)
            {
                ClientConnectionException(ip_address, ex);
            }
            finally
            {
                if (stream != null)
                    stream.Close();
                if (tcpClient != null)
                    tcpClient.Close();
            }
        }

    }
}
