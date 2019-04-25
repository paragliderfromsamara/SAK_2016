namespace NormaMeasure.MeasureControl.SACMeasureForms
{
    partial class SACCableTestForm
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
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.operatorsList = new System.Windows.Forms.ComboBox();
            this.label3 = new System.Windows.Forms.Label();
            this.temperature_NumericUpDown = new System.Windows.Forms.NumericUpDown();
            this.label4 = new System.Windows.Forms.Label();
            this.cableForTest_CB = new System.Windows.Forms.ComboBox();
            this.label5 = new System.Windows.Forms.Label();
            this.connectedFromTableElement_ComboBox = new System.Windows.Forms.ComboBox();
            this.label6 = new System.Windows.Forms.Label();
            this.connectionType = new System.Windows.Forms.GroupBox();
            this.mergedTable_RadioBatton = new System.Windows.Forms.RadioButton();
            this.splittedTable_RadioBatton = new System.Windows.Forms.RadioButton();
            this.barabanTypes_CB = new System.Windows.Forms.ComboBox();
            this.label7 = new System.Windows.Forms.Label();
            this.barabanSerial_TextBox = new System.Windows.Forms.TextBox();
            this.label8 = new System.Windows.Forms.Label();
            this.cableLength_NumericUpDown = new System.Windows.Forms.NumericUpDown();
            this.label9 = new System.Windows.Forms.Label();
            this.RizolSelector_CB = new System.Windows.Forms.ComboBox();
            this.label10 = new System.Windows.Forms.Label();
            this.useTemperatureSensor_CheckBox = new System.Windows.Forms.CheckBox();
            this.testProgram_GroupBox = new System.Windows.Forms.GroupBox();
            this.CableTestFormDataSet = new System.Data.DataSet();
            this.panel1 = new System.Windows.Forms.Panel();
            this.measureResultField = new System.Windows.Forms.Label();
            this.resetCurrentTest_Button = new System.Windows.Forms.Button();
            this.CurrentElement_Label = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.temperature_NumericUpDown)).BeginInit();
            this.connectionType.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.cableLength_NumericUpDown)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.CableTestFormDataSet)).BeginInit();
            this.panel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // measureControlButton
            // 
            this.measureControlButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.measureControlButton.Location = new System.Drawing.Point(12, 410);
            this.measureControlButton.Name = "measureControlButton";
            this.measureControlButton.Size = new System.Drawing.Size(112, 28);
            this.measureControlButton.TabIndex = 0;
            this.measureControlButton.Text = "СТАРТ";
            this.measureControlButton.UseVisualStyleBackColor = true;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(520, 11);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(35, 13);
            this.label1.TabIndex = 2;
            this.label1.Text = "label1";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(465, 11);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(35, 13);
            this.label2.TabIndex = 3;
            this.label2.Text = "label2";
            // 
            // operatorsList
            // 
            this.operatorsList.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.operatorsList.FormattingEnabled = true;
            this.operatorsList.Location = new System.Drawing.Point(12, 30);
            this.operatorsList.Name = "operatorsList";
            this.operatorsList.Size = new System.Drawing.Size(142, 21);
            this.operatorsList.TabIndex = 4;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(9, 14);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(56, 13);
            this.label3.TabIndex = 5;
            this.label3.Text = "Оператор";
            // 
            // temperature_NumericUpDown
            // 
            this.temperature_NumericUpDown.Location = new System.Drawing.Point(181, 30);
            this.temperature_NumericUpDown.Maximum = new decimal(new int[] {
            35,
            0,
            0,
            0});
            this.temperature_NumericUpDown.Minimum = new decimal(new int[] {
            5,
            0,
            0,
            0});
            this.temperature_NumericUpDown.Name = "temperature_NumericUpDown";
            this.temperature_NumericUpDown.Size = new System.Drawing.Size(120, 20);
            this.temperature_NumericUpDown.TabIndex = 6;
            this.temperature_NumericUpDown.Value = new decimal(new int[] {
            20,
            0,
            0,
            0});
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(178, 14);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(91, 13);
            this.label4.TabIndex = 7;
            this.label4.Text = "Температура, °С";
            // 
            // cableForTest_CB
            // 
            this.cableForTest_CB.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cableForTest_CB.FormattingEnabled = true;
            this.cableForTest_CB.Location = new System.Drawing.Point(12, 75);
            this.cableForTest_CB.Name = "cableForTest_CB";
            this.cableForTest_CB.Size = new System.Drawing.Size(289, 21);
            this.cableForTest_CB.TabIndex = 8;
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(9, 59);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(44, 13);
            this.label5.TabIndex = 9;
            this.label5.Text = "Кабель";
            // 
            // connectedFromTableElement_ComboBox
            // 
            this.connectedFromTableElement_ComboBox.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.connectedFromTableElement_ComboBox.FormattingEnabled = true;
            this.connectedFromTableElement_ComboBox.Location = new System.Drawing.Point(321, 75);
            this.connectedFromTableElement_ComboBox.Name = "connectedFromTableElement_ComboBox";
            this.connectedFromTableElement_ComboBox.Size = new System.Drawing.Size(89, 21);
            this.connectedFromTableElement_ComboBox.TabIndex = 10;
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(318, 59);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(92, 13);
            this.label6.TabIndex = 11;
            this.label6.Text = "Подключен с ПУ";
            // 
            // connectionType
            // 
            this.connectionType.Controls.Add(this.mergedTable_RadioBatton);
            this.connectionType.Controls.Add(this.splittedTable_RadioBatton);
            this.connectionType.Location = new System.Drawing.Point(433, 58);
            this.connectionType.Name = "connectionType";
            this.connectionType.Size = new System.Drawing.Size(146, 47);
            this.connectionType.TabIndex = 12;
            this.connectionType.TabStop = false;
            this.connectionType.Text = "Способ подключения";
            // 
            // mergedTable_RadioBatton
            // 
            this.mergedTable_RadioBatton.AutoSize = true;
            this.mergedTable_RadioBatton.Location = new System.Drawing.Point(75, 19);
            this.mergedTable_RadioBatton.Name = "mergedTable_RadioBatton";
            this.mergedTable_RadioBatton.Size = new System.Drawing.Size(62, 17);
            this.mergedTable_RadioBatton.TabIndex = 13;
            this.mergedTable_RadioBatton.Text = "без ДК";
            this.mergedTable_RadioBatton.UseVisualStyleBackColor = true;
            this.mergedTable_RadioBatton.CheckedChanged += new System.EventHandler(this.tableMode_RadioBatton_CheckedChanged);
            // 
            // splittedTable_RadioBatton
            // 
            this.splittedTable_RadioBatton.AutoSize = true;
            this.splittedTable_RadioBatton.Checked = true;
            this.splittedTable_RadioBatton.Location = new System.Drawing.Point(8, 19);
            this.splittedTable_RadioBatton.Name = "splittedTable_RadioBatton";
            this.splittedTable_RadioBatton.Size = new System.Drawing.Size(50, 17);
            this.splittedTable_RadioBatton.TabIndex = 0;
            this.splittedTable_RadioBatton.TabStop = true;
            this.splittedTable_RadioBatton.Text = "с ДК";
            this.splittedTable_RadioBatton.UseVisualStyleBackColor = true;
            this.splittedTable_RadioBatton.CheckedChanged += new System.EventHandler(this.tableMode_RadioBatton_CheckedChanged);
            // 
            // barabanTypes_CB
            // 
            this.barabanTypes_CB.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.barabanTypes_CB.FormattingEnabled = true;
            this.barabanTypes_CB.Location = new System.Drawing.Point(12, 132);
            this.barabanTypes_CB.Name = "barabanTypes_CB";
            this.barabanTypes_CB.Size = new System.Drawing.Size(142, 21);
            this.barabanTypes_CB.TabIndex = 13;
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(9, 116);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(50, 13);
            this.label7.TabIndex = 14;
            this.label7.Text = "Барабан";
            // 
            // barabanSerial_TextBox
            // 
            this.barabanSerial_TextBox.Location = new System.Drawing.Point(169, 132);
            this.barabanSerial_TextBox.Name = "barabanSerial_TextBox";
            this.barabanSerial_TextBox.Size = new System.Drawing.Size(132, 20);
            this.barabanSerial_TextBox.TabIndex = 15;
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Location = new System.Drawing.Point(166, 116);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(92, 13);
            this.label8.TabIndex = 16;
            this.label8.Text = "Номер барабана";
            // 
            // cableLength_NumericUpDown
            // 
            this.cableLength_NumericUpDown.Location = new System.Drawing.Point(321, 132);
            this.cableLength_NumericUpDown.Maximum = new decimal(new int[] {
            3000,
            0,
            0,
            0});
            this.cableLength_NumericUpDown.Minimum = new decimal(new int[] {
            50,
            0,
            0,
            0});
            this.cableLength_NumericUpDown.Name = "cableLength_NumericUpDown";
            this.cableLength_NumericUpDown.Size = new System.Drawing.Size(89, 20);
            this.cableLength_NumericUpDown.TabIndex = 17;
            this.cableLength_NumericUpDown.Value = new decimal(new int[] {
            1000,
            0,
            0,
            0});
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.Location = new System.Drawing.Point(318, 116);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(93, 13);
            this.label9.TabIndex = 18;
            this.label9.Text = "Длина кабеля, м";
            // 
            // RizolSelector_CB
            // 
            this.RizolSelector_CB.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.RizolSelector_CB.Enabled = false;
            this.RizolSelector_CB.FormattingEnabled = true;
            this.RizolSelector_CB.Location = new System.Drawing.Point(433, 131);
            this.RizolSelector_CB.Name = "RizolSelector_CB";
            this.RizolSelector_CB.Size = new System.Drawing.Size(146, 21);
            this.RizolSelector_CB.TabIndex = 19;
            // 
            // label10
            // 
            this.label10.AutoSize = true;
            this.label10.Location = new System.Drawing.Point(430, 116);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(138, 13);
            this.label10.TabIndex = 20;
            this.label10.Text = "Способ измерения Rизол";
            // 
            // useTemperatureSensor_CheckBox
            // 
            this.useTemperatureSensor_CheckBox.AutoSize = true;
            this.useTemperatureSensor_CheckBox.Location = new System.Drawing.Point(321, 30);
            this.useTemperatureSensor_CheckBox.Name = "useTemperatureSensor_CheckBox";
            this.useTemperatureSensor_CheckBox.Size = new System.Drawing.Size(167, 17);
            this.useTemperatureSensor_CheckBox.TabIndex = 21;
            this.useTemperatureSensor_CheckBox.Text = "Использовать термодатчик";
            this.useTemperatureSensor_CheckBox.UseVisualStyleBackColor = true;
            // 
            // testProgram_GroupBox
            // 
            this.testProgram_GroupBox.BackColor = System.Drawing.SystemColors.Control;
            this.testProgram_GroupBox.ForeColor = System.Drawing.Color.Navy;
            this.testProgram_GroupBox.Location = new System.Drawing.Point(12, 170);
            this.testProgram_GroupBox.Name = "testProgram_GroupBox";
            this.testProgram_GroupBox.Size = new System.Drawing.Size(567, 18);
            this.testProgram_GroupBox.TabIndex = 22;
            this.testProgram_GroupBox.TabStop = false;
            this.testProgram_GroupBox.Text = "Программа испытаний";
            // 
            // CableTestFormDataSet
            // 
            this.CableTestFormDataSet.DataSetName = "NewDataSet";
            // 
            // panel1
            // 
            this.panel1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.panel1.BackColor = System.Drawing.Color.MintCream;
            this.panel1.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Center;
            this.panel1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.panel1.Controls.Add(this.CurrentElement_Label);
            this.panel1.Controls.Add(this.measureResultField);
            this.panel1.Controls.Add(this.label2);
            this.panel1.Controls.Add(this.label1);
            this.panel1.Location = new System.Drawing.Point(12, 194);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(567, 210);
            this.panel1.TabIndex = 23;
            // 
            // measureResultField
            // 
            this.measureResultField.AutoSize = true;
            this.measureResultField.Font = new System.Drawing.Font("Tahoma", 36F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.measureResultField.Location = new System.Drawing.Point(146, 71);
            this.measureResultField.Name = "measureResultField";
            this.measureResultField.Size = new System.Drawing.Size(170, 58);
            this.measureResultField.TabIndex = 4;
            this.measureResultField.Text = "100.00";
            // 
            // resetCurrentTest_Button
            // 
            this.resetCurrentTest_Button.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.resetCurrentTest_Button.ImageAlign = System.Drawing.ContentAlignment.BottomLeft;
            this.resetCurrentTest_Button.Location = new System.Drawing.Point(138, 410);
            this.resetCurrentTest_Button.Name = "resetCurrentTest_Button";
            this.resetCurrentTest_Button.Size = new System.Drawing.Size(92, 28);
            this.resetCurrentTest_Button.TabIndex = 24;
            this.resetCurrentTest_Button.Text = "ОТКАТИТЬ";
            this.resetCurrentTest_Button.UseVisualStyleBackColor = true;
            this.resetCurrentTest_Button.Click += new System.EventHandler(this.resetCurrentTest_Button_Click);
            // 
            // CurrentElement_Label
            // 
            this.CurrentElement_Label.AutoSize = true;
            this.CurrentElement_Label.Font = new System.Drawing.Font("Tahoma", 15.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.CurrentElement_Label.Location = new System.Drawing.Point(11, 11);
            this.CurrentElement_Label.Name = "CurrentElement_Label";
            this.CurrentElement_Label.Size = new System.Drawing.Size(153, 25);
            this.CurrentElement_Label.TabIndex = 5;
            this.CurrentElement_Label.Text = "пара 1 жила A";
            // 
            // SACCableTestForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(590, 450);
            this.Controls.Add(this.resetCurrentTest_Button);
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.testProgram_GroupBox);
            this.Controls.Add(this.measureControlButton);
            this.Controls.Add(this.useTemperatureSensor_CheckBox);
            this.Controls.Add(this.barabanTypes_CB);
            this.Controls.Add(this.label10);
            this.Controls.Add(this.operatorsList);
            this.Controls.Add(this.RizolSelector_CB);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label9);
            this.Controls.Add(this.temperature_NumericUpDown);
            this.Controls.Add(this.cableLength_NumericUpDown);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.label8);
            this.Controls.Add(this.cableForTest_CB);
            this.Controls.Add(this.barabanSerial_TextBox);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.label7);
            this.Controls.Add(this.connectedFromTableElement_ComboBox);
            this.Controls.Add(this.label6);
            this.Controls.Add(this.connectionType);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "SACCableTestForm";
            this.Text = "Испытание кабеля";
            this.TransparencyKey = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(64)))));
            ((System.ComponentModel.ISupportInitialize)(this.temperature_NumericUpDown)).EndInit();
            this.connectionType.ResumeLayout(false);
            this.connectionType.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.cableLength_NumericUpDown)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.CableTestFormDataSet)).EndInit();
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button measureControlButton;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.ComboBox operatorsList;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.NumericUpDown temperature_NumericUpDown;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.ComboBox cableForTest_CB;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.ComboBox connectedFromTableElement_ComboBox;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.GroupBox connectionType;
        private System.Windows.Forms.RadioButton mergedTable_RadioBatton;
        private System.Windows.Forms.RadioButton splittedTable_RadioBatton;
        private System.Windows.Forms.ComboBox barabanTypes_CB;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.TextBox barabanSerial_TextBox;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.NumericUpDown cableLength_NumericUpDown;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.ComboBox RizolSelector_CB;
        private System.Windows.Forms.Label label10;
        private System.Windows.Forms.CheckBox useTemperatureSensor_CheckBox;
        private System.Windows.Forms.GroupBox testProgram_GroupBox;
        private System.Data.DataSet CableTestFormDataSet;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Button resetCurrentTest_Button;
        private System.Windows.Forms.Label measureResultField;
        private System.Windows.Forms.Label CurrentElement_Label;
    }
}