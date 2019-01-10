namespace NormaMeasure.SAC_APP
{
    partial class manualTestForm
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
            this.closeButton = new System.Windows.Forms.Button();
            this.measuredParamsList = new System.Windows.Forms.ComboBox();
            this.label1 = new System.Windows.Forms.Label();
            this.pair1 = new System.Windows.Forms.ComboBox();
            this.pairGroupBox = new System.Windows.Forms.GroupBox();
            this.leadNumberLabel = new System.Windows.Forms.Label();
            this.leadList = new System.Windows.Forms.ComboBox();
            this.pair2Lbl = new System.Windows.Forms.Label();
            this.pair1Lbl = new System.Windows.Forms.Label();
            this.pair2 = new System.Windows.Forms.ComboBox();
            this.tableMode = new System.Windows.Forms.CheckBox();
            this.freqGroupBox = new System.Windows.Forms.GroupBox();
            this.label4 = new System.Windows.Forms.Label();
            this.eFreqLabel = new System.Windows.Forms.Label();
            this.sFreqLabel = new System.Windows.Forms.Label();
            this.freqStepComboBox = new System.Windows.Forms.ComboBox();
            this.eFreqComboBox = new System.Windows.Forms.ComboBox();
            this.sFreqComboBox = new System.Windows.Forms.ComboBox();
            this.measureResultPanel = new System.Windows.Forms.Panel();
            this.quantitativeMeasureResult = new System.Windows.Forms.Label();
            this.mResultLabel = new System.Windows.Forms.Label();
            this.startStopMeasureBut = new System.Windows.Forms.Button();
            this.pairGroupBox.SuspendLayout();
            this.freqGroupBox.SuspendLayout();
            this.measureResultPanel.SuspendLayout();
            this.SuspendLayout();
            // 
            // closeButton
            // 
            this.closeButton.Location = new System.Drawing.Point(12, 258);
            this.closeButton.Name = "closeButton";
            this.closeButton.Size = new System.Drawing.Size(75, 23);
            this.closeButton.TabIndex = 0;
            this.closeButton.Text = "Закрыть";
            this.closeButton.UseVisualStyleBackColor = true;
            this.closeButton.Click += new System.EventHandler(this.closeButton_Click);
            // 
            // measuredParamsList
            // 
            this.measuredParamsList.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.measuredParamsList.FormattingEnabled = true;
            this.measuredParamsList.Location = new System.Drawing.Point(12, 25);
            this.measuredParamsList.Name = "measuredParamsList";
            this.measuredParamsList.Size = new System.Drawing.Size(121, 21);
            this.measuredParamsList.TabIndex = 1;
            this.measuredParamsList.SelectedIndexChanged += new System.EventHandler(this.measuredParamsList_SelectedIndexChanged);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(9, 9);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(127, 13);
            this.label1.TabIndex = 2;
            this.label1.Text = "Измеряемый параметр";
            // 
            // pair1
            // 
            this.pair1.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.pair1.DropDownWidth = 52;
            this.pair1.FormatString = "N0";
            this.pair1.FormattingEnabled = true;
            this.pair1.Location = new System.Drawing.Point(25, 34);
            this.pair1.Name = "pair1";
            this.pair1.Size = new System.Drawing.Size(42, 21);
            this.pair1.TabIndex = 3;
            this.pair1.SelectedIndexChanged += new System.EventHandler(this.startPair_SelectedIndexChanged);
            // 
            // pairGroupBox
            // 
            this.pairGroupBox.Controls.Add(this.leadNumberLabel);
            this.pairGroupBox.Controls.Add(this.leadList);
            this.pairGroupBox.Controls.Add(this.pair2Lbl);
            this.pairGroupBox.Controls.Add(this.pair1Lbl);
            this.pairGroupBox.Controls.Add(this.pair2);
            this.pairGroupBox.Controls.Add(this.pair1);
            this.pairGroupBox.Location = new System.Drawing.Point(12, 66);
            this.pairGroupBox.Name = "pairGroupBox";
            this.pairGroupBox.Size = new System.Drawing.Size(268, 69);
            this.pairGroupBox.TabIndex = 4;
            this.pairGroupBox.TabStop = false;
            this.pairGroupBox.Text = "Выбор пар";
            // 
            // leadNumberLabel
            // 
            this.leadNumberLabel.AutoSize = true;
            this.leadNumberLabel.Location = new System.Drawing.Point(194, 18);
            this.leadNumberLabel.Name = "leadNumberLabel";
            this.leadNumberLabel.Size = new System.Drawing.Size(33, 13);
            this.leadNumberLabel.TabIndex = 8;
            this.leadNumberLabel.Text = "жила";
            // 
            // leadList
            // 
            this.leadList.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.leadList.FormattingEnabled = true;
            this.leadList.Items.AddRange(new object[] {
            "a",
            "b"});
            this.leadList.Location = new System.Drawing.Point(196, 34);
            this.leadList.Name = "leadList";
            this.leadList.Size = new System.Drawing.Size(42, 21);
            this.leadList.TabIndex = 7;
            // 
            // pair2Lbl
            // 
            this.pair2Lbl.AutoSize = true;
            this.pair2Lbl.Location = new System.Drawing.Point(110, 18);
            this.pair2Lbl.Name = "pair2Lbl";
            this.pair2Lbl.Size = new System.Drawing.Size(19, 13);
            this.pair2Lbl.TabIndex = 6;
            this.pair2Lbl.Text = "по";
            // 
            // pair1Lbl
            // 
            this.pair1Lbl.AutoSize = true;
            this.pair1Lbl.Location = new System.Drawing.Point(22, 18);
            this.pair1Lbl.Name = "pair1Lbl";
            this.pair1Lbl.Size = new System.Drawing.Size(13, 13);
            this.pair1Lbl.TabIndex = 5;
            this.pair1Lbl.Text = "с";
            // 
            // pair2
            // 
            this.pair2.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.pair2.DropDownWidth = 52;
            this.pair2.FormattingEnabled = true;
            this.pair2.Location = new System.Drawing.Point(111, 34);
            this.pair2.Name = "pair2";
            this.pair2.Size = new System.Drawing.Size(42, 21);
            this.pair2.TabIndex = 4;
            this.pair2.SelectedIndexChanged += new System.EventHandler(this.endPair_SelectedIndexChanged);
            // 
            // tableMode
            // 
            this.tableMode.AutoSize = true;
            this.tableMode.Location = new System.Drawing.Point(155, 29);
            this.tableMode.Name = "tableMode";
            this.tableMode.Size = new System.Drawing.Size(63, 17);
            this.tableMode.TabIndex = 5;
            this.tableMode.Text = "без ДК";
            this.tableMode.UseVisualStyleBackColor = true;
            this.tableMode.CheckedChanged += new System.EventHandler(this.tableMode_CheckedChanged);
            // 
            // freqGroupBox
            // 
            this.freqGroupBox.Controls.Add(this.label4);
            this.freqGroupBox.Controls.Add(this.eFreqLabel);
            this.freqGroupBox.Controls.Add(this.sFreqLabel);
            this.freqGroupBox.Controls.Add(this.freqStepComboBox);
            this.freqGroupBox.Controls.Add(this.eFreqComboBox);
            this.freqGroupBox.Controls.Add(this.sFreqComboBox);
            this.freqGroupBox.Location = new System.Drawing.Point(12, 151);
            this.freqGroupBox.Name = "freqGroupBox";
            this.freqGroupBox.Size = new System.Drawing.Size(268, 83);
            this.freqGroupBox.TabIndex = 6;
            this.freqGroupBox.TabStop = false;
            this.freqGroupBox.Text = "Частота";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(210, 23);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(51, 13);
            this.label4.TabIndex = 5;
            this.label4.Text = "Шаг, кГц";
            // 
            // eFreqLabel
            // 
            this.eFreqLabel.AutoSize = true;
            this.eFreqLabel.Location = new System.Drawing.Point(108, 23);
            this.eFreqLabel.Name = "eFreqLabel";
            this.eFreqLabel.Size = new System.Drawing.Size(95, 13);
            this.eFreqLabel.TabIndex = 4;
            this.eFreqLabel.Text = "Кон. частота, кГц";
            // 
            // sFreqLabel
            // 
            this.sFreqLabel.AutoSize = true;
            this.sFreqLabel.Location = new System.Drawing.Point(4, 23);
            this.sFreqLabel.Name = "sFreqLabel";
            this.sFreqLabel.Size = new System.Drawing.Size(95, 13);
            this.sFreqLabel.TabIndex = 3;
            this.sFreqLabel.Text = "Нач. частота, кГц";
            // 
            // freqStepComboBox
            // 
            this.freqStepComboBox.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.freqStepComboBox.FormattingEnabled = true;
            this.freqStepComboBox.Location = new System.Drawing.Point(213, 39);
            this.freqStepComboBox.Name = "freqStepComboBox";
            this.freqStepComboBox.Size = new System.Drawing.Size(48, 21);
            this.freqStepComboBox.TabIndex = 2;
            // 
            // eFreqComboBox
            // 
            this.eFreqComboBox.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.eFreqComboBox.FormattingEnabled = true;
            this.eFreqComboBox.Location = new System.Drawing.Point(111, 39);
            this.eFreqComboBox.Name = "eFreqComboBox";
            this.eFreqComboBox.Size = new System.Drawing.Size(92, 21);
            this.eFreqComboBox.TabIndex = 1;
            this.eFreqComboBox.SelectedIndexChanged += new System.EventHandler(this.eFreqComboBox_SelectedIndexChanged);
            // 
            // sFreqComboBox
            // 
            this.sFreqComboBox.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.sFreqComboBox.FormattingEnabled = true;
            this.sFreqComboBox.Items.AddRange(new object[] {
            "256",
            "500",
            "1000",
            "2000"});
            this.sFreqComboBox.Location = new System.Drawing.Point(7, 39);
            this.sFreqComboBox.Name = "sFreqComboBox";
            this.sFreqComboBox.Size = new System.Drawing.Size(92, 21);
            this.sFreqComboBox.TabIndex = 0;
            this.sFreqComboBox.SelectedIndexChanged += new System.EventHandler(this.sFreqComboBox_SelectedIndexChanged);
            // 
            // measureResultPanel
            // 
            this.measureResultPanel.BackColor = System.Drawing.SystemColors.ControlLightLight;
            this.measureResultPanel.Controls.Add(this.quantitativeMeasureResult);
            this.measureResultPanel.Controls.Add(this.mResultLabel);
            this.measureResultPanel.Location = new System.Drawing.Point(358, 25);
            this.measureResultPanel.Name = "measureResultPanel";
            this.measureResultPanel.Size = new System.Drawing.Size(432, 137);
            this.measureResultPanel.TabIndex = 7;
            // 
            // quantitativeMeasureResult
            // 
            this.quantitativeMeasureResult.AutoSize = true;
            this.quantitativeMeasureResult.Font = new System.Drawing.Font("Microsoft Sans Serif", 30F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.quantitativeMeasureResult.Location = new System.Drawing.Point(238, 48);
            this.quantitativeMeasureResult.Name = "quantitativeMeasureResult";
            this.quantitativeMeasureResult.Size = new System.Drawing.Size(82, 46);
            this.quantitativeMeasureResult.TabIndex = 1;
            this.quantitativeMeasureResult.Text = "Ом";
            // 
            // mResultLabel
            // 
            this.mResultLabel.AutoSize = true;
            this.mResultLabel.Font = new System.Drawing.Font("Microsoft Sans Serif", 36F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.mResultLabel.Location = new System.Drawing.Point(116, 41);
            this.mResultLabel.Name = "mResultLabel";
            this.mResultLabel.Size = new System.Drawing.Size(51, 55);
            this.mResultLabel.TabIndex = 0;
            this.mResultLabel.Text = "0";
            // 
            // startStopMeasureBut
            // 
            this.startStopMeasureBut.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.startStopMeasureBut.Location = new System.Drawing.Point(358, 168);
            this.startStopMeasureBut.Name = "startStopMeasureBut";
            this.startStopMeasureBut.Size = new System.Drawing.Size(432, 41);
            this.startStopMeasureBut.TabIndex = 8;
            this.startStopMeasureBut.Text = "начать измерение";
            this.startStopMeasureBut.UseVisualStyleBackColor = true;
            this.startStopMeasureBut.Click += new System.EventHandler(this.startStopMeasureBut_Click);
            // 
            // manualTestForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(841, 308);
            this.ControlBox = false;
            this.Controls.Add(this.startStopMeasureBut);
            this.Controls.Add(this.measureResultPanel);
            this.Controls.Add(this.freqGroupBox);
            this.Controls.Add(this.tableMode);
            this.Controls.Add(this.pairGroupBox);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.measuredParamsList);
            this.Controls.Add(this.closeButton);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow;
            this.Name = "manualTestForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Ручные испытания";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.manualTestForm_FormClosing);
            this.pairGroupBox.ResumeLayout(false);
            this.pairGroupBox.PerformLayout();
            this.freqGroupBox.ResumeLayout(false);
            this.freqGroupBox.PerformLayout();
            this.measureResultPanel.ResumeLayout(false);
            this.measureResultPanel.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button closeButton;
        private System.Windows.Forms.ComboBox measuredParamsList;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.ComboBox pair1;
        private System.Windows.Forms.GroupBox pairGroupBox;
        private System.Windows.Forms.Label pair2Lbl;
        private System.Windows.Forms.Label pair1Lbl;
        private System.Windows.Forms.ComboBox pair2;
        private System.Windows.Forms.Label leadNumberLabel;
        private System.Windows.Forms.ComboBox leadList;
        private System.Windows.Forms.CheckBox tableMode;
        private System.Windows.Forms.GroupBox freqGroupBox;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label eFreqLabel;
        private System.Windows.Forms.Label sFreqLabel;
        private System.Windows.Forms.ComboBox freqStepComboBox;
        private System.Windows.Forms.ComboBox eFreqComboBox;
        private System.Windows.Forms.ComboBox sFreqComboBox;
        private System.Windows.Forms.Panel measureResultPanel;
        private System.Windows.Forms.Label quantitativeMeasureResult;
        private System.Windows.Forms.Label mResultLabel;
        private System.Windows.Forms.Button startStopMeasureBut;
    }
}