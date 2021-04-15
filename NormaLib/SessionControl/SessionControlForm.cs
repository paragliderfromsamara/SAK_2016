using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using NormaLib.DBControl.DBNormaMeasure.Forms;
using NormaLib.DBControl.Tables;
using NormaLib.DBControl;
using System.Diagnostics;
using System.Threading;
using NormaLib.SocketControl.TCPControlLib;

namespace NormaLib.SessionControl
{
    public partial class SessionControlForm : Form
    {
        bool isOnLoading = false;
        DBEntityTable AllowedUsers;
        public bool IsServerApp = false;
        public EventHandler OnUserSignedIn;
        public SessionControlForm()
        {
            InitializeComponent();
            SetDefaultFormState();
            FillAllowedUsersAsync();
            connectionSettingsButton.Visible = false;
        }


        public void InitUsersList()
        {
            FillAllowedUsers();
            if (AllowedUsers.Rows.Count == 0)
            {
                DBEntityTable t = UserRole.get_by_id_as_table(UserRole.Metrolog);
                User u = User.build();
                u.RoleId = UserRole.Metrolog;
                UserForm f = new UserForm(u, t, true);
                f.FormClosed += (o, s) => { FillAllowedUsersAsync(); };
                f.ShowDialog(this);
            }
        }

        public void SetDefaultFormState()
        {
            userComboBox.DropDownStyle = ComboBoxStyle.DropDown;
            userComboBox.Text = "Список пуст";
            userComboBox.Enabled = false;
            buttonEntering.Enabled = false;
            passwordTextBox.Enabled = false;
        }

        async public void FillAllowedUsersAsync()
        {
            if (isOnLoading) return;
            DataTable dt = new DataTable();
            bool flag = false;
            dbStatusLbl.Visible = true;
            dbStatusLbl.Text = "Попытка связи с базой данных...";
            connectionSettingsButton.Enabled = false;
            isOnLoading = true;
            await Task.Run((Action)(() => {
                try
                {
                    AllowedUsers = User.get_all_as_table();
                    flag = true;
                }
                catch
                {
                    flag = false;
                }
            }));
            isOnLoading = false;
            connectionSettingsButton.Enabled = true;
            dbStatusLbl.Visible = !flag;
            connectionSettingsButton.Visible = !flag && !IsServerApp;
            if (flag)
            {
                InitUsersList();
            }
            else
            {
                dbStatusLbl.Text = "Не удалось подключиться к Базе данных";
                SetDefaultFormState();
            }
        }


        private void FillAllowedUsers()
        {
            userComboBox.Enabled = true;
            userComboBox.DropDownStyle = ComboBoxStyle.DropDownList;
            buttonEntering.Enabled = true;
            passwordTextBox.Enabled = true;
            userComboBox.DataSource = AllowedUsers;
            Debug.WriteLine(AllowedUsers.Rows.Count);
            userComboBox.ValueMember = User.UserId_ColumnName;
            userComboBox.DisplayMember = User.FullName_ColumnName;
        }

        private void buttonEntering_Click(object sender, EventArgs e)
        {
            User u = User.SignInByIDAndPassword((uint)userComboBox.SelectedValue, passwordTextBox.Text);
            if (u == null)
            {
                MessageBox.Show("Неправильный пароль", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            }else
            {
                SessionControl.SignIn(u);
                OnUserSignedIn.Invoke(this, new EventArgs());
            }
        }

        private void checkBox1_CheckedChanged(object sender, EventArgs e)
        {
            if (checkBox1.Checked) passwordTextBox.PasswordChar = (char)0;
            else passwordTextBox.PasswordChar = '•';
        }

        private void connectionSettingsButton_Click(object sender, EventArgs e)
        {
            TCPSettingsForm f = new TCPSettingsForm(new TCPSettingsController(IsServerApp));
            f.StartPosition = FormStartPosition.CenterScreen;
            f.ShowDialog();
        }
    }
}
