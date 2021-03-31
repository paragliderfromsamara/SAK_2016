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
            this.measureResultDataGrid = new System.Windows.Forms.DataGridView();
            this.ElementNumber = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.SubElement_1 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.SubElement_2 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.SubElement_3 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.SubElement_4 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.IsMeasuredFlag = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.richTextBox1 = new System.Windows.Forms.RichTextBox();
            this.selectDevicePanel = new System.Windows.Forms.Panel();
            this.availableDevices = new System.Windows.Forms.ComboBox();
            this.deviceControlButton = new System.Windows.Forms.Button();
            this.startMeasureButton = new System.Windows.Forms.Button();
            this.panel1 = new System.Windows.Forms.Panel();
            this.comboBox3 = new System.Windows.Forms.ComboBox();
            this.label4 = new System.Windows.Forms.Label();
            this.panel2 = new System.Windows.Forms.Panel();
            this.normaLabel = new System.Windows.Forms.Label();
            this.measureTimerLabel = new System.Windows.Forms.Label();
            this.resultField = new System.Windows.Forms.Label();
            this.deviceInfo = new System.Windows.Forms.Label();
            this.labelPointNumber = new System.Windows.Forms.Label();
            this.voltagesGroupBox = new System.Windows.Forms.GroupBox();
            this.v1000_RadioButton = new System.Windows.Forms.RadioButton();
            this.v500_RadioButton = new System.Windows.Forms.RadioButton();
            this.v100_RadioButton = new System.Windows.Forms.RadioButton();
            this.v10_RadioButton = new System.Windows.Forms.RadioButton();
            this.panelMeasurePointControl = new System.Windows.Forms.Panel();
            this.panel3 = new System.Windows.Forms.Panel();
            this.panel6 = new System.Windows.Forms.Panel();
            this.lblNoMeasureData = new System.Windows.Forms.Label();
            this.measuredParameterDataTabs = new System.Windows.Forms.TabControl();
            this.tabPage1 = new System.Windows.Forms.TabPage();
            this.panelResultMeasure = new System.Windows.Forms.Panel();
            this.tabPage2 = new System.Windows.Forms.TabPage();
            this.testParamsControlPanel = new System.Windows.Forms.Panel();
            this.testDraftControlPanel = new System.Windows.Forms.Panel();
            this.resetTestButton = new System.Windows.Forms.Button();
            this.saveResultButton = new System.Windows.Forms.Button();
            this.temperatureValue = new System.Windows.Forms.NumericUpDown();
            this.label1 = new System.Windows.Forms.Label();
            this.temperatureComboBox = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.cableLengthNumericUpDown = new System.Windows.Forms.NumericUpDown();
            this.cableStructureCB = new System.Windows.Forms.ComboBox();
            this.deviceParametersGroupBox = new System.Windows.Forms.GroupBox();
            this.depolTimeLbl = new System.Windows.Forms.Label();
            this.polarDelayLbl = new System.Windows.Forms.Label();
            this.averagingCounter = new System.Windows.Forms.ComboBox();
            this.beforeMeasureDelayUpDown = new System.Windows.Forms.NumericUpDown();
            this.label5 = new System.Windows.Forms.Label();
            this.afterMeasureDelayUpDown = new System.Windows.Forms.NumericUpDown();
            this.cableComboBox = new System.Windows.Forms.ComboBox();
            this.panel5 = new System.Windows.Forms.Panel();
            this.panel4 = new System.Windows.Forms.Panel();
            this.measuredParameterSelect = new System.Windows.Forms.Panel();
            this.rIsolTypeSelectorCB = new System.Windows.Forms.ComboBox();
            this.measuredParameterCB = new System.Windows.Forms.ComboBox();
            ((System.ComponentModel.ISupportInitialize)(this.measureResultDataGrid)).BeginInit();
            this.selectDevicePanel.SuspendLayout();
            this.panel1.SuspendLayout();
            this.panel2.SuspendLayout();
            this.voltagesGroupBox.SuspendLayout();
            this.panelMeasurePointControl.SuspendLayout();
            this.panel3.SuspendLayout();
            this.panel6.SuspendLayout();
            this.measuredParameterDataTabs.SuspendLayout();
            this.tabPage1.SuspendLayout();
            this.panelResultMeasure.SuspendLayout();
            this.testParamsControlPanel.SuspendLayout();
            this.testDraftControlPanel.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.temperatureValue)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.cableLengthNumericUpDown)).BeginInit();
            this.deviceParametersGroupBox.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.beforeMeasureDelayUpDown)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.afterMeasureDelayUpDown)).BeginInit();
            this.panel5.SuspendLayout();
            this.panel4.SuspendLayout();
            this.measuredParameterSelect.SuspendLayout();
            this.SuspendLayout();
            // 
            // buttonPrevElement
            // 
            this.buttonPrevElement.BackColor = System.Drawing.Color.DodgerBlue;
            this.buttonPrevElement.Dock = System.Windows.Forms.DockStyle.Left;
            this.buttonPrevElement.FlatAppearance.BorderSize = 0;
            this.buttonPrevElement.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.buttonPrevElement.Location = new System.Drawing.Point(0, 0);
            this.buttonPrevElement.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.buttonPrevElement.Name = "buttonPrevElement";
            this.buttonPrevElement.Size = new System.Drawing.Size(44, 51);
            this.buttonPrevElement.TabIndex = 40;
            this.buttonPrevElement.Text = "<<";
            this.buttonPrevElement.UseVisualStyleBackColor = false;
            this.buttonPrevElement.Click += new System.EventHandler(this.buttonPrevElement_Click);
            // 
            // buttonNextElement
            // 
            this.buttonNextElement.BackColor = System.Drawing.Color.DodgerBlue;
            this.buttonNextElement.Dock = System.Windows.Forms.DockStyle.Left;
            this.buttonNextElement.FlatAppearance.BorderSize = 0;
            this.buttonNextElement.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.buttonNextElement.Location = new System.Drawing.Point(302, 0);
            this.buttonNextElement.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.buttonNextElement.Name = "buttonNextElement";
            this.buttonNextElement.Size = new System.Drawing.Size(44, 51);
            this.buttonNextElement.TabIndex = 39;
            this.buttonNextElement.Text = ">>";
            this.buttonNextElement.UseVisualStyleBackColor = false;
            this.buttonNextElement.Click += new System.EventHandler(this.buttonNextElement_Click);
            // 
            // buttonNextPoint
            // 
            this.buttonNextPoint.BackColor = System.Drawing.Color.DodgerBlue;
            this.buttonNextPoint.Dock = System.Windows.Forms.DockStyle.Left;
            this.buttonNextPoint.FlatAppearance.BorderSize = 0;
            this.buttonNextPoint.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.buttonNextPoint.Location = new System.Drawing.Point(262, 0);
            this.buttonNextPoint.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.buttonNextPoint.Name = "buttonNextPoint";
            this.buttonNextPoint.Size = new System.Drawing.Size(40, 51);
            this.buttonNextPoint.TabIndex = 38;
            this.buttonNextPoint.Text = ">";
            this.buttonNextPoint.UseVisualStyleBackColor = false;
            this.buttonNextPoint.Click += new System.EventHandler(this.buttonNextPoint_Click);
            // 
            // buttonPrevPoint
            // 
            this.buttonPrevPoint.BackColor = System.Drawing.Color.DodgerBlue;
            this.buttonPrevPoint.Dock = System.Windows.Forms.DockStyle.Left;
            this.buttonPrevPoint.FlatAppearance.BorderSize = 0;
            this.buttonPrevPoint.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.buttonPrevPoint.Location = new System.Drawing.Point(44, 0);
            this.buttonPrevPoint.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.buttonPrevPoint.Name = "buttonPrevPoint";
            this.buttonPrevPoint.Size = new System.Drawing.Size(40, 51);
            this.buttonPrevPoint.TabIndex = 37;
            this.buttonPrevPoint.Text = "<";
            this.buttonPrevPoint.UseVisualStyleBackColor = false;
            this.buttonPrevPoint.Click += new System.EventHandler(this.buttonPrevPoint_Click);
            // 
            // measureResultDataGrid
            // 
            this.measureResultDataGrid.AllowUserToAddRows = false;
            this.measureResultDataGrid.AllowUserToDeleteRows = false;
            this.measureResultDataGrid.AllowUserToResizeColumns = false;
            this.measureResultDataGrid.AllowUserToResizeRows = false;
            this.measureResultDataGrid.AutoSizeRowsMode = System.Windows.Forms.DataGridViewAutoSizeRowsMode.DisplayedCells;
            this.measureResultDataGrid.BackgroundColor = System.Drawing.Color.FromArgb(((int)(((byte)(248)))), ((int)(((byte)(248)))), ((int)(((byte)(248)))));
            this.measureResultDataGrid.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.measureResultDataGrid.ColumnHeadersBorderStyle = System.Windows.Forms.DataGridViewHeaderBorderStyle.None;
            this.measureResultDataGrid.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.measureResultDataGrid.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.ElementNumber,
            this.SubElement_1,
            this.SubElement_2,
            this.SubElement_3,
            this.SubElement_4,
            this.IsMeasuredFlag});
            this.measureResultDataGrid.Dock = System.Windows.Forms.DockStyle.Fill;
            this.measureResultDataGrid.EnableHeadersVisualStyles = false;
            this.measureResultDataGrid.GridColor = System.Drawing.SystemColors.ButtonFace;
            this.measureResultDataGrid.Location = new System.Drawing.Point(0, 0);
            this.measureResultDataGrid.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.measureResultDataGrid.MultiSelect = false;
            this.measureResultDataGrid.Name = "measureResultDataGrid";
            this.measureResultDataGrid.ReadOnly = true;
            this.measureResultDataGrid.RowHeadersBorderStyle = System.Windows.Forms.DataGridViewHeaderBorderStyle.Single;
            this.measureResultDataGrid.RowHeadersVisible = false;
            this.measureResultDataGrid.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.measureResultDataGrid.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.CellSelect;
            this.measureResultDataGrid.ShowCellErrors = false;
            this.measureResultDataGrid.ShowCellToolTips = false;
            this.measureResultDataGrid.ShowEditingIcon = false;
            this.measureResultDataGrid.ShowRowErrors = false;
            this.measureResultDataGrid.Size = new System.Drawing.Size(426, 610);
            this.measureResultDataGrid.TabIndex = 0;
            this.measureResultDataGrid.CellClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.measureResultDataGrid_CellClick);
            this.measureResultDataGrid.KeyDown += new System.Windows.Forms.KeyEventHandler(this.dataGridView1_KeyDown);
            // 
            // ElementNumber
            // 
            this.ElementNumber.DataPropertyName = "element_number";
            this.ElementNumber.HeaderText = "Элемент";
            this.ElementNumber.MinimumWidth = 100;
            this.ElementNumber.Name = "ElementNumber";
            this.ElementNumber.ReadOnly = true;
            this.ElementNumber.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            // 
            // SubElement_1
            // 
            this.SubElement_1.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            this.SubElement_1.DataPropertyName = "measure_1";
            this.SubElement_1.HeaderText = "Измерение 1";
            this.SubElement_1.Name = "SubElement_1";
            this.SubElement_1.ReadOnly = true;
            this.SubElement_1.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            // 
            // SubElement_2
            // 
            this.SubElement_2.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            this.SubElement_2.DataPropertyName = "measure_2";
            this.SubElement_2.HeaderText = "Измерение 2";
            this.SubElement_2.Name = "SubElement_2";
            this.SubElement_2.ReadOnly = true;
            this.SubElement_2.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            // 
            // SubElement_3
            // 
            this.SubElement_3.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            this.SubElement_3.DataPropertyName = "measure_3";
            this.SubElement_3.HeaderText = "Измерение 3";
            this.SubElement_3.Name = "SubElement_3";
            this.SubElement_3.ReadOnly = true;
            this.SubElement_3.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            // 
            // SubElement_4
            // 
            this.SubElement_4.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            this.SubElement_4.DataPropertyName = "measure_4";
            this.SubElement_4.HeaderText = "Измерение 4";
            this.SubElement_4.Name = "SubElement_4";
            this.SubElement_4.ReadOnly = true;
            this.SubElement_4.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            // 
            // IsMeasuredFlag
            // 
            this.IsMeasuredFlag.HeaderText = "Column1";
            this.IsMeasuredFlag.Name = "IsMeasuredFlag";
            this.IsMeasuredFlag.ReadOnly = true;
            this.IsMeasuredFlag.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            this.IsMeasuredFlag.Visible = false;
            // 
            // richTextBox1
            // 
            this.richTextBox1.Location = new System.Drawing.Point(8, 31);
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
            this.selectDevicePanel.Dock = System.Windows.Forms.DockStyle.Right;
            this.selectDevicePanel.Location = new System.Drawing.Point(440, 0);
            this.selectDevicePanel.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.selectDevicePanel.Name = "selectDevicePanel";
            this.selectDevicePanel.Size = new System.Drawing.Size(475, 51);
            this.selectDevicePanel.TabIndex = 33;
            // 
            // availableDevices
            // 
            this.availableDevices.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(224)))), ((int)(((byte)(224)))), ((int)(((byte)(224)))));
            this.availableDevices.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.availableDevices.FlatStyle = System.Windows.Forms.FlatStyle.System;
            this.availableDevices.Font = new System.Drawing.Font("Tahoma", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.availableDevices.IntegralHeight = false;
            this.availableDevices.ItemHeight = 19;
            this.availableDevices.Location = new System.Drawing.Point(0, 13);
            this.availableDevices.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.availableDevices.Name = "availableDevices";
            this.availableDevices.Size = new System.Drawing.Size(297, 27);
            this.availableDevices.TabIndex = 12;
            // 
            // deviceControlButton
            // 
            this.deviceControlButton.BackColor = System.Drawing.Color.RoyalBlue;
            this.deviceControlButton.Dock = System.Windows.Forms.DockStyle.Right;
            this.deviceControlButton.FlatAppearance.BorderSize = 0;
            this.deviceControlButton.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.deviceControlButton.Location = new System.Drawing.Point(297, 0);
            this.deviceControlButton.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.deviceControlButton.Name = "deviceControlButton";
            this.deviceControlButton.Size = new System.Drawing.Size(178, 51);
            this.deviceControlButton.TabIndex = 13;
            this.deviceControlButton.Text = "Подключить";
            this.deviceControlButton.UseVisualStyleBackColor = false;
            this.deviceControlButton.Click += new System.EventHandler(this.connectToDevice_Click);
            // 
            // startMeasureButton
            // 
            this.startMeasureButton.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(206)))), ((int)(((byte)(31)))));
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
            this.startMeasureButton.Click += new System.EventHandler(this.startMeasureButton_Click);
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.comboBox3);
            this.panel1.Controls.Add(this.label4);
            this.panel1.Location = new System.Drawing.Point(694, 21);
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
            // panel2
            // 
            this.panel2.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(56)))), ((int)(((byte)(104)))), ((int)(((byte)(169)))));
            this.panel2.Controls.Add(this.normaLabel);
            this.panel2.Controls.Add(this.measureTimerLabel);
            this.panel2.Controls.Add(this.resultField);
            this.panel2.Controls.Add(this.deviceInfo);
            this.panel2.Controls.Add(this.labelPointNumber);
            this.panel2.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel2.ForeColor = System.Drawing.Color.Gainsboro;
            this.panel2.Location = new System.Drawing.Point(0, 0);
            this.panel2.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(915, 176);
            this.panel2.TabIndex = 32;
            // 
            // normaLabel
            // 
            this.normaLabel.Font = new System.Drawing.Font("Tahoma", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.normaLabel.Location = new System.Drawing.Point(12, 98);
            this.normaLabel.Name = "normaLabel";
            this.normaLabel.Size = new System.Drawing.Size(879, 32);
            this.normaLabel.TabIndex = 22;
            this.normaLabel.Text = "норма: 10 МОм";
            this.normaLabel.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // measureTimerLabel
            // 
            this.measureTimerLabel.AutoSize = true;
            this.measureTimerLabel.Location = new System.Drawing.Point(201, 137);
            this.measureTimerLabel.Name = "measureTimerLabel";
            this.measureTimerLabel.Size = new System.Drawing.Size(51, 19);
            this.measureTimerLabel.TabIndex = 21;
            this.measureTimerLabel.Text = "00:00";
            // 
            // resultField
            // 
            this.resultField.Font = new System.Drawing.Font("Tahoma", 27.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.resultField.Location = new System.Drawing.Point(12, 54);
            this.resultField.Name = "resultField";
            this.resultField.Size = new System.Drawing.Size(794, 48);
            this.resultField.TabIndex = 1;
            this.resultField.Text = "106.56 ТОм⋅м";
            this.resultField.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
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
            this.labelPointNumber.Location = new System.Drawing.Point(12, 137);
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
            this.voltagesGroupBox.Location = new System.Drawing.Point(561, 103);
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
            // panelMeasurePointControl
            // 
            this.panelMeasurePointControl.Controls.Add(this.buttonNextElement);
            this.panelMeasurePointControl.Controls.Add(this.buttonNextPoint);
            this.panelMeasurePointControl.Controls.Add(this.startMeasureButton);
            this.panelMeasurePointControl.Controls.Add(this.buttonPrevPoint);
            this.panelMeasurePointControl.Controls.Add(this.buttonPrevElement);
            this.panelMeasurePointControl.Cursor = System.Windows.Forms.Cursors.Hand;
            this.panelMeasurePointControl.Dock = System.Windows.Forms.DockStyle.Left;
            this.panelMeasurePointControl.Location = new System.Drawing.Point(0, 0);
            this.panelMeasurePointControl.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.panelMeasurePointControl.Name = "panelMeasurePointControl";
            this.panelMeasurePointControl.Size = new System.Drawing.Size(351, 51);
            this.panelMeasurePointControl.TabIndex = 41;
            // 
            // panel3
            // 
            this.panel3.Controls.Add(this.panel6);
            this.panel3.Controls.Add(this.panel5);
            this.panel3.Controls.Add(this.panel4);
            this.panel3.Controls.Add(this.panel2);
            this.panel3.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel3.Location = new System.Drawing.Point(0, 0);
            this.panel3.Name = "panel3";
            this.panel3.Size = new System.Drawing.Size(915, 985);
            this.panel3.TabIndex = 42;
            // 
            // panel6
            // 
            this.panel6.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(243)))), ((int)(((byte)(243)))), ((int)(((byte)(243)))));
            this.panel6.Controls.Add(this.lblNoMeasureData);
            this.panel6.Controls.Add(this.measuredParameterDataTabs);
            this.panel6.Controls.Add(this.testParamsControlPanel);
            this.panel6.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel6.Location = new System.Drawing.Point(0, 227);
            this.panel6.Name = "panel6";
            this.panel6.Size = new System.Drawing.Size(915, 647);
            this.panel6.TabIndex = 45;
            // 
            // lblNoMeasureData
            // 
            this.lblNoMeasureData.AutoSize = true;
            this.lblNoMeasureData.Location = new System.Drawing.Point(89, 39);
            this.lblNoMeasureData.Name = "lblNoMeasureData";
            this.lblNoMeasureData.Size = new System.Drawing.Size(241, 19);
            this.lblNoMeasureData.TabIndex = 1;
            this.lblNoMeasureData.Text = "Для структуры {0} отсутсвуют ";
            this.lblNoMeasureData.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // measuredParameterDataTabs
            // 
            this.measuredParameterDataTabs.Controls.Add(this.tabPage1);
            this.measuredParameterDataTabs.Controls.Add(this.tabPage2);
            this.measuredParameterDataTabs.Dock = System.Windows.Forms.DockStyle.Fill;
            this.measuredParameterDataTabs.Font = new System.Drawing.Font("Tahoma", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.measuredParameterDataTabs.Location = new System.Drawing.Point(0, 0);
            this.measuredParameterDataTabs.Name = "measuredParameterDataTabs";
            this.measuredParameterDataTabs.SelectedIndex = 0;
            this.measuredParameterDataTabs.Size = new System.Drawing.Size(440, 647);
            this.measuredParameterDataTabs.TabIndex = 38;
            this.measuredParameterDataTabs.SelectedIndexChanged += new System.EventHandler(this.measuredParameterDataTabs_SelectedIndexChanged);
            // 
            // tabPage1
            // 
            this.tabPage1.Controls.Add(this.panelResultMeasure);
            this.tabPage1.Location = new System.Drawing.Point(4, 27);
            this.tabPage1.Name = "tabPage1";
            this.tabPage1.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage1.Size = new System.Drawing.Size(432, 616);
            this.tabPage1.TabIndex = 0;
            this.tabPage1.Text = "tabPage1";
            this.tabPage1.UseVisualStyleBackColor = true;
            // 
            // panelResultMeasure
            // 
            this.panelResultMeasure.Controls.Add(this.measureResultDataGrid);
            this.panelResultMeasure.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panelResultMeasure.Location = new System.Drawing.Point(3, 3);
            this.panelResultMeasure.Name = "panelResultMeasure";
            this.panelResultMeasure.Size = new System.Drawing.Size(426, 610);
            this.panelResultMeasure.TabIndex = 38;
            // 
            // tabPage2
            // 
            this.tabPage2.Location = new System.Drawing.Point(4, 27);
            this.tabPage2.Name = "tabPage2";
            this.tabPage2.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage2.Size = new System.Drawing.Size(432, 616);
            this.tabPage2.TabIndex = 1;
            this.tabPage2.Text = "tabPage2";
            this.tabPage2.UseVisualStyleBackColor = true;
            // 
            // testParamsControlPanel
            // 
            this.testParamsControlPanel.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(244)))), ((int)(((byte)(251)))), ((int)(((byte)(255)))));
            this.testParamsControlPanel.Controls.Add(this.testDraftControlPanel);
            this.testParamsControlPanel.Controls.Add(this.temperatureValue);
            this.testParamsControlPanel.Controls.Add(this.label1);
            this.testParamsControlPanel.Controls.Add(this.temperatureComboBox);
            this.testParamsControlPanel.Controls.Add(this.label2);
            this.testParamsControlPanel.Controls.Add(this.label3);
            this.testParamsControlPanel.Controls.Add(this.cableLengthNumericUpDown);
            this.testParamsControlPanel.Controls.Add(this.cableStructureCB);
            this.testParamsControlPanel.Controls.Add(this.deviceParametersGroupBox);
            this.testParamsControlPanel.Controls.Add(this.cableComboBox);
            this.testParamsControlPanel.Dock = System.Windows.Forms.DockStyle.Right;
            this.testParamsControlPanel.ForeColor = System.Drawing.Color.Black;
            this.testParamsControlPanel.Location = new System.Drawing.Point(440, 0);
            this.testParamsControlPanel.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.testParamsControlPanel.Name = "testParamsControlPanel";
            this.testParamsControlPanel.Size = new System.Drawing.Size(475, 647);
            this.testParamsControlPanel.TabIndex = 35;
            // 
            // testDraftControlPanel
            // 
            this.testDraftControlPanel.Controls.Add(this.resetTestButton);
            this.testDraftControlPanel.Controls.Add(this.saveResultButton);
            this.testDraftControlPanel.Location = new System.Drawing.Point(18, 387);
            this.testDraftControlPanel.Name = "testDraftControlPanel";
            this.testDraftControlPanel.Size = new System.Drawing.Size(432, 47);
            this.testDraftControlPanel.TabIndex = 38;
            // 
            // resetTestButton
            // 
            this.resetTestButton.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(249)))), ((int)(((byte)(130)))), ((int)(((byte)(43)))));
            this.resetTestButton.Dock = System.Windows.Forms.DockStyle.Left;
            this.resetTestButton.FlatAppearance.BorderSize = 0;
            this.resetTestButton.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.resetTestButton.ForeColor = System.Drawing.Color.White;
            this.resetTestButton.Location = new System.Drawing.Point(212, 0);
            this.resetTestButton.Name = "resetTestButton";
            this.resetTestButton.Size = new System.Drawing.Size(221, 47);
            this.resetTestButton.TabIndex = 1;
            this.resetTestButton.Text = "Сбросить результаты";
            this.resetTestButton.UseVisualStyleBackColor = false;
            this.resetTestButton.Click += new System.EventHandler(this.resetTestButton_Click);
            // 
            // saveResultButton
            // 
            this.saveResultButton.BackColor = System.Drawing.Color.LimeGreen;
            this.saveResultButton.Dock = System.Windows.Forms.DockStyle.Left;
            this.saveResultButton.FlatAppearance.BorderSize = 0;
            this.saveResultButton.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.saveResultButton.ForeColor = System.Drawing.Color.White;
            this.saveResultButton.Location = new System.Drawing.Point(0, 0);
            this.saveResultButton.Name = "saveResultButton";
            this.saveResultButton.Size = new System.Drawing.Size(212, 47);
            this.saveResultButton.TabIndex = 0;
            this.saveResultButton.Text = "Сохранить результаты";
            this.saveResultButton.UseVisualStyleBackColor = false;
            this.saveResultButton.Click += new System.EventHandler(this.saveResultButton_Click);
            // 
            // temperatureValue
            // 
            this.temperatureValue.Location = new System.Drawing.Point(142, 175);
            this.temperatureValue.Maximum = new decimal(new int[] {
            35,
            0,
            0,
            0});
            this.temperatureValue.Minimum = new decimal(new int[] {
            5,
            0,
            0,
            0});
            this.temperatureValue.Name = "temperatureValue";
            this.temperatureValue.Size = new System.Drawing.Size(102, 27);
            this.temperatureValue.TabIndex = 37;
            this.temperatureValue.Value = new decimal(new int[] {
            20,
            0,
            0,
            0});
            this.temperatureValue.ValueChanged += new System.EventHandler(this.temperature_ValueChanged);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(13, 15);
            this.label1.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(61, 19);
            this.label1.TabIndex = 24;
            this.label1.Text = "Кабель";
            // 
            // temperatureComboBox
            // 
            this.temperatureComboBox.AutoSize = true;
            this.temperatureComboBox.Font = new System.Drawing.Font("Tahoma", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.temperatureComboBox.Location = new System.Drawing.Point(138, 154);
            this.temperatureComboBox.Name = "temperatureComboBox";
            this.temperatureComboBox.Size = new System.Drawing.Size(104, 19);
            this.temperatureComboBox.TabIndex = 3;
            this.temperatureComboBox.Text = "Температура";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(13, 153);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(75, 19);
            this.label2.TabIndex = 26;
            this.label2.Text = "Длина, м";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(14, 81);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(85, 19);
            this.label3.TabIndex = 36;
            this.label3.Text = "Структура";
            // 
            // cableLengthNumericUpDown
            // 
            this.cableLengthNumericUpDown.Location = new System.Drawing.Point(18, 175);
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
            // cableStructureCB
            // 
            this.cableStructureCB.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cableStructureCB.FormattingEnabled = true;
            this.cableStructureCB.Location = new System.Drawing.Point(17, 103);
            this.cableStructureCB.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.cableStructureCB.Name = "cableStructureCB";
            this.cableStructureCB.Size = new System.Drawing.Size(434, 27);
            this.cableStructureCB.TabIndex = 35;
            this.cableStructureCB.SelectedValueChanged += new System.EventHandler(this.cableStructureCB_SelectedIndexChanged);
            // 
            // deviceParametersGroupBox
            // 
            this.deviceParametersGroupBox.Controls.Add(this.depolTimeLbl);
            this.deviceParametersGroupBox.Controls.Add(this.polarDelayLbl);
            this.deviceParametersGroupBox.Controls.Add(this.averagingCounter);
            this.deviceParametersGroupBox.Controls.Add(this.beforeMeasureDelayUpDown);
            this.deviceParametersGroupBox.Controls.Add(this.label5);
            this.deviceParametersGroupBox.Controls.Add(this.afterMeasureDelayUpDown);
            this.deviceParametersGroupBox.Font = new System.Drawing.Font("Tahoma", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.deviceParametersGroupBox.Location = new System.Drawing.Point(18, 229);
            this.deviceParametersGroupBox.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.deviceParametersGroupBox.Name = "deviceParametersGroupBox";
            this.deviceParametersGroupBox.Padding = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.deviceParametersGroupBox.Size = new System.Drawing.Size(433, 96);
            this.deviceParametersGroupBox.TabIndex = 28;
            this.deviceParametersGroupBox.TabStop = false;
            this.deviceParametersGroupBox.Text = "Настройки измерителя";
            // 
            // depolTimeLbl
            // 
            this.depolTimeLbl.AutoSize = true;
            this.depolTimeLbl.Location = new System.Drawing.Point(303, 28);
            this.depolTimeLbl.Name = "depolTimeLbl";
            this.depolTimeLbl.Size = new System.Drawing.Size(68, 19);
            this.depolTimeLbl.TabIndex = 8;
            this.depolTimeLbl.Text = "Пауза, c";
            // 
            // polarDelayLbl
            // 
            this.polarDelayLbl.AutoSize = true;
            this.polarDelayLbl.Location = new System.Drawing.Point(16, 28);
            this.polarDelayLbl.Name = "polarDelayLbl";
            this.polarDelayLbl.Size = new System.Drawing.Size(110, 19);
            this.polarDelayLbl.TabIndex = 1;
            this.polarDelayLbl.Text = "Выдержка, мс";
            // 
            // averagingCounter
            // 
            this.averagingCounter.FormattingEnabled = true;
            this.averagingCounter.Location = new System.Drawing.Point(153, 49);
            this.averagingCounter.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.averagingCounter.Name = "averagingCounter";
            this.averagingCounter.Size = new System.Drawing.Size(127, 27);
            this.averagingCounter.TabIndex = 6;
            // 
            // beforeMeasureDelayUpDown
            // 
            this.beforeMeasureDelayUpDown.Location = new System.Drawing.Point(19, 49);
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
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(150, 27);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(130, 19);
            this.label5.TabIndex = 5;
            this.label5.Text = "Усреднение, изм";
            // 
            // afterMeasureDelayUpDown
            // 
            this.afterMeasureDelayUpDown.Location = new System.Drawing.Point(306, 49);
            this.afterMeasureDelayUpDown.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.afterMeasureDelayUpDown.Maximum = new decimal(new int[] {
            300,
            0,
            0,
            0});
            this.afterMeasureDelayUpDown.Name = "afterMeasureDelayUpDown";
            this.afterMeasureDelayUpDown.Size = new System.Drawing.Size(106, 27);
            this.afterMeasureDelayUpDown.TabIndex = 7;
            // 
            // cableComboBox
            // 
            this.cableComboBox.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cableComboBox.FormattingEnabled = true;
            this.cableComboBox.Items.AddRange(new object[] {
            "Список пуст"});
            this.cableComboBox.Location = new System.Drawing.Point(17, 38);
            this.cableComboBox.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.cableComboBox.Name = "cableComboBox";
            this.cableComboBox.Size = new System.Drawing.Size(434, 27);
            this.cableComboBox.TabIndex = 23;
            // 
            // panel5
            // 
            this.panel5.Controls.Add(this.richTextBox1);
            this.panel5.Controls.Add(this.panel1);
            this.panel5.Controls.Add(this.voltagesGroupBox);
            this.panel5.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.panel5.Location = new System.Drawing.Point(0, 874);
            this.panel5.Name = "panel5";
            this.panel5.Size = new System.Drawing.Size(915, 111);
            this.panel5.TabIndex = 44;
            this.panel5.Visible = false;
            // 
            // panel4
            // 
            this.panel4.BackColor = System.Drawing.Color.White;
            this.panel4.Controls.Add(this.measuredParameterSelect);
            this.panel4.Controls.Add(this.panelMeasurePointControl);
            this.panel4.Controls.Add(this.selectDevicePanel);
            this.panel4.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel4.ForeColor = System.Drawing.Color.Gainsboro;
            this.panel4.Location = new System.Drawing.Point(0, 176);
            this.panel4.Name = "panel4";
            this.panel4.Size = new System.Drawing.Size(915, 51);
            this.panel4.TabIndex = 43;
            // 
            // measuredParameterSelect
            // 
            this.measuredParameterSelect.BackColor = System.Drawing.Color.Transparent;
            this.measuredParameterSelect.Controls.Add(this.rIsolTypeSelectorCB);
            this.measuredParameterSelect.Controls.Add(this.measuredParameterCB);
            this.measuredParameterSelect.Dock = System.Windows.Forms.DockStyle.Left;
            this.measuredParameterSelect.ForeColor = System.Drawing.Color.Black;
            this.measuredParameterSelect.Location = new System.Drawing.Point(351, 0);
            this.measuredParameterSelect.Name = "measuredParameterSelect";
            this.measuredParameterSelect.Size = new System.Drawing.Size(387, 51);
            this.measuredParameterSelect.TabIndex = 37;
            // 
            // rIsolTypeSelectorCB
            // 
            this.rIsolTypeSelectorCB.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.rIsolTypeSelectorCB.FormattingEnabled = true;
            this.rIsolTypeSelectorCB.Location = new System.Drawing.Point(239, 13);
            this.rIsolTypeSelectorCB.Name = "rIsolTypeSelectorCB";
            this.rIsolTypeSelectorCB.Size = new System.Drawing.Size(138, 27);
            this.rIsolTypeSelectorCB.TabIndex = 3;
            // 
            // measuredParameterCB
            // 
            this.measuredParameterCB.FormattingEnabled = true;
            this.measuredParameterCB.Location = new System.Drawing.Point(6, 13);
            this.measuredParameterCB.Name = "measuredParameterCB";
            this.measuredParameterCB.Size = new System.Drawing.Size(227, 27);
            this.measuredParameterCB.TabIndex = 2;
            this.measuredParameterCB.SelectedIndexChanged += new System.EventHandler(this.measuredParameterCB_SelectedIndexChanged);
            // 
            // MeasureForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(9F, 19F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.White;
            this.ClientSize = new System.Drawing.Size(915, 985);
            this.Controls.Add(this.panel3);
            this.Font = new System.Drawing.Font("Tahoma", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.MinimumSize = new System.Drawing.Size(931, 39);
            this.Name = "MeasureForm";
            this.Text = "Измерение";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.MeasureForm_FormClosing);
            this.KeyDown += new System.Windows.Forms.KeyEventHandler(this.dataGridView1_KeyDown);
            ((System.ComponentModel.ISupportInitialize)(this.measureResultDataGrid)).EndInit();
            this.selectDevicePanel.ResumeLayout(false);
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.panel2.ResumeLayout(false);
            this.panel2.PerformLayout();
            this.voltagesGroupBox.ResumeLayout(false);
            this.voltagesGroupBox.PerformLayout();
            this.panelMeasurePointControl.ResumeLayout(false);
            this.panel3.ResumeLayout(false);
            this.panel6.ResumeLayout(false);
            this.panel6.PerformLayout();
            this.measuredParameterDataTabs.ResumeLayout(false);
            this.tabPage1.ResumeLayout(false);
            this.panelResultMeasure.ResumeLayout(false);
            this.testParamsControlPanel.ResumeLayout(false);
            this.testParamsControlPanel.PerformLayout();
            this.testDraftControlPanel.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.temperatureValue)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.cableLengthNumericUpDown)).EndInit();
            this.deviceParametersGroupBox.ResumeLayout(false);
            this.deviceParametersGroupBox.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.beforeMeasureDelayUpDown)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.afterMeasureDelayUpDown)).EndInit();
            this.panel5.ResumeLayout(false);
            this.panel4.ResumeLayout(false);
            this.measuredParameterSelect.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion


        protected System.Windows.Forms.DataGridViewCellStyle dataGridViewColumnHeadersStyle;
        private System.Windows.Forms.Button buttonPrevElement;
        private System.Windows.Forms.Button buttonNextElement;
        private System.Windows.Forms.Button buttonNextPoint;
        private System.Windows.Forms.Button buttonPrevPoint;
        private System.Windows.Forms.DataGridView measureResultDataGrid;
        private System.Windows.Forms.RichTextBox richTextBox1;
        private System.Windows.Forms.Panel selectDevicePanel;
        private System.Windows.Forms.Button deviceControlButton;
        private System.Windows.Forms.Button startMeasureButton;
        private System.Windows.Forms.ComboBox availableDevices;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.ComboBox comboBox3;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Panel panel2;
        private System.Windows.Forms.Label resultField;
        private System.Windows.Forms.Label deviceInfo;
        private System.Windows.Forms.Label labelPointNumber;
        private System.Windows.Forms.GroupBox voltagesGroupBox;
        private System.Windows.Forms.RadioButton v1000_RadioButton;
        private System.Windows.Forms.RadioButton v500_RadioButton;
        private System.Windows.Forms.RadioButton v100_RadioButton;
        private System.Windows.Forms.RadioButton v10_RadioButton;
        private System.Windows.Forms.Panel panelMeasurePointControl;
        private System.Windows.Forms.Panel panel3;
        private System.Windows.Forms.Panel panel4;
        private System.Windows.Forms.Panel panel5;
        private System.Windows.Forms.Panel panel6;
        private System.Windows.Forms.Panel testParamsControlPanel;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label temperatureComboBox;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.NumericUpDown cableLengthNumericUpDown;
        private System.Windows.Forms.ComboBox cableStructureCB;
        private System.Windows.Forms.GroupBox deviceParametersGroupBox;
        private System.Windows.Forms.Label depolTimeLbl;
        private System.Windows.Forms.NumericUpDown afterMeasureDelayUpDown;
        private System.Windows.Forms.ComboBox averagingCounter;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label polarDelayLbl;
        private System.Windows.Forms.NumericUpDown beforeMeasureDelayUpDown;
        private System.Windows.Forms.ComboBox cableComboBox;
        private System.Windows.Forms.DataGridViewTextBoxColumn ElementNumber;
        private System.Windows.Forms.DataGridViewTextBoxColumn SubElement_1;
        private System.Windows.Forms.DataGridViewTextBoxColumn SubElement_2;
        private System.Windows.Forms.DataGridViewTextBoxColumn SubElement_3;
        private System.Windows.Forms.DataGridViewTextBoxColumn SubElement_4;
        private System.Windows.Forms.DataGridViewTextBoxColumn IsMeasuredFlag;
        private System.Windows.Forms.Panel measuredParameterSelect;
        private System.Windows.Forms.ComboBox measuredParameterCB;
        private System.Windows.Forms.NumericUpDown temperatureValue;
        private System.Windows.Forms.Label measureTimerLabel;
        private System.Windows.Forms.Label normaLabel;
        private System.Windows.Forms.ComboBox rIsolTypeSelectorCB;
        private System.Windows.Forms.TabControl measuredParameterDataTabs;
        private System.Windows.Forms.TabPage tabPage1;
        private System.Windows.Forms.TabPage tabPage2;
        private System.Windows.Forms.Panel panelResultMeasure;
        private System.Windows.Forms.Label lblNoMeasureData;
        private System.Windows.Forms.Panel testDraftControlPanel;
        private System.Windows.Forms.Button resetTestButton;
        private System.Windows.Forms.Button saveResultButton;
    }
}