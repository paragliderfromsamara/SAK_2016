using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using MySql.Data.MySqlClient;
using SAK_2016.dbSakModels;

namespace SAK_2016.dbForms
{
    public partial class oldDbDataMigration : Form
    {
        public mainForm mForm = null;
        public oldDbDataMigration(mainForm f)
        {
            mForm = f;
            mForm.switchMenuStripItems(false);
            InitializeComponent();
            loadOldDataSets();
            checkMigrateButEnable();
            test.Text = "string:value".Replace("string:", "");
        }

        private void oldDbDataMigration_FormClosing(object sender, FormClosingEventArgs e)
        {
            //e.Cancel = true;
            mForm.switchMenuStripItems(true);
            mForm.oldDbDataMigrationForm = null;
            this.Dispose();
        }

        /// <summary>
        /// Проверяет чек боксы, и если ни один не выбран, то выключает кнопку
        /// </summary>
        private void checkMigrateButEnable()
        {
            migrateSelected.Enabled = oldDbBarabansCheckBox.Checked || oldDbCablesCheckBox.Checked || oldUsersCheckBox.Checked;
        }
        /// <summary>
        /// Загружает данные из старых БД до 2016 года
        /// </summary>
        private void loadOldDataSets()
        {
            bool bdSysFlag = dataMigration.checkDBExists("bd_system");
            bool bdBarabanFlag = dataMigration.checkDBExists("bd_baraban");
            bool bdCableFlag = dataMigration.checkDBExists("bd_cable");
            if (bdSysFlag)
            {
                oldUsersCheckBox.Enabled = true;
                DBControl sysCon = new DBControl("bd_system");
                string selOldUsersQuery = sysCon.GetSQLCommand("users_in_old_db");
                sysCon.MyConn.Open();
                MySqlDataAdapter sysAd = new MySqlDataAdapter(selOldUsersQuery, sysCon.MyConn);
                sysAd.Fill(oldUserDbDataSet);
                sysCon.MyConn.Close();
                sysCon.Dispose();
                oldUsersCheckBox.Enabled = (oldUserDbDataSet.Tables[0].Rows.Count > 0) ? true : false;
            } else
            {
                oldUsersCheckBox.Enabled = false;
            }
            if (bdBarabanFlag)
            {
                oldDbBarabansCheckBox.Enabled = true;
            }
            else
            {
                oldDbBarabansCheckBox.Enabled = false;
            }
            if (bdCableFlag)
            {
                oldDbCablesCheckBox.Enabled = true;
            }
            else
            {
                oldDbCablesCheckBox.Enabled = false;
            }
            if (!bdCableFlag && !bdSysFlag && !bdBarabanFlag)
            {
                MessageBox.Show("Базы данных предыдущих версий отсутствуют", "Устаревшие БД отсутствуют", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
                migrateSelected.Enabled = false;
            }
                
        }

        private void oldUsersCheckBox_CheckedChanged(object sender, EventArgs e)
        {
            checkMigrateButEnable();
        }

        private void oldDbCablesCheckBox_CheckedChanged(object sender, EventArgs e)
        {
            checkMigrateButEnable();
        }

        private void oldDbBarabansCheckBox_CheckedChanged(object sender, EventArgs e)
        { 
        }

        private void migrateSelected_Click(object sender, EventArgs e)
        {
            if (oldUsersCheckBox.Enabled && oldUsersCheckBox.Checked) migrateOldUsers();
        }

        /// <summary>
        /// Производит миграцию пользоваетелей в новую бд с проверкой их наличия в ней
        /// </summary>
        private void migrateOldUsers()
        {
            
            //newUsersArr.IsFixedSize = false;
            string[][] chUsrParams = { new string[] { "name", "string" }, new string[] { "last_name", "string" }, new string[] { "third_name", "string" }, new string[] { "employee_number", "int" } };
            string[][] chRolesParams = { new string[] { "id", "int" }, new string[] { "name", "string" } };
            DataTable usrs = oldUserDbDataSet.Tables[0];
            string[][] newUsersArr = new string[usrs.Rows.Count][];
            DBControl mySql = new DBControl(Properties.Settings.Default.dbName);
            
            int difCounter = 0;
            for (int i=0; i< usrs.Rows.Count; i++)
            {
                string checkUserQuery="";
                string checkRoleQuery = "";
                bool eFlag;
                DataRow r = usrs.Rows[i];

                //mySql.MyConn.Open();
                //eFlag = mySql.checkFieldExistingInDb("users", checkUserQuery);
                //mySql.MyConn.Close();
                //if (!eFlag)
                //{
                    User usr = new User(r);
                    usr.Create();
                    difCounter++;
                    
                //}
                
            }
            if (newUsersArr.Length > 0)
            {
                string query = dataMigration.makeColumnsStringFromBiLevelArray(newUsersArr);
                test.Text = query;
            }
            
            //test.Text = query;
        }
    }
}
