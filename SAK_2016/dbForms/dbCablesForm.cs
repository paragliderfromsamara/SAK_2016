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

    /// <summary>
    /// Форма просмотра и изменения списка кабелей
    /// </summary>
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

/// <summary>
/// Подгружаем список кабелей
/// </summary>
private void initCablesList()
{
   DBControl mysql = new DBControl(Properties.dbSakQueries.Default.dbName);
   dbCablesDataSet.Reset();
   mysql.MyConn.Open();
   MySqlDataAdapter da = new MySqlDataAdapter(Properties.dbSakQueries.Default.selectCables, mysql.MyConn);
   da.Fill(dbCablesDataSet);
   mysql.MyConn.Close();
   cablesList.DataSource = dbCablesDataSet.Tables[0];
   cablesList.Refresh();
}

        private void editCableItem_Click(object sender, EventArgs e)
        {
            string sId;
            uint cableId;
            if (cablesList.SelectedRows.Count > 0)
            {
                sId = cablesList.SelectedRows[0].Cells[0].Value.ToString();
                //try
                //{
                    cableId = Convert.ToUInt32(cablesList.SelectedRows[0].Cells[0].Value);
                    newCableForm = new addCableForm(cableId);
                    newCableForm.Show();
                //}
                //catch(Exception)
                //{
               //     MessageBox.Show( "Выделите кабель, который хотите изменить", "Не был выбран кабель", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
               // }
                
                
            }
            else
            {
                MessageBox.Show("Выберите кабель, который хотите изменить", "Не был выбран кабель", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
            }
            
            
            
        }
    }
}
