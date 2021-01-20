using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NormaMeasure.Utils;
using NormaMeasure.Devices;

namespace NormaMeasure.Devices.XmlObjects
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


        public DeviceXMLState(DeviceBase device) : base()
        {
            ClientId = device.ClientId;
            Serial = device.Serial;
            WorkStatusId = (int)device.WorkStatus;
        }

        protected override void fillXMLDocument()
        {
            base.fillXMLDocument();
            setXmlProp(Serial_TagName, Serial);
            setXmlProp(TypeId_TagName, TypeId.ToString());
            setXmlProp(WorkStatusId_TagName, WorkStatusId.ToString());
            setXmlProp(ClientId_TagName, ClientId.ToString());
        }

        protected override void buildFromXML()
        {
            base.buildFromXML();
            fillSerialFromXML();
            fillClientIdFromXML();
            fillWorkStatusIdFromXML();
            fillTypeIdFromXML();
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
