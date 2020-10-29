using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using NormaSocketControl;
using System.Globalization;
using System.Threading;
using NormaMeasure.Utils;

namespace TeraMicroMeasure
{
    public partial class MainForm : Form
    {
        WorspaceState currentState;
        WorkSpaceSubState currentSubState;
        SortedList<WorspaceState, WorkSpaceSubState[]> ApplicationStateList;
        private SortedList<string, NormaServerClient> clientList = new SortedList<string, NormaServerClient>();
        NormaServer server;
        public MainForm()
        {
            AppTypeSelector appSel = new AppTypeSelector();
            //appSel.ShowDialog();
            if (appSel.ShowDialog() == DialogResult.OK)
            {
                InitializeComponent();
                InitCulture();
                initStatusBar();
                HideLeftPanel();
                InitWorkSpace();
                if (Properties.Settings.Default.IsServerApp) InitAsServerApp();
                else InitAsClientApp();
            }
            else
            {
                Close();
            }

        }

        private void InitWorkSpace()
        {
            ApplicationStateList = new SortedList<WorspaceState, WorkSpaceSubState[]>();
            ApplicationStateList[WorspaceState.SETTINGS] = new WorkSpaceSubState[] { WorkSpaceSubState.CONNECTION_SETTINGS, WorkSpaceSubState.CLIENTS_SETTINGS, WorkSpaceSubState.DATABASE_SETTINGS };
            ApplicationStateList[WorspaceState.DATABASE] = new WorkSpaceSubState[] { WorkSpaceSubState.DATABASE_USERS, WorkSpaceSubState.DATABASE_CABLES, WorkSpaceSubState.DATABASE_TEST_RESULTS };
            
        }

        private void SetWorkSpaceState(WorspaceState state, WorkSpaceSubState subState = WorkSpaceSubState.NONE)
        {
            if (subState == WorkSpaceSubState.NONE) subState = ApplicationStateList[state][0];
            switch(state)
            {
                case WorspaceState.SETTINGS:
                    SetSettingsSubState(subState);
                    break;
                case WorspaceState.DATABASE:
                    break;
                case WorspaceState.MEASURE:
                    break;
            }
        }

        private void SetWorkSpaceSubState(WorkSpaceSubState subSate)
        {
            WorspaceState state = GetStateBySubState(subSate);
            if (state != WorspaceState.NONE) SetWorkSpaceState(state, subSate);
        }

        private WorspaceState GetStateBySubState(WorkSpaceSubState subState)
        {
            WorspaceState s = WorspaceState.NONE;
            foreach(var key in ApplicationStateList.Keys)
            {
                foreach(var ss in ApplicationStateList[key])
                {
                    if (ss == subState) 
                    { s = key; break; }
                }
                if (s != WorspaceState.NONE) break;
            }
            return s;
        }

        private void InitAsServerApp()
        {
            retry:
            string currentIp = SettingsControl.GetServerIp();
            string[] ipList = NormaServer.GetAvailableIpAddressList();
            this.Text = "Сервер";
            if (!NormaServer.IncludesIpOnList(currentIp) && !SettingsControl.GetOfflineMode())
            {
                if (ipList.Length == 1)
                {
                    if(ipList[0] == "127.0.0.1")
                    {
                        if (MessageBox.Show("Отсутсвуют доступные подключения!\n\nПри нажатии \"Отмена\" приложение запустится в автономном режиме.\n\nПри нажатии \"Повтор\" будет произведен повторный поиск", "Отсутствуют подключения", MessageBoxButtons.RetryCancel, MessageBoxIcon.Information) == DialogResult.Retry) goto retry;
                        SettingsControl.SetServerIpAndPort(ipList[0], "8888");
                    }
                }
                SelectServerIpForm ipForm = new SelectServerIpForm();
                ipForm.ShowDialog();
            }
            initServer();
        }

        private void InitAsClientApp()
        {
            this.Text = "Клиент";
        }

        private void initServer()
        {
            if (!SettingsControl.GetOfflineMode())
            {
                server = new NormaServer(SettingsControl.GetServerIp(), SettingsControl.GetServerPort());
                server.ProcessOnServerConnectionException += onServerException;
                server.OnClientConnected += OnClientConnected;
                server.Start();
                serverStatusLabel.Text = $"IP aдрес: {server.IpAddress}; порт: {server.Port}";
            }else
            {
                serverStatusLabel.Text = "Сервер выключен";
            }

        }


