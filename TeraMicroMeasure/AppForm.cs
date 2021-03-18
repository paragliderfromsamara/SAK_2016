using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using NormaLib.SocketControl.TCPControlLib;
using NormaLib.UI;
using NormaLib.DBControl;
using NormaLib.DBControl.DBNormaMeasure;
using NormaLib.Devices;
using NormaLib.Devices.XmlObjects;
using TeraMicroMeasure.XmlObjects;
using TeraMicroMeasure.CommandProcessors;
using TeraMicroMeasure.Forms;
using System.Threading;
using NormaLib.SocketControl;
using TeraMicroMeasure.Properties;

namespace TeraMicroMeasure
{
 
    public partial class AppForm : UIMainForm
    {
        #region constants
        const string DefaultDbPassword = "kFr16YtWE";
        #endregion
        #region static fields
        public static ServerCommandDispatcher serverCommandDispatcher;
        public static ClientCommandDispatcher clientCommandDispatcher;
        public static DevicesDispatcher DeviceDispatcher;
        public static Dictionary<int, MeasureForm> measureFormsList = new Dictionary<int, MeasureForm>();
        public static Dictionary<string, DeviceXMLState> xmlDevices = new Dictionary<string, DeviceXMLState>();
        #endregion

        #region private fields 
        List<string> WillDisconnectDevice = new List<string>();
        #endregion

        static bool IsServerApp => Properties.Settings.Default.IsServerApp;
        static bool IsFirstRun => Properties.Settings.Default.FirstRun;
        public AppForm()
        {
            if (IsFirstRun)
            {
                AppTypeSelector appSel = new AppTypeSelector();
                if (appSel.ShowDialog() == DialogResult.OK)
                {
                    Properties.Settings.Default.FirstRun = false;
                }
            }
            if (!IsFirstRun)
            {
                if (IsServerApp) InitAsServerApp();
                else InitAsClientApp();
            }
            else
            {
                Close();
            }
        }

        private void ShowTCPSettingsForm()
        {
            TCPSettingsForm f = new TCPSettingsForm(new TCPSettingsController(IsServerApp, false));
            if (f.ShowDialog(this) == DialogResult.OK) ReinitTCP();
        }

        private void ReinitTCP()
        {

            if (IsServerApp)
            {
                SetMeasureFormsOffline();
                reinitServer();
            }
            else
            {
                connectionTimesNow = clientTryConnectionTimes;
                refreshConnectionToServer();
            }
        }

        private void SetMeasureFormsOffline()
        {
            foreach (var f in measureFormsList.Values)
            {
                f.SetConnectionStatus(false);
            }
        }

        #region Инициализация дизайна
        protected override void InitializeDesign()
        {
            base.InitializeDesign();
            InitializeComponent();
            ReorederMenuItems();
            FormClosing += MainForm_FormClosing;
            connectToServerButton.Click += switchConnectToServerButton_Click;
        }

        private void ReorederMenuItems()
        {
            this.panelMenu.Controls.Clear();
            this.panelMenu.Controls.Add(this.btnSettings);
            this.panelMenu.Controls.Add(this.btnDataBase);
            this.panelMenu.Controls.Add(this.btnMeasure);
            this.panelMenu.Controls.Add(this.panelHeader);
            this.panelMenu.Controls.Add(this.connectButPanel);
            connectButPanel.Visible = !IsServerApp;
        }

        protected override Form GetSettingsForm()
        {
            return new SettingsForm();
        }

        protected override Form GetDataBaseForm()
        {
            return new DataBaseTablesControlForm();
        }

        private MeasureForm getMeasureFormByClientId(int client_id)
        {
            MeasureForm f = null;
            if (measureFormsList.ContainsKey(client_id)) f = measureFormsList[client_id];
            return f;
        }

        private void MainForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            StopDeviceFinder();
            if (IsServerApp) MainForm_FormClosing_ForServer();
            else MainForm_FormClosing_ForClient();
        }
        #endregion

