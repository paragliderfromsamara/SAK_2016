namespace NormaMeasure.SAC_APP
{
    partial class dbUsersForm
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
            this.closeBut = new System.Windows.Forms.Button();
            this.addUserButton = new System.Windows.Forms.Button();
            this.userFirstName = new System.Windows.Forms.TextBox();
            this.userFirstNameLabel = new System.Windows.Forms.Label();
            this.userLastName = new System.Windows.Forms.TextBox();
            this.userThirdName = new System.Windows.Forms.TextBox();
            this.userLastNameLabel = new System.Windows.Forms.Label();
            this.userThirdNameLabel = new System.Windows.Forms.Label();
            this.userRole = new System.Windows.Forms.ComboBox();
            this.userRoleLabel = new System.Windows.Forms.Label();
            this.userFormField = new System.Windows.Forms.GroupBox();
            this.cancelEditUser = new System.Windows.Forms.Button();
            this.saveUserData = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.userTabNum = new System.Windows.Forms.TextBox();
            this.userPasswordLabel = new System.Windows.Forms.Label();
            this.userPassword = new System.Windows.Forms.TextBox();
            this.userIdLbl = new System.Windows.Forms.Label();
            this.rolesDataSet = new System.Data.DataSet();
            this.usersDataSet = new System.Data.DataSet();
            this.usersList = new System.Windows.Forms.DataGridView();
            this.id = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.last_name = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.name = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.third_name = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.employee_number = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.role_name = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.password = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.cellContextMenu = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.editUserListRow = new System.Windows.Forms.ToolStripMenuItem();
            this.delUserListRow = new System.Windows.Forms.ToolStripMenuItem();
            this.userFormField.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.rolesDataSet)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.usersDataSet)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.usersList)).BeginInit();
            this.cellContextMenu.SuspendLayout();
            this.SuspendLayout();
            // 
            // closeBut
            // 
            this.closeBut.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.closeBut.FlatStyle = System.Windows.Forms.FlatStyle.System;
            this.closeBut.Location = new System.Drawing.Point(12, 528);
            this.closeBut.Name = "closeBut";
            this.closeBut.Size = new System.Drawing.Size(75, 23);
            this.closeBut.TabIndex = 0;
            this.closeBut.Text = "Закрыть";
            this.closeBut.UseVisualStyleBackColor = true;
            this.closeBut.Click += new System.EventHandler(this.closeBut_Click);
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
            // userLastName
            // 
            this.userLastName.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.userLastName.Location = new System.Drawing.Point(21, 47);
            this.userLastName.Name = "userLastName";
            this.userLastName.Size = new System.Drawing.Size(123, 20);
            this.userLastName.TabIndex = 0;
            // 
            // userThirdName
            // 
            this.userThirdName.Location = new System.Drawing.Point(279, 47);
            this.userThirdName.Name = "userThirdName";
            this.userThirdName.Size = new System.Drawing.Size(123, 20);
            this.userThirdName.TabIndex = 2;
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
            // userThirdNameLabel
            // 
            this.userThirdNameLabel.AutoSize = true;
            this.userThirdNameLabel.Location = new System.Drawing.Point(279, 31);
            this.userThirdNameLabel.Name = "userThirdNameLabel";
            this.userThirdNameLabel.Size = new System.Drawing.Size(54, 13);
            this.userThirdNameLabel.TabIndex = 7;
            this.userThirdNameLabel.Text = "Отчество";
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
            // userRoleLabel
            // 
            this.userRoleLabel.AutoSize = true;
            this.userRoleLabel.Location = new System.Drawing.Point(513, 32);
            this.userRoleLabel.Name = "userRoleLabel";
            this.userRoleLabel.Size = new System.Drawing.Size(65, 13);
            this.userRoleLabel.TabIndex = 9;
            this.userRoleLabel.Text = "Должность";
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
            this.userFormField.Location = new System.Drawing.Point(12, 24);
            this.userFormField.Name = "userFormField";
            this.userFormField.Size = new System.Drawing.Size(942, 100);
            this.userFormField.TabIndex = 10;
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
            this.userTabNum.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.userTabNum_KeyPress);
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
            this.userPassword.Size = new System.Drawing.Size(106, 20);
            this.userPassword.TabIndex = 5;
            // 
            // userIdLbl
            // 
            this.userIdLbl.AutoSize = true;
            this.userIdLbl.Location = new System.Drawing.Point(12, 8);
            this.userIdLbl.Name = "userIdLbl";
            this.userIdLbl.Size = new System.Drawing.Size(36, 13);
            this.userIdLbl.TabIndex = 16;
            this.userIdLbl.Text = "userId";
            this.userIdLbl.Visible = false;
            // 
            // rolesDataSet
            // 
            this.rolesDataSet.DataSetName = "usersDataSet";
            // 
            // usersDataSet
            // 
            this.usersDataSet.DataSetName = "usersDataSet";
            // 
            // usersList
            // 
            this.usersList.AutoGenerateColumns = false;
            this.usersList.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.usersList.BackgroundColor = System.Drawing.Color.LightGray;
            this.usersList.ColumnHeadersHeight = 30;
            this.usersList.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.id,
            this.last_name,
            this.name,
            this.third_name,
            this.employee_number,
            this.role_name,
            this.password});
            this.usersList.ContextMenuStrip = this.cellContextMenu;
            this.usersList.DataSource = this.usersDataSet;
            this.usersList.Location = new System.Drawing.Point(12, 146);
            this.usersList.Name = "usersList";
            dataGridViewCellStyle1.Font = new System.Drawing.Font("Microsoft Sans Serif", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            dataGridViewCellStyle1.NullValue = "-";
            dataGridViewCellStyle1.Padding = new System.Windows.Forms.Padding(7, 0, 0, 0);
            dataGridViewCellStyle1.SelectionBackColor = System.Drawing.Color.Teal;
            dataGridViewCellStyle1.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.usersList.RowsDefaultCellStyle = dataGridViewCellStyle1;
            this.usersList.RowTemplate.Height = 30;
            this.usersList.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.usersList.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.usersList.Size = new System.Drawing.Size(952, 366);
            this.usersList.StandardTab = true;
            this.usersList.TabIndex = 12;
            // 
            // id
            // 
            this.id.DataPropertyName = "id";
            this.id.FillWeight = 101.7259F;
            this.id.HeaderText = "id";
            this.id.Name = "id";
            this.id.ReadOnly = true;
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
            this.name.DataPropertyName = "name";
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
            this.role_name.DataPropertyName = "role_name";
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
            // cellContextMenu
            // 
            this.cellContextMenu.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.editUserListRow,
            this.delUserListRow});
            this.cellContextMenu.Name = "cellContextMenu";
            this.cellContextMenu.Size = new System.Drawing.Size(127, 48);
            // 
            // editUserListRow
            // 
            this.editUserListRow.Name = "editUserListRow";
            this.editUserListRow.Size = new System.Drawing.Size(126, 22);
            this.editUserListRow.Text = "изменить";
            this.editUserListRow.Click += new System.EventHandler(this.editUserListRow_Click);
            // 
            // delUserListRow
            // 
            this.delUserListRow.Name = "delUserListRow";
            this.delUserListRow.Size = new System.Drawing.Size(126, 22);
            this.delUserListRow.Text = "удалить";
            this.delUserListRow.Click += new System.EventHandler(this.delUserToolStripMenuItem_Click);
            // 
            // dbUsersForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.CancelButton = this.closeBut;
            this.ClientSize = new System.Drawing.Size(987, 563);
            this.Controls.Add(this.userIdLbl);
            this.Controls.Add(this.usersList);
            this.Controls.Add(this.userFormField);
            this.Controls.Add(this.closeBut);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow;
            this.Name = "dbUsersForm";
            this.Text = "Информация о пользователях";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.dbUsersForm_FormClosing);
            this.userFormField.ResumeLayout(false);
            this.userFormField.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.rolesDataSet)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.usersDataSet)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.usersList)).EndInit();
            this.cellContextMenu.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button closeBut;
        private System.Windows.Forms.Button addUserButton;
        private System.Windows.Forms.TextBox userFirstName;
        private System.Windows.Forms.Label userFirstNameLabel;
        private System.Windows.Forms.TextBox userLastName;
        private System.Windows.Forms.TextBox userThirdName;
        private System.Windows.Forms.Label userLastNameLabel;
        private System.Windows.Forms.Label userThirdNameLabel;
        private System.Windows.Forms.ComboBox userRole;
        private System.Windows.Forms.Label userRoleLabel;
        private System.Windows.Forms.GroupBox userFormField;
        private System.Windows.Forms.Label userPasswordLabel;
        private System.Windows.Forms.TextBox userPassword;
        private System.Data.DataSet rolesDataSet;
        private System.Data.DataSet usersDataSet;
        private System.Windows.Forms.DataGridView usersList;
        private System.Windows.Forms.ContextMenuStrip cellContextMenu;
        private System.Windows.Forms.ToolStripMenuItem editUserListRow;
        private System.Windows.Forms.ToolStripMenuItem delUserListRow;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox userTabNum;
        private System.Windows.Forms.Button cancelEditUser;
        private System.Windows.Forms.Button saveUserData;
        private System.Windows.Forms.Label userIdLbl;
        private System.Windows.Forms.DataGridViewTextBoxColumn id;
        private System.Windows.Forms.DataGridViewTextBoxColumn last_name;
        private System.Windows.Forms.DataGridViewTextBoxColumn name;
        private System.Windows.Forms.DataGridViewTextBoxColumn third_name;
        private System.Windows.Forms.DataGridViewTextBoxColumn employee_number;
        private System.Windows.Forms.DataGridViewTextBoxColumn role_name;
        private System.Windows.Forms.DataGridViewTextBoxColumn password;
    }
}