using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using NormaMeasure.DBControl.Tables;

namespace NormaMeasure.DBControl.DBNormaMeasure.Forms
{
    public partial class UsersForm : DBEntityControlForm
    {
        private User selectedUser;
        private DBEntityTable usersTable;
        private DBEntityTable userRolesTable;


        public UsersForm()
        {
            InitializeComponent();
            this.formDataSet = userFormDataSet;
            fillUserRoles();
            fillUsers();
            MakeNewUser();
        }

        private void fillUsers()
        {
            usersTable = User.get_all_as_table();
            AddOrMergeTableToFormDataSet(usersTable);
            usersList.DataSource = usersTable;
            usersList.Refresh();
        }

        private void fillUserRoles()
        {
            userRolesTable = UserRole.get_all_as_table();
            AddOrMergeTableToFormDataSet(userRolesTable);
            userRole.DataSource = userRolesTable;
            userRole.ValueMember = "user_role_id";
            userRole.DisplayMember = "user_role_name";
            userRole.Refresh();
        }

        private void MakeNewUser()
        {
            selectedUser = User.build();
            switchUserFormMode();
        }

        private void switchUserFormMode()
        {
            bool isNewUser = selectedUser.IsNewRecord();
            saveUserData.Visible = cancelEditUser.Visible = !isNewUser;
            addUserButton.Visible = isNewUser;

            userLastName.Text = selectedUser.LastName;
            userFirstName.Text = selectedUser.FirstName;
            userThirdName.Text = selectedUser.ThirdName;
            userTabNum.Text = selectedUser.EmployeeNumber;
            userPassword.Text = selectedUser.Password;
            userRole.SelectedValue = selectedUser.RoleId;

            userFormField.Text = isNewUser ? "Новый пользователь" : "Изменение информации существующего пользователя";
        }

        private void addUserButton_Click(object sender, EventArgs e)
        {
            fillUserFromForm();
            if (selectedUser.Save())
            {
                fillUsers();
                MakeNewUser();
            }
        }

        private void saveUserData_Click(object sender, EventArgs e)
        {
            fillUserFromForm();
            if (selectedUser.Save())
            {
                fillUsers();
                MakeNewUser();
            }
        }

        private void fillUserFromForm()
        {
            selectedUser.FirstName = userFirstName.Text;
            selectedUser.LastName = userLastName.Text;
            selectedUser.ThirdName = userThirdName.Text;
            selectedUser.EmployeeNumber = userTabNum.Text;
            selectedUser.RoleId = Convert.ToUInt16(userRole.SelectedValue);
            selectedUser.Password = userPassword.Text;
        }

        private void cancelEditUser_Click(object sender, EventArgs e)
        {
            MakeNewUser();
        }

        private void initEditUserMode_Click(object sender, EventArgs e)
        {
            User selUser;
            if (usersList.SelectedRows.Count>0)
            {
                selUser = SelectUserInList();
                if (selUser != null)
                {
                    selectedUser = selUser;
                    switchUserFormMode();
                }
            }
        }


        private User SelectUserInList()
        {
            foreach(User u in usersTable.Rows)
            {
                if (u.UserId == Convert.ToUInt16(usersList.SelectedRows[0].Cells["user_id"].Value))
                {
                    return u;
                }
            }
            return null;
        }

        private void delUserMenuItem_Click(object sender, EventArgs e)
        {
            if (usersList.SelectedRows.Count > 0)
            {
                User delUser = SelectUserInList();
                if (delUser != null)
                {
                    DialogResult r = MessageBox.Show($"Вы уверены, что хотите удалить пользователя {delUser.FullNameShort}?", "Вопрос", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                    if (r == DialogResult.Yes)
                    {
                        if (delUser.DeleteUser())
                        {
                            fillUsers();
                        }
                    }
                }
            }
                
        }
    }
}
