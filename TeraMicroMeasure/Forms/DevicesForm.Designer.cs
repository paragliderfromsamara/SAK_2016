namespace TeraMicroMeasure.Forms
{
    partial class DevicesForm
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
            this.examplePanel = new System.Windows.Forms.Panel();
            this.versionLabel = new System.Windows.Forms.Label();
            this.deviceNameLabel = new System.Windows.Forms.Label();
            this.deviceSerialLabel = new System.Windows.Forms.Label();
            this.deviceStatus = new System.Windows.Forms.Label();
            this.devicePanelsContainer = new System.Windows.Forms.Panel();
            this.examplePanel.SuspendLayout();
            this.devicePanelsContainer.SuspendLayout();
            this.SuspendLayout();
            // 
            // examplePanel
            // 
            this.examplePanel.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(49)))), ((int)(((byte)(117)))));
            this.examplePanel.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.examplePanel.Controls.Add(this.deviceNameLabel);
            this.examplePanel.Controls.Add(this.versionLabel);
            this.examplePanel.Controls.Add(this.deviceSerialLabel);
            this.examplePanel.Controls.Add(this.deviceStatus);
            this.examplePanel.Cursor = System.Windows.Forms.Cursors.Hand;
            this.examplePanel.ForeColor = System.Drawing.SystemColors.ControlLight;
            this.examplePanel.Location = new System.Drawing.Point(12, 64);
            this.examplePanel.Name = "examplePanel";
            this.examplePanel.Size = new System.Drawing.Size(300, 180);
            this.examplePanel.TabIndex = 0;

            // 
            // versionLabel
            // 
            this.versionLabel.AutoSize = true;
            this.versionLabel.Location = new System.Drawing.Point(22, 102);
            this.versionLabel.Name = "versionLabel";
            this.versionLabel.Size = new System.Drawing.Size(97, 13);
            this.versionLabel.TabIndex = 4;
            this.versionLabel.Text = "Версия прошивки";
            // 
            // deviceNameLabel
            // 
            this.deviceNameLabel.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.deviceNameLabel.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.deviceNameLabel.Location = new System.Drawing.Point(0, 0);
            this.deviceNameLabel.Name = "deviceNameLabel";
            this.deviceNameLabel.Size = new System.Drawing.Size(296, 55);
            this.deviceNameLabel.TabIndex = 3;
            this.deviceNameLabel.Text = "Тераомметр ТОмМ-01";
            this.deviceNameLabel.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            
            // 
            // deviceSerialLabel
            // 
            this.deviceSerialLabel.AutoSize = true;
            this.deviceSerialLabel.Font = new System.Drawing.Font("Microsoft Sans Serif", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.deviceSerialLabel.Location = new System.Drawing.Point(22, 64);
            this.deviceSerialLabel.Name = "deviceSerialLabel";
            this.deviceSerialLabel.Size = new System.Drawing.Size(187, 18);
            this.deviceSerialLabel.TabIndex = 1;
            this.deviceSerialLabel.Text = "Серийный номер: 2021-05";
            this.deviceSerialLabel.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // deviceStatus
            // 
            this.deviceStatus.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.deviceStatus.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(120)))), ((int)(((byte)(200)))));
            this.deviceStatus.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.deviceStatus.ForeColor = System.Drawing.SystemColors.Control;
            this.deviceStatus.Location = new System.Drawing.Point(0, 137);
            this.deviceStatus.Name = "deviceStatus";
            this.deviceStatus.Size = new System.Drawing.Size(296, 39);
            this.deviceStatus.TabIndex = 2;
            this.deviceStatus.Text = "Статус";
            this.deviceStatus.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // devicePanelsContainer
            // 
            this.devicePanelsContainer.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.devicePanelsContainer.AutoScroll = true;
            this.devicePanelsContainer.Controls.Add(this.examplePanel);
            this.devicePanelsContainer.Location = new System.Drawing.Point(0, 0);
            this.devicePanelsContainer.Name = "devicePanelsContainer";
            this.devicePanelsContainer.Size = new System.Drawing.Size(978, 670);
            this.devicePanelsContainer.TabIndex = 1;
            this.devicePanelsContainer.Resize += new System.EventHandler(this.devicePanelsContainer_Resize);
            // 
            // DevicesForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.AutoScroll = true;
            this.ClientSize = new System.Drawing.Size(978, 670);
            this.Controls.Add(this.devicePanelsContainer);
            this.Name = "DevicesForm";
            this.Text = "Подключенные приборы";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.DevicesForm_FormClosing);
            this.examplePanel.ResumeLayout(false);
            this.examplePanel.PerformLayout();
            this.devicePanelsContainer.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel examplePanel;
        private System.Windows.Forms.Label deviceStatus;
        private System.Windows.Forms.Panel devicePanelsContainer;
        private System.Windows.Forms.Label versionLabel;
        private System.Windows.Forms.Label deviceNameLabel;
        private System.Windows.Forms.Label deviceSerialLabel;
    }
}