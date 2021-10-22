using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using NormaLib.UI;
using NormaLib.DBControl;

namespace TeraMicroMeasure.Forms
{
    public partial class DataBaseSettingsForm : Form
    {
        string hostWas;
        string userNameWas;
        string passwordWas;
        bool _changed = false;
        bool changed
        {
            set
            {
                btnReset.Enabled = btnSave.Enabled = _changed = value;
            }get
            {
                return _changed;
            }
        }

        public DataBaseSettingsForm()
        {
            InitializeComponent();
            tbHostName.Enabled = tbUserName.Enabled = tbUserPassword.Enabled = btnReset.Visible = btnSave.Visible = (SettingsControl.IsServerApp || SettingsControl.IsSinglePCMode);
            lblSettingsInfo.Visible = !(SettingsControl.IsServerApp || SettingsControl.IsSinglePCMode); 
        }

        private void DataBaseSettingsForm_Load(object sender, EventArgs e)
        {
            titleLbl1.ForeColor = NormaUIColors.PrimaryColor;
            subtitleLbl1.ForeColor = NormaUIColors.SecondaryColor;
            subTitleLbl2.ForeColor = NormaUIColors.SecondaryColor;
            subTitleLbl3.ForeColor = NormaUIColors.SecondaryColor;
            FillData();
            CheckChanges();
        }

        private void FillData()
        {
            hostWas = tbHostName.Text = DBSettingsControl.ServerHost;
            userNameWas = tbUserName.Text = DBSettingsControl.UserName;
            passwordWas = tbUserPassword.Text = DBSettingsControl.Password;
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            try
            {
                DBSettingsControl.ServerHost = tbHostName.Text;
                DBSettingsControl.UserName = tbUserName.Text;
                DBSettingsControl.Password = tbUserPassword.Text;
                MySQLDBControl dc = new MySQLDBControl();
                dc.GetDBList();
                dc.Dispose();
                MessageBox.Show("Новые параметры подключения сохранены", "Успешно", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
                hostWas = tbHostName.Text = DBSettingsControl.ServerHost;
                userNameWas = tbUserName.Text = DBSettingsControl.UserName;
                passwordWas = tbUserPassword.Text = DBSettingsControl.Password;
                CheckChanges();
            }
            catch(Exception)
            {
                DialogResult dr = MessageBox.Show($"Не удалось подключиться к серверу MySql с новыми параметрами\n\nВсё равно сохранить новые параметры?", "Ошибка", MessageBoxButtons.YesNo, MessageBoxIcon.Error);
                if (dr == DialogResult.No)
                {
                    tbHostName.Text = DBSettingsControl.ServerHost = hostWas;
                    tbUserName.Text = DBSettingsControl.UserName = userNameWas;
                    tbUserPassword.Text = DBSettingsControl.Password = passwordWas;
                }else
                {
                    hostWas = tbHostName.Text = DBSettingsControl.ServerHost;
                    userNameWas = tbUserName.Text = DBSettingsControl.UserName;
                    passwordWas = tbUserPassword.Text = DBSettingsControl.Password;
                    CheckChanges();
                }
            }
        }

        private void ResetParameters()
        {
            tbHostName.Text = DBSettingsControl.ServerHost = hostWas;
            tbUserName.Text = DBSettingsControl.UserName = userNameWas;
            tbUserPassword.Text = DBSettingsControl.Password = passwordWas;
            CheckChanges();
        }

        private void OnInput_ChangedHandler(object sender, EventArgs e)
        {
            CheckChanges();
        }

        void CheckChanges()
        {
            changed = (tbHostName.Text != hostWas || tbUserName.Text != userNameWas || tbUserPassword.Text != passwordWas);
        }


        private void btnReset_Click(object sender, EventArgs e)
        {
            ResetParameters();
        }
    }
}
