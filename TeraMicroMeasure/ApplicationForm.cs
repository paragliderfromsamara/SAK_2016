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
using TeraMicroMeasure.CommandProcessors;
using NormaMeasure.SocketControl.TCPServerControllLib;

namespace TeraMicroMeasure
{
    public partial class ApplicationForm : Form
    {
        object locker = new object();
        private System.Drawing.Color redColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))));
        private System.Drawing.Color greenColor = System.Drawing.Color.FromArgb(((int)(((byte)(21)))), ((int)(((byte)(179)))), ((int)(((byte)(9)))));
        private System.Drawing.Color orangeColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(140)))), ((int)(((byte)(0)))));
        private ClientXmlState[] clientList = new ClientXmlState[] { };
        //NormaServer server;
        //NormaTCPClient client;
        ClientTCPControl clientTCPControl;
        ServerTCPControl serverTCPControl;
        ClientXmlState currentClientState;
        ServerXmlState currentServerState;
        Dictionary<int, MeasureForm> measureFormsList;
        int recCounter = 0;
        public ApplicationForm()
        {
            AppTypeSelector appSel = new AppTypeSelector();
            measureFormsList = new Dictionary<int, MeasureForm>();
            //appSel.ShowDialog();
            if (appSel.ShowDialog() == DialogResult.OK)
            {
                InitializeComponent();
                InitCulture();
                initStatusBar();
                initTopBar();
                InitButtonsBar();
                if (Properties.Settings.Default.IsServerApp) InitAsServerApp();
                else InitAsClientApp();
            }
            else
            {
                Close();
            }

           //testXml();
        }

        private void initTopBar()
        {
            testLinesToolStripMenuItem.Visible = Properties.Settings.Default.IsServerApp;
            if (Properties.Settings.Default.IsServerApp) InitTestLinesTabOnTopBar();
        }

        private void InitButtonsBar()
        {
            switchConnectToServerButton.Visible = !Properties.Settings.Default.IsServerApp;
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
            /*
            ServerXmlState s = buildServerXML();
            ServerXmlState s1;
            ClientXmlState c = new ClientXmlState();
            ClientXmlState c1;
            ClientXmlState c2 = buildClientXML();
            richTextBox1.Text = c.InnerXml;
            c.ClientIP = "Хуйня";
            //richTextBox1.Text += "\n\n" + c.WasChanged;
            richTextBox1.Text += "\n\n" + c.InnerXml;
            c1 = new ClientXmlState(c.InnerXml);
            richTextBox1.Text += "\n\n" + c1.InnerXml;

            richTextBox1.Text += "\n\n" + c1.ClientIP;

            s.AddClient(c);
            s.AddClient(c2);

            s1 = new ServerXmlState(s.InnerXml);
            richTextBox1.Text += "\n\n" + s.InnerXml;
            richTextBox1.Text += "\n\n" + s1.InnerXml;
            richTextBox1.Text += "\n\n" + (s1.Clients[c1.ClientIP] ).InnerXml;
            s1.RemoveClient(c1);
            richTextBox1.Text += "\n\n" + s1.InnerXml;
            */
            //ClientXmlState c2;
            //ClientXmlState c1;
            //c.ClientID = 2;
            //c.ClientPort = 6666;
            //c.ClientIP = "192.168.0.2";
            //System.Threading.Thread.Sleep(1000);
            //richTextBox1.Text = c.InnerXml;
            //c.ClientPort = 9999;
            //richTextBox1.Text += "\n\n" + c.IsValid;

            //

            //c2 = new ClientXmlState(c.InnerXml);
            //richTextBox1.Text += "\n\n" + c2.IsValid;

            // c2.MeasureState.CableId = 1;
            //  richTextBox1.Text += "\n\n" + c2.MeasureState.InnerXml;
            //s.AddClient(c);
            //richTextBox1.Text = s.InnerXml;
            //richTextBox1.Text += "\n" + s.Clients.Keys.First<string>();
            //richTextBox1.Text += "\n" + s.Clients["192.168.0.2"].ClientID.ToString();

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
            try
            {
                TCPSettingsController c = new TCPSettingsController(false);
            }catch(TCPSettingsControllerException e)
            {
                MessageBox.Show(e.Message);
            }

            /*
            retry:
            string currentIp = SettingsControl.GetLocalIP();
            string[] ipList = NormaServerDeprecated.GetAvailableIpAddressList();
            currentServerState = buildServerXML();
            if (!NormaServerDeprecated.IncludesIpOnList(currentIp) && !SettingsControl.GetOfflineMode())
            {
                if (ipList.Length == 1)
                {
                    if (ipList[0] == "127.0.0.1")
                    {
                        if (MessageBox.Show("Отсутсвуют доступные подключения!\n\nПри нажатии \"Отмена\" приложение запустится в автономном режиме.\n\nПри нажатии \"Повтор\" будет произведен повторный поиск", "Отсутствуют подключения", MessageBoxButtons.RetryCancel, MessageBoxIcon.Information) == DialogResult.Retry) goto retry;
                        SettingsControl.SetLocalIpAndPort(ipList[0], "8888");
                    }
                }
                SelectServerIpForm ipForm = new SelectServerIpForm();
                ipForm.ShowDialog();
            }
            initServerControl();
            */
        }

        private void InitTestLinesTabOnTopBar()
        {
            int[] clientIds = SettingsControl.GetClientList();
            foreach(int id in clientIds)
            {
                ToolStripItem i = testLinesToolStripMenuItem.DropDownItems.Add($"Линия {id}");
                i.Name = $"ToolStripItemOfClient_{id}";
                i.Click += ToolStripItemOfClient_Click;
            }
            //transCounterLbl.Text = clientIds.Length.ToString();
        }

        private void ToolStripItemOfClient_Click(object sender, EventArgs e)
        {
            ToolStripItem i = sender as ToolStripItem;
            int clientId = -1;
            int.TryParse(i.Name.Replace("ToolStripItemOfClient_", ""), out clientId);
            if (clientId > 0) ShowClientForm(clientId);
        }

        private void ShowClientForm(int client_id)
        {
            MeasureForm f = getOrCreateMeasureFormByClientId(client_id);
            if (currentServerState.HasClientWithID(client_id)) f.ClientState = currentServerState.GetClientStateByClientID(client_id);
            if (!f.Visible) f.Show();
            if (f.WindowState == FormWindowState.Minimized) f.WindowState = FormWindowState.Normal;
        }

        private MeasureForm getMeasureFormByClientId(int client_id)
        {
            MeasureForm f = null;
            if (measureFormsList.ContainsKey(client_id)) f = measureFormsList[client_id];
            return f;
        }

        private MeasureForm getOrCreateMeasureFormByClientId(int client_id)
        {
            MeasureForm f = getMeasureFormByClientId(client_id);
            if (f == null) f = InitMeasureFormByClientId(client_id);
            return f;
        }

        private MeasureForm InitMeasureFormByClientId(int client_id)
        {
            MeasureForm f = new MeasureForm(client_id);
            f.MdiParent = this;
            f.FormClosing += MeasureForm_Closing;
            //f.SetStates(currentServerState, currentClientState);
            measureFormsList.Add(client_id, f);
            return f;
        }

        private void MeasureForm_Closing(object sender, FormClosingEventArgs e)
        {
            MeasureForm f = sender as MeasureForm;
            measureFormsList.Remove(f.ClientID);
        }

        private void InitAsClientApp()
        {
            currentClientState = buildClientXML();
            SetClientTitle();
            CheckBaseTCPClientParameters();
            setClientButtonStatus(ClientStatus.disconnected);
            InitMeasureForm();
            
            /////////////////////////////////////
            InitClientTCPControl();
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
                
                currentServerState.IPAddress = SettingsControl.GetLocalIP();
                currentServerState.Port = SettingsControl.GetLocalPort();
                currentServerState.RequestPeriod = SettingsControl.GetRequestPeriod();
                serverTCPControl = new ServerTCPControl(currentServerState);
                serverTCPControl.OnClientStateReceived += OnClientStateReceived;
                serverTCPControl.OnServerConnectionException += onServerException;
                serverTCPControl.OnClientListChanged += ServerStateUpdated_Handler;
                serverTCPControl.OnServerStatusChanged += onServerStatusChanged;
                serverTCPControl.Start();
            }
            else
            {
                serverStatusLabel.Text = "Сервер выключен";
            }

        }


        private void OnClientStateReceived(object o, EventArgs a)
        {
            lock (locker)
            {
                ClientXmlState cs = o as ClientXmlState;

                if (InvokeRequired)
                {
                    //ClientDetector cd = new ClientDetector(currentServerState, cs);
                    ServerStateUpdater ssu = new ServerStateUpdater(
                                                                     new ClientDetector(currentServerState, cs), ServerStateUpdated_Handler
                                                                    );
                    
                    currentServerState = ssu.ServerState;
                    BeginInvoke(new EventHandler(OnClientStateReceived), new object[] { cs, a });
                }
                else
                {
                    //richTextBox2.Text += "\n\n" + cs.InnerXml;
                    //richTextBox1.Text += "\n\n" + currentServerState.InnerXml;
                    refreshClientStateOnClientForm(cs);
                    transCounterLbl.Text = $"{recCounter++}";
                }
            }
        }

        private void refreshClientStateOnClientForm(ClientXmlState cs)
        {
            if (measureFormsList.ContainsKey(cs.ClientID))
            {
                MeasureForm f = measureFormsList[cs.ClientID];
                if (!f.HasClientState) f.ClientState = cs;
                else
                {
                    if (f.ClientState.StateId != cs.StateId) f.ClientState = cs;
                }

            }

        }

        private void ServerStateUpdated_Handler(object o, EventArgs e)
        {
           currentServerState = o as ServerXmlState;
           serverTCPControl.SendState(currentServerState);
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

        private void onServerException(object o, EventArgs a)
        {
            Exception ex = o as Exception;
            if (ex.HResult != -2147467259) MessageBox.Show(ex.Message, $"Ошибка соединения: {ex.HResult}");
        }


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
            if (serverTCPControl != null) reinitServer();

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
            //richTextBox2.Text = serverState.InnerXml;
            return serverState;
        }
        #endregion

        #region ClientModePart

        private ClientXmlState buildClientXML()
        {
            ClientXmlState clientXML = ClientXmlState.CreateDefaultByClientId(SettingsControl.GetClientId());

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
            //retry:
            // try
            //  {
                currentClientState.ClientIP = SettingsControl.GetLocalIP();
                currentClientState.ClientPort = SettingsControl.GetLocalPort();
                currentClientState.ServerIP = SettingsControl.GetServerIP();
                currentClientState.ServerPort = SettingsControl.GetServerPort();
                setClientButtonStatus(ClientStatus.tryConnect);
                clientTCPControl = new ClientTCPControl(currentClientState);
                clientTCPControl.OnServerConnected += onServerConnected_Handler;
                clientTCPControl.OnServerStateChanged += processReceivedFromServerState;
                clientTCPControl.OnConnectionException += LostServerConnection;
                clientTCPControl.OnStateWasChangedByServer += OnStateWasChangedByServer_Handler;
                clientTCPControl.Start();
            //}
           // catch (Exception)
           // {
           //     SelectServerIpForm ipForm = new SelectServerIpForm();
           //     ipForm.ShowDialog();
           //     goto retry;
                //MessageBox.Show(ex.Message);
            //}
        }

        private void processReceivedFromServerState(object state_for_a_process, EventArgs a)
        {
            if (InvokeRequired)
            {
                BeginInvoke(new EventHandler(processReceivedFromServerState), new object[] { state_for_a_process, a });
            }
            else
            {
                ServerXmlState sState = state_for_a_process as ServerXmlState;
                ClientIDProcessor clidp = new ClientIDProcessor(new TeraMicroStateProcessor(sState, currentClientState), ClientIdChange_Handler);
                //richTextBox1.Text = sState.InnerXml;
                transCounterLbl.Text = $"{recCounter++}";
                clientTCPControl.SendState(clidp.CurrentClientState);
            }
        }

        private void ClientIdChange_Handler(object o, EventArgs e)
        {
            int id = Convert.ToInt16(o);
            SettingsControl.SetClientId(id);
            SetClientTitle();
        }

        private void onServerConnected_Handler(object sender, EventArgs e)
        {
            if (InvokeRequired)
            {
                BeginInvoke(new EventHandler(onServerConnected_Handler), new object[] { sender, e });
            }
            else
            {
                setClientButtonStatus(ClientStatus.connected);
            }
        }

        private void OnStateWasChangedByServer_Handler(object o, EventArgs a)
        {
            if (InvokeRequired)
            {
                BeginInvoke(new EventHandler(OnStateWasChangedByServer_Handler), new object[] { o, a });
            }
            else
            {
                //ClientXmlState cs = o as ClientXmlState;
                SetClientTitle();
            }
        }

        private void LostServerConnection(object ex, EventArgs a)
        {
            if (InvokeRequired)
            {
                BeginInvoke(new EventHandler(LostServerConnection), new object[] { ex, a});
            }else
            {
                Exception e = ex as Exception;
                DialogResult r = MessageBox.Show($"{e.Message}", $"Ошибка связи с сервером", MessageBoxButtons.RetryCancel, MessageBoxIcon.Warning );
                if (r == DialogResult.Retry) reinitClientTCPControl();
                else if (r == DialogResult.Cancel)
                {
                   if  (MessageBox.Show("Хотите ввести новые данные сервера?", "Вопрос...", MessageBoxButtons.YesNo) == DialogResult.Yes)
                    {
                        SelectServerIpForm ipForm = new SelectServerIpForm();
                        ipForm.ShowDialog();
                        reinitClientTCPControl();
                        return;
                    }
                    clientTCPControl = null;
                    setClientButtonStatus(ClientStatus.disconnected);
                }

            }

        }

        private void MainForm_FormClosing_ForClient()
        {
            stopTCPControl();
        }

        private void reinitClientTCPControl()
        {
            stopTCPControl();
            InitClientTCPControl();
        }
        private void stopTCPControl()
        {
            if (clientTCPControl != null)
            {
                clientTCPControl.OnServerStateChanged = null;
                clientTCPControl.OnConnectionException = null;
                clientTCPControl.Stop();
                clientTCPControl = null;
            }
            setClientButtonStatus(ClientStatus.disconnected);
        }

        private void switchConnectToServerButton_Click(object sender, EventArgs e)
        {
            if (clientTCPControl != null)
            {
                stopTCPControl();
            } else
            {
                InitClientTCPControl();
            }
        }

        private void setClientButtonStatus(ClientStatus s)
        {
            switch(s)
            {
                case ClientStatus.connected:
                    switchConnectToServerButton.BackColor = greenColor;
                    switchConnectToServerButton.Text = char.ConvertFromUtf32(57723);
                    break;
                case ClientStatus.disconnected:
                    switchConnectToServerButton.BackColor = redColor;
                    switchConnectToServerButton.Text = char.ConvertFromUtf32(57722);
                    break;
                case ClientStatus.tryConnect:
                    switchConnectToServerButton.BackColor = orangeColor;
                    switchConnectToServerButton.Text = char.ConvertFromUtf32(57722);
                    break;
            }
        }

        private void SetClientTitle()
        {
            int cId = SettingsControl.GetClientId();
            this.Text = (cId <= 0) ? "Испытательная линия без номера" : $"Испытательная линия с номером {cId}";
        }

        private void InitMeasureForm()
        {
            MeasureForm f = getOrCreateMeasureFormByClientId(SettingsControl.GetClientId());
            f.ClientState = currentClientState;
            f.OnClientStateChanged += RefreshClientStateFromMeasureForm;
            f.Show();
            
            //measureForm = new MeasureForm();
            //measureForm.MdiParent = this;
            //measureForm.Show();
          //  MeasurePanel = new MeasurePanel(currentClientState);
          // MeasurePanel.Parent = centralPanel;
          // MeasurePanel.Location = new Point(0, 150);

            //  MeasurePanel.Dock = DockStyle.Fill;
        }

        private void RefreshClientStateFromMeasureForm(object sender, EventArgs e)
        {
            ClientXmlState s = sender as ClientXmlState;
            RefreshCurrentClientState(s);


        }

        private void RefreshCurrentClientState(ClientXmlState new_state)
        {
            currentClientState = new_state;
            clientTCPControl.SendState(currentClientState);
        }
        #endregion


    }

    enum ClientStatus : int
    {
        connected,
        disconnected,
        tryConnect
    }

}
