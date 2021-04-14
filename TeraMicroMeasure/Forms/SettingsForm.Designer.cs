namespace TeraMicroMeasure.Forms
{
    partial class SettingsForm
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
            this.connectionSettingsTabPage = new System.Windows.Forms.TabPage();
            this.dataBaseSettingsTabPage = new System.Windows.Forms.TabPage();
            this.protocolSettingsTabPage = new System.Windows.Forms.TabPage();
            this.settingsTab.SuspendLayout();
            this.SuspendLayout();
            // 
            // settingsTab
            // 
            this.settingsTab.Controls.Add(this.connectionSettingsTabPage);
            this.settingsTab.Controls.Add(this.dataBaseSettingsTabPage);
            this.settingsTab.Controls.Add(this.protocolSettingsTabPage);
            this.settingsTab.Padding = new System.Drawing.Point(25, 10);
            // 
            // connectionSettingsTabPage
            // 
            this.connectionSettingsTabPage.BackColor = System.Drawing.Color.White;
            this.connectionSettingsTabPage.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.connectionSettingsTabPage.Location = new System.Drawing.Point(4, 43);
            this.connectionSettingsTabPage.Name = "connectionSettingsTabPage";
            this.connectionSettingsTabPage.Padding = new System.Windows.Forms.Padding(3);
            this.connectionSettingsTabPage.Size = new System.Drawing.Size(1021, 499);
            this.connectionSettingsTabPage.TabIndex = 0;
            this.connectionSettingsTabPage.Text = "Настройки соединения";
            this.connectionSettingsTabPage.UseVisualStyleBackColor = true;
            // 
            // dataBaseSettingsTabPage
            // 
            this.dataBaseSettingsTabPage.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.dataBaseSettingsTabPage.Location = new System.Drawing.Point(4, 39);
            this.dataBaseSettingsTabPage.Name = "dataBaseSettingsTabPage";
            this.dataBaseSettingsTabPage.Padding = new System.Windows.Forms.Padding(3);
            this.dataBaseSettingsTabPage.Size = new System.Drawing.Size(1021, 503);
            this.dataBaseSettingsTabPage.TabIndex = 1;
            this.dataBaseSettingsTabPage.Text = "Настройки Базы Данных";
            this.dataBaseSettingsTabPage.UseVisualStyleBackColor = true;
            // 
            // protocolSettingsTabPage
            // 
            this.protocolSettingsTabPage.Location = new System.Drawing.Point(4, 39);
            this.protocolSettingsTabPage.Name = "protocolSettingsTabPage";
            this.protocolSettingsTabPage.Padding = new System.Windows.Forms.Padding(3);
            this.protocolSettingsTabPage.Size = new System.Drawing.Size(1021, 503);
            this.protocolSettingsTabPage.TabIndex = 2;
            this.protocolSettingsTabPage.Text = "Настройки протоколов";
            this.protocolSettingsTabPage.UseVisualStyleBackColor = true;
            // 
            // SettingsForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1029, 546);
            this.Name = "SettingsForm";
            this.settingsTab.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.TabPage connectionSettingsTabPage;
        private System.Windows.Forms.TabPage dataBaseSettingsTabPage;
        private System.Windows.Forms.TabPage protocolSettingsTabPage;
    }
}