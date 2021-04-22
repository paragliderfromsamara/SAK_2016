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
    delegate void ProtocolSettingsDelegate(ProtocolSettingsXMLState state);
    public partial class ProtocolSettingsForm : Form
    {
        public ProtocolSettingsForm()
        {
            InitializeComponent();
            initMSWordPanel();
            initCommonPanel();
            FormClosing += ProtocolSettingsForm_FormClosing;
            

            ProtocolSettings.OnCommonSettingsChanged += (o, s) =>
            {
                if (ProtocolSettings.ReadOnly)
                {
                    ProtocolSettingsXMLState state = o as ProtocolSettingsXMLState;
                    RefreshProtocolSettings(state);
                }
            };
        }


        private void RefreshProtocolSettings(ProtocolSettingsXMLState settings)
        {
            if (InvokeRequired)
            {
                BeginInvoke(new ProtocolSettingsDelegate(RefreshProtocolSettings), new object[] { settings });
            }else
            {
                companyNameTextBox.Text = settings.CompanyName;
                protocolHeaderText.Text = settings.ProtocolHeader;
                printProtocolNumberAtTitle.Checked = settings.AddTestIdToProtocolTitleFlag;
            }
        }


        private void ProtocolSettingsForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            bool flag = true;
            flag &= SaveCommonSettings();
            flag &= SaveMSWordSettings();
            flag = false;
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
                    ProtocolSettings.MSWordProtocolsPath = msWordFilePathTextBox.Text = folderBrowser.SelectedPath;
                }
            }
        }

        private bool SaveMSWordSettings()
        {
            ProtocolSettings.MSWordProtocolsPath = msWordFilePathTextBox.Text;
            return true;
        }
        #endregion

        #region CommonSettings

        private bool SaveCommonSettings()
        {
            ProtocolSettings.DoesAddTestIdOnProtocolHeader = printProtocolNumberAtTitle.Checked;
            ProtocolSettings.CompanyName = companyNameTextBox.Text;
            ProtocolSettings.ProtocolHeader = protocolHeaderText.Text;
            return true;
        }

        private void initCommonPanel()
        {
            companyNameTextBox.Text = ProtocolSettings.CompanyName;
            printProtocolNumberAtTitle.Checked = ProtocolSettings.DoesAddTestIdOnProtocolHeader;
            protocolHeaderText.Text = ProtocolSettings.ProtocolHeader;
            commonSettingsPanel.Enabled = !ProtocolSettings.ReadOnly;
        }
        #endregion

        private void printProtocolNumberAtTitle_CheckedChanged(object sender, EventArgs e)
        {
            if (commonSettingsPanel.Enabled) ProtocolSettings.DoesAddTestIdOnProtocolHeader = (sender as CheckBox).Checked;
        }

        private void companyNameTextBox_TextChanged(object sender, EventArgs e)
        {
           if (commonSettingsPanel.Enabled) ProtocolSettings.CompanyName = (sender as TextBox).Text;
        }

        private void protocolHeaderText_TextChanged(object sender, EventArgs e)
        {
           if (commonSettingsPanel.Enabled) ProtocolSettings.ProtocolHeader = (sender as TextBox).Text;
        }
    }

}
