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
        static string SelfTCPInfoSectionName = "LocalTCPSettings";
        static string ServerTCPInfoSectionName = "ServerTCPSettings";

        public static void SetLocalIpAndPort(string ip, string port)
        {
            IniFile f = IniFile.GetAppSettingsFile();
            f.Write("ip", ip, SelfTCPInfoSectionName);
            f.Write("port", port, SelfTCPInfoSectionName);
            SetOfflineMode(ip == "127.0.0.1");
        }

        public static void SetServerIpAndPort(string ip, string port)
        {
            IniFile f = IniFile.GetAppSettingsFile();
            f.Write("ip", ip, ServerTCPInfoSectionName);
            f.Write("port", port, ServerTCPInfoSectionName);
            SetOfflineMode(ip == "127.0.0.1");
        }

        public static string GetLocalIP()
        {
            IniFile f = IniFile.GetAppSettingsFile();
            return f.Read("ip", SelfTCPInfoSectionName);
        }

        public static int GetLocalPort()
        {
            IniFile f = IniFile.GetAppSettingsFile();
            return f.ReadInt("port", SelfTCPInfoSectionName);
        }

        public static string GetServerIP()
        {
            IniFile f = IniFile.GetAppSettingsFile();
            return f.Read("ip", ServerTCPInfoSectionName);
        }

        public static int GetServerPort()
        {
            IniFile f = IniFile.GetAppSettingsFile();
            return f.ReadInt("port", ServerTCPInfoSectionName);
        }

        public static void SetOfflineMode(bool isOffline)
        {
            IniFile f = IniFile.GetAppSettingsFile();
            f.Write("offline", ((isOffline) ? "1" : "0"), SelfTCPInfoSectionName);
        }
        public static bool GetOfflineMode()
        {
            IniFile f = IniFile.GetAppSettingsFile();
            return (f.ReadInt("offline", SelfTCPInfoSectionName) == 1);
        }

    }
}
