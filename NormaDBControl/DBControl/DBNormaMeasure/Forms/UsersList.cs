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
    public partial class UsersList : DBEntityControlForm
    {
        private User selectedUser;
        private DBEntityTable usersTable;
        private DBEntityTable userRolesTable;


        public UsersList()
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
            usersListTable.DataSource = usersTable;
            usersListTable.Refresh();
        }

        private void fillUserRoles()
        {
            userRolesTable = UserRole.get_all_as_table();
            AddOrMergeTableToFormDataSet(userRolesTable);
        }

        private void MakeNewUser()
        {
            selectedUser = User.build();
        }


        private void addUserButton_Click(object sender, EventArgs e)
        {
            UserForm uf = new UserForm(selectedUser, userRolesTable);
            uf.Location = this.Location;
            uf.Owner = this.Owner;
            DialogResult dr = uf.ShowDialog();
            if (dr == DialogResult.OK)
            {
                fillUsers();
                MakeNewUser();
            }
        }

        private void cancelEditUser_Click(object sender, EventArgs e)
        {
            MakeNewUser();
        }

        private void initEditUserMode_Click(object sender, EventArgs e)
        {
            User selUser;
            if (usersListTable.SelectedRows.Count>0)
            {
                selUser = SelectUserInList();
                
                if (selUser != null)
                {
                    UserForm uf = new UserForm(selUser, userRolesTable);
                    DialogResult dr = uf.ShowDialog(this.Parent);
                    if (dr == DialogResult.OK)
                    {
                        fillUsers();
                    }
                }
            }
        }


        private User SelectUserInList()
        {
            foreach(User u in usersTable.Rows)
            {
                if (u.UserId == Convert.ToUInt16(usersListTable.SelectedRows[0].Cells["user_id"].Value))
                {
                    return u;
                }
            }
            return null;
        }

        private void delUserMenuItem_Click(object sender, EventArgs e)
        {
            if (usersListTable.SelectedRows.Count > 0)
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
