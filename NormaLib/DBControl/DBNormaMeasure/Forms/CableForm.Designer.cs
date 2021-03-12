namespace NormaLib.DBControl.DBNormaMeasure.Forms
{
    partial class CableForm
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
            this.cableFormDataSet = new System.Data.DataSet();
            this.dataTable1 = new System.Data.DataTable();
            this.CableMark_input = new System.Windows.Forms.ComboBox();
            this.CableStructures_input = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.DocumentName_input = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.DocumentNumber_input = new System.Windows.Forms.ComboBox();
            this.label7 = new System.Windows.Forms.Label();
            this.Ucover_input = new System.Windows.Forms.NumericUpDown();
            this.label6 = new System.Windows.Forms.Label();
            this.linearMass_input = new System.Windows.Forms.NumericUpDown();
            this.BuildLength_input = new System.Windows.Forms.NumericUpDown();
            this.label5 = new System.Windows.Forms.Label();
            this.CodeKCH_input = new System.Windows.Forms.MaskedTextBox();
            this.CodeOKP_input = new System.Windows.Forms.MaskedTextBox();
            this.label12 = new System.Windows.Forms.Label();
            this.label11 = new System.Windows.Forms.Label();
            this.label13 = new System.Windows.Forms.Label();
            this.Notes_input = new System.Windows.Forms.RichTextBox();
            this.panel1 = new System.Windows.Forms.Panel();
            this.saveCableButton = new System.Windows.Forms.Button();
            this.closeNoSaveButton = new System.Windows.Forms.Button();
            this.newTabPage = new System.Windows.Forms.TabPage();
            this.structureDataContainer = new System.Windows.Forms.Panel();
            this.structureType = new System.Windows.Forms.Label();
            this.cbStructureType = new System.Windows.Forms.ComboBox();
            this.tabControl1 = new System.Windows.Forms.TabControl();
            this.btnRemoveCurrentStructure = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.cableFormDataSet)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dataTable1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.Ucover_input)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.linearMass_input)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.BuildLength_input)).BeginInit();
            this.panel1.SuspendLayout();
            this.newTabPage.SuspendLayout();
            this.structureDataContainer.SuspendLayout();
            this.tabControl1.SuspendLayout();
            this.SuspendLayout();
            // 
            // cableFormDataSet
            // 
            this.cableFormDataSet.DataSetName = "NewDataSet";
            this.cableFormDataSet.Tables.AddRange(new System.Data.DataTable[] {
            this.dataTable1});
            // 
            // dataTable1
            // 
            this.dataTable1.TableName = "Table1";
            // 
            // CableMark_input
            // 
            this.CableMark_input.DataSource = this.cableFormDataSet;
            this.CableMark_input.FormattingEnabled = true;
            this.CableMark_input.Location = new System.Drawing.Point(19, 42);
            this.CableMark_input.Name = "CableMark_input";
            this.CableMark_input.Size = new System.Drawing.Size(173, 26);
            this.CableMark_input.TabIndex = 2;
            this.CableMark_input.SelectedIndexChanged += new System.EventHandler(this.CableMark_input_SelectedIndexChanged);
            this.CableMark_input.TextChanged += new System.EventHandler(this.cableNameTextField_TextChanged);
            // 
            // CableStructures_input
            // 
            this.CableStructures_input.Location = new System.Drawing.Point(211, 42);
            this.CableStructures_input.Name = "CableStructures_input";
            this.CableStructures_input.Size = new System.Drawing.Size(681, 26);
            this.CableStructures_input.TabIndex = 4;
            this.CableStructures_input.TextChanged += new System.EventHandler(this.CableStructures_input_TextChanged);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(208, 21);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(151, 18);
            this.label2.TabIndex = 6;
            this.label2.Text = "Описание структуры";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(19, 21);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(103, 18);
            this.label1.TabIndex = 5;
            this.label1.Text = "Марка кабеля";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(208, 83);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(305, 18);
            this.label4.TabIndex = 12;
            this.label4.Text = "Полное название нормативного документа";
            // 
            // DocumentName_input
            // 
            this.DocumentName_input.Location = new System.Drawing.Point(211, 104);
            this.DocumentName_input.Name = "DocumentName_input";
            this.DocumentName_input.Size = new System.Drawing.Size(681, 26);
            this.DocumentName_input.TabIndex = 11;
            this.DocumentName_input.TextChanged += new System.EventHandler(this.DocumentName_input_TextChanged);
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(19, 83);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(76, 18);
            this.label3.TabIndex = 10;
            this.label3.Text = "Норматив";
            // 
            // DocumentNumber_input
            // 
            this.DocumentNumber_input.DataSource = this.cableFormDataSet;
            this.DocumentNumber_input.FormattingEnabled = true;
            this.DocumentNumber_input.Location = new System.Drawing.Point(19, 104);
            this.DocumentNumber_input.Name = "DocumentNumber_input";
            this.DocumentNumber_input.Size = new System.Drawing.Size(173, 26);
            this.DocumentNumber_input.TabIndex = 9;
            this.DocumentNumber_input.TextChanged += new System.EventHandler(this.DocumentNumberInput_Changed);
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(352, 147);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(138, 18);
            this.label7.TabIndex = 21;
            this.label7.Text = "U исп. оболочки, В";
            // 
            // Ucover_input
            // 
            this.Ucover_input.Location = new System.Drawing.Point(355, 167);
            this.Ucover_input.Maximum = new decimal(new int[] {
            10000,
            0,
            0,
            0});
            this.Ucover_input.Name = "Ucover_input";
            this.Ucover_input.Size = new System.Drawing.Size(135, 26);
            this.Ucover_input.TabIndex = 20;
            this.Ucover_input.ValueChanged += new System.EventHandler(this.Ucover_input_ValueChanged);
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(208, 147);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(128, 18);
            this.label6.TabIndex = 19;
            this.label6.Text = "Пог. масса, кг/км";
            // 
            // linearMass_input
            // 
            this.linearMass_input.DecimalPlaces = 2;
            this.linearMass_input.Location = new System.Drawing.Point(211, 167);
            this.linearMass_input.Maximum = new decimal(new int[] {
            10000,
            0,
            0,
            0});
            this.linearMass_input.Name = "linearMass_input";
            this.linearMass_input.Size = new System.Drawing.Size(125, 26);
            this.linearMass_input.TabIndex = 18;
            this.linearMass_input.ThousandsSeparator = true;
            this.linearMass_input.ValueChanged += new System.EventHandler(this.linearMass_input_ValueChanged);
            // 
            // BuildLength_input
            // 
            this.BuildLength_input.Location = new System.Drawing.Point(22, 168);
            this.BuildLength_input.Maximum = new decimal(new int[] {
            10000,
            0,
            0,
            0});
            this.BuildLength_input.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.BuildLength_input.Name = "BuildLength_input";
            this.BuildLength_input.Size = new System.Drawing.Size(173, 26);
            this.BuildLength_input.TabIndex = 17;
            this.BuildLength_input.Value = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.BuildLength_input.ValueChanged += new System.EventHandler(this.BuildLength_input_ValueChanged);
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(19, 147);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(170, 18);
            this.label5.TabIndex = 16;
            this.label5.Text = "Строительная длина, м";
            // 
            // CodeKCH_input
            // 
            this.CodeKCH_input.Location = new System.Drawing.Point(853, 168);
            this.CodeKCH_input.Mask = "00";
            this.CodeKCH_input.Name = "CodeKCH_input";
            this.CodeKCH_input.Size = new System.Drawing.Size(39, 26);
            this.CodeKCH_input.TabIndex = 29;
            this.CodeKCH_input.TextChanged += new System.EventHandler(this.CodeKCH_input_TextChanged);
            // 
            // CodeOKP_input
            // 
            this.CodeOKP_input.Location = new System.Drawing.Point(744, 168);
            this.CodeOKP_input.Mask = "00 0000 0000";
            this.CodeOKP_input.Name = "CodeOKP_input";
            this.CodeOKP_input.Size = new System.Drawing.Size(100, 26);
            this.CodeOKP_input.TabIndex = 28;
            this.CodeOKP_input.TextChanged += new System.EventHandler(this.CodeOKP_input_TextChanged);
            // 
            // label12
            // 
            this.label12.AutoSize = true;
            this.label12.Location = new System.Drawing.Point(850, 152);
            this.label12.Name = "label12";
            this.label12.Size = new System.Drawing.Size(27, 18);
            this.label12.TabIndex = 27;
            this.label12.Text = "КЧ";
            // 
            // label11
            // 
            this.label11.AutoSize = true;
            this.label11.Location = new System.Drawing.Point(741, 152);
            this.label11.Name = "label11";
            this.label11.Size = new System.Drawing.Size(68, 18);
            this.label11.TabIndex = 26;
            this.label11.Text = "Код ОКП";
            // 
            // label13
            // 
            this.label13.AutoSize = true;
            this.label13.Location = new System.Drawing.Point(19, 209);
            this.label13.Name = "label13";
            this.label13.Size = new System.Drawing.Size(92, 18);
            this.label13.TabIndex = 31;
            this.label13.Text = "Примечание";
            // 
            // Notes_input
            // 
            this.Notes_input.BackColor = System.Drawing.Color.White;
            this.Notes_input.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.Notes_input.Font = new System.Drawing.Font("Tahoma", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.Notes_input.Location = new System.Drawing.Point(22, 240);
            this.Notes_input.Name = "Notes_input";
            this.Notes_input.Size = new System.Drawing.Size(870, 100);
            this.Notes_input.TabIndex = 30;
            this.Notes_input.Text = "";
            this.Notes_input.TextChanged += new System.EventHandler(this.Notes_input_TextChanged);
            // 
            // panel1
            // 
            this.panel1.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.panel1.Controls.Add(this.saveCableButton);
            this.panel1.Controls.Add(this.closeNoSaveButton);
            this.panel1.Location = new System.Drawing.Point(22, 767);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(445, 72);
            this.panel1.TabIndex = 32;
            // 
            // saveCableButton
            // 
            this.saveCableButton.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.saveCableButton.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(82)))), ((int)(((byte)(214)))), ((int)(((byte)(37)))));
            this.saveCableButton.FlatAppearance.BorderSize = 0;
            this.saveCableButton.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.saveCableButton.ForeColor = System.Drawing.Color.Gainsboro;
            this.saveCableButton.Location = new System.Drawing.Point(15, 17);
            this.saveCableButton.Name = "saveCableButton";
            this.saveCableButton.Size = new System.Drawing.Size(158, 40);
            this.saveCableButton.TabIndex = 0;
            this.saveCableButton.Text = "Сохранить";
            this.saveCableButton.UseVisualStyleBackColor = false;
            this.saveCableButton.Click += new System.EventHandler(this.saveCableButton_Click);
            // 
            // closeNoSaveButton
            // 
            this.closeNoSaveButton.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.closeNoSaveButton.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(9)))), ((int)(((byte)(44)))), ((int)(((byte)(91)))));
            this.closeNoSaveButton.FlatAppearance.BorderSize = 0;
            this.closeNoSaveButton.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.closeNoSaveButton.ForeColor = System.Drawing.Color.Gainsboro;
            this.closeNoSaveButton.Location = new System.Drawing.Point(189, 17);
            this.closeNoSaveButton.Name = "closeNoSaveButton";
            this.closeNoSaveButton.Size = new System.Drawing.Size(241, 40);
            this.closeNoSaveButton.TabIndex = 26;
            this.closeNoSaveButton.Text = "Закрыть без сохранения";
            this.closeNoSaveButton.UseVisualStyleBackColor = false;
            this.closeNoSaveButton.Click += new System.EventHandler(this.closeNoSaveButton_Click);
            // 
            // newTabPage
            // 
            this.newTabPage.Controls.Add(this.structureDataContainer);
            this.newTabPage.Location = new System.Drawing.Point(4, 27);
            this.newTabPage.Name = "newTabPage";
            this.newTabPage.Padding = new System.Windows.Forms.Padding(3);
            this.newTabPage.Size = new System.Drawing.Size(862, 371);
            this.newTabPage.TabIndex = 0;
            this.newTabPage.Text = "Новая структура";
            this.newTabPage.UseVisualStyleBackColor = true;
            // 
            // structureDataContainer
            // 
            this.structureDataContainer.Controls.Add(this.btnRemoveCurrentStructure);
            this.structureDataContainer.Controls.Add(this.structureType);
            this.structureDataContainer.Controls.Add(this.cbStructureType);
            this.structureDataContainer.Dock = System.Windows.Forms.DockStyle.Fill;
            this.structureDataContainer.Location = new System.Drawing.Point(3, 3);
            this.structureDataContainer.Name = "structureDataContainer";
            this.structureDataContainer.Size = new System.Drawing.Size(856, 365);
            this.structureDataContainer.TabIndex = 0;
            // 
            // structureType
            // 
            this.structureType.AutoSize = true;
            this.structureType.Location = new System.Drawing.Point(5, 11);
            this.structureType.Name = "structureType";
            this.structureType.Size = new System.Drawing.Size(111, 18);
            this.structureType.TabIndex = 1;
            this.structureType.Text = "Тип структуры";
            // 
            // cbStructureType
            // 
            this.cbStructureType.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbStructureType.FormattingEnabled = true;
            this.cbStructureType.Location = new System.Drawing.Point(8, 32);
            this.cbStructureType.Name = "cbStructureType";
            this.cbStructureType.Size = new System.Drawing.Size(108, 26);
            this.cbStructureType.TabIndex = 0;
            this.cbStructureType.SelectedIndexChanged += new System.EventHandler(this.cbStructureType_SelectedIndexChanged);
            // 
            // tabControl1
            // 
            this.tabControl1.Controls.Add(this.newTabPage);
            this.tabControl1.Location = new System.Drawing.Point(22, 359);
            this.tabControl1.Name = "tabControl1";
            this.tabControl1.SelectedIndex = 0;
            this.tabControl1.Size = new System.Drawing.Size(870, 402);
            this.tabControl1.TabIndex = 33;
            this.tabControl1.SelectedIndexChanged += new System.EventHandler(this.tabControl1_SelectedIndexChanged);
            this.tabControl1.Selecting += new System.Windows.Forms.TabControlCancelEventHandler(this.tabControl1_Selecting);
            // 
            // btnRemoveCurrentStructure
            // 
            this.btnRemoveCurrentStructure.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))));
            this.btnRemoveCurrentStructure.FlatAppearance.BorderSize = 0;
            this.btnRemoveCurrentStructure.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnRemoveCurrentStructure.ForeColor = System.Drawing.Color.WhiteSmoke;
            this.btnRemoveCurrentStructure.Location = new System.Drawing.Point(701, 21);
            this.btnRemoveCurrentStructure.Name = "btnRemoveCurrentStructure";
            this.btnRemoveCurrentStructure.Size = new System.Drawing.Size(147, 37);
            this.btnRemoveCurrentStructure.TabIndex = 2;
            this.btnRemoveCurrentStructure.Text = "Удалить";
            this.btnRemoveCurrentStructure.UseVisualStyleBackColor = false;
            this.btnRemoveCurrentStructure.Click += new System.EventHandler(this.btnRemoveCurrentStructure_Click);
            // 
            // CableForm
            // 
            this.BackColor = System.Drawing.Color.White;
            this.ClientSize = new System.Drawing.Size(916, 851);
            this.Controls.Add(this.tabControl1);
            this.Controls.Add(this.Notes_input);
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.label13);
            this.Controls.Add(this.CodeKCH_input);
            this.Controls.Add(this.CodeOKP_input);
            this.Controls.Add(this.label12);
            this.Controls.Add(this.label11);
            this.Controls.Add(this.label7);
            this.Controls.Add(this.Ucover_input);
            this.Controls.Add(this.label6);
            this.Controls.Add(this.linearMass_input);
            this.Controls.Add(this.BuildLength_input);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.DocumentName_input);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.DocumentNumber_input);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.CableStructures_input);
            this.Controls.Add(this.CableMark_input);
            this.Font = new System.Drawing.Font("Tahoma", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.Name = "CableForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            ((System.ComponentModel.ISupportInitialize)(this.cableFormDataSet)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dataTable1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.Ucover_input)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.linearMass_input)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.BuildLength_input)).EndInit();
            this.panel1.ResumeLayout(false);
            this.newTabPage.ResumeLayout(false);
            this.structureDataContainer.ResumeLayout(false);
            this.structureDataContainer.PerformLayout();
            this.tabControl1.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }
        #endregion

        protected System.Data.DataSet cableFormDataSet;
        protected System.Windows.Forms.ComboBox CableMark_input;
        protected System.Windows.Forms.TextBox CableStructures_input;
        protected System.Windows.Forms.Label label2;
        protected System.Windows.Forms.Label label1;
        protected System.Windows.Forms.Label label4;
        protected System.Windows.Forms.TextBox DocumentName_input;
        protected System.Windows.Forms.Label label3;
        protected System.Windows.Forms.ComboBox DocumentNumber_input;
        protected System.Windows.Forms.Label label7;
        protected System.Windows.Forms.NumericUpDown Ucover_input;
        protected System.Windows.Forms.Label label6;
        protected System.Windows.Forms.NumericUpDown linearMass_input;
        protected System.Windows.Forms.NumericUpDown BuildLength_input;
        protected System.Windows.Forms.Label label5;
        protected System.Windows.Forms.MaskedTextBox CodeKCH_input;
        protected System.Windows.Forms.MaskedTextBox CodeOKP_input;
        protected System.Windows.Forms.Label label12;
        protected System.Windows.Forms.Label label11;
        protected System.Windows.Forms.Label label13;
        protected System.Windows.Forms.RichTextBox Notes_input;
        protected System.Windows.Forms.Panel panel1;
        protected System.Windows.Forms.Button closeNoSaveButton;
        protected System.Windows.Forms.Button saveCableButton;
        private System.Data.DataTable dataTable1;
        private System.Windows.Forms.TabPage newTabPage;
        private System.Windows.Forms.Panel structureDataContainer;
        private System.Windows.Forms.Label structureType;
        private System.Windows.Forms.ComboBox cbStructureType;
        private System.Windows.Forms.TabControl tabControl1;
        private System.Windows.Forms.Button btnRemoveCurrentStructure;
    }
}