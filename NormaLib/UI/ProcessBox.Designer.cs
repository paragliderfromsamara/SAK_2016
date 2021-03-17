namespace NormaLib.UI
{
    partial class ProcessBox
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
            this.components = new System.ComponentModel.Container();
            this.processNameLbl = new System.Windows.Forms.Label();
            this.processIndicator = new System.Windows.Forms.Label();
            this.processTimer = new System.Windows.Forms.Timer(this.components);
            this.SuspendLayout();
            // 
            // processNameLbl
            // 
            this.processNameLbl.Font = new System.Drawing.Font("Microsoft Sans Serif", 15.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.processNameLbl.ForeColor = System.Drawing.Color.White;
            this.processNameLbl.Location = new System.Drawing.Point(12, 15);
            this.processNameLbl.Name = "processNameLbl";
            this.processNameLbl.Size = new System.Drawing.Size(314, 72);
            this.processNameLbl.TabIndex = 0;
            this.processNameLbl.Text = "Название процесса";
            this.processNameLbl.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // processIndicator
            // 
            this.processIndicator.AutoSize = true;
            this.processIndicator.Font = new System.Drawing.Font("Microsoft Sans Serif", 36F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.processIndicator.ForeColor = System.Drawing.SystemColors.ButtonHighlight;
            this.processIndicator.Location = new System.Drawing.Point(131, 90);
            this.processIndicator.Name = "processIndicator";
            this.processIndicator.Size = new System.Drawing.Size(75, 55);
            this.processIndicator.TabIndex = 1;
            this.processIndicator.Text = "•••";
            // 
            // ProcessBox
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(17)))), ((int)(((byte)(148)))), ((int)(((byte)(255)))));
            this.ClientSize = new System.Drawing.Size(338, 170);
            this.Controls.Add(this.processIndicator);
            this.Controls.Add(this.processNameLbl);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Name = "ProcessBox";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "ProcessBox";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label processNameLbl;
        private System.Windows.Forms.Label processIndicator;
        private System.Windows.Forms.Timer processTimer;
    }
}