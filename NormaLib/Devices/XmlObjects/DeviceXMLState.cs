using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NormaLib.Utils;
using NormaLib.Devices;

namespace NormaLib.Devices.XmlObjects
{
    public class DeviceXMLState : NormaXmlObject
    {
        const string Serial_TagName = "Serial";
        const string TypeId_TagName = "TypeID";
        const string ClientId_TagName = "ClientID";
        const string WorkStatusId_TagName = "WorkStatusId";
        const string ModelVersion_TagName = "ModelVersion";
        const string TypeNameFull_TagName = "TypeNameFull";
        const string TypeNameShort_TagName = "TypeNameShort";
        const string IsOnPCMode_TagName = "IsOnPCMode";
        const string WorkStatusText_TagName = "WorkStatusText";
        const string RawResult_TagName = "RawResult";
        const string ConvertedResult_TagName = "ConvertedResult";
        const string MeasureStatusId_TagName = "MeasureStatusId";
        const string MeasureStatusText_TagName = "MeasureStatusText";
        const string IsOnMeasureCycle_TagName = "IsOnMeasureCycle";



        string serial;
        /// <summary>
        /// Серийный номер
        /// </summary>
        public string Serial
        {
            get
            {
                return serial;
            }
            set
            {
                bool f = serial != value;
                if (f)
                {
                    serial = value;
                    setChangedFlag(f);
                }
            }
        }

        int type_id;
        /// <summary>
        /// Тип прибора
        /// </summary>
        public int TypeId
        {
            get
            {
                return type_id;
            }
            set
            {
                bool f = type_id != value;
                if (f)
                {
                    type_id = value;
                    setChangedFlag(f);
                }
            }
        }

        int client_id;
        /// <summary>
        /// ID клиента, которым используется в данный момент прибор
        /// </summary>
        public int ClientId
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

        int work_status_id;
        public int WorkStatusId
        {
            get
            {
                return work_status_id;
            }
            set
            {
                bool f = work_status_id != value;
                if (f)
                {
                    work_status_id = value;
                    setChangedFlag(f);
                }
            }
        }

        string work_status_text;
        public string WorkStatusText
        {
            get
            {
                return work_status_text;
            }
            set
            {
                bool f = work_status_text != value;
                if (f)
                {
                    work_status_text = value;
                    setChangedFlag(f);
                }
            }
        }

        string type_name_full;
        public string TypeNameFull
        {
            get
            {
                return type_name_full;
            }
            set
            {
                bool f = type_name_full != value;
                if (f)
                {
                    type_name_full = value;
                    setChangedFlag(f);
                }
            }
        }

        string type_name_short;
        public string TypeNameShort
        {
            get
            {
                return type_name_short;
            }
            set
            {
                bool f = type_name_short != value;
                if (f)
                {
                    type_name_short = value;
                    setChangedFlag(f);
                }
            }
        }


        bool is_on_pc_mode;
        public bool IsOnPCMode
        {
            get
            {
                return is_on_pc_mode;
            }
            set
            {
                bool f = is_on_pc_mode != value;
                if (f)
                {
                    is_on_pc_mode = value;
                    setChangedFlag(f);
                }
            }
        }

        bool is_on_measure_cycle;

        public bool IsOnMeasureCycle
        {
            get
            {
                return is_on_measure_cycle;
            }
            set
            {
                bool f = is_on_measure_cycle != value;
                if (f)
                {
                    is_on_measure_cycle = value;
                    setChangedFlag(f);
                }
            }
        }

        private double raw_result;
        public double RawResult
        {
            get
            {
                return raw_result;
            }
            set
            {
                bool f = raw_result != value;
                if (f)
                {
                    raw_result = value;
                    setChangedFlag(f);
                }
            }
        }
        private double converted_result;
        public double ConvertedResult
        {
            get
            {
                return converted_result;
            }
            set
            {
                bool f = converted_result != value;
                if (f)
                {
                    converted_result = value;
                    setChangedFlag(f);
                }
            }
        }



        private uint measure_status_id;
        public uint MeasureStatusId
        {
            get
            {
                return measure_status_id;
            }
            set
            {
                bool f = measure_status_id != value;
                if (f)
                {
                    measure_status_id = value;
                    setChangedFlag(f);
                }
            }
        }

        private string measure_status_text = string.Empty;
        public string MeasureStatusText
        {
            get
            {
                return measure_status_text;
            }
            set
            {
                bool f = measure_status_text != value;
                if (f)
                {
                    measure_status_text = value;
                    setChangedFlag(f);
                }
            }
        }

