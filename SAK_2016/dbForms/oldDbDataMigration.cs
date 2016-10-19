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
using System.Threading;

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
        private string baseStatus = "На данном устройстве обнаружены следующие БД от \nпредыдущих систем".ToString();
        public oldDbDataMigration(mainForm f)
        {
            mForm = f;
            mForm.switchMenuStripItems(false);
            InitializeComponent();
            loadOldDataSets();
            switchProgressBar(false);
        }

        private void oldDbDataMigration_FormClosing(object sender, FormClosingEventArgs e)
        {
            //e.Cancel = true;
            mForm.switchMenuStripItems(true);
            mForm.oldDbDataMigrationForm = null;
            mySql.Dispose();
            oldEntitiesDS.Dispose();
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
                mySql = new DBControl("bd_system");
                string selOldUsersQuery = mySql.GetSQLCommand("users_in_old_db");
                mySql.MyConn.Open();
                MySqlDataAdapter sysAd = new MySqlDataAdapter(selOldUsersQuery, mySql.MyConn);
                sysAd.Fill(oldEntitiesDS.Tables["users"]);
                mySql.Dispose();
                oldUsersCount = oldEntitiesDS.Tables["users"].Rows.Count;
                oldUsersCheckBox.Enabled = (oldUsersCount > 0) ? true : false;
            } else
            {
                oldUsersCheckBox.Enabled = false;
            }
            if (bdBarabanFlag && cBarabans == 0)
            {
                oldDbBarabansCheckBox.Enabled = true;
                mySql = new DBControl("bd_baraban");
                string selOldBarabanTypesQuery = mySql.GetSQLCommand("barabans_in_old_db");
                mySql.MyConn.Open();
                MySqlDataAdapter brbAd = new MySqlDataAdapter(selOldBarabanTypesQuery, mySql.MyConn);
                brbAd.Fill(oldEntitiesDS.Tables["baraban_types"]);
                mySql.MyConn.Close();
                mySql.Dispose();
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
                mySql = new DBControl("bd_cable");
                string selCables = mySql.GetSQLCommand("cables_in_old_db");
                string selStructures = mySql.GetSQLCommand("structures_in_old_db");
                string selMeasuredParams = mySql.GetSQLCommand("measured_parameters_in_old_db");
                string selFreqRanges = mySql.GetSQLCommand("frequency_ranges_in_old_db");
                mySql.MyConn.Open();
                MySqlDataAdapter cAd = new MySqlDataAdapter(selCables, mySql.MyConn);
                MySqlDataAdapter sAd = new MySqlDataAdapter(selStructures, mySql.MyConn);
                MySqlDataAdapter mpAd = new MySqlDataAdapter(selMeasuredParams, mySql.MyConn);
                MySqlDataAdapter frAd = new MySqlDataAdapter(selFreqRanges, mySql.MyConn);
                cAd.Fill(oldEntitiesDS.Tables["cables"]);
                sAd.Fill(oldEntitiesDS.Tables["cable_structures"]);
                mpAd.Fill(oldEntitiesDS.Tables["measured_params"]);
                frAd.Fill(oldEntitiesDS.Tables["freq_ranges"]);
                mySql.MyConn.Close();
                mySql.Dispose();
                oldCablesCount = oldEntitiesDS.Tables["cables"].Rows.Count + oldEntitiesDS.Tables["cable_structures"].Rows.Count + oldEntitiesDS.Tables["measured_params"].Rows.Count;//  +  oldEntitiesDS.Tables["cable_structures"].Rows.Count + oldEntitiesDS.Tables["freq_ranges"].Rows.Count;
                cAd.Dispose();
                sAd.Dispose();
                mpAd.Dispose();
                mySql.Dispose();
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
            checkMigrateButEnable();
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
            if (pbScope > 0) switchProgressBar(true); //включаем прогресс бар
            oldDBMigrationPBar.Maximum = pbScope;
            oldDBMigrationPBar.Minimum = 1;
            oldDBMigrationPBar.Step = 1;
            try
            {
                if (u) migrateOldUsers();
                if (b) migrateOldBarabans();
                if (c) migrateOldCables();
            }catch(DBException)
            {
                MessageBox.Show(String.Format("Произошла ошибка во время операции \"{0}\"", pBarStatus.Text), "Ошибка при миграции данных", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

            switchProgressBar(false); //выключаем прогресс бар
            oldDbBarabansCheckBox.Checked = oldDbCablesCheckBox.Checked = oldUsersCheckBox.Checked = false;
            loadOldDataSets();
        }

        private void migrateOldCables()
        {
            string cQueryMask, sQueryMask, mQueryMask, fQueryMask, cQuery, sQuery, mQuery, fQuery;
            DataRowCollection c, s, f, m;
            mySql = new DBControl(Properties.Settings.Default.dbName);
            c = oldEntitiesDS.Tables["cables"].Rows;
            s = oldEntitiesDS.Tables["cable_structures"].Rows;
            f = oldEntitiesDS.Tables["freq_ranges"].Rows;
            m = oldEntitiesDS.Tables["measured_params"].Rows;
            cQueryMask = "({0}, \"{1}\", \"{2}\", {3}, {4}, \"{5}\", {6}, \"{7}\", \"{8}\", {9}, {10}, {11})";
            sQueryMask = "({0}, {1}, {2}, {3}, {4}, {5}, {6}, {7}, {8}, {9}, {10}, {11}, {12}, {13})";
            mQueryMask = "({0}, {1}, {2}, {3}, {4}, {5}, {6}, {7}, {8})";
            fQueryMask = "({0}, {1}, {2}, {3})";
            cQuery = "";
            sQuery = "";
            mQuery = "";
            fQuery = "";
            //Создаём список для добавления в БД кабелей
            changePBStatus("Формирование списка кабелей");
            foreach (DataRow r in c)
            {
                if (cQuery.Length > 0) cQuery += ", ";
                cQuery += String.Format(cQueryMask,
                                        r["id"],
                                        r["name"],
                                        r["struct_name"],
                                        DBControl.setDbDefaultIfNull(r["build_length"].ToString()),
                                        r["document_id"],
                                        r["notes"],
                                        DBControl.setDbDefaultIfNull(r["linear_mass"].ToString()),
                                        r["code_okp"],
                                        r["code_kch"],
                                        DBControl.setDbDefaultIfNull(r["u_cover"].ToString()),
                                        DBControl.setDbDefaultIfNull(r["p_min"].ToString()),
                                        DBControl.setDbDefaultIfNull(r["p_max"].ToString())
                                        );
                oldDBMigrationPBar.PerformStep();
            }
            //Создаём список для добавления в БД структур кабелей
            changePBStatus("Формирование списка структур кабелей");
            foreach (DataRow r in s)
            {
                if (sQuery.Length > 0) sQuery += ", ";
                sQuery += String.Format(sQueryMask,
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
                
                oldDBMigrationPBar.PerformStep();
            }
            //Создаём список для добавления в БД значений измеряемых параметров
            changePBStatus("Формирование списка измеряемых параметров");
            foreach (DataRow r in m)
            {
                if (mQuery.Length > 0) mQuery += ", ";
                mQuery += String.Format(mQueryMask,
                                        r["cable_id"],
                                        r["cable_structure_id"],
                                        r["measured_parameter_type_id"],
                                        DBControl.setDbDefaultIfNull(r["min_val"].ToString()),
                                        DBControl.setDbDefaultIfNull(r["max_val"].ToString()),
                                        DBControl.setDbDefaultIfNull(r["bringing_length"].ToString()),
                                        DBControl.setDbDefaultIfNull(r["bringing_length_type_id"].ToString()),
                                        DBControl.setDbDefaultIfNull(r["percent"].ToString()),
                                        DBControl.setDbDefaultIfNull(r["frequency_range_id"].ToString())
                                        );
                oldDBMigrationPBar.PerformStep();
            }
            //Создаём список для добавления в БД диапазонов частот
            changePBStatus("Формирование списка диапазонов частот");
            foreach (DataRow r in f)
            {
                if (fQuery.Length > 0) fQuery += ", ";
                fQuery += String.Format(fQueryMask,
                                        r["id"],
                                        DBControl.setDbDefaultIfNull(r["freq_min"].ToString()),
                                        DBControl.setDbDefaultIfNull(r["freq_max"].ToString()),
                                        DBControl.setDbDefaultIfNull(r["freq_step"].ToString())
                                        );
                oldDBMigrationPBar.PerformStep();
            }
            cQuery = "INSERT IGNORE INTO cables VALUES " + cQuery;
            sQuery = "INSERT IGNORE INTO cable_structures VALUES " + sQuery;
            mQuery = "INSERT IGNORE INTO measured_parameter_values VALUES " + mQuery;
            fQuery = "INSERT IGNORE INTO frequency_ranges VALUES " + fQuery;
            changePBStatus("Подключение к " + Properties.Settings.Default.dbName);
            mySql.MyConn.Open();
            changePBStatus("Заполнение таблицы Кабелей");
            mySql.RunNoQuery(cQuery);
            changePBStatus("Заполнение таблицы Структур кабелей");
            mySql.RunNoQuery(sQuery);
            changePBStatus("Заполнение таблицы Значений измеряемых параметров");
            mySql.RunNoQuery(mQuery);
            changePBStatus("Заполнение таблицы диапазонов частот");
            mySql.RunNoQuery(fQuery);
            mySql.MyConn.Close();
            cQuery = sQuery = mQuery = fQuery = "";

        }

        private void migrateOldBarabans()
        {
            mySql = new DBControl(Properties.Settings.Default.dbName);
            
            DataRowCollection b_types = oldEntitiesDS.Tables["baraban_types"].Rows;
            string bQueryMask = "({0}, \"{1}\", {2})";
            string bQuery = "";
            changePBStatus("Формирование списка типов барабанов");
            foreach (DataRow r in b_types)
            {
                if (bQuery.Length > 0) bQuery += ", ";
                bQuery += String.Format(bQueryMask, 
                                                    r["id"], 
                                                    r["name"],
                                                    DBControl.setDbDefaultIfNull(r["weight"].ToString())
                                         );
                oldDBMigrationPBar.PerformStep();
            }
            bQuery = "INSERT IGNORE INTO baraban_types VALUES " + bQuery;
            mySql.MyConn.Open();
            changePBStatus("Заполнение таблицы типов барабанов");
            mySql.RunNoQuery(bQuery);
            mySql.MyConn.Close();
        }

        /// <summary>
        /// Производит миграцию пользоваетелей в новую бд с проверкой их наличия в ней
        /// </summary>
        private void migrateOldUsers()
        {
            mySql = new DBControl(Properties.Settings.Default.dbName);
            DataRowCollection u = oldEntitiesDS.Tables["users"].Rows;
            string result_query = "";
            string s = "({0}, \"{1}\", \"{2}\", \"{3}\", {4}, {5}, \"{6}\", {7})";
            changePBStatus("Формирование списка пользователей");
            foreach (DataRow r in u)
            {
               
                if (result_query.Length > 0) result_query += ", ";
                result_query += String.Format(s, r["id"].ToString(), r["last_name"].ToString(), r["name"].ToString(), r["third_name"].ToString(), DBControl.setDbDefaultIfNull(r["employee_number"].ToString()), r["role_id"].ToString(), "123".ToString(), r["is_active"].ToString());
                oldDBMigrationPBar.PerformStep();
            }
            result_query = "INSERT IGNORE INTO users VALUES " + result_query;
            changePBStatus("Подключение к " + Properties.Settings.Default.dbName);
            mySql.MyConn.Open();
            changePBStatus("Заполнение таблицы пользователей");
            mySql.RunNoQuery(result_query);
            mySql.MyConn.Close();
            //test.Text = query;
        }


        private void cancelBut_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        /// <summary>
        /// Переключает форму во время миграции
        /// </summary>
        /// <param name="s"></param>
        private void switchProgressBar(bool s)
        {
            migrateSelected.Enabled = cancelBut.Enabled = oldDbBarabansCheckBox.Visible = oldDbCablesCheckBox.Visible = oldUsersCheckBox.Visible = !s;
            oldDBMigrationPBar.Visible = pBarStatus.Visible = s;
            pBarStatus.Text = "";
            label1.Text = (s) ? "Подождите... Идёт загрузка данных" : baseStatus;
            label1.Update();
            this.ControlBox = !s;
        }
        /// <summary>
        /// Изменяет статус строки прогресс бара
        /// </summary>
        /// <param name="status"></param>
        private void changePBStatus(string status)
        {
            pBarStatus.Text = status;
            pBarStatus.Update();
            Thread.Sleep(500);
        }

    }
}
