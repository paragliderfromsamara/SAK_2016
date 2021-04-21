using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net;
using System.Net.Sockets;
using System.Threading;
using System.Diagnostics;
using NormaLib.SocketControl.TCPControlLib;
using System.Windows.Forms;
using System.Runtime.InteropServices;
using NormaLib.SocketControl.TCPControllLib;

namespace NormaLib.SocketControl
{
    public enum TCP_CLIENT_STATUS
    {
        CONNECTED,
        DISCONNECTED,
        TRY_CONNECT,
        WILL_DISCONNECT, 
        ABORTED
    }
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
    public class NormaTCPClient : IDisposable
    {
        public EventHandler OnAnswerReceived;
        public EventHandler OnMessageReceived;
        public EventHandler OnClientStatusChanged;
        public EventHandler OnClientDisconnectedWithException;
        //public NormaTCPClientExceptionDelegate ClientReceiveMessageException;
        //public NormaTCPClientExceptionDelegate ClientSendMessageException;

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
        public int LocalPort => localPort;
        public int RemotePort => remotePort;
        private string messageToSend;
        private bool sendingIsActive = false;
        private bool receiveIsActive = false;
        public Exception Exception;

        public TCP_CLIENT_STATUS Status => status;
        private TCP_CLIENT_STATUS _status = TCP_CLIENT_STATUS.DISCONNECTED;
        private TCPSettingsController _tcpSettingsController;

        [DllImport("kernel32.dll", CharSet = CharSet.Auto, SetLastError = true)]
        static extern uint SetThreadExecutionState(EXECUTION_STATE esFlags);
        private void KeepAlive()
        {

            SetThreadExecutionState(EXECUTION_STATE.ES_SYSTEM_REQUIRED | EXECUTION_STATE.ES_CONTINUOUS);
        }

        private TCPSettingsController tcpSettingsController
        {
            get
            {
                return _tcpSettingsController;
            }set
            {
                if (value != null)
                {
                    localIP = value.localIPOnSettingsFile;
                    localPort = value.localPortOnSettingsFile;
                    if (!value.IsServerSettings)
                    {
                        remoteIP = value.serverIPOnSettingsFile;
                        remotePort = value.serverPortOnSettingsFile;
                    }
                }
                _tcpSettingsController = value;
            }
        }

        private TCP_CLIENT_STATUS status
        {
            get
            {
                return _status;
            }set
            {
                if (_status != value)
                {
                    _status = value;
                    OnClientStatusChanged?.Invoke(this, new EventArgs());
                }
            }
        }

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

        public TcpClient TCPClient
        {
            set
            {
                tcpClient = value;
                string fullRemoteAddr = tcpClient.Client.RemoteEndPoint.ToString();
                string fullLocalAddr = tcpClient.Client.LocalEndPoint.ToString();
                this.remoteIP = GetIPFromFullAddr(fullRemoteAddr); //fullRemoteAddr.Substring(0, fullRemoteAddr.IndexOf(':'));
                this.localIP = GetIPFromFullAddr(fullLocalAddr);// fullLocalAddr.Substring(0, fullLocalAddr.IndexOf(':'));
                this.localPort = getPortFromFullAddr(fullLocalAddr);
                this.remotePort = getPortFromFullAddr(fullRemoteAddr);
            }
        }

        public NormaTCPClient(TcpClient _client)
        {
            TCPClient = _client;
        }

        public NormaTCPClient(TCPSettingsController tcp_settings_controller)
        {
            this.tcpSettingsController = tcp_settings_controller;

        }

