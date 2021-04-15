using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NormaLib.Utils;
using System.Net;
using System.Net.Sockets;
using System.Windows.Forms;

namespace NormaLib.SocketControl.TCPControlLib
{
    public class TCPSettingsControllerException : FormatException
    {
        public TCPSettingsControllerException(string message) : base(message)
        {

        }
    }
    public class TCPSettingsController
    {
        const string ServerTCPInfoSectionName = "ServerTCPSettings";
        const string SelfTCPInfoSectionName = "LocalTCPSettings";
        
        public static EventHandler OnTCPSettingsChanged;

        public string localIPOnSettingsFile
        {
            get
            {
                string v;
                IniFile f = IniFile.GetAppSettingsFile();
                v = f.Read("ip", SelfTCPInfoSectionName);
                if (String.IsNullOrWhiteSpace(v)) localIPOnSettingsFile = v = "127.0.0.0";
                return v;
            }
            set
            {
                IniFile f = IniFile.GetAppSettingsFile();
                f.Write("ip", value, SelfTCPInfoSectionName);
            }
        }
        public int localPortOnSettingsFile
        {
            get
            {
                int p;
                IniFile f = IniFile.GetAppSettingsFile();
                p = f.ReadInt("port", SelfTCPInfoSectionName);
                if (p < 1000) localPortOnSettingsFile = p = 3001;
                return p;
            }
            set
            {
                IniFile f = IniFile.GetAppSettingsFile();
                f.Write("port", value.ToString(), SelfTCPInfoSectionName);
            }
        }

        public string serverIPOnSettingsFile
        {
            get
            {
                string v;
                IniFile f = IniFile.GetAppSettingsFile();
                v = f.Read("ip", ServerTCPInfoSectionName);
                if (String.IsNullOrWhiteSpace(v)) serverIPOnSettingsFile = v = "127.0.0.0";
                return v;
            }
            set
            {
                IniFile f = IniFile.GetAppSettingsFile();
                f.Write("ip", value, ServerTCPInfoSectionName);
            }
        }

        public int serverPortOnSettingsFile
        {
            get
            {
                int p;
                IniFile f = IniFile.GetAppSettingsFile();
                p = f.ReadInt("port", ServerTCPInfoSectionName);
                if (p < 1000) serverPortOnSettingsFile = p = 3000;
                return p;
            }
            set
            {
                IniFile f = IniFile.GetAppSettingsFile();
                f.Write("port", value.ToString(), ServerTCPInfoSectionName);
            }
        }

        public IPAddress localIPAddress;
        public IPAddress serverIPAddress;

        public bool IsValid = true;
        public bool IsServerSettings;

        public TCPSettingsController(bool is_it_server, bool parse_addresses = true)
        {
            IsServerSettings = is_it_server;
            if (parse_addresses) InitIPAddressesFromSettingsData();
        }

        public TCPSettingsController(bool v) 
        {
            this.IsServerSettings = v;
            InitIPAddressesFromSettingsData();
        }

        private void InitIPAddressesFromSettingsData()
        {
            string local_ip = localIPOnSettingsFile;
            string remote_ip = serverIPOnSettingsFile;
            InitIPAddresses(local_ip, remote_ip);
        }

        public void InitIPAddresses(string local_ip, string remote_ip)
        {
            IPAddress l = IPAddress.Parse("127.0.0.1");
            IPAddress r = IPAddress.Parse("127.0.0.1");
            string msg = string.Empty;
            if (!IPAddress.TryParse(local_ip, out l)) msg += "Неверный формат локального IP адреса.\n";
            if (!IsServerSettings)
            {
               if (!IPAddress.TryParse(remote_ip, out r)) msg += "Неверный формат IP адреса сервера.\n";
            }
            if (string.IsNullOrWhiteSpace(msg))
            {
                localIPAddress = l;
                serverIPAddress = r;
            }
            else
            {
                throw new TCPSettingsControllerException(msg+$"{IsServerSettings}");
            }
            //localEndPoint = new IPEndPoint(IPAddress.Parse(local_ip), local_port);
            //if (!IsServerSettings) serverEndPoint = new IPEndPoint(IPAddress.Parse(remote_ip), remote_port);
        }
    }
}
