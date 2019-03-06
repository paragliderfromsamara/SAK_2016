using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Globalization;
using System.Threading;
using NormaMeasure.DBControl.SAC.DBEntities;
using NormaMeasure.DBControl.SAC.Forms;

namespace NormaMeasure.SAC_APP
{

    public partial class mainForm : Form
    {
        public bool isTestApp = Properties.Settings.Default.isTestApp;
        public string user_type = "undefined";
        public string user_id = "undefined";
        public manualTestForm manualTestForm = null;
        public autoTestForm autoTestForm = null;
        public dbCablesForm dbCablesForm = null;
        public dbTestsForm dbTestForm = null;
        public dbUsersForm dbUsersForm = null;

        private UsersForm usersForm;
        private CablesListForm cablesListForm;
        private BarabanTypesControlForm barabanTypesControlForm;


        public dbForms.oldDbDataMigration oldDbDataMigrationForm = null;

        public mainForm()
        {
            InitializeComponent();
            if (isTestApp) this.Text += " (Тестовый режим)";
            Thread.CurrentThread.CurrentCulture = new CultureInfo("my");
            Thread.CurrentThread.CurrentCulture.NumberFormat.NumberDecimalSeparator = ".";
        }
        //Управление дочерними окнами
        private void autoTestToolStripMenuItem_Click(object sender, EventArgs e)
        {
            autoTestForm = new autoTestForm(this);
            autoTestForm.MdiParent = this;
            switchMenuStripItems(false);
            autoTestForm.Show();
        }

        private void manualTestToolStripMenuItem_Click(object sender, EventArgs e)
        {
            manualTestForm = new manualTestForm(this);
            manualTestForm.MdiParent = this;
            switchMenuStripItems(false);
            manualTestForm.Show();
        }

        public void switchMenuStripItems(bool v)
        {
           autoTestToolStripMenuItem.Enabled = manualTestToolStripMenuItem.Enabled = testsToolStripMenuItem.Enabled = dbControlToolStripMenuItem.Enabled = v;
        }

        private void mainForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (this.manualTestForm != null)
            {
                this.manualTestForm.closeThread();
            }
        }

        private void dbUsersToolStripMenuItem_Click(object sender, EventArgs e)
        {
            usersForm = new UsersForm();
            initChildForm(usersForm);
        }

        private void mainForm_Load(object sender, EventArgs e)
        {
            //dbSakModels.dataMigration dm = new dbSakModels.dataMigration();
            //dm.createTables();
            //DBSACTablesMigration dbMigartion = new DBSACTablesMigration();
           // dbMigartion.InitDataBase();
            NormaMeasure.DBControl.SAC.DBEntities.CableOld c = new NormaMeasure.DBControl.SAC.DBEntities.CableOld();
            //string cols = DBSACTablesMigration.DocumentsTable.SelectQuery;

            //MessageBox.Show(cols);
            Signin signForm = new Signin();
            DialogResult result = signForm.ShowDialog();
            if (result == System.Windows.Forms.DialogResult.Cancel) this.Close();
            else if (result == System.Windows.Forms.DialogResult.OK)
            {
                sesUserName.Text = String.Format("{0} {1}.{2}.", signForm.userLastName, signForm.userFirstName[0], signForm.userThirdName[0]);
                sesTabNum.Text = "Таб.номер: " + signForm.userTabNum;
                sesRole.Text = "Должность: " + signForm.userRole;
                user_type = signForm.userRole;
                user_id = signForm.userId;
                signForm.Dispose();
                checkTabsPermission();
            }
            
        }

        private void checkTabsPermission()
        {
            UserGrants grants = new UserGrants(user_type);
            dbUsersToolStripMenuItem.Enabled = grants.userCouldSeeUserDb();
            dbBarabanToolStripMenuItem.Enabled = grants.userCouldSeeBarabansDb();
            dbCableToolStripMenuItem.Enabled = grants.userCouldSeeCablesDb();
            dbTestToolStripMenuItem.Enabled = grants.userCouldSeeTestDb();
            oldDbMigrationStripMenuItem.Enabled = grants.userCouldMigrateDataFromOldDB();
        }

        private void dbBarabanToolStripMenuItem_Click(object sender, EventArgs e)
        {
            barabanTypesControlForm = new BarabanTypesControlForm();
            initChildForm(barabanTypesControlForm);
        }

        private void dbCableToolStripMenuItem_Click(object sender, EventArgs e)
        {
            cablesListForm = new CablesListForm();
            initChildForm(cablesListForm);
        }

        private void initChildForm(Form f)
        {
            f.MdiParent = this;
            f.Shown += new EventHandler(ChildForm_Shown);
            f.FormClosed += new FormClosedEventHandler(ChildForm_Closed);
            f.Show();
        }


        private void ChildForm_Shown(object sender, EventArgs e)
        {
            switchMenuStripItems(false);
        }
        private void ChildForm_Closed(object sender, EventArgs e)
        {
            switchMenuStripItems(true);
        }



        private void oldDbMigrationStripMenuItem_Click(object sender, EventArgs e)
        {
            oldDbDataMigrationForm = new dbForms.oldDbDataMigration(this);
            oldDbDataMigrationForm.Show();
        }
    }
}
