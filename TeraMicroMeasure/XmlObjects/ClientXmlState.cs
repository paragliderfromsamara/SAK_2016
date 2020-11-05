using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NormaMeasure.Utils;

namespace TeraMicroMeasure.XmlObjects
{
    class ClientXmlState : NormaXmlObject
    {
        public ClientXmlState() : base()
        {
        }

        public ClientXmlState(string innerXml) : base(innerXml)
        {

        }

        public int ClientID
        {
            get
            {
                int v = 0;
                tryGetIntXmlProp("ClientID", out v);
                return v;
            }
            set
            {
                setXmlProp("ClientID", value.ToString());
            }
        }

        public string ClientIP
        {
            get
            {
                return getXmlProp("ClientIP");
            }
            set
            {
                setXmlProp("ClientIP", value);
            }
        }

        public string ServerIP
        {
            get
            {
                return getXmlProp("ServerIP");
            }
            set
            {
                setXmlProp("ServerIP", value);
            }
        }
        public int ClientPort
        {
            get
            {
                int v = 8000;
                tryGetIntXmlProp("ClientPort", out v);
                return v;
            }
            set
            {
                setXmlProp("ClientPort", value.ToString());
            }
        }
        public int ServerPort
        {
            get
            {
                int v = 8000;
                tryGetIntXmlProp("ServerPort", out v);
                return v;
            }
            set
            {
                setXmlProp("ServerPort", value.ToString());
            }
        }
    }
}
