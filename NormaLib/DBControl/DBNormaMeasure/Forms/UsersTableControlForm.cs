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
using NormaLib.DBControl.Tables;
using NormaLib.SessionControl;

namespace NormaLib.DBControl.DBNormaMeasure.Forms
{
    public partial class UsersTableControlForm : DBTableContolForm
    {
        protected DBEntityTable usersTable;
        protected DBEntityTable userRolesTable;
        public UsersTableControlForm() : base()
        {

        }
        protected override void InitDesign()
        {
            base.InitDesign();
            InitializeComponent();
        }

        protected override void InitNewEntityForm()
        {
            base.InitNewEntityForm();
            User user = User.build();
            UserForm uForm = new UserForm(user, userRolesTable);
            DialogResult dr = uForm.ShowDialog();
            if (dr == DialogResult.OK)
            {
                FillDataGridAsync();
            }
        }

        protected override void EditEntityHandler(DataGridViewRow selectedRow)
        {
            User u = GetUserByDataGridRow(selectedRow);
            if (u != null)
            {
                UserForm f = new UserForm(u, userRolesTable);
                DialogResult dr = f.ShowDialog();
                if (dr == DialogResult.OK)
                {
                    FillDataGridAsync();
                }
            }
        }

        protected override void RemoveEntityHandler(DataGridViewSelectedRowCollection selectedRows)
        {
            base.RemoveEntityHandler(selectedRows);
            string q = selectedRows.Count > 1 ? "Выбранные пользователи будут удалены безвозвратно" : "Выбранный пользователь будет удалён из Базы Данных безвозвратно";
            if (MessageBox.Show($"{q}.\n\nПродолжить?", "Вопрос", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
            {
                DeleteUsersAsync(selectedRows);
            }
        }

        async protected void DeleteUsersAsync(DataGridViewSelectedRowCollection rows)
        {
            int idx = 0;
            string msg = string.Empty;
            await Task.Run(() => {
                    foreach (DataGridViewRow row in rows)
                    {
                        User u = GetUserByDataGridRow(row);
                        if (u.DeleteUser())
                        {
                        idx++;
                        }
                    }
            });
            if (idx == 0)
            {
                MessageBox.Show("Не удалось удалить пользователя", "Ошибка...", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }else if (idx == rows.Count)
            {
                msg = idx > 1 ? "Выбранные пользователи успешно удалены" : $"Выбранный пользователь успешно удалён";
                MessageBox.Show($"{msg}", "Успешно", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
            }else if (idx != rows.Count)
            {
                MessageBox.Show($"Удалено удалено {idx} пользователей из {rows.Count}");
            }
            if (idx > 0) FillDataGridAsync();
        } 

        private User GetUserByDataGridRow(DataGridViewRow r)
        {
            User u = null;
            uint user_id = 0;
            if (UInt32.TryParse(r.Cells["user_id_column"].Value.ToString(), out user_id))
            {
                u = SelectUserInListById(user_id);
            }
            return u;
        }


        private User SelectUserInListById(uint id)
        {
            DataRow[] r = usersTable.Select($"user_id = {id}");
            if (r.Length > 0)
            {
                return (User)r[0];
            }
            else
            {
                return null;
            }
        }


        protected override List<DataGridViewColumn> BuildColumnsForDataGrid()
        {
            List<DataGridViewColumn> columns = base.BuildColumnsForDataGrid();
            columns.Add(BuildDataGridTextColumn("user_id", "ID", true));
            columns.Add(BuildDataGridTextColumn("last_name", "Фамилия", true));
            columns.Add(BuildDataGridTextColumn("first_name", "Имя", true));
            columns.Add(BuildDataGridTextColumn("third_name", "Отчество", true));
            columns.Add(BuildDataGridTextColumn("employee_number", "Табельный номер", true));
            columns.Add(BuildDataGridTextColumn("user_role_name", "Должность", true));
            columns.Add(BuildDataGridTextColumn("full_name", "Ф.И.О."));
            columns.Add(BuildDataGridTextColumn("user_role_id", "role_id"));
            columns.Add(BuildDataGridTextColumn("password", "Пароль"));
            columns.Add(BuildDataGridTextColumn("is_active", "Статус"));
            return columns;
        }

        protected override void ApplyUserRights()
        {
            AllowAddEntity = SessionControl.SessionControl.AllowAdd_User;
            AllowRemoveEntity = SessionControl.SessionControl.AllowEdit_User;
            AllowEditEntity = SessionControl.SessionControl.AllowRemove_User;
            base.ApplyUserRights();
        }

        protected void FillUsersDataTable()
        {
            usersTable = User.get_all_as_table();
            AddOrMergeTableToFormDataSet(usersTable);
        }

        protected void FillUserRolesDataTable()
        {
            userRolesTable = UserRole.get_all_as_table();
            AddOrMergeTableToFormDataSet(userRolesTable);
        }

        protected override DataTable FillDataSetAndGetDataGridTable()
        {
            WillShowDBException = DBSettingsControl.IsEnabled;
            FillUsersDataTable();
            FillUserRolesDataTable();
            return usersTable;
        }
    }
}
