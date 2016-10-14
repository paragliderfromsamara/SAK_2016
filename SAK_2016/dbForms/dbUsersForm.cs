using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using MySql.Data.MySqlClient;

namespace SAK_2016
{
    public partial class dbUsersForm : Form
    {
        public mainForm mForm = null;
        public dbUsersForm(mainForm f)
        {
           
            mForm = f;
            InitializeComponent();
            initRolesList();
            initUserList();
            switchForm(true);
            clearForm();
        }

        private void closeBut_Click(object sender, EventArgs e)
        {
            this.Close();
            mForm.switchMenuStripItems(true);
            this.Dispose();

        }


       private void initRolesList()
        {
            DBControl mysql = new DBControl("bd_system");
            string com = mysql.GetSQLCommand("Roles");
            MySqlDataAdapter da = new MySqlDataAdapter(com, mysql.MyConn);
            da.Fill(rolesDataSet);
            userRole.DataSource = rolesDataSet.Tables[0];
            userRole.DisplayMember = "Dolshnost";
            userRole.ValueMember = "DolshNum";
            mysql.MyConn.Close();
            userRole.Update();

        }

        private void initUserList()
        {
            DBControl mysql = new DBControl("bd_system");
            usersDataSet.Reset();
            //mysql.MyConn.Open();
            string com = mysql.GetSQLCommand("Users");
            MySqlDataAdapter da = new MySqlDataAdapter(com, mysql.MyConn);
            da.Fill(usersDataSet);
            mysql.MyConn.Close();
            usersList.DataSource = usersDataSet.Tables[0];
            usersList.Refresh();
        }

        private void addUserButton_Click(object sender, EventArgs e)
        {
            DBControl mysql = new DBControl("bd_system");
           // MySqlCommand com;
            string com = mysql.GetSQLCommand("AddUser");
            if (checkForm(false))
            {
                userLastNameLabel.BorderStyle = new BorderStyle();
            }
            else
            {
                int[] arr = new int[usersList.Rows.Count - 1];
                com = String.Format(com, userLastName.Text, userFirstName.Text, userThirdName.Text, userPassword.Text, userTabNum.Text, userRole.SelectedValue);
                
                //queryView.Text = com;
                //MessageBox.Show(com, "Запрос", MessageBoxButtons.AbortRetryIgnore);
                //mysql.MyConn.Open();
                long id = mysql.RunNoQuery(com);
                mysql.MyConn.Close();
                //usersList.Rows.Add(id, userLastName.Text, userFirstName.Text, userThirdName.Text, userPassword.Text, userTabNum.Text, userRole.SelectedValue);
                for (int i = 0; i < usersList.Rows.Count - 1; i++)
                {
                    arr[i] = Convert.ToInt32(usersList.Rows[i].Cells[0].Value);
                }

                initUserList();
                clearForm();
                for (int i = 0; i < usersList.Rows.Count - 1; i++)
                {
                    bool hasVal = false;
                    for (int j = 0; j < arr.Length; j++)
                    {
                        hasVal = (arr[j] == Convert.ToInt32(usersList.Rows[i].Cells[0].Value));
                        if (hasVal) break;
                    }
                    if (!hasVal)
                    {
                        usersList.Rows[i].Selected = true;
                        usersList.FirstDisplayedScrollingRowIndex = i;
                        //break;
                    }
                    else usersList.Rows[i].Selected = false;
                }
            }
            
        }


        private void delUserToolStripMenuItem_Click(object sender, EventArgs e)
        {
            string val = ""; 
            if (usersList.SelectedRows.Count > 0)
            {
                DBControl mysql = new DBControl("bd_system");
                string com = mysql.GetSQLCommand("HideUsers");
                for (int i = 0; i < usersList.SelectedRows.Count; i++)
                {
                    val += usersList.SelectedRows[i].Cells[0].Value.ToString();
                    if (i != usersList.SelectedRows.Count - 1) val += ",";
                    usersList.Rows.RemoveAt(usersList.SelectedRows[i].Index);
                }
                com = String.Format(com, val);
                //mysql.MyConn.Open();
                mysql.RunNoQuery(com);
                mysql.MyConn.Close();
            }
           
        }

