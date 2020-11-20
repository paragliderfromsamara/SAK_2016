using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace NormaMeasure.SocketControl.TCPServerControllLib
{
    public partial class TCPSettingsForm : Form
    {

        string localIPWas;
        int localPortWas;
        string remoteIPWas;
        int remotePortWas;
        public TCPSettingsForm()
        {
            InitializeComponent();
            InitLocalIPComboBox();
        }

        private void InitLocalIPComboBox()
        {
            throw new NotImplementedException();
        }

        public TCPSettingsForm(TCPSettingsController controller) : this()
        {
            localPortWas = controller.localPortOnSettingsFile;
            localIPWas = controller.localIPOnSettingsFile;

        }

        private void saveButton_Click(object sender, EventArgs e)
        {
           // if (lo)
        }
    }
}
