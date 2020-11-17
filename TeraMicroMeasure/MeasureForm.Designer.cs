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
            this.cableComboBox = new System.Windows.Forms.ComboBox();
            this.label1 = new System.Windows.Forms.Label();
            this.numericUpDown1 = new System.Windows.Forms.NumericUpDown();
            this.label2 = new System.Windows.Forms.Label();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.RizolRadioButton = new System.Windows.Forms.RadioButton();
            this.RleadRadioButton = new System.Windows.Forms.RadioButton();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.depolTimeLbl = new System.Windows.Forms.Label();
            this.depolDelay = new System.Windows.Forms.NumericUpDown();
            this.averagingCounter = new System.Windows.Forms.ComboBox();
            this.label5 = new System.Windows.Forms.Label();
            this.polarDelayLbl = new System.Windows.Forms.Label();
            this.polarDelayUpDown = new System.Windows.Forms.NumericUpDown();
            this.temperatureComboBox = new System.Windows.Forms.Label();
            this.measurePanel = new System.Windows.Forms.Panel();
            this.panel1 = new System.Windows.Forms.Panel();
            this.comboBox3 = new System.Windows.Forms.ComboBox();
            this.label4 = new System.Windows.Forms.Label();
            this.temperature = new System.Windows.Forms.ComboBox();
            this.startMeasureButton = new System.Windows.Forms.Button();
            this.panel2 = new System.Windows.Forms.Panel();
            this.voltagesGroupBox = new System.Windows.Forms.GroupBox();
            this.v1000_RadioButton = new System.Windows.Forms.RadioButton();
            this.v500_RadioButton = new System.Windows.Forms.RadioButton();
            this.v100_RadioButton = new System.Windows.Forms.RadioButton();
            this.v10_RadioButton = new System.Windows.Forms.RadioButton();
            this.richTextBox1 = new System.Windows.Forms.RichTextBox();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDown1)).BeginInit();
            this.groupBox1.SuspendLayout();
            this.groupBox2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.depolDelay)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.polarDelayUpDown)).BeginInit();
            this.measurePanel.SuspendLayout();
            this.panel1.SuspendLayout();
            this.voltagesGroupBox.SuspendLayout();
            this.SuspendLayout();
            // 
            // cableComboBox
            // 
            this.cableComboBox.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cableComboBox.FormattingEnabled = true;
            this.cableComboBox.Items.AddRange(new object[] {
            "Кабель 1",
            "Кабель 2",
            "Кабель 3",
            "Кабель 4"});
            this.cableComboBox.Location = new System.Drawing.Point(17, 62);
            this.cableComboBox.Margin = new System.Windows.Forms.Padding(4);
            this.cableComboBox.Name = "cableComboBox";
            this.cableComboBox.Size = new System.Drawing.Size(218, 31);
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
            // numericUpDown1
            // 
            this.numericUpDown1.Location = new System.Drawing.Point(248, 63);
            this.numericUpDown1.Maximum = new decimal(new int[] {
            10000,
            0,
            0,
            0});
            this.numericUpDown1.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.numericUpDown1.Name = "numericUpDown1";
            this.numericUpDown1.Size = new System.Drawing.Size(136, 30);
            this.numericUpDown1.TabIndex = 2;
            this.numericUpDown1.Value = new decimal(new int[] {
            1000,
            0,
            0,
            0});
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(243, 37);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(90, 23);
            this.label2.TabIndex = 3;
            this.label2.Text = "Длина, м";
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.RizolRadioButton);
            this.groupBox1.Controls.Add(this.RleadRadioButton);
            this.groupBox1.Location = new System.Drawing.Point(17, 117);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(367, 82);
            this.groupBox1.TabIndex = 4;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Измеряемый параметр";
            this.groupBox1.UseCompatibleTextRendering = true;
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
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.depolTimeLbl);
            this.groupBox2.Controls.Add(this.depolDelay);
            this.groupBox2.Controls.Add(this.averagingCounter);
            this.groupBox2.Controls.Add(this.label5);
            this.groupBox2.Controls.Add(this.polarDelayLbl);
            this.groupBox2.Controls.Add(this.polarDelayUpDown);
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
            this.depolTimeLbl.Size = new System.Drawing.Size(72, 18);
            this.depolTimeLbl.TabIndex = 8;
            this.depolTimeLbl.Text = "Разряд, c";
            // 
            // depolDelay
            // 
            this.depolDelay.Location = new System.Drawing.Point(384, 62);
            this.depolDelay.Maximum = new decimal(new int[] {
            300,
            0,
            0,
            0});
            this.depolDelay.Name = "depolDelay";
            this.depolDelay.Size = new System.Drawing.Size(115, 26);
            this.depolDelay.TabIndex = 7;
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
            // polarDelayUpDown
            // 
            this.polarDelayUpDown.Location = new System.Drawing.Point(22, 61);
            this.polarDelayUpDown.Maximum = new decimal(new int[] {
            10000,
            0,
            0,
            0});
            this.polarDelayUpDown.Minimum = new decimal(new int[] {
            200,
            0,
            0,
            0});
            this.polarDelayUpDown.Name = "polarDelayUpDown";
            this.polarDelayUpDown.Size = new System.Drawing.Size(115, 26);
            this.polarDelayUpDown.TabIndex = 0;
            this.polarDelayUpDown.Value = new decimal(new int[] {
            200,
            0,
            0,
            0});
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
            // measurePanel
            // 
            this.measurePanel.BackColor = System.Drawing.SystemColors.ControlLightLight;
            this.measurePanel.Controls.Add(this.panel1);
            this.measurePanel.Controls.Add(this.startMeasureButton);
            this.measurePanel.Controls.Add(this.panel2);
            this.measurePanel.Controls.Add(this.voltagesGroupBox);
            this.measurePanel.Controls.Add(this.cableComboBox);
            this.measurePanel.Controls.Add(this.groupBox2);
            this.measurePanel.Controls.Add(this.label1);
            this.measurePanel.Controls.Add(this.groupBox1);
            this.measurePanel.Controls.Add(this.numericUpDown1);
            this.measurePanel.Controls.Add(this.label2);
            this.measurePanel.Location = new System.Drawing.Point(-2, -1);
            this.measurePanel.Name = "measurePanel";
            this.measurePanel.Size = new System.Drawing.Size(917, 412);
            this.measurePanel.TabIndex = 6;
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.comboBox3);
            this.panel1.Controls.Add(this.temperatureComboBox);
            this.panel1.Controls.Add(this.label4);
            this.panel1.Controls.Add(this.temperature);
            this.panel1.Location = new System.Drawing.Point(17, 215);
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
            // startMeasureButton
            // 
            this.startMeasureButton.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(65)))), ((int)(((byte)(124)))), ((int)(((byte)(224)))));
            this.startMeasureButton.Cursor = System.Windows.Forms.Cursors.Hand;
            this.startMeasureButton.FlatAppearance.BorderSize = 0;
            this.startMeasureButton.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.startMeasureButton.ForeColor = System.Drawing.SystemColors.ControlLightLight;
            this.startMeasureButton.Location = new System.Drawing.Point(405, 199);
            this.startMeasureButton.Name = "startMeasureButton";
            this.startMeasureButton.Size = new System.Drawing.Size(198, 48);
            this.startMeasureButton.TabIndex = 11;
            this.startMeasureButton.Text = "Пуск измерения";
            this.startMeasureButton.UseVisualStyleBackColor = false;
            // 
            // panel2
            // 
            this.panel2.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(211)))), ((int)(((byte)(237)))), ((int)(((byte)(250)))));
            this.panel2.Location = new System.Drawing.Point(405, 0);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(512, 199);
            this.panel2.TabIndex = 10;
            // 
            // voltagesGroupBox
            // 
            this.voltagesGroupBox.Controls.Add(this.v1000_RadioButton);
            this.voltagesGroupBox.Controls.Add(this.v500_RadioButton);
            this.voltagesGroupBox.Controls.Add(this.v100_RadioButton);
            this.voltagesGroupBox.Controls.Add(this.v10_RadioButton);
            this.voltagesGroupBox.Font = new System.Drawing.Font("Tahoma", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.voltagesGroupBox.Location = new System.Drawing.Point(17, 293);
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
            // richTextBox1
            // 
            this.richTextBox1.Location = new System.Drawing.Point(-2, 417);
            this.richTextBox1.Name = "richTextBox1";
            this.richTextBox1.Size = new System.Drawing.Size(917, 289);
            this.richTextBox1.TabIndex = 7;
            this.richTextBox1.Text = "";
            // 
            // MeasureForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(10F, 23F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.SystemColors.ControlLight;
            this.ClientSize = new System.Drawing.Size(914, 709);
            this.Controls.Add(this.richTextBox1);
            this.Controls.Add(this.measurePanel);
            this.Font = new System.Drawing.Font("Tahoma", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Margin = new System.Windows.Forms.Padding(4, 6, 4, 6);
            this.MaximizeBox = false;
            this.Name = "MeasureForm";
            this.Text = "MeasureForm";
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDown1)).EndInit();
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.depolDelay)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.polarDelayUpDown)).EndInit();
            this.measurePanel.ResumeLayout(false);
            this.measurePanel.PerformLayout();
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.voltagesGroupBox.ResumeLayout(false);
            this.voltagesGroupBox.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.ComboBox cableComboBox;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.NumericUpDown numericUpDown1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.RadioButton RizolRadioButton;
        private System.Windows.Forms.RadioButton RleadRadioButton;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.ComboBox temperature;
        private System.Windows.Forms.ComboBox averagingCounter;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label temperatureComboBox;
        private System.Windows.Forms.Label polarDelayLbl;
        private System.Windows.Forms.NumericUpDown polarDelayUpDown;
        private System.Windows.Forms.Panel measurePanel;
        private System.Windows.Forms.GroupBox voltagesGroupBox;
        private System.Windows.Forms.RadioButton v1000_RadioButton;
        private System.Windows.Forms.RadioButton v500_RadioButton;
        private System.Windows.Forms.RadioButton v100_RadioButton;
        private System.Windows.Forms.RadioButton v10_RadioButton;
        private System.Windows.Forms.Label depolTimeLbl;
        private System.Windows.Forms.NumericUpDown depolDelay;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.ComboBox comboBox3;
        private System.Windows.Forms.Button startMeasureButton;
        private System.Windows.Forms.Panel panel2;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.RichTextBox richTextBox1;
    }
}