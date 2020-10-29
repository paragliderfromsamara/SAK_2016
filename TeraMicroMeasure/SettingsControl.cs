using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NormaMeasure.Utils;
namespace TeraMicroMeasure
{
    static class SettingsControl
    {
        static string ServerInfoSectionName = "ServerSettings";

        public static void SetServerIpAndPort(string ip, string port)
        {
            IniFile f = IniFile.GetAppSettingsFile();
            f.Write("ip", ip, ServerInfoSectionName);
            f.Write("port", port, ServerInfoSectionName);
            SetOfflineMode(ip == "127.0.0.1");
        }

        public static string GetServerIp()
        {
            IniFile f = IniFile.GetAppSettingsFile();
            return f.Read("ip", ServerInfoSectionName);
        }

        public static int GetServerPort()
        {
            IniFile f = IniFile.GetAppSettingsFile();
            return f.ReadInt("port", ServerInfoSectionName);
        }

        public static void SetOfflineMode(bool isOffline)
        {
            IniFile f = IniFile.GetAppSettingsFile();
            f.Write("offline", ((isOffline) ? "1" : "0"), ServerInfoSectionName);
        }
        public static bool GetOfflineMode()
        {
            IniFile f = IniFile.GetAppSettingsFile();
            return (f.ReadInt("offline", ServerInfoSectionName) == 1);
        }

    }
}
