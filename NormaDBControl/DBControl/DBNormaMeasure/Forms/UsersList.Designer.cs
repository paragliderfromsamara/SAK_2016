namespace NormaMeasure.DBControl.DBNormaMeasure.Forms
{
    partial class UsersList
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
            this.usersListTable = new System.Windows.Forms.DataGridView();
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
            this.RowContextMenuStrip = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.initEditUserMode = new System.Windows.Forms.ToolStripMenuItem();
            this.delUserMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.addUserButton = new System.Windows.Forms.Button();
            this.userFormDataSet = new System.Data.DataSet();
            ((System.ComponentModel.ISupportInitialize)(this.usersListTable)).BeginInit();
            this.RowContextMenuStrip.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.userFormDataSet)).BeginInit();
            this.SuspendLayout();
            // 
            // usersListTable
            // 
            this.usersListTable.AllowUserToAddRows = false;
            this.usersListTable.AllowUserToDeleteRows = false;
            this.usersListTable.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.usersListTable.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.usersListTable.BackgroundColor = System.Drawing.Color.LightGray;
            this.usersListTable.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.usersListTable.ColumnHeadersHeight = 30;
            this.usersListTable.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
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
            this.usersListTable.Location = new System.Drawing.Point(12, 96);
            this.usersListTable.MultiSelect = false;
            this.usersListTable.Name = "usersListTable";
            dataGridViewCellStyle1.Font = new System.Drawing.Font("Microsoft Sans Serif", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            dataGridViewCellStyle1.NullValue = "-";
            dataGridViewCellStyle1.Padding = new System.Windows.Forms.Padding(7, 0, 0, 0);
            dataGridViewCellStyle1.SelectionBackColor = System.Drawing.Color.Teal;
            dataGridViewCellStyle1.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.usersListTable.RowsDefaultCellStyle = dataGridViewCellStyle1;
            this.usersListTable.RowTemplate.ContextMenuStrip = this.RowContextMenuStrip;
            this.usersListTable.RowTemplate.Height = 30;
            this.usersListTable.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.usersListTable.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.usersListTable.Size = new System.Drawing.Size(925, 458);
            this.usersListTable.StandardTab = true;
            this.usersListTable.TabIndex = 13;
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
            // addUserButton
            // 
            this.addUserButton.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(120)))), ((int)(((byte)(231)))), ((int)(((byte)(101)))));
            this.addUserButton.FlatAppearance.BorderSize = 0;
            this.addUserButton.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.addUserButton.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.addUserButton.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(54)))), ((int)(((byte)(54)))), ((int)(((byte)(54)))));
            this.addUserButton.Location = new System.Drawing.Point(12, 29);
            this.addUserButton.Name = "addUserButton";
            this.addUserButton.Size = new System.Drawing.Size(154, 44);
            this.addUserButton.TabIndex = 6;
            this.addUserButton.Text = "ДОБАВИТЬ";
            this.addUserButton.UseVisualStyleBackColor = false;
            this.addUserButton.Click += new System.EventHandler(this.addUserButton_Click);
            // 
            // userFormDataSet
            // 
            this.userFormDataSet.DataSetName = "NewDataSet";
            // 
            // UsersList
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(949, 566);
            this.Controls.Add(this.addUserButton);
            this.Controls.Add(this.usersListTable);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "UsersList";
            this.Text = "Управление пользователями";
            ((System.ComponentModel.ISupportInitialize)(this.usersListTable)).EndInit();
            this.RowContextMenuStrip.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.userFormDataSet)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.DataGridView usersListTable;
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