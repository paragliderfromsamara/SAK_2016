namespace NormaMeasure.MeasureControl.SACMeasureForms
{
    partial class SACHandMeasureForm
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
            this.measureControlButton = new System.Windows.Forms.Button();
            this.ParameterTypes_CB = new System.Windows.Forms.ComboBox();
            this.label1 = new System.Windows.Forms.Label();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.NoDK_RadioButton = new System.Windows.Forms.RadioButton();
            this.withDK_RadioButton = new System.Windows.Forms.RadioButton();
            this.Etalon_RadioButton = new System.Windows.Forms.RadioButton();
            this.waveResistance_CB = new System.Windows.Forms.ComboBox();
            this.label2 = new System.Windows.Forms.Label();
            this.pair1_Label = new System.Windows.Forms.Label();
            this.leadCB = new System.Windows.Forms.ComboBox();
            this.pair2OrLead_Label = new System.Windows.Forms.Label();
            this.freqMin_CB = new System.Windows.Forms.ComboBox();
            this.freqStep_CB = new System.Windows.Forms.ComboBox();
            this.freqMax_CB = new System.Windows.Forms.ComboBox();
            this.freqParameters_Panel = new System.Windows.Forms.Panel();
            this.label4 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.freqMin_Label = new System.Windows.Forms.Label();
            this.tableElementsPanel = new System.Windows.Forms.Panel();
            this.pairSelector_ComboBox_1 = new NormaMeasure.MeasureControl.SACMeasureForms.PairSelector_ComboBox();
            this.measureResultPanel_Container = new System.Windows.Forms.Panel();
            this.measureCycles_NumericUpDown = new System.Windows.Forms.NumericUpDown();
            this.label5 = new System.Windows.Forms.Label();
            this.groupBox1.SuspendLayout();
            this.freqParameters_Panel.SuspendLayout();
            this.tableElementsPanel.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.measureCycles_NumericUpDown)).BeginInit();
            this.SuspendLayout();
            // 
            // measureControlButton
            // 
            this.measureControlButton.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.measureControlButton.Location = new System.Drawing.Point(235, 248);
            this.measureControlButton.Name = "measureControlButton";
            this.measureControlButton.Size = new System.Drawing.Size(453, 42);
            this.measureControlButton.TabIndex = 0;
            this.measureControlButton.Text = "СТАРТ";
            this.measureControlButton.UseVisualStyleBackColor = true;
            // 
            // ParameterTypes_CB
            // 
            this.ParameterTypes_CB.FormattingEnabled = true;
            this.ParameterTypes_CB.Location = new System.Drawing.Point(12, 30);
            this.ParameterTypes_CB.Name = "ParameterTypes_CB";
            this.ParameterTypes_CB.Size = new System.Drawing.Size(206, 21);
            this.ParameterTypes_CB.TabIndex = 2;
            this.ParameterTypes_CB.SelectedIndexChanged += new System.EventHandler(this.ParameterTypes_CB_SelectedIndexChanged);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(9, 14);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(58, 13);
            this.label1.TabIndex = 3;
            this.label1.Text = "Параметр";
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.NoDK_RadioButton);
            this.groupBox1.Controls.Add(this.withDK_RadioButton);
            this.groupBox1.Controls.Add(this.Etalon_RadioButton);
            this.groupBox1.Location = new System.Drawing.Point(12, 70);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(206, 53);
            this.groupBox1.TabIndex = 4;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Тип коммутации";
            // 
            // NoDK_RadioButton
            // 
            this.NoDK_RadioButton.AutoSize = true;
            this.NoDK_RadioButton.Location = new System.Drawing.Point(134, 23);
            this.NoDK_RadioButton.Name = "NoDK_RadioButton";
            this.NoDK_RadioButton.Size = new System.Drawing.Size(62, 17);
            this.NoDK_RadioButton.TabIndex = 5;
            this.NoDK_RadioButton.TabStop = true;
            this.NoDK_RadioButton.Text = "без ДК";
            this.NoDK_RadioButton.UseVisualStyleBackColor = true;
            this.NoDK_RadioButton.CheckedChanged += new System.EventHandler(this.NoDK_RadioButton_CheckedChanged);
            // 
            // withDK_RadioButton
            // 
            this.withDK_RadioButton.AutoSize = true;
            this.withDK_RadioButton.Location = new System.Drawing.Point(78, 23);
            this.withDK_RadioButton.Name = "withDK_RadioButton";
            this.withDK_RadioButton.Size = new System.Drawing.Size(50, 17);
            this.withDK_RadioButton.TabIndex = 5;
            this.withDK_RadioButton.TabStop = true;
            this.withDK_RadioButton.Text = "с ДК";
            this.withDK_RadioButton.UseVisualStyleBackColor = true;
            this.withDK_RadioButton.CheckedChanged += new System.EventHandler(this.withDK_RadioButton_CheckedChanged);
            // 
            // Etalon_RadioButton
            // 
            this.Etalon_RadioButton.AutoSize = true;
            this.Etalon_RadioButton.Location = new System.Drawing.Point(11, 23);
            this.Etalon_RadioButton.Name = "Etalon_RadioButton";
            this.Etalon_RadioButton.Size = new System.Drawing.Size(61, 17);
            this.Etalon_RadioButton.TabIndex = 0;
            this.Etalon_RadioButton.TabStop = true;
            this.Etalon_RadioButton.Text = "Эталон";
            this.Etalon_RadioButton.UseVisualStyleBackColor = true;
            this.Etalon_RadioButton.CheckedChanged += new System.EventHandler(this.Etalon_RadioButton_CheckedChanged);
            // 
            // waveResistance_CB
            // 
            this.waveResistance_CB.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.waveResistance_CB.FormattingEnabled = true;
            this.waveResistance_CB.Items.AddRange(new object[] {
            "150",
            "400",
            "500",
            "600"});
            this.waveResistance_CB.Location = new System.Drawing.Point(11, 74);
            this.waveResistance_CB.Name = "waveResistance_CB";
            this.waveResistance_CB.Size = new System.Drawing.Size(206, 21);
            this.waveResistance_CB.TabIndex = 5;
            this.waveResistance_CB.SelectedIndexChanged += new System.EventHandler(this.waveResistance_CB_SelectedIndexChanged);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(8, 58);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(77, 13);
            this.label2.TabIndex = 6;
            this.label2.Text = "Нагрузка, Ом";
            // 
            // pair1_Label
            // 
            this.pair1_Label.AutoSize = true;
            this.pair1_Label.Location = new System.Drawing.Point(8, 7);
            this.pair1_Label.Name = "pair1_Label";
            this.pair1_Label.Size = new System.Drawing.Size(33, 13);
            this.pair1_Label.TabIndex = 8;
            this.pair1_Label.Text = "Пара";
            // 
            // leadCB
            // 
            this.leadCB.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.leadCB.FormattingEnabled = true;
            this.leadCB.Items.AddRange(new object[] {
            "Жила А",
            "Жила Б"});
            this.leadCB.Location = new System.Drawing.Point(127, 23);
            this.leadCB.Name = "leadCB";
            this.leadCB.Size = new System.Drawing.Size(90, 21);
            this.leadCB.TabIndex = 9;
            this.leadCB.SelectedIndexChanged += new System.EventHandler(this.leadCB_SelectedIndexChanged);
            // 
            // pair2OrLead_Label
            // 
            this.pair2OrLead_Label.AutoSize = true;
            this.pair2OrLead_Label.Location = new System.Drawing.Point(124, 7);
            this.pair2OrLead_Label.Name = "pair2OrLead_Label";
            this.pair2OrLead_Label.Size = new System.Drawing.Size(36, 13);
            this.pair2OrLead_Label.TabIndex = 10;
            this.pair2OrLead_Label.Text = "Жила";
            // 
            // freqMin_CB
            // 
            this.freqMin_CB.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.freqMin_CB.FormattingEnabled = true;
            this.freqMin_CB.Location = new System.Drawing.Point(11, 23);
            this.freqMin_CB.Name = "freqMin_CB";
            this.freqMin_CB.Size = new System.Drawing.Size(63, 21);
            this.freqMin_CB.TabIndex = 11;
            this.freqMin_CB.SelectedIndexChanged += new System.EventHandler(this.freqMin_CB_SelectedIndexChanged);
            // 
            // freqStep_CB
            // 
            this.freqStep_CB.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.freqStep_CB.FormattingEnabled = true;
            this.freqStep_CB.Location = new System.Drawing.Point(83, 23);
            this.freqStep_CB.Name = "freqStep_CB";
            this.freqStep_CB.Size = new System.Drawing.Size(63, 21);
            this.freqStep_CB.TabIndex = 12;
            this.freqStep_CB.SelectedIndexChanged += new System.EventHandler(this.freqStep_CB_SelectedIndexChanged);
            // 
            // freqMax_CB
            // 
            this.freqMax_CB.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.freqMax_CB.FormattingEnabled = true;
            this.freqMax_CB.Location = new System.Drawing.Point(154, 23);
            this.freqMax_CB.Name = "freqMax_CB";
            this.freqMax_CB.Size = new System.Drawing.Size(63, 21);
            this.freqMax_CB.TabIndex = 13;
            this.freqMax_CB.SelectedIndexChanged += new System.EventHandler(this.freqMax_CB_SelectedIndexChanged);
            // 
            // freqParameters_Panel
            // 
            this.freqParameters_Panel.Controls.Add(this.label4);
            this.freqParameters_Panel.Controls.Add(this.label3);
            this.freqParameters_Panel.Controls.Add(this.freqMin_Label);
            this.freqParameters_Panel.Controls.Add(this.freqMin_CB);
            this.freqParameters_Panel.Controls.Add(this.freqMax_CB);
            this.freqParameters_Panel.Controls.Add(this.label2);
            this.freqParameters_Panel.Controls.Add(this.freqStep_CB);
            this.freqParameters_Panel.Controls.Add(this.waveResistance_CB);
            this.freqParameters_Panel.Location = new System.Drawing.Point(1, 189);
            this.freqParameters_Panel.Name = "freqParameters_Panel";
            this.freqParameters_Panel.Size = new System.Drawing.Size(228, 100);
            this.freqParameters_Panel.TabIndex = 14;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(151, 4);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(53, 13);
            this.label4.TabIndex = 15;
            this.label4.Text = "fmax, кГц";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(80, 4);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(53, 13);
            this.label3.TabIndex = 15;
            this.label3.Text = "fшаг, кГц";
            // 
            // freqMin_Label
            // 
            this.freqMin_Label.AutoSize = true;
            this.freqMin_Label.Location = new System.Drawing.Point(8, 4);
            this.freqMin_Label.Name = "freqMin_Label";
            this.freqMin_Label.Size = new System.Drawing.Size(50, 13);
            this.freqMin_Label.TabIndex = 15;
            this.freqMin_Label.Text = "fmin, кГц";
            // 
            // tableElementsPanel
            // 
            this.tableElementsPanel.Controls.Add(this.pairSelector_ComboBox_1);
            this.tableElementsPanel.Controls.Add(this.leadCB);
            this.tableElementsPanel.Controls.Add(this.pair2OrLead_Label);
            this.tableElementsPanel.Controls.Add(this.pair1_Label);
            this.tableElementsPanel.Location = new System.Drawing.Point(1, 129);
            this.tableElementsPanel.Name = "tableElementsPanel";
            this.tableElementsPanel.Size = new System.Drawing.Size(228, 54);
            this.tableElementsPanel.TabIndex = 16;
            // 
            // pairSelector_ComboBox_1
            // 
            this.pairSelector_ComboBox_1.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.pairSelector_ComboBox_1.FormattingEnabled = true;
            this.pairSelector_ComboBox_1.Items.AddRange(new object[] {
            "1",
            "2",
            "3",
            "4",
            "5",
            "6",
            "7",
            "8",
            "9",
            "10",
            "11",
            "12",
            "13",
            "14",
            "15",
            "16",
            "17",
            "18",
            "19",
            "20",
            "21",
            "22",
            "23",
            "24",
            "25",
            "26",
            "27",
            "28",
            "29",
            "30",
            "31",
            "32",
            "33",
            "34",
            "35",
            "36",
            "37",
            "38",
            "39",
            "40",
            "41",
            "42",
            "43",
            "44",
            "45",
            "46",
            "47",
            "48",
            "49",
            "50",
            "51",
            "52",
            "53",
            "54",
            "55",
            "56",
            "57",
            "58",
            "59",
            "60",
            "61",
            "62",
            "63",
            "64",
            "65",
            "66",
            "67",
            "68",
            "69",
            "70",
            "71",
            "72",
            "73",
            "74",
            "75",
            "76",
            "77",
            "78",
            "79",
            "80",
            "81",
            "82",
            "83",
            "84",
            "85",
            "86",
            "87",
            "88",
            "89",
            "90",
            "91",
            "92",
            "93",
            "94",
            "95",
            "96",
            "97",
            "98",
            "99",
            "100",
            "101",
            "102",
            "103",
            "104",
            "1",
            "2",
            "3",
            "4",
            "5",
            "6",
            "7",
            "8",
            "9",
            "10",
            "11",
            "12",
            "13",
            "14",
            "15",
            "16",
            "17",
            "18",
            "19",
            "20",
            "21",
            "22",
            "23",
            "24",
            "25",
            "26",
            "27",
            "28",
            "29",
            "30",
            "31",
            "32",
            "33",
            "34",
            "35",
            "36",
            "37",
            "38",
            "39",
            "40",
            "41",
            "42",
            "43",
            "44",
            "45",
            "46",
            "47",
            "48",
            "49",
            "50",
            "51",
            "52",
            "53",
            "54",
            "55",
            "56",
            "57",
            "58",
            "59",
            "60",
            "61",
            "62",
            "63",
            "64",
            "65",
            "66",
            "67",
            "68",
            "69",
            "70",
            "71",
            "72",
            "73",
            "74",
            "75",
            "76",
            "77",
            "78",
            "79",
            "80",
            "81",
            "82",
            "83",
            "84",
            "85",
            "86",
            "87",
            "88",
            "89",
            "90",
            "91",
            "92",
            "93",
            "94",
            "95",
            "96",
            "97",
            "98",
            "99",
            "100",
            "101",
            "102",
            "103",
            "104",
            "1",
            "2",
            "3",
            "4",
            "5",
            "6",
            "7",
            "8",
            "9",
            "10",
            "11",
            "12",
            "13",
            "14",
            "15",
            "16",
            "17",
            "18",
            "19",
            "20",
            "21",
            "22",
            "23",
            "24",
            "25",
            "26",
            "27",
            "28",
            "29",
            "30",
            "31",
            "32",
            "33",
            "34",
            "35",
            "36",
            "37",
            "38",
            "39",
            "40",
            "41",
            "42",
            "43",
            "44",
            "45",
            "46",
            "47",
            "48",
            "49",
            "50",
            "51",
            "52",
            "53",
            "54",
            "55",
            "56",
            "57",
            "58",
            "59",
            "60",
            "61",
            "62",
            "63",
            "64",
            "65",
            "66",
            "67",
            "68",
            "69",
            "70",
            "71",
            "72",
            "73",
            "74",
            "75",
            "76",
            "77",
            "78",
            "79",
            "80",
            "81",
            "82",
            "83",
            "84",
            "85",
            "86",
            "87",
            "88",
            "89",
            "90",
            "91",
            "92",
            "93",
            "94",
            "95",
            "96",
            "97",
            "98",
            "99",
            "100",
            "101",
            "102",
            "103",
            "104",
            "1",
            "2",
            "3",
            "4",
            "5",
            "6",
            "7",
            "8",
            "9",
            "10",
            "11",
            "12",
            "13",
            "14",
            "15",
            "16",
            "17",
            "18",
            "19",
            "20",
            "21",
            "22",
            "23",
            "24",
            "25",
            "26",
            "27",
            "28",
            "29",
            "30",
            "31",
            "32",
            "33",
            "34",
            "35",
            "36",
            "37",
            "38",
            "39",
            "40",
            "41",
            "42",
            "43",
            "44",
            "45",
            "46",
            "47",
            "48",
            "49",
            "50",
            "51",
            "52",
            "53",
            "54",
            "55",
            "56",
            "57",
            "58",
            "59",
            "60",
            "61",
            "62",
            "63",
            "64",
            "65",
            "66",
            "67",
            "68",
            "69",
            "70",
            "71",
            "72",
            "73",
            "74",
            "75",
            "76",
            "77",
            "78",
            "79",
            "80",
            "81",
            "82",
            "83",
            "84",
            "85",
            "86",
            "87",
            "88",
            "89",
            "90",
            "91",
            "92",
            "93",
            "94",
            "95",
            "96",
            "97",
            "98",
            "99",
            "100",
            "101",
            "102",
            "103",
            "104",
            "1",
            "2",
            "3",
            "4",
            "5",
            "6",
            "7",
            "8",
            "9",
            "10",
            "11",
            "12",
            "13",
            "14",
            "15",
            "16",
            "17",
            "18",
            "19",
            "20",
            "21",
            "22",
            "23",
            "24",
            "25",
            "26",
            "27",
            "28",
            "29",
            "30",
            "31",
            "32",
            "33",
            "34",
            "35",
            "36",
            "37",
            "38",
            "39",
            "40",
            "41",
            "42",
            "43",
            "44",
            "45",
            "46",
            "47",
            "48",
            "49",
            "50",
            "51",
            "52",
            "53",
            "54",
            "55",
            "56",
            "57",
            "58",
            "59",
            "60",
            "61",
            "62",
            "63",
            "64",
            "65",
            "66",
            "67",
            "68",
            "69",
            "70",
            "71",
            "72",
            "73",
            "74",
            "75",
            "76",
            "77",
            "78",
            "79",
            "80",
            "81",
            "82",
            "83",
            "84",
            "85",
            "86",
            "87",
            "88",
            "89",
            "90",
            "91",
            "92",
            "93",
            "94",
            "95",
            "96",
            "97",
            "98",
            "99",
            "100",
            "101",
            "102",
            "103",
            "104",
            "1",
            "2",
            "3",
            "4",
            "5",
            "6",
            "7",
            "8",
            "9",
            "10",
            "11",
            "12",
            "13",
            "14",
            "15",
            "16",
            "17",
            "18",
            "19",
            "20",
            "21",
            "22",
            "23",
            "24",
            "25",
            "26",
            "27",
            "28",
            "29",
            "30",
            "31",
            "32",
            "33",
            "34",
            "35",
            "36",
            "37",
            "38",
            "39",
            "40",
            "41",
            "42",
            "43",
            "44",
            "45",
            "46",
            "47",
            "48",
            "49",
            "50",
            "51",
            "52",
            "53",
            "54",
            "55",
            "56",
            "57",
            "58",
            "59",
            "60",
            "61",
            "62",
            "63",
            "64",
            "65",
            "66",
            "67",
            "68",
            "69",
            "70",
            "71",
            "72",
            "73",
            "74",
            "75",
            "76",
            "77",
            "78",
            "79",
            "80",
            "81",
            "82",
            "83",
            "84",
            "85",
            "86",
            "87",
            "88",
            "89",
            "90",
            "91",
            "92",
            "93",
            "94",
            "95",
            "96",
            "97",
            "98",
            "99",
            "100",
            "101",
            "102",
            "103",
            "104"});
            this.pairSelector_ComboBox_1.Location = new System.Drawing.Point(11, 23);
            this.pairSelector_ComboBox_1.Name = "pairSelector_ComboBox_1";
            this.pairSelector_ComboBox_1.Size = new System.Drawing.Size(74, 21);
            this.pairSelector_ComboBox_1.TabIndex = 11;
            this.pairSelector_ComboBox_1.SelectedIndexChanged += new System.EventHandler(this.pairSelector_ComboBox_1_SelectedIndexChanged);
            // 
            // measureResultPanel_Container
            // 
            this.measureResultPanel_Container.Location = new System.Drawing.Point(235, 30);
            this.measureResultPanel_Container.Name = "measureResultPanel_Container";
            this.measureResultPanel_Container.Size = new System.Drawing.Size(453, 165);
            this.measureResultPanel_Container.TabIndex = 17;
            // 
            // measureCycles_NumericUpDown
            // 
            this.measureCycles_NumericUpDown.Location = new System.Drawing.Point(612, 222);
            this.measureCycles_NumericUpDown.Maximum = new decimal(new int[] {
            500,
            0,
            0,
            0});
            this.measureCycles_NumericUpDown.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.measureCycles_NumericUpDown.Name = "measureCycles_NumericUpDown";
            this.measureCycles_NumericUpDown.Size = new System.Drawing.Size(76, 20);
            this.measureCycles_NumericUpDown.TabIndex = 18;
            this.measureCycles_NumericUpDown.Value = new decimal(new int[] {
            20,
            0,
            0,
            0});
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(609, 206);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(65, 13);
            this.label5.TabIndex = 19;
            this.label5.Text = "Измерений";
            // 
            // SACHandMeasureForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(700, 305);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.measureCycles_NumericUpDown);
            this.Controls.Add(this.measureResultPanel_Container);
            this.Controls.Add(this.tableElementsPanel);
            this.Controls.Add(this.freqParameters_Panel);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.ParameterTypes_CB);
            this.Controls.Add(this.measureControlButton);
            this.Name = "SACHandMeasureForm";
            this.Text = "SACHandMeasureForm";
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.freqParameters_Panel.ResumeLayout(false);
            this.freqParameters_Panel.PerformLayout();
            this.tableElementsPanel.ResumeLayout(false);
            this.tableElementsPanel.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.measureCycles_NumericUpDown)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button measureControlButton;
        private System.Windows.Forms.ComboBox ParameterTypes_CB;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.RadioButton NoDK_RadioButton;
        private System.Windows.Forms.RadioButton withDK_RadioButton;
        private System.Windows.Forms.RadioButton Etalon_RadioButton;
        private System.Windows.Forms.ComboBox waveResistance_CB;
        private System.Windows.Forms.Label label2;
        
        private System.Windows.Forms.Label pair1_Label;
        private System.Windows.Forms.ComboBox leadCB;
        private System.Windows.Forms.Label pair2OrLead_Label;
        private System.Windows.Forms.ComboBox freqMin_CB;
        private System.Windows.Forms.ComboBox freqStep_CB;
        private System.Windows.Forms.ComboBox freqMax_CB;
        private System.Windows.Forms.Panel freqParameters_Panel;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label freqMin_Label;
        private System.Windows.Forms.Panel tableElementsPanel;
        private System.Windows.Forms.Panel measureResultPanel_Container;
        private System.Windows.Forms.NumericUpDown measureCycles_NumericUpDown;
        private System.Windows.Forms.Label label5;
        private PairSelector_ComboBox pairSelector_ComboBox_1;
    }
}