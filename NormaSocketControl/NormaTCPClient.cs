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

    public class NormaTCPClientEventArgs : EventArgs
    {
        public string Message;
        public NormaTCPClientEventArgs(string _message)
        {
            Message = _message;
        }
    }

    public delegate void NormaTCPMessageDelegate(string message, NormaTCPClient client);
    public delegate void NormaTCPClientDelegate(NormaTCPClient client);
    public delegate void NormaTCPClientExceptionDelegate(string ipAddr, Exception ex);
    public class NormaTCPClient
    {
        public NormaTCPMessageDelegate OnAnswerReceived;
        public EventHandler OnMessageReceived;
        public NormaTCPClientExceptionDelegate ClientReceiveMessageException;
        public NormaTCPClientExceptionDelegate ClientSendMessageException;

        private int receiveTimeout = 500;
        private int sendTimeout = 500;
        protected TcpClient tcpClient;
        protected string remoteIP;
        protected string localIP;
        protected int localPort;
        protected int remotePort;
        protected Thread clientThread;
        public string RemoteIP => remoteIP;
        public string LocalIP => localIP;
        private string messageToSend;
        private bool sendingIsActive = false;
        private bool receiveIsActive = false;
       
        public string MessageToSend
        {
            set
            {
                messageToSend = value;
            }
            get
            {
                return messageToSend;
            }
        }
        public void SetTimeouts(int rec_time, int send_time)
        {
            this.receiveTimeout = rec_time;
            this.sendTimeout = send_time;
        }

        public void RefreshConnection()
        {
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

        public void InitReceiveThread()
        {
            if (!receiveIsActive)
            {
                clientThread = new Thread(new ThreadStart(receiveProcess));
                clientThread.Start();
            }
        }

        public void InitSending()
        {
            clientThread = new Thread(new ThreadStart(sendProcess));
            clientThread.Start();
        }

        public void Send(string message)
        {
            //if (tcpClient == null) initTCPClient();
            MessageToSend = message;
            if (!sendingIsActive) InitSending();
        }

        public void StopSending()
        {
            sendingIsActive = false;
            Thread.Sleep(300);
        }

        private void sendProcess()
        {
            NetworkStream stream = null;

            try
            {
                IPEndPoint localPoint = new IPEndPoint(IPAddress.Parse(localIP), localPort);
                IPEndPoint remotePoint = new IPEndPoint(IPAddress.Parse(remoteIP), remotePort);
                tcpClient = new TcpClient(localPoint);
                tcpClient.ReceiveTimeout = receiveTimeout;
                tcpClient.SendTimeout = sendTimeout;
                tcpClient.Connect(IPAddress.Parse(remoteIP), remotePort);
                stream = tcpClient.GetStream();
                byte[] dataIn = new byte[256]; // буфер для получаемых данных
                byte[] dataOut;
                sendingIsActive = true; 
                while (sendingIsActive)
                {
                    // получаем сообщение
                    dataOut = Encoding.Default.GetBytes(MessageToSend);
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
                    OnAnswerReceived?.Invoke(recMessage, this);
                    Thread.Sleep(300);
                 }
               stream.Close();
               dispose_tcp_client();
            }
            catch (Exception ex)
            {
                sendingIsActive = false;
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
            sendingIsActive = false;
            receiveIsActive = false;
            //if (tcpClient != null)
            //{
            //    tcpClient.GetStream().Close();
            //    tcpClient.Close();
            //    tcpClient.Dispose();
            //     tcpClient = null;
            //    if (clientThread != null) clientThread.Abort();
            // }
            //Thread.Sleep(3000);
        }

        private void receiveProcess()
        {
            NetworkStream stream = null;

            try
            {
                tcpClient.ReceiveTimeout = receiveTimeout;
                tcpClient.SendTimeout = sendTimeout;
                stream = tcpClient.GetStream();
                byte[] data = new byte[256]; // буфер для получаемых данных
                receiveIsActive = true;
                while (receiveIsActive)
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
                    OnMessageReceived_Handler(recMessage);

                    data = Encoding.Default.GetBytes(MessageToSend);
                    stream.Write(data, 0, data.Length);
                }
                stream.Close();
                dispose_tcp_client();
            }
            catch (Exception ex)
            {
                receiveIsActive = false;
                Debug.WriteLine("Клиент отвалился от сервера");
                ClientReceiveMessageException?.Invoke(remoteIP, ex);
            }
            finally
            {
                if (stream != null)
                    stream.Close();
                if (tcpClient != null) dispose_tcp_client();
            }
        }

        private void OnMessageReceived_Handler(string message)
        {
            OnMessageReceived?.Invoke(this, new NormaTCPClientEventArgs(message));
        }

        private void dispose_tcp_client()
        {
            tcpClient.Close();
            tcpClient.Dispose();
            tcpClient = null;
        }
    }


}
