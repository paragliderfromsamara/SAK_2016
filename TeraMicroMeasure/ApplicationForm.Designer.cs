namespace TeraMicroMeasure
{
    partial class ApplicationForm
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
            this.menuStrip = new System.Windows.Forms.MenuStrip();
            this.settingsToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.настройкиСервераToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.базыДанныхToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.пользователиToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.кабелиToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.испытанияToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.testLinesToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.bottomStatusMenu = new System.Windows.Forms.StatusStrip();
            this.clientCounterStatus = new System.Windows.Forms.ToolStripStatusLabel();
            this.connectionStatusLabel = new System.Windows.Forms.ToolStripStatusLabel();
            this.failureCounter = new System.Windows.Forms.ToolStripStatusLabel();
            this.disconnectedDevices = new System.Windows.Forms.ToolStripStatusLabel();
            this.topPanel = new System.Windows.Forms.Panel();
            this.switchConnectToServerButton = new System.Windows.Forms.Button();
            this.button1 = new System.Windows.Forms.Button();
            this.menuStrip.SuspendLayout();
            this.bottomStatusMenu.SuspendLayout();
            this.topPanel.SuspendLayout();
            this.SuspendLayout();
            // 
            // menuStrip
            // 
            this.menuStrip.BackColor = System.Drawing.SystemColors.GradientInactiveCaption;
            this.menuStrip.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.menuStrip.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.settingsToolStripMenuItem,
            this.базыДанныхToolStripMenuItem,
            this.testLinesToolStripMenuItem});
            this.menuStrip.Location = new System.Drawing.Point(0, 0);
            this.menuStrip.MinimumSize = new System.Drawing.Size(600, 0);
            this.menuStrip.Name = "menuStrip";
            this.menuStrip.Size = new System.Drawing.Size(1496, 25);
            this.menuStrip.TabIndex = 0;
            this.menuStrip.Text = "menuStrip1";
            // 
            // settingsToolStripMenuItem
            // 
            this.settingsToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.настройкиСервераToolStripMenuItem});
            this.settingsToolStripMenuItem.Name = "settingsToolStripMenuItem";
            this.settingsToolStripMenuItem.Size = new System.Drawing.Size(83, 21);
            this.settingsToolStripMenuItem.Text = "Настройки";
            // 
            // настройкиСервераToolStripMenuItem
            // 
            this.настройкиСервераToolStripMenuItem.Name = "настройкиСервераToolStripMenuItem";
            this.настройкиСервераToolStripMenuItem.Size = new System.Drawing.Size(193, 22);
            this.настройкиСервераToolStripMenuItem.Text = "Настройки сервера";
            this.настройкиСервераToolStripMenuItem.Click += new System.EventHandler(this.serverStatusLabel_Click);
            // 
            // базыДанныхToolStripMenuItem
            // 
            this.базыДанныхToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.пользователиToolStripMenuItem,
            this.кабелиToolStripMenuItem,
            this.испытанияToolStripMenuItem});
            this.базыДанныхToolStripMenuItem.Name = "базыДанныхToolStripMenuItem";
            this.базыДанныхToolStripMenuItem.Size = new System.Drawing.Size(96, 21);
            this.базыДанныхToolStripMenuItem.Text = "Базы данных";
            // 
            // пользователиToolStripMenuItem
            // 
            this.пользователиToolStripMenuItem.Name = "пользователиToolStripMenuItem";
            this.пользователиToolStripMenuItem.Size = new System.Drawing.Size(161, 22);
            this.пользователиToolStripMenuItem.Text = "Пользователи";
            this.пользователиToolStripMenuItem.Click += new System.EventHandler(this.usersToolStripMenuItem_Click);
            // 
            // кабелиToolStripMenuItem
            // 
            this.кабелиToolStripMenuItem.Name = "кабелиToolStripMenuItem";
            this.кабелиToolStripMenuItem.Size = new System.Drawing.Size(161, 22);
            this.кабелиToolStripMenuItem.Text = "Кабели";
            this.кабелиToolStripMenuItem.Click += new System.EventHandler(this.cablesToolStripMenuItem_Click);
            // 
            // испытанияToolStripMenuItem
            // 
            this.испытанияToolStripMenuItem.Name = "испытанияToolStripMenuItem";
            this.испытанияToolStripMenuItem.Size = new System.Drawing.Size(161, 22);
            this.испытанияToolStripMenuItem.Text = "Испытания";
            this.испытанияToolStripMenuItem.Click += new System.EventHandler(this.testsToolStripMenuItem_Click);
            // 
            // testLinesToolStripMenuItem
            // 
            this.testLinesToolStripMenuItem.Name = "testLinesToolStripMenuItem";
            this.testLinesToolStripMenuItem.Size = new System.Drawing.Size(152, 21);
            this.testLinesToolStripMenuItem.Text = "Испытательные линии";
            // 
            // bottomStatusMenu
            // 
            this.bottomStatusMenu.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(26)))), ((int)(((byte)(69)))), ((int)(((byte)(128)))));
            this.bottomStatusMenu.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.clientCounterStatus,
            this.connectionStatusLabel,
            this.failureCounter,
            this.disconnectedDevices});
            this.bottomStatusMenu.Location = new System.Drawing.Point(0, 873);
            this.bottomStatusMenu.Name = "bottomStatusMenu";
            this.bottomStatusMenu.Size = new System.Drawing.Size(1496, 22);
            this.bottomStatusMenu.TabIndex = 2;
            this.bottomStatusMenu.Text = "statusStrip1";
            // 
            // clientCounterStatus
            // 
            this.clientCounterStatus.ForeColor = System.Drawing.SystemColors.ControlLight;
            this.clientCounterStatus.Name = "clientCounterStatus";
            this.clientCounterStatus.Size = new System.Drawing.Size(71, 17);
            this.clientCounterStatus.Text = "Клиентов: 0";
            // 
            // connectionStatusLabel
            // 
            this.connectionStatusLabel.ForeColor = System.Drawing.SystemColors.ControlLight;
            this.connectionStatusLabel.Name = "connectionStatusLabel";
            this.connectionStatusLabel.Size = new System.Drawing.Size(205, 17);
            this.connectionStatusLabel.Text = "IP адрес: 192.168.100.220 Порт: 16000";
            this.connectionStatusLabel.Click += new System.EventHandler(this.serverStatusLabel_Click);
            // 
            // failureCounter
            // 
            this.failureCounter.ForeColor = System.Drawing.Color.FloralWhite;
            this.failureCounter.Name = "failureCounter";
            this.failureCounter.Size = new System.Drawing.Size(124, 17);
            this.failureCounter.Text = "Отвалов от сервера 0";
            // 
            // disconnectedDevices
            // 
            this.disconnectedDevices.ForeColor = System.Drawing.SystemColors.ButtonHighlight;
            this.disconnectedDevices.Name = "disconnectedDevices";
            this.disconnectedDevices.Size = new System.Drawing.Size(93, 17);
            this.disconnectedDevices.Text = "отключено раз:";
            // 
            // topPanel
            // 
            this.topPanel.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.topPanel.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(16)))), ((int)(((byte)(49)))), ((int)(((byte)(99)))));
            this.topPanel.Controls.Add(this.switchConnectToServerButton);
            this.topPanel.Controls.Add(this.button1);
            this.topPanel.Dock = System.Windows.Forms.DockStyle.Top;
            this.topPanel.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.topPanel.Location = new System.Drawing.Point(0, 25);
            this.topPanel.Name = "topPanel";
            this.topPanel.Size = new System.Drawing.Size(1496, 50);
            this.topPanel.TabIndex = 3;
            // 
            // switchConnectToServerButton
            // 
            this.switchConnectToServerButton.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(21)))), ((int)(((byte)(179)))), ((int)(((byte)(9)))));
            this.switchConnectToServerButton.Dock = System.Windows.Forms.DockStyle.Right;
            this.switchConnectToServerButton.FlatAppearance.BorderSize = 0;
            this.switchConnectToServerButton.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.switchConnectToServerButton.Font = new System.Drawing.Font("Segoe MDL2 Assets", 15.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.switchConnectToServerButton.ForeColor = System.Drawing.SystemColors.ControlLight;
            this.switchConnectToServerButton.Location = new System.Drawing.Point(1446, 0);
            this.switchConnectToServerButton.Name = "switchConnectToServerButton";
            this.switchConnectToServerButton.Size = new System.Drawing.Size(50, 50);
            this.switchConnectToServerButton.TabIndex = 3;
            this.switchConnectToServerButton.Text = "";
            this.switchConnectToServerButton.UseVisualStyleBackColor = false;
            this.switchConnectToServerButton.Click += new System.EventHandler(this.switchConnectToServerButton_Click);
            // 
            // button1
            // 
            this.button1.AccessibleDescription = "fdfdfd";
            this.button1.BackColor = System.Drawing.SystemColors.HotTrack;
            this.button1.Dock = System.Windows.Forms.DockStyle.Left;
            this.button1.FlatAppearance.BorderSize = 0;
            this.button1.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.button1.Font = new System.Drawing.Font("Segoe MDL2 Assets", 15.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.button1.ForeColor = System.Drawing.Color.Cornsilk;
            this.button1.Location = new System.Drawing.Point(0, 0);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(50, 50);
            this.button1.TabIndex = 2;
            this.button1.Text = "";
            this.button1.UseVisualStyleBackColor = false;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // ApplicationForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.ClientSize = new System.Drawing.Size(1496, 895);
            this.Controls.Add(this.topPanel);
            this.Controls.Add(this.bottomStatusMenu);
            this.Controls.Add(this.menuStrip);
            this.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.IsMdiContainer = true;
            this.MinimumSize = new System.Drawing.Size(700, 500);
            this.Name = "ApplicationForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Form1";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.MainForm_FormClosing);
            this.menuStrip.ResumeLayout(false);
            this.menuStrip.PerformLayout();
            this.bottomStatusMenu.ResumeLayout(false);
            this.bottomStatusMenu.PerformLayout();
            this.topPanel.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.MenuStrip menuStrip;
        private System.Windows.Forms.ToolStripMenuItem settingsToolStripMenuItem;
        private System.Windows.Forms.StatusStrip bottomStatusMenu;
        private System.Windows.Forms.Panel topPanel;
        private System.Windows.Forms.ToolStripStatusLabel clientCounterStatus;
        private System.Windows.Forms.ToolStripStatusLabel connectionStatusLabel;
        private System.Windows.Forms.ToolStripMenuItem базыДанныхToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem пользователиToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem кабелиToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem испытанияToolStripMenuItem;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.ToolStripMenuItem настройкиСервераToolStripMenuItem;
        private System.Windows.Forms.Button switchConnectToServerButton;
        private System.Windows.Forms.ToolStripMenuItem testLinesToolStripMenuItem;
        private System.Windows.Forms.ToolStripStatusLabel failureCounter;
        private System.Windows.Forms.ToolStripStatusLabel disconnectedDevices;
    }
}

