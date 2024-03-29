﻿namespace NormaLib.ProtocolBuilders
{
    partial class ProtocolSettingsForm
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
            this.msWordProtocolsSettings = new System.Windows.Forms.GroupBox();
            this.serchFolderButton = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.msWordFilePathTextBox = new System.Windows.Forms.TextBox();
            this.companyNameTextBox = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.protocolHeaderText = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.printProtocolNumberAtTitle = new System.Windows.Forms.CheckBox();
            this.commonSettingsPanel = new System.Windows.Forms.GroupBox();
            this.titleLbl1 = new System.Windows.Forms.Label();
            this.msWordProtocolsSettings.SuspendLayout();
            this.commonSettingsPanel.SuspendLayout();
            this.SuspendLayout();
            // 
            // msWordProtocolsSettings
            // 
            this.msWordProtocolsSettings.Controls.Add(this.serchFolderButton);
            this.msWordProtocolsSettings.Controls.Add(this.label1);
            this.msWordProtocolsSettings.Controls.Add(this.msWordFilePathTextBox);
            this.msWordProtocolsSettings.Location = new System.Drawing.Point(12, 181);
            this.msWordProtocolsSettings.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.msWordProtocolsSettings.Name = "msWordProtocolsSettings";
            this.msWordProtocolsSettings.Padding = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.msWordProtocolsSettings.Size = new System.Drawing.Size(853, 111);
            this.msWordProtocolsSettings.TabIndex = 0;
            this.msWordProtocolsSettings.TabStop = false;
            this.msWordProtocolsSettings.Text = "Протоколы  MS Word";
            // 
            // serchFolderButton
            // 
            this.serchFolderButton.BackColor = System.Drawing.Color.MidnightBlue;
            this.serchFolderButton.FlatAppearance.BorderSize = 0;
            this.serchFolderButton.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.serchFolderButton.ForeColor = System.Drawing.Color.Gainsboro;
            this.serchFolderButton.Location = new System.Drawing.Point(610, 53);
            this.serchFolderButton.Name = "serchFolderButton";
            this.serchFolderButton.Size = new System.Drawing.Size(89, 37);
            this.serchFolderButton.TabIndex = 3;
            this.serchFolderButton.Text = "Обзор";
            this.serchFolderButton.UseVisualStyleBackColor = false;
            this.serchFolderButton.Click += new System.EventHandler(this.serchFolderButton_Click);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(7, 33);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(130, 20);
            this.label1.TabIndex = 2;
            this.label1.Text = "Папка хранения";
            // 
            // msWordFilePathTextBox
            // 
            this.msWordFilePathTextBox.Location = new System.Drawing.Point(11, 58);
            this.msWordFilePathTextBox.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.msWordFilePathTextBox.Name = "msWordFilePathTextBox";
            this.msWordFilePathTextBox.ReadOnly = true;
            this.msWordFilePathTextBox.Size = new System.Drawing.Size(582, 26);
            this.msWordFilePathTextBox.TabIndex = 0;
            // 
            // companyNameTextBox
            // 
            this.companyNameTextBox.Location = new System.Drawing.Point(11, 55);
            this.companyNameTextBox.Name = "companyNameTextBox";
            this.companyNameTextBox.Size = new System.Drawing.Size(321, 26);
            this.companyNameTextBox.TabIndex = 1;
            this.companyNameTextBox.TextChanged += new System.EventHandler(this.companyNameTextBox_TextChanged);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(7, 32);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(188, 20);
            this.label2.TabIndex = 2;
            this.label2.Text = "Название предприятия";
            // 
            // protocolHeaderText
            // 
            this.protocolHeaderText.Location = new System.Drawing.Point(349, 55);
            this.protocolHeaderText.Name = "protocolHeaderText";
            this.protocolHeaderText.Size = new System.Drawing.Size(230, 26);
            this.protocolHeaderText.TabIndex = 3;
            this.protocolHeaderText.TextChanged += new System.EventHandler(this.protocolHeaderText_TextChanged);
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(345, 32);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(175, 20);
            this.label3.TabIndex = 4;
            this.label3.Text = "Заголовок протокола";
            // 
            // printProtocolNumberAtTitle
            // 
            this.printProtocolNumberAtTitle.AutoSize = true;
            this.printProtocolNumberAtTitle.Location = new System.Drawing.Point(600, 55);
            this.printProtocolNumberAtTitle.Name = "printProtocolNumberAtTitle";
            this.printProtocolNumberAtTitle.Size = new System.Drawing.Size(226, 24);
            this.printProtocolNumberAtTitle.TabIndex = 5;
            this.printProtocolNumberAtTitle.Text = "ID испытания в заголовке";
            this.printProtocolNumberAtTitle.UseVisualStyleBackColor = true;
            this.printProtocolNumberAtTitle.CheckedChanged += new System.EventHandler(this.printProtocolNumberAtTitle_CheckedChanged);
            // 
            // commonSettingsPanel
            // 
            this.commonSettingsPanel.Controls.Add(this.companyNameTextBox);
            this.commonSettingsPanel.Controls.Add(this.protocolHeaderText);
            this.commonSettingsPanel.Controls.Add(this.printProtocolNumberAtTitle);
            this.commonSettingsPanel.Controls.Add(this.label3);
            this.commonSettingsPanel.Controls.Add(this.label2);
            this.commonSettingsPanel.Location = new System.Drawing.Point(12, 73);
            this.commonSettingsPanel.Name = "commonSettingsPanel";
            this.commonSettingsPanel.Size = new System.Drawing.Size(853, 100);
            this.commonSettingsPanel.TabIndex = 6;
            this.commonSettingsPanel.TabStop = false;
            this.commonSettingsPanel.Text = "Общие настройки";
            // 
            // titleLbl1
            // 
            this.titleLbl1.AutoSize = true;
            this.titleLbl1.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.titleLbl1.Location = new System.Drawing.Point(8, 24);
            this.titleLbl1.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.titleLbl1.Name = "titleLbl1";
            this.titleLbl1.Size = new System.Drawing.Size(320, 24);
            this.titleLbl1.TabIndex = 7;
            this.titleLbl1.Text = "Настройки протоколов испытаний";
            // 
            // ProtocolSettingsForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(9F, 20F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.White;
            this.ClientSize = new System.Drawing.Size(881, 362);
            this.Controls.Add(this.titleLbl1);
            this.Controls.Add(this.commonSettingsPanel);
            this.Controls.Add(this.msWordProtocolsSettings);
            this.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.Name = "ProtocolSettingsForm";
            this.Text = "ProtocolSettingsForm";
            this.msWordProtocolsSettings.ResumeLayout(false);
            this.msWordProtocolsSettings.PerformLayout();
            this.commonSettingsPanel.ResumeLayout(false);
            this.commonSettingsPanel.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.GroupBox msWordProtocolsSettings;
        private System.Windows.Forms.Button serchFolderButton;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox msWordFilePathTextBox;
        private System.Windows.Forms.TextBox companyNameTextBox;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox protocolHeaderText;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.CheckBox printProtocolNumberAtTitle;
        private System.Windows.Forms.GroupBox commonSettingsPanel;
        private System.Windows.Forms.Label titleLbl1;
    }
}