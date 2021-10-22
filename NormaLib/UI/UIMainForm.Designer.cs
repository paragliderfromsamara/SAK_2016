namespace NormaLib.UI
{
    partial class UIMainForm
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(UIMainForm));
            this.childFormPanel = new System.Windows.Forms.Panel();
            this.panelTitleBar = new System.Windows.Forms.Panel();
            this.MinimizeAppButton = new System.Windows.Forms.Button();
            this.MaximizeAppButton = new System.Windows.Forms.Button();
            this.CloseAppButton = new System.Windows.Forms.Button();
            this.lblTitle = new System.Windows.Forms.Label();
            this.panelMenu = new System.Windows.Forms.Panel();
            this.panelHeader = new System.Windows.Forms.Panel();
            this.btnMeasure = new System.Windows.Forms.Button();
            this.btnDataBase = new System.Windows.Forms.Button();
            this.btnSettings = new System.Windows.Forms.Button();
            this.ContentPanel.SuspendLayout();
            this.panelTitleBar.SuspendLayout();
            this.panelMenu.SuspendLayout();
            this.SuspendLayout();
            // 
            // ContentPanel
            // 
            this.ContentPanel.Controls.Add(this.childFormPanel);
            this.ContentPanel.Controls.Add(this.panelTitleBar);
            this.ContentPanel.Controls.Add(this.panelMenu);
            this.ContentPanel.Size = new System.Drawing.Size(1373, 613);
            // 
            // childFormPanel
            // 
            this.childFormPanel.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(224)))), ((int)(((byte)(224)))), ((int)(((byte)(224)))));
            this.childFormPanel.Dock = System.Windows.Forms.DockStyle.Fill;
            this.childFormPanel.Location = new System.Drawing.Point(320, 98);
            this.childFormPanel.Margin = new System.Windows.Forms.Padding(4);
            this.childFormPanel.Name = "childFormPanel";
            this.childFormPanel.Size = new System.Drawing.Size(1053, 515);
            this.childFormPanel.TabIndex = 2;
            // 
            // panelTitleBar
            // 
            this.panelTitleBar.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(155)))), ((int)(((byte)(136)))));
            this.panelTitleBar.Controls.Add(this.MinimizeAppButton);
            this.panelTitleBar.Controls.Add(this.MaximizeAppButton);
            this.panelTitleBar.Controls.Add(this.CloseAppButton);
            this.panelTitleBar.Controls.Add(this.lblTitle);
            this.panelTitleBar.Dock = System.Windows.Forms.DockStyle.Top;
            this.panelTitleBar.Location = new System.Drawing.Point(320, 0);
            this.panelTitleBar.Margin = new System.Windows.Forms.Padding(4);
            this.panelTitleBar.Name = "panelTitleBar";
            this.panelTitleBar.Size = new System.Drawing.Size(1053, 98);
            this.panelTitleBar.TabIndex = 1;
            this.panelTitleBar.MouseDown += new System.Windows.Forms.MouseEventHandler(this.panelTitleBar_MouseDown);
            // 
            // MinimizeAppButton
            // 
            this.MinimizeAppButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.MinimizeAppButton.BackColor = System.Drawing.Color.Transparent;
            this.MinimizeAppButton.FlatAppearance.BorderSize = 0;
            this.MinimizeAppButton.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.MinimizeAppButton.Image = ((System.Drawing.Image)(resources.GetObject("MinimizeAppButton.Image")));
            this.MinimizeAppButton.Location = new System.Drawing.Point(881, 0);
            this.MinimizeAppButton.Margin = new System.Windows.Forms.Padding(4);
            this.MinimizeAppButton.Name = "MinimizeAppButton";
            this.MinimizeAppButton.Size = new System.Drawing.Size(53, 31);
            this.MinimizeAppButton.TabIndex = 3;
            this.MinimizeAppButton.UseVisualStyleBackColor = false;
            this.MinimizeAppButton.Click += new System.EventHandler(this.MinimizeAppButton_Click);
            // 
            // MaximizeAppButton
            // 
            this.MaximizeAppButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.MaximizeAppButton.BackColor = System.Drawing.Color.Transparent;
            this.MaximizeAppButton.FlatAppearance.BorderSize = 0;
            this.MaximizeAppButton.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.MaximizeAppButton.Image = ((System.Drawing.Image)(resources.GetObject("MaximizeAppButton.Image")));
            this.MaximizeAppButton.Location = new System.Drawing.Point(942, 0);
            this.MaximizeAppButton.Margin = new System.Windows.Forms.Padding(4);
            this.MaximizeAppButton.Name = "MaximizeAppButton";
            this.MaximizeAppButton.Size = new System.Drawing.Size(53, 31);
            this.MaximizeAppButton.TabIndex = 2;
            this.MaximizeAppButton.UseVisualStyleBackColor = false;
            this.MaximizeAppButton.Click += new System.EventHandler(this.MaximizeAppButton_Click);
            // 
            // CloseAppButton
            // 
            this.CloseAppButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.CloseAppButton.BackColor = System.Drawing.Color.Transparent;
            this.CloseAppButton.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Center;
            this.CloseAppButton.FlatAppearance.BorderSize = 0;
            this.CloseAppButton.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.CloseAppButton.Image = ((System.Drawing.Image)(resources.GetObject("CloseAppButton.Image")));
            this.CloseAppButton.Location = new System.Drawing.Point(999, 0);
            this.CloseAppButton.Margin = new System.Windows.Forms.Padding(4);
            this.CloseAppButton.Name = "CloseAppButton";
            this.CloseAppButton.Size = new System.Drawing.Size(53, 31);
            this.CloseAppButton.TabIndex = 1;
            this.CloseAppButton.UseVisualStyleBackColor = false;
            this.CloseAppButton.Click += new System.EventHandler(this.CloseAppButton_Click);
            // 
            // lblTitle
            // 
            this.lblTitle.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.lblTitle.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.lblTitle.ForeColor = System.Drawing.Color.White;
            this.lblTitle.Location = new System.Drawing.Point(201, 32);
            this.lblTitle.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lblTitle.Name = "lblTitle";
            this.lblTitle.Size = new System.Drawing.Size(650, 37);
            this.lblTitle.TabIndex = 0;
            this.lblTitle.Text = "ГЛАВНОЕ МЕНЮ";
            this.lblTitle.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // panelMenu
            // 
            this.panelMenu.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(3)))), ((int)(((byte)(64)))), ((int)(((byte)(114)))));
            this.panelMenu.Controls.Add(this.panelHeader);
            this.panelMenu.Controls.Add(this.btnMeasure);
            this.panelMenu.Controls.Add(this.btnDataBase);
            this.panelMenu.Controls.Add(this.btnSettings);
            this.panelMenu.Dock = System.Windows.Forms.DockStyle.Left;
            this.panelMenu.Location = new System.Drawing.Point(0, 0);
            this.panelMenu.Margin = new System.Windows.Forms.Padding(4);
            this.panelMenu.Name = "panelMenu";
            this.panelMenu.Size = new System.Drawing.Size(320, 613);
            this.panelMenu.TabIndex = 0;
            // 
            // panelHeader
            // 
            this.panelHeader.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(3)))), ((int)(((byte)(130)))), ((int)(((byte)(233)))));
            this.panelHeader.Dock = System.Windows.Forms.DockStyle.Top;
            this.panelHeader.Location = new System.Drawing.Point(0, 222);
            this.panelHeader.Margin = new System.Windows.Forms.Padding(4);
            this.panelHeader.Name = "panelHeader";
            this.panelHeader.Size = new System.Drawing.Size(320, 98);
            this.panelHeader.TabIndex = 0;
            // 
            // btnMeasure
            // 
            this.btnMeasure.Dock = System.Windows.Forms.DockStyle.Top;
            this.btnMeasure.FlatAppearance.BorderSize = 0;
            this.btnMeasure.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnMeasure.Font = new System.Drawing.Font("Microsoft Sans Serif", 11F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.btnMeasure.ForeColor = System.Drawing.Color.Gainsboro;
            this.btnMeasure.Image = ((System.Drawing.Image)(resources.GetObject("btnMeasure.Image")));
            this.btnMeasure.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnMeasure.Location = new System.Drawing.Point(0, 148);
            this.btnMeasure.Margin = new System.Windows.Forms.Padding(4);
            this.btnMeasure.Name = "btnMeasure";
            this.btnMeasure.Padding = new System.Windows.Forms.Padding(13, 0, 0, 0);
            this.btnMeasure.Size = new System.Drawing.Size(320, 74);
            this.btnMeasure.TabIndex = 1;
            this.btnMeasure.Text = " Измерения";
            this.btnMeasure.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnMeasure.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
            this.btnMeasure.UseVisualStyleBackColor = true;
            this.btnMeasure.Click += new System.EventHandler(this.btnMeasure_Click);
            // 
            // btnDataBase
            // 
            this.btnDataBase.Dock = System.Windows.Forms.DockStyle.Top;
            this.btnDataBase.FlatAppearance.BorderSize = 0;
            this.btnDataBase.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnDataBase.Font = new System.Drawing.Font("Microsoft Sans Serif", 11F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.btnDataBase.ForeColor = System.Drawing.Color.Gainsboro;
            this.btnDataBase.Image = ((System.Drawing.Image)(resources.GetObject("btnDataBase.Image")));
            this.btnDataBase.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnDataBase.Location = new System.Drawing.Point(0, 74);
            this.btnDataBase.Margin = new System.Windows.Forms.Padding(4);
            this.btnDataBase.Name = "btnDataBase";
            this.btnDataBase.Padding = new System.Windows.Forms.Padding(13, 0, 0, 0);
            this.btnDataBase.Size = new System.Drawing.Size(320, 74);
            this.btnDataBase.TabIndex = 2;
            this.btnDataBase.Text = " База Данных";
            this.btnDataBase.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnDataBase.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
            this.btnDataBase.UseVisualStyleBackColor = true;
            this.btnDataBase.Click += new System.EventHandler(this.btnDataBase_Click);
            // 
            // btnSettings
            // 
            this.btnSettings.Dock = System.Windows.Forms.DockStyle.Top;
            this.btnSettings.FlatAppearance.BorderSize = 0;
            this.btnSettings.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnSettings.Font = new System.Drawing.Font("Microsoft Sans Serif", 11F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.btnSettings.ForeColor = System.Drawing.Color.Gainsboro;
            this.btnSettings.Image = ((System.Drawing.Image)(resources.GetObject("btnSettings.Image")));
            this.btnSettings.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnSettings.Location = new System.Drawing.Point(0, 0);
            this.btnSettings.Margin = new System.Windows.Forms.Padding(4);
            this.btnSettings.Name = "btnSettings";
            this.btnSettings.Padding = new System.Windows.Forms.Padding(13, 0, 0, 0);
            this.btnSettings.Size = new System.Drawing.Size(320, 74);
            this.btnSettings.TabIndex = 3;
            this.btnSettings.Text = " Настройки";
            this.btnSettings.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnSettings.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
            this.btnSettings.UseVisualStyleBackColor = true;
            this.btnSettings.Click += new System.EventHandler(this.btnSettings_Click);
            // 
            // UIMainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.ClientSize = new System.Drawing.Size(1375, 615);
            this.Name = "UIMainForm";
            this.Text = "UIMainForm";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.UIMainForm_FormClosing);
            this.ContentPanel.ResumeLayout(false);
            this.panelTitleBar.ResumeLayout(false);
            this.panelMenu.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        protected System.Windows.Forms.Panel panelMenu;
        private System.Windows.Forms.Panel panelTitleBar;
        private System.Windows.Forms.Label lblTitle;
        private System.Windows.Forms.Button MinimizeAppButton;
        private System.Windows.Forms.Button MaximizeAppButton;
        private System.Windows.Forms.Button CloseAppButton;
        private System.Windows.Forms.Panel childFormPanel;
        protected System.Windows.Forms.Panel panelHeader;
        protected System.Windows.Forms.Button btnMeasure;
        protected System.Windows.Forms.Button btnSettings;
        protected System.Windows.Forms.Button btnDataBase;
    }
}