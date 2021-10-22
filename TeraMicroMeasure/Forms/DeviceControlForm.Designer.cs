namespace TeraMicroMeasure.Forms
{
    partial class DeviceControlForm
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
            this.deviceNameLabel = new System.Windows.Forms.Label();
            this.serialLabel = new System.Windows.Forms.Label();
            this.panelMenu.SuspendLayout();
            this.panelHeader.SuspendLayout();
            this.SuspendLayout();
            // 
            // panelMenu
            // 
            this.panelMenu.Size = new System.Drawing.Size(240, 861);
            // 
            // panelHeader
            // 
            this.panelHeader.Controls.Add(this.serialLabel);
            this.panelHeader.Controls.Add(this.deviceNameLabel);
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
            // deviceNameLabel
            // 
            this.deviceNameLabel.Dock = System.Windows.Forms.DockStyle.Top;
            this.deviceNameLabel.Font = new System.Drawing.Font("Microsoft Sans Serif", 12.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.deviceNameLabel.ForeColor = System.Drawing.Color.WhiteSmoke;
            this.deviceNameLabel.Location = new System.Drawing.Point(0, 0);
            this.deviceNameLabel.Name = "deviceNameLabel";
            this.deviceNameLabel.Size = new System.Drawing.Size(240, 37);
            this.deviceNameLabel.TabIndex = 0;
            this.deviceNameLabel.Text = "Тераомметр ТОмМ-01";
            this.deviceNameLabel.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // serialLabel
            // 
            this.serialLabel.AutoSize = true;
            this.serialLabel.Dock = System.Windows.Forms.DockStyle.Top;
            this.serialLabel.Font = new System.Drawing.Font("Microsoft Sans Serif", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.serialLabel.ForeColor = System.Drawing.Color.WhiteSmoke;
            this.serialLabel.Location = new System.Drawing.Point(0, 37);
            this.serialLabel.Name = "serialLabel";
            this.serialLabel.Padding = new System.Windows.Forms.Padding(15, 0, 0, 0);
            this.serialLabel.Size = new System.Drawing.Size(125, 18);
            this.serialLabel.TabIndex = 1;
            this.serialLabel.Text = "Зав.№ 2021-10";
            // 
            // DeviceControlForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1035, 861);
            this.Location = new System.Drawing.Point(0, 0);
            this.Name = "DeviceControlForm";
            this.Text = "DeviceControlForm";
            this.panelMenu.ResumeLayout(false);
            this.panelHeader.ResumeLayout(false);
            this.panelHeader.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Label serialLabel;
        private System.Windows.Forms.Label deviceNameLabel;
    }
}