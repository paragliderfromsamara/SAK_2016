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
                initStatusBar();
                if (Properties.Settings.Default.IsServerApp) InitAsServerApp();
                else InitAsClientApp();
            }
            else
            {
                Close();
            }
        }

        private void InitAsServerApp()
        {
            this.Text = "Сервер";
            initServer();
        }

        private void InitAsClientApp()
        {
            this.Text = "Клиент";
        }

        private void initServer()
        {
            server = new NormaServer(13389);
            server.ProcessOnServerConnectionException += onServerException;
            server.OnClientConnected += OnClientConnected;
            server.Start();
            serverStatusLabel.Text = "IP aдрес: " + server.IpAddress;
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
    }
}
