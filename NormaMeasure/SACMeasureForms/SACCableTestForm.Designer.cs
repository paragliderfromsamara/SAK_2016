﻿namespace NormaMeasure.MeasureControl.SACMeasureForms
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
            this.numericUpDown1 = new System.Windows.Forms.NumericUpDown();
            this.label4 = new System.Windows.Forms.Label();
            this.cableForTest_CB = new System.Windows.Forms.ComboBox();
            this.label5 = new System.Windows.Forms.Label();
            this.connectedFromTableElement_ComboBox = new System.Windows.Forms.ComboBox();
            this.label6 = new System.Windows.Forms.Label();
            this.connectionType = new System.Windows.Forms.GroupBox();
            this.doubleTable_RadioBatton = new System.Windows.Forms.RadioButton();
            this.singleTable_RadioBatton = new System.Windows.Forms.RadioButton();
            this.barabanTypes_CB = new System.Windows.Forms.ComboBox();
            this.label7 = new System.Windows.Forms.Label();
            this.textBox1 = new System.Windows.Forms.TextBox();
            this.label8 = new System.Windows.Forms.Label();
            this.cableLength_NumericUpDown = new System.Windows.Forms.NumericUpDown();
            this.label9 = new System.Windows.Forms.Label();
            this.RizolSelector_CB = new System.Windows.Forms.ComboBox();
            this.label10 = new System.Windows.Forms.Label();
            this.checkBox1 = new System.Windows.Forms.CheckBox();
            this.testProgram_GroupBox = new System.Windows.Forms.GroupBox();
            this.CableTestFormDataSet = new System.Data.DataSet();
            this.panel1 = new System.Windows.Forms.Panel();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDown1)).BeginInit();
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
            this.measureControlButton.Size = new System.Drawing.Size(111, 28);
            this.measureControlButton.TabIndex = 0;
            this.measureControlButton.Text = "СТАРТ";
            this.measureControlButton.UseVisualStyleBackColor = true;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(306, 142);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(35, 13);
            this.label1.TabIndex = 2;
            this.label1.Text = "label1";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(76, 39);
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
            // numericUpDown1
            // 
            this.numericUpDown1.Location = new System.Drawing.Point(181, 30);
            this.numericUpDown1.Maximum = new decimal(new int[] {
            35,
            0,
            0,
            0});
            this.numericUpDown1.Minimum = new decimal(new int[] {
            5,
            0,
            0,
            0});
            this.numericUpDown1.Name = "numericUpDown1";
            this.numericUpDown1.Size = new System.Drawing.Size(120, 20);
            this.numericUpDown1.TabIndex = 6;
            this.numericUpDown1.Value = new decimal(new int[] {
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
            this.cableForTest_CB.SelectedValueChanged += new System.EventHandler(this.cableForTest_CB_SelectedIndexChanged);
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
            this.connectionType.Controls.Add(this.doubleTable_RadioBatton);
            this.connectionType.Controls.Add(this.singleTable_RadioBatton);
            this.connectionType.Location = new System.Drawing.Point(433, 58);
            this.connectionType.Name = "connectionType";
            this.connectionType.Size = new System.Drawing.Size(146, 47);
            this.connectionType.TabIndex = 12;
            this.connectionType.TabStop = false;
            this.connectionType.Text = "Способ подключения";
            // 
            // doubleTable_RadioBatton
            // 
            this.doubleTable_RadioBatton.AutoSize = true;
            this.doubleTable_RadioBatton.Location = new System.Drawing.Point(75, 19);
            this.doubleTable_RadioBatton.Name = "doubleTable_RadioBatton";
            this.doubleTable_RadioBatton.Size = new System.Drawing.Size(62, 17);
            this.doubleTable_RadioBatton.TabIndex = 13;
            this.doubleTable_RadioBatton.Text = "без ДК";
            this.doubleTable_RadioBatton.UseVisualStyleBackColor = true;
            this.doubleTable_RadioBatton.CheckedChanged += new System.EventHandler(this.tableMode_RadioBatton_CheckedChanged);
            // 
            // singleTable_RadioBatton
            // 
            this.singleTable_RadioBatton.AutoSize = true;
            this.singleTable_RadioBatton.Checked = true;
            this.singleTable_RadioBatton.Location = new System.Drawing.Point(8, 19);
            this.singleTable_RadioBatton.Name = "singleTable_RadioBatton";
            this.singleTable_RadioBatton.Size = new System.Drawing.Size(50, 17);
            this.singleTable_RadioBatton.TabIndex = 0;
            this.singleTable_RadioBatton.TabStop = true;
            this.singleTable_RadioBatton.Text = "с ДК";
            this.singleTable_RadioBatton.UseVisualStyleBackColor = true;
            this.singleTable_RadioBatton.CheckedChanged += new System.EventHandler(this.tableMode_RadioBatton_CheckedChanged);
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
            // textBox1
            // 
            this.textBox1.Location = new System.Drawing.Point(169, 132);
            this.textBox1.Name = "textBox1";
            this.textBox1.Size = new System.Drawing.Size(132, 20);
            this.textBox1.TabIndex = 15;
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
            // checkBox1
            // 
            this.checkBox1.AutoSize = true;
            this.checkBox1.Location = new System.Drawing.Point(321, 30);
            this.checkBox1.Name = "checkBox1";
            this.checkBox1.Size = new System.Drawing.Size(167, 17);
            this.checkBox1.TabIndex = 21;
            this.checkBox1.Text = "Использовать термодатчик";
            this.checkBox1.UseVisualStyleBackColor = true;
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
            this.panel1.Controls.Add(this.label2);
            this.panel1.Controls.Add(this.label1);
            this.panel1.Location = new System.Drawing.Point(12, 194);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(567, 210);
            this.panel1.TabIndex = 23;
            // 
            // SACCableTestForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(590, 450);
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.testProgram_GroupBox);
            this.Controls.Add(this.measureControlButton);
            this.Controls.Add(this.checkBox1);
            this.Controls.Add(this.barabanTypes_CB);
            this.Controls.Add(this.label10);
            this.Controls.Add(this.operatorsList);
            this.Controls.Add(this.RizolSelector_CB);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label9);
            this.Controls.Add(this.numericUpDown1);
            this.Controls.Add(this.cableLength_NumericUpDown);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.label8);
            this.Controls.Add(this.cableForTest_CB);
            this.Controls.Add(this.textBox1);
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
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDown1)).EndInit();
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
        private System.Windows.Forms.NumericUpDown numericUpDown1;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.ComboBox cableForTest_CB;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.ComboBox connectedFromTableElement_ComboBox;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.GroupBox connectionType;
        private System.Windows.Forms.RadioButton doubleTable_RadioBatton;
        private System.Windows.Forms.RadioButton singleTable_RadioBatton;
        private System.Windows.Forms.ComboBox barabanTypes_CB;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.TextBox textBox1;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.NumericUpDown cableLength_NumericUpDown;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.ComboBox RizolSelector_CB;
        private System.Windows.Forms.Label label10;
        private System.Windows.Forms.CheckBox checkBox1;
        private System.Windows.Forms.GroupBox testProgram_GroupBox;
        private System.Data.DataSet CableTestFormDataSet;
        private System.Windows.Forms.Panel panel1;
    }
}