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

namespace NormaLib.SessionControl
{
    public partial class SessionControlForm : Form
    {
        DBEntityTable AllowedUsers;
        public EventHandler OnUserSignedIn;
        public SessionControlForm()
        {
            InitializeComponent();
            FillAllowedUsers();
            if (AllowedUsers.Rows.Count == 0)
            {
                DBEntityTable t = UserRole.get_by_id_as_table(UserRole.Metrolog);
                User u = User.build();
                u.RoleId = UserRole.Metrolog;
                UserForm f = new UserForm(u, t, true);
                f.FormClosed += (o, s) => { FillAllowedUsers(); };
                f.ShowDialog(this);

            }
        }

        private void FillAllowedUsers()
        {
            AllowedUsers = User.get_all_as_table();
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
    }
}
