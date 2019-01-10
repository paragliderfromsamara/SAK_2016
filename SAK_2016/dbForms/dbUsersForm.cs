using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using MySql.Data.MySqlClient;

namespace NormaMeasure.SAC_APP
{
    public partial class dbUsersForm : Form
    {
        public mainForm mForm = null;
        private DBControl mysql = new DBControl(Properties.dbSakQueries.Default.dbName);
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
            //mForm.switchMenuStripItems(true);
            //this.Dispose();
            //MessageBox.Show("Good", "GD", MessageBoxButtons.OK);
        }


       private void initRolesList()
        {
            string com = mysql.GetSQLCommand("Roles");
            MySqlDataAdapter da = new MySqlDataAdapter(com, mysql.MyConn);
            da.Fill(rolesDataSet);
            userRole.DataSource = rolesDataSet.Tables[0];
            userRole.DisplayMember = "name";
            userRole.ValueMember = "id";
            mysql.MyConn.Close();
            userRole.Update();

        }

        private void initUserList()
        {
            usersDataSet.Reset();
            mysql.MyConn.Open();
            string com = mysql.GetSQLCommand("Users");
            MySqlDataAdapter da = new MySqlDataAdapter(com, mysql.MyConn);
            da.Fill(usersDataSet);
            mysql.MyConn.Close();
            usersList.DataSource = usersDataSet.Tables[0];
            usersList.Refresh();
        }

        private void addUserButton_Click(object sender, EventArgs e)
        {
            string com = mysql.GetSQLCommand("AddUser");
            if (checkForm(false))
            {
                userLastNameLabel.BorderStyle = new BorderStyle();
            }
            else
            {
                int[] arr = new int[usersList.Rows.Count - 1];
                com = String.Format(com, userLastName.Text, userFirstName.Text, userThirdName.Text, userPassword.Text, userTabNum.Text, userRole.SelectedValue);
                mysql.MyConn.Open();
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
                DBControl mysql = new DBControl(Properties.dbSakQueries.Default.dbName);
                string com = mysql.GetSQLCommand("HideUsers");
                for (int i = 0; i < usersList.SelectedRows.Count; i++)
                {
                    val += usersList.SelectedRows[i].Cells[0].Value.ToString();
                    if (i != usersList.SelectedRows.Count - 1) val += ",";
                    usersList.Rows.RemoveAt(usersList.SelectedRows[i].Index);
                }
                com = String.Format(com, val);
                mysql.MyConn.Open();
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
                mysql.MyConn.Open();
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
            bool noLastName = String.IsNullOrWhiteSpace(userLastName.Text);
            bool noName = String.IsNullOrWhiteSpace(userFirstName.Text);
            bool noPswrd = String.IsNullOrWhiteSpace(userPassword.Text);
            bool noTabNum = String.IsNullOrWhiteSpace(userTabNum.Text);
            userLastName.BackColor = noLastName ? System.Drawing.Color.FromArgb(255, 193, 193) : System.Drawing.Color.Empty;
            userFirstName.BackColor = noName ? System.Drawing.Color.FromArgb(255, 193, 193) : System.Drawing.Color.Empty;
            userPassword.BackColor = noPswrd && !isEdit ? System.Drawing.Color.FromArgb(255, 193, 193) : System.Drawing.Color.Empty;
            userTabNum.BackColor = noTabNum ? System.Drawing.Color.FromArgb(255, 193, 193) : System.Drawing.Color.Empty;
            if ((noLastName || noName || noPswrd || noTabNum) && !isEdit)
           {
               string msgText = (isEdit) ? "Поля имя, фамилия и табельный номер должны быть заполнены!" : "Поля имя, фамилия, табельный номер и пароль должны быть заполнены!";

                MessageBox.Show(msgText, "Недопустимы пустые поля", MessageBoxButtons.OK, MessageBoxIcon.Error);
               
               return true;
           }
           else return false;
        }

        private void userTabNum_KeyPress(object sender, KeyPressEventArgs e)
        {
            e.Handled = !ServiceFunctions.IntCharChecker(e.KeyChar);
        }

        private void dbUsersForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            mForm.switchMenuStripItems(true);
            this.mysql.Dispose();
            this.Dispose();
            MessageBox.Show("Good", "GD", MessageBoxButtons.OK);
        }
    }
}
