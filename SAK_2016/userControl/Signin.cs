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
            string query = "select Familija_imja_ot.UserNum, Familija_imja_ot.Familija, Familija_imja_ot.Imja, Familija_imja_ot.Otchestvo, Familija_imja_ot.TabNum, Dolshnosti.Dolshnost from Familija_imja_ot LEFT JOIN Dolshnosti ON (Familija_imja_ot.Dolshnost = Dolshnosti.DolshNum) WHERE Familija_imja_ot.Familija = \"{0}\" AND Familija_imja_ot.TabNum = {1} AND Familija_imja_ot.Pass = {2} LIMIT 1";
            if (String.IsNullOrWhiteSpace(lastName.Text) || String.IsNullOrWhiteSpace(tabNum.Text) || String.IsNullOrWhiteSpace(password.Text))
            {
                MessageBox.Show("Необходимо заполнить все поля", "Неверный ввод", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            else
            {
                query = String.Format(query, lastName.Text, tabNum.Text, password.Text);
                DBControl mysql = new DBControl("bd_system");
                signedUser.Reset();
                //mysql.MyConn.Open();
                MySqlDataAdapter da = new MySqlDataAdapter(query, mysql.MyConn);
                 da.Fill(signedUser);
                mysql.MyConn.Close();
                if (signedUser.Tables[0].Rows.Count > 0)
                {
                    this.userId = signedUser.Tables[0].Rows[0]["UserNum"].ToString();
                    this.userTabNum = signedUser.Tables[0].Rows[0]["TabNum"].ToString();
                    this.userFirstName = signedUser.Tables[0].Rows[0]["Imja"].ToString();
                    this.userLastName = signedUser.Tables[0].Rows[0]["Familija"].ToString();
                    this.userThirdName = signedUser.Tables[0].Rows[0]["Otchestvo"].ToString();
                    this.userRole = signedUser.Tables[0].Rows[0]["Dolshnost"].ToString();

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
