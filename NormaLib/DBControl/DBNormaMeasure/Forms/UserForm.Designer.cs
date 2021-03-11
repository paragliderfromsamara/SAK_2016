namespace NormaLib.DBControl.DBNormaMeasure.Forms
{
    partial class UserForm
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
            this.label1 = new System.Windows.Forms.Label();
            this.name = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.thirdName = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.tabNum = new System.Windows.Forms.TextBox();
            this.label4 = new System.Windows.Forms.Label();
            this.saveBurtton = new System.Windows.Forms.Button();
            this.cancelButton = new System.Windows.Forms.Button();
            this.passwodText = new System.Windows.Forms.TextBox();
            this.label5 = new System.Windows.Forms.Label();
            this.userRole = new System.Windows.Forms.ComboBox();
            this.label6 = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // lastName
            // 
            this.lastName.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.lastName.Font = new System.Drawing.Font("Microsoft Sans Serif", 11F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.lastName.Location = new System.Drawing.Point(23, 46);
            this.lastName.Name = "lastName";
            this.lastName.Size = new System.Drawing.Size(184, 24);
            this.lastName.TabIndex = 0;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(19, 25);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(73, 18);
            this.label1.TabIndex = 1;
            this.label1.Text = "Фамилия";
            // 
            // name
            // 
            this.name.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.name.Font = new System.Drawing.Font("Microsoft Sans Serif", 11F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.name.Location = new System.Drawing.Point(239, 46);
            this.name.Name = "name";
            this.name.Size = new System.Drawing.Size(185, 24);
            this.name.TabIndex = 1;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(236, 25);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(38, 18);
            this.label2.TabIndex = 3;
            this.label2.Text = "Имя";
            // 
            // thirdName
            // 
            this.thirdName.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.thirdName.Font = new System.Drawing.Font("Microsoft Sans Serif", 11F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.thirdName.Location = new System.Drawing.Point(22, 107);
            this.thirdName.Name = "thirdName";
            this.thirdName.Size = new System.Drawing.Size(185, 24);
            this.thirdName.TabIndex = 2;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(19, 86);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(75, 18);
            this.label3.TabIndex = 5;
            this.label3.Text = "Отчество";
            // 
            // tabNum
            // 
            this.tabNum.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.tabNum.Font = new System.Drawing.Font("Microsoft Sans Serif", 11F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.tabNum.Location = new System.Drawing.Point(239, 107);
            this.tabNum.Name = "tabNum";
            this.tabNum.Size = new System.Drawing.Size(185, 24);
            this.tabNum.TabIndex = 3;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(236, 86);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(134, 18);
            this.label4.TabIndex = 7;
            this.label4.Text = "Табельный номер";
            // 
            // saveBurtton
            // 
            this.saveBurtton.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(82)))), ((int)(((byte)(214)))), ((int)(((byte)(37)))));
            this.saveBurtton.DialogResult = System.Windows.Forms.DialogResult.OK;
            this.saveBurtton.FlatAppearance.BorderSize = 0;
            this.saveBurtton.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.saveBurtton.ForeColor = System.Drawing.Color.White;
            this.saveBurtton.Location = new System.Drawing.Point(22, 245);
            this.saveBurtton.Name = "saveBurtton";
            this.saveBurtton.Size = new System.Drawing.Size(185, 40);
            this.saveBurtton.TabIndex = 6;
            this.saveBurtton.Text = "Сохранить";
            this.saveBurtton.UseVisualStyleBackColor = false;
            this.saveBurtton.Click += new System.EventHandler(this.saveBurtton_Click);
            // 
            // cancelButton
            // 
            this.cancelButton.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(9)))), ((int)(((byte)(44)))), ((int)(((byte)(91)))));
            this.cancelButton.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.cancelButton.FlatAppearance.BorderSize = 0;
            this.cancelButton.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.cancelButton.ForeColor = System.Drawing.Color.Gainsboro;
            this.cancelButton.Location = new System.Drawing.Point(239, 245);
            this.cancelButton.Name = "cancelButton";
            this.cancelButton.Size = new System.Drawing.Size(185, 40);
            this.cancelButton.TabIndex = 7;
            this.cancelButton.Text = "Отмена";
            this.cancelButton.UseVisualStyleBackColor = false;
            // 
            // passwodText
            // 
            this.passwodText.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.passwodText.Font = new System.Drawing.Font("Microsoft Sans Serif", 11F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.passwodText.Location = new System.Drawing.Point(22, 181);
            this.passwodText.Name = "passwodText";
            this.passwodText.Size = new System.Drawing.Size(185, 24);
            this.passwodText.TabIndex = 4;
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(20, 160);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(61, 18);
            this.label5.TabIndex = 11;
            this.label5.Text = "Пароль";
            // 
            // userRole
            // 
            this.userRole.AccessibleRole = System.Windows.Forms.AccessibleRole.None;
            this.userRole.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.userRole.Font = new System.Drawing.Font("Microsoft Sans Serif", 11F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.userRole.FormattingEnabled = true;
            this.userRole.Location = new System.Drawing.Point(239, 179);
            this.userRole.Name = "userRole";
            this.userRole.Size = new System.Drawing.Size(185, 26);
            this.userRole.TabIndex = 5;
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(236, 160);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(89, 18);
            this.label6.TabIndex = 13;
            this.label6.Text = "Должность";
            // 
            // UserForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(9F, 18F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.White;
            this.ClientSize = new System.Drawing.Size(451, 312);
            this.Controls.Add(this.label6);
            this.Controls.Add(this.userRole);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.passwodText);
            this.Controls.Add(this.cancelButton);
            this.Controls.Add(this.saveBurtton);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.tabNum);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.thirdName);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.name);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.lastName);
            this.Font = new System.Drawing.Font("Microsoft Sans Serif", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(55)))), ((int)(((byte)(96)))));
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.Margin = new System.Windows.Forms.Padding(4);
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "UserForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "UserForm";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.UserForm_FormClosing);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TextBox lastName;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox name;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox thirdName;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.TextBox tabNum;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Button saveBurtton;
        private System.Windows.Forms.Button cancelButton;
        private System.Windows.Forms.TextBox passwodText;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.ComboBox userRole;
        private System.Windows.Forms.Label label6;
    }
}