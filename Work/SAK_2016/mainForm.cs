﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace SAK_2016
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
        public dbBarabansForm dbBarabansForm = null;

        public mainForm()
        {
            InitializeComponent();
            if (isTestApp) this.Text += " (Тестовый режим)";
            //MessageBox.Show(user_type, "Check", MessageBoxButtons.OK);
            
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
            dbUsersForm = new dbUsersForm(this);
            dbUsersForm.MdiParent = this;
            switchMenuStripItems(false);
            dbUsersForm.Show();
        }

        private void mainForm_Load(object sender, EventArgs e)
        {
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
        }

        private void dbBarabanToolStripMenuItem_Click(object sender, EventArgs e)
        {
            dbBarabansForm = new dbBarabansForm(this);
            dbBarabansForm.MdiParent = this;
            switchMenuStripItems(false);
            dbBarabansForm.Show();
        }

        private void dbCableToolStripMenuItem_Click(object sender, EventArgs e)
        {
            dbCablesForm = new dbCablesForm(this);
            dbCablesForm.MdiParent = this;
            switchMenuStripItems(false);
            dbCablesForm.Show();
        }

    }
}
