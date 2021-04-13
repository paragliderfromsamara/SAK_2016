using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace NormaLib.ProtocolBuilders
{
    public partial class ProtocolSettingsForm : Form
    {
        public ProtocolSettingsForm()
        {
            InitializeComponent();
            initMSWordPanel();
        }

        #region MS Word протоколы
        private void initMSWordPanel()
        {
            msWordFilePathTextBox.Text = ProtocolSettings.MSWordProtocolsPath;
        }

        private void serchFolderButton_Click(object sender, EventArgs e)
        {
            using (FolderBrowserDialog folderBrowser = new FolderBrowserDialog())
            {
                folderBrowser.SelectedPath = ProtocolSettings.MSWordProtocolsPath;
                if (folderBrowser.ShowDialog() == DialogResult.OK)
                {
                    ProtocolSettings.MSWordProtocolsPath = folderBrowser.SelectedPath;
                    initMSWordPanel();
                }
            }
        }

        #endregion
    }

}
