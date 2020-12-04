using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NormaMeasure.Utils;
using System.Diagnostics;

namespace TeraMicroMeasure
{
    static class SettingsControl
    {
        const string SelfTCPInfoSectionName = "LocalTCPSettings";
        const string ServerTCPInfoSectionName = "ServerTCPSettings";
        const string ClientSettingsSectionName = "ClientSettings";

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

        public static int GetClientId()
        {
            IniFile f = IniFile.GetAppSettingsFile();
            int v = f.ReadInt("client_id", ClientSettingsSectionName);
            if (v == 0) v = -1;
            return v;
        }

        public static void SetClientId(int id)
        {
            IniFile f = IniFile.GetAppSettingsFile();
            f.Write("client_id", id.ToString(), ClientSettingsSectionName);
        }

        public static void SetRequestPeriod(int time)
        {
            IniFile f = IniFile.GetAppSettingsFile();
            f.Write("request_period", time.ToString(), ClientSettingsSectionName);
        }

        public static int GetRequestPeriod()
        {
            IniFile f = IniFile.GetAppSettingsFile();
            int requestPeriod = f.ReadInt("request_period", ServerTCPInfoSectionName);
            if (requestPeriod == 0)
            {
                requestPeriod = 1000;
                f.Write("request_period", requestPeriod.ToString(), ClientSettingsSectionName);
            }
            return requestPeriod;
        }
        
        public static int GetClientIdByIp(string ip)
        {
            IniFile f = IniFile.GetAppSettingsFile();
            int clId=0;
            while(f.KeyExists("ip", $"Client_{++clId}"))
            {
                if (f.Read("ip", $"Client_{clId}") == ip) break;
            }
            return clId;
        }

        public static void SetClientIP(int client_id, string ip)
        {
            IniFile f = IniFile.GetAppSettingsFile();
            f.Write("ip", ip, $"Client_{client_id}");
        }

        public static string GetClientIP(int client_id)
        {
            IniFile f = IniFile.GetAppSettingsFile();
            return f.Read("ip", $"Client_{client_id}");
        }

        public static int[] GetClientList()
        {
            IniFile f = IniFile.GetAppSettingsFile();
            List<int> cl = new List<int>();
            for(int i = 1; i<=100; i++)
            {
                if (f.KeyExists("ip", $"Client_{i}")) cl.Add(i);
            }
            return cl.ToArray();
        }

        public static int GetClientID(int cId, string cIP)
        {
            IniFile f = IniFile.GetAppSettingsFile();
            int idByIP = -1;
            int idNew = -1;
            for (int i = 1; i <= 25; i++)
            {
                if (idNew > 0 && idByIP > 0) break;
                if (f.KeyExists("ip", $"Client_{i}") && idByIP < 0)
                {
                    if (cIP == f.Read("ip", $"Client_{i}")) idByIP = i;
                }
                else if (idNew < 0) idNew = i;
            }
            if (idByIP > 0) return idByIP;
            if (cId < 0) 
            {
                SetClientIP(idNew, cIP);
                return idNew;
            }else
            {
                SetClientIP(cId, cIP);
                return cId;
            }
            
        }

    }
}
