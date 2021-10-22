namespace NormaMeasure
{
    partial class SinglePCAppForm
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
            this.titleLabel_1 = new System.Windows.Forms.Label();
            this.titleLabel_2 = new System.Windows.Forms.Label();
            this.clientTitle = new System.Windows.Forms.Label();
            this.sessionPanel = new System.Windows.Forms.Panel();
            this.roleTitleLabel = new System.Windows.Forms.Label();
            this.userNameLabel = new System.Windows.Forms.Label();
            this.button1 = new System.Windows.Forms.Button();
            this.devicesListPanel = new System.Windows.Forms.Panel();
            this.panel1 = new System.Windows.Forms.Panel();
            this.deviceButtonsContainerPanel = new System.Windows.Forms.Panel();
            this.panelMenu.SuspendLayout();
            this.panelHeader.SuspendLayout();
            this.ContentPanel.SuspendLayout();
            this.sessionPanel.SuspendLayout();
            this.devicesListPanel.SuspendLayout();
            this.SuspendLayout();
            // 
            // panelMenu
            // 
            this.panelMenu.Controls.Add(this.devicesListPanel);
            this.panelMenu.Controls.Add(this.sessionPanel);
            this.panelMenu.Controls.SetChildIndex(this.btnSettings, 0);
            this.panelMenu.Controls.SetChildIndex(this.btnDataBase, 0);
            this.panelMenu.Controls.SetChildIndex(this.btnMeasure, 0);
            this.panelMenu.Controls.SetChildIndex(this.panelHeader, 0);
            this.panelMenu.Controls.SetChildIndex(this.sessionPanel, 0);
            this.panelMenu.Controls.SetChildIndex(this.devicesListPanel, 0);
            // 
            // panelHeader
            // 
            this.panelHeader.Controls.Add(this.titleLabel_1);
            this.panelHeader.Controls.Add(this.titleLabel_2);
            this.panelHeader.Controls.Add(this.clientTitle);
            // 
            // btnMeasure
            // 
            this.btnMeasure.FlatAppearance.BorderSize = 0;
            // 
            // btnSettings
            // 
            this.btnSettings.FlatAppearance.BorderSize = 0;
            // 
            // btnDataBase
            // 
            this.btnDataBase.FlatAppearance.BorderSize = 0;
            // 
            // titleLabel_1
            // 
            this.titleLabel_1.AutoSize = true;
            this.titleLabel_1.ForeColor = System.Drawing.SystemColors.ButtonHighlight;
            this.titleLabel_1.Location = new System.Drawing.Point(22, 51);
            this.titleLabel_1.Name = "titleLabel_1";
            this.titleLabel_1.Size = new System.Drawing.Size(0, 13);
            this.titleLabel_1.TabIndex = 5;
            // 
            // titleLabel_2
            // 
            this.titleLabel_2.AutoSize = true;
            this.titleLabel_2.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F);
            this.titleLabel_2.ForeColor = System.Drawing.SystemColors.ButtonHighlight;
            this.titleLabel_2.Location = new System.Drawing.Point(22, 68);
            this.titleLabel_2.Name = "titleLabel_2";
            this.titleLabel_2.Size = new System.Drawing.Size(0, 15);
            this.titleLabel_2.TabIndex = 4;
            // 
            // clientTitle
            // 
            this.clientTitle.AutoSize = true;
            this.clientTitle.Font = new System.Drawing.Font("Microsoft Sans Serif", 20.25F);
            this.clientTitle.ForeColor = System.Drawing.Color.White;
            this.clientTitle.Location = new System.Drawing.Point(19, 14);
            this.clientTitle.Name = "clientTitle";
            this.clientTitle.Size = new System.Drawing.Size(200, 31);
            this.clientTitle.TabIndex = 3;
            this.clientTitle.Text = "NormaMeasure";
            this.clientTitle.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // sessionPanel
            // 
            this.sessionPanel.BackColor = System.Drawing.Color.DarkSlateGray;
            this.sessionPanel.Controls.Add(this.roleTitleLabel);
            this.sessionPanel.Controls.Add(this.userNameLabel);
            this.sessionPanel.Controls.Add(this.button1);
            this.sessionPanel.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.sessionPanel.Location = new System.Drawing.Point(0, 553);
            this.sessionPanel.Name = "sessionPanel";
            this.sessionPanel.Size = new System.Drawing.Size(320, 60);
            this.sessionPanel.TabIndex = 7;
            // 
            // roleTitleLabel
            // 
            this.roleTitleLabel.Dock = System.Windows.Forms.DockStyle.Top;
            this.roleTitleLabel.ForeColor = System.Drawing.Color.Gainsboro;
            this.roleTitleLabel.Location = new System.Drawing.Point(76, 27);
            this.roleTitleLabel.Name = "roleTitleLabel";
            this.roleTitleLabel.Size = new System.Drawing.Size(244, 21);
            this.roleTitleLabel.TabIndex = 2;
            this.roleTitleLabel.Text = "Администратор";
            this.roleTitleLabel.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // userNameLabel
            // 
            this.userNameLabel.Dock = System.Windows.Forms.DockStyle.Top;
            this.userNameLabel.ForeColor = System.Drawing.Color.Gainsboro;
            this.userNameLabel.Location = new System.Drawing.Point(76, 0);
            this.userNameLabel.Name = "userNameLabel";
            this.userNameLabel.Size = new System.Drawing.Size(244, 27);
            this.userNameLabel.TabIndex = 1;
            this.userNameLabel.Text = "Иванов Иван Иванович";
            this.userNameLabel.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // button1
            // 
            this.button1.BackColor = System.Drawing.Color.MidnightBlue;
            this.button1.Cursor = System.Windows.Forms.Cursors.Hand;
            this.button1.Dock = System.Windows.Forms.DockStyle.Left;
            this.button1.FlatAppearance.BorderSize = 0;
            this.button1.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.button1.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F);
            this.button1.ForeColor = System.Drawing.Color.Gainsboro;
            this.button1.Location = new System.Drawing.Point(0, 0);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(76, 60);
            this.button1.TabIndex = 0;
            this.button1.Text = "Выйти";
            this.button1.UseVisualStyleBackColor = false;
            this.button1.Click += new System.EventHandler(this.signoutButton_Click);
            // 
            // devicesListPanel
            // 
            this.devicesListPanel.Controls.Add(this.panel1);
            this.devicesListPanel.Controls.Add(this.deviceButtonsContainerPanel);
            this.devicesListPanel.Dock = System.Windows.Forms.DockStyle.Fill;
            this.devicesListPanel.Location = new System.Drawing.Point(0, 320);
            this.devicesListPanel.Name = "devicesListPanel";
            this.devicesListPanel.Size = new System.Drawing.Size(320, 233);
            this.devicesListPanel.TabIndex = 8;
            // 
            // panel1
            // 
            this.panel1.BackColor = System.Drawing.Color.DimGray;
            this.panel1.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel1.Location = new System.Drawing.Point(0, 0);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(320, 1);
            this.panel1.TabIndex = 1;
            // 
            // deviceButtonsContainerPanel
            // 
            this.deviceButtonsContainerPanel.AutoScroll = true;
            this.deviceButtonsContainerPanel.Dock = System.Windows.Forms.DockStyle.Fill;
            this.deviceButtonsContainerPanel.Location = new System.Drawing.Point(0, 0);
            this.deviceButtonsContainerPanel.Name = "deviceButtonsContainerPanel";
            this.deviceButtonsContainerPanel.Size = new System.Drawing.Size(320, 233);
            this.deviceButtonsContainerPanel.TabIndex = 1;
            // 
            // SinglePCAppForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1375, 615);
            this.Name = "SinglePCAppForm";
            this.Text = "SinglePCAppForm";
            this.panelMenu.ResumeLayout(false);
            this.panelHeader.ResumeLayout(false);
            this.panelHeader.PerformLayout();
            this.ContentPanel.ResumeLayout(false);
            this.sessionPanel.ResumeLayout(false);
            this.devicesListPanel.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion
        private System.Windows.Forms.Label titleLabel_1;
        private System.Windows.Forms.Label titleLabel_2;
        private System.Windows.Forms.Label clientTitle;
        private System.Windows.Forms.Panel sessionPanel;
        private System.Windows.Forms.Label roleTitleLabel;
        private System.Windows.Forms.Label userNameLabel;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.Panel devicesListPanel;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Panel deviceButtonsContainerPanel;
    }
}