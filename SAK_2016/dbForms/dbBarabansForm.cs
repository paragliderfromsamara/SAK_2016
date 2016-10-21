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
    public partial class dbBarabansForm : Form
    {
        public mainForm mForm = null;
        private DBControl mysql = new DBControl(Properties.dbSakQueries.Default.dbName);
        public dbBarabansForm(mainForm f)
        {
            mForm = f;
            InitializeComponent();
            initBarabansList();
        }
        private void initBarabansList()
        {
            barabanTypesDS.Tables["baraban_types"].Rows.Clear();
            string com = Properties.dbSakQueries.Default.selectBarabanTypes;
            mysql.MyConn.Open();
            MySqlDataAdapter da = new MySqlDataAdapter(com, mysql.MyConn);
            da.Fill(barabanTypesDS.Tables["baraban_types"]);
            mysql.MyConn.Close();
            barabanTypeList.DataSource = barabanTypesDS.Tables["baraban_types"];
            barabanTypeList.Refresh();
        }

        private void closeBut_Click(object sender, EventArgs e)
        {
            this.Close();

        }

        private void addBarabanType_Click(object sender, EventArgs e)
        {
            string com = Properties.dbSakQueries.Default.insertBarabanType;
            if (checkForm())
            {
                int[] arr = new int[barabanTypeList.Rows.Count - 1];
                com = String.Format(com, barabanName.Text, barabanWeight.Text);
                mysql.MyConn.Open();
                long id = mysql.RunNoQuery(com);
                mysql.MyConn.Close();
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
            bool f;
            if (String.IsNullOrWhiteSpace(barabanName.Text) || String.IsNullOrWhiteSpace(barabanWeight.Text))
            {
                string msgText = "Все поля должны быть заполнены!";
                MessageBox.Show(msgText, "Недопустимы пустые поля", MessageBoxButtons.OK, MessageBoxIcon.Error);
                
                f = false;
            }else
            {

                f = true;
            }
            barabanName.BackColor = !f ? System.Drawing.Color.FromArgb(255, 193, 193) : System.Drawing.Color.Empty;
            barabanWeight.BackColor = !f ? System.Drawing.Color.FromArgb(255, 193, 193) : System.Drawing.Color.Empty;
            return f;
        }

        private void barabanWeight_KeyPress(object sender, KeyPressEventArgs e)
        {
            e.Handled = !ServiceFunctions.DoubleCharChecker(e.KeyChar);
        }

        private void barabanWeight_Leave(object sender, EventArgs e)
        {
            barabanWeight.Text = ServiceFunctions.checkDecimalValInTextBox(barabanWeight);
        }

        private void dbBarabansForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            mForm.switchMenuStripItems(true);
            this.Dispose();
        }
    }
}