        public static string GetIPFromFullAddr(string addr)
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
                status = TCP_CLIENT_STATUS.TRY_CONNECT;
                clientThread = new Thread(new ThreadStart(receiveProcess));
                RefreshEstimateConnectionTime();
                clientThread.Start();
            }
        }

        public void InitSending()
        {
            clientThread = new Thread(new ThreadStart(sendLoopProcess));
            clientThread.Name = $"Client_{LocalIP}";
            status = TCP_CLIENT_STATUS.TRY_CONNECT;
            RefreshEstimateConnectionTime();
            clientThread.Start();
        }

        public bool ConnectionTimeoutIsOver
        {
            get
            {
                return DateTime.Compare(estimateTryConnectionTime, DateTime.Now) <= 0;
            }
        }
        public DateTime EstimateConnectionTime => estimateTryConnectionTime;
        private DateTime estimateTryConnectionTime;
        private void RefreshEstimateConnectionTime()
        {
            estimateTryConnectionTime = DateTime.Now.AddSeconds(5);
        }

        private void sendLoopProcess()
        {
            uint failedSendCounter = 0;
            uint goodSendCounter = 0;
            string message = "";
            sendingIsActive = true;
            Thread connectionStatusChecker = new Thread(new ThreadStart(()=> {
                while(!ConnectionTimeoutIsOver && sendingIsActive)
                {
                    Thread.Sleep(750);
                }
                sendingIsActive = false;
                status = TCP_CLIENT_STATUS.DISCONNECTED;
            }));
            connectionStatusChecker.Start();
            while (sendingIsActive)
            {
                KeepAlive();
                if (oneTimeSend(out message))
                {
                    OnAnswerReceived_Handler(message);
                    status = TCP_CLIENT_STATUS.CONNECTED;
                    goodSendCounter++;
                    Thread.Sleep(300);
                    failedSendCounter = 0;
                    RefreshEstimateConnectionTime();
                }
                else
                {
                    status = TCP_CLIENT_STATUS.TRY_CONNECT;
                    failedSendCounter++;
                }
            }
            status = TCP_CLIENT_STATUS.DISCONNECTED;
        }

        

        private bool oneTimeSend(out string answer)
        {
            NetworkStream stream = null;
            answer = "";
            try
            {
                IPEndPoint localPoint = new IPEndPoint(tcpSettingsController.localIPAddress, tcpSettingsController.localPortOnSettingsFile);
                IPEndPoint remotePoint = new IPEndPoint(tcpSettingsController.serverIPAddress, tcpSettingsController.serverPortOnSettingsFile);
                tcpClient = new TcpClient(localPoint);
                tcpClient.ReceiveTimeout = receiveTimeout;
                tcpClient.SendTimeout = sendTimeout;
                tcpClient.Connect(remotePoint);
                stream = tcpClient.GetStream();
                byte[] dataIn = new byte[32768];
                byte[] dataOut;
                KeepAlive();
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
                answer = builder.ToString();
                answer.Trim();
                return true;
            }
            catch (ArgumentNullException e)
            {
                this.Exception = e;
                return false;
            }
            catch (SocketException e)
            {
                this.Exception = e;
                return false;
            }
            catch (Exception e)
            {
                this.Exception = e;
                return false;
            }
            finally
            {
                if (stream != null) stream.Close();
                if (tcpClient != null)
                {
                    dispose_tcp_client();
                }
            }
            
        }

        private void OnAnswerReceived_Handler(string recMessage)
        {
            OnAnswerReceived?.Invoke(this, new NormaTCPClientEventArgs(recMessage));
        }

        public void Close()
        {
            if (status == TCP_CLIENT_STATUS.CONNECTED) status = TCP_CLIENT_STATUS.WILL_DISCONNECT;
            sendingIsActive = false;
            receiveIsActive = false;
        }

        private void receiveProcess()
        {
            NetworkStream stream = null;

            try
            {
                tcpClient.ReceiveTimeout = receiveTimeout;
                tcpClient.SendTimeout = sendTimeout;
                stream = tcpClient.GetStream();
                byte[] data = new byte[512]; // буфер для получаемых данных
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
                    status = TCP_CLIENT_STATUS.CONNECTED;
                    RefreshEstimateConnectionTime();
                }
            }
            /*
            catch (ArgumentNullException e)
            {
                this.Exception = e;//SocketLogFile.WriteExceptionAsync(e).GetAwaiter();
            }
            catch (SocketException e)
            {
                this.Exception = e;//SocketLogFile.WriteExceptionAsync(e).GetAwaiter();
            }
            catch (Exception e)
            {
                this.Exception = e;//SocketLogFile.WriteExceptionAsync(e).GetAwaiter();
            }
            */
            catch (Exception ex)
            {
                this.Exception = ex;
                OnClientDisconnectedWithException?.Invoke(this, new EventArgs());
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

        public void Dispose()
        {
            Close();
        }
    }


}
