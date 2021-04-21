using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Net;
using System.Net.Sockets;
using System.Diagnostics;
using System.Runtime.InteropServices;

namespace NormaLib.SocketControl.TCPControlLib
{
    class TCPServerClientEventArgs : EventArgs
    {
        public int ClientConnectionNumber;
        public TCPServerClientEventArgs(int connection_number) : base()
        {
            ClientConnectionNumber = connection_number;
        }
    }
    public enum NORMA_SERVER_STATUS
    {
        ACTIVE,
        STOPPED,
        TRY_START,
        ABORTED
    }

    public enum EXECUTION_STATE : uint
    {
        ES_AWAYMODE_REQUIRED = 0x00000040,
        ES_CONTINUOUS = 0x80000000,
        ES_DISPLAY_REQUIRED = 0x00000002,
        ES_SYSTEM_REQUIRED = 0x00000001,
        ES_USER_PRESENT = 0x00000004
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
        private Dictionary<string, int> ClientConnectionCounter = new Dictionary<string, int>();

        [DllImport("kernel32.dll", CharSet = CharSet.Auto, SetLastError = true)]
        static extern uint SetThreadExecutionState(EXECUTION_STATE esFlags);
        private void KeepAlive()
        {
            
            SetThreadExecutionState(EXECUTION_STATE.ES_SYSTEM_REQUIRED | EXECUTION_STATE.ES_CONTINUOUS);
        }

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
            tcpListener = new TcpListener(settings.localIPAddress, settings.localPortOnSettingsFile);
            try
            {
                
                tcpListener.Start();
                status = NORMA_SERVER_STATUS.ACTIVE;
                while (true)
                {
                    KeepAlive();
                    TcpClient client = tcpListener.AcceptTcpClient();
                    NormaTCPClient client_object = new NormaTCPClient(client);
                    ProcessClient(client_object);
                }
            }
            catch (SocketException ex) when (ex.ErrorCode == 10004 || ex.ErrorCode == 10049)
            {
                return;
            }
            catch (Exception ex)
            {
                this.Exception = ex;
            }
            finally
            {
                Dispose();
            }
        }

        private void ProcessClient(NormaTCPClient client_object)
        {

            OnClientDetected?.Invoke(client_object, new TCPServerClientEventArgs(RefreshConnectionCounter(client_object)));
            
        }

        private int RefreshConnectionCounter(NormaTCPClient cl)
        {
            if (!ClientConnectionCounter.ContainsKey(cl.RemoteIP)) ClientConnectionCounter[cl.RemoteIP] = 0;
            return ++ClientConnectionCounter[cl.RemoteIP];
        }

        public void Dispose()
        {
            Stop();
        }
    }

}
