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
    public partial class Signin : Form
    {
        public string userRole = "undefined";
        public string userId = "undefined";
        public string userFirstName = "undefined";
        public string userLastName = "undefined";
        public string userThirdName = "undefined";
        public string userTabNum = "undefined";
        public Signin()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            string query = "select users.id, users.last_name, users.name, users.third_name, users.employee_number, roles.name as role_name from users LEFT JOIN roles ON (users.role_id = roles.id) WHERE users.last_name = \"{0}\" AND users.employee_number = {1} AND users.password = {2} LIMIT 1";
            if (String.IsNullOrWhiteSpace(lastName.Text) || String.IsNullOrWhiteSpace(tabNum.Text) || String.IsNullOrWhiteSpace(password.Text))
            {
                MessageBox.Show("Необходимо заполнить все поля", "Неверный ввод", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            else
            {
                query = String.Format(query, lastName.Text, tabNum.Text, password.Text);
                DBControl mysql = new DBControl(Properties.dbSakQueries.Default.dbName);
                signedUser.Reset();
                mysql.MyConn.Open();
                MySqlDataAdapter da = new MySqlDataAdapter(query, mysql.MyConn);
                 da.Fill(signedUser);
                mysql.MyConn.Close();
                if (signedUser.Tables[0].Rows.Count > 0)
                {
                    this.userId = signedUser.Tables[0].Rows[0]["id"].ToString();
                    this.userTabNum = signedUser.Tables[0].Rows[0]["employee_number"].ToString();
                    this.userFirstName = signedUser.Tables[0].Rows[0]["name"].ToString();
                    this.userLastName = signedUser.Tables[0].Rows[0]["last_name"].ToString();
                    this.userThirdName = signedUser.Tables[0].Rows[0]["third_name"].ToString();
                    this.userRole = signedUser.Tables[0].Rows[0]["role_name"].ToString();

                    this.DialogResult = System.Windows.Forms.DialogResult.OK;
                }
                else
                {
                    MessageBox.Show("Пользователь с указанными данными не найден. Проверьте правильность введённых символов", "Пользователь не найден", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
                }
            }




        }
    }
}