        #region ClientSideApp
        private void InitAsClientApp()
        {
            //throw new NotImplementedException();
            CheckClientDBSettings();
            connectionTimesNow = clientTryConnectionTimes;
            SetClientTitle();
            setClientButtonStatus(ClientStatus.disconnected);
            ConnectToServer();
            //InitMeasureForm();
        }

        private void CheckClientDBSettings()
        {
            string client_id = $"NM_Client{SettingsControl.GetClientId()}";
            string password = DefaultDbPassword;
            if (DBSettingsControl.ServerHost != SettingsControl.GetServerIP()) DBSettingsControl.ServerHost = SettingsControl.GetServerIP();
            if (DBSettingsControl.UserName != client_id) DBSettingsControl.UserName = client_id;
            if (DBSettingsControl.Password != password) DBSettingsControl.Password = password;
        }

        private void SetClientTitle()
        {
            int cId = SettingsControl.GetClientId();
            clientTitle.Text = (cId <= 0) ? "Клиент N" : $"Клиент {cId}";
        }

        private ClientXmlState buildClientXML()
        {
            ClientXmlState clientXML = ClientXmlState.CreateDefaultByClientId(SettingsControl.GetClientId());
            return clientXML;
        }


        private void ConnectToServer()
        {
            try
            {
                ClientXmlState currentClientState = buildClientXML();
                TCPSettingsController tsc = new TCPSettingsController(false);
                currentClientState.ClientIP = tsc.localIPOnSettingsFile;
                currentClientState.ClientPort = tsc.localPortOnSettingsFile;
                currentClientState.ServerIP = tsc.serverIPOnSettingsFile;
                currentClientState.ServerPort = tsc.serverPortOnSettingsFile;
                clientCommandDispatcher = new ClientCommandDispatcher(
                                                                        new TCPClientConnectionControl(
                                                                                                            new NormaTCPClient(tsc),
                                                                                                            ClientConnectionStatus_Handler
                                                                                                       ),
                                                                        currentClientState,
                                                                        OnClientIDChanged_Handler,
                                                                        OnMeasureStatusChanged_Handler,
                                                                        OnServerStateReceived_Handler

                                                                      );
            }
            catch (TCPSettingsControllerException)
            {
                ShowTCPSettingsForm();
            }
        }

        private void ClientConnectionStatus_Handler(object sender, EventArgs e)
        {
            if (InvokeRequired)
            {
                BeginInvoke(new EventHandler(ClientConnectionStatus_Handler), new object[] { sender, e });
            }
            else
            {
                NormaTCPClient client = sender as NormaTCPClient;
                switch (client.Status)
                {
                    case TCP_CLIENT_STATUS.CONNECTED:
                        setClientButtonStatus(ClientStatus.connected);
                        connectionTimesNow = clientTryConnectionTimes;
                        break;
                    case TCP_CLIENT_STATUS.DISCONNECTED:
                        //failureCounter.Text = $"Отвалов от сервера {failureCount++}";
                        setClientButtonStatus(ClientStatus.disconnected);
                        break;
                    case TCP_CLIENT_STATUS.TRY_CONNECT:
                        setClientButtonStatus(ClientStatus.tryConnect);
                        break;
                    case TCP_CLIENT_STATUS.ABORTED:
                        tryToConnect();
                        break;
                    default:
                        setClientButtonStatus(ClientStatus.disconnected);
                        break;
                }
            }
        }

        private void switchConnectToServerButton_Click(object sender, EventArgs e)
        {
            if (clientCommandDispatcher != null)
            {
                disconnectFromServer();
            }
            else
            {
                connectionTimesNow = clientTryConnectionTimes;
                ConnectToServer();
            }
        }

        private void tryToConnect()
        {
            if (connectionTimesNow-- > 0)
            {
                Thread t = new Thread(new ThreadStart(tryConnectThreadFunc));
                t.Start();
            }
            else
            {
                setClientButtonStatus(ClientStatus.disconnected);
                disconnectFromServer();
            }
        }

        private void tryConnectThreadFunc()
        {
            Thread.Sleep(500);
            refreshConnectionToServer();
        }

