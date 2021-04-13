namespace NormaLib.ProtocolBuilders
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
            this.msWordProtocolsSettings.SuspendLayout();
            this.SuspendLayout();
            // 
            // msWordProtocolsSettings
            // 
            this.msWordProtocolsSettings.Controls.Add(this.serchFolderButton);
            this.msWordProtocolsSettings.Controls.Add(this.label1);
            this.msWordProtocolsSettings.Controls.Add(this.msWordFilePathTextBox);
            this.msWordProtocolsSettings.Location = new System.Drawing.Point(18, 18);
            this.msWordProtocolsSettings.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.msWordProtocolsSettings.Name = "msWordProtocolsSettings";
            this.msWordProtocolsSettings.Padding = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.msWordProtocolsSettings.Size = new System.Drawing.Size(717, 177);
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
            // ProtocolSettingsForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(9F, 20F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(750, 402);
            this.Controls.Add(this.msWordProtocolsSettings);
            this.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.Name = "ProtocolSettingsForm";
            this.Text = "ProtocolSettingsForm";
            this.msWordProtocolsSettings.ResumeLayout(false);
            this.msWordProtocolsSettings.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.GroupBox msWordProtocolsSettings;
        private System.Windows.Forms.Button serchFolderButton;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox msWordFilePathTextBox;
    }
}