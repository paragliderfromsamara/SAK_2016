using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net;
using System.Net.Sockets;
using System.Threading;
using System.Diagnostics;
namespace NormaMeasure.SocketControl
{
    public delegate void NormaTCPMessageDelegate(string message, NormaTCPClient client);
    public delegate void NormaTCPClientDelegate(NormaTCPClient client);
    public delegate void NormaTCPClientExceptionDelegate(string ipAddr, Exception ex);
    public class NormaTCPClient
    {
        public NormaTCPMessageDelegate OnAnswerReceived;
        public NormaTCPMessageDelegate OnMessageReceived;
        public NormaTCPClientExceptionDelegate ClientReceiveMessageException;
        public NormaTCPClientExceptionDelegate ClientSendMessageException;

        protected TcpClient tcpClient;
        protected string remoteIP;
        protected string localIP;
        protected int localPort;
        protected int remotePort;
        protected Thread clientThread;
        public string RemoteIP => remoteIP;
        public string LocalIP => localIP;
        private string messageToSend;
        private bool messageSentFlag = true;
        public string MessageToSend
        {
            set
            {
                messageToSend = value;
                messageSentFlag = false;
            }
            get
            {
                return messageToSend;
            }
        }

        public NormaTCPClient(string local_ip, string remote_ip, int local_port, int remote_port)
        {
            localIP = local_ip;
            remoteIP = remote_ip;
            localPort = local_port;
            remotePort = remote_port;
        }

        public NormaTCPClient(TcpClient _client)
        {
            string fullRemoteAddr = _client.Client.RemoteEndPoint.ToString();
            string fullLocalAddr = _client.Client.LocalEndPoint.ToString();
            this.remoteIP = getIPFromFullAddr(fullRemoteAddr); //fullRemoteAddr.Substring(0, fullRemoteAddr.IndexOf(':'));
            this.localIP = getIPFromFullAddr(fullLocalAddr);// fullLocalAddr.Substring(0, fullLocalAddr.IndexOf(':'));
            this.localPort = getPortFromFullAddr(fullLocalAddr);
            this.remotePort = getPortFromFullAddr(fullRemoteAddr);
            tcpClient = _client;
        }

        private string getIPFromFullAddr(string addr)
        {
            return addr.Substring(0, addr.IndexOf(':'));
        }

        private int getPortFromFullAddr(string addr)
        {
            int v = 8000;
            string strPort;
            try
            {
                strPort = addr.Substring(addr.IndexOf(':') + 1, 4);
            }
            catch
            {
                strPort = "8000";
            }
            int.TryParse(strPort, out v);
            return v;
        }

        private void initTCPClient()
        {
            IPEndPoint localPoint = new IPEndPoint(IPAddress.Parse(localIP), localPort);
            IPEndPoint remotePoint = new IPEndPoint(IPAddress.Parse(remoteIP), remotePort);
            tcpClient = new TcpClient(localPoint);
            tcpClient.Connect(remotePoint);
           // InitOnClientThread();
        }

        public void InitOnServerThread()
        {
            clientThread = new Thread(clientReceiveProcess);
            clientThread.Start();
        }

        public void InitOnClientThread()
        {
            clientThread = new Thread(clientSendProcess);
            clientThread.Start();
        }

        public void Send(string message)
        {
            if (tcpClient == null) initTCPClient();
            MessageToSend = message;
            InitOnClientThread();
        }

        private void clientSendProcess()
        {
            NetworkStream stream = null;
            try
            {
                tcpClient.ReceiveTimeout = 3000;
                tcpClient.SendTimeout = 3000;
                stream = tcpClient.GetStream();

                byte[] dataIn = new byte[256]; // буфер для получаемых данных
                byte[] dataOut = Encoding.Default.GetBytes(MessageToSend);
               // while (true)
               // {
                    // получаем сообщение
              //      if (messageSentFlag) continue;
                    StringBuilder builder = new StringBuilder();
                    int bytes = 0;
                    stream.Write(dataOut, 0, dataOut.Length);
                    do
                    {
                        bytes = stream.Read(dataIn, 0, dataIn.Length);
                        builder.Append(Encoding.Default.GetString(dataIn, 0, bytes));
                    }
                    while (stream.DataAvailable);
                    string recMessage = builder.ToString();
                    recMessage.Trim();
                    OnAnswerReceived(recMessage, this);
                    messageSentFlag = true;
               // }
               stream.Close();
               tcpClient.Close();
            }
            catch (Exception ex)
            {
                ClientSendMessageException?.Invoke(remoteIP, ex);
            }
            finally
            {
                if (stream != null)
                    stream.Close();
                if (tcpClient != null)
                {
                    dispose_tcp_client();
                }
                    
            }
        }

        public void Close()
        {
            if (clientThread != null)
            {
                clientThread.Abort();
            }
            if (tcpClient != null) dispose_tcp_client();
            
        }

        private void clientReceiveProcess()
        {
            NetworkStream stream = null;

            try
            {
                tcpClient.ReceiveTimeout = 3000;
                tcpClient.SendTimeout = 3000;
                stream = tcpClient.GetStream();
                byte[] data = new byte[256]; // буфер для получаемых данных
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
                    string recMessage = builder.ToString();
                    recMessage.Trim();
                    if (!string.IsNullOrWhiteSpace(recMessage))
                    {
                        OnMessageReceived(recMessage, this);
                        data = Encoding.Default.GetBytes(MessageToSend);
                        stream.Write(data, 0, data.Length);
                    }else
                    {
                        data = Encoding.Default.GetBytes("EMPTY");
                        stream.Write(data, 0, data.Length);
                    }

                }
            }
            catch (Exception ex)
            {
                ClientReceiveMessageException?.Invoke(remoteIP, ex);
                Debug.WriteLine("Клиент отвалился от сервера");
            }
            finally
            {
                if (stream != null)
                    stream.Close();
                if (tcpClient != null) dispose_tcp_client();
                    
            }
        }

        private void dispose_tcp_client()
        {
            tcpClient.Close();
            tcpClient.Dispose();
            tcpClient = null;
        }
    }


}
