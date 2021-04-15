namespace TeraMicroMeasure.Forms
{
    partial class DataBaseSettingsForm
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
            this.titleLbl1 = new System.Windows.Forms.Label();
            this.subtitleLbl1 = new System.Windows.Forms.Label();
            this.tbHostName = new System.Windows.Forms.TextBox();
            this.tbUserName = new System.Windows.Forms.TextBox();
            this.subTitleLbl2 = new System.Windows.Forms.Label();
            this.tbUserPassword = new System.Windows.Forms.TextBox();
            this.subTitleLbl3 = new System.Windows.Forms.Label();
            this.btnSave = new System.Windows.Forms.Button();
            this.btnReset = new System.Windows.Forms.Button();
            this.lblSettingsInfo = new System.Windows.Forms.Label();
            this.panel1 = new System.Windows.Forms.Panel();
            this.cancelButton = new System.Windows.Forms.Button();
            this.panel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // titleLbl1
            // 
            this.titleLbl1.AutoSize = true;
            this.titleLbl1.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.titleLbl1.Location = new System.Drawing.Point(32, 29);
            this.titleLbl1.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.titleLbl1.Name = "titleLbl1";
            this.titleLbl1.Size = new System.Drawing.Size(374, 24);
            this.titleLbl1.TabIndex = 0;
            this.titleLbl1.Text = "Параметры подключения к Базе Данных";
            // 
            // subtitleLbl1
            // 
            this.subtitleLbl1.AutoSize = true;
            this.subtitleLbl1.Font = new System.Drawing.Font("Microsoft Sans Serif", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.subtitleLbl1.Location = new System.Drawing.Point(35, 106);
            this.subtitleLbl1.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.subtitleLbl1.Name = "subtitleLbl1";
            this.subtitleLbl1.Size = new System.Drawing.Size(42, 18);
            this.subtitleLbl1.TabIndex = 1;
            this.subtitleLbl1.Text = "Хост";
            // 
            // tbHostName
            // 
            this.tbHostName.Font = new System.Drawing.Font("Microsoft Sans Serif", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.tbHostName.Location = new System.Drawing.Point(38, 129);
            this.tbHostName.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.tbHostName.Name = "tbHostName";
            this.tbHostName.Size = new System.Drawing.Size(205, 24);
            this.tbHostName.TabIndex = 2;
            this.tbHostName.TextChanged += new System.EventHandler(this.OnInput_ChangedHandler);
            // 
            // tbUserName
            // 
            this.tbUserName.Font = new System.Drawing.Font("Microsoft Sans Serif", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.tbUserName.Location = new System.Drawing.Point(251, 129);
            this.tbUserName.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.tbUserName.Name = "tbUserName";
            this.tbUserName.Size = new System.Drawing.Size(191, 24);
            this.tbUserName.TabIndex = 3;
            this.tbUserName.TextChanged += new System.EventHandler(this.OnInput_ChangedHandler);
            // 
            // subTitleLbl2
            // 
            this.subTitleLbl2.AutoSize = true;
            this.subTitleLbl2.Font = new System.Drawing.Font("Microsoft Sans Serif", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.subTitleLbl2.Location = new System.Drawing.Point(248, 106);
            this.subTitleLbl2.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.subTitleLbl2.Name = "subTitleLbl2";
            this.subTitleLbl2.Size = new System.Drawing.Size(110, 18);
            this.subTitleLbl2.TabIndex = 4;
            this.subTitleLbl2.Text = "Пользователь";
            // 
            // tbUserPassword
            // 
            this.tbUserPassword.Font = new System.Drawing.Font("Microsoft Sans Serif", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.tbUserPassword.Location = new System.Drawing.Point(450, 129);
            this.tbUserPassword.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.tbUserPassword.Name = "tbUserPassword";
            this.tbUserPassword.Size = new System.Drawing.Size(244, 24);
            this.tbUserPassword.TabIndex = 5;
            this.tbUserPassword.TextChanged += new System.EventHandler(this.OnInput_ChangedHandler);
            // 
            // subTitleLbl3
            // 
            this.subTitleLbl3.AutoSize = true;
            this.subTitleLbl3.Font = new System.Drawing.Font("Microsoft Sans Serif", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.subTitleLbl3.Location = new System.Drawing.Point(447, 106);
            this.subTitleLbl3.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.subTitleLbl3.Name = "subTitleLbl3";
            this.subTitleLbl3.Size = new System.Drawing.Size(61, 18);
            this.subTitleLbl3.TabIndex = 6;
            this.subTitleLbl3.Text = "Пароль";
            // 
            // btnSave
            // 
            this.btnSave.BackColor = System.Drawing.Color.LawnGreen;
            this.btnSave.DialogResult = System.Windows.Forms.DialogResult.Retry;
            this.btnSave.Dock = System.Windows.Forms.DockStyle.Left;
            this.btnSave.FlatAppearance.BorderSize = 0;
            this.btnSave.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnSave.Font = new System.Drawing.Font("Microsoft Sans Serif", 11.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.btnSave.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.btnSave.Location = new System.Drawing.Point(0, 0);
            this.btnSave.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.btnSave.Name = "btnSave";
            this.btnSave.Size = new System.Drawing.Size(145, 45);
            this.btnSave.TabIndex = 7;
            this.btnSave.Text = "Применить";
            this.btnSave.UseVisualStyleBackColor = false;
            this.btnSave.Click += new System.EventHandler(this.btnSave_Click);
            // 
            // btnReset
            // 
            this.btnReset.BackColor = System.Drawing.Color.SkyBlue;
            this.btnReset.Dock = System.Windows.Forms.DockStyle.Left;
            this.btnReset.FlatAppearance.BorderColor = System.Drawing.Color.White;
            this.btnReset.FlatAppearance.BorderSize = 0;
            this.btnReset.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnReset.Font = new System.Drawing.Font("Microsoft Sans Serif", 11.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.btnReset.ForeColor = System.Drawing.Color.Black;
            this.btnReset.Location = new System.Drawing.Point(145, 0);
            this.btnReset.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.btnReset.Name = "btnReset";
            this.btnReset.Size = new System.Drawing.Size(161, 45);
            this.btnReset.TabIndex = 8;
            this.btnReset.Text = "Сбросить";
            this.btnReset.UseVisualStyleBackColor = false;
            this.btnReset.Click += new System.EventHandler(this.btnReset_Click);
            // 
            // lblSettingsInfo
            // 
            this.lblSettingsInfo.AutoSize = true;
            this.lblSettingsInfo.Font = new System.Drawing.Font("Microsoft Sans Serif", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.lblSettingsInfo.Location = new System.Drawing.Point(33, 70);
            this.lblSettingsInfo.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lblSettingsInfo.Name = "lblSettingsInfo";
            this.lblSettingsInfo.Size = new System.Drawing.Size(569, 18);
            this.lblSettingsInfo.TabIndex = 9;
            this.lblSettingsInfo.Text = "В режиме \"Клиент\" приложение получает данные подключения к БД от сервера";
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.cancelButton);
            this.panel1.Controls.Add(this.btnReset);
            this.panel1.Controls.Add(this.btnSave);
            this.panel1.Font = new System.Drawing.Font("Microsoft Sans Serif", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.panel1.Location = new System.Drawing.Point(36, 175);
            this.panel1.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(658, 45);
            this.panel1.TabIndex = 10;
            // 
            // cancelButton
            // 
            this.cancelButton.BackColor = System.Drawing.Color.Orange;
            this.cancelButton.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.cancelButton.Dock = System.Windows.Forms.DockStyle.Left;
            this.cancelButton.FlatAppearance.BorderSize = 0;
            this.cancelButton.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.cancelButton.Font = new System.Drawing.Font("Microsoft Sans Serif", 11.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.cancelButton.Location = new System.Drawing.Point(306, 0);
            this.cancelButton.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.cancelButton.Name = "cancelButton";
            this.cancelButton.Size = new System.Drawing.Size(143, 45);
            this.cancelButton.TabIndex = 11;
            this.cancelButton.Text = "Отмена";
            this.cancelButton.UseVisualStyleBackColor = false;
            this.cancelButton.Visible = false;
            // 
            // DataBaseSettingsForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(9F, 20F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.White;
            this.ClientSize = new System.Drawing.Size(734, 245);
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.lblSettingsInfo);
            this.Controls.Add(this.subTitleLbl3);
            this.Controls.Add(this.tbUserPassword);
            this.Controls.Add(this.subTitleLbl2);
            this.Controls.Add(this.tbUserName);
            this.Controls.Add(this.tbHostName);
            this.Controls.Add(this.subtitleLbl1);
            this.Controls.Add(this.titleLbl1);
            this.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "DataBaseSettingsForm";
            this.Text = "Настройки подключения к Базе Данных";
            this.Load += new System.EventHandler(this.DataBaseSettingsForm_Load);
            this.panel1.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label titleLbl1;
        private System.Windows.Forms.Label subtitleLbl1;
        private System.Windows.Forms.TextBox tbHostName;
        private System.Windows.Forms.TextBox tbUserName;
        private System.Windows.Forms.Label subTitleLbl2;
        private System.Windows.Forms.TextBox tbUserPassword;
        private System.Windows.Forms.Label subTitleLbl3;
        private System.Windows.Forms.Label lblSettingsInfo;
        private System.Windows.Forms.Panel panel1;
        public System.Windows.Forms.Button cancelButton;
        public System.Windows.Forms.Button btnSave;
        public System.Windows.Forms.Button btnReset;
    }
}