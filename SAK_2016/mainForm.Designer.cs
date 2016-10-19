namespace SAK_2016
{
    partial class mainForm
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.menuStrip1 = new System.Windows.Forms.MenuStrip();
            this.testsToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.autoTestToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.manualTestToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.dbControlToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.dbTestToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.dbUsersToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.dbCableToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.dbBarabanToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.oldDbMigrationStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.statusStrip1 = new System.Windows.Forms.StatusStrip();
            this.sesUserName = new System.Windows.Forms.ToolStripStatusLabel();
            this.sesTabNum = new System.Windows.Forms.ToolStripStatusLabel();
            this.sesRole = new System.Windows.Forms.ToolStripStatusLabel();
            this.menuStrip1.SuspendLayout();
            this.statusStrip1.SuspendLayout();
            this.SuspendLayout();
            // 
            // menuStrip1
            // 
            this.menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.testsToolStripMenuItem,
            this.dbControlToolStripMenuItem});
            this.menuStrip1.Location = new System.Drawing.Point(0, 0);
            this.menuStrip1.Name = "menuStrip1";
            this.menuStrip1.Size = new System.Drawing.Size(981, 24);
            this.menuStrip1.TabIndex = 1;
            this.menuStrip1.Text = "menuStrip1";
            // 
            // testsToolStripMenuItem
            // 
            this.testsToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.autoTestToolStripMenuItem,
            this.manualTestToolStripMenuItem});
            this.testsToolStripMenuItem.Name = "testsToolStripMenuItem";
            this.testsToolStripMenuItem.Size = new System.Drawing.Size(81, 20);
            this.testsToolStripMenuItem.Text = "Испытания";
            // 
            // autoTestToolStripMenuItem
            // 
            this.autoTestToolStripMenuItem.Name = "autoTestToolStripMenuItem";
            this.autoTestToolStripMenuItem.ShortcutKeys = System.Windows.Forms.Keys.F2;
            this.autoTestToolStripMenuItem.Size = new System.Drawing.Size(247, 22);
            this.autoTestToolStripMenuItem.Text = "Автоматические испытания";
            this.autoTestToolStripMenuItem.Click += new System.EventHandler(this.autoTestToolStripMenuItem_Click);
            // 
            // manualTestToolStripMenuItem
            // 
            this.manualTestToolStripMenuItem.Name = "manualTestToolStripMenuItem";
            this.manualTestToolStripMenuItem.ShortcutKeys = System.Windows.Forms.Keys.F3;
            this.manualTestToolStripMenuItem.Size = new System.Drawing.Size(247, 22);
            this.manualTestToolStripMenuItem.Text = "Ручные испытания";
            this.manualTestToolStripMenuItem.Click += new System.EventHandler(this.manualTestToolStripMenuItem_Click);
            // 
            // dbControlToolStripMenuItem
            // 
            this.dbControlToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.dbTestToolStripMenuItem,
            this.dbUsersToolStripMenuItem,
            this.dbCableToolStripMenuItem,
            this.dbBarabanToolStripMenuItem,
            this.oldDbMigrationStripMenuItem});
            this.dbControlToolStripMenuItem.Name = "dbControlToolStripMenuItem";
            this.dbControlToolStripMenuItem.Size = new System.Drawing.Size(163, 20);
            this.dbControlToolStripMenuItem.Text = "Управление базой данных";
            // 
            // dbTestToolStripMenuItem
            // 
            this.dbTestToolStripMenuItem.Name = "dbTestToolStripMenuItem";
            this.dbTestToolStripMenuItem.Size = new System.Drawing.Size(239, 22);
            this.dbTestToolStripMenuItem.Text = "Испытания";
            // 
            // dbUsersToolStripMenuItem
            // 
            this.dbUsersToolStripMenuItem.Name = "dbUsersToolStripMenuItem";
            this.dbUsersToolStripMenuItem.Size = new System.Drawing.Size(239, 22);
            this.dbUsersToolStripMenuItem.Text = "Пользователи";
            this.dbUsersToolStripMenuItem.Click += new System.EventHandler(this.dbUsersToolStripMenuItem_Click);
            // 
            // dbCableToolStripMenuItem
            // 
            this.dbCableToolStripMenuItem.Name = "dbCableToolStripMenuItem";
            this.dbCableToolStripMenuItem.Size = new System.Drawing.Size(239, 22);
            this.dbCableToolStripMenuItem.Text = "Кабели";
            this.dbCableToolStripMenuItem.Click += new System.EventHandler(this.dbCableToolStripMenuItem_Click);
            // 
            // dbBarabanToolStripMenuItem
            // 
            this.dbBarabanToolStripMenuItem.Name = "dbBarabanToolStripMenuItem";
            this.dbBarabanToolStripMenuItem.Size = new System.Drawing.Size(239, 22);
            this.dbBarabanToolStripMenuItem.Text = "Типы барабанов";
            this.dbBarabanToolStripMenuItem.Click += new System.EventHandler(this.dbBarabanToolStripMenuItem_Click);
            // 
            // oldDbMigrationStripMenuItem
            // 
            this.oldDbMigrationStripMenuItem.Name = "oldDbMigrationStripMenuItem";
            this.oldDbMigrationStripMenuItem.Size = new System.Drawing.Size(239, 22);
            this.oldDbMigrationStripMenuItem.Text = "Загрузка данных из старой БД";
            this.oldDbMigrationStripMenuItem.Click += new System.EventHandler(this.oldDbMigrationStripMenuItem_Click);
            // 
            // statusStrip1
            // 
            this.statusStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.sesUserName,
            this.sesTabNum,
            this.sesRole});
            this.statusStrip1.Location = new System.Drawing.Point(0, 437);
            this.statusStrip1.Name = "statusStrip1";
            this.statusStrip1.Size = new System.Drawing.Size(981, 22);
            this.statusStrip1.TabIndex = 3;
            this.statusStrip1.Text = "statusStrip1";
            // 
            // sesUserName
            // 
            this.sesUserName.Name = "sesUserName";
            this.sesUserName.Size = new System.Drawing.Size(85, 17);
            this.sesUserName.Text = "Фамилия И.О.";
            // 
            // sesTabNum
            // 
            this.sesTabNum.Name = "sesTabNum";
            this.sesTabNum.Size = new System.Drawing.Size(108, 17);
            this.sesTabNum.Text = "Табельный номер";
            // 
            // sesRole
            // 
            this.sesRole.Name = "sesRole";
            this.sesRole.Size = new System.Drawing.Size(69, 17);
            this.sesRole.Text = "Должность";
            // 
            // mainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(981, 459);
            this.Controls.Add(this.statusStrip1);
            this.Controls.Add(this.menuStrip1);
            this.IsMdiContainer = true;
            this.MainMenuStrip = this.menuStrip1;
            this.Name = "mainForm";
            this.Text = "САК 2016";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.mainForm_FormClosing);
            this.Load += new System.EventHandler(this.mainForm_Load);
            this.menuStrip1.ResumeLayout(false);
            this.menuStrip1.PerformLayout();
            this.statusStrip1.ResumeLayout(false);
            this.statusStrip1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.MenuStrip menuStrip1;
        private System.Windows.Forms.ToolStripMenuItem testsToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem autoTestToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem manualTestToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem dbControlToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem dbUsersToolStripMenuItem;
        private System.Windows.Forms.StatusStrip statusStrip1;
        private System.Windows.Forms.ToolStripStatusLabel sesUserName;
        private System.Windows.Forms.ToolStripStatusLabel sesRole;
        private System.Windows.Forms.ToolStripStatusLabel sesTabNum;
        private System.Windows.Forms.ToolStripMenuItem dbTestToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem dbCableToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem dbBarabanToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem oldDbMigrationStripMenuItem;
    }
}