        private void OnClientConnected(NormaServerClient client)
        {
            //MessageBox.Show(client.IpAddress);
            if (InvokeRequired)
            {
                BeginInvoke(new NormaServerClientDelegate(OnClientConnected), new object[] { client });
                return;
            }
            else
            {
                client.OnMessageReceived += OnClientMeassageReceived;
                client.ClientConnectionException += OnClientDisconnected;
                clientList.Add(client.IpAddress, client);
                refreshClientCounterStatusText();
                MessageBox.Show(client.IpAddress);
            }
        }

        private void OnClientDisconnected(string addr, Exception ex)
        {
            if (InvokeRequired)
            {
                BeginInvoke(new NormaServerClientExceptionDelegate(OnClientDisconnected), new object[] { addr, ex });
                return;
            }
            else
            {
                clientList.Remove(addr);
                refreshClientCounterStatusText();
                return;
            }
        }
        private void onServerException(Exception ex)
        {
           if (ex.HResult != -2147467259) MessageBox.Show(ex.Message, $"Ошибка соединения: {ex.HResult}");
        }
        
        private string OnClientMeassageReceived(string message, NormaServerClient cl)
        {
            if (InvokeRequired)
            {
                BeginInvoke(new NormaServerClientMessageDelegate(OnClientMeassageReceived), new object[] { message, cl });
                return "Ok";
            }
            else
            {
                return "";
            }
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
            clientCounterStatus.Text =  $"Клиентов подключено: {clientList.Count}";
        }

        private void MainForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (Properties.Settings.Default.IsServerApp) MainForm_FormClosing_ForServer();
            else MainForm_FormClosing_ForClient();

        }

        private void MainForm_FormClosing_ForServer()
        {
            if (server != null) server.Stop();
            foreach (NormaServerClient cl in clientList.Values)
            {
                cl.Close();
            }

        }

        private void MainForm_FormClosing_ForClient()
        {

        }

        private void InitCulture()
        {
            Thread.CurrentThread.CurrentCulture = new CultureInfo("my");
            Thread.CurrentThread.CurrentCulture.NumberFormat.NumberDecimalSeparator = ".";
        }

        private void HideLeftPanel()
        {
            leftPanel.Hide();
            workSpacePanel.Dock = System.Windows.Forms.DockStyle.Fill;
        }

        private void ShowLeftPanel()
        {
            leftPanel.Show();
            workSpacePanel.Dock = System.Windows.Forms.DockStyle.None;
            workSpacePanel.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
            | System.Windows.Forms.AnchorStyles.Left)
            | System.Windows.Forms.AnchorStyles.Right)));
            workSpacePanel.Width = this.Width - leftPanel.Width;
            workSpacePanel.Height = this.Height - topPanel.Height;

        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (leftPanel.Visible) HideLeftPanel();
            else ShowLeftPanel();
        }


        private void label1_MouseHover(object sender, EventArgs e)
        {
            Label l = sender as Label;
            l.BackColor = System.Drawing.SystemColors.MenuHighlight;
        }

        private void label1_MouseLeave(object sender, EventArgs e)
        {
            Label l = sender as Label;
            l.BackColor = System.Drawing.SystemColors.HotTrack;
        }

        private void настройкиСервераToolStripMenuItem_Click(object sender, EventArgs e)
        {
            SetWorkSpaceSubState(WorkSpaceSubState.CONNECTION_SETTINGS);
        }


        #region Управление меню настроек

        private void SetSettingsSubState(WorkSpaceSubState subState)
        {
            if (currentState != WorspaceState.SETTINGS) InitSettingsLeftMenu();
        }

        private void InitSettingsLeftMenu()
        {
           
        }
        #endregion


    }

    enum WorspaceState
    {
        NONE,
        SETTINGS,
        DATABASE,
        MEASURE
    }

    enum WorkSpaceSubState
    {
        NONE,
        CONNECTION_SETTINGS,
        DATABASE_SETTINGS,
        CLIENTS_SETTINGS,
        DATABASE_USERS,
        DATABASE_CABLES,
        DATABASE_TEST_RESULTS
    }


}
