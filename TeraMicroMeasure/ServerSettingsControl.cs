using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using NormaMeasure.SocketControl;

namespace TeraMicroMeasure
{
    public class ServerSettingsControl
    {
        private System.Drawing.Point curPoint;
        private string ipAddress;
        private int port;
        private ComboBox cb;
        private NumericUpDown ud;
        private Panel panel;
        private string[] addrList;
        public Button ConfirmButton;
        public EventHandler OnButtonClick;
        public ServerSettingsControl(Control parent)
        {
            panel = new Panel();
            panel.Parent = parent;
            panel.Dock = DockStyle.Fill;
            panel.BackColor = System.Drawing.Color.AliceBlue;
            panel.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            addrList = NormaServer.GetAvailableIpAddressList();
            fill_data_from_settings();
            init_panel();
        }

        private void fill_data_from_settings()
        {
            ipAddress = SettingsControl.GetServerIp();
            port = SettingsControl.GetServerPort();
        }

        private void init_panel()
        {
            curPoint = new System.Drawing.Point(0, 0);
            init_ip_input();
            init_port_input();
            init_save_button();
        }

        private int get_selected_ip_index()
        {
            if (addrList.Length == 0) return -1;
            for(int i=0; i<addrList.Length; i++)
            {
                if (addrList[i] == ipAddress) return i; 
            }
            return 0;
        }

        private void init_ip_input()
        {
            cb = new ComboBox();
            Label l = new Label();
            Button rb = new Button();
            rb.Parent = l.Parent = cb.Parent = panel;
            l.Text = "IP адрес сервера";
            foreach(var s in addrList)
            {
                cb.Items.Add(s);
            }
            rb.Text = "Обновить";
            cb.Location = new System.Drawing.Point(20, 30);
            rb.Location = new System.Drawing.Point(cb.Location.X + cb.Width, 30);
            rb.Height = cb.Height;
            rb.Click += Rb_Click;
            l.Location = new System.Drawing.Point(20, 12);
            cb.SelectedIndex = get_selected_ip_index();
            cb.DropDownStyle = ComboBoxStyle.DropDownList;

            curPoint.X = rb.Location.X + rb.Width + 20;
            curPoint.Y = 30;
        }

        private void fill_ip_input_items()
        {
            addrList = NormaServer.GetAvailableIpAddressList();

        }

        private void Rb_Click(object sender, EventArgs e)
        {
            cb.Items.Clear();
            addrList = NormaServer.GetAvailableIpAddressList();
            foreach (var s in addrList)
            {
                cb.Items.Add(s);
            }
            cb.SelectedIndex = get_selected_ip_index();
        }

        private void init_port_input()
        {
            ud = new NumericUpDown();
            Label l = new Label();
            l.Parent = ud.Parent = panel;
            l.Text = "Порт сервера";
            ud.Maximum = 16000;
            ud.Minimum = 3000;
            ud.Value = port < ud.Minimum ? ud.Minimum : port;
            ud.Location = new System.Drawing.Point(curPoint.X, curPoint.Y);
            l.Location = new System.Drawing.Point(curPoint.X, curPoint.Y - 18);
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

        private void ConfirmButton_Click(object sender, EventArgs e)
        {
            SettingsControl.SetServerIpAndPort(cb.SelectedItem.ToString(), ud.Value.ToString());
            OnButtonClick?.Invoke(sender, e);
        }
    }
}
