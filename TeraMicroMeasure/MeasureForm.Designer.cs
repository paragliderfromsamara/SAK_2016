namespace TeraMicroMeasure
{
    partial class MeasureForm
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
            this.buttonPrevElement = new System.Windows.Forms.Button();
            this.buttonNextElement = new System.Windows.Forms.Button();
            this.buttonNextPoint = new System.Windows.Forms.Button();
            this.buttonPrevPoint = new System.Windows.Forms.Button();
            this.label3 = new System.Windows.Forms.Label();
            this.cableStructureCB = new System.Windows.Forms.ComboBox();
            this.neasureResultPanel = new System.Windows.Forms.Panel();
            this.measureResultDataGrid = new System.Windows.Forms.DataGridView();
            this.ElementNumber = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.SubElement_1 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.SubElement_2 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.SubElement_3 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.SubElement_4 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.IsMeasuredFlag = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.richTextBox1 = new System.Windows.Forms.RichTextBox();
            this.selectDevicePanel = new System.Windows.Forms.Panel();
            this.deviceControlButton = new System.Windows.Forms.Button();
            this.startMeasureButton = new System.Windows.Forms.Button();
            this.availableDevices = new System.Windows.Forms.ComboBox();
            this.panel1 = new System.Windows.Forms.Panel();
            this.comboBox3 = new System.Windows.Forms.ComboBox();
            this.label4 = new System.Windows.Forms.Label();
            this.temperatureComboBox = new System.Windows.Forms.Label();
            this.temperature = new System.Windows.Forms.ComboBox();
            this.panel2 = new System.Windows.Forms.Panel();
            this.resultField = new System.Windows.Forms.Label();
            this.deviceInfo = new System.Windows.Forms.Label();
            this.labelPointNumber = new System.Windows.Forms.Label();
            this.voltagesGroupBox = new System.Windows.Forms.GroupBox();
            this.v1000_RadioButton = new System.Windows.Forms.RadioButton();
            this.v500_RadioButton = new System.Windows.Forms.RadioButton();
            this.v100_RadioButton = new System.Windows.Forms.RadioButton();
            this.v10_RadioButton = new System.Windows.Forms.RadioButton();
            this.cableComboBox = new System.Windows.Forms.ComboBox();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.depolTimeLbl = new System.Windows.Forms.Label();
            this.afterMeasureDelayUpDown = new System.Windows.Forms.NumericUpDown();
            this.averagingCounter = new System.Windows.Forms.ComboBox();
            this.label5 = new System.Windows.Forms.Label();
            this.polarDelayLbl = new System.Windows.Forms.Label();
            this.beforeMeasureDelayUpDown = new System.Windows.Forms.NumericUpDown();
            this.label1 = new System.Windows.Forms.Label();
            this.measuredParametersGroupBox = new System.Windows.Forms.GroupBox();
            this.RizolRadioButton = new System.Windows.Forms.RadioButton();
            this.RleadRadioButton = new System.Windows.Forms.RadioButton();
            this.cableLengthNumericUpDown = new System.Windows.Forms.NumericUpDown();
            this.label2 = new System.Windows.Forms.Label();
            this.panelMeasurePointControl = new System.Windows.Forms.Panel();
            this.panel3 = new System.Windows.Forms.Panel();
            this.panel4 = new System.Windows.Forms.Panel();
            ((System.ComponentModel.ISupportInitialize)(this.measureResultDataGrid)).BeginInit();
            this.selectDevicePanel.SuspendLayout();
            this.panel1.SuspendLayout();
            this.panel2.SuspendLayout();
            this.voltagesGroupBox.SuspendLayout();
            this.groupBox2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.afterMeasureDelayUpDown)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.beforeMeasureDelayUpDown)).BeginInit();
            this.measuredParametersGroupBox.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.cableLengthNumericUpDown)).BeginInit();
            this.panelMeasurePointControl.SuspendLayout();
            this.panel3.SuspendLayout();
            this.panel4.SuspendLayout();
            this.SuspendLayout();
            // 
            // buttonPrevElement
            // 
            this.buttonPrevElement.Dock = System.Windows.Forms.DockStyle.Left;
            this.buttonPrevElement.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.buttonPrevElement.Location = new System.Drawing.Point(40, 0);
            this.buttonPrevElement.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.buttonPrevElement.Name = "buttonPrevElement";
            this.buttonPrevElement.Size = new System.Drawing.Size(44, 51);
            this.buttonPrevElement.TabIndex = 40;
            this.buttonPrevElement.Text = "<<";
            this.buttonPrevElement.UseVisualStyleBackColor = true;
            this.buttonPrevElement.Click += new System.EventHandler(this.buttonPrevElement_Click);
            // 
            // buttonNextElement
            // 
            this.buttonNextElement.Dock = System.Windows.Forms.DockStyle.Left;
            this.buttonNextElement.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.buttonNextElement.Location = new System.Drawing.Point(302, 0);
            this.buttonNextElement.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.buttonNextElement.Name = "buttonNextElement";
            this.buttonNextElement.Size = new System.Drawing.Size(44, 51);
            this.buttonNextElement.TabIndex = 39;
            this.buttonNextElement.Text = ">>";
            this.buttonNextElement.UseVisualStyleBackColor = true;
            this.buttonNextElement.Click += new System.EventHandler(this.buttonNextElement_Click);
            // 
            // buttonNextPoint
            // 
            this.buttonNextPoint.Dock = System.Windows.Forms.DockStyle.Left;
            this.buttonNextPoint.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.buttonNextPoint.Location = new System.Drawing.Point(262, 0);
            this.buttonNextPoint.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.buttonNextPoint.Name = "buttonNextPoint";
            this.buttonNextPoint.Size = new System.Drawing.Size(40, 51);
            this.buttonNextPoint.TabIndex = 38;
            this.buttonNextPoint.Text = ">";
            this.buttonNextPoint.UseVisualStyleBackColor = true;
            this.buttonNextPoint.Click += new System.EventHandler(this.buttonNextPoint_Click);
            // 
            // buttonPrevPoint
            // 
            this.buttonPrevPoint.Dock = System.Windows.Forms.DockStyle.Left;
            this.buttonPrevPoint.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.buttonPrevPoint.Location = new System.Drawing.Point(0, 0);
            this.buttonPrevPoint.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.buttonPrevPoint.Name = "buttonPrevPoint";
            this.buttonPrevPoint.Size = new System.Drawing.Size(40, 51);
            this.buttonPrevPoint.TabIndex = 37;
            this.buttonPrevPoint.Text = "<";
            this.buttonPrevPoint.UseVisualStyleBackColor = true;
            this.buttonPrevPoint.Click += new System.EventHandler(this.buttonPrevPoint_Click);
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(345, 13);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(85, 19);
            this.label3.TabIndex = 36;
            this.label3.Text = "Структура";
            // 
            // cableStructureCB
            // 
            this.cableStructureCB.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cableStructureCB.FormattingEnabled = true;
            this.cableStructureCB.Location = new System.Drawing.Point(348, 35);
            this.cableStructureCB.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.cableStructureCB.Name = "cableStructureCB";
            this.cableStructureCB.Size = new System.Drawing.Size(197, 27);
            this.cableStructureCB.TabIndex = 35;
            this.cableStructureCB.SelectedValueChanged += new System.EventHandler(this.cableStructureCB_SelectedIndexChanged);
            // 
            // neasureResultPanel
            // 
            this.neasureResultPanel.Location = new System.Drawing.Point(11, 380);
            this.neasureResultPanel.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.neasureResultPanel.Name = "neasureResultPanel";
            this.neasureResultPanel.Size = new System.Drawing.Size(774, 211);
            this.neasureResultPanel.TabIndex = 34;
            // 
            // measureResultDataGrid
            // 
            this.measureResultDataGrid.AllowUserToAddRows = false;
            this.measureResultDataGrid.AllowUserToDeleteRows = false;
            this.measureResultDataGrid.AllowUserToResizeColumns = false;
            this.measureResultDataGrid.AllowUserToResizeRows = false;
            this.measureResultDataGrid.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.measureResultDataGrid.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.ElementNumber,
            this.SubElement_1,
            this.SubElement_2,
            this.SubElement_3,
            this.SubElement_4,
            this.IsMeasuredFlag});
            this.measureResultDataGrid.Location = new System.Drawing.Point(18, 439);
            this.measureResultDataGrid.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.measureResultDataGrid.Name = "measureResultDataGrid";
            this.measureResultDataGrid.ReadOnly = true;
            this.measureResultDataGrid.RowHeadersVisible = false;
            this.measureResultDataGrid.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.measureResultDataGrid.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.CellSelect;
            this.measureResultDataGrid.Size = new System.Drawing.Size(774, 211);
            this.measureResultDataGrid.TabIndex = 0;
            this.measureResultDataGrid.KeyDown += new System.Windows.Forms.KeyEventHandler(this.dataGridView1_KeyDown);
            // 
            // ElementNumber
            // 
            this.ElementNumber.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            this.ElementNumber.DataPropertyName = "element_number";
            this.ElementNumber.HeaderText = "№ Элемента";
            this.ElementNumber.Name = "ElementNumber";
            this.ElementNumber.ReadOnly = true;
            // 
            // SubElement_1
            // 
            this.SubElement_1.DataPropertyName = "measure_1";
            this.SubElement_1.HeaderText = "Измерение 1";
            this.SubElement_1.Name = "SubElement_1";
            this.SubElement_1.ReadOnly = true;
            // 
            // SubElement_2
            // 
            this.SubElement_2.DataPropertyName = "measure_2";
            this.SubElement_2.HeaderText = "Измерение 2";
            this.SubElement_2.Name = "SubElement_2";
            this.SubElement_2.ReadOnly = true;
            // 
            // SubElement_3
            // 
            this.SubElement_3.DataPropertyName = "measure_3";
            this.SubElement_3.HeaderText = "Измерение 3";
            this.SubElement_3.Name = "SubElement_3";
            this.SubElement_3.ReadOnly = true;
            // 
            // SubElement_4
            // 
            this.SubElement_4.DataPropertyName = "measure_4";
            this.SubElement_4.HeaderText = "Измерение 4";
            this.SubElement_4.Name = "SubElement_4";
            this.SubElement_4.ReadOnly = true;
            // 
            // IsMeasuredFlag
            // 
            this.IsMeasuredFlag.HeaderText = "Column1";
            this.IsMeasuredFlag.Name = "IsMeasuredFlag";
            this.IsMeasuredFlag.ReadOnly = true;
            this.IsMeasuredFlag.Visible = false;
            // 
            // richTextBox1
            // 
            this.richTextBox1.Location = new System.Drawing.Point(31, 773);
            this.richTextBox1.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.richTextBox1.Name = "richTextBox1";
            this.richTextBox1.Size = new System.Drawing.Size(386, 125);
            this.richTextBox1.TabIndex = 30;
            this.richTextBox1.Text = "";
            // 
            // selectDevicePanel
            // 
            this.selectDevicePanel.Controls.Add(this.availableDevices);
            this.selectDevicePanel.Controls.Add(this.deviceControlButton);
            this.selectDevicePanel.Location = new System.Drawing.Point(159, 284);
            this.selectDevicePanel.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.selectDevicePanel.Name = "selectDevicePanel";
            this.selectDevicePanel.Size = new System.Drawing.Size(547, 48);
            this.selectDevicePanel.TabIndex = 33;
            // 
            // deviceControlButton
            // 
            this.deviceControlButton.Dock = System.Windows.Forms.DockStyle.Right;
            this.deviceControlButton.Location = new System.Drawing.Point(369, 0);
            this.deviceControlButton.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.deviceControlButton.Name = "deviceControlButton";
            this.deviceControlButton.Size = new System.Drawing.Size(178, 48);
            this.deviceControlButton.TabIndex = 13;
            this.deviceControlButton.Text = "Подключить";
            this.deviceControlButton.UseVisualStyleBackColor = true;
            // 
            // startMeasureButton
            // 
            this.startMeasureButton.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(65)))), ((int)(((byte)(124)))), ((int)(((byte)(224)))));
            this.startMeasureButton.Cursor = System.Windows.Forms.Cursors.Hand;
            this.startMeasureButton.Dock = System.Windows.Forms.DockStyle.Left;
            this.startMeasureButton.FlatAppearance.BorderSize = 0;
            this.startMeasureButton.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.startMeasureButton.ForeColor = System.Drawing.SystemColors.ControlLightLight;
            this.startMeasureButton.Location = new System.Drawing.Point(84, 0);
            this.startMeasureButton.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.startMeasureButton.Name = "startMeasureButton";
            this.startMeasureButton.Size = new System.Drawing.Size(178, 51);
            this.startMeasureButton.TabIndex = 11;
            this.startMeasureButton.Text = "Пуск измерения";
            this.startMeasureButton.UseVisualStyleBackColor = false;
            // 
            // availableDevices
            // 
            this.availableDevices.BackColor = System.Drawing.SystemColors.InactiveCaption;
            this.availableDevices.Dock = System.Windows.Forms.DockStyle.Fill;
            this.availableDevices.DrawMode = System.Windows.Forms.DrawMode.OwnerDrawVariable;
            this.availableDevices.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.availableDevices.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.availableDevices.Font = new System.Drawing.Font("Tahoma", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.availableDevices.ItemHeight = 40;
            this.availableDevices.Location = new System.Drawing.Point(0, 0);
            this.availableDevices.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.availableDevices.Name = "availableDevices";
            this.availableDevices.Size = new System.Drawing.Size(369, 46);
            this.availableDevices.TabIndex = 12;
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.comboBox3);
            this.panel1.Controls.Add(this.label4);
            this.panel1.Location = new System.Drawing.Point(382, 717);
            this.panel1.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(206, 51);
            this.panel1.TabIndex = 31;
            // 
            // comboBox3
            // 
            this.comboBox3.Font = new System.Drawing.Font("Tahoma", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.comboBox3.FormattingEnabled = true;
            this.comboBox3.Location = new System.Drawing.Point(0, 24);
            this.comboBox3.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.comboBox3.Name = "comboBox3";
            this.comboBox3.Size = new System.Drawing.Size(197, 27);
            this.comboBox3.TabIndex = 8;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Font = new System.Drawing.Font("Tahoma", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.label4.Location = new System.Drawing.Point(-4, 2);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(80, 19);
            this.label4.TabIndex = 9;
            this.label4.Text = "Материал";
            // 
            // temperatureComboBox
            // 
            this.temperatureComboBox.AutoSize = true;
            this.temperatureComboBox.Font = new System.Drawing.Font("Tahoma", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.temperatureComboBox.Location = new System.Drawing.Point(658, 15);
            this.temperatureComboBox.Name = "temperatureComboBox";
            this.temperatureComboBox.Size = new System.Drawing.Size(104, 19);
            this.temperatureComboBox.TabIndex = 3;
            this.temperatureComboBox.Text = "Температура";
            // 
            // temperature
            // 
            this.temperature.Font = new System.Drawing.Font("Tahoma", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.temperature.FormattingEnabled = true;
            this.temperature.Location = new System.Drawing.Point(661, 36);
            this.temperature.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.temperature.Name = "temperature";
            this.temperature.Size = new System.Drawing.Size(124, 27);
            this.temperature.TabIndex = 7;
            // 
            // panel2
            // 
            this.panel2.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(211)))), ((int)(((byte)(237)))), ((int)(((byte)(250)))));
            this.panel2.Controls.Add(this.resultField);
            this.panel2.Controls.Add(this.deviceInfo);
            this.panel2.Controls.Add(this.labelPointNumber);
            this.panel2.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel2.Location = new System.Drawing.Point(0, 0);
            this.panel2.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(846, 149);
            this.panel2.TabIndex = 32;
            // 
            // resultField
            // 
            this.resultField.Font = new System.Drawing.Font("Tahoma", 27.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.resultField.Location = new System.Drawing.Point(25, 59);
            this.resultField.Name = "resultField";
            this.resultField.Size = new System.Drawing.Size(589, 48);
            this.resultField.TabIndex = 1;
            this.resultField.Text = "106.56 ТОм⋅м";
            this.resultField.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // deviceInfo
            // 
            this.deviceInfo.AutoSize = true;
            this.deviceInfo.Location = new System.Drawing.Point(16, 8);
            this.deviceInfo.Name = "deviceInfo";
            this.deviceInfo.Size = new System.Drawing.Size(236, 19);
            this.deviceInfo.TabIndex = 0;
            this.deviceInfo.Text = "Тераомметр ТОмМ-01 2021-01";
            // 
            // labelPointNumber
            // 
            this.labelPointNumber.AutoSize = true;
            this.labelPointNumber.Location = new System.Drawing.Point(14, 120);
            this.labelPointNumber.Name = "labelPointNumber";
            this.labelPointNumber.Size = new System.Drawing.Size(18, 19);
            this.labelPointNumber.TabIndex = 20;
            this.labelPointNumber.Text = "0";
            // 
            // voltagesGroupBox
            // 
            this.voltagesGroupBox.Controls.Add(this.v1000_RadioButton);
            this.voltagesGroupBox.Controls.Add(this.v500_RadioButton);
            this.voltagesGroupBox.Controls.Add(this.v100_RadioButton);
            this.voltagesGroupBox.Controls.Add(this.v10_RadioButton);
            this.voltagesGroupBox.Font = new System.Drawing.Font("Tahoma", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.voltagesGroupBox.Location = new System.Drawing.Point(33, 697);
            this.voltagesGroupBox.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.voltagesGroupBox.Name = "voltagesGroupBox";
            this.voltagesGroupBox.Padding = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.voltagesGroupBox.Size = new System.Drawing.Size(330, 71);
            this.voltagesGroupBox.TabIndex = 29;
            this.voltagesGroupBox.TabStop = false;
            this.voltagesGroupBox.Text = "Измерительное напряжение";
            // 
            // v1000_RadioButton
            // 
            this.v1000_RadioButton.AutoSize = true;
            this.v1000_RadioButton.Location = new System.Drawing.Point(246, 31);
            this.v1000_RadioButton.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.v1000_RadioButton.Name = "v1000_RadioButton";
            this.v1000_RadioButton.Size = new System.Drawing.Size(72, 22);
            this.v1000_RadioButton.TabIndex = 3;
            this.v1000_RadioButton.Text = "1000 В";
            this.v1000_RadioButton.UseVisualStyleBackColor = true;
            // 
            // v500_RadioButton
            // 
            this.v500_RadioButton.AutoSize = true;
            this.v500_RadioButton.Location = new System.Drawing.Point(167, 31);
            this.v500_RadioButton.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.v500_RadioButton.Name = "v500_RadioButton";
            this.v500_RadioButton.Size = new System.Drawing.Size(64, 22);
            this.v500_RadioButton.TabIndex = 2;
            this.v500_RadioButton.Text = "500 В";
            this.v500_RadioButton.UseVisualStyleBackColor = true;
            // 
            // v100_RadioButton
            // 
            this.v100_RadioButton.AutoSize = true;
            this.v100_RadioButton.Location = new System.Drawing.Point(90, 31);
            this.v100_RadioButton.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.v100_RadioButton.Name = "v100_RadioButton";
            this.v100_RadioButton.Size = new System.Drawing.Size(64, 22);
            this.v100_RadioButton.TabIndex = 1;
            this.v100_RadioButton.Text = "100 В";
            this.v100_RadioButton.UseVisualStyleBackColor = true;
            // 
            // v10_RadioButton
            // 
            this.v10_RadioButton.AutoSize = true;
            this.v10_RadioButton.Checked = true;
            this.v10_RadioButton.Location = new System.Drawing.Point(21, 31);
            this.v10_RadioButton.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.v10_RadioButton.Name = "v10_RadioButton";
            this.v10_RadioButton.Size = new System.Drawing.Size(56, 22);
            this.v10_RadioButton.TabIndex = 0;
            this.v10_RadioButton.TabStop = true;
            this.v10_RadioButton.Text = "10 В";
            this.v10_RadioButton.UseVisualStyleBackColor = true;
            // 
            // cableComboBox
            // 
            this.cableComboBox.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cableComboBox.FormattingEnabled = true;
            this.cableComboBox.Items.AddRange(new object[] {
            "Список пуст"});
            this.cableComboBox.Location = new System.Drawing.Point(12, 35);
            this.cableComboBox.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.cableComboBox.Name = "cableComboBox";
            this.cableComboBox.Size = new System.Drawing.Size(331, 27);
            this.cableComboBox.TabIndex = 23;
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.depolTimeLbl);
            this.groupBox2.Controls.Add(this.afterMeasureDelayUpDown);
            this.groupBox2.Controls.Add(this.averagingCounter);
            this.groupBox2.Controls.Add(this.label5);
            this.groupBox2.Controls.Add(this.polarDelayLbl);
            this.groupBox2.Controls.Add(this.beforeMeasureDelayUpDown);
            this.groupBox2.Font = new System.Drawing.Font("Tahoma", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.groupBox2.Location = new System.Drawing.Point(324, 72);
            this.groupBox2.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Padding = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.groupBox2.Size = new System.Drawing.Size(461, 96);
            this.groupBox2.TabIndex = 28;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "Настройки измерителя";
            // 
            // depolTimeLbl
            // 
            this.depolTimeLbl.AutoSize = true;
            this.depolTimeLbl.Location = new System.Drawing.Point(343, 30);
            this.depolTimeLbl.Name = "depolTimeLbl";
            this.depolTimeLbl.Size = new System.Drawing.Size(68, 19);
            this.depolTimeLbl.TabIndex = 8;
            this.depolTimeLbl.Text = "Пауза, c";
            // 
            // afterMeasureDelayUpDown
            // 
            this.afterMeasureDelayUpDown.Location = new System.Drawing.Point(346, 51);
            this.afterMeasureDelayUpDown.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.afterMeasureDelayUpDown.Maximum = new decimal(new int[] {
            300,
            0,
            0,
            0});
            this.afterMeasureDelayUpDown.Name = "afterMeasureDelayUpDown";
            this.afterMeasureDelayUpDown.Size = new System.Drawing.Size(103, 27);
            this.afterMeasureDelayUpDown.TabIndex = 7;
            // 
            // averagingCounter
            // 
            this.averagingCounter.FormattingEnabled = true;
            this.averagingCounter.Location = new System.Drawing.Point(182, 50);
            this.averagingCounter.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.averagingCounter.Name = "averagingCounter";
            this.averagingCounter.Size = new System.Drawing.Size(104, 27);
            this.averagingCounter.TabIndex = 6;
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(179, 28);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(130, 19);
            this.label5.TabIndex = 5;
            this.label5.Text = "Усреднение, изм";
            // 
            // polarDelayLbl
            // 
            this.polarDelayLbl.AutoSize = true;
            this.polarDelayLbl.Location = new System.Drawing.Point(17, 29);
            this.polarDelayLbl.Name = "polarDelayLbl";
            this.polarDelayLbl.Size = new System.Drawing.Size(110, 19);
            this.polarDelayLbl.TabIndex = 1;
            this.polarDelayLbl.Text = "Выдержка, мс";
            // 
            // beforeMeasureDelayUpDown
            // 
            this.beforeMeasureDelayUpDown.Location = new System.Drawing.Point(20, 50);
            this.beforeMeasureDelayUpDown.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.beforeMeasureDelayUpDown.Maximum = new decimal(new int[] {
            10000,
            0,
            0,
            0});
            this.beforeMeasureDelayUpDown.Name = "beforeMeasureDelayUpDown";
            this.beforeMeasureDelayUpDown.Size = new System.Drawing.Size(103, 27);
            this.beforeMeasureDelayUpDown.TabIndex = 0;
            this.beforeMeasureDelayUpDown.Value = new decimal(new int[] {
            200,
            0,
            0,
            0});
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(8, 12);
            this.label1.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(61, 19);
            this.label1.TabIndex = 24;
            this.label1.Text = "Кабель";
            // 
            // measuredParametersGroupBox
            // 
            this.measuredParametersGroupBox.Controls.Add(this.RizolRadioButton);
            this.measuredParametersGroupBox.Controls.Add(this.RleadRadioButton);
            this.measuredParametersGroupBox.Location = new System.Drawing.Point(11, 72);
            this.measuredParametersGroupBox.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.measuredParametersGroupBox.Name = "measuredParametersGroupBox";
            this.measuredParametersGroupBox.Padding = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.measuredParametersGroupBox.Size = new System.Drawing.Size(261, 96);
            this.measuredParametersGroupBox.TabIndex = 27;
            this.measuredParametersGroupBox.TabStop = false;
            this.measuredParametersGroupBox.Text = "Измеряемый параметр";
            this.measuredParametersGroupBox.UseCompatibleTextRendering = true;
            // 
            // RizolRadioButton
            // 
            this.RizolRadioButton.AutoSize = true;
            this.RizolRadioButton.Location = new System.Drawing.Point(175, 49);
            this.RizolRadioButton.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.RizolRadioButton.Name = "RizolRadioButton";
            this.RizolRadioButton.Size = new System.Drawing.Size(71, 23);
            this.RizolRadioButton.TabIndex = 1;
            this.RizolRadioButton.Text = "Rизол";
            this.RizolRadioButton.UseVisualStyleBackColor = true;
            // 
            // RleadRadioButton
            // 
            this.RleadRadioButton.AutoSize = true;
            this.RleadRadioButton.Checked = true;
            this.RleadRadioButton.Location = new System.Drawing.Point(32, 49);
            this.RleadRadioButton.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.RleadRadioButton.Name = "RleadRadioButton";
            this.RleadRadioButton.Size = new System.Drawing.Size(67, 23);
            this.RleadRadioButton.TabIndex = 0;
            this.RleadRadioButton.TabStop = true;
            this.RleadRadioButton.Text = "Rжил";
            this.RleadRadioButton.UseVisualStyleBackColor = true;
            // 
            // cableLengthNumericUpDown
            // 
            this.cableLengthNumericUpDown.Location = new System.Drawing.Point(550, 35);
            this.cableLengthNumericUpDown.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.cableLengthNumericUpDown.Maximum = new decimal(new int[] {
            10000,
            0,
            0,
            0});
            this.cableLengthNumericUpDown.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.cableLengthNumericUpDown.Name = "cableLengthNumericUpDown";
            this.cableLengthNumericUpDown.Size = new System.Drawing.Size(105, 27);
            this.cableLengthNumericUpDown.TabIndex = 25;
            this.cableLengthNumericUpDown.Value = new decimal(new int[] {
            1000,
            0,
            0,
            0});
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(545, 13);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(75, 19);
            this.label2.TabIndex = 26;
            this.label2.Text = "Длина, м";
            // 
            // panelMeasurePointControl
            // 
            this.panelMeasurePointControl.Controls.Add(this.buttonNextElement);
            this.panelMeasurePointControl.Controls.Add(this.buttonNextPoint);
            this.panelMeasurePointControl.Controls.Add(this.startMeasureButton);
            this.panelMeasurePointControl.Controls.Add(this.buttonPrevElement);
            this.panelMeasurePointControl.Controls.Add(this.buttonPrevPoint);
            this.panelMeasurePointControl.Dock = System.Windows.Forms.DockStyle.Left;
            this.panelMeasurePointControl.Location = new System.Drawing.Point(0, 0);
            this.panelMeasurePointControl.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.panelMeasurePointControl.Name = "panelMeasurePointControl";
            this.panelMeasurePointControl.Size = new System.Drawing.Size(351, 51);
            this.panelMeasurePointControl.TabIndex = 41;
            // 
            // panel3
            // 
            this.panel3.Controls.Add(this.measureResultDataGrid);
            this.panel3.Controls.Add(this.panel4);
            this.panel3.Controls.Add(this.panel2);
            this.panel3.Location = new System.Drawing.Point(895, 207);
            this.panel3.Name = "panel3";
            this.panel3.Size = new System.Drawing.Size(846, 652);
            this.panel3.TabIndex = 42;
            // 
            // panel4
            // 
            this.panel4.BackColor = System.Drawing.Color.Cyan;
            this.panel4.Controls.Add(this.panelMeasurePointControl);
            this.panel4.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel4.Location = new System.Drawing.Point(0, 149);
            this.panel4.Name = "panel4";
            this.panel4.Size = new System.Drawing.Size(846, 51);
            this.panel4.TabIndex = 43;
            // 
            // MeasureForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(9F, 19F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.White;
            this.ClientSize = new System.Drawing.Size(1786, 899);
            this.Controls.Add(this.panel3);
            this.Controls.Add(this.temperatureComboBox);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.cableStructureCB);
            this.Controls.Add(this.temperature);
            this.Controls.Add(this.neasureResultPanel);
            this.Controls.Add(this.richTextBox1);
            this.Controls.Add(this.selectDevicePanel);
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.voltagesGroupBox);
            this.Controls.Add(this.cableComboBox);
            this.Controls.Add(this.groupBox2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.measuredParametersGroupBox);
            this.Controls.Add(this.cableLengthNumericUpDown);
            this.Controls.Add(this.label2);
            this.Font = new System.Drawing.Font("Tahoma", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.Name = "MeasureForm";
            this.Text = "Измерение";
            this.KeyDown += new System.Windows.Forms.KeyEventHandler(this.dataGridView1_KeyDown);
            ((System.ComponentModel.ISupportInitialize)(this.measureResultDataGrid)).EndInit();
            this.selectDevicePanel.ResumeLayout(false);
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.panel2.ResumeLayout(false);
            this.panel2.PerformLayout();
            this.voltagesGroupBox.ResumeLayout(false);
            this.voltagesGroupBox.PerformLayout();
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.afterMeasureDelayUpDown)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.beforeMeasureDelayUpDown)).EndInit();
            this.measuredParametersGroupBox.ResumeLayout(false);
            this.measuredParametersGroupBox.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.cableLengthNumericUpDown)).EndInit();
            this.panelMeasurePointControl.ResumeLayout(false);
            this.panel3.ResumeLayout(false);
            this.panel4.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button buttonPrevElement;
        private System.Windows.Forms.Button buttonNextElement;
        private System.Windows.Forms.Button buttonNextPoint;
        private System.Windows.Forms.Button buttonPrevPoint;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.ComboBox cableStructureCB;
        private System.Windows.Forms.Panel neasureResultPanel;
        private System.Windows.Forms.DataGridView measureResultDataGrid;
        private System.Windows.Forms.DataGridViewTextBoxColumn ElementNumber;
        private System.Windows.Forms.DataGridViewTextBoxColumn SubElement_1;
        private System.Windows.Forms.DataGridViewTextBoxColumn SubElement_2;
        private System.Windows.Forms.DataGridViewTextBoxColumn SubElement_3;
        private System.Windows.Forms.DataGridViewTextBoxColumn SubElement_4;
        private System.Windows.Forms.DataGridViewTextBoxColumn IsMeasuredFlag;
        private System.Windows.Forms.RichTextBox richTextBox1;
        private System.Windows.Forms.Panel selectDevicePanel;
        private System.Windows.Forms.Button deviceControlButton;
        private System.Windows.Forms.Button startMeasureButton;
        private System.Windows.Forms.ComboBox availableDevices;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.ComboBox comboBox3;
        private System.Windows.Forms.Label temperatureComboBox;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.ComboBox temperature;
        private System.Windows.Forms.Panel panel2;
        private System.Windows.Forms.Label resultField;
        private System.Windows.Forms.Label deviceInfo;
        private System.Windows.Forms.Label labelPointNumber;
        private System.Windows.Forms.GroupBox voltagesGroupBox;
        private System.Windows.Forms.RadioButton v1000_RadioButton;
        private System.Windows.Forms.RadioButton v500_RadioButton;
        private System.Windows.Forms.RadioButton v100_RadioButton;
        private System.Windows.Forms.RadioButton v10_RadioButton;
        private System.Windows.Forms.ComboBox cableComboBox;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.Label depolTimeLbl;
        private System.Windows.Forms.NumericUpDown afterMeasureDelayUpDown;
        private System.Windows.Forms.ComboBox averagingCounter;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label polarDelayLbl;
        private System.Windows.Forms.NumericUpDown beforeMeasureDelayUpDown;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.GroupBox measuredParametersGroupBox;
        private System.Windows.Forms.RadioButton RizolRadioButton;
        private System.Windows.Forms.RadioButton RleadRadioButton;
        private System.Windows.Forms.NumericUpDown cableLengthNumericUpDown;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Panel panelMeasurePointControl;
        private System.Windows.Forms.Panel panel3;
        private System.Windows.Forms.Panel panel4;
    }
}