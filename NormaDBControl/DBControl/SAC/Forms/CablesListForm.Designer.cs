namespace NormaMeasure.DBControl.SAC.Forms
{
    partial class CablesListForm
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
            this.newCableFormButton = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // newCableFormButton
            // 
            this.newCableFormButton.Location = new System.Drawing.Point(22, 223);
            this.newCableFormButton.Name = "newCableFormButton";
            this.newCableFormButton.Size = new System.Drawing.Size(94, 26);
            this.newCableFormButton.TabIndex = 0;
            this.newCableFormButton.Text = "Добавить";
            this.newCableFormButton.UseVisualStyleBackColor = true;
            this.newCableFormButton.Click += new System.EventHandler(this.newCableFormButton_Click);
            // 
            // CablesListForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(582, 261);
            this.Controls.Add(this.newCableFormButton);
            this.Name = "CablesListForm";
            this.Text = "CablesListForm";
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button newCableFormButton;
    }
}