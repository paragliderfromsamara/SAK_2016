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
            this.label1 = new System.Windows.Forms.Label();
            this.migrateSelected = new System.Windows.Forms.Button();
            this.oldUserDbDataSet = new System.Data.DataSet();
            this.oldUsersCheckBox = new System.Windows.Forms.CheckBox();
            this.oldDbCablesCheckBox = new System.Windows.Forms.CheckBox();
            this.oldDbBarabansCheckBox = new System.Windows.Forms.CheckBox();
            this.test = new System.Windows.Forms.Label();
            this.richTextBox1 = new System.Windows.Forms.RichTextBox();
            this.oldBarabansDataSet = new System.Data.DataSet();
            this.oldCables = new System.Data.DataSet();
            this.oldStructures = new System.Data.DataSet();
            this.oldFreqRanges = new System.Data.DataSet();
            this.oldMeasParams = new System.Data.DataSet();
            this.backgroundWorker1 = new System.ComponentModel.BackgroundWorker();
            ((System.ComponentModel.ISupportInitialize)(this.oldUserDbDataSet)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.oldBarabansDataSet)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.oldCables)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.oldStructures)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.oldFreqRanges)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.oldMeasParams)).BeginInit();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(9, 26);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(138, 13);
            this.label1.TabIndex = 1;
            this.label1.Text = "Список доступных таблиц";
            // 
            // migrateSelected
            // 
            this.migrateSelected.Location = new System.Drawing.Point(12, 218);
            this.migrateSelected.Name = "migrateSelected";
            this.migrateSelected.Size = new System.Drawing.Size(123, 23);
            this.migrateSelected.TabIndex = 2;
            this.migrateSelected.Text = "Загрузить данные";
            this.migrateSelected.UseVisualStyleBackColor = true;
            this.migrateSelected.Click += new System.EventHandler(this.migrateSelected_Click);
            // 
            // oldUserDbDataSet
            // 
            this.oldUserDbDataSet.DataSetName = "NewDataSet";
            // 
            // oldUsersCheckBox
            // 
            this.oldUsersCheckBox.AutoSize = true;
            this.oldUsersCheckBox.Location = new System.Drawing.Point(12, 53);
            this.oldUsersCheckBox.Name = "oldUsersCheckBox";
            this.oldUsersCheckBox.Size = new System.Drawing.Size(220, 17);
            this.oldUsersCheckBox.TabIndex = 3;
            this.oldUsersCheckBox.Text = "Старая БД пользователей (bd_system)";
            this.oldUsersCheckBox.UseVisualStyleBackColor = true;
            this.oldUsersCheckBox.CheckedChanged += new System.EventHandler(this.oldUsersCheckBox_CheckedChanged);
            // 
            // oldDbCablesCheckBox
            // 
            this.oldDbCablesCheckBox.AutoSize = true;
            this.oldDbCablesCheckBox.Location = new System.Drawing.Point(12, 76);
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
            this.oldDbBarabansCheckBox.Location = new System.Drawing.Point(12, 99);
            this.oldDbBarabansCheckBox.Name = "oldDbBarabansCheckBox";
            this.oldDbBarabansCheckBox.Size = new System.Drawing.Size(100, 17);
            this.oldDbBarabansCheckBox.TabIndex = 5;
            this.oldDbBarabansCheckBox.Text = "БД Барабанов";
            this.oldDbBarabansCheckBox.UseVisualStyleBackColor = true;
            this.oldDbBarabansCheckBox.CheckedChanged += new System.EventHandler(this.oldDbBarabansCheckBox_CheckedChanged);
            // 
            // test
            // 
            this.test.AutoSize = true;
            this.test.Location = new System.Drawing.Point(62, 152);
            this.test.Name = "test";
            this.test.Size = new System.Drawing.Size(35, 13);
            this.test.TabIndex = 6;
            this.test.Text = "label2";
            // 
            // richTextBox1
            // 
            this.richTextBox1.Location = new System.Drawing.Point(268, 26);
            this.richTextBox1.Name = "richTextBox1";
            this.richTextBox1.Size = new System.Drawing.Size(823, 321);
            this.richTextBox1.TabIndex = 7;
            this.richTextBox1.Text = "";
            // 
            // oldBarabansDataSet
            // 
            this.oldBarabansDataSet.DataSetName = "NewDataSet";
            // 
            // oldCables
            // 
            this.oldCables.DataSetName = "NewDataSet";
            // 
            // oldStructures
            // 
            this.oldStructures.DataSetName = "NewDataSet";
            // 
            // oldFreqRanges
            // 
            this.oldFreqRanges.DataSetName = "NewDataSet";
            // 
            // oldMeasParams
            // 
            this.oldMeasParams.DataSetName = "NewDataSet";
            // 
            // 
            // oldDbDataMigration
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1147, 372);
            this.Controls.Add(this.richTextBox1);
            this.Controls.Add(this.test);
            this.Controls.Add(this.oldDbBarabansCheckBox);
            this.Controls.Add(this.oldDbCablesCheckBox);
            this.Controls.Add(this.oldUsersCheckBox);
            this.Controls.Add(this.migrateSelected);
            this.Controls.Add(this.label1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow;
            this.Name = "oldDbDataMigration";
            this.Text = "Миграция данных из старых Баз Данных";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.oldDbDataMigration_FormClosing);
            ((System.ComponentModel.ISupportInitialize)(this.oldUserDbDataSet)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.oldBarabansDataSet)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.oldCables)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.oldStructures)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.oldFreqRanges)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.oldMeasParams)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button migrateSelected;
        private System.Data.DataSet oldUserDbDataSet;
        private System.Windows.Forms.CheckBox oldUsersCheckBox;
        private System.Windows.Forms.CheckBox oldDbCablesCheckBox;
        private System.Windows.Forms.CheckBox oldDbBarabansCheckBox;
        private System.Windows.Forms.Label test;
        private System.Windows.Forms.RichTextBox richTextBox1;
        private System.Data.DataSet oldBarabansDataSet;
        private System.Data.DataSet oldCables;
        private System.Data.DataSet oldStructures;
        private System.Data.DataSet oldFreqRanges;
        private System.Data.DataSet oldMeasParams;
        private System.ComponentModel.BackgroundWorker backgroundWorker1;
    }
}