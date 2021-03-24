﻿namespace TeraMicroMeasure
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
            this.cableComboBox = new System.Windows.Forms.ComboBox();
            this.label1 = new System.Windows.Forms.Label();
            this.cableLengthNumericUpDown = new System.Windows.Forms.NumericUpDown();
            this.label2 = new System.Windows.Forms.Label();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.depolTimeLbl = new System.Windows.Forms.Label();
            this.afterMeasureDelayUpDown = new System.Windows.Forms.NumericUpDown();
            this.averagingCounter = new System.Windows.Forms.ComboBox();
            this.label5 = new System.Windows.Forms.Label();
            this.polarDelayLbl = new System.Windows.Forms.Label();
            this.beforeMeasureDelayUpDown = new System.Windows.Forms.NumericUpDown();
            this.measurePanel = new System.Windows.Forms.Panel();
            this.buttonPrevElement = new System.Windows.Forms.Button();
            this.buttonNextElement = new System.Windows.Forms.Button();
            this.labelPointNumber = new System.Windows.Forms.Label();
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
            this.temperatureComboBox = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.temperature = new System.Windows.Forms.ComboBox();
            this.panel2 = new System.Windows.Forms.Panel();
            this.resultField = new System.Windows.Forms.Label();
            this.deviceInfo = new System.Windows.Forms.Label();
            this.voltagesGroupBox = new System.Windows.Forms.GroupBox();
            this.v1000_RadioButton = new System.Windows.Forms.RadioButton();
            this.v500_RadioButton = new System.Windows.Forms.RadioButton();
            this.v100_RadioButton = new System.Windows.Forms.RadioButton();
            this.v10_RadioButton = new System.Windows.Forms.RadioButton();
            this.measuredParametersGroupBox = new System.Windows.Forms.GroupBox();
            this.RizolRadioButton = new System.Windows.Forms.RadioButton();
            this.RleadRadioButton = new System.Windows.Forms.RadioButton();
            ((System.ComponentModel.ISupportInitialize)(this.cableLengthNumericUpDown)).BeginInit();
            this.groupBox2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.afterMeasureDelayUpDown)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.beforeMeasureDelayUpDown)).BeginInit();
            this.measurePanel.SuspendLayout();
            this.neasureResultPanel.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.measureResultDataGrid)).BeginInit();
            this.selectDevicePanel.SuspendLayout();
            this.panel1.SuspendLayout();
            this.panel2.SuspendLayout();
            this.voltagesGroupBox.SuspendLayout();
            this.measuredParametersGroupBox.SuspendLayout();
            this.SuspendLayout();
            // 
            // cableComboBox
            // 
            this.cableComboBox.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cableComboBox.FormattingEnabled = true;
            this.cableComboBox.Items.AddRange(new object[] {
            "Список пуст"});
            this.cableComboBox.Location = new System.Drawing.Point(17, 62);
            this.cableComboBox.Margin = new System.Windows.Forms.Padding(4);
            this.cableComboBox.Name = "cableComboBox";
            this.cableComboBox.Size = new System.Drawing.Size(367, 31);
            this.cableComboBox.TabIndex = 0;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(13, 34);
            this.label1.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(72, 23);
            this.label1.TabIndex = 1;
            this.label1.Text = "Кабель";
            // 
            // cableLengthNumericUpDown
            // 
            this.cableLengthNumericUpDown.Location = new System.Drawing.Point(247, 250);
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
            this.cableLengthNumericUpDown.Size = new System.Drawing.Size(136, 30);
            this.cableLengthNumericUpDown.TabIndex = 2;
            this.cableLengthNumericUpDown.Value = new decimal(new int[] {
            1000,
            0,
            0,
            0});
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(242, 224);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(90, 23);
            this.label2.TabIndex = 3;
            this.label2.Text = "Длина, м";
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.depolTimeLbl);
            this.groupBox2.Controls.Add(this.afterMeasureDelayUpDown);
            this.groupBox2.Controls.Add(this.averagingCounter);
            this.groupBox2.Controls.Add(this.label5);
            this.groupBox2.Controls.Add(this.polarDelayLbl);
            this.groupBox2.Controls.Add(this.beforeMeasureDelayUpDown);
            this.groupBox2.Font = new System.Drawing.Font("Tahoma", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.groupBox2.Location = new System.Drawing.Point(405, 263);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(512, 116);
            this.groupBox2.TabIndex = 5;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "Настройки измерителя";
            // 
            // depolTimeLbl
            // 
            this.depolTimeLbl.AutoSize = true;
            this.depolTimeLbl.Location = new System.Drawing.Point(381, 36);
            this.depolTimeLbl.Name = "depolTimeLbl";
            this.depolTimeLbl.Size = new System.Drawing.Size(66, 18);
            this.depolTimeLbl.TabIndex = 8;
            this.depolTimeLbl.Text = "Пауза, c";
            // 
            // afterMeasureDelayUpDown
            // 
            this.afterMeasureDelayUpDown.Location = new System.Drawing.Point(384, 62);
            this.afterMeasureDelayUpDown.Maximum = new decimal(new int[] {
            300,
            0,
            0,
            0});
            this.afterMeasureDelayUpDown.Name = "afterMeasureDelayUpDown";
            this.afterMeasureDelayUpDown.Size = new System.Drawing.Size(115, 26);
            this.afterMeasureDelayUpDown.TabIndex = 7;
            // 
            // averagingCounter
            // 
            this.averagingCounter.FormattingEnabled = true;
            this.averagingCounter.Location = new System.Drawing.Point(202, 60);
            this.averagingCounter.Name = "averagingCounter";
            this.averagingCounter.Size = new System.Drawing.Size(115, 26);
            this.averagingCounter.TabIndex = 6;
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(199, 34);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(123, 18);
            this.label5.TabIndex = 5;
            this.label5.Text = "Усреднение, изм";
            // 
            // polarDelayLbl
            // 
            this.polarDelayLbl.AutoSize = true;
            this.polarDelayLbl.Location = new System.Drawing.Point(19, 35);
            this.polarDelayLbl.Name = "polarDelayLbl";
            this.polarDelayLbl.Size = new System.Drawing.Size(105, 18);
            this.polarDelayLbl.TabIndex = 1;
            this.polarDelayLbl.Text = "Выдержка, мс";
            // 
            // beforeMeasureDelayUpDown
            // 
            this.beforeMeasureDelayUpDown.Location = new System.Drawing.Point(22, 61);
            this.beforeMeasureDelayUpDown.Maximum = new decimal(new int[] {
            10000,
            0,
            0,
            0});
            this.beforeMeasureDelayUpDown.Name = "beforeMeasureDelayUpDown";
            this.beforeMeasureDelayUpDown.Size = new System.Drawing.Size(115, 26);
            this.beforeMeasureDelayUpDown.TabIndex = 0;
            this.beforeMeasureDelayUpDown.Value = new decimal(new int[] {
            200,
            0,
            0,
            0});
            // 
            // measurePanel
            // 
            this.measurePanel.BackColor = System.Drawing.SystemColors.ControlLightLight;
            this.measurePanel.Controls.Add(this.buttonPrevElement);
            this.measurePanel.Controls.Add(this.buttonNextElement);
            this.measurePanel.Controls.Add(this.labelPointNumber);
            this.measurePanel.Controls.Add(this.buttonNextPoint);
            this.measurePanel.Controls.Add(this.buttonPrevPoint);
            this.measurePanel.Controls.Add(this.label3);
            this.measurePanel.Controls.Add(this.cableStructureCB);
            this.measurePanel.Controls.Add(this.neasureResultPanel);
            this.measurePanel.Controls.Add(this.richTextBox1);
            this.measurePanel.Controls.Add(this.selectDevicePanel);
            this.measurePanel.Controls.Add(this.panel1);
            this.measurePanel.Controls.Add(this.panel2);
            this.measurePanel.Controls.Add(this.voltagesGroupBox);
            this.measurePanel.Controls.Add(this.cableComboBox);
            this.measurePanel.Controls.Add(this.groupBox2);
            this.measurePanel.Controls.Add(this.label1);
            this.measurePanel.Controls.Add(this.measuredParametersGroupBox);
            this.measurePanel.Controls.Add(this.cableLengthNumericUpDown);
            this.measurePanel.Controls.Add(this.label2);
            this.measurePanel.Dock = System.Windows.Forms.DockStyle.Fill;
            this.measurePanel.Location = new System.Drawing.Point(0, 0);
            this.measurePanel.Name = "measurePanel";
            this.measurePanel.Size = new System.Drawing.Size(1108, 861);
            this.measurePanel.TabIndex = 6;
            // 
            // buttonPrevElement
            // 
            this.buttonPrevElement.Location = new System.Drawing.Point(17, 351);
            this.buttonPrevElement.Name = "buttonPrevElement";
            this.buttonPrevElement.Size = new System.Drawing.Size(49, 49);
            this.buttonPrevElement.TabIndex = 22;
            this.buttonPrevElement.Text = "<<";
            this.buttonPrevElement.UseVisualStyleBackColor = true;
            this.buttonPrevElement.Click += new System.EventHandler(this.buttonPrevElement_Click);
            // 
            // buttonNextElement
            // 
            this.buttonNextElement.Location = new System.Drawing.Point(279, 351);
            this.buttonNextElement.Name = "buttonNextElement";
            this.buttonNextElement.Size = new System.Drawing.Size(49, 49);
            this.buttonNextElement.TabIndex = 21;
            this.buttonNextElement.Text = ">>";
            this.buttonNextElement.UseVisualStyleBackColor = true;
            this.buttonNextElement.Click += new System.EventHandler(this.buttonNextElement_Click);
            // 
            // labelPointNumber
            // 
            this.labelPointNumber.AutoSize = true;
            this.labelPointNumber.Location = new System.Drawing.Point(165, 364);
            this.labelPointNumber.Name = "labelPointNumber";
            this.labelPointNumber.Size = new System.Drawing.Size(20, 23);
            this.labelPointNumber.TabIndex = 20;
            this.labelPointNumber.Text = "0";
            // 
            // buttonNextPoint
            // 
            this.buttonNextPoint.Location = new System.Drawing.Point(228, 351);
            this.buttonNextPoint.Name = "buttonNextPoint";
            this.buttonNextPoint.Size = new System.Drawing.Size(45, 49);
            this.buttonNextPoint.TabIndex = 19;
            this.buttonNextPoint.Text = ">";
            this.buttonNextPoint.UseVisualStyleBackColor = true;
            this.buttonNextPoint.Click += new System.EventHandler(this.buttonNextPoint_Click);
            // 
            // buttonPrevPoint
            // 
            this.buttonPrevPoint.Location = new System.Drawing.Point(72, 351);
            this.buttonPrevPoint.Name = "buttonPrevPoint";
            this.buttonPrevPoint.Size = new System.Drawing.Size(45, 49);
            this.buttonPrevPoint.TabIndex = 18;
            this.buttonPrevPoint.Text = "<";
            this.buttonPrevPoint.UseVisualStyleBackColor = true;
            this.buttonPrevPoint.Click += new System.EventHandler(this.buttonPrevPoint_Click);
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(8, 223);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(98, 23);
            this.label3.TabIndex = 17;
            this.label3.Text = "Структура";
            // 
            // cableStructureCB
            // 
            this.cableStructureCB.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cableStructureCB.FormattingEnabled = true;
            this.cableStructureCB.Location = new System.Drawing.Point(12, 249);
            this.cableStructureCB.Name = "cableStructureCB";
            this.cableStructureCB.Size = new System.Drawing.Size(218, 31);
            this.cableStructureCB.TabIndex = 16;
            this.cableStructureCB.SelectedIndexChanged += new System.EventHandler(this.cableStructureCB_SelectedIndexChanged);
            // 
            // neasureResultPanel
            // 
            this.neasureResultPanel.Controls.Add(this.measureResultDataGrid);
            this.neasureResultPanel.Location = new System.Drawing.Point(17, 406);
            this.neasureResultPanel.Name = "neasureResultPanel";
            this.neasureResultPanel.Size = new System.Drawing.Size(626, 434);
            this.neasureResultPanel.TabIndex = 15;
            // 
            // measureResultDataGrid
            // 
            this.measureResultDataGrid.AllowUserToAddRows = false;
            this.measureResultDataGrid.AllowUserToDeleteRows = false;
            this.measureResultDataGrid.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.measureResultDataGrid.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.ElementNumber,
            this.SubElement_1,
            this.SubElement_2,
            this.SubElement_3,
            this.SubElement_4,
            this.IsMeasuredFlag});
            this.measureResultDataGrid.Dock = System.Windows.Forms.DockStyle.Fill;
            this.measureResultDataGrid.Location = new System.Drawing.Point(0, 0);
            this.measureResultDataGrid.Name = "measureResultDataGrid";
            this.measureResultDataGrid.ReadOnly = true;
            this.measureResultDataGrid.RowHeadersVisible = false;
            this.measureResultDataGrid.Size = new System.Drawing.Size(626, 434);
            this.measureResultDataGrid.TabIndex = 0;
            this.measureResultDataGrid.CellClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.measureResultDataGrid_CellClick);
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
            this.richTextBox1.Location = new System.Drawing.Point(667, 690);
            this.richTextBox1.Name = "richTextBox1";
            this.richTextBox1.Size = new System.Drawing.Size(429, 150);
            this.richTextBox1.TabIndex = 7;
            this.richTextBox1.Text = "";
            // 
            // selectDevicePanel
            // 
            this.selectDevicePanel.Controls.Add(this.deviceControlButton);
            this.selectDevicePanel.Controls.Add(this.startMeasureButton);
            this.selectDevicePanel.Controls.Add(this.availableDevices);
            this.selectDevicePanel.Location = new System.Drawing.Point(391, 205);
            this.selectDevicePanel.Name = "selectDevicePanel";
            this.selectDevicePanel.Size = new System.Drawing.Size(530, 58);
            this.selectDevicePanel.TabIndex = 14;
            // 
            // deviceControlButton
            // 
            this.deviceControlButton.Location = new System.Drawing.Point(324, 6);
            this.deviceControlButton.Name = "deviceControlButton";
            this.deviceControlButton.Size = new System.Drawing.Size(198, 49);
            this.deviceControlButton.TabIndex = 13;
            this.deviceControlButton.Text = "Подключить";
            this.deviceControlButton.UseVisualStyleBackColor = true;
            this.deviceControlButton.Click += new System.EventHandler(this.connectToDevice_Click);
            // 
            // startMeasureButton
            // 
            this.startMeasureButton.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(65)))), ((int)(((byte)(124)))), ((int)(((byte)(224)))));
            this.startMeasureButton.Cursor = System.Windows.Forms.Cursors.Hand;
            this.startMeasureButton.FlatAppearance.BorderSize = 0;
            this.startMeasureButton.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.startMeasureButton.ForeColor = System.Drawing.SystemColors.ControlLightLight;
            this.startMeasureButton.Location = new System.Drawing.Point(120, 7);
            this.startMeasureButton.Name = "startMeasureButton";
            this.startMeasureButton.Size = new System.Drawing.Size(198, 48);
            this.startMeasureButton.TabIndex = 11;
            this.startMeasureButton.Text = "Пуск измерения";
            this.startMeasureButton.UseVisualStyleBackColor = false;
            this.startMeasureButton.Click += new System.EventHandler(this.startMeasureButton_Click);
            // 
            // availableDevices
            // 
            this.availableDevices.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.availableDevices.Font = new System.Drawing.Font("Tahoma", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.availableDevices.FormattingEnabled = true;
            this.availableDevices.Location = new System.Drawing.Point(10, 19);
            this.availableDevices.Name = "availableDevices";
            this.availableDevices.Size = new System.Drawing.Size(293, 27);
            this.availableDevices.TabIndex = 12;
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.comboBox3);
            this.panel1.Controls.Add(this.temperatureComboBox);
            this.panel1.Controls.Add(this.label4);
            this.panel1.Controls.Add(this.temperature);
            this.panel1.Location = new System.Drawing.Point(728, 385);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(368, 62);
            this.panel1.TabIndex = 7;
            // 
            // comboBox3
            // 
            this.comboBox3.Font = new System.Drawing.Font("Tahoma", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.comboBox3.FormattingEnabled = true;
            this.comboBox3.Location = new System.Drawing.Point(0, 29);
            this.comboBox3.Name = "comboBox3";
            this.comboBox3.Size = new System.Drawing.Size(218, 27);
            this.comboBox3.TabIndex = 8;
            // 
            // temperatureComboBox
            // 
            this.temperatureComboBox.AutoSize = true;
            this.temperatureComboBox.Font = new System.Drawing.Font("Tahoma", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.temperatureComboBox.Location = new System.Drawing.Point(227, 3);
            this.temperatureComboBox.Name = "temperatureComboBox";
            this.temperatureComboBox.Size = new System.Drawing.Size(104, 19);
            this.temperatureComboBox.TabIndex = 3;
            this.temperatureComboBox.Text = "Температура";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Font = new System.Drawing.Font("Tahoma", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.label4.Location = new System.Drawing.Point(-4, 3);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(80, 19);
            this.label4.TabIndex = 9;
            this.label4.Text = "Материал";
            // 
            // temperature
            // 
            this.temperature.Font = new System.Drawing.Font("Tahoma", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.temperature.FormattingEnabled = true;
            this.temperature.Location = new System.Drawing.Point(230, 29);
            this.temperature.Name = "temperature";
            this.temperature.Size = new System.Drawing.Size(137, 27);
            this.temperature.TabIndex = 7;
            // 
            // panel2
            // 
            this.panel2.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(211)))), ((int)(((byte)(237)))), ((int)(((byte)(250)))));
            this.panel2.Controls.Add(this.resultField);
            this.panel2.Controls.Add(this.deviceInfo);
            this.panel2.Location = new System.Drawing.Point(405, 0);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(512, 199);
            this.panel2.TabIndex = 10;
            // 
            // resultField
            // 
            this.resultField.Font = new System.Drawing.Font("Tahoma", 27.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.resultField.Location = new System.Drawing.Point(17, 77);
            this.resultField.Name = "resultField";
            this.resultField.Size = new System.Drawing.Size(482, 103);
            this.resultField.TabIndex = 1;
            this.resultField.Text = "106.56 ТОм⋅м";
            this.resultField.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // deviceInfo
            // 
            this.deviceInfo.AutoSize = true;
            this.deviceInfo.Location = new System.Drawing.Point(18, 10);
            this.deviceInfo.Name = "deviceInfo";
            this.deviceInfo.Size = new System.Drawing.Size(273, 23);
            this.deviceInfo.TabIndex = 0;
            this.deviceInfo.Text = "Тераомметр ТОмМ-01 2021-01";
            // 
            // voltagesGroupBox
            // 
            this.voltagesGroupBox.Controls.Add(this.v1000_RadioButton);
            this.voltagesGroupBox.Controls.Add(this.v500_RadioButton);
            this.voltagesGroupBox.Controls.Add(this.v100_RadioButton);
            this.voltagesGroupBox.Controls.Add(this.v10_RadioButton);
            this.voltagesGroupBox.Font = new System.Drawing.Font("Tahoma", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.voltagesGroupBox.Location = new System.Drawing.Point(729, 467);
            this.voltagesGroupBox.Name = "voltagesGroupBox";
            this.voltagesGroupBox.Size = new System.Drawing.Size(367, 86);
            this.voltagesGroupBox.TabIndex = 6;
            this.voltagesGroupBox.TabStop = false;
            this.voltagesGroupBox.Text = "Измерительное напряжение";
            // 
            // v1000_RadioButton
            // 
            this.v1000_RadioButton.AutoSize = true;
            this.v1000_RadioButton.Location = new System.Drawing.Point(273, 38);
            this.v1000_RadioButton.Name = "v1000_RadioButton";
            this.v1000_RadioButton.Size = new System.Drawing.Size(72, 22);
            this.v1000_RadioButton.TabIndex = 3;
            this.v1000_RadioButton.Text = "1000 В";
            this.v1000_RadioButton.UseVisualStyleBackColor = true;
            // 
            // v500_RadioButton
            // 
            this.v500_RadioButton.AutoSize = true;
            this.v500_RadioButton.Location = new System.Drawing.Point(186, 38);
            this.v500_RadioButton.Name = "v500_RadioButton";
            this.v500_RadioButton.Size = new System.Drawing.Size(64, 22);
            this.v500_RadioButton.TabIndex = 2;
            this.v500_RadioButton.Text = "500 В";
            this.v500_RadioButton.UseVisualStyleBackColor = true;
            // 
            // v100_RadioButton
            // 
            this.v100_RadioButton.AutoSize = true;
            this.v100_RadioButton.Location = new System.Drawing.Point(100, 38);
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
            this.v10_RadioButton.Location = new System.Drawing.Point(23, 38);
            this.v10_RadioButton.Name = "v10_RadioButton";
            this.v10_RadioButton.Size = new System.Drawing.Size(56, 22);
            this.v10_RadioButton.TabIndex = 0;
            this.v10_RadioButton.TabStop = true;
            this.v10_RadioButton.Text = "10 В";
            this.v10_RadioButton.UseVisualStyleBackColor = true;
            // 
            // measuredParametersGroupBox
            // 
            this.measuredParametersGroupBox.Controls.Add(this.RizolRadioButton);
            this.measuredParametersGroupBox.Controls.Add(this.RleadRadioButton);
            this.measuredParametersGroupBox.Location = new System.Drawing.Point(17, 117);
            this.measuredParametersGroupBox.Name = "measuredParametersGroupBox";
            this.measuredParametersGroupBox.Size = new System.Drawing.Size(367, 82);
            this.measuredParametersGroupBox.TabIndex = 4;
            this.measuredParametersGroupBox.TabStop = false;
            this.measuredParametersGroupBox.Text = "Измеряемый параметр";
            this.measuredParametersGroupBox.UseCompatibleTextRendering = true;
            // 
            // RizolRadioButton
            // 
            this.RizolRadioButton.AutoSize = true;
            this.RizolRadioButton.Location = new System.Drawing.Point(230, 36);
            this.RizolRadioButton.Name = "RizolRadioButton";
            this.RizolRadioButton.Size = new System.Drawing.Size(81, 27);
            this.RizolRadioButton.TabIndex = 1;
            this.RizolRadioButton.Text = "Rизол";
            this.RizolRadioButton.UseVisualStyleBackColor = true;
            // 
            // RleadRadioButton
            // 
            this.RleadRadioButton.AutoSize = true;
            this.RleadRadioButton.Checked = true;
            this.RleadRadioButton.Location = new System.Drawing.Point(36, 36);
            this.RleadRadioButton.Name = "RleadRadioButton";
            this.RleadRadioButton.Size = new System.Drawing.Size(76, 27);
            this.RleadRadioButton.TabIndex = 0;
            this.RleadRadioButton.TabStop = true;
            this.RleadRadioButton.Text = "Rжил";
            this.RleadRadioButton.UseVisualStyleBackColor = true;
            this.RleadRadioButton.CheckedChanged += new System.EventHandler(this.MeasureTypeRadioButton_CheckedChanged_Common);
            // 
            // MeasureForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(10F, 23F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.White;
            this.ClientSize = new System.Drawing.Size(1108, 861);
            this.Controls.Add(this.measurePanel);
            this.Font = new System.Drawing.Font("Tahoma", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Margin = new System.Windows.Forms.Padding(4, 6, 4, 6);
            this.Name = "MeasureForm";
            this.Text = "Измерение";
            ((System.ComponentModel.ISupportInitialize)(this.cableLengthNumericUpDown)).EndInit();
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.afterMeasureDelayUpDown)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.beforeMeasureDelayUpDown)).EndInit();
            this.measurePanel.ResumeLayout(false);
            this.measurePanel.PerformLayout();
            this.neasureResultPanel.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.measureResultDataGrid)).EndInit();
            this.selectDevicePanel.ResumeLayout(false);
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.panel2.ResumeLayout(false);
            this.panel2.PerformLayout();
            this.voltagesGroupBox.ResumeLayout(false);
            this.voltagesGroupBox.PerformLayout();
            this.measuredParametersGroupBox.ResumeLayout(false);
            this.measuredParametersGroupBox.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.ComboBox cableComboBox;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.NumericUpDown cableLengthNumericUpDown;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.ComboBox averagingCounter;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label polarDelayLbl;
        private System.Windows.Forms.NumericUpDown beforeMeasureDelayUpDown;
        private System.Windows.Forms.Panel measurePanel;
        private System.Windows.Forms.Label depolTimeLbl;
        private System.Windows.Forms.NumericUpDown afterMeasureDelayUpDown;
        private System.Windows.Forms.Button startMeasureButton;
        private System.Windows.Forms.Panel panel2;
        private System.Windows.Forms.RichTextBox richTextBox1;
        private System.Windows.Forms.ComboBox availableDevices;
        private System.Windows.Forms.Panel selectDevicePanel;
        private System.Windows.Forms.Button deviceControlButton;
        private System.Windows.Forms.Label resultField;
        private System.Windows.Forms.Label deviceInfo;
        private System.Windows.Forms.GroupBox measuredParametersGroupBox;
        private System.Windows.Forms.RadioButton RizolRadioButton;
        private System.Windows.Forms.RadioButton RleadRadioButton;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.ComboBox comboBox3;
        private System.Windows.Forms.Label temperatureComboBox;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.ComboBox temperature;
        private System.Windows.Forms.GroupBox voltagesGroupBox;
        private System.Windows.Forms.RadioButton v1000_RadioButton;
        private System.Windows.Forms.RadioButton v500_RadioButton;
        private System.Windows.Forms.RadioButton v100_RadioButton;
        private System.Windows.Forms.RadioButton v10_RadioButton;
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
        private System.Windows.Forms.Label labelPointNumber;
        private System.Windows.Forms.Button buttonNextPoint;
        private System.Windows.Forms.Button buttonPrevPoint;
        private System.Windows.Forms.Button buttonPrevElement;
        private System.Windows.Forms.Button buttonNextElement;
    }
}