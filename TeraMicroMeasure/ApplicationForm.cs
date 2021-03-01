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
using NormaMeasure.SocketControl.TCPControlLib;
using NormaMeasure.Devices;
using NormaMeasure.Devices.XmlObjects;
using NormaMeasure.DBControl.DBNormaMeasure;
using NormaMeasure.DBControl.DBNormaMeasure.Forms;
//using System.Diagnostics;

namespace TeraMicroMeasure
{
    public partial class ApplicationForm : Form
    {
        const int clientTryConnectionTimes = 5;
        int connectionTimesNow = clientTryConnectionTimes;
        int deviceDisconnectTimes = 0;
        int clientDisconnectedTimes = 0;
        int failureCount = 0;
        bool IsServerApp
        {
            get
            {
                return Properties.Settings.Default.IsServerApp;
            }
        }


        private System.Drawing.Color redColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))));
        private System.Drawing.Color greenColor = System.Drawing.Color.FromArgb(((int)(((byte)(21)))), ((int)(((byte)(179)))), ((int)(((byte)(9)))));
        private System.Drawing.Color orangeColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(140)))), ((int)(((byte)(0)))));
        private ClientXmlState[] clientList = new ClientXmlState[] { };

        ServerCommandDispatcher serverCommandDispatcher;
        ClientCommandDispatcher clientCommandDispatcher;
        Dictionary<int, MeasureForm> measureFormsList;
        DevicesDispatcher DeviceDispatcher;
        Dictionary<string, DeviceXMLState> xmlDevices = new Dictionary<string, DeviceXMLState>();
        List<string> WillDisconnectDevice = new List<string>();
        public ApplicationForm()
        {
            AppTypeSelector appSel = new AppTypeSelector();
            measureFormsList = new Dictionary<int, MeasureForm>();
            if (appSel.ShowDialog() == DialogResult.OK)
            {
                InitializeComponent();
                InitCulture();
                initStatusBar();
                initTopBar();
                InitButtonsBar();
                if (IsServerApp) InitAsServerApp();
                else InitAsClientApp();
                InitDeviceFinder();
            }
            else
            {
                Close();
            }
        }

        private void InitDeviceFinder()
        {
            DeviceDispatcher = new DevicesDispatcher(new DeviceCommandProtocol(), OnDeviceFound_EventHandler);
        }

        private void OnDeviceFound_EventHandler(object sender, EventArgs e)
        {
            if (InvokeRequired)
            {
                BeginInvoke(new EventHandler(OnDeviceFound_EventHandler), new object[] { sender, e});
            }else
            {
                DeviceBase d = sender as DeviceBase;
                switch (d.TypeId)
                {
                    case DeviceType.Microohmmeter:
                    case DeviceType.Teraohmmeter:
                        AddSimpleDeviceToSettingsFile(d);
                        break;
                }
                if (IsServerApp)
                {
                    AddDeviceToServerCommandDispatcher(d);
                }
                MessageBox.Show($"Подключено {d.GetType().Name} Серийный номер {d.SerialYear}-{d.SerialNumber}");
                AddOrUpdateDeviceOnToolStrip(d);

                //d.OnDisconnected += OnDeviceDisconnected_EventHandler;
                d.OnXMLStateChanged += DeviceXMLStateChanged_Handler;
                d.OnWorkStatusChanged += WorkStatusChanged_Handler;
                d.OnGetMeasureResult += OnGetMeasureResult_Handler;
                d.OnMeasureCycleFlagChanged += OnMeasureCycleFlagChanged_Handler;
            }

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

        private void AddOrUpdateDeviceOnToolStrip(DeviceBase d)
        {
            bool has = false;
            string el_name = $"deviceToolStripLabel_{(int)d.TypeId}_{d.SerialYear}_{d.SerialNumber}";
            string text = $"{d.SerialWithShortName} ({d.WorkStatusText})";
            foreach (var item in bottomStatusMenu.Items)
            {
                if (item.GetType().Name == typeof(ToolStripStatusLabel).Name)
                {
                    ToolStripStatusLabel l = item as ToolStripStatusLabel;
                    if (l.Name == el_name)
                    {
                        l.Text = text;
                        has = true;
                        break;
                    }
                }
            }
            if (!has)
            {
                ToolStripStatusLabel l = new ToolStripStatusLabel(text);
                l.Name = el_name;
                l.ForeColor = SystemColors.ControlLight;
                bottomStatusMenu.Items.Add(l);
            }

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
                AddOrUpdateDeviceOnToolStrip(d);
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

        private void AddSimpleDeviceToSettingsFile(DeviceBase d)
        {
            SettingsControl.CreateOrUpdateDevice(d);
        }

        private void OnDeviceDisconnected_EventHandler(object sender, EventArgs e)
        {
            DeviceBase d = sender as DeviceBase;
            
            MessageBox.Show($"Отключено {d.GetType().Name} Серийный номер {d.SerialYear}-{d.SerialNumber}");
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
            }
        }

        private void initTopBar()
        {
            testLinesToolStripMenuItem.Visible = IsServerApp;
            if (IsServerApp) InitTestLinesTabOnTopBar();
        }

        private void InitButtonsBar()
        {
            switchConnectToServerButton.Visible = !IsServerApp;
        }

        private void MainForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            StopDeviceFinder();
            if (IsServerApp) MainForm_FormClosing_ForServer();
            else MainForm_FormClosing_ForClient();
        }

        private void StopDeviceFinder()
        {
            if (DeviceDispatcher != null) DeviceDispatcher.Dispose();
        }

        private void InitCulture()
        {
            Thread.CurrentThread.CurrentCulture = new CultureInfo("my");
            Thread.CurrentThread.CurrentCulture.NumberFormat.NumberDecimalSeparator = ".";
        }


        private void InitAsServerApp()
        {
            this.Text = "Сервер измерений";
            initServerControl();
            initDataBase();
            SettingsControl.SetClientId(0);
        }

        private void initDataBase()
        {
            DBNormaMeasureTablesMigration tm = new DBNormaMeasureTablesMigration();
            tm.InitDataBase();
        }

        private void OnServerStatusChanged_Handler(object sender, EventArgs e)
        {
            if (InvokeRequired)
            {
                BeginInvoke(new EventHandler(OnServerStatusChanged_Handler), new object[] { sender, e });
            }else
            {
                TCPServer s = sender as TCPServer;
                switch(s.Status)
                {
                    case NORMA_SERVER_STATUS.ACTIVE:
                        connectionStatusLabel.Text = $"Сервер активен IP:{SettingsControl.GetLocalIP()} Порт: {SettingsControl.GetLocalPort()}";
                        break;
                    case NORMA_SERVER_STATUS.STOPPED:
                        connectionStatusLabel.Text = "Сервер остановлен";
                        break;
                    case NORMA_SERVER_STATUS.TRY_START:
                        connectionStatusLabel.Text = "Запуск сервера...";
                        break;
                    default:
                        connectionStatusLabel.Text = "Статус не обработан";
                        break;
                }
                if (s.Exception != null) MessageBox.Show(s.Exception.Message, s.Exception.GetType().Name);
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
           foreach(var f in measureFormsList.Values)
            {
                f.SetConnectionStatus(false);
            }
        }

        private void InitTestLinesTabOnTopBar()
        {
            int[] clientIds = SettingsControl.GetClientList();
            foreach(int id in clientIds)
            {
                AddTestLineDropDownItem(id);
            }
        }

        private ToolStripItem AddTestLineDropDownItem(int line_id)
        {
            ToolStripItem i = testLinesToolStripMenuItem.DropDownItems.Add($"Линия {line_id}");
            i.Name = $"ToolStripItemOfClient_{line_id}";
            i.Click += ToolStripItemOfClient_Click;
            return i;
        }

        private void ToolStripItemOfClient_Click(object sender, EventArgs e)
        {
            ToolStripItem i = sender as ToolStripItem;
            int clientId = GetLineIDFromToolStripItemName(i);
            if (clientId > 0) ShowClientForm(clientId);
        }

        private int GetLineIDFromToolStripItemName(ToolStripItem item)
        {
            int clientId = -1;
            int.TryParse(item.Name.Replace("ToolStripItemOfClient_", ""), out clientId);
            return clientId;
        }



        private void ShowClientForm(int client_id)
        {
            MeasureForm f = getOrCreateMeasureFormByClientId(client_id);
            if (HasClientWithId(client_id)) f.RefreshMeasureState(GetClientStateByClientID(client_id).MeasureState);
            if (!f.Visible) f.Show();
            if (f.WindowState == FormWindowState.Minimized) f.WindowState = FormWindowState.Normal;
        }

        private bool HasClientWithId(int client_id)
        {
            if (serverCommandDispatcher != null)
            {
                return serverCommandDispatcher.HasClientWithId(client_id);
            }
            else return false;
        }

        private ClientXmlState GetClientStateByClientID(int client_id)
        {
            if (serverCommandDispatcher != null)
            {
                return serverCommandDispatcher.GetClientStateByClientID(client_id);
            }return null;
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
            f.SetXmlDeviceList(xmlDevices);
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
            connectionTimesNow = clientTryConnectionTimes;
            SetClientTitle();
            setClientButtonStatus(ClientStatus.disconnected);
            ConnectToServer();
            InitMeasureForm();
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
                    ShowTCPSettingsForm();
                }
            }
            else
            {
                connectionStatusLabel.Text = "Сервер выключен";
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
                failureCounter.Text = $"Отвалов от сервера {++clientDisconnectedTimes}";
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
            }
        }

        private void RefreshTestLinesMenuItems(ClientXmlState onFocusClientState)
        {
            bool f = false;
            foreach(ToolStripDropDownItem tb in testLinesToolStripMenuItem.DropDownItems)
            {
                f = GetLineIDFromToolStripItemName(tb) == onFocusClientState.ClientID;
                if (f) break;
            }
            if (!f) AddTestLineDropDownItem(onFocusClientState.ClientID);
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
            }else
            {
                ClientIDChangedEventArgs a = e as ClientIDChangedEventArgs;
                MeasureForm f = getMeasureFormByClientId(a.IdWas);
                if (f != null) f.ClientID = a.IdNew;
            }
        }



        private void OnServerStateChangedByClient_Handler(object sender, EventArgs e)
        {
            ServerXmlStateEventArgs a = e as ServerXmlStateEventArgs;
            ServerCommandDispatcher d = sender as ServerCommandDispatcher;
            if (InvokeRequired)
            {
                d.RefreshCurrentServerState(a.ServerState);
                BeginInvoke(new EventHandler(OnServerStateChangedByClient_Handler), new object[] { d, a});
            }else
            {
                refreshClientCounterStatusText(a.ServerState.Clients.Count);
            }
        }

        private void initStatusBar()
        {
            bool isServer = Properties.Settings.Default.IsServerApp;

            clientCounterStatus.Visible = isServer;
            refreshClientCounterStatusText(0);
            connectionStatusLabel.Text = (isServer) ? "Сервер выключен" : "Нет подключения к серверу";
        }

        private void refreshClientCounterStatusText(int clients_count)
        {
            clientCounterStatus.Text = $"Клиентов подключено: {clients_count}";
        }


        private void serverStatusLabel_Click(object sender, EventArgs e)
        {
            ShowTCPSettingsForm();
        }


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
        #endregion

        #region ClientModePart

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

        private void OnServerStateReceived_Handler(object sender, EventArgs e)
        {
            ServerXmlState sState = sender as ServerXmlState;
            CheckDeviceListChanges(sState.Devices);
        }

        private void CheckDeviceListChanges(Dictionary<string, DeviceXMLState> devices)
        {
            bool listWasChanged = false;
            List<string> disList = new List<string>();
            foreach(var key in devices.Keys)
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
                    }else
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

        private void OnXmlDevicesListChanged_Handler()
        {
            foreach(var f in measureFormsList.Values)
            {
                f.SetXmlDeviceList(xmlDevices);
            }
        }

        private void DisconnectDeviceOnClientSideTimerTick_Handler(object o, EventArgs e)
        {
            bool f = false;
            DeviceDisconnectionTimer t = o as DeviceDisconnectionTimer;
            foreach(var key in t.DeviceKeys)
            {
                if (xmlDevices.ContainsKey(key) && WillDisconnectDevice.Contains(key))
                {
                    xmlDevices.Remove(key);
                    disconnectedDevices.Text = $"Отключено раз:{++deviceDisconnectTimes}";
                    
                    f = true;
                }
            }
            if (f) OnXmlDevicesListChanged_Handler();
            t.Dispose();
        }

        private void OnDeviceChangedOnServer_Handler(DeviceXMLState xml_device)
        {
            //MessageBox.Show($"Изменен {xml_device.ClientId}");
            foreach(var f in measureFormsList.Values)
            {
                if (xml_device.Serial == f.CapturedDeviceSerial && (int)f.CapturedDeviceType == xml_device.TypeId)
                {
                    f.RefreshCapturedXmlDevice(xml_device);
                }
            }
        }

        private void OnDeviceDisconnectedFromServer_Handler(DeviceXMLState xml_device)
        {
            //MessageBox.Show($"Отключен {xml_device.TypeNameFull}");
            foreach(var f in measureFormsList.Values)
            {
                if (f.CapturedDeviceSerial == xml_device.Serial && (int)f.CapturedDeviceType == xml_device.TypeId)
                {
                    f.DisconnectDeviceFromServerSide();
                }
            }
        }

        private void OnDeviceConnectedToServer_Handler(DeviceXMLState xml_device)
        {
            //MessageBox.Show($"Подключен {xml_device.TypeNameFull}");
        }



        private void OnMeasureStatusChanged_Handler(object sender, EventArgs e)
        {
            throw new NotImplementedException();
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
                        failureCounter.Text = $"Отвалов от сервера {failureCount++}";
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


        private void tryToConnect()
        {
            if (connectionTimesNow-- > 0)
            {
                Thread t = new Thread(new ThreadStart(tryConnectThreadFunc));
                t.Start(); 
            }else
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



        private void ClientIdChange_Handler(object o, EventArgs e)
        {
            if (InvokeRequired)
            {
                BeginInvoke(new EventHandler(ClientIdChange_Handler), new object[] { o, e });
            }else
            {
                int id = Convert.ToInt16(o);
                SettingsControl.SetClientId(id);
                SetClientTitle();
            }
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
                SetClientTitle();
            }
        }

        private void MainForm_FormClosing_ForClient()
        {
            disconnectFromServer();
        }

        private void refreshConnectionToServer()
        {
            disconnectFromServer();
            ConnectToServer();
        }


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

        private void switchConnectToServerButton_Click(object sender, EventArgs e)
        {
            if (clientCommandDispatcher != null)
            {
                disconnectFromServer();
            } else
            {
                connectionTimesNow = clientTryConnectionTimes;
                ConnectToServer();
            }
        }

        private void setClientButtonStatus(ClientStatus s)
        {
            switch(s)
            {
                case ClientStatus.connected:
                    switchConnectToServerButton.BackColor = greenColor;
                    switchConnectToServerButton.Text = char.ConvertFromUtf32(57723);
                    connectionStatusLabel.Text = $"Подключен к IP: {SettingsControl.GetServerIP()} Порт: {SettingsControl.GetServerPort()}";
                    break;
                case ClientStatus.disconnected:
                    switchConnectToServerButton.BackColor = redColor;
                    switchConnectToServerButton.Text = char.ConvertFromUtf32(57722);
                    connectionStatusLabel.Text = "Нет подключения к серверу";
                    break;
                case ClientStatus.tryConnect:
                    switchConnectToServerButton.BackColor = orangeColor;
                    switchConnectToServerButton.Text = char.ConvertFromUtf32(57722);
                    connectionStatusLabel.Text = $"Подключение.... Осталось попыток: {connectionTimesNow+1}";
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
            f.OnMeasureStateChanged += RefreshMeasureStateOnClientDispatcher;
            f.Show();
            //if (!IsServerApp) f.SetXmlDeviceList(xmlDevices);
        }

        private void RefreshMeasureStateOnClientDispatcher(object sender, EventArgs e)
        {
            MeasureXMLState s = sender as MeasureXMLState;
            if (clientCommandDispatcher != null) clientCommandDispatcher.RefreshMeasureState(s);
        }


        #endregion

        private void button1_Click(object sender, EventArgs e)
        {
            InitMeasureForm();
        }

        private void usersToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Form form = GetFormByTypeName(typeof(UsersForm).Name);
            UsersForm uForm = (form == null) ? new UsersForm() : form as UsersForm;
            uForm.MdiParent = this;
            uForm.Show();
        }

        private Form GetFormByTypeName(string name)
        {
            Form form = null;
            foreach(var f in this.MdiChildren)
            {
                if (f.GetType().Name == name)
                {
                    form = f;
                    break;
                }
            }
            return form;
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
            Thread.Sleep(1500);
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
