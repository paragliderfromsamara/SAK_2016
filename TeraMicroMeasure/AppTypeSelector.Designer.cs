namespace TeraMicroMeasure
{
    partial class AppTypeSelector
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
            this.serverLabelBut = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // serverLabelBut
            // 
            this.serverLabelBut.BackColor = System.Drawing.SystemColors.HotTrack;
            this.serverLabelBut.Cursor = System.Windows.Forms.Cursors.Hand;
            this.serverLabelBut.Font = new System.Drawing.Font("Tahoma", 15.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.serverLabelBut.ForeColor = System.Drawing.SystemColors.HighlightText;
            this.serverLabelBut.Location = new System.Drawing.Point(0, 0);
            this.serverLabelBut.Name = "serverLabelBut";
            this.serverLabelBut.Size = new System.Drawing.Size(300, 200);
            this.serverLabelBut.TabIndex = 0;
            this.serverLabelBut.Text = "С Е Р В Е Р";
            this.serverLabelBut.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.serverLabelBut.Click += new System.EventHandler(this.label1_Click);
            this.serverLabelBut.MouseLeave += new System.EventHandler(this.panel_MouseLeave);
            this.serverLabelBut.MouseHover += new System.EventHandler(this.panel_MouseHover);
            // 
            // label2
            // 
            this.label2.BackColor = System.Drawing.SystemColors.HotTrack;
            this.label2.Cursor = System.Windows.Forms.Cursors.Hand;
            this.label2.Font = new System.Drawing.Font("Tahoma", 15.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.label2.ForeColor = System.Drawing.SystemColors.ButtonHighlight;
            this.label2.Location = new System.Drawing.Point(300, 0);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(300, 200);
            this.label2.TabIndex = 0;
            this.label2.Text = "К Л И Е Н Т";
            this.label2.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.label2.Click += new System.EventHandler(this.label2_Click);
            this.label2.MouseLeave += new System.EventHandler(this.panel_MouseLeave);
            this.label2.MouseHover += new System.EventHandler(this.panel_MouseHover);
            // 
            // AppTypeSelector
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(600, 200);
            this.ControlBox = false;
            this.Controls.Add(this.label2);
            this.Controls.Add(this.serverLabelBut);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "AppTypeSelector";
            this.Text = "AppTypeSelector";
            this.TopMost = true;
            this.ResumeLayout(false);

        }

        #endregion
        private System.Windows.Forms.Label serverLabelBut;
        private System.Windows.Forms.Label label2;
    }
}