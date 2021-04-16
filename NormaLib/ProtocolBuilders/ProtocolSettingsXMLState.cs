using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NormaLib.Utils;

namespace NormaLib.ProtocolBuilders
{
    public class ProtocolSettingsXMLState : NormaXmlObject
    {
        private static ProtocolSettingsXMLState current_state = null;
        public static ProtocolSettingsXMLState CurrentState
        {
            get
            {
                if (current_state == null)
                {
                    CurrentState = new ProtocolSettingsXMLState();
                }
                return current_state;
            }private set
            {
                current_state = value;
            }
        }

        public ProtocolSettingsXMLState() : base()
        {
            CompanyName = ProtocolSettings.CompanyName;
            ProtocolHeader = ProtocolSettings.ProtocolHeader;
            AddTestIdToProtocolTitleFlag = ProtocolSettings.DoesAddTestIdOnProtocolHeader;
        }

        public ProtocolSettingsXMLState(string inner_xml) : base(inner_xml)
        {

        }


        string company_name = string.Empty;
        /// <summary>
        /// Название компании
        /// </summary>
        public string CompanyName
        {
            get
            {
                return company_name;
            }
            set
            {
                bool f = company_name != value;
                if (f)
                {
                    company_name = value;
                    setChangedFlag(f);
                }
            }
        }

        string protocol_header = string.Empty;
        /// <summary>
        /// Заголовок протокола
        /// </summary>
        public string ProtocolHeader
        {
            get
            {
                return protocol_header;
            }
            set
            {
                bool f = protocol_header != value;
                if (f)
                {
                    protocol_header = value;
                    setChangedFlag(f);
                }
            }
        }

        bool add_test_id_to_protocol_title_flag = false;
        /// <summary>
        /// Флаг отображения ID испытаний в заголовке протокола
        /// </summary>
        public bool AddTestIdToProtocolTitleFlag
        {
            get
            {
                return add_test_id_to_protocol_title_flag;
            }
            set
            {
                bool f = add_test_id_to_protocol_title_flag != value;
                if (f)
                {
                    add_test_id_to_protocol_title_flag = value;
                    setChangedFlag(f);
                }
            }
        }


        protected override void buildFromXML()
        {
            base.buildFromXML();
            fillCompanyName();
            fillProtocolTitle();
            fillAddTestIdToProtocolTitleFlag();

        }

        protected override void fillXMLDocument()
        {
            base.fillXMLDocument();
            setXmlProp(CompanyName_TagName, company_name);
            setXmlProp(ProtocolHeader_TagName, protocol_header);
            setXmlProp(AddTestIdToProtocolTitleFlag_TagName, add_test_id_to_protocol_title_flag ? "1" : "0");
        }

        private void fillCompanyName()
        {
            company_name = getXmlProp(CompanyName_TagName);
        }

        private void fillProtocolTitle()
        {
            protocol_header = getXmlProp(ProtocolHeader_TagName);
        }

        private void fillAddTestIdToProtocolTitleFlag()
        {
            add_test_id_to_protocol_title_flag = getXmlProp(AddTestIdToProtocolTitleFlag_TagName) == "1";
        }

        const string CompanyName_TagName = "CompanyName";
        const string ProtocolHeader_TagName = "ProtocolHeader";
        const string AddTestIdToProtocolTitleFlag_TagName = "AddTestIdToProtocolTitleFlag";

        public static void CheckCurrentStateChanges(ProtocolSettingsXMLState protocolSettingsState)
        {
            if (CurrentState.StateId != protocolSettingsState.StateId)
            {
                if (protocolSettingsState.CompanyName != CurrentState.CompanyName) ProtocolSettings.CompanyName = protocolSettingsState.CompanyName;
                if (protocolSettingsState.ProtocolHeader != CurrentState.ProtocolHeader) ProtocolSettings.ProtocolHeader = protocolSettingsState.ProtocolHeader;
                if (protocolSettingsState.AddTestIdToProtocolTitleFlag != CurrentState.AddTestIdToProtocolTitleFlag) ProtocolSettings.DoesAddTestIdOnProtocolHeader = protocolSettingsState.AddTestIdToProtocolTitleFlag;
                CurrentState = new ProtocolSettingsXMLState(protocolSettingsState.InnerXml);
            }
            
        }
    }
}
