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
        const string clientID_TagName = "ClientID";
        const string clientIP_TagName = "ClientIP";
        const string clientPort_TagName = "ClientPort";
        const string serverIP_TagName = "ServerIP";
        const string serverPort_TagName = "ServerPort";
        const string measureParameterType_TagName = "MeasureParameterType";
        const string measureVoltage_TagName = "MeasureVoltage";
        const string measuredCableID_TagName = "MeasuredCableId";
        const string measuredCableLength_TagName = "MeasuredCableLength";
        const string measuredMaterialId_TagName = "MeasuredMaterialID";
        const string temperature_TagName = "Temperature";
        const string beforeMeasureDelay_TagName = "BeforeMeasureDelay";
        const string afterMeasureDelay_TagName = "AfterMeasureDelay";
        const string averagingTimes_TagName = "AveragingTimes";

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


        uint measured_parameter_type_id = 1;
        /// <summary>
        /// ID измеряемого параметра
        /// Должен соответствовать ID параметра из таблицы MeasuredParameterType
        /// </summary>
        public uint MeasureTypeId
        {
            get
            {
                return measured_parameter_type_id;
            }
            set
            {
                bool f = measured_parameter_type_id != value;
                if (f)
                {
                    measured_parameter_type_id = value;
                    setChangedFlag(f);
                }
            }
        }

        uint measure_voltage = 10;
        /// <summary>
        /// Испытательное напряжение
        /// </summary>
        public uint MeasureVoltage
        {
            get
            {
                return measure_voltage;
            }
            set
            {
                bool f = measure_voltage != value;
                if (f)
                {
                    measure_voltage = value;
                    setChangedFlag(f);
                }
            }
        }

        int measured_cable_id = -1;
        public int MeasuredCableID
        {
            get
            {
                return measured_cable_id;
            }
            set
            {
                bool f = measured_cable_id != value;
                if (f)
                {
                    measured_cable_id = value;
                    setChangedFlag(f);
                }
            }
        }

        uint measured_cable_length = 1000;

        public uint MeasuredCableLength
        {
            get
            {
                return measured_cable_length;
            }
            set
            {
                bool f = measured_cable_length != value;
                if (f)
                {
                    measured_cable_length = value;
                    setChangedFlag(f);
                }
            }
        }

        int measured_material_id = -1;
        public int MeasuredMaterialID
        {
            get
            {
                return measured_material_id;
            }
            set
            {
                bool f = measured_material_id != value;
                if (f)
                {
                    measured_material_id = value;
                    setChangedFlag(f);
                }
            }
        }

        uint temperature = 20;
        public uint Temperature
        {
            get
            {
                return temperature;
            }
             set
            {
                bool f = temperature != value;
                if (f)
                {
                    temperature = value;
                    setChangedFlag(f);
                }
            }
        }

        uint before_measure_delay = 0;

        public uint BeforeMeasureDelay
        {
            get
            {
                return before_measure_delay;
            }
            set
            {
                bool f = before_measure_delay != value;
                if (f)
                {
                    before_measure_delay = value;
                    setChangedFlag(f);
                }
            }
        }

        uint after_measure_delay = 0;

        public uint AfterMeasureDelay
        {
            get
            {
                return after_measure_delay;
            }
            set
            {
                bool f = after_measure_delay != value;
                if (f)
                {
                    after_measure_delay = value;
                    setChangedFlag(f);
                }
            }
        }

        uint averaging_times = 0;
        public uint AveragingTimes
        {
            get
            {
                return averaging_times;
            }
            set
            {
                bool f = averaging_times != value;
                if (f)
                {
                    averaging_times = value;
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
            setXmlProp(measureParameterType_TagName, measured_parameter_type_id.ToString());
            setXmlProp(measureVoltage_TagName, measure_voltage.ToString());
            setXmlProp(measuredCableID_TagName, measured_cable_id.ToString());
            setXmlProp(measuredCableLength_TagName, measured_cable_length.ToString());
            setXmlProp(measuredMaterialId_TagName, measured_material_id.ToString());
            setXmlProp(temperature_TagName, temperature.ToString());
            setXmlProp(beforeMeasureDelay_TagName, before_measure_delay.ToString());
            setXmlProp(afterMeasureDelay_TagName, after_measure_delay.ToString());
            setXmlProp(averagingTimes_TagName, averaging_times.ToString());
        }

        protected override void buildFromXML()
        {
            fillClientIDFromXML();
            fillClientIPFromXML();
            fillServerIPFromXML();
            fillClientPortFromXML();
            fillServerPortFromXML();
            fillMeasureParameterTypeFromXML();
            fillMeasureVoltageFromXML();
            fillMeasuredCableIDFromXML();
            fillMeasuredCableLengthFromXML();
            fillMeasuredMaterialIDFormXML();
            fillTemperatureFromXML();
            fillBeforeMeasureDelayFromXML();
            fillAfterMeasureDelayFromXML();
            fillAveragingTimesFromXML();
        }

        private void fillAveragingTimesFromXML()
        {
            uint v = 0;
            tryGetUIntXmlProp(averagingTimes_TagName, out v);
            averaging_times = v;
        }

        private void fillAfterMeasureDelayFromXML()
        {
            uint v = 0;
            tryGetUIntXmlProp(afterMeasureDelay_TagName, out v);
            after_measure_delay = v;
        }

        private void fillBeforeMeasureDelayFromXML()
        {
            uint v = 0;
            tryGetUIntXmlProp(beforeMeasureDelay_TagName, out v);
            before_measure_delay = v;
        }

        private void fillTemperatureFromXML()
        {
            uint v = 20;
            tryGetUIntXmlProp(temperature_TagName, out v);
            if (v < 5) v = 20;
            temperature = v;
        }

        private void fillMeasuredMaterialIDFormXML()
        {
            int v = -1;
            tryGetIntXmlProp(measuredMaterialId_TagName, out v);
            measured_material_id = v;
        }

        private void fillMeasuredCableLengthFromXML()
        {
            uint v = 1;
            tryGetUIntXmlProp(measureVoltage_TagName, out v);
            if (v == 0) v = 1;
            measured_cable_length = v;
        }

        private void fillMeasuredCableIDFromXML()
        {
            int v = -1;
            tryGetIntXmlProp(measuredCableID_TagName, out v);
            measured_cable_id = v;
        }

        private void fillMeasureVoltageFromXML()
        {
            uint v = 10;
            tryGetUIntXmlProp(measureVoltage_TagName, out v);
            if (v == 0) v = 10;
            measure_voltage = v;
        }

        private void fillMeasureParameterTypeFromXML()
        {
            uint v = 1;
            tryGetUIntXmlProp(measureParameterType_TagName, out v);
            if (v == 0) v = 1;
            measured_parameter_type_id = v;
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
            client_ip = getXmlProp(clientIP_TagName);
        }

        private void fillClientIDFromXML()
        {
            int _client_id = -1;
            tryGetIntXmlProp(clientID_TagName, out _client_id);
            client_id = _client_id;
        }

    }
}
