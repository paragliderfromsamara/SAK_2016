namespace NormaMeasure.MeasureControl.SACMeasureForms
{
    partial class SACHandMeasureForm
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
            this.measureControlButton = new System.Windows.Forms.Button();
            this.resultLabel = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // measureControlButton
            // 
            this.measureControlButton.Location = new System.Drawing.Point(39, 226);
            this.measureControlButton.Name = "measureControlButton";
            this.measureControlButton.Size = new System.Drawing.Size(75, 23);
            this.measureControlButton.TabIndex = 0;
            this.measureControlButton.Text = "Пуск";
            this.measureControlButton.UseVisualStyleBackColor = true;
            // 
            // resultLabel
            // 
            this.resultLabel.AutoSize = true;
            this.resultLabel.Font = new System.Drawing.Font("Microsoft Sans Serif", 27.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.resultLabel.Location = new System.Drawing.Point(32, 110);
            this.resultLabel.Name = "resultLabel";
            this.resultLabel.Size = new System.Drawing.Size(118, 42);
            this.resultLabel.TabIndex = 1;
            this.resultLabel.Text = "label1";
            // 
            // SACHandMeasureForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(487, 261);
            this.Controls.Add(this.resultLabel);
            this.Controls.Add(this.measureControlButton);
            this.Name = "SACHandMeasureForm";
            this.Text = "SACHandMeasureForm";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button measureControlButton;
        private System.Windows.Forms.Label resultLabel;
    }
}