        private void refreshConnectionToServer()
        {
            disconnectFromServer();
            ConnectToServer();
        }


        private void setClientButtonStatus(ClientStatus s)
        {
            switch (s)
            {
                case ClientStatus.connected:
                    connectToServerButton.BackColor = NormaUIColors.GreenColor;
                    connectToServerButton.Text = "  Подключен";//char.ConvertFromUtf32(57723);
                    connectToServerButton.Image = Resources.connected_white;
                    titleLabel_1.Text = $"Сервер IP: {SettingsControl.GetServerIP()} Порт: {SettingsControl.GetServerPort()}";
                    titleLabel_2.Text = "";
                    break;
                case ClientStatus.disconnected:
                    connectToServerButton.BackColor = NormaUIColors.RedColor;
                    connectToServerButton.Text = "  Отключен";
                    connectToServerButton.Image = Resources.disconnect_white;
                    titleLabel_1.Text = "Нет подключения к серверу";
                    titleLabel_2.Text = "";
                    break;
                case ClientStatus.tryConnect:
                    connectToServerButton.BackColor = NormaUIColors.OrangeColor;
                    connectToServerButton.Text = "  Подключение...";
                    connectToServerButton.Image = Resources.disconnect_white;
                    titleLabel_1.Text = $"Подключение...";
                    titleLabel_2.Text = $"Осталось попыток: {connectionTimesNow + 1}";
                    break;
            }
        }

        private void OnServerStateReceived_Handler(object sender, EventArgs e)
        {
            ServerXmlState sState = sender as ServerXmlState;
            CheckDeviceListChanges(sState.Devices);
        }

        private void MainForm_FormClosing_ForClient()
        {
            disconnectFromServer();
        }

        const int clientTryConnectionTimes = 5;
        int connectionTimesNow = clientTryConnectionTimes;
        private void disconnectFromServer()
        {
            if (clientCommandDispatcher != null)
            {
                clientCommandDispatcher.Dispose();
                clientCommandDispatcher = null;
            }
            CheckDeviceListChanges(new Dictionary<string, DeviceXMLState>());
            ResetRemoteXmlDevices();
            connectionTimesNow = -1; //Чтоб при выключении не совершал попыток поиска связи
        }

        private void ResetRemoteXmlDevices()
        {
            CheckDeviceListChanges(new Dictionary<string, DeviceXMLState>());
        }
        #endregion

        #region ServerSideApp
        private void InitAsServerApp()
        {
            clientTitle.Text = "Сервер";
            refreshClientCounterStatusText(0);
            initServerControl();
            InitDataBaseOnServer();
            SettingsControl.SetClientId(0);
            //throw new NotImplementedException();
        }

