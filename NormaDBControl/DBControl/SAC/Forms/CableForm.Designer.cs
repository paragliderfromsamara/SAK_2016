namespace NormaMeasure.DBControl.SAC.Forms
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
            this.saveCableButton = new System.Windows.Forms.Button();
            this.CableMark_input = new System.Windows.Forms.ComboBox();
            this.cableFormDataSet = new System.Data.DataSet();
            this.label1 = new System.Windows.Forms.Label();
            this.CableStructures_input = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.DocumentNumber_input = new System.Windows.Forms.ComboBox();
            this.label3 = new System.Windows.Forms.Label();
            this.DocumentName_input = new System.Windows.Forms.TextBox();
            this.label4 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.BuildLength_input = new System.Windows.Forms.NumericUpDown();
            this.linearMass_input = new System.Windows.Forms.NumericUpDown();
            this.label6 = new System.Windows.Forms.Label();
            this.Ucover_input = new System.Windows.Forms.NumericUpDown();
            this.label7 = new System.Windows.Forms.Label();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.label9 = new System.Windows.Forms.Label();
            this.label8 = new System.Windows.Forms.Label();
            this.Pmin_input = new System.Windows.Forms.NumericUpDown();
            this.Pmax_input = new System.Windows.Forms.NumericUpDown();
            this.label11 = new System.Windows.Forms.Label();
            this.label12 = new System.Windows.Forms.Label();
            this.Notes_input = new System.Windows.Forms.RichTextBox();
            this.label13 = new System.Windows.Forms.Label();
            this.CodeOKP_input = new System.Windows.Forms.MaskedTextBox();
            this.CodeKCH_input = new System.Windows.Forms.MaskedTextBox();
            this.button1 = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.cableFormDataSet)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.BuildLength_input)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.linearMass_input)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.Ucover_input)).BeginInit();
            this.groupBox1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.Pmin_input)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.Pmax_input)).BeginInit();
            this.SuspendLayout();
            // 
            // saveCableButton
            // 
            this.saveCableButton.Location = new System.Drawing.Point(3, 563);
            this.saveCableButton.Name = "saveCableButton";
            this.saveCableButton.Size = new System.Drawing.Size(112, 32);
            this.saveCableButton.TabIndex = 0;
            this.saveCableButton.Text = "Сохранить";
            this.saveCableButton.UseVisualStyleBackColor = true;
            this.saveCableButton.Click += new System.EventHandler(this.saveCableButton_Click);
            // 
            // CableMark_input
            // 
            this.CableMark_input.DataSource = this.cableFormDataSet;
            this.CableMark_input.FormattingEnabled = true;
            this.CableMark_input.Location = new System.Drawing.Point(12, 41);
            this.CableMark_input.Name = "CableMark_input";
            this.CableMark_input.Size = new System.Drawing.Size(173, 21);
            this.CableMark_input.TabIndex = 1;
            this.CableMark_input.SelectedIndexChanged += new System.EventHandler(this.CableMark_input_SelectedIndexChanged);
            this.CableMark_input.TextUpdate += new System.EventHandler(this.CableMark_input_SelectedIndexChanged);
            // 
            // cableFormDataSet
            // 
            this.cableFormDataSet.DataSetName = "NewDataSet";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(9, 25);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(79, 13);
            this.label1.TabIndex = 2;
            this.label1.Text = "Марка кабеля";
            // 
            // CableStructures_input
            // 
            this.CableStructures_input.Location = new System.Drawing.Point(191, 41);
            this.CableStructures_input.Name = "CableStructures_input";
            this.CableStructures_input.Size = new System.Drawing.Size(609, 20);
            this.CableStructures_input.TabIndex = 3;
            this.CableStructures_input.TextChanged += new System.EventHandler(this.CableStructures_input_TextChanged);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(188, 25);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(112, 13);
            this.label2.TabIndex = 4;
            this.label2.Text = "Описание структуры";
            // 
            // DocumentNumber_input
            // 
            this.DocumentNumber_input.DataSource = this.cableFormDataSet;
            this.DocumentNumber_input.FormattingEnabled = true;
            this.DocumentNumber_input.Location = new System.Drawing.Point(12, 94);
            this.DocumentNumber_input.Name = "DocumentNumber_input";
            this.DocumentNumber_input.Size = new System.Drawing.Size(173, 21);
            this.DocumentNumber_input.TabIndex = 5;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(9, 78);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(58, 13);
            this.label3.TabIndex = 6;
            this.label3.Text = "Норматив";
            // 
            // DocumentName_input
            // 
            this.DocumentName_input.Location = new System.Drawing.Point(191, 95);
            this.DocumentName_input.Name = "DocumentName_input";
            this.DocumentName_input.Size = new System.Drawing.Size(609, 20);
            this.DocumentName_input.TabIndex = 7;
            this.DocumentName_input.TextChanged += new System.EventHandler(this.DocumentName_input_TextChanged);
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(188, 78);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(228, 13);
            this.label4.TabIndex = 8;
            this.label4.Text = "Полное название нормативного документа";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(9, 134);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(125, 13);
            this.label5.TabIndex = 10;
            this.label5.Text = "Строительная длина, м";
            // 
            // BuildLength_input
            // 
            this.BuildLength_input.Location = new System.Drawing.Point(12, 150);
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
            this.BuildLength_input.Size = new System.Drawing.Size(173, 20);
            this.BuildLength_input.TabIndex = 11;
            this.BuildLength_input.Value = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.BuildLength_input.ValueChanged += new System.EventHandler(this.BuildLength_input_ValueChanged);
            // 
            // linearMass_input
            // 
            this.linearMass_input.DecimalPlaces = 2;
            this.linearMass_input.Location = new System.Drawing.Point(191, 150);
            this.linearMass_input.Maximum = new decimal(new int[] {
            10000,
            0,
            0,
            0});
            this.linearMass_input.Name = "linearMass_input";
            this.linearMass_input.Size = new System.Drawing.Size(97, 20);
            this.linearMass_input.TabIndex = 12;
            this.linearMass_input.ThousandsSeparator = true;
            this.linearMass_input.ValueChanged += new System.EventHandler(this.linearMass_input_ValueChanged);
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(188, 134);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(100, 13);
            this.label6.TabIndex = 13;
            this.label6.Text = "Пог. масса, кг/км";
            // 
            // Ucover_input
            // 
            this.Ucover_input.Location = new System.Drawing.Point(296, 150);
            this.Ucover_input.Maximum = new decimal(new int[] {
            10000,
            0,
            0,
            0});
            this.Ucover_input.Name = "Ucover_input";
            this.Ucover_input.Size = new System.Drawing.Size(99, 20);
            this.Ucover_input.TabIndex = 14;
            this.Ucover_input.ValueChanged += new System.EventHandler(this.Ucover_input_ValueChanged);
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(293, 134);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(102, 13);
            this.label7.TabIndex = 15;
            this.label7.Text = "U исп. оболочки, В";
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.label9);
            this.groupBox1.Controls.Add(this.label8);
            this.groupBox1.Controls.Add(this.Pmin_input);
            this.groupBox1.Controls.Add(this.Pmax_input);
            this.groupBox1.Location = new System.Drawing.Point(412, 121);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(208, 61);
            this.groupBox1.TabIndex = 16;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Изб. давление, кгс/см^2";
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.Location = new System.Drawing.Point(102, 31);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(27, 13);
            this.label9.TabIndex = 17;
            this.label9.Text = "Max";
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Location = new System.Drawing.Point(6, 31);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(24, 13);
            this.label8.TabIndex = 17;
            this.label8.Text = "Min";
            // 
            // Pmin_input
            // 
            this.Pmin_input.Location = new System.Drawing.Point(33, 29);
            this.Pmin_input.Maximum = new decimal(new int[] {
            10000,
            0,
            0,
            0});
            this.Pmin_input.Name = "Pmin_input";
            this.Pmin_input.Size = new System.Drawing.Size(63, 20);
            this.Pmin_input.TabIndex = 17;
            this.Pmin_input.ValueChanged += new System.EventHandler(this.Pmin_input_ValueChanged);
            // 
            // Pmax_input
            // 
            this.Pmax_input.Location = new System.Drawing.Point(131, 29);
            this.Pmax_input.Maximum = new decimal(new int[] {
            10000,
            0,
            0,
            0});
            this.Pmax_input.Name = "Pmax_input";
            this.Pmax_input.Size = new System.Drawing.Size(63, 20);
            this.Pmax_input.TabIndex = 17;
            this.Pmax_input.ValueChanged += new System.EventHandler(this.Pmax_input_ValueChanged);
            // 
            // label11
            // 
            this.label11.AutoSize = true;
            this.label11.Location = new System.Drawing.Point(632, 134);
            this.label11.Name = "label11";
            this.label11.Size = new System.Drawing.Size(52, 13);
            this.label11.TabIndex = 20;
            this.label11.Text = "Код ОКП";
            // 
            // label12
            // 
            this.label12.AutoSize = true;
            this.label12.Location = new System.Drawing.Point(741, 134);
            this.label12.Name = "label12";
            this.label12.Size = new System.Drawing.Size(22, 13);
            this.label12.TabIndex = 21;
            this.label12.Text = "КЧ";
            // 
            // Notes_input
            // 
            this.Notes_input.Location = new System.Drawing.Point(12, 199);
            this.Notes_input.Name = "Notes_input";
            this.Notes_input.Size = new System.Drawing.Size(788, 72);
            this.Notes_input.TabIndex = 22;
            this.Notes_input.Text = "";
            this.Notes_input.TextChanged += new System.EventHandler(this.Notes_input_TextChanged);
            // 
            // label13
            // 
            this.label13.AutoSize = true;
            this.label13.Location = new System.Drawing.Point(9, 183);
            this.label13.Name = "label13";
            this.label13.Size = new System.Drawing.Size(70, 13);
            this.label13.TabIndex = 23;
            this.label13.Text = "Примечание";
            // 
            // CodeOKP_input
            // 
            this.CodeOKP_input.Location = new System.Drawing.Point(635, 150);
            this.CodeOKP_input.Mask = "00 0000 0000";
            this.CodeOKP_input.Name = "CodeOKP_input";
            this.CodeOKP_input.Size = new System.Drawing.Size(100, 20);
            this.CodeOKP_input.TabIndex = 24;
            this.CodeOKP_input.TextChanged += new System.EventHandler(this.CodeOKP_input_TextChanged);
            // 
            // CodeKCH_input
            // 
            this.CodeKCH_input.Location = new System.Drawing.Point(744, 150);
            this.CodeKCH_input.Mask = "00";
            this.CodeKCH_input.Name = "CodeKCH_input";
            this.CodeKCH_input.Size = new System.Drawing.Size(51, 20);
            this.CodeKCH_input.TabIndex = 25;
            this.CodeKCH_input.TextChanged += new System.EventHandler(this.CodeKCH_input_TextChanged);
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(191, 550);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(129, 31);
            this.button1.TabIndex = 26;
            this.button1.Text = "button1";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // CableForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(809, 620);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.CodeKCH_input);
            this.Controls.Add(this.CodeOKP_input);
            this.Controls.Add(this.label13);
            this.Controls.Add(this.Notes_input);
            this.Controls.Add(this.label12);
            this.Controls.Add(this.label11);
            this.Controls.Add(this.groupBox1);
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
            this.Controls.Add(this.CableStructures_input);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.CableMark_input);
            this.Controls.Add(this.saveCableButton);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Name = "CableForm";
            this.Text = "CableForm";
            ((System.ComponentModel.ISupportInitialize)(this.cableFormDataSet)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.BuildLength_input)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.linearMass_input)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.Ucover_input)).EndInit();
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.Pmin_input)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.Pmax_input)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button saveCableButton;
        private System.Windows.Forms.ComboBox CableMark_input;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox CableStructures_input;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.ComboBox DocumentNumber_input;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.TextBox DocumentName_input;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.NumericUpDown BuildLength_input;
        private System.Windows.Forms.NumericUpDown linearMass_input;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.NumericUpDown Ucover_input;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.NumericUpDown Pmin_input;
        private System.Windows.Forms.NumericUpDown Pmax_input;
        private System.Windows.Forms.Label label11;
        private System.Windows.Forms.Label label12;
        private System.Windows.Forms.RichTextBox Notes_input;
        private System.Windows.Forms.Label label13;
        private System.Windows.Forms.MaskedTextBox CodeOKP_input;
        private System.Windows.Forms.MaskedTextBox CodeKCH_input;
        private System.Data.DataSet cableFormDataSet;
        private System.Windows.Forms.Button button1;
    }
}