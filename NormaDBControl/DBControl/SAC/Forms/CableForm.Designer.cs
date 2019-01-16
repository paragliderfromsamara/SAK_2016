namespace NormaMeasure.DBControl.SAC.Forms
{
    partial class CableForm
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
            this.saveCableButton = new System.Windows.Forms.Button();
            this.cableNameTextField = new System.Windows.Forms.TextBox();
            this.SuspendLayout();
            // 
            // saveCableButton
            // 
            this.saveCableButton.Location = new System.Drawing.Point(12, 217);
            this.saveCableButton.Name = "saveCableButton";
            this.saveCableButton.Size = new System.Drawing.Size(112, 32);
            this.saveCableButton.TabIndex = 0;
            this.saveCableButton.Text = "Сохранить";
            this.saveCableButton.UseVisualStyleBackColor = true;
            this.saveCableButton.Click += new System.EventHandler(this.saveCableButton_Click);
            // 
            // cableNameTextField
            // 
            this.cableNameTextField.Location = new System.Drawing.Point(12, 23);
            this.cableNameTextField.Name = "cableNameTextField";
            this.cableNameTextField.Size = new System.Drawing.Size(100, 20);
            this.cableNameTextField.TabIndex = 1;
            this.cableNameTextField.TextChanged += new System.EventHandler(this.cableNameTextField_TextChanged);
            // 
            // CableForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(852, 261);
            this.Controls.Add(this.cableNameTextField);
            this.Controls.Add(this.saveCableButton);
            this.Name = "CableForm";
            this.Text = "CableForm";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button saveCableButton;
        private System.Windows.Forms.TextBox cableNameTextField;
    }
}