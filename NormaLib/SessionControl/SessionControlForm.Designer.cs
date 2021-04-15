namespace NormaLib.SessionControl
{
    partial class SessionControlForm
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
            this.passwordTextBox = new System.Windows.Forms.TextBox();
            this.userComboBox = new System.Windows.Forms.ComboBox();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.buttonEntering = new System.Windows.Forms.Button();
            this.panel1 = new System.Windows.Forms.Panel();
            this.checkBox1 = new System.Windows.Forms.CheckBox();
            this.dbStatusLbl = new System.Windows.Forms.Label();
            this.connectionSettingsButton = new System.Windows.Forms.Button();
            this.panel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // passwordTextBox
            // 
            this.passwordTextBox.Location = new System.Drawing.Point(3, 158);
            this.passwordTextBox.Name = "passwordTextBox";
            this.passwordTextBox.PasswordChar = '•';
            this.passwordTextBox.Size = new System.Drawing.Size(281, 29);
            this.passwordTextBox.TabIndex = 1;
            // 
            // userComboBox
            // 
            this.userComboBox.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.userComboBox.FormattingEnabled = true;
            this.userComboBox.Location = new System.Drawing.Point(3, 82);
            this.userComboBox.Name = "userComboBox";
            this.userComboBox.Size = new System.Drawing.Size(281, 32);
            this.userComboBox.TabIndex = 2;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(-1, 55);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(139, 24);
            this.label1.TabIndex = 3;
            this.label1.Text = "Пользователь";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(-1, 131);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(76, 24);
            this.label2.TabIndex = 4;
            this.label2.Text = "Пароль";
            // 
            // buttonEntering
            // 
            this.buttonEntering.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(67)))), ((int)(((byte)(230)))), ((int)(((byte)(38)))));
            this.buttonEntering.FlatAppearance.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(67)))), ((int)(((byte)(230)))), ((int)(((byte)(38)))));
            this.buttonEntering.FlatAppearance.BorderSize = 0;
            this.buttonEntering.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.buttonEntering.ForeColor = System.Drawing.Color.Black;
            this.buttonEntering.Location = new System.Drawing.Point(3, 232);
            this.buttonEntering.Name = "buttonEntering";
            this.buttonEntering.Size = new System.Drawing.Size(281, 44);
            this.buttonEntering.TabIndex = 5;
            this.buttonEntering.Text = "Войти";
            this.buttonEntering.UseVisualStyleBackColor = false;
            this.buttonEntering.Click += new System.EventHandler(this.buttonEntering_Click);
            // 
            // panel1
            // 
            this.panel1.Anchor = System.Windows.Forms.AnchorStyles.Top;
            this.panel1.Controls.Add(this.connectionSettingsButton);
            this.panel1.Controls.Add(this.dbStatusLbl);
            this.panel1.Controls.Add(this.checkBox1);
            this.panel1.Controls.Add(this.buttonEntering);
            this.panel1.Controls.Add(this.passwordTextBox);
            this.panel1.Controls.Add(this.label2);
            this.panel1.Controls.Add(this.userComboBox);
            this.panel1.Controls.Add(this.label1);
            this.panel1.Location = new System.Drawing.Point(405, 116);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(287, 343);
            this.panel1.TabIndex = 6;
            // 
            // checkBox1
            // 
            this.checkBox1.AutoSize = true;
            this.checkBox1.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.checkBox1.Location = new System.Drawing.Point(3, 193);
            this.checkBox1.Name = "checkBox1";
            this.checkBox1.Size = new System.Drawing.Size(235, 20);
            this.checkBox1.TabIndex = 6;
            this.checkBox1.Text = "Отобразить вводимые символы";
            this.checkBox1.UseVisualStyleBackColor = true;
            this.checkBox1.CheckedChanged += new System.EventHandler(this.checkBox1_CheckedChanged);
            // 
            // dbStatusLbl
            // 
            this.dbStatusLbl.AutoSize = true;
            this.dbStatusLbl.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.dbStatusLbl.Location = new System.Drawing.Point(-3, 20);
            this.dbStatusLbl.Name = "dbStatusLbl";
            this.dbStatusLbl.Size = new System.Drawing.Size(235, 16);
            this.dbStatusLbl.TabIndex = 7;
            this.dbStatusLbl.Text = "Отсутствует связь с базой данных";
            this.dbStatusLbl.Visible = false;
            // 
            // connectionSettingsButton
            // 
            this.connectionSettingsButton.BackColor = System.Drawing.Color.MidnightBlue;
            this.connectionSettingsButton.FlatAppearance.BorderSize = 0;
            this.connectionSettingsButton.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.connectionSettingsButton.ForeColor = System.Drawing.Color.White;
            this.connectionSettingsButton.Location = new System.Drawing.Point(3, 282);
            this.connectionSettingsButton.Name = "connectionSettingsButton";
            this.connectionSettingsButton.Size = new System.Drawing.Size(281, 44);
            this.connectionSettingsButton.TabIndex = 8;
            this.connectionSettingsButton.Text = "Настройки подключения";
            this.connectionSettingsButton.UseVisualStyleBackColor = false;
            this.connectionSettingsButton.Click += new System.EventHandler(this.connectionSettingsButton_Click);
            // 
            // SessionControlForm
            // 
            this.AcceptButton = this.buttonEntering;
            this.AutoScaleDimensions = new System.Drawing.SizeF(11F, 24F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1083, 482);
            this.Controls.Add(this.panel1);
            this.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.Margin = new System.Windows.Forms.Padding(6);
            this.Name = "SessionControlForm";
            this.Text = "Авторизация";
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion
        private System.Windows.Forms.TextBox passwordTextBox;
        private System.Windows.Forms.ComboBox userComboBox;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Button buttonEntering;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.CheckBox checkBox1;
        private System.Windows.Forms.Label dbStatusLbl;
        private System.Windows.Forms.Button connectionSettingsButton;
    }
}