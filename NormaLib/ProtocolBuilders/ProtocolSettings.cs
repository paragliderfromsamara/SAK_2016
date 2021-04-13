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
    }
}
