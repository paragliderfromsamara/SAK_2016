using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Net;
using System.Net.Sockets;

namespace NormaMeasure.SocketControl.TCPControlLib
{
    public enum NORMA_SERVER_STATUS
    {
        ACTIVE,
        STOPPED,
        TRY_START
    }
    public class TCPServer : IDisposable
    {
        public EventHandler OnServerStatusChanged;
        public EventHandler OnClientDetected;
        public Exception Exception;
        public bool IsActive => status == NORMA_SERVER_STATUS.ACTIVE;
        public NORMA_SERVER_STATUS Status => status;

        private TcpListener tcpListener;
        private TCPSettingsController settings;
        private NORMA_SERVER_STATUS _status;
        private Thread serverThread;
        private NORMA_SERVER_STATUS status
        {
            set
            {
                NORMA_SERVER_STATUS sts = _status;
                _status = value;
                EventArgs a = new EventArgs();
                if (_status != sts) OnServerStatusChanged?.Invoke(this, new EventArgs());
            }
            get
            {
                return _status;
            }
        }
        public TCPServer(TCPSettingsController _settings, EventHandler on_server_status_changed)
        {
            this.settings = _settings;
            OnServerStatusChanged += on_server_status_changed;
        }

        public void Start()
        {
            if (serverThread == null)
            {
                status = NORMA_SERVER_STATUS.TRY_START;
                serverThread = new Thread(ServerThreadProcess);
                serverThread.Start();
            }
        }

        public void Stop()
        {
            if (tcpListener != null) tcpListener.Stop();
            status = NORMA_SERVER_STATUS.STOPPED;
        }

        private void ServerThreadProcess()
        {
            try
            {
                tcpListener = new TcpListener(settings.localIPAddress, settings.localPortOnSettingsFile);
                tcpListener.Start();
                status = NORMA_SERVER_STATUS.ACTIVE;
                //OnStatusChanged?.Invoke($"IP aдрес: {ipAddress}; порт: {port}", new EventArgs());
                while (true)
                {
                    TcpClient client = tcpListener.AcceptTcpClient();
                    NormaTCPClient client_object = new NormaTCPClient(client);
                    ProcessClient(client_object);
                }
            }
            catch (Exception ex)
            {
                this.Exception = ex;
            }
            finally
            {
                Dispose();
                //throw new NormaServerException();
            }
        }

        private void ProcessClient(NormaTCPClient client_object)
        {
            OnClientDetected?.Invoke(client_object, new EventArgs());
        }

        public void Dispose()
        {
            Stop();
        }
    }

}
