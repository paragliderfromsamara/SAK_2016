using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using NormaLib.DBControl.Tables;


namespace NormaMeasure.SAC_APP
{
    public partial class Signin : Form
    {
        /// <summary>
        /// Логиненный пользователь
        /// </summary>
        public User sUser;

        /// <summary>
        /// 
        /// </summary>
        public Signin()
        {
            InitializeComponent();
            sUser = User.build();
        }

        private void fillUserFromFields()
        {
            sUser.LastName = lastName.Text;
            sUser.EmployeeNumber = tabNum.Text;
            sUser.Password = password.Text;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            fillUserFromFields();
            if (String.IsNullOrWhiteSpace(sUser.LastName) || String.IsNullOrWhiteSpace(sUser.EmployeeNumber) || String.IsNullOrWhiteSpace(sUser.Password))
            {
                MessageBox.Show("Необходимо заполнить все поля", "Неверный ввод", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            else
            {
                sUser = User.SignIn(sUser);
                if (sUser != null)
                {
                    DialogResult = DialogResult.OK;
                }
                else
                {
                    MessageBox.Show("Пользователь с указанными данными не найден. Проверьте правильность введённых символов", "Пользователь не найден", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
                }
            }




        }
    }
}
