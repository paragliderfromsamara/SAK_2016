using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NormaMeasure.Utils;
using NormaMeasure.Devices.XmlObjects;
//using System.Diagnostics;
using System.Windows.Forms;


namespace TeraMicroMeasure.XmlObjects
{
    public class ClientXmlState : NormaXmlObject
    {
        const string clientID_TagName = "ClientID";
        const string clientIP_TagName = "ClientIP";
        const string clientPort_TagName = "ClientPort";
        const string serverIP_TagName = "ServerIP";
        const string serverPort_TagName = "ServerPort";

        private MeasureXMLState _measure_state;
        public MeasureXMLState MeasureState
        {
            get
            {
                if (_measure_state == null)
                {
                    MeasureState = MeasureXMLState.GetDefault();
                }
                return _measure_state;
            }
            set
            {
                if (value != null)
                { 
                    _measure_state = value;
                    setChangedFlag(true);
                 }
            }
        }


        public static ClientXmlState CreateDefaultByClientId(int client_id)
        {
            ClientXmlState s = new ClientXmlState();
            s.ClientID = client_id;
            

            return s;
        }

        public ClientXmlState() : base()
        {
            MeasureState = new MeasureXMLState();
        }

        public ClientXmlState(string innerXml) : base(innerXml)
        {

        }

        int client_id;
        
        public int ClientID
        {
            get
            {
                return client_id;
            }
            set
            {
                bool f = client_id != value;
                if (f)
                {
                    client_id = value;
                    setChangedFlag(f);
                }
            }
        }

        string client_ip;
        public string ClientIP
        {
            get
            {
                return client_ip;
            }
            set
            {
                bool f = client_ip != value;
                if (f)
                {
                    id = client_ip = value;
                    setChangedFlag(f);
                }
            }
        }

        string server_ip;
        public string ServerIP
        {
            get
            {
                return server_ip;
            }
            set
            {
                bool f = server_ip != value;
                if (f)
                {
                    server_ip = value;
                    setChangedFlag(f);
                }
            }
        }

        int client_port;
        public int ClientPort
        {
            get
            {
                return client_port;
            }
            set
            {
                bool f = client_port != value;
                if (f)
                {
                    client_port = value;
                    setChangedFlag(f);
                }
            }
        }

        int server_port;
        public int ServerPort
        {
            get
            {
                return server_port;
            }
            set
            {
                bool f = server_port != value;
                if (f)
                {
                    server_port = value;
                    setChangedFlag(f);
                }
            }
        }




        protected override void fillXMLDocument()
        {
            base.fillXMLDocument();
            setXmlProp(clientID_TagName, client_id.ToString());
            setXmlProp(clientIP_TagName, client_ip);
            setXmlProp(serverIP_TagName, server_ip);
            setXmlProp(clientPort_TagName, client_port.ToString());
            setXmlProp(serverPort_TagName, server_port.ToString());
            AddXMLObject(MeasureState);
        }

        protected override void buildFromXML()
        {
            base.buildFromXML();
            fillClientIDFromXML();
            fillClientIPFromXML();
            fillServerIPFromXML();
            fillClientPortFromXML();
            fillServerPortFromXML();
            fillMeasureStateFromXML();
        }

        private void fillMeasureStateFromXML()
        {
            MeasureXMLState s = new MeasureXMLState();

            try
            {
                s = new MeasureXMLState(getOuterOfXMLObject(s.RootElementTagName));
                if (s.IsValid) MeasureState = s;
            }
            catch(System.Xml.XmlException)
            {
                MeasureState = MeasureXMLState.GetDefault();
            }
            //s.InnerXml = getOuterOfXMLObject(s.RootElementTagName);
            
        }

        private void fillServerPortFromXML()
        {
            int v = 8000;
            tryGetIntXmlProp(serverPort_TagName, out v);
            server_port = v;
        }

        private void fillClientPortFromXML()
        {
            int v = 8000;
            tryGetIntXmlProp(clientPort_TagName, out v);
            client_port = v;
        }

        private void fillServerIPFromXML()
        {
            server_ip = getXmlProp(serverIP_TagName);
        }

        private void fillClientIPFromXML()
        {
            id = client_ip = getXmlProp(clientIP_TagName);
        }

        private void fillClientIDFromXML()
        {
            int _client_id = -1;
            tryGetIntXmlProp(clientID_TagName, out _client_id);
            client_id = _client_id;
        }

    }
}
