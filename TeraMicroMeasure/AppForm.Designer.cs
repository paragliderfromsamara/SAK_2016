namespace TeraMicroMeasure
{
    partial class AppForm
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(AppForm));
            this.clientTitle = new System.Windows.Forms.Label();
            this.titleLabel_2 = new System.Windows.Forms.Label();
            this.titleLabel_1 = new System.Windows.Forms.Label();
            this.connectToServerButton = new System.Windows.Forms.Button();
            this.connectButPanel = new System.Windows.Forms.Panel();
            this.sessionPanel = new System.Windows.Forms.Panel();
            this.roleTitleLabel = new System.Windows.Forms.Label();
            this.userNameLabel = new System.Windows.Forms.Label();
            this.button1 = new System.Windows.Forms.Button();
            this.panelMenu.SuspendLayout();
            this.panelHeader.SuspendLayout();
            this.connectButPanel.SuspendLayout();
            this.sessionPanel.SuspendLayout();
            this.SuspendLayout();
            // 
            // panelMenu
            // 
            this.panelMenu.Controls.Add(this.sessionPanel);
            this.panelMenu.Controls.Add(this.connectButPanel);
            this.panelMenu.Controls.SetChildIndex(this.btnSettings, 0);
            this.panelMenu.Controls.SetChildIndex(this.btnDataBase, 0);
            this.panelMenu.Controls.SetChildIndex(this.btnMeasure, 0);
            this.panelMenu.Controls.SetChildIndex(this.panelHeader, 0);
            this.panelMenu.Controls.SetChildIndex(this.connectButPanel, 0);
            this.panelMenu.Controls.SetChildIndex(this.sessionPanel, 0);
            // 
            // panelHeader
            // 
            this.panelHeader.Controls.Add(this.titleLabel_1);
            this.panelHeader.Controls.Add(this.titleLabel_2);
            this.panelHeader.Controls.Add(this.clientTitle);
            resources.ApplyResources(this.panelHeader, "panelHeader");
            // 
            // btnMeasure
            // 
            this.btnMeasure.FlatAppearance.BorderSize = 0;
            resources.ApplyResources(this.btnMeasure, "btnMeasure");
            // 
            // btnSettings
            // 
            this.btnSettings.FlatAppearance.BorderSize = 0;
            // 
            // btnDataBase
            // 
            this.btnDataBase.FlatAppearance.BorderSize = 0;
            // 
            // clientTitle
            // 
            resources.ApplyResources(this.clientTitle, "clientTitle");
            this.clientTitle.ForeColor = System.Drawing.Color.White;
            this.clientTitle.Name = "clientTitle";
            // 
            // titleLabel_2
            // 
            resources.ApplyResources(this.titleLabel_2, "titleLabel_2");
            this.titleLabel_2.ForeColor = System.Drawing.SystemColors.ButtonHighlight;
            this.titleLabel_2.Name = "titleLabel_2";
            // 
            // titleLabel_1
            // 
            resources.ApplyResources(this.titleLabel_1, "titleLabel_1");
            this.titleLabel_1.ForeColor = System.Drawing.SystemColors.ButtonHighlight;
            this.titleLabel_1.Name = "titleLabel_1";
            // 
            // connectToServerButton
            // 
            this.connectToServerButton.Cursor = System.Windows.Forms.Cursors.Hand;
            resources.ApplyResources(this.connectToServerButton, "connectToServerButton");
            this.connectToServerButton.FlatAppearance.BorderSize = 0;
            this.connectToServerButton.ForeColor = System.Drawing.Color.Gainsboro;
            this.connectToServerButton.Image = global::TeraMicroMeasure.Properties.Resources.disconnect_white;
            this.connectToServerButton.Name = "connectToServerButton";
            this.connectToServerButton.UseVisualStyleBackColor = true;
            // 
            // connectButPanel
            // 
            this.connectButPanel.Controls.Add(this.connectToServerButton);
            resources.ApplyResources(this.connectButPanel, "connectButPanel");
            this.connectButPanel.Name = "connectButPanel";
            // 
            // sessionPanel
            // 
            this.sessionPanel.BackColor = System.Drawing.Color.DarkSlateGray;
            this.sessionPanel.Controls.Add(this.roleTitleLabel);
            this.sessionPanel.Controls.Add(this.userNameLabel);
            this.sessionPanel.Controls.Add(this.button1);
            resources.ApplyResources(this.sessionPanel, "sessionPanel");
            this.sessionPanel.Name = "sessionPanel";
            // 
            // roleTitleLabel
            // 
            resources.ApplyResources(this.roleTitleLabel, "roleTitleLabel");
            this.roleTitleLabel.ForeColor = System.Drawing.Color.Gainsboro;
            this.roleTitleLabel.Name = "roleTitleLabel";
            // 
            // userNameLabel
            // 
            resources.ApplyResources(this.userNameLabel, "userNameLabel");
            this.userNameLabel.ForeColor = System.Drawing.Color.Gainsboro;
            this.userNameLabel.Name = "userNameLabel";
            // 
            // button1
            // 
            this.button1.BackColor = System.Drawing.Color.MidnightBlue;
            this.button1.Cursor = System.Windows.Forms.Cursors.Hand;
            resources.ApplyResources(this.button1, "button1");
            this.button1.FlatAppearance.BorderSize = 0;
            this.button1.ForeColor = System.Drawing.Color.Gainsboro;
            this.button1.Name = "button1";
            this.button1.UseVisualStyleBackColor = false;
            this.button1.Click += new System.EventHandler(this.signoutButton_Click);
            // 
            // AppForm
            // 
            resources.ApplyResources(this, "$this");
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Name = "AppForm";
            this.panelMenu.ResumeLayout(false);
            this.panelHeader.ResumeLayout(false);
            this.panelHeader.PerformLayout();
            this.connectButPanel.ResumeLayout(false);
            this.sessionPanel.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Label clientTitle;
        private System.Windows.Forms.Label titleLabel_1;
        private System.Windows.Forms.Label titleLabel_2;
        private System.Windows.Forms.Button connectToServerButton;
        private System.Windows.Forms.Panel connectButPanel;
        private System.Windows.Forms.Panel sessionPanel;
        private System.Windows.Forms.Label roleTitleLabel;
        private System.Windows.Forms.Label userNameLabel;
        private System.Windows.Forms.Button button1;
    }
}