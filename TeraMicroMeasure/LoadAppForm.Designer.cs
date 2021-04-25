namespace TeraMicroMeasure
{
    partial class LoadAppForm
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
            this.primaryTaskLabel = new System.Windows.Forms.Label();
            this.subTaskLabel = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // primaryTaskLabel
            // 
            this.primaryTaskLabel.BackColor = System.Drawing.Color.Transparent;
            this.primaryTaskLabel.Location = new System.Drawing.Point(30, 143);
            this.primaryTaskLabel.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.primaryTaskLabel.Name = "primaryTaskLabel";
            this.primaryTaskLabel.Size = new System.Drawing.Size(340, 20);
            this.primaryTaskLabel.TabIndex = 0;
            this.primaryTaskLabel.Text = "Загрузка";
            // 
            // subTaskLabel
            // 
            this.subTaskLabel.BackColor = System.Drawing.Color.Transparent;
            this.subTaskLabel.Location = new System.Drawing.Point(30, 164);
            this.subTaskLabel.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.subTaskLabel.Name = "subTaskLabel";
            this.subTaskLabel.Size = new System.Drawing.Size(340, 20);
            this.subTaskLabel.TabIndex = 1;
            // 
            // LoadAppForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(18)))), ((int)(((byte)(51)))), ((int)(((byte)(102)))));
            this.BackgroundImage = global::NormaMeasure.Properties.Resources.ЗаставкаПОv12;
            this.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.ClientSize = new System.Drawing.Size(420, 230);
            this.Controls.Add(this.subTaskLabel);
            this.Controls.Add(this.primaryTaskLabel);
            this.DoubleBuffered = true;
            this.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.ForeColor = System.Drawing.Color.White;
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Margin = new System.Windows.Forms.Padding(4);
            this.Name = "LoadAppForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Загрузка...";
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Label primaryTaskLabel;
        private System.Windows.Forms.Label subTaskLabel;
    }
}