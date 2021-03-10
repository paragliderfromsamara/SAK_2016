namespace TeraMicroMeasure.Forms
{
    partial class DataBaseTablesControlForm
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
            this.usersTabPage = new System.Windows.Forms.TabPage();
            this.barabanTypeTabPage = new System.Windows.Forms.TabPage();
            this.cablesTabPage = new System.Windows.Forms.TabPage();
            this.resultsTabPage = new System.Windows.Forms.TabPage();
            this.settingsTab.SuspendLayout();
            this.SuspendLayout();
            // 
            // settingsTab
            // 
            this.settingsTab.Controls.Add(this.usersTabPage);
            this.settingsTab.Controls.Add(this.barabanTypeTabPage);
            this.settingsTab.Controls.Add(this.cablesTabPage);
            this.settingsTab.Controls.Add(this.resultsTabPage);
            // 
            // usersTabPage
            // 
            this.usersTabPage.Location = new System.Drawing.Point(4, 39);
            this.usersTabPage.Name = "usersTabPage";
            this.usersTabPage.Size = new System.Drawing.Size(1021, 503);
            this.usersTabPage.TabIndex = 0;
            this.usersTabPage.Text = "Пользователи";
            this.usersTabPage.UseVisualStyleBackColor = true;
            // 
            // barabanTypeTabPage
            // 
            this.barabanTypeTabPage.Location = new System.Drawing.Point(4, 39);
            this.barabanTypeTabPage.Name = "barabanTypeTabPage";
            this.barabanTypeTabPage.Size = new System.Drawing.Size(1021, 503);
            this.barabanTypeTabPage.TabIndex = 1;
            this.barabanTypeTabPage.Text = "Типы барабанов";
            this.barabanTypeTabPage.UseVisualStyleBackColor = true;
            // 
            // cablesTabPage
            // 
            this.cablesTabPage.Location = new System.Drawing.Point(4, 39);
            this.cablesTabPage.Name = "cablesTabPage";
            this.cablesTabPage.Size = new System.Drawing.Size(1021, 503);
            this.cablesTabPage.TabIndex = 2;
            this.cablesTabPage.Text = "Кабели";
            this.cablesTabPage.UseVisualStyleBackColor = true;
            // 
            // resultsTabPage
            // 
            this.resultsTabPage.Location = new System.Drawing.Point(4, 39);
            this.resultsTabPage.Name = "resultsTabPage";
            this.resultsTabPage.Size = new System.Drawing.Size(1021, 503);
            this.resultsTabPage.TabIndex = 3;
            this.resultsTabPage.Text = "Результаты испытаний";
            this.resultsTabPage.UseVisualStyleBackColor = true;
            // 
            // DataBaseTablesControlForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1029, 546);
            this.Name = "DataBaseTablesControlForm";
            this.Text = "БАЗА ДАННЫХ";
            this.settingsTab.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.TabPage usersTabPage;
        private System.Windows.Forms.TabPage barabanTypeTabPage;
        private System.Windows.Forms.TabPage cablesTabPage;
        private System.Windows.Forms.TabPage resultsTabPage;
    }
}