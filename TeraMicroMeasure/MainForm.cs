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

namespace TeraMicroMeasure
{
    public partial class MainForm : Form
    {
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

        private void reinitServer()
        {
            stopServer();
            initServer();
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
            stopServer();
        }

        private void stopServer()
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

        private void serverStatusLabel_Click(object sender, EventArgs e)
        {
            SelectServerIpForm ipForm = new SelectServerIpForm();
            ipForm.FormClosed += IpForm_FormClosed;
            ipForm.ShowDialog();
        }

        private void IpForm_FormClosed(object sender, FormClosedEventArgs e)
        {
            reinitServer();
        }
    }


}
