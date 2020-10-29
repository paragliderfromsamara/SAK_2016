namespace TeraMicroMeasure
{
    partial class SelectServerIpForm
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
            this.formTitleLabel = new System.Windows.Forms.Label();
            this.topPanel = new System.Windows.Forms.Panel();
            this.workSpacePanel = new System.Windows.Forms.Panel();
            this.topPanel.SuspendLayout();
            this.SuspendLayout();
            // 
            // formTitleLabel
            // 
            this.formTitleLabel.AutoSize = true;
            this.formTitleLabel.Font = new System.Drawing.Font("Tahoma", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.formTitleLabel.Location = new System.Drawing.Point(12, 21);
            this.formTitleLabel.Name = "formTitleLabel";
            this.formTitleLabel.Size = new System.Drawing.Size(127, 19);
            this.formTitleLabel.TabIndex = 3;
            this.formTitleLabel.Text = "Данные сервера";
            // 
            // topPanel
            // 
            this.topPanel.BackColor = System.Drawing.SystemColors.GradientInactiveCaption;
            this.topPanel.Controls.Add(this.formTitleLabel);
            this.topPanel.Dock = System.Windows.Forms.DockStyle.Top;
            this.topPanel.Location = new System.Drawing.Point(0, 0);
            this.topPanel.Name = "topPanel";
            this.topPanel.Size = new System.Drawing.Size(384, 62);
            this.topPanel.TabIndex = 4;
            // 
            // workSpacePanel
            // 
            this.workSpacePanel.Location = new System.Drawing.Point(0, 62);
            this.workSpacePanel.Name = "workSpacePanel";
            this.workSpacePanel.Size = new System.Drawing.Size(384, 132);
            this.workSpacePanel.TabIndex = 5;
            // 
            // SelectServerIpForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(384, 194);
            this.Controls.Add(this.workSpacePanel);
            this.Controls.Add(this.topPanel);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Name = "SelectServerIpForm";
            this.Text = "SelectServerIpForm";
            this.topPanel.ResumeLayout(false);
            this.topPanel.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion
        private System.Windows.Forms.Label formTitleLabel;
        private System.Windows.Forms.Panel topPanel;
        private System.Windows.Forms.Panel workSpacePanel;
    }
}