﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;

namespace NormaLib.SocketControl.TCPControlLib
{
    public class TCPClientConnectionControl : IDisposable
    {
        public EventHandler OnConnectionStatusChanged;
        public EventHandler OnServerAnswerReceived;
        private NormaTCPClient client;
        public string LocalIP => client.LocalIP;
        public int LocalPort => client.LocalPort;
        public string RemoteIP => client.RemoteIP;
        public int RemotePort => client.RemotePort;
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
            client.OnClientStatusChanged += OnClientStatusChanged_Handler;
            OnConnectionStatusChanged += on_connection_status_changed;
        }

        private void OnClientStatusChanged_Handler(object sender, EventArgs e)
        {
            OnConnectionStatusChanged?.Invoke(sender, e);
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
           OnServerAnswerReceived?.Invoke(sender, e);
        }
    }
}
