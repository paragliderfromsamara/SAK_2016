using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NormaMeasure.Utils;
using NormaMeasure.DBControl.Tables;


namespace TeraMicroMeasure.XmlObjects
{
    public class ClientXmlState : NormaXmlObject
    {

        public static ClientXmlState CreateDefaultByClientId(int client_id)
        {
            ClientXmlState s = new ClientXmlState();
            s.MeasureTypeId = MeasuredParameterType.Rleads;
            s.ClientID = client_id;
            s.MeasureVoltage = 10;

            return s;
        }

        public ClientXmlState() : base()
        {
            //_measure_state = new MeasureState();
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

        /// <summary>
        /// ID измеряемого параметра
        /// Должен соответствовать ID параметра из таблицы MeasuredParameterType
        /// </summary>
        public uint MeasureTypeId
        {
            get
            {
                uint v = MeasuredParameterType.Rleads;
                tryGetUIntXmlProp("MeasureTypeId", out v);
                return v;
            }
            set
            {
                setXmlProp("MeasureTypeId", value.ToString());
            }
        }

        public uint MeasureVoltage
        {
            get
            {
                uint v = 10;
                tryGetUIntXmlProp("MeasureVoltage", out v);
                return v;
            }
            set
            {
                setXmlProp("MeasureVoltage", value.ToString());
            }
        }


       

    }
}
