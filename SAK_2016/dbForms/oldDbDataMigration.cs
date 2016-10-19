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
        private DBControl mySql = new DBControl(Properties.Settings.Default.dbName);
        private int oldUsersCount = 0;
        private int oldCablesCount = 0;
        private int oldBarabansCount = 0;
        private int pbScope = 0;
        public oldDbDataMigration(mainForm f)
        {
            mForm = f;
            mForm.switchMenuStripItems(false);
            InitializeComponent();
            loadOldDataSets();
            checkMigrateButEnable();
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
            
            bool bdSysFlag = DBControl.checkDBExists("bd_system");
            bool bdBarabanFlag = DBControl.checkDBExists("bd_baraban");
            bool bdCableFlag = DBControl.checkDBExists("bd_cable");
            mySql = new DBControl(Properties.Settings.Default.dbName);
            mySql.MyConn.Open();
            long cUsers = mySql.Count("users");
            long cCables = mySql.Count("cables");
            long cBarabans = mySql.Count("baraban_types");
            mySql.MyConn.Close();
            if (bdSysFlag && cUsers == 1)
            {
                oldUsersCheckBox.Enabled = true;
                DBControl sysCon = new DBControl("bd_system");
                string selOldUsersQuery = sysCon.GetSQLCommand("users_in_old_db");
                sysCon.MyConn.Open();
                MySqlDataAdapter sysAd = new MySqlDataAdapter(selOldUsersQuery, sysCon.MyConn);
                sysAd.Fill(oldEntitiesDS.Tables["users"]);
                sysCon.Dispose();
                oldUsersCount = oldEntitiesDS.Tables["users"].Rows.Count;
                oldUsersCheckBox.Enabled = (oldUsersCount > 0) ? true : false;
            } else
            {
                oldUsersCheckBox.Enabled = false;
            }
            if (bdBarabanFlag && cBarabans == 0)
            {
                oldDbBarabansCheckBox.Enabled = true;
                DBControl brbCon = new DBControl("bd_baraban");
                string selOldBarabanTypesQuery = brbCon.GetSQLCommand("barabans_in_old_db");
                brbCon.MyConn.Open();
                MySqlDataAdapter brbAd = new MySqlDataAdapter(selOldBarabanTypesQuery, brbCon.MyConn);
                brbAd.Fill(oldEntitiesDS.Tables["baraban_types"]);
                brbCon.MyConn.Close();
                brbCon.Dispose();
                oldBarabansCount = oldEntitiesDS.Tables["baraban_types"].Rows.Count;
                oldDbBarabansCheckBox.Enabled = (oldBarabansCount > 0) ? true : false;
            }
            else
            {
                oldDbBarabansCheckBox.Enabled = false;
            }
            if (bdCableFlag && cCables == 0)
            {
                oldDbCablesCheckBox.Enabled = true;
                DBControl cblCon = new DBControl("bd_cable");
                string selCables = cblCon.GetSQLCommand("cables_in_old_db");
                string selStructures = cblCon.GetSQLCommand("structures_in_old_db");
                string selMeasuredParams = cblCon.GetSQLCommand("measured_parameters_in_old_db");
                string selFreqRanges = cblCon.GetSQLCommand("frequency_ranges_in_old_db");
                cblCon.MyConn.Open();
                MySqlDataAdapter cAd = new MySqlDataAdapter(selCables, cblCon.MyConn);
                MySqlDataAdapter sAd = new MySqlDataAdapter(selStructures, cblCon.MyConn);
                MySqlDataAdapter mpAd = new MySqlDataAdapter(selMeasuredParams, cblCon.MyConn);
                MySqlDataAdapter frAd = new MySqlDataAdapter(selFreqRanges, cblCon.MyConn);
                cAd.Fill(oldEntitiesDS.Tables["cables"]);
                sAd.Fill(oldEntitiesDS.Tables["cable_structures"]);
                mpAd.Fill(oldEntitiesDS.Tables["measured_params"]);
                frAd.Fill(oldEntitiesDS.Tables["freq_ranges"]);
                cblCon.MyConn.Close();
                cblCon.Dispose();
                oldCablesCount = oldEntitiesDS.Tables["cables"].Rows.Count;// + oldEntitiesDS.Tables["measured_params"].Rows.Count +  oldEntitiesDS.Tables["cable_structures"].Rows.Count + oldEntitiesDS.Tables["freq_ranges"].Rows.Count;
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
            checkMigrateButEnable();
        }

        private void migrateSelected_Click(object sender, EventArgs e)
        {
            bool u = (oldUsersCheckBox.Enabled && oldUsersCheckBox.Checked);
            bool b = (oldDbBarabansCheckBox.Enabled && oldDbBarabansCheckBox.Checked);
            bool c = (oldDbCablesCheckBox.Enabled && oldDbCablesCheckBox.Checked);
            if (u) pbScope += oldUsersCount;
            if (b) pbScope += oldBarabansCount;
            if (c) pbScope += oldCablesCount;
            progressBar1.Maximum = pbScope;
            progressBar1.Minimum = 1;
            progressBar1.Step = 1;
            if (u) migrateOldUsers();
            if (b) migrateOldBarabans();
            if (c) migrateOldCables();
        }

        private void migrateOldCables()
        {
            mySql = new DBControl(Properties.Settings.Default.dbName);
            DataRowCollection c = oldEntitiesDS.Tables["cables"].Rows;
            DataRowCollection s = oldEntitiesDS.Tables["cables_structures"].Rows;
            DataRowCollection f = oldEntitiesDS.Tables["freq_ranges"].Rows;
            DataRowCollection m = oldEntitiesDS.Tables["measured_params"].Rows;
            string rowQuery = mySql.GetSQLCommand("AddCableFromOldDB");
            mySql.MyConn.Open();
            rowQuery = mySql.GetSQLCommand("AddStructureFromOldDB");
            foreach (DataRow r in s)
            {
                string q = String.Format(rowQuery,
                                        r["id"],
                                        r["cable_id"],
                                        r["structure_type_id"],
                                        r["nominal_amount"],
                                        r["fact_amount"],
                                        r["lead_material_id"],
                                        r["lead_diameter"],
                                        r["isolation_material_id"],
                                        r["wave_resistance"],
                                        r["u_lead_lead"],
                                        r["u_lead_shield"],
                                        r["test_group_work_capacity"],
                                        r["dr_formula_id"],
                                        r["dr_bringing_formula_id"]
                                        );
                mySql.RunNoQuery(q);
                progressBar1.PerformStep();
            }

            mySql.MyConn.Close();
        }

        private void migrateOldBarabans()
        {
            mySql = new DBControl(Properties.Settings.Default.dbName);
            string rowQuery = mySql.GetSQLCommand("AddBarabanFromOldDB");
            DataRowCollection b_types = oldEntitiesDS.Tables["baraban_types"].Rows;
            mySql.MyConn.Open();
            foreach (DataRow r in b_types)
            {
                string q = String.Format(rowQuery, r["id"], r["name"], r["weight"].ToString().Replace(",", ".") );
                mySql.RunNoQuery(q);
                progressBar1.PerformStep();
                //MySqlCommand cmd = new MySqlCommand(q, mySql.MyConn);
                //cmd.ExecuteNonQuery();
            }
            mySql.MyConn.Close();
        }

        /// <summary>
        /// Производит миграцию пользоваетелей в новую бд с проверкой их наличия в ней
        /// </summary>
        private void migrateOldUsers()
        {
            mySql = new DBControl(Properties.Settings.Default.dbName);
            //newUsersArr.IsFixedSize = false;
            DataRowCollection u = oldEntitiesDS.Tables["users"].Rows;
            string result_query = "";
            string s = "({0}, \"{1}\", \"{2}\", \"{3}\", {4}, {5}, \"{6}\", {7})";
            foreach (DataRow r in u)
            {
               
                if (result_query.Length > 0) result_query += ", ";
                result_query += String.Format(s, r["id"].ToString(), r["last_name"].ToString(), r["name"].ToString(), r["third_name"].ToString(), r["employee_number"].ToString(), r["role_id"].ToString(), "123".ToString(), r["is_active"].ToString());

                progressBar1.PerformStep();
            }
            result_query = "INSERT IGNORE INTO users VALUES " + result_query;
            mySql.MyConn.Open();
            mySql.RunNoQuery(result_query);
            mySql.MyConn.Close();
            //test.Text = query;
        }

        private void cancelBut_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
