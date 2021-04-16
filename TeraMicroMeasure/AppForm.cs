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
using System.Diagnostics;
using TeraMicroMeasure.Properties;
using NormaLib.SessionControl;
using NormaLib.Utils;
using NormaLib.ProtocolBuilders;

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
        //public static Dictionary<int, MeasureForm> measureFormsList = new Dictionary<int, MeasureForm>();
        public static Dictionary<string, DeviceXMLState> xmlDevices = new Dictionary<string, DeviceXMLState>();
        #endregion

        #region private fields 
        List<string> WillDisconnectDevice = new List<string>();
        #endregion

        LoadAppForm LoadAppForm;
        Type CurrentFormType => currentForm == null ? null : currentForm.GetType();
        MeasureForm OpenedMeasureForm => (CurrentFormType == typeof(MeasureForm)) ? (MeasureForm)currentForm : null;  

        static bool IsServerApp => SettingsControl.IsServerApp;
        static bool IsFirstRun => !IniFile.SettingsFileExists();


        public AppForm()
        {
            if (IsFirstRun)
            {
                AppTypeSelector appSel = new AppTypeSelector();
                appSel.ShowDialog();
            }
            if (!IsFirstRun)
            {
                TCPSettingsController.OnTCPSettingsChanged += (o, s) => {ReinitTCP();};
                Load += (s, a) =>
                {
                    bool flag = true;
                    if (IsServerApp)
                        flag = InitAsServerApp();
                    else flag = InitAsClientApp();
                    if (flag)
                    {
                        InitSessionForm();
                        InitDeviceFinder();
                    }else
                    {
                        MessageBox.Show("Невозможно запустить приложение", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        Close();
                    }
                };
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
            if (OpenedMeasureForm != null) OpenedMeasureForm.SetConnectionStatus(false);
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
            this.panelMenu.Controls.Add(this.sessionPanel);
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

        protected override Form GetMeasureForm()
        {
            MeasureForm f = new MeasureForm(SettingsControl.GetClientId());
            f.OnMeasureStateChanged += RefreshMeasureStateHandler;
            f.Load += (s, a) => { f.SetXmlDeviceList(xmlDevices); };
            return f;
        }

        /// <summary>
        /// Ищем открытую форму измерений
        /// </summary>
        /// <returns></returns>
        private MeasureForm FindOpened_MeasureForm()
        {
            if (currentForm.GetType() == typeof(MeasureForm))
                return (MeasureForm)currentForm;
            else
                return null;
        }

        private bool MeasureIsActive()
        {
            MeasureForm mf = FindOpened_MeasureForm();
            if (mf == null) return false;
            return mf.MeasureState.MeasureStartFlag;
        }

        private void RefreshMeasureStateHandler(object sender, EventArgs e)
        {
            if (IsServerApp)
                RefreshMeasureStateOnServerSide(sender, e);
            else
                RefreshMeasureStateOnClientSide(sender, e);
        }

        //private MeasureForm getMeasureFormByClientId(int client_id)
        //{
        //    MeasureForm f = null;
        //    if (measureFormsList.ContainsKey(client_id)) f = measureFormsList[client_id];
        //    return f;
        //}

        private void MainForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (!MeasureIsActive())
            {
                StopDeviceFinder();
                if (IsServerApp) MainForm_FormClosing_ForServer();
                else MainForm_FormClosing_ForClient();
            }else
            {
                MessageBox.Show("Невозможно перейти в другое меню или закрыть приложение при запущенном измерении!", "Измерение не завершено!", MessageBoxButtons.OK, MessageBoxIcon.Stop);
                e.Cancel = true;
            }

        }
        #endregion

        #region ClientSideApp
        private bool InitAsClientApp()
        {
            LoadAppForm = new LoadAppForm();
            LoadAppForm.Show();
            LoadAppForm.SetTaskLabelsValue("Поиск сервера");
            Thread.Sleep(750);
            ConnectToServer();
            CheckClientDBSettings();
            connectionTimesNow = clientTryConnectionTimes;
            SetClientTitle();
            setClientButtonStatus(ClientStatus.disconnected);
            LoadAppForm.Close();
            LoadAppForm.Dispose();
            return true;
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
            clientTitle.Text = (cId <= 0) ? "Линия N" : $"Линия {cId}";
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
                RefreshSessionForm(client.Status);
            }
        }



        private void switchConnectToServerButton_Click(object sender, EventArgs e)
        {
            if (clientCommandDispatcher != null)
            {
                if (MeasureIsActive()) MessageBox.Show("Прежде чем отключиться от сервера, необходимо остановить измерение!", "Предупреждение", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                else
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

        private void RefreshMeasureStateOnClientSide(object sender, EventArgs e)
        {
            MeasureXMLState s = sender as MeasureXMLState;
            if (clientCommandDispatcher != null) clientCommandDispatcher.RefreshMeasureState(s);
        }

        private void OnServerStateReceived_Handler(object sender, EventArgs e)
        {
            ServerXmlState sState = sender as ServerXmlState;
            CheckDeviceListChanges(sState.Devices);
            ProtocolSettingsXMLState.CheckCurrentStateChanges(sState.ProtocolSettingsState);
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
        private bool InitAsServerApp()
        {
            bool flag = true;
            clientTitle.Text = "Сервер";
            refreshClientCounterStatusText(0);

            LoadAppForm = new LoadAppForm();
            LoadAppForm.Show();
            LoadAppForm.Refresh();
            flag &= InitDataBaseOnServer();
            Thread.Sleep(1500);
            if (flag)
            {
                LoadAppForm.SetTaskLabelsValue("Запуск сервера");
                Thread.Sleep(1500);
                initServerControl();
            }
            LoadAppForm.Close();

            return flag;
            //throw new NotImplementedException();
        }

        private bool InitDataBaseOnServer()
        {
            retry:
            try
            {
                DBNormaMeasureTablesMigration dbnm = new DBNormaMeasureTablesMigration();
                dbnm.OnStepChanged += (o, s) =>
                {
                    LoadAppForm.SetTaskLabelsValue(dbnm.CurrentStep, dbnm.CurrentSubStep);
                    Thread.Sleep(750);
                };
                dbnm.InitDataBase();
                return true;
            }
            catch(Exception ex)
            {
                MessageBox.Show($"{ex.Message}", "Не удалось подключиться к Базе данных", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                DataBaseSettingsForm dbfm = new DataBaseSettingsForm();
                dbfm.StartPosition = FormStartPosition.CenterScreen;
                dbfm.cancelButton.Visible = true;
                DialogResult dr =  dbfm.ShowDialog();
                if (dr == DialogResult.Retry) goto retry;
                else return false;
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
                CheckDeviceListChanges(serverCommandDispatcher.GetServerDevices());
            }
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
            MeasureForm f = OpenedMeasureForm;
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
                //if (a.OnFocusClientState.ClientID == SettingsControl.GetClientId())RefreshMeasureStateOnMeasureForm(a.OnFocusClientState.MeasureState);
                refreshClientCounterStatusText(a.ServerState.Clients.Count);
                CheckClientUseOnDataBaseAsync(a.OnFocusClientState.ClientID);
            }
        }

        private async void CheckClientUseOnDataBaseAsync(int client_id)
        {
            DBNormaMeasureTablesMigration cbt = new DBNormaMeasureTablesMigration();
            MySQLDBControl c = new MySQLDBControl(cbt.DBName);
            await c.CreateIfNotExists($"NM_Client{client_id}", cbt.DBName, DefaultDbPassword);
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
            /*
            if (InvokeRequired)
            {
                BeginInvoke(new EventHandler(OnMeasureSettingsChanged_Handler), new object[] { sender, e });
            }
            else
            {
                MeasureSettingsChangedEventArgs a = e as MeasureSettingsChangedEventArgs;
                if (a.ClientId == SettingsControl.GetClientId() && a.ClientId != SettingsControl.GetClientId()) RefreshMeasureStateOnMeasureForm(a.MeasureState_New);
            }
            */
        }

        private void RefreshMeasureStateOnMeasureForm(MeasureXMLState new_state)
        {
            if (OpenedMeasureForm != null) OpenedMeasureForm.RefreshMeasureState(new_state);
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
                CheckClientDBSettings();
                if (OpenedMeasureForm != null) OpenedMeasureForm.ClientID = a.IdNew;
                if (IsServerApp)CheckClientUseOnDataBaseAsync(a.IdNew);
                SetClientTitle();
                RefreshSessionForm(TCP_CLIENT_STATUS.CONNECTED);
            }
        }

        private void RefreshSessionForm(TCP_CLIENT_STATUS status)
        {
            if (CurrentFormType == typeof(SessionControlForm))
            {
                SessionControlForm f = currentForm as SessionControlForm;
                if (status == TCP_CLIENT_STATUS.CONNECTED)
                    f.FillAllowedUsersAsync();
                else if (status == TCP_CLIENT_STATUS.DISCONNECTED)
                    f.SetDefaultFormState();
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
            serverState.ProtocolSettingsState = ProtocolSettingsXMLState.CurrentState;

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
        private void InitDeviceFinder()
        {
            DeviceDispatcher = new DevicesDispatcher(new DeviceCommandProtocol(), OnDeviceFound_EventHandler);
        }

        private void OnDeviceFound_EventHandler(object sender, EventArgs e)
        {
            if (InvokeRequired)
            {
                BeginInvoke(new EventHandler(OnDeviceFound_EventHandler), new object[] { sender, e });
            }
            else
            {
                DeviceBase d = sender as DeviceBase;
                DeviceXMLState dxl = new DeviceXMLState(d.GetXMLState().InnerXml);
                switch (d.TypeId)
                {
                    case DeviceType.Microohmmeter:
                    case DeviceType.Teraohmmeter:
                        //xmlDevices.Add($"{dxl.TypeId}-{dxl.Serial}", dxl);//AddSimpleDeviceToSettingsFile(d);
                        OnDeviceConnected_Handler(dxl);
                        break;
                }
                if (IsServerApp)
                {
                    AddDeviceToServerCommandDispatcher(d);
                   
                }
                
                //MessageBox.Show($"Подключено {d.GetType().Name} Серийный номер {d.SerialYear}-{d.SerialNumber}");
                //AddOrUpdateDeviceOnToolStrip(d);

                //d.OnDisconnected += OnDeviceDisconnected_EventHandler;
                d.OnXMLStateChanged += DeviceXMLStateChanged_Handler;
                d.OnWorkStatusChanged += WorkStatusChanged_Handler;
                d.OnGetMeasureResult += OnGetMeasureResult_Handler;
                d.OnMeasureCycleFlagChanged += OnMeasureCycleFlagChanged_Handler;
            }

        }

        private void InitSessionForm()
        {
            SessionControlForm scf = new SessionControlForm();
            scf.IsServerApp = IsServerApp;
            scf.Shown += (o, s) => { panelMenu.Visible = false; };
            scf.OnUserSignedIn += (o, s) =>
            {
                panelMenu.VisibleChanged += (s1, o1) =>
                {
                    if (!panelMenu.Visible) return;
                    SetActiveForm(GetMeasureForm(), btnMeasure);
                    userNameLabel.Text = SessionControl.CurrentUser.FullNameShort;
                    roleTitleLabel.Text = SessionControl.CurrentUserRole.UserRoleName;
                };
                panelMenu.Visible = SessionControl.SignedIn;

            };
            SetActiveForm(scf);
        }

        private void WorkStatusChanged_Handler(object sender, EventArgs e)
        {

            if (InvokeRequired)
            {
                BeginInvoke(new EventHandler(WorkStatusChanged_Handler), new object[] { sender, e });
            }
            else
            {
                DeviceBase d = sender as DeviceBase;
                DeviceWorkStatusEventArgs a = e as DeviceWorkStatusEventArgs;
               // AddOrUpdateDeviceOnToolStrip(d);
                if (d.WorkStatus == DeviceWorkStatus.DISCONNECTED)
                {
                    OnDeviceDisconnected_EventHandler(sender, e);
                }
                else
                {
                    if (IsServerApp)
                    {
                        ReplaceDeviceXMLStateOnServerCommandDispatcher(d.GetXMLState());
                    }
                }
            }
        }

        private void OnDeviceDisconnected_EventHandler(object sender, EventArgs e)
        {
            DeviceBase d = sender as DeviceBase;
            //MessageBox.Show($"Отключено {d.GetType().Name} Серийный номер {d.SerialYear}-{d.SerialNumber}");
            if (IsServerApp)
            {
                RemoveDeviceFromServerCommandDispatcher(d);
            }
        }

        private void RemoveDeviceFromServerCommandDispatcher(DeviceBase d)
        {
            if (serverCommandDispatcher != null)
            {
                serverCommandDispatcher.RemoveDeviceFromServerState(d.TypeId, d.Serial);
                CheckDeviceListChanges(serverCommandDispatcher.GetServerDevices());
            }
        }

        private void DeviceXMLStateChanged_Handler(object sender, EventArgs e)
        {
            DeviceBase d = sender as DeviceBase;
            if (IsServerApp)
            {
                ReplaceDeviceXMLStateOnServerCommandDispatcher(d.GetXMLState());
            }
        }

        private void AddDeviceToServerCommandDispatcher(DeviceBase d)
        {
            if (serverCommandDispatcher != null)
            {
                serverCommandDispatcher.AddDeviceToServerState(d.GetXMLState());
                CheckDeviceListChanges(serverCommandDispatcher.GetServerDevices());
            }
        }

        private void OnMeasureStatusChanged_Handler(object sender, EventArgs e)
        {
            //throw new NotImplementedException();
        }
        private void StopDeviceFinder()
        {
            if (DeviceDispatcher != null) DeviceDispatcher.Dispose();
        }

        private void RefreshMeasureStateOnServerSide(object arg1, EventArgs arg2)
        {
            if (serverCommandDispatcher != null)
            {
                MeasureXMLState s = arg1 as MeasureXMLState;
                serverCommandDispatcher.RefreshServerMeasureXMLState(s);
            }
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
                    OnDeviceConnected_Handler(ds);
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
                        OnDeviceConnected_Handler(xmlDevices[key]);
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
            MeasureForm f = OpenedMeasureForm;
            if (f != null)
            {
                if (xml_device.Serial == f.CapturedDeviceSerial && (int)f.CapturedDeviceType == xml_device.TypeId)
                {
                    f.RefreshCapturedXmlDevice(xml_device);
                }
            }
            //foreach (var f in measureFormsList.Values)
            //{
            //    if (xml_device.Serial == f.CapturedDeviceSerial && (int)f.CapturedDeviceType == xml_device.TypeId)
            //    {
            //        f.RefreshCapturedXmlDevice(xml_device);
            //    }
            //}
        }

        private void OnDeviceConnected_Handler(DeviceXMLState xml_device)
        {
            if (OpenedMeasureForm != null) OpenedMeasureForm.SetXmlDeviceList(xmlDevices);
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
            DisconnectDeviceFromMeasureForm(xml_device);
        }
        private void OnXmlDevicesListChanged_Handler()
        {
            RefreshDeviceListOnMeasureForm();

        }

        private void DisconnectDeviceFromMeasureForm(DeviceXMLState xml_device)
        {
            MeasureForm f = OpenedMeasureForm;
            if (f != null)
            {
                if (f.CapturedDeviceSerial == xml_device.Serial && (int)f.CapturedDeviceType == xml_device.TypeId)
                {
                    f.DisconnectDeviceFromServerSide();
                }
            }
        }
        private void RefreshDeviceListOnMeasureForm()
        {
            if (OpenedMeasureForm != null) OpenedMeasureForm.SetXmlDeviceList(xmlDevices);
        }

        private void OnMeasureCycleFlagChanged_Handler(object sender, EventArgs e)
        {
            if (InvokeRequired)
            {
                BeginInvoke(new EventHandler(OnMeasureCycleFlagChanged_Handler), new object[] { sender, e });
            }
            else
            {
                DeviceBase d = sender as DeviceBase;
                if (IsServerApp)
                {
                    ReplaceDeviceXMLStateOnServerCommandDispatcher(d.GetXMLState());
                }
            }
        }

        private void OnGetMeasureResult_Handler(object sender, EventArgs e)
        {
            if (InvokeRequired)
            {
                BeginInvoke(new EventHandler(OnGetMeasureResult_Handler), new object[] { sender, e });
            }
            else
            {
                DeviceBase d = sender as DeviceBase;
                MeasureResultEventArgs a = e as MeasureResultEventArgs;

                if (IsServerApp)
                {
                    ReplaceDeviceXMLStateOnServerCommandDispatcher(d.GetXMLState());
                }
            }
        }
        #endregion

        private void signoutButton_Click(object sender, EventArgs e)
        {
            currentForm.FormClosed += (s, a) =>
            {
                SessionControl.SignOut();
            };
            InitSessionForm();
        }
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
