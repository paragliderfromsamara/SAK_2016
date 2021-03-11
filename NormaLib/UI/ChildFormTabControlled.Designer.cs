namespace NormaLib.UI
{
    partial class ChildFormTabControlled
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
            this.settingsTab = new System.Windows.Forms.TabControl();
            this.SuspendLayout();
            // 
            // settingsTab
            // 
            this.settingsTab.Dock = System.Windows.Forms.DockStyle.Fill;
            this.settingsTab.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.settingsTab.Location = new System.Drawing.Point(0, 0);
            this.settingsTab.Name = "settingsTab";
            this.settingsTab.Padding = new System.Drawing.Point(25, 15);
            this.settingsTab.SelectedIndex = 0;
            this.settingsTab.Size = new System.Drawing.Size(1029, 546);
            this.settingsTab.TabIndex = 0;
            this.settingsTab.SelectedIndexChanged += new System.EventHandler(this.settingsTab_SelectedIndexChanged);
            this.settingsTab.Selecting += new System.Windows.Forms.TabControlCancelEventHandler(this.settingsTab_Selecting);
            // 
            // ChildFormTabControlled
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1029, 546);
            this.Controls.Add(this.settingsTab);
            this.Name = "ChildFormTabControlled";
            this.Text = "НАСТРОЙКИ";
            this.ResumeLayout(false);

        }

        #endregion

        protected System.Windows.Forms.TabControl settingsTab;
    }
}