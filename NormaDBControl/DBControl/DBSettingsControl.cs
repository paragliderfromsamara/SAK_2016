using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NormaMeasure.Utils;

namespace NormaMeasure.DBControl
{
    public static class DBSettingsControl
    {
        const string DBSettingsSectionName = "DBSettings";
        public static string ServerHost
        {
            get
            {
                IniFile f = IniFile.GetAppSettingsFile();
                if (f.KeyExists("host", DBSettingsSectionName))
                {
                    return f.Read("host", DBSettingsSectionName);
                }else
                {
                    return "localhost";
                }
                
            }
            set
            {
                IniFile f = IniFile.GetAppSettingsFile();
                f.Write("host", value, DBSettingsSectionName);
            }
        }

        public static string UserName
        {
            get
            {
                IniFile f = IniFile.GetAppSettingsFile();
                if (f.KeyExists("userName", DBSettingsSectionName))
                {
                    return f.Read("userName", DBSettingsSectionName);
                }
                else
                {
                    return "root";
                }
            }
            set
            {
                IniFile f = IniFile.GetAppSettingsFile();
                f.Write("userName", value, DBSettingsSectionName);
            }
        }

        public static string Password
        {
            get
            {
                IniFile f = IniFile.GetAppSettingsFile();
                if (f.KeyExists("password", DBSettingsSectionName))
                {
                    return f.Read("password", DBSettingsSectionName);
                }
                else
                {
                    return "";
                }
            }
            set
            {
                IniFile f = IniFile.GetAppSettingsFile();
                f.Write("password", value, DBSettingsSectionName);
            }
        }

        public static bool IsEnabled
        {
            get
            {
                IniFile f = IniFile.GetAppSettingsFile();
                return f.Read("Enabled", DBSettingsSectionName) == "1";
            }set
            {
                IniFile f = IniFile.GetAppSettingsFile();
                f.Write("Enabled", (value) ? "1" : "0", DBSettingsSectionName);
            }
        }
    }
}
