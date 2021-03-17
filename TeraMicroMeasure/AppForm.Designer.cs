namespace TeraMicroMeasure
{
    partial class AppForm
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
            this.clientTitle = new System.Windows.Forms.Label();
            this.titleLabel_2 = new System.Windows.Forms.Label();
            this.titleLabel_1 = new System.Windows.Forms.Label();
            this.panelMenu.SuspendLayout();
            this.panelHeader.SuspendLayout();
            this.SuspendLayout();
            // 
            // panelHeader
            // 
            this.panelHeader.Controls.Add(this.titleLabel_1);
            this.panelHeader.Controls.Add(this.titleLabel_2);
            this.panelHeader.Controls.Add(this.clientTitle);
            // 
            // clientTitle
            // 
            this.clientTitle.AutoSize = true;
            this.clientTitle.Font = new System.Drawing.Font("Microsoft Sans Serif", 20.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.clientTitle.ForeColor = System.Drawing.Color.White;
            this.clientTitle.Location = new System.Drawing.Point(12, 3);
            this.clientTitle.Name = "clientTitle";
            this.clientTitle.Size = new System.Drawing.Size(108, 31);
            this.clientTitle.TabIndex = 0;
            this.clientTitle.Text = "Сервер";
            this.clientTitle.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // titleLabel_2
            // 
            this.titleLabel_2.AutoSize = true;
            this.titleLabel_2.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.titleLabel_2.ForeColor = System.Drawing.SystemColors.ButtonHighlight;
            this.titleLabel_2.Location = new System.Drawing.Point(15, 57);
            this.titleLabel_2.Name = "titleLabel_2";
            this.titleLabel_2.Size = new System.Drawing.Size(181, 15);
            this.titleLabel_2.TabIndex = 1;
            this.titleLabel_2.Text = "IP 192.168.100.232 Порт: 4999";
            // 
            // titleLabel_1
            // 
            this.titleLabel_1.AutoSize = true;
            this.titleLabel_1.ForeColor = System.Drawing.SystemColors.ButtonHighlight;
            this.titleLabel_1.Location = new System.Drawing.Point(15, 40);
            this.titleLabel_1.Name = "titleLabel_1";
            this.titleLabel_1.Size = new System.Drawing.Size(55, 13);
            this.titleLabel_1.TabIndex = 2;
            this.titleLabel_1.Text = "Клиентов";
            // 
            // AppForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1224, 884);
            this.Location = new System.Drawing.Point(0, 0);
            this.Name = "AppForm";
            this.Text = "AppForm";
            this.panelMenu.ResumeLayout(false);
            this.panelHeader.ResumeLayout(false);
            this.panelHeader.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Label clientTitle;
        private System.Windows.Forms.Label titleLabel_1;
        private System.Windows.Forms.Label titleLabel_2;
    }
}