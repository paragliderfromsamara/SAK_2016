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
            this.components = new System.ComponentModel.Container();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle4 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle6 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle5 = new System.Windows.Forms.DataGridViewCellStyle();
            this.cableFormDataSet = new System.Data.DataSet();
            this.CableMark_input = new System.Windows.Forms.ComboBox();
            this.CableStructures_input = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.DocumentName_input = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.DocumentNumber_input = new System.Windows.Forms.ComboBox();
            this.label7 = new System.Windows.Forms.Label();
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
            this.panelControlButtons = new System.Windows.Forms.Panel();
            this.saveCableButton = new System.Windows.Forms.Button();
            this.closeNoSaveButton = new System.Windows.Forms.Button();
            this.newTabPage = new System.Windows.Forms.TabPage();
            this.structureDataContainer = new System.Windows.Forms.Panel();
            this.cbLeadDiameters = new System.Windows.Forms.ComboBox();
            this.cbWaveResistance = new System.Windows.Forms.ComboBox();
            this.label22 = new System.Windows.Forms.Label();
            this.panel1 = new System.Windows.Forms.Panel();
            this.dgMeasuredParameters = new System.Windows.Forms.DataGridView();
            this.MeasuredParametersBindingSource = new System.Windows.Forms.BindingSource(this.components);
            this.cbGroupCapacity = new System.Windows.Forms.CheckBox();
            this.groupBox3 = new System.Windows.Forms.GroupBox();
            this.cbDRBringingFormula = new System.Windows.Forms.ComboBox();
            this.label21 = new System.Windows.Forms.Label();
            this.cbDRCalculatingFormula = new System.Windows.Forms.ComboBox();
            this.label20 = new System.Windows.Forms.Label();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.label19 = new System.Windows.Forms.Label();
            this.nudLeadToShieldVoltage = new System.Windows.Forms.NumericUpDown();
            this.label18 = new System.Windows.Forms.Label();
            this.nudLeadToLeadVoltage = new System.Windows.Forms.NumericUpDown();
            this.nudNumberInGroup = new System.Windows.Forms.NumericUpDown();
            this.label17 = new System.Windows.Forms.Label();
            this.label16 = new System.Windows.Forms.Label();
            this.cbIsolationMaterial = new System.Windows.Forms.ComboBox();
            this.label15 = new System.Windows.Forms.Label();
            this.label14 = new System.Windows.Forms.Label();
            this.cbLeadMaterial = new System.Windows.Forms.ComboBox();
            this.label10 = new System.Windows.Forms.Label();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.label9 = new System.Windows.Forms.Label();
            this.nudRrealElementsAmount = new System.Windows.Forms.NumericUpDown();
            this.nudDisplayedElementAmount = new System.Windows.Forms.NumericUpDown();
            this.label8 = new System.Windows.Forms.Label();
            this.btnRemoveCurrentStructure = new System.Windows.Forms.Button();
            this.structureType = new System.Windows.Forms.Label();
            this.cbStructureType = new System.Windows.Forms.ComboBox();
            this.tabControl1 = new System.Windows.Forms.TabControl();
            this.addMeasurerParameterContextMenu = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.AddAllToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.contextMenuBringingLength = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.toolStripMenuItem1 = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripMenuItem2 = new System.Windows.Forms.ToolStripMenuItem();
            this.groupBox4 = new System.Windows.Forms.GroupBox();
            this.label24 = new System.Windows.Forms.Label();
            this.label23 = new System.Windows.Forms.Label();
            this.cbPmax = new System.Windows.Forms.ComboBox();
            this.cbPmin = new System.Windows.Forms.ComboBox();
            this.cbVoltageOfCoverTest = new System.Windows.Forms.ComboBox();
            this.parameter_type_name_column = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.parameter_type_id_column = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.parameterTypeDescriptionColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.parameterTypeMeasureColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.minValueColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.maxValueColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.percentColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.resultMeasureColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.lengthBringingColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.frequencyMinColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.freqMaxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.freqStepColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.delButtonColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.MeasuredParameterDataId_Column = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.lengthBringingTypeIdColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.BringingLengthMeasureTitleColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.cbRisolVoltageValue = new System.Windows.Forms.ComboBox();
            this.label25 = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.cableFormDataSet)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.linearMass_input)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.BuildLength_input)).BeginInit();
            this.panelControlButtons.SuspendLayout();
            this.newTabPage.SuspendLayout();
            this.structureDataContainer.SuspendLayout();
            this.panel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgMeasuredParameters)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.MeasuredParametersBindingSource)).BeginInit();
            this.groupBox3.SuspendLayout();
            this.groupBox2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.nudLeadToShieldVoltage)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.nudLeadToLeadVoltage)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.nudNumberInGroup)).BeginInit();
            this.groupBox1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.nudRrealElementsAmount)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.nudDisplayedElementAmount)).BeginInit();
            this.tabControl1.SuspendLayout();
            this.addMeasurerParameterContextMenu.SuspendLayout();
            this.contextMenuBringingLength.SuspendLayout();
            this.groupBox4.SuspendLayout();
            this.SuspendLayout();
            // 
            // cableFormDataSet
            // 
            this.cableFormDataSet.DataSetName = "NewDataSet";
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
            this.CableStructures_input.Size = new System.Drawing.Size(795, 26);
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
            this.DocumentName_input.Size = new System.Drawing.Size(795, 26);
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
            this.label7.Location = new System.Drawing.Point(208, 148);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(138, 18);
            this.label7.TabIndex = 21;
            this.label7.Text = "U исп. оболочки, В";
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(19, 203);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(128, 18);
            this.label6.TabIndex = 19;
            this.label6.Text = "Пог. масса, кг/км";
            // 
            // linearMass_input
            // 
            this.linearMass_input.DecimalPlaces = 2;
            this.linearMass_input.Location = new System.Drawing.Point(22, 223);
            this.linearMass_input.Maximum = new decimal(new int[] {
            10000,
            0,
            0,
            0});
            this.linearMass_input.Name = "linearMass_input";
            this.linearMass_input.Size = new System.Drawing.Size(173, 26);
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
            this.CodeKCH_input.Location = new System.Drawing.Point(320, 222);
            this.CodeKCH_input.Mask = "00";
            this.CodeKCH_input.Name = "CodeKCH_input";
            this.CodeKCH_input.Size = new System.Drawing.Size(39, 26);
            this.CodeKCH_input.TabIndex = 29;
            this.CodeKCH_input.TextChanged += new System.EventHandler(this.CodeKCH_input_TextChanged);
            // 
            // CodeOKP_input
            // 
            this.CodeOKP_input.Location = new System.Drawing.Point(211, 222);
            this.CodeOKP_input.Mask = "00 0000 0000";
            this.CodeOKP_input.Name = "CodeOKP_input";
            this.CodeOKP_input.Size = new System.Drawing.Size(100, 26);
            this.CodeOKP_input.TabIndex = 28;
            this.CodeOKP_input.TextChanged += new System.EventHandler(this.CodeOKP_input_TextChanged);
            // 
            // label12
            // 
            this.label12.AutoSize = true;
            this.label12.Location = new System.Drawing.Point(317, 206);
            this.label12.Name = "label12";
            this.label12.Size = new System.Drawing.Size(27, 18);
            this.label12.TabIndex = 27;
            this.label12.Text = "КЧ";
            // 
            // label11
            // 
            this.label11.AutoSize = true;
            this.label11.Location = new System.Drawing.Point(208, 206);
            this.label11.Name = "label11";
            this.label11.Size = new System.Drawing.Size(68, 18);
            this.label11.TabIndex = 26;
            this.label11.Text = "Код ОКП";
            // 
            // label13
            // 
            this.label13.AutoSize = true;
            this.label13.Location = new System.Drawing.Point(582, 148);
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
            this.Notes_input.Location = new System.Drawing.Point(585, 168);
            this.Notes_input.Name = "Notes_input";
            this.Notes_input.Size = new System.Drawing.Size(421, 80);
            this.Notes_input.TabIndex = 30;
            this.Notes_input.Text = "";
            this.Notes_input.TextChanged += new System.EventHandler(this.Notes_input_TextChanged);
            // 
            // panelControlButtons
            // 
            this.panelControlButtons.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.panelControlButtons.Controls.Add(this.saveCableButton);
            this.panelControlButtons.Controls.Add(this.closeNoSaveButton);
            this.panelControlButtons.Location = new System.Drawing.Point(22, 887);
            this.panelControlButtons.Name = "panelControlButtons";
            this.panelControlButtons.Size = new System.Drawing.Size(412, 72);
            this.panelControlButtons.TabIndex = 32;
            // 
            // saveCableButton
            // 
            this.saveCableButton.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.saveCableButton.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(82)))), ((int)(((byte)(214)))), ((int)(((byte)(37)))));
            this.saveCableButton.FlatAppearance.BorderSize = 0;
            this.saveCableButton.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.saveCableButton.ForeColor = System.Drawing.Color.Gainsboro;
            this.saveCableButton.Location = new System.Drawing.Point(0, 17);
            this.saveCableButton.Name = "saveCableButton";
            this.saveCableButton.Size = new System.Drawing.Size(158, 40);
            this.saveCableButton.TabIndex = 0;
            this.saveCableButton.Text = "Сохранить";
            this.saveCableButton.UseVisualStyleBackColor = false;
            this.saveCableButton.Click += new System.EventHandler(this.saveCableButton_Click);
            // 
            // closeNoSaveButton
            // 
            this.closeNoSaveButton.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.closeNoSaveButton.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(9)))), ((int)(((byte)(44)))), ((int)(((byte)(91)))));
            this.closeNoSaveButton.FlatAppearance.BorderSize = 0;
            this.closeNoSaveButton.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.closeNoSaveButton.ForeColor = System.Drawing.Color.Gainsboro;
            this.closeNoSaveButton.Location = new System.Drawing.Point(171, 17);
            this.closeNoSaveButton.Name = "closeNoSaveButton";
            this.closeNoSaveButton.Size = new System.Drawing.Size(241, 40);
            this.closeNoSaveButton.TabIndex = 26;
            this.closeNoSaveButton.Text = "Закрыть";
            this.closeNoSaveButton.UseVisualStyleBackColor = false;
            this.closeNoSaveButton.Click += new System.EventHandler(this.closeNoSaveButton_Click);
            // 
            // newTabPage
            // 
            this.newTabPage.Controls.Add(this.structureDataContainer);
            this.newTabPage.Location = new System.Drawing.Point(4, 39);
            this.newTabPage.Name = "newTabPage";
            this.newTabPage.Padding = new System.Windows.Forms.Padding(3);
            this.newTabPage.Size = new System.Drawing.Size(980, 583);
            this.newTabPage.TabIndex = 0;
            this.newTabPage.Text = "Новая структура";
            this.newTabPage.UseVisualStyleBackColor = true;
            // 
            // structureDataContainer
            // 
            this.structureDataContainer.Controls.Add(this.label25);
            this.structureDataContainer.Controls.Add(this.cbRisolVoltageValue);
            this.structureDataContainer.Controls.Add(this.cbLeadDiameters);
            this.structureDataContainer.Controls.Add(this.cbWaveResistance);
            this.structureDataContainer.Controls.Add(this.label22);
            this.structureDataContainer.Controls.Add(this.panel1);
            this.structureDataContainer.Controls.Add(this.cbGroupCapacity);
            this.structureDataContainer.Controls.Add(this.groupBox3);
            this.structureDataContainer.Controls.Add(this.groupBox2);
            this.structureDataContainer.Controls.Add(this.nudNumberInGroup);
            this.structureDataContainer.Controls.Add(this.label17);
            this.structureDataContainer.Controls.Add(this.label16);
            this.structureDataContainer.Controls.Add(this.cbIsolationMaterial);
            this.structureDataContainer.Controls.Add(this.label15);
            this.structureDataContainer.Controls.Add(this.label14);
            this.structureDataContainer.Controls.Add(this.cbLeadMaterial);
            this.structureDataContainer.Controls.Add(this.label10);
            this.structureDataContainer.Controls.Add(this.groupBox1);
            this.structureDataContainer.Controls.Add(this.btnRemoveCurrentStructure);
            this.structureDataContainer.Controls.Add(this.structureType);
            this.structureDataContainer.Controls.Add(this.cbStructureType);
            this.structureDataContainer.Dock = System.Windows.Forms.DockStyle.Fill;
            this.structureDataContainer.Location = new System.Drawing.Point(3, 3);
            this.structureDataContainer.Name = "structureDataContainer";
            this.structureDataContainer.Size = new System.Drawing.Size(974, 577);
            this.structureDataContainer.TabIndex = 0;
            // 
            // cbLeadDiameters
            // 
            this.cbLeadDiameters.FormattingEnabled = true;
            this.cbLeadDiameters.Location = new System.Drawing.Point(459, 37);
            this.cbLeadDiameters.Name = "cbLeadDiameters";
            this.cbLeadDiameters.Size = new System.Drawing.Size(128, 26);
            this.cbLeadDiameters.TabIndex = 24;
            this.cbLeadDiameters.TextChanged += new System.EventHandler(this.cbLeadDiameters_TextChanged);
            this.cbLeadDiameters.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.numFormatCheckerOn_KeyPress);
            // 
            // cbWaveResistance
            // 
            this.cbWaveResistance.FormattingEnabled = true;
            this.cbWaveResistance.Location = new System.Drawing.Point(138, 170);
            this.cbWaveResistance.Name = "cbWaveResistance";
            this.cbWaveResistance.Size = new System.Drawing.Size(108, 26);
            this.cbWaveResistance.TabIndex = 23;
            this.cbWaveResistance.TextChanged += new System.EventHandler(this.cbWaveResistance_TextChanged);
            this.cbWaveResistance.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.numFormatCheckerOn_KeyPress);
            // 
            // label22
            // 
            this.label22.AutoSize = true;
            this.label22.Location = new System.Drawing.Point(264, 74);
            this.label22.Name = "label22";
            this.label22.Size = new System.Drawing.Size(244, 18);
            this.label22.TabIndex = 21;
            this.label22.Text = "Таблица измеряемых параметров";
            // 
            // panel1
            // 
            this.panel1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.panel1.Controls.Add(this.dgMeasuredParameters);
            this.panel1.Location = new System.Drawing.Point(267, 95);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(693, 431);
            this.panel1.TabIndex = 20;
            // 
            // dgMeasuredParameters
            // 
            this.dgMeasuredParameters.AllowUserToAddRows = false;
            this.dgMeasuredParameters.AllowUserToDeleteRows = false;
            this.dgMeasuredParameters.AutoGenerateColumns = false;
            this.dgMeasuredParameters.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.dgMeasuredParameters.AutoSizeRowsMode = System.Windows.Forms.DataGridViewAutoSizeRowsMode.DisplayedCells;
            this.dgMeasuredParameters.BackgroundColor = System.Drawing.Color.White;
            this.dgMeasuredParameters.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.dgMeasuredParameters.CausesValidation = false;
            this.dgMeasuredParameters.ColumnHeadersBorderStyle = System.Windows.Forms.DataGridViewHeaderBorderStyle.None;
            dataGridViewCellStyle4.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle4.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(65)))), ((int)(((byte)(109)))));
            dataGridViewCellStyle4.Font = new System.Drawing.Font("Tahoma", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            dataGridViewCellStyle4.ForeColor = System.Drawing.Color.Gainsboro;
            dataGridViewCellStyle4.NullValue = "-";
            dataGridViewCellStyle4.Padding = new System.Windows.Forms.Padding(3);
            dataGridViewCellStyle4.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle4.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle4.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.dgMeasuredParameters.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle4;
            this.dgMeasuredParameters.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgMeasuredParameters.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.parameter_type_name_column,
            this.parameter_type_id_column,
            this.parameterTypeDescriptionColumn,
            this.parameterTypeMeasureColumn,
            this.minValueColumn,
            this.maxValueColumn,
            this.percentColumn,
            this.resultMeasureColumn,
            this.lengthBringingColumn,
            this.frequencyMinColumn,
            this.freqMaxColumn,
            this.freqStepColumn,
            this.delButtonColumn,
            this.MeasuredParameterDataId_Column,
            this.lengthBringingTypeIdColumn,
            this.BringingLengthMeasureTitleColumn});
            this.dgMeasuredParameters.DataSource = this.MeasuredParametersBindingSource;
            this.dgMeasuredParameters.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dgMeasuredParameters.EnableHeadersVisualStyles = false;
            this.dgMeasuredParameters.Location = new System.Drawing.Point(0, 0);
            this.dgMeasuredParameters.MultiSelect = false;
            this.dgMeasuredParameters.Name = "dgMeasuredParameters";
            this.dgMeasuredParameters.RowHeadersBorderStyle = System.Windows.Forms.DataGridViewHeaderBorderStyle.Single;
            this.dgMeasuredParameters.RowHeadersVisible = false;
            dataGridViewCellStyle6.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(244)))), ((int)(((byte)(251)))), ((int)(((byte)(255)))));
            dataGridViewCellStyle6.Font = new System.Drawing.Font("Microsoft Sans Serif", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            dataGridViewCellStyle6.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            dataGridViewCellStyle6.NullValue = "-";
            dataGridViewCellStyle6.Padding = new System.Windows.Forms.Padding(5, 2, 5, 2);
            dataGridViewCellStyle6.SelectionBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(20)))), ((int)(((byte)(108)))), ((int)(((byte)(205)))));
            dataGridViewCellStyle6.SelectionForeColor = System.Drawing.Color.Gainsboro;
            this.dgMeasuredParameters.RowsDefaultCellStyle = dataGridViewCellStyle6;
            this.dgMeasuredParameters.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.dgMeasuredParameters.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.CellSelect;
            this.dgMeasuredParameters.Size = new System.Drawing.Size(693, 431);
            this.dgMeasuredParameters.TabIndex = 0;
            this.dgMeasuredParameters.CellMouseClick += new System.Windows.Forms.DataGridViewCellMouseEventHandler(this.dgMeasuredParameters_CellMouseClick);
            this.dgMeasuredParameters.ColumnHeaderMouseClick += new System.Windows.Forms.DataGridViewCellMouseEventHandler(this.dgMeasuredParameters_ColumnHeaderMouseClick);
            this.dgMeasuredParameters.CurrentCellDirtyStateChanged += new System.EventHandler(this.dgMeasuredParameters_CurrentCellDirtyStateChanged);
            this.dgMeasuredParameters.DataError += new System.Windows.Forms.DataGridViewDataErrorEventHandler(this.dgMeasuredParameters_DataError);
            this.dgMeasuredParameters.RowsAdded += new System.Windows.Forms.DataGridViewRowsAddedEventHandler(this.dgMeasuredParameters_RowsAdded);
            // 
            // MeasuredParametersBindingSource
            // 
            this.MeasuredParametersBindingSource.DataSourceChanged += new System.EventHandler(this.MeasuredParametersBindingSource_DataSourceChanged);
            // 
            // cbGroupCapacity
            // 
            this.cbGroupCapacity.AutoSize = true;
            this.cbGroupCapacity.Location = new System.Drawing.Point(21, 539);
            this.cbGroupCapacity.Name = "cbGroupCapacity";
            this.cbGroupCapacity.Size = new System.Drawing.Size(97, 22);
            this.cbGroupCapacity.TabIndex = 19;
            this.cbGroupCapacity.Text = "Ср группы";
            this.cbGroupCapacity.UseVisualStyleBackColor = true;
            this.cbGroupCapacity.CheckedChanged += new System.EventHandler(this.cbGroupCapacity_CheckedChanged);
            // 
            // groupBox3
            // 
            this.groupBox3.Controls.Add(this.cbDRBringingFormula);
            this.groupBox3.Controls.Add(this.label21);
            this.groupBox3.Controls.Add(this.cbDRCalculatingFormula);
            this.groupBox3.Controls.Add(this.label20);
            this.groupBox3.Location = new System.Drawing.Point(8, 366);
            this.groupBox3.Name = "groupBox3";
            this.groupBox3.Size = new System.Drawing.Size(241, 160);
            this.groupBox3.TabIndex = 17;
            this.groupBox3.TabStop = false;
            this.groupBox3.Text = "Омическая ассиметрия";
            // 
            // cbDRBringingFormula
            // 
            this.cbDRBringingFormula.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbDRBringingFormula.FormattingEnabled = true;
            this.cbDRBringingFormula.Location = new System.Drawing.Point(14, 112);
            this.cbDRBringingFormula.Name = "cbDRBringingFormula";
            this.cbDRBringingFormula.Size = new System.Drawing.Size(212, 26);
            this.cbDRBringingFormula.TabIndex = 18;
            this.cbDRBringingFormula.SelectedIndexChanged += new System.EventHandler(this.cbDRBringingFormula_SelectedIndexChanged);
            // 
            // label21
            // 
            this.label21.AutoSize = true;
            this.label21.Location = new System.Drawing.Point(11, 91);
            this.label21.Name = "label21";
            this.label21.Size = new System.Drawing.Size(153, 18);
            this.label21.TabIndex = 19;
            this.label21.Text = "Формула приведения";
            // 
            // cbDRCalculatingFormula
            // 
            this.cbDRCalculatingFormula.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbDRCalculatingFormula.FormattingEnabled = true;
            this.cbDRCalculatingFormula.Location = new System.Drawing.Point(14, 52);
            this.cbDRCalculatingFormula.Name = "cbDRCalculatingFormula";
            this.cbDRCalculatingFormula.Size = new System.Drawing.Size(212, 26);
            this.cbDRCalculatingFormula.TabIndex = 18;
            this.cbDRCalculatingFormula.SelectedIndexChanged += new System.EventHandler(this.cbDRCalculatingFormula_SelectedIndexChanged);
            // 
            // label20
            // 
            this.label20.AutoSize = true;
            this.label20.Location = new System.Drawing.Point(11, 31);
            this.label20.Name = "label20";
            this.label20.Size = new System.Drawing.Size(154, 18);
            this.label20.TabIndex = 0;
            this.label20.Text = "Формула вычисления";
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.label19);
            this.groupBox2.Controls.Add(this.nudLeadToShieldVoltage);
            this.groupBox2.Controls.Add(this.label18);
            this.groupBox2.Controls.Add(this.nudLeadToLeadVoltage);
            this.groupBox2.Location = new System.Drawing.Point(8, 260);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(241, 100);
            this.groupBox2.TabIndex = 16;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "Испытательные напряжения, В";
            // 
            // label19
            // 
            this.label19.AutoSize = true;
            this.label19.Location = new System.Drawing.Point(126, 31);
            this.label19.Name = "label19";
            this.label19.Size = new System.Drawing.Size(87, 18);
            this.label19.TabIndex = 17;
            this.label19.Text = "жила-экран";
            // 
            // nudLeadToShieldVoltage
            // 
            this.nudLeadToShieldVoltage.Location = new System.Drawing.Point(130, 52);
            this.nudLeadToShieldVoltage.Maximum = new decimal(new int[] {
            3000,
            0,
            0,
            0});
            this.nudLeadToShieldVoltage.Name = "nudLeadToShieldVoltage";
            this.nudLeadToShieldVoltage.Size = new System.Drawing.Size(96, 26);
            this.nudLeadToShieldVoltage.TabIndex = 2;
            this.nudLeadToShieldVoltage.ValueChanged += new System.EventHandler(this.numericUpDown6_ValueChanged);
            // 
            // label18
            // 
            this.label18.AutoSize = true;
            this.label18.Location = new System.Drawing.Point(11, 31);
            this.label18.Name = "label18";
            this.label18.Size = new System.Drawing.Size(85, 18);
            this.label18.TabIndex = 1;
            this.label18.Text = "жила-жила";
            // 
            // nudLeadToLeadVoltage
            // 
            this.nudLeadToLeadVoltage.Location = new System.Drawing.Point(14, 52);
            this.nudLeadToLeadVoltage.Maximum = new decimal(new int[] {
            3000,
            0,
            0,
            0});
            this.nudLeadToLeadVoltage.Name = "nudLeadToLeadVoltage";
            this.nudLeadToLeadVoltage.Size = new System.Drawing.Size(97, 26);
            this.nudLeadToLeadVoltage.TabIndex = 0;
            this.nudLeadToLeadVoltage.ValueChanged += new System.EventHandler(this.numericUpDown5_ValueChanged);
            // 
            // nudNumberInGroup
            // 
            this.nudNumberInGroup.Location = new System.Drawing.Point(139, 215);
            this.nudNumberInGroup.Name = "nudNumberInGroup";
            this.nudNumberInGroup.Size = new System.Drawing.Size(110, 26);
            this.nudNumberInGroup.TabIndex = 15;
            this.nudNumberInGroup.ValueChanged += new System.EventHandler(this.nudNumberInGroup_ValueChanged);
            // 
            // label17
            // 
            this.label17.AutoSize = true;
            this.label17.Location = new System.Drawing.Point(8, 210);
            this.label17.Name = "label17";
            this.label17.Size = new System.Drawing.Size(88, 36);
            this.label17.TabIndex = 14;
            this.label17.Text = "Элементов \nв пучке";
            // 
            // label16
            // 
            this.label16.AutoSize = true;
            this.label16.Location = new System.Drawing.Point(8, 165);
            this.label16.Name = "label16";
            this.label16.Size = new System.Drawing.Size(111, 36);
            this.label16.TabIndex = 12;
            this.label16.Text = "Волновое\nсопротивление";
            // 
            // cbIsolationMaterial
            // 
            this.cbIsolationMaterial.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbIsolationMaterial.FormattingEnabled = true;
            this.cbIsolationMaterial.Location = new System.Drawing.Point(605, 37);
            this.cbIsolationMaterial.Name = "cbIsolationMaterial";
            this.cbIsolationMaterial.Size = new System.Drawing.Size(174, 26);
            this.cbIsolationMaterial.TabIndex = 11;
            this.cbIsolationMaterial.SelectedIndexChanged += new System.EventHandler(this.cbIsolationMaterial_SelectedIndexChanged);
            // 
            // label15
            // 
            this.label15.AutoSize = true;
            this.label15.Location = new System.Drawing.Point(602, 16);
            this.label15.Name = "label15";
            this.label15.Size = new System.Drawing.Size(145, 18);
            this.label15.TabIndex = 10;
            this.label15.Text = "Материал изоляции";
            // 
            // label14
            // 
            this.label14.AutoSize = true;
            this.label14.Location = new System.Drawing.Point(456, 16);
            this.label14.Name = "label14";
            this.label14.Size = new System.Drawing.Size(131, 18);
            this.label14.TabIndex = 9;
            this.label14.Text = "Диаметр жил, мм";
            // 
            // cbLeadMaterial
            // 
            this.cbLeadMaterial.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbLeadMaterial.FormattingEnabled = true;
            this.cbLeadMaterial.Location = new System.Drawing.Point(267, 37);
            this.cbLeadMaterial.Name = "cbLeadMaterial";
            this.cbLeadMaterial.Size = new System.Drawing.Size(174, 26);
            this.cbLeadMaterial.TabIndex = 7;
            this.cbLeadMaterial.SelectedIndexChanged += new System.EventHandler(this.cbLeadMaterial_SelectedIndexChanged);
            // 
            // label10
            // 
            this.label10.AutoSize = true;
            this.label10.Location = new System.Drawing.Point(264, 16);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(109, 18);
            this.label10.TabIndex = 6;
            this.label10.Text = "Материал жил";
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.label9);
            this.groupBox1.Controls.Add(this.nudRrealElementsAmount);
            this.groupBox1.Controls.Add(this.nudDisplayedElementAmount);
            this.groupBox1.Controls.Add(this.label8);
            this.groupBox1.Location = new System.Drawing.Point(8, 69);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(241, 91);
            this.groupBox1.TabIndex = 5;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Количество элементов";
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.Location = new System.Drawing.Point(126, 26);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(100, 18);
            this.label9.TabIndex = 6;
            this.label9.Text = "Номинальное";
            // 
            // nudRrealElementsAmount
            // 
            this.nudRrealElementsAmount.Location = new System.Drawing.Point(14, 47);
            this.nudRrealElementsAmount.Maximum = new decimal(new int[] {
            10000,
            0,
            0,
            0});
            this.nudRrealElementsAmount.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.nudRrealElementsAmount.Name = "nudRrealElementsAmount";
            this.nudRrealElementsAmount.Size = new System.Drawing.Size(97, 26);
            this.nudRrealElementsAmount.TabIndex = 3;
            this.nudRrealElementsAmount.Value = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.nudRrealElementsAmount.ValueChanged += new System.EventHandler(this.nudRrealElementsAmount_ValueChanged);
            // 
            // nudDisplayedElementAmount
            // 
            this.nudDisplayedElementAmount.Location = new System.Drawing.Point(129, 47);
            this.nudDisplayedElementAmount.Maximum = new decimal(new int[] {
            10000,
            0,
            0,
            0});
            this.nudDisplayedElementAmount.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.nudDisplayedElementAmount.Name = "nudDisplayedElementAmount";
            this.nudDisplayedElementAmount.Size = new System.Drawing.Size(97, 26);
            this.nudDisplayedElementAmount.TabIndex = 4;
            this.nudDisplayedElementAmount.Value = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.nudDisplayedElementAmount.ValueChanged += new System.EventHandler(this.nudDisplayedElementAmount_ValueChanged);
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Location = new System.Drawing.Point(11, 26);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(95, 18);
            this.label8.TabIndex = 6;
            this.label8.Text = "Фактическое";
            // 
            // btnRemoveCurrentStructure
            // 
            this.btnRemoveCurrentStructure.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnRemoveCurrentStructure.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))));
            this.btnRemoveCurrentStructure.FlatAppearance.BorderSize = 0;
            this.btnRemoveCurrentStructure.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnRemoveCurrentStructure.ForeColor = System.Drawing.Color.WhiteSmoke;
            this.btnRemoveCurrentStructure.Location = new System.Drawing.Point(763, 532);
            this.btnRemoveCurrentStructure.Name = "btnRemoveCurrentStructure";
            this.btnRemoveCurrentStructure.Size = new System.Drawing.Size(197, 40);
            this.btnRemoveCurrentStructure.TabIndex = 2;
            this.btnRemoveCurrentStructure.Text = "Удалить структуру";
            this.btnRemoveCurrentStructure.UseVisualStyleBackColor = false;
            this.btnRemoveCurrentStructure.Click += new System.EventHandler(this.btnRemoveCurrentStructure_Click);
            // 
            // structureType
            // 
            this.structureType.AutoSize = true;
            this.structureType.Location = new System.Drawing.Point(7, 16);
            this.structureType.Name = "structureType";
            this.structureType.Size = new System.Drawing.Size(111, 18);
            this.structureType.TabIndex = 1;
            this.structureType.Text = "Тип структуры";
            // 
            // cbStructureType
            // 
            this.cbStructureType.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbStructureType.FormattingEnabled = true;
            this.cbStructureType.Location = new System.Drawing.Point(8, 37);
            this.cbStructureType.Name = "cbStructureType";
            this.cbStructureType.Size = new System.Drawing.Size(241, 26);
            this.cbStructureType.TabIndex = 0;
            this.cbStructureType.SelectedIndexChanged += new System.EventHandler(this.cbStructureType_SelectedIndexChanged);
            this.cbStructureType.SelectionChangeCommitted += new System.EventHandler(this.cbStructureType_SelectionChangeCommitted);
            // 
            // tabControl1
            // 
            this.tabControl1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.tabControl1.Controls.Add(this.newTabPage);
            this.tabControl1.ItemSize = new System.Drawing.Size(129, 35);
            this.tabControl1.Location = new System.Drawing.Point(22, 263);
            this.tabControl1.Name = "tabControl1";
            this.tabControl1.Padding = new System.Drawing.Point(25, 3);
            this.tabControl1.SelectedIndex = 0;
            this.tabControl1.Size = new System.Drawing.Size(988, 626);
            this.tabControl1.TabIndex = 33;
            this.tabControl1.Selecting += new System.Windows.Forms.TabControlCancelEventHandler(this.tabControl1_Selecting);
            this.tabControl1.Deselecting += new System.Windows.Forms.TabControlCancelEventHandler(this.tabControl1_Deselecting);
            // 
            // addMeasurerParameterContextMenu
            // 
            this.addMeasurerParameterContextMenu.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(65)))), ((int)(((byte)(109)))));
            this.addMeasurerParameterContextMenu.Font = new System.Drawing.Font("Segoe UI", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.addMeasurerParameterContextMenu.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.AddAllToolStripMenuItem});
            this.addMeasurerParameterContextMenu.Name = "addMeasurerParameterContextMenu";
            this.addMeasurerParameterContextMenu.ShowImageMargin = false;
            this.addMeasurerParameterContextMenu.Size = new System.Drawing.Size(148, 28);
            this.addMeasurerParameterContextMenu.Opening += new System.ComponentModel.CancelEventHandler(this.addMeasurerParameterContextMenu_Opening);
            // 
            // AddAllToolStripMenuItem
            // 
            this.AddAllToolStripMenuItem.ForeColor = System.Drawing.Color.Gainsboro;
            this.AddAllToolStripMenuItem.Name = "AddAllToolStripMenuItem";
            this.AddAllToolStripMenuItem.Size = new System.Drawing.Size(147, 24);
            this.AddAllToolStripMenuItem.Tag = "0";
            this.AddAllToolStripMenuItem.Text = "Добавить все";
            this.AddAllToolStripMenuItem.Click += new System.EventHandler(this.AddMeasureParameterContextMenuItem_Click);
            // 
            // contextMenuBringingLength
            // 
            this.contextMenuBringingLength.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripMenuItem1,
            this.toolStripMenuItem2});
            this.contextMenuBringingLength.Name = "contextMenuBringingLength";
            this.contextMenuBringingLength.ShowCheckMargin = true;
            this.contextMenuBringingLength.ShowImageMargin = false;
            this.contextMenuBringingLength.Size = new System.Drawing.Size(181, 48);
            // 
            // toolStripMenuItem1
            // 
            this.toolStripMenuItem1.Name = "toolStripMenuItem1";
            this.toolStripMenuItem1.Size = new System.Drawing.Size(180, 22);
            this.toolStripMenuItem1.Text = "toolStripMenuItem1";
            // 
            // toolStripMenuItem2
            // 
            this.toolStripMenuItem2.Name = "toolStripMenuItem2";
            this.toolStripMenuItem2.Size = new System.Drawing.Size(180, 22);
            this.toolStripMenuItem2.Text = "toolStripMenuItem2";
            // 
            // groupBox4
            // 
            this.groupBox4.Controls.Add(this.label24);
            this.groupBox4.Controls.Add(this.label23);
            this.groupBox4.Controls.Add(this.cbPmax);
            this.groupBox4.Controls.Add(this.cbPmin);
            this.groupBox4.Location = new System.Drawing.Point(378, 149);
            this.groupBox4.Name = "groupBox4";
            this.groupBox4.Size = new System.Drawing.Size(198, 100);
            this.groupBox4.TabIndex = 34;
            this.groupBox4.TabStop = false;
            this.groupBox4.Text = "Изб. давление, кг/см^2";
            // 
            // label24
            // 
            this.label24.AutoSize = true;
            this.label24.Location = new System.Drawing.Point(3, 33);
            this.label24.Name = "label24";
            this.label24.Size = new System.Drawing.Size(31, 18);
            this.label24.TabIndex = 3;
            this.label24.Text = "min";
            // 
            // label23
            // 
            this.label23.AutoSize = true;
            this.label23.Location = new System.Drawing.Point(102, 33);
            this.label23.Name = "label23";
            this.label23.Size = new System.Drawing.Size(37, 18);
            this.label23.TabIndex = 2;
            this.label23.Text = "max";
            // 
            // cbPmax
            // 
            this.cbPmax.FormattingEnabled = true;
            this.cbPmax.Location = new System.Drawing.Point(105, 54);
            this.cbPmax.Name = "cbPmax";
            this.cbPmax.Size = new System.Drawing.Size(85, 26);
            this.cbPmax.TabIndex = 1;
            this.cbPmax.TextChanged += new System.EventHandler(this.cbPmax_TextChanged);
            this.cbPmax.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.numFormatCheckerOn_KeyPress);
            // 
            // cbPmin
            // 
            this.cbPmin.FormattingEnabled = true;
            this.cbPmin.Location = new System.Drawing.Point(6, 54);
            this.cbPmin.Name = "cbPmin";
            this.cbPmin.Size = new System.Drawing.Size(85, 26);
            this.cbPmin.TabIndex = 0;
            this.cbPmin.TextChanged += new System.EventHandler(this.cbPmin_TextChanged);
            this.cbPmin.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.numFormatCheckerOn_KeyPress);
            // 
            // cbVoltageOfCoverTest
            // 
            this.cbVoltageOfCoverTest.FormattingEnabled = true;
            this.cbVoltageOfCoverTest.Location = new System.Drawing.Point(211, 167);
            this.cbVoltageOfCoverTest.Name = "cbVoltageOfCoverTest";
            this.cbVoltageOfCoverTest.Size = new System.Drawing.Size(148, 26);
            this.cbVoltageOfCoverTest.TabIndex = 35;
            this.cbVoltageOfCoverTest.TextChanged += new System.EventHandler(this.cbVoltageOfCoverTest_TextChanged);
            this.cbVoltageOfCoverTest.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.numFormatCheckerOn_KeyPress);
            // 
            // parameter_type_name_column
            // 
            this.parameter_type_name_column.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            this.parameter_type_name_column.DataPropertyName = "parameter_name";
            dataGridViewCellStyle5.BackColor = System.Drawing.Color.Transparent;
            this.parameter_type_name_column.DefaultCellStyle = dataGridViewCellStyle5;
            this.parameter_type_name_column.FillWeight = 91.57822F;
            this.parameter_type_name_column.HeaderText = "+";
            this.parameter_type_name_column.MinimumWidth = 40;
            this.parameter_type_name_column.Name = "parameter_type_name_column";
            this.parameter_type_name_column.ReadOnly = true;
            this.parameter_type_name_column.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            this.parameter_type_name_column.ToolTipText = "Добавить параметр";
            // 
            // parameter_type_id_column
            // 
            this.parameter_type_id_column.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            this.parameter_type_id_column.DataPropertyName = "parameter_type_id";
            this.parameter_type_id_column.HeaderText = "ID параметра";
            this.parameter_type_id_column.Name = "parameter_type_id_column";
            this.parameter_type_id_column.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            this.parameter_type_id_column.Visible = false;
            // 
            // parameterTypeDescriptionColumn
            // 
            this.parameterTypeDescriptionColumn.DataPropertyName = "parameter_description";
            this.parameterTypeDescriptionColumn.HeaderText = "Описание параметра";
            this.parameterTypeDescriptionColumn.Name = "parameterTypeDescriptionColumn";
            this.parameterTypeDescriptionColumn.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            this.parameterTypeDescriptionColumn.Visible = false;
            // 
            // parameterTypeMeasureColumn
            // 
            this.parameterTypeMeasureColumn.DataPropertyName = "parameter_measure";
            this.parameterTypeMeasureColumn.HeaderText = "Мера";
            this.parameterTypeMeasureColumn.MinimumWidth = 10;
            this.parameterTypeMeasureColumn.Name = "parameterTypeMeasureColumn";
            this.parameterTypeMeasureColumn.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            this.parameterTypeMeasureColumn.Visible = false;
            // 
            // minValueColumn
            // 
            this.minValueColumn.DataPropertyName = "min_value";
            this.minValueColumn.FillWeight = 80F;
            this.minValueColumn.HeaderText = "Min";
            this.minValueColumn.MinimumWidth = 75;
            this.minValueColumn.Name = "minValueColumn";
            this.minValueColumn.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            // 
            // maxValueColumn
            // 
            this.maxValueColumn.DataPropertyName = "max_value";
            this.maxValueColumn.FillWeight = 80F;
            this.maxValueColumn.HeaderText = "Max";
            this.maxValueColumn.MinimumWidth = 75;
            this.maxValueColumn.Name = "maxValueColumn";
            this.maxValueColumn.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            // 
            // percentColumn
            // 
            this.percentColumn.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.ColumnHeader;
            this.percentColumn.DataPropertyName = "percent";
            this.percentColumn.FillWeight = 83.9467F;
            this.percentColumn.HeaderText = "%";
            this.percentColumn.MinimumWidth = 50;
            this.percentColumn.Name = "percentColumn";
            this.percentColumn.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            this.percentColumn.Width = 50;
            // 
            // resultMeasureColumn
            // 
            this.resultMeasureColumn.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            this.resultMeasureColumn.DataPropertyName = "result_measure";
            this.resultMeasureColumn.FillWeight = 82.42039F;
            this.resultMeasureColumn.HeaderText = "Ед. изм.";
            this.resultMeasureColumn.MinimumWidth = 75;
            this.resultMeasureColumn.Name = "resultMeasureColumn";
            this.resultMeasureColumn.ReadOnly = true;
            this.resultMeasureColumn.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            // 
            // lengthBringingColumn
            // 
            this.lengthBringingColumn.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.AllCells;
            this.lengthBringingColumn.DataPropertyName = "length_bringing";
            this.lengthBringingColumn.FillWeight = 82.42039F;
            this.lengthBringingColumn.HeaderText = "Lприв, м";
            this.lengthBringingColumn.MinimumWidth = 52;
            this.lengthBringingColumn.Name = "lengthBringingColumn";
            this.lengthBringingColumn.ReadOnly = true;
            this.lengthBringingColumn.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            this.lengthBringingColumn.Width = 77;
            // 
            // frequencyMinColumn
            // 
            this.frequencyMinColumn.DataPropertyName = "frequency_min";
            this.frequencyMinColumn.FillWeight = 82.42039F;
            this.frequencyMinColumn.HeaderText = "fmin, кГц";
            this.frequencyMinColumn.Name = "frequencyMinColumn";
            this.frequencyMinColumn.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            // 
            // freqMaxColumn
            // 
            this.freqMaxColumn.DataPropertyName = "frequency_max";
            this.freqMaxColumn.FillWeight = 82.42039F;
            this.freqMaxColumn.HeaderText = "fmax, кГц";
            this.freqMaxColumn.Name = "freqMaxColumn";
            this.freqMaxColumn.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            // 
            // freqStepColumn
            // 
            this.freqStepColumn.DataPropertyName = "frequency_step";
            this.freqStepColumn.FillWeight = 82.42039F;
            this.freqStepColumn.HeaderText = "fшаг, кГц";
            this.freqStepColumn.Name = "freqStepColumn";
            this.freqStepColumn.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            // 
            // delButtonColumn
            // 
            this.delButtonColumn.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.AllCells;
            this.delButtonColumn.HeaderText = "x";
            this.delButtonColumn.MinimumWidth = 40;
            this.delButtonColumn.Name = "delButtonColumn";
            this.delButtonColumn.ReadOnly = true;
            this.delButtonColumn.Resizable = System.Windows.Forms.DataGridViewTriState.False;
            this.delButtonColumn.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            this.delButtonColumn.ToolTipText = "Удалить";
            this.delButtonColumn.Width = 40;
            // 
            // MeasuredParameterDataId_Column
            // 
            this.MeasuredParameterDataId_Column.DataPropertyName = "measured_parameter_data_id";
            this.MeasuredParameterDataId_Column.HeaderText = "ID параметров измерения";
            this.MeasuredParameterDataId_Column.Name = "MeasuredParameterDataId_Column";
            this.MeasuredParameterDataId_Column.Visible = false;
            // 
            // lengthBringingTypeIdColumn
            // 
            this.lengthBringingTypeIdColumn.DataPropertyName = "length_bringing_type_id";
            this.lengthBringingTypeIdColumn.HeaderText = "ID типа длины приведения";
            this.lengthBringingTypeIdColumn.Name = "lengthBringingTypeIdColumn";
            this.lengthBringingTypeIdColumn.Visible = false;
            // 
            // BringingLengthMeasureTitleColumn
            // 
            this.BringingLengthMeasureTitleColumn.HeaderText = "Мера длины приведения";
            this.BringingLengthMeasureTitleColumn.Name = "BringingLengthMeasureTitleColumn";
            this.BringingLengthMeasureTitleColumn.Visible = false;
            // 
            // cbRisolVoltageValue
            // 
            this.cbRisolVoltageValue.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbRisolVoltageValue.FormattingEnabled = true;
            this.cbRisolVoltageValue.Items.AddRange(new object[] {
            "10",
            "100",
            "500",
            "1000"});
            this.cbRisolVoltageValue.Location = new System.Drawing.Point(797, 37);
            this.cbRisolVoltageValue.Name = "cbRisolVoltageValue";
            this.cbRisolVoltageValue.Size = new System.Drawing.Size(163, 26);
            this.cbRisolVoltageValue.TabIndex = 25;
            this.cbRisolVoltageValue.SelectedIndexChanged += new System.EventHandler(this.cbRisolVoltageValue_SelectedIndexChanged);
            // 
            // label25
            // 
            this.label25.AutoSize = true;
            this.label25.Location = new System.Drawing.Point(794, 16);
            this.label25.Name = "label25";
            this.label25.Size = new System.Drawing.Size(158, 18);
            this.label25.TabIndex = 26;
            this.label25.Text = "Напряжение Rизол, В";
            // 
            // CableForm
            // 
            this.BackColor = System.Drawing.Color.White;
            this.ClientSize = new System.Drawing.Size(1034, 962);
            this.Controls.Add(this.cbVoltageOfCoverTest);
            this.Controls.Add(this.groupBox4);
            this.Controls.Add(this.tabControl1);
            this.Controls.Add(this.Notes_input);
            this.Controls.Add(this.panelControlButtons);
            this.Controls.Add(this.label13);
            this.Controls.Add(this.CodeKCH_input);
            this.Controls.Add(this.CodeOKP_input);
            this.Controls.Add(this.label12);
            this.Controls.Add(this.label11);
            this.Controls.Add(this.label7);
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
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.MaximizeBox = false;
            this.Name = "CableForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            ((System.ComponentModel.ISupportInitialize)(this.cableFormDataSet)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.linearMass_input)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.BuildLength_input)).EndInit();
            this.panelControlButtons.ResumeLayout(false);
            this.newTabPage.ResumeLayout(false);
            this.structureDataContainer.ResumeLayout(false);
            this.structureDataContainer.PerformLayout();
            this.panel1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dgMeasuredParameters)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.MeasuredParametersBindingSource)).EndInit();
            this.groupBox3.ResumeLayout(false);
            this.groupBox3.PerformLayout();
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.nudLeadToShieldVoltage)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.nudLeadToLeadVoltage)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.nudNumberInGroup)).EndInit();
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.nudRrealElementsAmount)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.nudDisplayedElementAmount)).EndInit();
            this.tabControl1.ResumeLayout(false);
            this.addMeasurerParameterContextMenu.ResumeLayout(false);
            this.contextMenuBringingLength.ResumeLayout(false);
            this.groupBox4.ResumeLayout(false);
            this.groupBox4.PerformLayout();
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
        protected System.Windows.Forms.Panel panelControlButtons;
        protected System.Windows.Forms.Button closeNoSaveButton;
        protected System.Windows.Forms.Button saveCableButton;
        private System.Windows.Forms.TabPage newTabPage;
        private System.Windows.Forms.Panel structureDataContainer;
        private System.Windows.Forms.TabControl tabControl1;
        private System.Windows.Forms.Button btnRemoveCurrentStructure;
        private System.Windows.Forms.ComboBox cbIsolationMaterial;
        private System.Windows.Forms.Label label15;
        private System.Windows.Forms.Label label14;
        private System.Windows.Forms.ComboBox cbLeadMaterial;
        private System.Windows.Forms.Label label10;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.DataGridView dgMeasuredParameters;
        private System.Windows.Forms.BindingSource MeasuredParametersBindingSource;
        private System.Windows.Forms.ContextMenuStrip addMeasurerParameterContextMenu;
        private System.Windows.Forms.ToolStripMenuItem AddAllToolStripMenuItem;
        private System.Windows.Forms.DataGridViewCellStyle DisabledCellStyle;
        private System.Windows.Forms.DataGridViewCellStyle EnabledCellStyle;
        private System.Windows.Forms.DataGridViewCellStyle EnabledBringingLengthCellStyle;
        private System.Windows.Forms.DataGridViewCellStyle DisabledBringingLengthCellStyle;
        private System.Windows.Forms.Label label22;
        private System.Windows.Forms.ContextMenuStrip contextMenuBringingLength;
        private System.Windows.Forms.ToolStripMenuItem toolStripMenuItem1;
        private System.Windows.Forms.ToolStripMenuItem toolStripMenuItem2;
        private System.Windows.Forms.CheckBox cbGroupCapacity;
        private System.Windows.Forms.GroupBox groupBox3;
        private System.Windows.Forms.ComboBox cbDRBringingFormula;
        private System.Windows.Forms.Label label21;
        private System.Windows.Forms.ComboBox cbDRCalculatingFormula;
        private System.Windows.Forms.Label label20;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.Label label19;
        private System.Windows.Forms.NumericUpDown nudLeadToShieldVoltage;
        private System.Windows.Forms.Label label18;
        private System.Windows.Forms.NumericUpDown nudLeadToLeadVoltage;
        private System.Windows.Forms.NumericUpDown nudNumberInGroup;
        private System.Windows.Forms.Label label17;
        private System.Windows.Forms.Label label16;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.NumericUpDown nudRrealElementsAmount;
        private System.Windows.Forms.NumericUpDown nudDisplayedElementAmount;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.Label structureType;
        private System.Windows.Forms.ComboBox cbStructureType;
        private System.Windows.Forms.GroupBox groupBox4;
        private System.Windows.Forms.Label label24;
        private System.Windows.Forms.Label label23;
        private System.Windows.Forms.ComboBox cbPmax;
        private System.Windows.Forms.ComboBox cbPmin;
        private System.Windows.Forms.ComboBox cbVoltageOfCoverTest;
        private System.Windows.Forms.ComboBox cbWaveResistance;
        private System.Windows.Forms.ComboBox cbLeadDiameters;
        private System.Windows.Forms.DataGridViewTextBoxColumn parameter_type_name_column;
        private System.Windows.Forms.DataGridViewTextBoxColumn parameter_type_id_column;
        private System.Windows.Forms.DataGridViewTextBoxColumn parameterTypeDescriptionColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn parameterTypeMeasureColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn minValueColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn maxValueColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn percentColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn resultMeasureColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn lengthBringingColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn frequencyMinColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn freqMaxColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn freqStepColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn delButtonColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn MeasuredParameterDataId_Column;
        private System.Windows.Forms.DataGridViewTextBoxColumn lengthBringingTypeIdColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn BringingLengthMeasureTitleColumn;
        private System.Windows.Forms.Label label25;
        private System.Windows.Forms.ComboBox cbRisolVoltageValue;
    }
}