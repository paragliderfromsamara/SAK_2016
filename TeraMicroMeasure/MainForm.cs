using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using NormaMeasure.SocketControl;
using System.Globalization;
using System.Threading;
using NormaMeasure.Utils;
using System.Xml;
using TeraMicroMeasure.XmlObjects;


namespace TeraMicroMeasure
{
    public partial class MainForm : Form
    {
        private ClientXmlState[] clientList = new ClientXmlState[] { };
        //NormaServer server;
        //NormaTCPClient client;
        ClientTCPControl clientTCPControl;
        ServerTCPControl serverTCPControl;
        int recCounter = 0;
        public MainForm()
        {
            AppTypeSelector appSel = new AppTypeSelector();
            //appSel.ShowDialog();
            if (appSel.ShowDialog() == DialogResult.OK)
            {
                InitializeComponent();
                InitCulture();
                initStatusBar();
                if (Properties.Settings.Default.IsServerApp) InitAsServerApp();
                else InitAsClientApp();
            }
            else
            {
                Close();
            }

            //testXml();
        }

        private void MainForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (Properties.Settings.Default.IsServerApp) MainForm_FormClosing_ForServer();
            else MainForm_FormClosing_ForClient();

        }

        private void InitCulture()
        {
            Thread.CurrentThread.CurrentCulture = new CultureInfo("my");
            Thread.CurrentThread.CurrentCulture.NumberFormat.NumberDecimalSeparator = ".";
        }


        private void testXml()
        {
            ServerXmlState s = buildServerXML();
            ClientXmlState c = new ClientXmlState();
            ClientXmlState c2;
            ClientXmlState c1;
            c.ClientID = 2;
            c.ClientPort = 6666;
            c.ClientIP = "192.168.0.2";

            c2 = new ClientXmlState(c.InnerXml);
            s.AddClient(c);
            richTextBox1.Text = s.InnerXml;
            richTextBox1.Text += "\n" + s.Clients.Keys.First<string>();
            richTextBox1.Text += "\n" + s.Clients["192.168.0.2"].ClientID.ToString();

           // c1 = new ClientXmlState(c.InnerXml.Clone().ToString());
           // c1.ClientID = 376;
           // c1.ClientPort = 6666;
           // c1.ClientIP = "192.168.0.2";

           // richTextBox1.Text += "\n\n";
           // s.ReplaceClient(c1);
           // richTextBox1.Text += "\n\n" + s.InnerXml;
           // richTextBox1.Text += "\n\n" + c1.InnerXml;
           // richTextBox2.Text = c2.InnerXml;
            //richTextBox1.Text += "\n" + s.Clients["192.168.1.0"].ClientID.ToString();
        }

        private void InitAsServerApp()
        {
            retry:
            string currentIp = SettingsControl.GetLocalIP();
            string[] ipList = NormaServer.GetAvailableIpAddressList();
            this.Text = "Сервер";
            if (!NormaServer.IncludesIpOnList(currentIp) && !SettingsControl.GetOfflineMode())
            {
                if (ipList.Length == 1)
                {
                    if(ipList[0] == "127.0.0.1")
                    {
                        if (MessageBox.Show("Отсутсвуют доступные подключения!\n\nПри нажатии \"Отмена\" приложение запустится в автономном режиме.\n\nПри нажатии \"Повтор\" будет произведен повторный поиск", "Отсутствуют подключения", MessageBoxButtons.RetryCancel, MessageBoxIcon.Information) == DialogResult.Retry) goto retry;
                        SettingsControl.SetLocalIpAndPort(ipList[0], "8888");
                    }
                }
                SelectServerIpForm ipForm = new SelectServerIpForm();
                ipForm.ShowDialog();
            }
            initServerControl();
        }
        private void InitAsClientApp()
        {
            this.Text = "Клиент";
            CheckBaseTCPClientParameters();
            InitClientTCPControl();
        }


