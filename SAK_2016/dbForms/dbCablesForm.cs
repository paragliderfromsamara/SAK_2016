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
    public partial class dbCablesForm : Form
    {
        public mainForm mForm = null;
        private addCableForm newCableForm = null;

        public dbCablesForm(mainForm f)
        {
            UserGrants grants = new UserGrants(f.user_type);
            mForm = f;
            InitializeComponent();
            initCablesList();
            openCableFormBut.Visible = grants.userCouldAddCable();
        }
        private void closeBut_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void openCableFormBut_Click(object sender, EventArgs e)
        {
            newCableForm = new addCableForm();
            newCableForm.Show();
        }

        private void dbCablesForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            mForm.switchMenuStripItems(true);
            this.Dispose();
        }

private void initCablesList()
{
   DBControl mysql = new DBControl(Properties.Settings.Default.dbName);
   dbCablesDataSet.Reset();
   mysql.MyConn.Open();
   string com = mysql.GetSQLCommand("ShowCables");
   MySqlDataAdapter da = new MySqlDataAdapter(com, mysql.MyConn);
   da.Fill(dbCablesDataSet);
   mysql.MyConn.Close();
   cablesList.DataSource = dbCablesDataSet.Tables[0];
   cablesList.Refresh();
}



    }
}
