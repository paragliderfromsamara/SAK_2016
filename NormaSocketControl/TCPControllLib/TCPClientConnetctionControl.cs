using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NormaMeasure.SocketControl.TCPControlLib
{
    public class TCPClientConnectionControl
    {
        public EventHandler OnConnectionStatusChanged;
        private NormaTCPClient client;
        public TCPClientConnectionControl(NormaTCPClient _client)
        {
            client = _client;
        }
    }
}
