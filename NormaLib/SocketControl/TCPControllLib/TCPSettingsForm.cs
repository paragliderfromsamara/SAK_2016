using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Net;
using System.Net.Sockets;

namespace NormaLib.SocketControl.TCPControlLib
{
    public partial class TCPSettingsForm : Form
    {

        string localIP => localIPComboBox.SelectedItem.ToString();
        int localPort => Convert.ToInt16(localPortInput.Value);
        string remoteIP => remoteIPInput.Text;
        int remotePort => Convert.ToInt16(remotePortInput.Value);
        string localIPWas;
        int localPortWas;
        string remoteIPWas;
        int remotePortWas;
        bool isPartOfSettingsMenu = false;

        bool isItServer => controller.IsServerSettings;
        TCPSettingsController controller;
        public TCPSettingsForm()
        {
            InitializeComponent();
            
        }

        private void InitLocalIPComboBox()
        {
            var host = Dns.GetHostEntry(Dns.GetHostName());
            int idx = -1;
            int i = -1;
            localIPComboBox.Items.Clear();
            foreach (var ip in host.AddressList)
            {
                if (ip.AddressFamily == AddressFamily.InterNetwork)
                {
                    i++;
                    if (ip.ToString() == localIPWas) idx = i;
                    localIPComboBox.Items.Add(ip.ToString());//addrs.Add(ip.ToString());
                }
            }
            if (localIPComboBox.Items.Count > 0)
            {
               localIPComboBox.SelectedIndex = (idx >= 0) ? idx : 0;
            }
            //return addrs.ToArray();
        }

        public TCPSettingsForm(TCPSettingsController _controller) : this()
        {
            controller = _controller;
            localPortWas = controller.localPortOnSettingsFile;
            localIPWas = controller.localIPOnSettingsFile;
            if (!isItServer)
            {
                remoteIPWas = controller.serverIPOnSettingsFile;
                remotePortWas = controller.serverPortOnSettingsFile;
            } 
            InitLocalIPComboBox();
            InitInputs();
        }

        private void InitInputs()
        {
            localPortInput.Value = (localPortWas < localPortInput.Minimum || localPortWas > localPortInput.Maximum) ? localPortInput.Minimum : localPortWas;
            if (!isItServer)
            {
                serverSettingsPanel.Visible = true;
                remoteIPInput.Text = remoteIPWas;
                remotePortInput.Value = (remotePortWas < remotePortInput.Minimum || remotePortWas > remotePortInput.Maximum) ? remotePortInput.Minimum : remotePortWas;
            }
            else
            {
                int delta = serverSettingsPanel.Height + 15;
                serverSettingsPanel.Visible = false;
                this.Height -= delta;
                saveButton.Top -= (delta);
                cancelButton.Top -= (delta);
            }
        }

        bool WasDataChanged()
        {
            bool f = (localIPWas != localIP || localPortWas != localPort);
            if (!isItServer) f |= (remoteIPWas != remoteIP || remotePortWas != remotePort);
            return f;
        }
        private void saveButton_Click(object sender, EventArgs e)
        {
           try
            {
                if (WasDataChanged())
                {
                    controller.InitIPAddresses(localIP, remoteIP);
                    controller.localIPOnSettingsFile = localIP;
                    controller.localPortOnSettingsFile = localPort;
                    if (!isItServer)
                    {
                        controller.serverIPOnSettingsFile = remoteIP;
                        controller.serverPortOnSettingsFile = remotePort;
                    }
                }
                if (isPartOfSettingsMenu) MessageBox.Show("Настройки успешно обновлены", "Успешно", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
                TCPSettingsController.OnTCPSettingsChanged?.Invoke(controller, new EventArgs());
            }
            catch(TCPSettingsControllerException ex)
            {
                MessageBox.Show(ex.Message, "Неверный формат TCP параметров");
            }
           // if (lo)
        }

        private void TCPSettingsForm_Load(object sender, EventArgs e)
        {
            if (this.Parent != null)
            {
                isPartOfSettingsMenu = this.Parent.GetType().Name == "TabPage";
            }
            else isPartOfSettingsMenu = false;
            
            cancelButton.Visible = !isPartOfSettingsMenu;
            InitLocalIPComboBox();
        }
    }
}
