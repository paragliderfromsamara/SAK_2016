namespace NormaLib.Devices.Teraohmmeter
{
    partial class TeraDeviceControlForm
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
            this.RLModeButton = new System.Windows.Forms.Button();
            this.RModeButton = new System.Windows.Forms.Button();
            this.pVModeButton = new System.Windows.Forms.Button();
            this.pSButton = new System.Windows.Forms.Button();
            this.deviceModesPanel.SuspendLayout();
            this.ContentPanel.SuspendLayout();
            this.SuspendLayout();
            // 
            // deviceModesPanel
            // 
            this.deviceModesPanel.Controls.Add(this.pSButton);
            this.deviceModesPanel.Controls.Add(this.pVModeButton);
            this.deviceModesPanel.Controls.Add(this.RLModeButton);
            this.deviceModesPanel.Controls.Add(this.RModeButton);
            // 
            // RLModeButton
            // 
            this.RLModeButton.BackColor = System.Drawing.Color.Turquoise;
            this.RLModeButton.Dock = System.Windows.Forms.DockStyle.Top;
            this.RLModeButton.FlatAppearance.BorderSize = 0;
            this.RLModeButton.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.RLModeButton.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.RLModeButton.ForeColor = System.Drawing.SystemColors.ActiveCaptionText;
            this.RLModeButton.Location = new System.Drawing.Point(0, 50);
            this.RLModeButton.Name = "RLModeButton";
            this.RLModeButton.Size = new System.Drawing.Size(60, 50);
            this.RLModeButton.TabIndex = 0;
            this.RLModeButton.Text = "RL";
            this.RLModeButton.UseVisualStyleBackColor = false;
            // 
            // RModeButton
            // 
            this.RModeButton.BackColor = System.Drawing.Color.Turquoise;
            this.RModeButton.Dock = System.Windows.Forms.DockStyle.Top;
            this.RModeButton.FlatAppearance.BorderSize = 0;
            this.RModeButton.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.RModeButton.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.RModeButton.ForeColor = System.Drawing.SystemColors.ActiveCaptionText;
            this.RModeButton.Location = new System.Drawing.Point(0, 0);
            this.RModeButton.Name = "RModeButton";
            this.RModeButton.Size = new System.Drawing.Size(60, 50);
            this.RModeButton.TabIndex = 1;
            this.RModeButton.Text = "R";
            this.RModeButton.UseVisualStyleBackColor = false;
            // 
            // pVModeButton
            // 
            this.pVModeButton.BackColor = System.Drawing.Color.Turquoise;
            this.pVModeButton.Dock = System.Windows.Forms.DockStyle.Top;
            this.pVModeButton.FlatAppearance.BorderSize = 0;
            this.pVModeButton.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.pVModeButton.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.pVModeButton.ForeColor = System.Drawing.SystemColors.ActiveCaptionText;
            this.pVModeButton.Location = new System.Drawing.Point(0, 100);
            this.pVModeButton.Name = "pVModeButton";
            this.pVModeButton.Size = new System.Drawing.Size(60, 50);
            this.pVModeButton.TabIndex = 2;
            this.pVModeButton.Text = "ρV";
            this.pVModeButton.UseVisualStyleBackColor = false;
            // 
            // pSButton
            // 
            this.pSButton.BackColor = System.Drawing.Color.Turquoise;
            this.pSButton.Dock = System.Windows.Forms.DockStyle.Top;
            this.pSButton.FlatAppearance.BorderSize = 0;
            this.pSButton.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.pSButton.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.pSButton.ForeColor = System.Drawing.SystemColors.ActiveCaptionText;
            this.pSButton.Location = new System.Drawing.Point(0, 150);
            this.pSButton.Name = "pSButton";
            this.pSButton.Size = new System.Drawing.Size(60, 50);
            this.pSButton.TabIndex = 3;
            this.pSButton.Text = "ρS";
            this.pSButton.UseVisualStyleBackColor = false;
            // 
            // TeraDeviceControlForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(825, 569);
            this.Name = "TeraDeviceControlForm";
            this.Text = "TeraDeviceControlForm";
            this.deviceModesPanel.ResumeLayout(false);
            this.ContentPanel.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button RLModeButton;
        private System.Windows.Forms.Button pSButton;
        private System.Windows.Forms.Button pVModeButton;
        private System.Windows.Forms.Button RModeButton;
    }
}