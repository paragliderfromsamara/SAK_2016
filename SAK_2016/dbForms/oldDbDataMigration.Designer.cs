namespace SAK_2016.dbForms
{
    partial class oldDbDataMigration
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
            this.migrateSelected = new System.Windows.Forms.Button();
            this.oldEntitiesDS = new System.Data.DataSet();
            this.users = new System.Data.DataTable();
            this.cables = new System.Data.DataTable();
            this.baraban_types = new System.Data.DataTable();
            this.cable_structures = new System.Data.DataTable();
            this.freq_ranges = new System.Data.DataTable();
            this.measured_params = new System.Data.DataTable();
            this.oldUsersCheckBox = new System.Windows.Forms.CheckBox();
            this.oldDbCablesCheckBox = new System.Windows.Forms.CheckBox();
            this.oldDbBarabansCheckBox = new System.Windows.Forms.CheckBox();
            this.cancelBut = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.panel1 = new System.Windows.Forms.Panel();
            this.pBarStatus = new System.Windows.Forms.Label();
            this.oldDBMigrationPBar = new System.Windows.Forms.ProgressBar();
            this.documents = new System.Data.DataTable();
            this.dataColumn1 = new System.Data.DataColumn();
            this.dataColumn2 = new System.Data.DataColumn();
            this.dataColumn3 = new System.Data.DataColumn();
            ((System.ComponentModel.ISupportInitialize)(this.oldEntitiesDS)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.users)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.cables)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.baraban_types)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.cable_structures)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.freq_ranges)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.measured_params)).BeginInit();
            this.panel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.documents)).BeginInit();
            this.SuspendLayout();
            // 
            // migrateSelected
            // 
            this.migrateSelected.Location = new System.Drawing.Point(12, 159);
            this.migrateSelected.Name = "migrateSelected";
            this.migrateSelected.Size = new System.Drawing.Size(123, 23);
            this.migrateSelected.TabIndex = 2;
            this.migrateSelected.Text = "Загрузить данные";
            this.migrateSelected.UseVisualStyleBackColor = true;
            this.migrateSelected.Click += new System.EventHandler(this.migrateSelected_Click);
            // 
            // oldEntitiesDS
            // 
            this.oldEntitiesDS.DataSetName = "NewDataSet";
            this.oldEntitiesDS.Tables.AddRange(new System.Data.DataTable[] {
            this.users,
            this.cables,
            this.baraban_types,
            this.cable_structures,
            this.freq_ranges,
            this.measured_params,
            this.documents});
            // 
            // users
            // 
            this.users.TableName = "users";
            // 
            // cables
            // 
            this.cables.TableName = "cables";
            // 
            // baraban_types
            // 
            this.baraban_types.TableName = "baraban_types";
            // 
            // cable_structures
            // 
            this.cable_structures.TableName = "cable_structures";
            // 
            // freq_ranges
            // 
            this.freq_ranges.TableName = "freq_ranges";
            // 
            // measured_params
            // 
            this.measured_params.TableName = "measured_params";
            // 
            // oldUsersCheckBox
            // 
            this.oldUsersCheckBox.AutoSize = true;
            this.oldUsersCheckBox.Location = new System.Drawing.Point(5, 4);
            this.oldUsersCheckBox.Name = "oldUsersCheckBox";
            this.oldUsersCheckBox.Size = new System.Drawing.Size(181, 17);
            this.oldUsersCheckBox.TabIndex = 3;
            this.oldUsersCheckBox.Text = "БД пользователей (bd_system)";
            this.oldUsersCheckBox.UseVisualStyleBackColor = true;
            this.oldUsersCheckBox.CheckedChanged += new System.EventHandler(this.oldUsersCheckBox_CheckedChanged);
            // 
            // oldDbCablesCheckBox
            // 
            this.oldDbCablesCheckBox.AutoSize = true;
            this.oldDbCablesCheckBox.Location = new System.Drawing.Point(5, 27);
            this.oldDbCablesCheckBox.Name = "oldDbCablesCheckBox";
            this.oldDbCablesCheckBox.Size = new System.Drawing.Size(140, 17);
            this.oldDbCablesCheckBox.TabIndex = 4;
            this.oldDbCablesCheckBox.Text = "БД кабелей (db_cable)";
            this.oldDbCablesCheckBox.UseVisualStyleBackColor = true;
            this.oldDbCablesCheckBox.CheckedChanged += new System.EventHandler(this.oldDbCablesCheckBox_CheckedChanged);
            // 
            // oldDbBarabansCheckBox
            // 
            this.oldDbBarabansCheckBox.AutoSize = true;
            this.oldDbBarabansCheckBox.Location = new System.Drawing.Point(5, 50);
            this.oldDbBarabansCheckBox.Name = "oldDbBarabansCheckBox";
            this.oldDbBarabansCheckBox.Size = new System.Drawing.Size(166, 17);
            this.oldDbBarabansCheckBox.TabIndex = 5;
            this.oldDbBarabansCheckBox.Text = "БД Барабанов (bd_baraban)";
            this.oldDbBarabansCheckBox.UseVisualStyleBackColor = true;
            this.oldDbBarabansCheckBox.CheckedChanged += new System.EventHandler(this.oldDbBarabansCheckBox_CheckedChanged);
            // 
            // cancelBut
            // 
            this.cancelBut.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.cancelBut.Location = new System.Drawing.Point(141, 159);
            this.cancelBut.Name = "cancelBut";
            this.cancelBut.Size = new System.Drawing.Size(123, 23);
            this.cancelBut.TabIndex = 6;
            this.cancelBut.Text = "Ничего не делать";
            this.cancelBut.UseVisualStyleBackColor = true;
            this.cancelBut.Click += new System.EventHandler(this.cancelBut_Click);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Tahoma", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.label1.Location = new System.Drawing.Point(9, 22);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(368, 36);
            this.label1.TabIndex = 7;
            this.label1.Text = "На данном устройстве обнаружены следующие БД \nот предыдущих систем";
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.pBarStatus);
            this.panel1.Controls.Add(this.oldDBMigrationPBar);
            this.panel1.Controls.Add(this.oldDbBarabansCheckBox);
            this.panel1.Controls.Add(this.oldDbCablesCheckBox);
            this.panel1.Controls.Add(this.oldUsersCheckBox);
            this.panel1.Location = new System.Drawing.Point(7, 70);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(399, 77);
            this.panel1.TabIndex = 8;
            // 
            // pBarStatus
            // 
            this.pBarStatus.AutoSize = true;
            this.pBarStatus.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.pBarStatus.Location = new System.Drawing.Point(29, 16);
            this.pBarStatus.Name = "pBarStatus";
            this.pBarStatus.Size = new System.Drawing.Size(70, 16);
            this.pBarStatus.TabIndex = 7;
            this.pBarStatus.Text = "pBarStatus";
            // 
            // oldDBMigrationPBar
            // 
            this.oldDBMigrationPBar.Location = new System.Drawing.Point(32, 39);
            this.oldDBMigrationPBar.Name = "oldDBMigrationPBar";
            this.oldDBMigrationPBar.Size = new System.Drawing.Size(338, 23);
            this.oldDBMigrationPBar.TabIndex = 6;
            // 
            // documents
            // 
            this.documents.Columns.AddRange(new System.Data.DataColumn[] {
            this.dataColumn1,
            this.dataColumn2,
            this.dataColumn3});
            this.documents.TableName = "documents";
            // 
            // dataColumn1
            // 
            this.dataColumn1.AllowDBNull = false;
            this.dataColumn1.AutoIncrement = true;
            this.dataColumn1.ColumnName = "id";
            this.dataColumn1.DataType = typeof(int);
            // 
            // dataColumn2
            // 
            this.dataColumn2.ColumnName = "short_name";
            // 
            // dataColumn3
            // 
            this.dataColumn3.ColumnName = "full_name";
            // 
            // oldDbDataMigration
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(423, 191);
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.cancelBut);
            this.Controls.Add(this.migrateSelected);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow;
            this.Name = "oldDbDataMigration";
            this.Text = "Миграция данных из старых Баз Данных ";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.oldDbDataMigration_FormClosing);
            ((System.ComponentModel.ISupportInitialize)(this.oldEntitiesDS)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.users)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.cables)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.baraban_types)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.cable_structures)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.freq_ranges)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.measured_params)).EndInit();
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.documents)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.Button migrateSelected;
        private System.Data.DataSet oldEntitiesDS;
        private System.Windows.Forms.CheckBox oldUsersCheckBox;
        private System.Windows.Forms.CheckBox oldDbCablesCheckBox;
        private System.Windows.Forms.CheckBox oldDbBarabansCheckBox;
        private System.Windows.Forms.Button cancelBut;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.ProgressBar oldDBMigrationPBar;
        private System.Data.DataTable users;
        private System.Data.DataTable cables;
        private System.Data.DataTable baraban_types;
        private System.Data.DataTable cable_structures;
        private System.Data.DataTable freq_ranges;
        private System.Data.DataTable measured_params;
        private System.Windows.Forms.Label pBarStatus;
        private System.Data.DataTable documents;
        private System.Data.DataColumn dataColumn1;
        private System.Data.DataColumn dataColumn2;
        private System.Data.DataColumn dataColumn3;
    }
}