        public DeviceXMLState(string innerXml) : base(innerXml)
        {

        }

        public DeviceXMLState(DeviceBase device) : base()
        {
            ClientId = device.ClientId;
            Serial = device.Serial;
            WorkStatusId = (int)device.WorkStatus;
            WorkStatusText = device.WorkStatusText;
            TypeId = (int)device.TypeId;
            TypeNameFull = device.TypeNameFull;
            TypeNameShort = device.TypeNameShort;
            IsOnPCMode = device.IsOnPCMode;
            MeasureStatusId = device.MeasureStatusId;
            RawResult = device.RawResult;
            ConvertedResult = device.ConvertedResult;
            IsOnMeasureCycle = device.IsOnMeasureCycle;
            MeasureStatusId = device.MeasureStatusId;
            MeasureStatusText = device.MeasureStatusText;

            id = $"{TypeId}-{Serial}";
        }

        protected override void fillXMLDocument()
        {
            base.fillXMLDocument();
            setXmlProp(Serial_TagName, Serial);
            setXmlProp(TypeId_TagName, TypeId.ToString());
            setXmlProp(WorkStatusId_TagName, WorkStatusId.ToString());
            setXmlProp(ClientId_TagName, ClientId.ToString());
            setXmlProp(TypeNameFull_TagName, type_name_full);
            setXmlProp(TypeNameShort_TagName, type_name_short);
            setXmlProp(IsOnPCMode_TagName, (is_on_pc_mode) ? "1" : "0");
            setXmlProp(WorkStatusText_TagName, work_status_text);
            setXmlProp(RawResult_TagName, raw_result.ToString());
            setXmlProp(ConvertedResult_TagName, converted_result.ToString());
            setXmlProp(MeasureStatusId_TagName, measure_status_id.ToString());
            setXmlProp(IsOnMeasureCycle_TagName, (is_on_measure_cycle) ? "1" : "0");
            setXmlProp(MeasureStatusText_TagName, measure_status_text);
        }

        protected override void buildFromXML()
        {
            base.buildFromXML();
            fillSerialFromXML();
            fillClientIdFromXML();
            fillWorkStatusIdFromXML();
            fillTypeIdFromXML();
            fillTypeNameFull();
            fillTypeNameShort();
            fillIsOnPCModeFlag();
            fillWorkStatusText();
            fillRawResult();
            fillConvertedResult();
            fillMeasureStatusId();
            fillIsOnMeasureCycle();
            fillMeasureStatusText();
        }

        private void fillMeasureStatusText()
        {
            measure_status_text = getXmlProp(MeasureStatusText_TagName);
        }

        private void fillIsOnMeasureCycle()
        { 
           is_on_measure_cycle = (getXmlProp(IsOnMeasureCycle_TagName) == "1");
        }

        private void fillMeasureStatusId()
        {
            uint val = 0;
            uint.TryParse(getXmlProp(MeasureStatusId_TagName), out val);
            measure_status_id = val;
        }

        private void fillConvertedResult()
        {
            double val = 0;
            double.TryParse(getXmlProp(ConvertedResult_TagName), out val);
            converted_result = val;
        }

        private void fillRawResult()
        {
            double val = 0;
            double.TryParse(getXmlProp(RawResult_TagName), out val);
            raw_result = val;
        }

        private void fillWorkStatusText()
        {
            work_status_text = getXmlProp(WorkStatusText_TagName);
        }

        private void fillIsOnPCModeFlag()
        {
            is_on_pc_mode = (getXmlProp(IsOnPCMode_TagName) == "1");
        }

        private void fillTypeNameShort()
        {
            type_name_short = getXmlProp(TypeNameShort_TagName);
        }

        private void fillTypeNameFull()
        {
            type_name_full = getXmlProp(TypeNameFull_TagName);
        }

        private void fillTypeIdFromXML()
        {
            int v = 0;
            tryGetIntXmlProp(TypeId_TagName, out v);
            type_id = v;
        }

        private void fillWorkStatusIdFromXML()
        {
            int v = 0;
            tryGetIntXmlProp(WorkStatusId_TagName, out v);
            work_status_id = v;
        }

        private void fillSerialFromXML()
        {
            serial = getXmlProp(Serial_TagName);
        }

        private void fillClientIdFromXML()
        {
            int v = -1;
            tryGetIntXmlProp(ClientId_TagName, out v);
            client_id = v;
        }
    }
}
