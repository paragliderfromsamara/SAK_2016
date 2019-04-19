namespace NormaMeasure.DBControl.SAC.Forms
{
    partial class UsersForm
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
            this.components = new System.ComponentModel.Container();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle1 = new System.Windows.Forms.DataGridViewCellStyle();
            this.usersList = new System.Windows.Forms.DataGridView();
            this.RowContextMenuStrip = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.initEditUserMode = new System.Windows.Forms.ToolStripMenuItem();
            this.delUserMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.userFormField = new System.Windows.Forms.GroupBox();
            this.cancelEditUser = new System.Windows.Forms.Button();
            this.saveUserData = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.userTabNum = new System.Windows.Forms.TextBox();
            this.userPasswordLabel = new System.Windows.Forms.Label();
            this.userPassword = new System.Windows.Forms.TextBox();
            this.userRole = new System.Windows.Forms.ComboBox();
            this.addUserButton = new System.Windows.Forms.Button();
            this.userRoleLabel = new System.Windows.Forms.Label();
            this.userFirstName = new System.Windows.Forms.TextBox();
            this.userFirstNameLabel = new System.Windows.Forms.Label();
            this.userThirdNameLabel = new System.Windows.Forms.Label();
            this.userLastName = new System.Windows.Forms.TextBox();
            this.userLastNameLabel = new System.Windows.Forms.Label();
            this.userThirdName = new System.Windows.Forms.TextBox();
            this.userFormDataSet = new System.Data.DataSet();
            this.user_id = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.full_name = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.last_name = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.name = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.third_name = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.employee_number = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.role_name = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.password = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.user_role_id = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.is_active = new System.Windows.Forms.DataGridViewTextBoxColumn();
            ((System.ComponentModel.ISupportInitialize)(this.usersList)).BeginInit();
            this.RowContextMenuStrip.SuspendLayout();
            this.userFormField.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.userFormDataSet)).BeginInit();
            this.SuspendLayout();
            // 
            // usersList
            // 
            this.usersList.AllowUserToAddRows = false;
            this.usersList.AllowUserToDeleteRows = false;
            this.usersList.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.usersList.BackgroundColor = System.Drawing.Color.LightGray;
            this.usersList.ColumnHeadersHeight = 30;
            this.usersList.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.user_id,
            this.full_name,
            this.last_name,
            this.name,
            this.third_name,
            this.employee_number,
            this.role_name,
            this.password,
            this.user_role_id,
            this.is_active});
            this.usersList.Location = new System.Drawing.Point(12, 145);
            this.usersList.MultiSelect = false;
            this.usersList.Name = "usersList";
            dataGridViewCellStyle1.Font = new System.Drawing.Font("Microsoft Sans Serif", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            dataGridViewCellStyle1.NullValue = "-";
            dataGridViewCellStyle1.Padding = new System.Windows.Forms.Padding(7, 0, 0, 0);
            dataGridViewCellStyle1.SelectionBackColor = System.Drawing.Color.Teal;
            dataGridViewCellStyle1.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.usersList.RowsDefaultCellStyle = dataGridViewCellStyle1;
            this.usersList.RowTemplate.ContextMenuStrip = this.RowContextMenuStrip;
            this.usersList.RowTemplate.Height = 30;
            this.usersList.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.usersList.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.usersList.Size = new System.Drawing.Size(952, 366);
            this.usersList.StandardTab = true;
            this.usersList.TabIndex = 13;
            // 
            // RowContextMenuStrip
            // 
            this.RowContextMenuStrip.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.initEditUserMode,
            this.delUserMenuItem});
            this.RowContextMenuStrip.Name = "RowContextMenuStrip";
            this.RowContextMenuStrip.Size = new System.Drawing.Size(129, 48);
            // 
            // initEditUserMode
            // 
            this.initEditUserMode.Name = "initEditUserMode";
            this.initEditUserMode.Size = new System.Drawing.Size(128, 22);
            this.initEditUserMode.Text = "Изменить";
            this.initEditUserMode.Click += new System.EventHandler(this.initEditUserMode_Click);
            // 
            // delUserMenuItem
            // 
            this.delUserMenuItem.Name = "delUserMenuItem";
            this.delUserMenuItem.Size = new System.Drawing.Size(128, 22);
            this.delUserMenuItem.Text = "Удалить";
            this.delUserMenuItem.Click += new System.EventHandler(this.delUserMenuItem_Click);
            // 
            // userFormField
            // 
            this.userFormField.Controls.Add(this.cancelEditUser);
            this.userFormField.Controls.Add(this.saveUserData);
            this.userFormField.Controls.Add(this.label1);
            this.userFormField.Controls.Add(this.userTabNum);
            this.userFormField.Controls.Add(this.userPasswordLabel);
            this.userFormField.Controls.Add(this.userPassword);
            this.userFormField.Controls.Add(this.userRole);
            this.userFormField.Controls.Add(this.addUserButton);
            this.userFormField.Controls.Add(this.userRoleLabel);
            this.userFormField.Controls.Add(this.userFirstName);
            this.userFormField.Controls.Add(this.userFirstNameLabel);
            this.userFormField.Controls.Add(this.userThirdNameLabel);
            this.userFormField.Controls.Add(this.userLastName);
            this.userFormField.Controls.Add(this.userLastNameLabel);
            this.userFormField.Controls.Add(this.userThirdName);
            this.userFormField.Location = new System.Drawing.Point(12, 21);
            this.userFormField.Name = "userFormField";
            this.userFormField.Size = new System.Drawing.Size(952, 100);
            this.userFormField.TabIndex = 14;
            this.userFormField.TabStop = false;
            this.userFormField.Text = "Новый пользователь";
            // 
            // cancelEditUser
            // 
            this.cancelEditUser.Location = new System.Drawing.Point(853, 39);
            this.cancelEditUser.Name = "cancelEditUser";
            this.cancelEditUser.Size = new System.Drawing.Size(68, 32);
            this.cancelEditUser.TabIndex = 15;
            this.cancelEditUser.Text = "Отменить";
            this.cancelEditUser.UseVisualStyleBackColor = true;
            this.cancelEditUser.Click += new System.EventHandler(this.cancelEditUser_Click);
            // 
            // saveUserData
            // 
            this.saveUserData.Location = new System.Drawing.Point(763, 39);
            this.saveUserData.Name = "saveUserData";
            this.saveUserData.Size = new System.Drawing.Size(84, 32);
            this.saveUserData.TabIndex = 14;
            this.saveUserData.Text = "Сохранить";
            this.saveUserData.UseVisualStyleBackColor = true;
            this.saveUserData.Click += new System.EventHandler(this.saveUserData_Click);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(408, 32);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(99, 13);
            this.label1.TabIndex = 13;
            this.label1.Text = "Табельный номер";
            // 
            // userTabNum
            // 
            this.userTabNum.Location = new System.Drawing.Point(408, 47);
            this.userTabNum.Name = "userTabNum";
            this.userTabNum.Size = new System.Drawing.Size(99, 20);
            this.userTabNum.TabIndex = 3;
            // 
            // userPasswordLabel
            // 
            this.userPasswordLabel.AutoSize = true;
            this.userPasswordLabel.Location = new System.Drawing.Point(648, 31);
            this.userPasswordLabel.Name = "userPasswordLabel";
            this.userPasswordLabel.Size = new System.Drawing.Size(45, 13);
            this.userPasswordLabel.TabIndex = 11;
            this.userPasswordLabel.Text = "Пароль";
            // 
            // userPassword
            // 
            this.userPassword.Location = new System.Drawing.Point(651, 47);
            this.userPassword.Name = "userPassword";
            this.userPassword.PasswordChar = '*';
            this.userPassword.Size = new System.Drawing.Size(106, 20);
            this.userPassword.TabIndex = 5;
            // 
            // userRole
            // 
            this.userRole.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.userRole.FormattingEnabled = true;
            this.userRole.Location = new System.Drawing.Point(513, 46);
            this.userRole.Name = "userRole";
            this.userRole.Size = new System.Drawing.Size(132, 21);
            this.userRole.TabIndex = 4;
            // 
            // addUserButton
            // 
            this.addUserButton.Location = new System.Drawing.Point(832, 39);
            this.addUserButton.Name = "addUserButton";
            this.addUserButton.Size = new System.Drawing.Size(89, 32);
            this.addUserButton.TabIndex = 6;
            this.addUserButton.Text = "Добавить";
            this.addUserButton.UseVisualStyleBackColor = true;
            this.addUserButton.Click += new System.EventHandler(this.addUserButton_Click);
            // 
            // userRoleLabel
            // 
            this.userRoleLabel.AutoSize = true;
            this.userRoleLabel.Location = new System.Drawing.Point(513, 32);
            this.userRoleLabel.Name = "userRoleLabel";
            this.userRoleLabel.Size = new System.Drawing.Size(65, 13);
            this.userRoleLabel.TabIndex = 9;
            this.userRoleLabel.Text = "Должность";
            // 
            // userFirstName
            // 
            this.userFirstName.BackColor = System.Drawing.SystemColors.Window;
            this.userFirstName.Location = new System.Drawing.Point(150, 47);
            this.userFirstName.Name = "userFirstName";
            this.userFirstName.Size = new System.Drawing.Size(123, 20);
            this.userFirstName.TabIndex = 1;
            // 
            // userFirstNameLabel
            // 
            this.userFirstNameLabel.AutoSize = true;
            this.userFirstNameLabel.Location = new System.Drawing.Point(149, 31);
            this.userFirstNameLabel.Name = "userFirstNameLabel";
            this.userFirstNameLabel.Size = new System.Drawing.Size(29, 13);
            this.userFirstNameLabel.TabIndex = 3;
            this.userFirstNameLabel.Text = "Имя";
            // 
            // userThirdNameLabel
            // 
            this.userThirdNameLabel.AutoSize = true;
            this.userThirdNameLabel.Location = new System.Drawing.Point(279, 31);
            this.userThirdNameLabel.Name = "userThirdNameLabel";
            this.userThirdNameLabel.Size = new System.Drawing.Size(54, 13);
            this.userThirdNameLabel.TabIndex = 7;
            this.userThirdNameLabel.Text = "Отчество";
            // 
            // userLastName
            // 
            this.userLastName.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.userLastName.Location = new System.Drawing.Point(21, 47);
            this.userLastName.Name = "userLastName";
            this.userLastName.Size = new System.Drawing.Size(123, 20);
            this.userLastName.TabIndex = 0;
            // 
            // userLastNameLabel
            // 
            this.userLastNameLabel.AutoSize = true;
            this.userLastNameLabel.Location = new System.Drawing.Point(18, 31);
            this.userLastNameLabel.Name = "userLastNameLabel";
            this.userLastNameLabel.Size = new System.Drawing.Size(56, 13);
            this.userLastNameLabel.TabIndex = 6;
            this.userLastNameLabel.Text = "Фамилия";
            // 
            // userThirdName
            // 
            this.userThirdName.Location = new System.Drawing.Point(279, 47);
            this.userThirdName.Name = "userThirdName";
            this.userThirdName.Size = new System.Drawing.Size(123, 20);
            this.userThirdName.TabIndex = 2;
            // 
            // userFormDataSet
            // 
            this.userFormDataSet.DataSetName = "NewDataSet";
            // 
            // user_id
            // 
            this.user_id.DataPropertyName = "user_id";
            this.user_id.FillWeight = 101.7259F;
            this.user_id.HeaderText = "id";
            this.user_id.Name = "user_id";
            this.user_id.ReadOnly = true;
            // 
            // full_name
            // 
            this.full_name.DataPropertyName = "full_name";
            this.full_name.HeaderText = "Полное имя";
            this.full_name.Name = "full_name";
            this.full_name.Visible = false;
            // 
            // last_name
            // 
            this.last_name.DataPropertyName = "last_name";
            this.last_name.FillWeight = 101.7259F;
            this.last_name.HeaderText = "Фамилия";
            this.last_name.Name = "last_name";
            this.last_name.ReadOnly = true;
            // 
            // name
            // 
            this.name.DataPropertyName = "first_name";
            this.name.FillWeight = 101.7259F;
            this.name.HeaderText = "Имя";
            this.name.Name = "name";
            this.name.ReadOnly = true;
            // 
            // third_name
            // 
            this.third_name.DataPropertyName = "third_name";
            this.third_name.FillWeight = 101.7259F;
            this.third_name.HeaderText = "Отчество";
            this.third_name.Name = "third_name";
            this.third_name.ReadOnly = true;
            // 
            // employee_number
            // 
            this.employee_number.DataPropertyName = "employee_number";
            this.employee_number.FillWeight = 91.37056F;
            this.employee_number.HeaderText = "Табельный номер";
            this.employee_number.MinimumWidth = 30;
            this.employee_number.Name = "employee_number";
            this.employee_number.ReadOnly = true;
            // 
            // role_name
            // 
            this.role_name.DataPropertyName = "user_role_name";
            this.role_name.FillWeight = 101.7259F;
            this.role_name.HeaderText = "Должность";
            this.role_name.Name = "role_name";
            this.role_name.ReadOnly = true;
            // 
            // password
            // 
            this.password.DataPropertyName = "password";
            this.password.HeaderText = "Пароль";
            this.password.Name = "password";
            this.password.ReadOnly = true;
            this.password.Visible = false;
            // 
            // user_role_id
            // 
            this.user_role_id.DataPropertyName = "user_role_id";
            this.user_role_id.HeaderText = "user_role_id";
            this.user_role_id.Name = "user_role_id";
            this.user_role_id.Visible = false;
            // 
            // is_active
            // 
            this.is_active.DataPropertyName = "is_active";
            this.is_active.HeaderText = "is_active";
            this.is_active.Name = "is_active";
            this.is_active.Visible = false;
            // 
            // UsersForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(978, 576);
            this.Controls.Add(this.userFormField);
            this.Controls.Add(this.usersList);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow;
            this.Name = "UsersForm";
            this.Text = "Управление пользователями";
            ((System.ComponentModel.ISupportInitialize)(this.usersList)).EndInit();
            this.RowContextMenuStrip.ResumeLayout(false);
            this.userFormField.ResumeLayout(false);
            this.userFormField.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.userFormDataSet)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.DataGridView usersList;
        private System.Windows.Forms.GroupBox userFormField;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox userTabNum;
        private System.Windows.Forms.Label userPasswordLabel;
        private System.Windows.Forms.TextBox userPassword;
        private System.Windows.Forms.ComboBox userRole;
        private System.Windows.Forms.Label userRoleLabel;
        private System.Windows.Forms.TextBox userFirstName;
        private System.Windows.Forms.Label userFirstNameLabel;
        private System.Windows.Forms.Label userThirdNameLabel;
        private System.Windows.Forms.TextBox userLastName;
        private System.Windows.Forms.Label userLastNameLabel;
        private System.Windows.Forms.TextBox userThirdName;
        private System.Windows.Forms.Button cancelEditUser;
        private System.Windows.Forms.Button saveUserData;
        private System.Windows.Forms.Button addUserButton;
        private System.Data.DataSet userFormDataSet;
        private System.Windows.Forms.ContextMenuStrip RowContextMenuStrip;
        private System.Windows.Forms.ToolStripMenuItem initEditUserMode;
        private System.Windows.Forms.ToolStripMenuItem delUserMenuItem;
        private System.Windows.Forms.DataGridViewTextBoxColumn user_id;
        private System.Windows.Forms.DataGridViewTextBoxColumn full_name;
        private System.Windows.Forms.DataGridViewTextBoxColumn last_name;
        private System.Windows.Forms.DataGridViewTextBoxColumn name;
        private System.Windows.Forms.DataGridViewTextBoxColumn third_name;
        private System.Windows.Forms.DataGridViewTextBoxColumn employee_number;
        private System.Windows.Forms.DataGridViewTextBoxColumn role_name;
        private System.Windows.Forms.DataGridViewTextBoxColumn password;
        private System.Windows.Forms.DataGridViewTextBoxColumn user_role_id;
        private System.Windows.Forms.DataGridViewTextBoxColumn is_active;
    }
}