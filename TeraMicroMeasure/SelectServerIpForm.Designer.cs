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
            this.workSpacePanel = new System.Windows.Forms.Panel();
            this.SuspendLayout();
            // 
            // workSpacePanel
            // 
            this.workSpacePanel.Dock = System.Windows.Forms.DockStyle.Fill;
            this.workSpacePanel.Location = new System.Drawing.Point(0, 0);
            this.workSpacePanel.Name = "workSpacePanel";
            this.workSpacePanel.Size = new System.Drawing.Size(344, 116);
            this.workSpacePanel.TabIndex = 5;
            // 
            // SelectServerIpForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(344, 116);
            this.ControlBox = false;
            this.Controls.Add(this.workSpacePanel);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow;
            this.MaximizeBox = false;
            this.Name = "SelectServerIpForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Настройки соединения";
            this.ResumeLayout(false);

        }

        #endregion
        private System.Windows.Forms.Panel workSpacePanel;
    }
}