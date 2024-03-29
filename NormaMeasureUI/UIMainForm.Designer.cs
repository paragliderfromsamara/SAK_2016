﻿namespace NormaLib.UI
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
            this.panelMenu = new System.Windows.Forms.Panel();
            this.btnDataBase = new System.Windows.Forms.Button();
            this.btnSettings = new System.Windows.Forms.Button();
            this.btnMeasure = new System.Windows.Forms.Button();
            this.panelHeader = new System.Windows.Forms.Panel();
            this.panelTitleBar = new System.Windows.Forms.Panel();
            this.MinimizeAppButton = new System.Windows.Forms.Button();
            this.MaximizeAppButton = new System.Windows.Forms.Button();
            this.CloseAppButton = new System.Windows.Forms.Button();
            this.lblTitle = new System.Windows.Forms.Label();
            this.childFormPanel = new System.Windows.Forms.Panel();
            this.panelMenu.SuspendLayout();
            this.panelTitleBar.SuspendLayout();
            this.SuspendLayout();
            // 
            // panelMenu
            // 
            this.panelMenu.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(3)))), ((int)(((byte)(64)))), ((int)(((byte)(114)))));
            this.panelMenu.Controls.Add(this.btnSettings);
            this.panelMenu.Controls.Add(this.btnDataBase);
            this.panelMenu.Controls.Add(this.btnMeasure);
            this.panelMenu.Controls.Add(this.panelHeader);
            this.panelMenu.Dock = System.Windows.Forms.DockStyle.Left;
            this.panelMenu.Location = new System.Drawing.Point(0, 0);
            this.panelMenu.Name = "panelMenu";
            this.panelMenu.Size = new System.Drawing.Size(240, 884);
            this.panelMenu.TabIndex = 0;
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
            this.btnDataBase.Location = new System.Drawing.Point(0, 140);
            this.btnDataBase.Name = "btnDataBase";
            this.btnDataBase.Padding = new System.Windows.Forms.Padding(10, 0, 0, 0);
            this.btnDataBase.Size = new System.Drawing.Size(240, 60);
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
            this.btnSettings.Location = new System.Drawing.Point(0, 200);
            this.btnSettings.Name = "btnSettings";
            this.btnSettings.Padding = new System.Windows.Forms.Padding(10, 0, 0, 0);
            this.btnSettings.Size = new System.Drawing.Size(240, 60);
            this.btnSettings.TabIndex = 3;
            this.btnSettings.Text = " Настройки";
            this.btnSettings.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnSettings.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
            this.btnSettings.UseVisualStyleBackColor = true;
            this.btnSettings.Click += new System.EventHandler(this.btnSettings_Click);
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
            this.btnMeasure.Location = new System.Drawing.Point(0, 80);
            this.btnMeasure.Name = "btnMeasure";
            this.btnMeasure.Padding = new System.Windows.Forms.Padding(10, 0, 0, 0);
            this.btnMeasure.Size = new System.Drawing.Size(240, 60);
            this.btnMeasure.TabIndex = 1;
            this.btnMeasure.Text = " Измерения";
            this.btnMeasure.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnMeasure.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
            this.btnMeasure.UseVisualStyleBackColor = true;
            this.btnMeasure.Click += new System.EventHandler(this.btnMeasure_Click);
            // 
            // panelHeader
            // 
            this.panelHeader.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(3)))), ((int)(((byte)(130)))), ((int)(((byte)(233)))));
            this.panelHeader.Dock = System.Windows.Forms.DockStyle.Top;
            this.panelHeader.Location = new System.Drawing.Point(0, 0);
            this.panelHeader.Name = "panelHeader";
            this.panelHeader.Size = new System.Drawing.Size(240, 80);
            this.panelHeader.TabIndex = 0;
            // 
            // panelTitleBar
            // 
            this.panelTitleBar.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(155)))), ((int)(((byte)(136)))));
            this.panelTitleBar.Controls.Add(this.MinimizeAppButton);
            this.panelTitleBar.Controls.Add(this.MaximizeAppButton);
            this.panelTitleBar.Controls.Add(this.CloseAppButton);
            this.panelTitleBar.Controls.Add(this.lblTitle);
            this.panelTitleBar.Dock = System.Windows.Forms.DockStyle.Top;
            this.panelTitleBar.Location = new System.Drawing.Point(240, 0);
            this.panelTitleBar.Name = "panelTitleBar";
            this.panelTitleBar.Size = new System.Drawing.Size(984, 80);
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
            this.MinimizeAppButton.Location = new System.Drawing.Point(855, 0);
            this.MinimizeAppButton.Name = "MinimizeAppButton";
            this.MinimizeAppButton.Size = new System.Drawing.Size(40, 25);
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
            this.MaximizeAppButton.Location = new System.Drawing.Point(901, 0);
            this.MaximizeAppButton.Name = "MaximizeAppButton";
            this.MaximizeAppButton.Size = new System.Drawing.Size(40, 25);
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
            this.CloseAppButton.Location = new System.Drawing.Point(944, 0);
            this.CloseAppButton.Name = "CloseAppButton";
            this.CloseAppButton.Size = new System.Drawing.Size(40, 25);
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
            this.lblTitle.Location = new System.Drawing.Point(325, 26);
            this.lblTitle.Name = "lblTitle";
            this.lblTitle.Size = new System.Drawing.Size(339, 30);
            this.lblTitle.TabIndex = 0;
            this.lblTitle.Text = "ГЛАВНОЕ МЕНЮ";
            this.lblTitle.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // childFormPanel
            // 
            this.childFormPanel.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(224)))), ((int)(((byte)(224)))), ((int)(((byte)(224)))));
            this.childFormPanel.Dock = System.Windows.Forms.DockStyle.Fill;
            this.childFormPanel.Location = new System.Drawing.Point(240, 80);
            this.childFormPanel.Name = "childFormPanel";
            this.childFormPanel.Size = new System.Drawing.Size(984, 804);
            this.childFormPanel.TabIndex = 2;
            // 
            // UIMainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.White;
            this.ClientSize = new System.Drawing.Size(1224, 884);
            this.ControlBox = false;
            this.Controls.Add(this.childFormPanel);
            this.Controls.Add(this.panelTitleBar);
            this.Controls.Add(this.panelMenu);
            this.MinimumSize = new System.Drawing.Size(1000, 900);
            this.Name = "UIMainForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "UIMainForm";
            this.panelMenu.ResumeLayout(false);
            this.panelTitleBar.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        protected System.Windows.Forms.Panel panelMenu;
        private System.Windows.Forms.Panel panelHeader;
        private System.Windows.Forms.Button btnMeasure;
        private System.Windows.Forms.Panel panelTitleBar;
        private System.Windows.Forms.Label lblTitle;
        private System.Windows.Forms.Button btnSettings;
        private System.Windows.Forms.Button MinimizeAppButton;
        private System.Windows.Forms.Button MaximizeAppButton;
        private System.Windows.Forms.Button CloseAppButton;
        private System.Windows.Forms.Button btnDataBase;
        private System.Windows.Forms.Panel childFormPanel;
    }
}