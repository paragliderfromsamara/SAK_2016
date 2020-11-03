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

namespace TeraMicroMeasure
{
    public partial class MainForm : Form
    {
        private SortedList<string, NormaTCPClient> clientList = new SortedList<string, NormaTCPClient>();
        NormaServer server;
        NormaTCPClient client;
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

            testXml();
        }

        private void testXml()
        {
            TeraMicroClientXmlObject o = new TeraMicroClientXmlObject();
            o.OperatorName = "Вася";
            richTextBox1.Text = o.InnerXml;
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
            initServer();
        }
        private void InitAsClientApp()
        {
            this.Text = "Клиент";
            retry:
            string localIp = SettingsControl.GetLocalIP();
            int localPort = SettingsControl.GetLocalPort();
            string serverIp = SettingsControl.GetServerIP();
            int serverPort = SettingsControl.GetServerPort();
            if (string.IsNullOrWhiteSpace(localIp) || string.IsNullOrWhiteSpace(serverIp))
            {
                SelectServerIpForm ipForm = new SelectServerIpForm();
                ipForm.ShowDialog();
            }
            //if (String.IsNullOrWhiteSpace(currentIp)) MessageBox.Show("Нет данных о сервере");
            //else MessageBox.Show(currentIp);
            try
            {
                client = new NormaTCPClient(localIp, serverIp, localPort, serverPort);
                client.OnAnswerReceived += OnServerAnswerReceived;
                client.ClientSendMessageException += LostServerConnection;
                client.Send("Привет!!!");
                // client.OnMessageReceived += ;
                // client.
            }
            catch(Exception ex)
            {
                SelectServerIpForm ipForm = new SelectServerIpForm();
                ipForm.ShowDialog();
                //MessageBox.Show(ex.Message);
                goto retry;
            }
        }

        private void LostServerConnection(string ipAddr, Exception ex)
        {
            MessageBox.Show("Подключение разорвано");
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
                server = new NormaServer(SettingsControl.GetLocalIP(), SettingsControl.GetLocalPort());
                server.ProcessOnServerConnectionException += onServerException;
                server.OnClientConnected += OnClientConnected;
                server.Start();
                serverStatusLabel.Text = $"IP aдрес: {server.IpAddress}; порт: {server.Port}";
            }else
            {
                serverStatusLabel.Text = "Сервер выключен";
            }

        }

        private void OnServerAnswerReceived(string message, NormaTCPClient cl)
        {
            //cl.MessageToSend = "Hello Fucker!!!";
            if (InvokeRequired)
            {
                BeginInvoke(new NormaTCPMessageDelegate(OnServerAnswerReceived), new object[] { message, cl });
            }
            else
            {
                MessageBox.Show(message);//
            }
        }

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
        private void onServerException(Exception ex)
        {
           if (ex.HResult != -2147467259) MessageBox.Show(ex.Message, $"Ошибка соединения: {ex.HResult}");
        }
        
        private void OnClientMeassageReceived(string message, NormaTCPClient cl)
        {
            cl.MessageToSend = "Hello Fucker!!!";
            if (InvokeRequired)
            {
                BeginInvoke(new NormaTCPMessageDelegate(OnClientMeassageReceived), new object[] {message, cl });
            }
            else
            {
                MessageBox.Show(message, "От клиента");//
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
            foreach (NormaTCPClient cl in clientList.Values)
            {
               cl.Close();
            }
        }

        private void MainForm_FormClosing_ForClient()
        {
            stopConnectionToServer();
        }

        private void stopConnectionToServer()
        {
            if (client != null)
            {
                client.ClientReceiveMessageException = null;
                client.ClientSendMessageException = null;
                client.Close();
            } 
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