        private void processReceivedState(object state, EventArgs a)
        {
            //cl.MessageToSend = "Hello Fucker!!!";
            if (InvokeRequired)
            {
                BeginInvoke(new EventHandler(processReceivedState), new object[] { state, a });
            }
            else
            {
                ServerXmlState sState = state as ServerXmlState;
                richTextBox1.Text = sState.InnerXml;
                transCounterLbl.Text = $"{recCounter++}";
                //MessageBox.Show(message);//
            }

        }

        #region ServerModePart
        private void reinitServer()
        {
            stopServer();
            initServerControl();
        }

        private void initServerControl()
        {
            if (!SettingsControl.GetOfflineMode())
            {
                serverTCPControl = new ServerTCPControl(buildServerXML());
                serverTCPControl.OnClientStateReceived += OnClientStateReceived;
                serverTCPControl.OnServerConnectionException += onServerException;
                serverTCPControl.OnClientListChanged += onClientListChanged;
                serverTCPControl.OnServerStatusChanged += onServerStatusChanged;
                //server = new NormaServer(SettingsControl.GetLocalIP(), SettingsControl.GetLocalPort());
                //server.ProcessOnServerConnectionException += onServerException;
                //server.OnClientConnected += OnClientConnected;
                serverTCPControl.Start();
                
            }
            else
            {
                serverStatusLabel.Text = "Сервер выключен";
            }

        }
        
        private void processClientStateOnServerSide(ClientXmlState cs)
        {
            if (cs.ClientID == 0)
            {

            }
        }

        private void OnClientStateReceived(object o, EventArgs a)
        {
            //cl.MessageToSend = "Hello Fucker!!!";
            ClientXmlState cs = o as ClientXmlState;
            if (InvokeRequired)
            {
                cs = o as ClientXmlState;
                if (cs.ClientID == 0) cs.ClientID = 1;
                BeginInvoke(new EventHandler(OnClientStateReceived), new object[] { cs, a});
            }
            else
            {
                richTextBox1.Text = cs.InnerXml;
                transCounterLbl.Text = $"{recCounter++}";
               //MessageBox.Show(message);//
            }
       }
        private void onClientListChanged(object o, EventArgs e)
        {
            if (InvokeRequired)
            {
                BeginInvoke(new EventHandler(onClientListChanged), new object[] { o, e });
            }
            else
            {
                clientList = o as ClientXmlState[];
                refreshClientCounterStatusText();
            }
        }

        private void onServerStatusChanged(object o, EventArgs a)
        {
            if (InvokeRequired)
            {
                BeginInvoke(new EventHandler(onServerStatusChanged), new object[] { o, a });
            }
            else
            {
                serverStatusLabel.Text = o as string;
            }
        }
        /*
        private void OnClientConnected(NormaTCPClient client)
        {
            //MessageBox.Show(client.IpAddress);
            if (InvokeRequired)
            {
                BeginInvoke(new NormaTCPClientDelegate(OnClientConnected), new object[] { client });
                return;
            }
            else
            {
                client.OnMessageReceived += OnClientMeassageReceived;
                client.ClientReceiveMessageException += OnClientDisconnected;
                clientList.Add(client.RemoteIP, client);
                refreshClientCounterStatusText();
                // init_messager(client);
                //MessageBox.Show(client.IpAddress);
            }
        }

        private void OnClientDisconnected(string addr, Exception ex)
        {
            if (InvokeRequired)
            {
                BeginInvoke(new NormaTCPClientExceptionDelegate(OnClientDisconnected), new object[] { addr, ex });
            }
            else
            {
                clientList.Remove(addr);
                refreshClientCounterStatusText();
            }
        }
        */
        private void onServerException(object o, EventArgs a)
        {
            Exception ex = o as Exception;
            if (ex.HResult != -2147467259) MessageBox.Show(ex.Message, $"Ошибка соединения: {ex.HResult}");
        }
        /*
        private void OnClientMeassageReceived(string message, NormaTCPClient cl)
        {
            cl.MessageToSend = "Hello Fucker!!!";
            if (InvokeRequired)
            {
                BeginInvoke(new NormaTCPMessageDelegate(OnClientMeassageReceived), new object[] { message, cl });
            }
            else
            {
                MessageBox.Show(message, "От клиента");//
            }
        }
        */

