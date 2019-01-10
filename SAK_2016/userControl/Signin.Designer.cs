namespace NormaMeasure.SAC_APP
{
    partial class Signin
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
            this.lastName = new System.Windows.Forms.TextBox();
            this.tabNum = new System.Windows.Forms.TextBox();
            this.password = new System.Windows.Forms.TextBox();
            this.nameLbl = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.button1 = new System.Windows.Forms.Button();
            this.button2 = new System.Windows.Forms.Button();
            this.signedUser = new System.Data.DataSet();
            ((System.ComponentModel.ISupportInitialize)(this.signedUser)).BeginInit();
            this.SuspendLayout();
            // 
            // lastName
            // 
            this.lastName.Location = new System.Drawing.Point(16, 51);
            this.lastName.Name = "lastName";
            this.lastName.Size = new System.Drawing.Size(100, 20);
            this.lastName.TabIndex = 0;
            this.lastName.Text = "Sysadmin";
            // 
            // tabNum
            // 
            this.tabNum.Location = new System.Drawing.Point(137, 51);
            this.tabNum.Name = "tabNum";
            this.tabNum.Size = new System.Drawing.Size(100, 20);
            this.tabNum.TabIndex = 1;
            this.tabNum.Text = "0";
            // 
            // password
            // 
            this.password.Location = new System.Drawing.Point(259, 51);
            this.password.Name = "password";
            this.password.PasswordChar = '*';
            this.password.Size = new System.Drawing.Size(100, 20);
            this.password.TabIndex = 2;
            this.password.Text = "123456";
            // 
            // nameLbl
            // 
            this.nameLbl.AutoSize = true;
            this.nameLbl.Location = new System.Drawing.Point(13, 35);
            this.nameLbl.Name = "nameLbl";
            this.nameLbl.Size = new System.Drawing.Size(56, 13);
            this.nameLbl.TabIndex = 3;
            this.nameLbl.Text = "Фамилия";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(134, 35);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(99, 13);
            this.label1.TabIndex = 4;
            this.label1.Text = "Табельный номер";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(256, 35);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(45, 13);
            this.label2.TabIndex = 5;
            this.label2.Text = "Пароль";
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(16, 96);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(75, 23);
            this.button1.TabIndex = 6;
            this.button1.Text = "Вход";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // button2
            // 
            this.button2.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.button2.Location = new System.Drawing.Point(106, 96);
            this.button2.Name = "button2";
            this.button2.Size = new System.Drawing.Size(75, 23);
            this.button2.TabIndex = 7;
            this.button2.Text = "Отмена";
            this.button2.UseVisualStyleBackColor = true;
            // 
            // signedUser
            // 
            this.signedUser.DataSetName = "NewDataSet";
            // 
            // Signin
            // 
            this.AcceptButton = this.button1;
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.CancelButton = this.button2;
            this.ClientSize = new System.Drawing.Size(378, 148);
            this.ControlBox = false;
            this.Controls.Add(this.button2);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.nameLbl);
            this.Controls.Add(this.password);
            this.Controls.Add(this.tabNum);
            this.Controls.Add(this.lastName);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow;
            this.Name = "Signin";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Аутентификация";
            ((System.ComponentModel.ISupportInitialize)(this.signedUser)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TextBox lastName;
        private System.Windows.Forms.TextBox tabNum;
        private System.Windows.Forms.TextBox password;
        private System.Windows.Forms.Label nameLbl;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.Button button2;
        private System.Data.DataSet signedUser;
    }
}