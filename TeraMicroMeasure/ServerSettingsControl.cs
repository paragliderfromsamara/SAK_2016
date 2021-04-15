using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using NormaLib.SocketControl;

namespace TeraMicroMeasure
{
    public class ServerSettingsControl
    {
        private System.Drawing.Point curPoint;
        private string localIpAddress;
        private int localPort;
        private int serverPort;
        private string serverIpAddress;
        private ComboBox localIpComboBox;
        private TextBox serverIpTextBox;
        private NumericUpDown localPortUpDown;
        private NumericUpDown serverPortUpDown;
        private Button refreshIpListButton;
        private Panel panel;
        private string[] addrList;
        public Button ConfirmButton;
        public EventHandler OnButtonClick;
        private bool is_server => Properties.Settings.Default.IsServerApp;
        public ServerSettingsControl(Control parent)
        {
            panel = new Panel();
            panel.Parent = parent;
            panel.Dock = DockStyle.Fill;
            panel.BackColor = System.Drawing.Color.AliceBlue;
            panel.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            addrList = NormaServerDeprecated.GetAvailableIpAddressList();
            fill_data_from_settings();
            init_panel();
        }

        private void fill_data_from_settings()
        {
            localIpAddress = SettingsControl.GetLocalIP();
            localPort = SettingsControl.GetLocalPort();
            if (!is_server)
            {
                serverIpAddress = SettingsControl.GetServerIP();
                serverPort = SettingsControl.GetServerPort();
            }
        }

        private void init_panel()
        {
            curPoint = new System.Drawing.Point(20, 40);
            init_local_settings_input();
            if (!is_server) init_server_settings_input();
            init_save_button();
        }

        private void init_local_settings_input()
        {
            init_local_ip_combobox();
            init_local_port_input();
        }

        private void init_server_settings_input()
        {
            init_server_ip_input();
            init_server_port_input();
        }

        private void init_local_port_input()
        {
            localPortUpDown = new NumericUpDown();
            Label l = new Label();
            l.Parent = localPortUpDown.Parent = panel;
            l.Text = "Локальный порт";
            localPortUpDown.Maximum = 16000;
            localPortUpDown.Minimum = 3000;
            localPortUpDown.Value = localPort < localPortUpDown.Minimum ? localPortUpDown.Minimum : localPort;
            localPortUpDown.Location = new System.Drawing.Point(curPoint.X, curPoint.Y);
            l.Location = new System.Drawing.Point(curPoint.X, curPoint.Y - 25);
            l.Width = localPortUpDown.Width;
            curPoint.X = 20;
            curPoint.Y = curPoint.Y + 60;
        }

        private void init_server_port_input()
        {
            serverPortUpDown = new NumericUpDown();
            curPoint.X = localPortUpDown.Location.X;
            Label l = new Label();
            l.Parent = serverPortUpDown.Parent = panel;
            l.Text = "Порт сервера";
            serverPortUpDown.Maximum = 16000;
            serverPortUpDown.Minimum = 3000;
            serverPortUpDown.Value = serverPort < serverPortUpDown.Minimum ? serverPortUpDown.Minimum : serverPort;
            serverPortUpDown.Location = new System.Drawing.Point(curPoint.X, curPoint.Y);
            l.Location = new System.Drawing.Point(curPoint.X, curPoint.Y - 25);
            l.Width = serverPortUpDown.Width;
            curPoint.X = 20;
            curPoint.Y = 150;
        }

        private int get_selected_ip_index()
        {
            if (addrList.Length == 0) return -1;
            for(int i=0; i<addrList.Length; i++)
            {
                if (addrList[i] == localIpAddress) return i; 
            }
            return 0;
        }

        //private void init_ip_input()
        //{
        //    if (Properties.Settings.Default.IsServerApp)
        //    {
        //        init_as_combobox();
        //    }
        //    else init_as_text_input();
        //
        //}

