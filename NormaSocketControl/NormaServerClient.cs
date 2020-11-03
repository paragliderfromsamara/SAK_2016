using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Threading;
using System.Net;
using System.Net.Sockets;

namespace NormaMeasure.SocketControl
{
    public delegate string NormaServerClientMessageDelegate(string message, NormaServerClient client);
    public delegate void NormaServerClientDelegate(NormaServerClient client);
    public delegate void NormaServerClientExceptionDelegate(string ipAddr, Exception ex);
    public class NormaServerClient
    {
        public NormaServerClientMessageDelegate MessageSent;
        public NormaServerClientExceptionDelegate ClientConnectionException;
        public NormaServerClientMessageDelegate OnMessageReceived;
        private TcpClient tcpClient;
        private Thread clientThread;
        private string ip_address;
        private string _message_from = string.Empty;
        private string _message_to = string.Empty;
        private bool HasDataForSend => !String.IsNullOrEmpty(_message_to);
        public string MessageTo
        {
            set
            {
                _message_to = value;
            }
            get
            {
                return _message_to;
            }
        }
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
            string fullAddr = tcpClient.Client.RemoteEndPoint.ToString();
            this.ip_address = fullAddr.Substring(0, fullAddr.IndexOf(':'));
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
                byte[] data = new byte[512]; // буфер для получаемых данных
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
                    if (!string.IsNullOrWhiteSpace(message))
                    {
                        MessageTo = OnMessageReceived(message, this);
                    }
                    if (HasDataForSend)
                    {
                        data = Encoding.Default.GetBytes(MessageTo);
                        stream.Write(data, 0, data.Length);
                        MessageSent?.Invoke(MessageTo, this);
                        MessageTo = string.Empty;
                    }
                }
            }
            catch (Exception ex)
            {
                ClientConnectionException?.Invoke(ip_address, ex);
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