        private void InitDataBaseOnServer()
        {
            ProcessBox pc = null;
            MySQLDBControl dbc = null;
            try
            {
                dbc = new MySQLDBControl();

                if (!dbc.IsDBExists("db_norma_measure"))
                {
                    pc = new ProcessBox("Создание Базы данных");
                    pc.Show();
                    DBNormaMeasureTablesMigration dbnm = new DBNormaMeasureTablesMigration();
                    dbnm.InitDataBase();
                    pc.Close();

                }
            }
            catch(Exception ex)
            {
                if (pc != null) pc.Dispose();
                if (dbc != null) dbc.Dispose();
                MessageBox.Show(ex.Message, "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void reinitServer()
        {
            stopServer();
            initServerControl();
        }


        private void initServerControl()
        {
            if (!SettingsControl.GetOfflineMode())
            {
                try
                {
                    serverCommandDispatcher = new ServerCommandDispatcher(
                                                                       new TCPServerClientsControl(
                                                                                new TCPServer(
                                                                                                new TCPSettingsController(true),
                                                                                                OnServerStatusChanged_Handler
                                                                                                )
                                                                             ),
                                                                        buildServerXML(),
                                                                        OnClientIDChanged_Handler,
                                                                        OnMeasureSettingsChanged_Handler,
                                                                        OnMeasureStart_Handler,
                                                                        OnMeasureStop_Handler,
                                                                        OnClientConnected_Handler,
                                                                        OnClientDisconnected_Handler,
                                                                        OnDeviceTryCapture_Handler,
                                                                        OnDeviceReleased_Handler
                                                                    );
                }
                catch (TCPSettingsControllerException e)
                {
                    //ShowTCPSettingsForm();
                }
            }
            else
            {
                titleLabel_1.Text = "";
                titleLabel_2.Text = "Сервер выключен";
            }

        }

        private void OnDeviceTryCapture_Handler(object sender, EventArgs e)
        {
            ClientXmlState cs = sender as ClientXmlState;
            if (DeviceDispatcher != null)
            {
                DeviceXMLState ds = DeviceDispatcher.CaptureDeviceAndGetDeviceXmlState(cs.MeasureState.CapturedDeviceTypeId, cs.MeasureState.CapturedDeviceSerial, cs.ClientID);
                if (ds != null)
                {
                    ReplaceDeviceXMLStateOnServerCommandDispatcher(ds);
                }
            }
        }
        private void ReplaceDeviceXMLStateOnServerCommandDispatcher(DeviceXMLState xml_device)
        {
            if (serverCommandDispatcher != null)
            {
                serverCommandDispatcher.ReplaceDeviceOnServerState(xml_device);
            }
            if (measureFormsList.ContainsKey(xml_device.ClientId)) measureFormsList[xml_device.ClientId].RefreshCapturedXmlDevice(xml_device);
        }

        private void OnDeviceReleased_Handler(object sender, EventArgs e)
        {
            ClientXmlState cs = sender as ClientXmlState;
            if (DeviceDispatcher != null)
            {
                DeviceXMLState ds = DeviceDispatcher.ReleaseDeviceAndGetDeviceXmlState(cs.ClientID);
                if (ds != null)
                {
                    ReplaceDeviceXMLStateOnServerCommandDispatcher(ds);
                }
            }
        }

        private void OnClientDisconnected_Handler(object sender, EventArgs e)
        {
            if (InvokeRequired)
            {
                BeginInvoke(new EventHandler(OnClientDisconnected_Handler), new object[] { sender, e });
            }
            else
            {
                ClientListChangedEventArgs a = e as ClientListChangedEventArgs;
                SetOfflineStatusOnMeasureForm(a.OnFocusClientState);
                refreshClientCounterStatusText(a.ServerState.Clients.Count);
                //failureCounter.Text = $"Отвалов от сервера {++clientDisconnectedTimes}";
                if (!String.IsNullOrWhiteSpace(a.OnFocusClientState.MeasureState.CapturedDeviceSerial))
                {
                    OnDeviceReleased_Handler(a.OnFocusClientState, new EventArgs());
                }
            }
        }

        private void SetOfflineStatusOnMeasureForm(ClientXmlState s)
        {
            MeasureForm f = getMeasureFormByClientId(s.ClientID);
            if (f != null) f.SetConnectionStatus(false);
        }

        private void OnClientConnected_Handler(object sender, EventArgs e)
        {
            if (InvokeRequired)
            {
                BeginInvoke(new EventHandler(OnClientConnected_Handler), new object[] { sender, e });
            }
            else
            {
                ClientListChangedEventArgs a = e as ClientListChangedEventArgs;
                RefreshTestLinesMenuItems(a.OnFocusClientState);
                RefreshMeasureStateOnMeasureForm(a.OnFocusClientState.ClientID, a.OnFocusClientState.MeasureState);
                refreshClientCounterStatusText(a.ServerState.Clients.Count);
                CheckClientUseOnDataBaseAsync(a.OnFocusClientState.ClientID);
            }
        }

        private void CheckClientUseOnDataBaseAsync(int client_id)
        {
            DBNormaMeasureTablesMigration cbt = new DBNormaMeasureTablesMigration();
            MySQLDBControl c = new MySQLDBControl(cbt.DBName);
            c.CreateIfNotExists($"NM_Client{client_id}", cbt.DBName, DefaultDbPassword).GetAwaiter();
            c.Dispose();
        }

        private void RefreshTestLinesMenuItems(ClientXmlState onFocusClientState)
        {
            /*
            bool f = false;
            foreach (ToolStripDropDownItem tb in testLinesToolStripMenuItem.DropDownItems)
            {
                f = GetLineIDFromToolStripItemName(tb) == onFocusClientState.ClientID;
                if (f) break;
            }
            if (!f) AddTestLineDropDownItem(onFocusClientState.ClientID);
            */
        }

        private void OnMeasureStop_Handler(object sender, EventArgs e)
        {
            if (InvokeRequired)
            {
                BeginInvoke(new EventHandler(OnMeasureStop_Handler), new object[] { sender, e });
            }
            else
            {
                ClientXmlState cs = sender as ClientXmlState;
                DeviceBase d = DeviceDispatcher.GetDeviceByTypeAndSerial(cs.MeasureState.CapturedDeviceTypeId, cs.MeasureState.CapturedDeviceSerial);
                if (d != null)
                {
                    if (d.ClientId == cs.ClientID) d.StopMeasure();
                }
            }
        }

        private void OnMeasureStart_Handler(object sender, EventArgs e)
        {
            if (InvokeRequired)
            {
                BeginInvoke(new EventHandler(OnMeasureStart_Handler), new object[] { sender, e });
            }
            else
            {
                ClientXmlState cs = sender as ClientXmlState;
                DeviceBase d = DeviceDispatcher.GetDeviceByTypeAndSerial(cs.MeasureState.CapturedDeviceTypeId, cs.MeasureState.CapturedDeviceSerial);
                if (d != null)
                {
                    if (d.ClientId == cs.ClientID) d.InitMeasureFromXMLState(cs.MeasureState);
                }
            }
        }

        private void OnMeasureSettingsChanged_Handler(object sender, EventArgs e)
        {
            if (InvokeRequired)
            {
                BeginInvoke(new EventHandler(OnMeasureSettingsChanged_Handler), new object[] { sender, e });
            }
            else
            {
                MeasureSettingsChangedEventArgs a = e as MeasureSettingsChangedEventArgs;
                RefreshMeasureStateOnMeasureForm(a.ClientId, a.MeasureState_New);
            }
        }

        private void RefreshMeasureStateOnMeasureForm(int client_id, MeasureXMLState new_state)
        {
            MeasureForm f = getMeasureFormByClientId(client_id);
            if (f != null) f.RefreshMeasureState(new_state);
        }

        private void OnClientIDChanged_Handler(object sender, EventArgs e)
        {
            if (InvokeRequired)
            {
                BeginInvoke(new EventHandler(OnClientIDChanged_Handler), new object[] { sender, e });
            }
            else
            {
                ClientIDChangedEventArgs a = e as ClientIDChangedEventArgs;
                MeasureForm f = getMeasureFormByClientId(a.IdWas);
                CheckClientDBSettings();
                if (f != null) f.ClientID = a.IdNew;
                CheckClientUseOnDataBaseAsync(a.IdNew);
            }
        }



        private void OnServerStateChangedByClient_Handler(object sender, EventArgs e)
        {
            ServerXmlStateEventArgs a = e as ServerXmlStateEventArgs;
            ServerCommandDispatcher d = sender as ServerCommandDispatcher;
            if (InvokeRequired)
            {
                d.RefreshCurrentServerState(a.ServerState);
                BeginInvoke(new EventHandler(OnServerStateChangedByClient_Handler), new object[] { d, a });
            }
            else
            {
                refreshClientCounterStatusText(a.ServerState.Clients.Count);
            }
        }
        /*
        private void initStatusBar()
        {
            bool isServer = Properties.Settings.Default.IsServerApp;

            clientCounterStatus.Visible = isServer;
            refreshClientCounterStatusText(0);
            connectionStatusLabel.Text = (isServer) ? "Сервер выключен" : "Нет подключения к серверу";
        }
        */
        private void refreshClientCounterStatusText(int clients_count)
        {
            titleLabel_1.Text = $"Клиентов подключено: {clients_count}";
        }

/*
        private void serverStatusLabel_Click(object sender, EventArgs e)
        {
            ShowTCPSettingsForm();
        }
        */

        private void MainForm_FormClosing_ForServer()
        {
            stopServer();
        }

        private void stopServer()
        {
            if (serverCommandDispatcher != null)
            {
                serverCommandDispatcher.Dispose();
                serverCommandDispatcher = null;
            }

        }

        private ServerXmlState buildServerXML()
        {
            ServerXmlState serverState = new ServerXmlState();
            serverState.IPAddress = SettingsControl.GetLocalIP();
            serverState.Port = SettingsControl.GetLocalPort();
            serverState.RequestPeriod = SettingsControl.GetRequestPeriod();
            return serverState;
        }


        private void OnServerStatusChanged_Handler(object sender, EventArgs e)
        {
            if (InvokeRequired)
            {
                BeginInvoke(new EventHandler(OnServerStatusChanged_Handler), new object[] { sender, e });
            }
            else
            {
                TCPServer s = sender as TCPServer;
                switch (s.Status)
                {
                    case NORMA_SERVER_STATUS.ACTIVE:
                        titleLabel_2.Text = $"IP:{SettingsControl.GetLocalIP()} Порт: {SettingsControl.GetLocalPort()}";
                        break;
                    case NORMA_SERVER_STATUS.STOPPED:
                        titleLabel_2.Text = "Сервер остановлен";
                        break;
                    case NORMA_SERVER_STATUS.TRY_START:
                        titleLabel_2.Text = "Запуск сервера...";
                        break;
                    default:
                        titleLabel_2.Text = "Статус не обработан";
                        break;
                }
                if (s.Exception != null) MessageBox.Show(s.Exception.Message, s.Exception.GetType().Name);
            }
        }
        #endregion

        #region device_control
        private void OnMeasureStatusChanged_Handler(object sender, EventArgs e)
        {
            throw new NotImplementedException();
        }
        private void StopDeviceFinder()
        {
            if (DeviceDispatcher != null) DeviceDispatcher.Dispose();
        }

        private void CheckDeviceListChanges(Dictionary<string, DeviceXMLState> devices)
        {
            bool listWasChanged = false;
            List<string> disList = new List<string>();
            foreach (var key in devices.Keys)
            {
                if (WillDisconnectDevice.Contains(key)) WillDisconnectDevice.Remove(key);
            }
            if (devices.Values.Count > 0 && xmlDevices.Values.Count == 0)
            {
                foreach (var key in devices.Keys)
                {
                    DeviceXMLState ds = new DeviceXMLState(devices[key].InnerXml);
                    xmlDevices.Add(key, ds);
                    OnDeviceConnectedToServer_Handler(ds);
                }
                listWasChanged = true;
            }
            else if (devices.Values.Count == 0 && xmlDevices.Values.Count > 0)
            {

                foreach (var key in xmlDevices.Keys)
                {
                    if (!WillDisconnectDevice.Contains(key))
                    {
                        disList.Add(key);
                        WillDisconnectDevice.Add(key);
                    }
                    //  OnDeviceDisconnectedFromServer_Handler(d);
                    //  disconnectedDevices.Text = $"Отключено раз:{++deviceDisconnectTimes}";
                }
                /*
                foreach(var d in xmlDevices.Values)
                {

                    OnDeviceDisconnectedFromServer_Handler(d);
                    disconnectedDevices.Text = $"Отключено раз:{++deviceDisconnectTimes}";
                }
                xmlDevices.Clear();
                listWasChanged = true;
                */
            }
            else if (devices.Values.Count > 0 && xmlDevices.Values.Count > 0)
            {
                foreach (var key in xmlDevices.Keys)
                {
                    if (!devices.ContainsKey(key) && !WillDisconnectDevice.Contains(key))
                    {
                        WillDisconnectDevice.Add(key);
                        disList.Add(key);
                        /*OnDeviceDisconnectedFromServer_Handler(xmlDevices[key]);
                        xmlDevices.Remove(key);
                        disconnectedDevices.Text = $"Отключено раз:{++deviceDisconnectTimes}";
                        listWasChanged = true;*/
                    }
                }
                foreach (var key in devices.Keys)
                {
                    if (xmlDevices.ContainsKey(key))
                    {
                        if (xmlDevices[key].StateId != devices[key].StateId)
                        {
                            xmlDevices[key] = devices[key];
                            OnDeviceChangedOnServer_Handler(xmlDevices[key]);
                            listWasChanged = true;
                        }
                    }
                    else
                    {
                        xmlDevices.Add(key, devices[key]);
                        OnDeviceConnectedToServer_Handler(xmlDevices[key]);
                        listWasChanged = true;
                    }
                }
            }
            if (disList.Count > 0)
            {
                DeviceDisconnectionTimer t = new DeviceDisconnectionTimer(DisconnectDeviceOnClientSideTimerTick_Handler, disList);
            }
            if (listWasChanged) OnXmlDevicesListChanged_Handler();
        }

        private void OnDeviceChangedOnServer_Handler(DeviceXMLState xml_device)
        {
            //MessageBox.Show($"Изменен {xml_device.ClientId}");
            foreach (var f in measureFormsList.Values)
            {
                if (xml_device.Serial == f.CapturedDeviceSerial && (int)f.CapturedDeviceType == xml_device.TypeId)
                {
                    f.RefreshCapturedXmlDevice(xml_device);
                }
            }
        }

        private void OnDeviceConnectedToServer_Handler(DeviceXMLState xml_device)
        {
            //MessageBox.Show($"Подключен {xml_device.TypeNameFull}");
        }

        private void DisconnectDeviceOnClientSideTimerTick_Handler(object o, EventArgs e)
        {
            bool f = false;
            DeviceDisconnectionTimer t = o as DeviceDisconnectionTimer;
            foreach (var key in t.DeviceKeys)
            {
                if (xmlDevices.ContainsKey(key) && WillDisconnectDevice.Contains(key))
                {
                    OnDeviceDisconnectedFromServer_Handler(xmlDevices[key]);
                    xmlDevices.Remove(key);
                    //disconnectedDevices.Text = $"Отключено раз:{++deviceDisconnectTimes}";

                    f = true;
                }
            }
            if (f) OnXmlDevicesListChanged_Handler();
            t.Dispose();
        }
        private void OnDeviceDisconnectedFromServer_Handler(DeviceXMLState xml_device)
        {
            //MessageBox.Show($"Отключен {xml_device.TypeNameFull}");
            foreach (var f in measureFormsList.Values)
            {
                if (f.CapturedDeviceSerial == xml_device.Serial && (int)f.CapturedDeviceType == xml_device.TypeId)
                {
                    f.DisconnectDeviceFromServerSide();
                }
            }
        }
        private void OnXmlDevicesListChanged_Handler()
        {
            foreach (var f in measureFormsList.Values)
            {
                f.SetXmlDeviceList(xmlDevices);
            }
        }
        #endregion
    }

    enum ClientStatus : int
    {
        connected,
        disconnected,
        tryConnect
    }

    class DeviceDisconnectionTimer : IDisposable
    {
        public List<string> DeviceKeys = new List<string>();
        Thread thread;
        EventHandler OnTimerEnd;
        public DeviceDisconnectionTimer(EventHandler onTimerTick, List<string> keys)
        {
            DeviceKeys = keys;
            OnTimerEnd += onTimerTick;
            thread = new Thread(new ThreadStart(ThreadFunc));
            thread.Start();
        }

        void ThreadFunc()
        {
            Thread.Sleep(5000);
            OnTimerEnd?.Invoke(this, new EventArgs());
        }

        public void Dispose()
        {
            if (thread != null)
            {
                thread.Abort();
            }
        }
    }
}