        private void editUserListRow_Click(object sender, EventArgs e)
        {
            if (usersList.SelectedRows.Count > 0)
            {
                switchForm(false);
                clearForm();
                userIdLbl.Text = usersList.SelectedRows[0].Cells[0].Value.ToString();
                userLastName.Text = usersList.SelectedRows[0].Cells[1].Value.ToString();
                userFirstName.Text = usersList.SelectedRows[0].Cells[2].Value.ToString();
                userThirdName.Text = usersList.SelectedRows[0].Cells[3].Value.ToString();
                userTabNum.Text = usersList.SelectedRows[0].Cells[4].Value.ToString();
                userRole.SelectedIndex = userRole.FindString(usersList.SelectedRows[0].Cells[5].Value.ToString());
            }
           
        }

        private void switchForm(bool f) //переключение формы создание на редактирование
        {
            cancelEditUser.Visible = saveUserData.Visible = !f;
            addUserButton.Visible = f;
            userFormField.Text = f ? "Новый пользователь" : "Редактирование существующего пользователя ";
        }

        private void saveUserData_Click(object sender, EventArgs e)
        {
            //UPDATE Familija_imja_ot SET Familija_imja_ot.Imja = "{1}", Familija_imja_ot.Familija = "{2}", Familija_imja_ot.Otchestvo = "{3}", Familija_imja_ot.TabNum = {4}, Familija_imja_ot.Dolshnost = {5}, Familija_imja_ot.Pass = "{6}"   
	        //WHERE Familija_imja_ot.UserNum IN({0})
            DBControl mysql = new DBControl("bd_system");
            int id = Convert.ToInt32(userIdLbl.Text);
            string com = "";
            if (!checkForm(true))
            {
                if (String.IsNullOrWhiteSpace(userPassword.Text))
                {
                    com = mysql.GetSQLCommand("UpdateUserWithoutPass");
                    com = String.Format(com, userIdLbl.Text, userFirstName.Text, userLastName.Text, userThirdName.Text, userTabNum.Text, userRole.SelectedValue);
                }
                else
                {
                    com = mysql.GetSQLCommand("UpdateUserWithPass");
                    com = String.Format(com, userIdLbl.Text, userFirstName.Text, userLastName.Text, userThirdName.Text, userTabNum.Text, userRole.SelectedValue, userPassword.Text);
                }
                //mysql.MyConn.Open();
                mysql.RunNoQuery(com);
                mysql.MyConn.Close();
                for (int i = 0; i < usersList.Rows.Count - 1; i++)
                {
                    if (usersList.Rows[i].Cells[0].Value.ToString() == userIdLbl.Text)
                    {
                        usersList.Rows[i].SetValues(userIdLbl.Text, userLastName.Text, userFirstName.Text, userThirdName.Text, userTabNum.Text, userRole.Text);
                        usersList.Rows[i].Selected = true;
                        usersList.FirstDisplayedScrollingRowIndex = i;
                    }
                    else
                    {
                        usersList.Rows[i].Selected = false;
                    }
                }
                
                usersList.Update();
                switchForm(true);
                clearForm();
            }
        }

        private void cancelEditUser_Click(object sender, EventArgs e)
        {
            switchForm(true);
            clearForm();
        }

        private void clearForm()
        {
            userLastName.Clear();
            userFirstName.Clear();
            userPassword.Clear();
            userThirdName.Clear();
            userTabNum.Clear();
            userIdLbl.Text = "";
        }

        private bool checkForm(bool isEdit)
        {
           if (String.IsNullOrWhiteSpace(userLastNameLabel.Text) || String.IsNullOrWhiteSpace(userFirstName.Text) || (String.IsNullOrWhiteSpace(userPassword.Text) && !isEdit))
           {
               string msgText = (isEdit) ? "Поля имя и фамилия должны быть заполнены!" : "Поля имя, фамилия и пароль должны быть заполнены!";
               MessageBox.Show(msgText, "Недопустимы пустые поля", MessageBoxButtons.OK, MessageBoxIcon.Error);
               return true;
           }
           else return false;
        }

    }
}
