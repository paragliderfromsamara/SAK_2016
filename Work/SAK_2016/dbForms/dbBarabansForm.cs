using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace SAK_2016
{
    public partial class dbBarabansForm : Form
    {
        public mainForm mForm = null;
        public dbBarabansForm(mainForm f)
        {
            mForm = f;
            InitializeComponent();
            initBarabansList();
        }

        private void initBarabansList()
        {
            DBControl mysql = new DBControl("bd_baraban");
            barabanTypeDataSet.Reset();
            mysql.MyConn.Open();
            string com = mysql.GetSQLCommand("Barabans");
            CoreLab.MySql.MySqlDataAdapter da = new CoreLab.MySql.MySqlDataAdapter(com, mysql.MyConn);
            da.Fill(barabanTypeDataSet);
            mysql.MyConn.Close();
            barabanTypeList.DataSource = barabanTypeDataSet.Tables[0];
            barabanTypeList.Refresh();

        }

        private void closeBut_Click(object sender, EventArgs e)
        {
            this.Close();
            mForm.switchMenuStripItems(true);
            this.Dispose();
        }

        private void addBarabanType_Click(object sender, EventArgs e)
        {
            DBControl mysql = new DBControl("bd_baraban");
            string com = mysql.GetSQLCommand("AddBaraban");
            if (checkForm())
            {
                //to_do
            }
            else
            {
                int[] arr = new int[barabanTypeList.Rows.Count - 1];
                com = String.Format(com, barabanName.Text, barabanWeight.Text);
                mysql.MyConn.Open();
                long id = mysql.RunNoQuery(com);
                mysql.MyConn.Close();
                //usersList.Rows.Add(id, userLastName.Text, userFirstName.Text, userThirdName.Text, userPassword.Text, userTabNum.Text, userRole.SelectedValue);
                for (int i = 0; i < barabanTypeList.Rows.Count - 1; i++)
                {
                    arr[i] = Convert.ToInt32(barabanTypeList.Rows[i].Cells[0].Value);
                }

                initBarabansList();
                clearForm();
                for (int i = 0; i < barabanTypeList.Rows.Count - 1; i++)
                {
                    bool hasVal = false;
                    for (int j = 0; j < arr.Length; j++)
                    {
                        hasVal = (arr[j] == Convert.ToInt32(barabanTypeList.Rows[i].Cells[0].Value));
                        if (hasVal) break;
                    }
                    if (!hasVal)
                    {
                        barabanTypeList.Rows[i].Selected = true;
                        barabanTypeList.FirstDisplayedScrollingRowIndex = i;
                        //break;
                    }
                    else barabanTypeList.Rows[i].Selected = false;
                }
            }
        }

        private void clearForm()
        {
            barabanName.Text = barabanWeight.Text = "";
        }

        private bool checkForm()
        {
            if (String.IsNullOrWhiteSpace(barabanName.Text) || String.IsNullOrWhiteSpace(barabanWeight.Text))
            {
                string msgText = "Все поля должны быть заполнены!";
                MessageBox.Show(msgText, "Недопустимы пустые поля", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return true;
            }
            else return false;
        }

    }
}
