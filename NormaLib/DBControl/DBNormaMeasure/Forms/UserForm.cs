using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using NormaLib.DBControl.Tables;

namespace NormaLib.DBControl.DBNormaMeasure.Forms
{
    public partial class UserForm : Form
    {
        User user;
        DBEntityTable userRolesTable;
        public UserForm(User form_user, DBEntityTable roles)
        {
            InitializeComponent();
            user = form_user;
            userRolesTable = roles;
            fillUserRolesComboBox();
            InitByUser();
        }

        private void InitByUser()
        {
            saveBurtton.Text = user.IsNewRecord() ? "Создать" : "Сохранить";
            this.Text = user.IsNewRecord() ? "Новый пользователь" : "Изменение информации о пользователе";

            name.Text = user.FirstName;
            lastName.Text = user.LastName;
            thirdName.Text = user.ThirdName;
            tabNum.Text = user.EmployeeNumber;
            userRole.SelectedValue = user.RoleId;
            passwodText.Text = user.Password;
        }


        private void UserForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (DialogResult == DialogResult.Cancel)
            {
                e.Cancel = false;
            }else if (DialogResult == DialogResult.OK)
            {
                if (user.Save())
                {
                    e.Cancel = false;
                }else
                {
                    e.Cancel = true;
                    MessageBox.Show(this.Parent, user.ErrorsAsNumericList, "Не удалось сохранить пользователя...", System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Warning);

                }

            }
        }

        private void saveBurtton_Click(object sender, EventArgs e)
        {
            user.FirstName = name.Text;
            user.LastName = lastName.Text;
            user.ThirdName = thirdName.Text;
            user.EmployeeNumber = tabNum.Text;
            user.Password = passwodText.Text;
            user.RoleId = (uint)userRole.SelectedValue;

        }

        private void fillUserRolesComboBox()
        {
            userRolesTable = UserRole.get_all_as_table();
            userRole.DataSource = userRolesTable;
            userRole.ValueMember = "user_role_id";
            userRole.DisplayMember = "user_role_name";
            userRole.Refresh();
        }
    }
}
