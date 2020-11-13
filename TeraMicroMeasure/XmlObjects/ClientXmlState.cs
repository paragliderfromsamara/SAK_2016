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
            _measure_state = new MeasureState();
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
                ElementId = value;
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

        private MeasureState _measure_state;
        public MeasureState MeasureState
        {
            get
            {
                if (_measure_state == null)
                {
                    _measure_state = new MeasureState();
                    try
                    {
                        string v = InnerXmlOfElement(_measure_state.GetType().Name);
                        if (string.IsNullOrEmpty(v)) _measure_state = new MeasureState();
                        else _measure_state = new MeasureState(v);
                    }
                    catch (System.Xml.XmlException)
                    {
                        _measure_state = new MeasureState();
                    }
                }

                return _measure_state;
            }set
            {
                ReplaceElement(_measure_state.GetType().Name, value.InnerXml);
                _measure_state = value;
            }
        }

    }
}
