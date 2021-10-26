namespace NormaLib.UI
{
    partial class DeviceControlFormBase
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(DeviceControlFormBase));
            this.panel1 = new System.Windows.Forms.Panel();
            this.panelTitleBar = new System.Windows.Forms.Panel();
            this.MaximizeAppButton = new System.Windows.Forms.Button();
            this.MinimizeAppButton = new System.Windows.Forms.Button();
            this.CloseAppButton = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.deviceModesPanel = new System.Windows.Forms.Panel();
            this.currentModePanel = new System.Windows.Forms.Panel();
            this.ContentPanel.SuspendLayout();
            this.panel1.SuspendLayout();
            this.panelTitleBar.SuspendLayout();
            this.SuspendLayout();
            // 
            // ContentPanel
            // 
            this.ContentPanel.Controls.Add(this.currentModePanel);
            this.ContentPanel.Controls.Add(this.deviceModesPanel);
            this.ContentPanel.Controls.Add(this.panel1);
            // 
            // panel1
            // 
            this.panel1.BackColor = System.Drawing.Color.LightSeaGreen;
            this.panel1.Controls.Add(this.panelTitleBar);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel1.Location = new System.Drawing.Point(0, 0);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(823, 31);
            this.panel1.TabIndex = 0;
            // 
            // panelTitleBar
            // 
            this.panelTitleBar.Controls.Add(this.MaximizeAppButton);
            this.panelTitleBar.Controls.Add(this.MinimizeAppButton);
            this.panelTitleBar.Controls.Add(this.CloseAppButton);
            this.panelTitleBar.Controls.Add(this.label1);
            this.panelTitleBar.Dock = System.Windows.Forms.DockStyle.Top;
            this.panelTitleBar.Location = new System.Drawing.Point(0, 0);
            this.panelTitleBar.Name = "panelTitleBar";
            this.panelTitleBar.Size = new System.Drawing.Size(823, 31);
            this.panelTitleBar.TabIndex = 7;
            // 
            // MaximizeAppButton
            // 
            this.MaximizeAppButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.MaximizeAppButton.BackColor = System.Drawing.Color.Transparent;
            this.MaximizeAppButton.FlatAppearance.BorderSize = 0;
            this.MaximizeAppButton.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.MaximizeAppButton.Image = ((System.Drawing.Image)(resources.GetObject("MaximizeAppButton.Image")));
            this.MaximizeAppButton.Location = new System.Drawing.Point(713, 0);
            this.MaximizeAppButton.Margin = new System.Windows.Forms.Padding(4);
            this.MaximizeAppButton.Name = "MaximizeAppButton";
            this.MaximizeAppButton.Size = new System.Drawing.Size(53, 31);
            this.MaximizeAppButton.TabIndex = 5;
            this.MaximizeAppButton.UseVisualStyleBackColor = false;
            this.MaximizeAppButton.Click += new System.EventHandler(this.MaximizeAppButton_Click);
            // 
            // MinimizeAppButton
            // 
            this.MinimizeAppButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.MinimizeAppButton.BackColor = System.Drawing.Color.Transparent;
            this.MinimizeAppButton.FlatAppearance.BorderSize = 0;
            this.MinimizeAppButton.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.MinimizeAppButton.Image = ((System.Drawing.Image)(resources.GetObject("MinimizeAppButton.Image")));
            this.MinimizeAppButton.Location = new System.Drawing.Point(652, 0);
            this.MinimizeAppButton.Margin = new System.Windows.Forms.Padding(4);
            this.MinimizeAppButton.Name = "MinimizeAppButton";
            this.MinimizeAppButton.Size = new System.Drawing.Size(53, 31);
            this.MinimizeAppButton.TabIndex = 6;
            this.MinimizeAppButton.UseVisualStyleBackColor = false;
            this.MinimizeAppButton.Click += new System.EventHandler(this.MinimizeAppButton_Click);
            // 
            // CloseAppButton
            // 
            this.CloseAppButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.CloseAppButton.BackColor = System.Drawing.Color.Transparent;
            this.CloseAppButton.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Center;
            this.CloseAppButton.FlatAppearance.BorderSize = 0;
            this.CloseAppButton.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.CloseAppButton.Image = ((System.Drawing.Image)(resources.GetObject("CloseAppButton.Image")));
            this.CloseAppButton.Location = new System.Drawing.Point(770, 0);
            this.CloseAppButton.Margin = new System.Windows.Forms.Padding(4);
            this.CloseAppButton.Name = "CloseAppButton";
            this.CloseAppButton.Size = new System.Drawing.Size(53, 31);
            this.CloseAppButton.TabIndex = 4;
            this.CloseAppButton.UseVisualStyleBackColor = false;
            this.CloseAppButton.Click += new System.EventHandler(this.CloseAppButton_Click);
            // 
            // label1
            // 
            this.label1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 11.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.label1.ForeColor = System.Drawing.Color.WhiteSmoke;
            this.label1.ImageAlign = System.Drawing.ContentAlignment.TopLeft;
            this.label1.Location = new System.Drawing.Point(0, 0);
            this.label1.Margin = new System.Windows.Forms.Padding(15, 0, 3, 0);
            this.label1.Name = "label1";
            this.label1.Padding = new System.Windows.Forms.Padding(15, 0, 0, 0);
            this.label1.Size = new System.Drawing.Size(823, 31);
            this.label1.TabIndex = 0;
            this.label1.Text = "Тераомметр ТОмМ-01 2021-02";
            this.label1.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.label1.MouseDown += new System.Windows.Forms.MouseEventHandler(this.panelTitleBar_MouseDown);
            // 
            // deviceModesPanel
            // 
            this.deviceModesPanel.BackColor = System.Drawing.Color.LightSeaGreen;
            this.deviceModesPanel.Dock = System.Windows.Forms.DockStyle.Left;
            this.deviceModesPanel.Location = new System.Drawing.Point(0, 31);
            this.deviceModesPanel.Name = "deviceModesPanel";
            this.deviceModesPanel.Size = new System.Drawing.Size(60, 536);
            this.deviceModesPanel.TabIndex = 8;
            // 
            // currentModePanel
            // 
            this.currentModePanel.BackColor = System.Drawing.SystemColors.HotTrack;
            this.currentModePanel.Dock = System.Windows.Forms.DockStyle.Fill;
            this.currentModePanel.Location = new System.Drawing.Point(60, 31);
            this.currentModePanel.Name = "currentModePanel";
            this.currentModePanel.Size = new System.Drawing.Size(763, 536);
            this.currentModePanel.TabIndex = 9;
            // 
            // DeviceControlFormBase
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.SystemColors.HotTrack;
            this.ClientSize = new System.Drawing.Size(825, 569);
            this.Name = "DeviceControlFormBase";
            this.Text = "DeviceControlFormBase";
            this.ContentPanel.ResumeLayout(false);
            this.panel1.ResumeLayout(false);
            this.panelTitleBar.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button MinimizeAppButton;
        private System.Windows.Forms.Button MaximizeAppButton;
        private System.Windows.Forms.Button CloseAppButton;
        private System.Windows.Forms.Panel panelTitleBar;
        protected System.Windows.Forms.Panel deviceModesPanel;
        private System.Windows.Forms.Panel currentModePanel;
    }
}