        private void initStatusBar()
        {
            bool isServer = Properties.Settings.Default.IsServerApp;

            clientCounterStatus.Visible = isServer;
            refreshClientCounterStatusText();

            serverStatusLabel.Visible = isServer;
            serverStatusLabel.Text = "Сервер выключен";
        }

        private void refreshClientCounterStatusText()
        {
            clientCounterStatus.Text = $"Клиентов подключено: {clientList.Length}";
        }
        private void serverStatusLabel_Click(object sender, EventArgs e)
        {
            SelectServerIpForm ipForm = new SelectServerIpForm();
            ipForm.FormClosed += IpForm_FormClosed;
            ipForm.ShowDialog();
        }

        private void IpForm_FormClosed(object sender, FormClosedEventArgs e)
        {
            if (serverTCPControl != null)reinitServer();
            
        }

        private void MainForm_FormClosing_ForServer()
        {

            stopServer();
        }

        private void stopServer()
        {
            if (serverTCPControl != null) serverTCPControl.Stop();
        }

        private ServerXmlState buildServerXML()
        {
            ServerXmlState serverState = new ServerXmlState();
            serverState.IPAddress = SettingsControl.GetLocalIP();
            serverState.Port = SettingsControl.GetLocalPort();
            serverState.RequestPeriod = SettingsControl.GetRequestPeriod();
            return serverState;
        }
        #endregion

        #region ClientModePart

        private ClientXmlState buildClientXML()
        {
            ClientXmlState clientXML = new ClientXmlState();
            clientXML.ClientID = SettingsControl.GetClientId();
            clientXML.ClientIP = SettingsControl.GetLocalIP();
            clientXML.ClientPort = SettingsControl.GetLocalPort();
            clientXML.ServerIP = SettingsControl.GetServerIP();
            clientXML.ServerPort = SettingsControl.GetServerPort();
            return clientXML;
        }

        private void CheckBaseTCPClientParameters()
        {
            string localIp = SettingsControl.GetLocalIP();
            int localPort = SettingsControl.GetLocalPort();
            string serverIp = SettingsControl.GetServerIP();
            int serverPort = SettingsControl.GetServerPort();
            if (string.IsNullOrWhiteSpace(localIp) || string.IsNullOrWhiteSpace(serverIp) || serverIp == "127.0.0.1" || localIp == "127.0.0.1")
            {
                SelectServerIpForm ipForm = new SelectServerIpForm();
                ipForm.ShowDialog();
            }
        }

        private void InitClientTCPControl()
        {
            retry:
            try
            {
                clientTCPControl = new ClientTCPControl(buildClientXML());
                clientTCPControl.OnServerStateChanged += processReceivedState;
                clientTCPControl.OnConnectionException += LostServerConnection;
                clientTCPControl.Start();
            }
            catch (Exception)
            {
                SelectServerIpForm ipForm = new SelectServerIpForm();
                ipForm.ShowDialog();
                goto retry;
                //MessageBox.Show(ex.Message);
            }
        }


        private void LostServerConnection(object ex, EventArgs a)
        {
            Exception e = ex as Exception;
            MessageBox.Show(e.Message, $"Потеря связи с сервером {e.HResult} {e.GetType().Name}");
        }

        private void MainForm_FormClosing_ForClient()
        {
            stopTCPControl();
        }

        private void stopTCPControl()
        {
            if (clientTCPControl != null)
            {
                clientTCPControl.OnServerStateChanged = null;
                clientTCPControl.OnConnectionException = null;
                clientTCPControl.Stop();
            }
        }
        #endregion

    }


}