        private void init_server_ip_input()
        {
            serverIpTextBox = new TextBox();
            Label l = new Label();
            serverIpTextBox.Parent = l.Parent = panel;
            l.Text = "IP адрес сервера";
            serverIpTextBox.Location = new System.Drawing.Point(curPoint.X, curPoint.Y);
            l.Location = new System.Drawing.Point(curPoint.X, curPoint.Y - 25);
            serverIpTextBox.Text = serverIpAddress;
            serverIpTextBox.Width = localIpComboBox.Width + localIpComboBox.Height;
            l.Width = serverIpTextBox.Width;
            curPoint.X = serverIpTextBox.Location.X + serverIpTextBox.Width + 20;
            curPoint.Y = serverIpTextBox.Location.Y;
        }

        private void init_local_ip_combobox()
        {
            localIpComboBox = new ComboBox();
            Label l = new Label();
            refreshIpListButton = new Button();
            refreshIpListButton.Parent = l.Parent = localIpComboBox.Parent = panel;
            l.Text = "Локальный IP адрес";
            foreach (var s in addrList)
            {
                localIpComboBox.Items.Add(s);
            }
            refreshIpListButton.FlatStyle = FlatStyle.Flat;
            refreshIpListButton.FlatAppearance.BorderSize = 1;
            refreshIpListButton.Font = new System.Drawing.Font("Segoe MDL2 Assets", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            refreshIpListButton.Text = char.ConvertFromUtf32(57673);
            localIpComboBox.Location = new System.Drawing.Point(curPoint.X, curPoint.Y);
            refreshIpListButton.Location = new System.Drawing.Point(localIpComboBox.Location.X + localIpComboBox.Width, curPoint.Y);
            refreshIpListButton.Height = localIpComboBox.Height;
            refreshIpListButton.Width = localIpComboBox.Height;
            refreshIpListButton.Cursor = Cursors.Hand;
            refreshIpListButton.Click += Rb_Click;
            l.Location = new System.Drawing.Point(curPoint.X, curPoint.Y - 25);
            localIpComboBox.SelectedIndex = get_selected_ip_index();
            localIpComboBox.DropDownStyle = ComboBoxStyle.DropDownList;
            l.Width = localIpComboBox.Width + localIpComboBox.Height;
            curPoint.X = refreshIpListButton.Location.X + refreshIpListButton.Width + 20;
            //curPoint.Y = 30;
        }


        private void Rb_Click(object sender, EventArgs e)
        {
            localIpComboBox.Items.Clear();
            addrList = NormaServerDeprecated.GetAvailableIpAddressList();
            foreach (var s in addrList)
            {
                localIpComboBox.Items.Add(s);
            }
            localIpComboBox.SelectedIndex = get_selected_ip_index();
        }



        private void init_save_button()
        {
            ConfirmButton = new Button();
            ConfirmButton.Parent = panel;
            ConfirmButton.Text = "ОК";
            ConfirmButton.Size = new System.Drawing.Size(90, 30);
            ConfirmButton.Location = new System.Drawing.Point(panel.Size.Width / 2 - ConfirmButton.Size.Width / 2, panel.Size.Height - 20 - ConfirmButton.Size.Height);
            ConfirmButton.Click += ConfirmButton_Click;
        }

        private bool IsValidData()
        {
            string ip = localIpComboBox.SelectedItem.ToString();
            string srvIp = !is_server ? serverIpTextBox.Text : "";
            int p = Convert.ToUInt16(localPortUpDown.Value);
            int srvPort = is_server ? 0 : Convert.ToUInt16(serverPortUpDown.Value);
            bool f;
            try
            {
                f = NormaServerDeprecated.IsValidIPString(ip);


                if (!is_server) f &= NormaServerDeprecated.IsValidIPString(srvIp);
            }
            catch
            {
                return false;
            }
            this.localIpAddress = ip;
            this.localPort = p;
            if (!is_server)
            {
                serverPort = srvPort;
                serverIpAddress = srvIp;
            }
            return f;
        }
    
        private void ConfirmButton_Click(object sender, EventArgs e)
        {
           if (IsValidData())
           {
                SettingsControl.SetLocalIpAndPort(localIpAddress, localPort.ToString());
                if (!is_server) SettingsControl.SetServerIpAndPort(serverIpAddress, serverPort.ToString());
                OnButtonClick?.Invoke(sender, e);
            }else
            {
                MessageBox.Show("IP адрес введён некорректно!!!", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
    }
}
