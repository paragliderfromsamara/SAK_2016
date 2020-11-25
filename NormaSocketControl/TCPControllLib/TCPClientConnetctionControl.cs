using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NormaMeasure.SocketControl.TCPControlLib
{
    public class TCPClientConnectionControl : IDisposable
    {
        public EventHandler OnConnectionStatusChanged;
        public EventHandler OnServerAnswerReceived;
        private NormaTCPClient client;
        public string MessageTo
        {
            get
            {
                return client.MessageToSend;
            }set
            {
                client.MessageToSend = value;
            }
        }

        public TCPClientConnectionControl(NormaTCPClient _client, EventHandler on_connection_status_changed)
        {
            client = _client;
            client.OnAnswerReceived += OnServerAnswerReceived_Handler;
            OnConnectionStatusChanged += on_connection_status_changed;
            client.InitSending();
        }

        public void InitSending()
        {
            client.InitSending();
        }

        public void Dispose()
        {
            client.Close();
        }

        private void OnServerAnswerReceived_Handler(object sender, EventArgs e)
        {
            //NormaTCPClientEventArgs a = e as NormaTCPClientEventArgs;
            OnServerAnswerReceived?.Invoke(sender, e);
        }
    }
}
