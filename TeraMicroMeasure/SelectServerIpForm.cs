using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace TeraMicroMeasure
{
    public partial class SelectServerIpForm : Form
    {
        ServerSettingsControl serverControl;
        public SelectServerIpForm()
        {
            InitializeComponent();
            if (!Properties.Settings.Default.IsServerApp) this.Height *= 2;
            InitControlPanel();

        }

        private void InitControlPanel()
        {
            serverControl = new ServerSettingsControl(workSpacePanel);
            serverControl.OnButtonClick += ConfirmButton_Click;
        }

        private void ConfirmButton_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
