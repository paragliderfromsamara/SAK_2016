using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NormaLib.Utils;

namespace NormaLib.ProtocolBuilders
{
    internal static class ProtocolSettings
    {

        #region MSWord Section 
        private const string MSWordProtocolSettings_SectionName = "MSWordProtocolSettings";

        /// <summary>
        /// Адрес папки хранилища в которой сохраняются протоколы испытаний
        /// </summary>
        public static string MSWordProtocolsPath
        {
            get
            {
                IniFile f = IniFile.GetAppSettingsFile();
                string path = f.KeyExists("path", MSWordProtocolSettings_SectionName) ? f.Read("path", MSWordProtocolSettings_SectionName) : "";
                if (string.IsNullOrWhiteSpace(path))
                {
                    path = AppContext.BaseDirectory +  @"Протоколы испытаний\MS Word";
                    MSWordProtocolsPath = path;
                }
                return path;
            }
            set
            {
                IniFile f = IniFile.GetAppSettingsFile();
                f.Write("path", value, MSWordProtocolSettings_SectionName);
            }
        }
        #endregion

        #region CommonSection
        private const string CommonProtocolSettings_SectionName = "CommonProtocolSettings";
        public static string ProtocolHeader
        {
            get
            {
                IniFile f = IniFile.GetAppSettingsFile();
                string text = f.KeyExists("header_text", CommonProtocolSettings_SectionName) ? f.Read("header_text", CommonProtocolSettings_SectionName) : "";
                if (string.IsNullOrWhiteSpace(text))
                {
                    text = "Испытание";
                    ProtocolHeader = text;
                }
                return text;
            }
            set
            {
                IniFile f = IniFile.GetAppSettingsFile();
                f.Write("header_text", value, CommonProtocolSettings_SectionName);
            }
        }

        public static bool DoesAddTestIdOnProtocolHeader
        {
            get
            {
                IniFile f = IniFile.GetAppSettingsFile();
                bool v = f.KeyExists("add_test_id_to_header", CommonProtocolSettings_SectionName) ? f.Read("add_test_id_to_header", CommonProtocolSettings_SectionName) == "1" : false;
                return v;
            }
            set
            {
                IniFile f = IniFile.GetAppSettingsFile();
                f.Write("add_test_id_to_header", value ? "1" : "0", CommonProtocolSettings_SectionName);
            }
        }

        public static string CompanyName
        {
            get
            {
                IniFile f = IniFile.GetAppSettingsFile();
                string text = f.KeyExists("company_name", CommonProtocolSettings_SectionName) ? f.Read("company_name", CommonProtocolSettings_SectionName) : "";
                if (string.IsNullOrWhiteSpace(text))
                {
                    text = "ООО \"НПП \"Норма\"";
                    CompanyName = text;
                }
                return text;
            }
            set
            {
                IniFile f = IniFile.GetAppSettingsFile();
                f.Write("company_name", value, CommonProtocolSettings_SectionName);
            }
        }
        #endregion
    }
}
