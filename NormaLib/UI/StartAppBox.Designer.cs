namespace NormaLib.UI
{
    partial class StartAppBox
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
            this.statusText = new System.Windows.Forms.Label();
            this.processTimer = new System.Windows.Forms.Timer(this.components);
            this.SuspendLayout();
            // 
            // processNameLbl
            // 
            this.processNameLbl.Font = new System.Drawing.Font("Microsoft Sans Serif", 15.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.processNameLbl.ForeColor = System.Drawing.Color.White;
            this.processNameLbl.Location = new System.Drawing.Point(12, 9);
            this.processNameLbl.Name = "processNameLbl";
            this.processNameLbl.Size = new System.Drawing.Size(236, 72);
            this.processNameLbl.TabIndex = 0;
            this.processNameLbl.Text = "НОРМА.Измерения";
            this.processNameLbl.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // statusText
            // 
            this.statusText.AutoSize = true;
            this.statusText.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.statusText.ForeColor = System.Drawing.SystemColors.ButtonHighlight;
            this.statusText.Location = new System.Drawing.Point(14, 126);
            this.statusText.Name = "statusText";
            this.statusText.Size = new System.Drawing.Size(54, 16);
            this.statusText.TabIndex = 1;
            this.statusText.Text = "Статус";
            // 
            // StartAppBox
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(17)))), ((int)(((byte)(148)))), ((int)(((byte)(255)))));
            this.ClientSize = new System.Drawing.Size(338, 170);
            this.Controls.Add(this.statusText);
            this.Controls.Add(this.processNameLbl);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Name = "StartAppBox";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "ProcessBox";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label processNameLbl;
        private System.Windows.Forms.Label statusText;
        private System.Windows.Forms.Timer processTimer;
    }
}