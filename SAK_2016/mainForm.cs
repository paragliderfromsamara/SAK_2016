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
using NormaMeasure.DBControl.DBNormaMeasure.DBEntities;
using NormaMeasure.DBControl.DBNormaMeasure.Forms;
using NormaMeasure.MeasureControl.SACMeasureForms;
using NormaMeasure.Devices.SAC;



namespace NormaMeasure.SAC_APP
{

    public partial class mainForm : Form
    {
        /// <summary>
        /// 
        /// </summary>
        public bool isTestApp = Properties.Settings.Default.isTestApp;
        /// <summary>
        /// 
        /// </summary>
        public string user_type = "undefined";
        /// <summary>
        /// 
        /// </summary>
        public string user_id = "undefined";

        /// <summary>
        /// 
        /// </summary>
        public SACCableTestForm CableTestForm = null;
        /// <summary>
        /// 
        /// </summary>
        public SACHandMeasureForm HandMeasureForm = null;


        private UsersList usersForm;
        private CablesListForm cablesListForm;
        private BarabanTypesControlForm barabanTypesControlForm;

        private SAC_Device sacDevice;

        /// <summary>
        /// 
        /// </summary>
        public mainForm()
        {
            InitializeComponent();
            InitCulture();


            if (isTestApp) this.Text += " (Тестовый режим)";
        }

        private void InitSAC()
        {
            sacDevice = new SAC_Device();
            CPSStatusLabel.Text = "Крейт не найден";
            sacDevice.OnCPSFound += SacDevice_OnCPSFound;
            sacDevice.OnCPSLost += SacDevice_OnCPSLost;
            sacDevice.OnTableFound += SacDevice_OnTableFound;
            sacDevice.OnTableLost += SacDevice_OnTableLost;
            sacDevice.Find();


        }

        private void SacDevice_OnTableLost(SAC_Device sac, SACTable table)
        {
            tableToolStripLabel.Text = "Нет связи со столом";
        }

        private void SacDevice_OnTableFound(SAC_Device sac, SACTable table)
        {
            tableToolStripLabel.Text = $"Стол №{table.DeviceId} ({table.PortName})";
        }

        private void CheckSACLink()
        {
            //throw new NotImplementedException();
            sacDevice.Find();
        }
        

        private void SacDevice_OnCPSLost(SAC_Device sac, SACCPS cps)
        {
            CPSStatusLabel.Text = "Крейт не найден";
        }

        private void SacDevice_OnCPSFound(SAC_Device sac, SACCPS cps)
        {
            CPSStatusLabel.Text = $"Крейт №{cps.DeviceId}";
        }

        private void InitCulture()
        {
            Thread.CurrentThread.CurrentCulture = new CultureInfo("my");
            Thread.CurrentThread.CurrentCulture.NumberFormat.NumberDecimalSeparator = ".";
        }

        //Управление дочерними окнами
        private void autoTestToolStripMenuItem_Click(object sender, EventArgs e)
        {
            CableTestForm = new SACCableTestForm(sacDevice);
            if (!CableTestForm.IsDisposed) initChildForm(CableTestForm);
        }

        private void manualTestToolStripMenuItem_Click(object sender, EventArgs e)
        {
            HandMeasureForm = new SACHandMeasureForm(sacDevice);
            if (!HandMeasureForm.IsDisposed) initChildForm(HandMeasureForm);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="v"></param>
        public void switchMenuStripItems(bool v)
        {
           autoTestToolStripMenuItem.Enabled = manualTestToolStripMenuItem.Enabled = testsToolStripMenuItem.Enabled = dbControlToolStripMenuItem.Enabled = v;
        }

        private void dbUsersToolStripMenuItem_Click(object sender, EventArgs e)
        {
            usersForm = new UsersList();
            initChildForm(usersForm);
        }

        private void mainForm_Load(object sender, EventArgs e)
        {
            //dbSakModels.dataMigration dm = new dbSakModels.dataMigration();
            //dm.createTables();
            //DBSACTablesMigration dbMigartion = new DBSACTablesMigration();
           // dbMigartion.InitDataBase();
           // NormaMeasure.DBControl.DBNormaMeasure.DBEntities.CableOld c = new NormaMeasure.DBControl.DBNormaMeasure.DBEntities.CableOld();
            //string cols = DBSACTablesMigration.DocumentsTable.SelectQuery;

            //MessageBox.Show(cols);

            initLogin();
        }

        private void initLogin()
        {
            Signin signForm = new Signin();
            DialogResult result = signForm.ShowDialog();
            if (result == System.Windows.Forms.DialogResult.Cancel) this.Close();
            else if (result == System.Windows.Forms.DialogResult.OK)
            {
                sesUserName.Text = signForm.sUser.FullNameShort;
                sesTabNum.Text = "Таб.номер: " + signForm.sUser.EmployeeNumber;
                sesRole.Text = "Должность: " + signForm.sUser.Role.UserRoleName;
                user_type = signForm.sUser.Role.UserRoleName;
                user_id = signForm.sUser.UserId.ToString();
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



        private void CPSStatusLabel_Click(object sender, EventArgs e)
        {
            sacDevice.CentralSysPult.Find();
            //CheckSACLink();
        }

        private void tableToolStripLabel_Click(object sender, EventArgs e)
        {
           if (!sacDevice.table.IsConnected) sacDevice.table.Find();
           else tableToolStripLabel.Text = $"Стол №{sacDevice.table.DeviceId} ({sacDevice.table.PortName})";
        }

        private void Table_OnDataReceive(Devices.DeviceBaseOld device)
        {
            MessageBox.Show("Принят номер стола");
        }

        private void mainForm_FormClosed(object sender, FormClosedEventArgs e)
        {
            sacDevice?.Dispose();
        }

        private void mainForm_Shown(object sender, EventArgs e)
        {
            InitSAC();
        }

        private void toolStripStatusLabel1_Click(object sender, EventArgs e)
        {
            TableMap_testForm map = new TableMap_testForm();
            map.MdiParent = this;
            map.Show();
        }
    }
}
