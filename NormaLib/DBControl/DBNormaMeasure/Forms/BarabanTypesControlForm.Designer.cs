namespace NormaLib.DBControl.DBNormaMeasure.Forms
{
    partial class BarabanTypesControlForm
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
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle1 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle2 = new System.Windows.Forms.DataGridViewCellStyle();
            this.barabanTypesDS = new System.Data.DataSet();
            this.newBarabanType = new System.Windows.Forms.GroupBox();
            this.textBox1 = new System.Windows.Forms.TextBox();
            this.addBarabanType = new System.Windows.Forms.Button();
            this.label2 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.barabanName = new System.Windows.Forms.TextBox();
            this.barabanTypeList = new System.Windows.Forms.DataGridView();
            this.baraban_type_id = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.name = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Weight = new System.Windows.Forms.DataGridViewTextBoxColumn();
            ((System.ComponentModel.ISupportInitialize)(this.barabanTypesDS)).BeginInit();
            this.newBarabanType.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.barabanTypeList)).BeginInit();
            this.SuspendLayout();
            // 
            // barabanTypesDS
            // 
            this.barabanTypesDS.DataSetName = "NewDataSet";
            // 
            // newBarabanType
            // 
            this.newBarabanType.Controls.Add(this.textBox1);
            this.newBarabanType.Controls.Add(this.addBarabanType);
            this.newBarabanType.Controls.Add(this.label2);
            this.newBarabanType.Controls.Add(this.label1);
            this.newBarabanType.Controls.Add(this.barabanName);
            this.newBarabanType.Location = new System.Drawing.Point(12, 12);
            this.newBarabanType.Name = "newBarabanType";
            this.newBarabanType.Size = new System.Drawing.Size(596, 83);
            this.newBarabanType.TabIndex = 2;
            this.newBarabanType.TabStop = false;
            this.newBarabanType.Text = "Новый тип барабана";
            // 
            // textBox1
            // 
            this.textBox1.Location = new System.Drawing.Point(309, 45);
            this.textBox1.Name = "textBox1";
            this.textBox1.Size = new System.Drawing.Size(128, 20);
            this.textBox1.TabIndex = 5;
            this.textBox1.TextChanged += new System.EventHandler(this.textBox1_TextChanged);
            // 
            // addBarabanType
            // 
            this.addBarabanType.Location = new System.Drawing.Point(473, 43);
            this.addBarabanType.Name = "addBarabanType";
            this.addBarabanType.Size = new System.Drawing.Size(75, 23);
            this.addBarabanType.TabIndex = 4;
            this.addBarabanType.Text = "Добавить";
            this.addBarabanType.UseVisualStyleBackColor = true;
            this.addBarabanType.Click += new System.EventHandler(this.addBarabanType_Click);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(306, 29);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(43, 13);
            this.label2.TabIndex = 3;
            this.label2.Text = "Вес, кг";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(15, 29);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(109, 13);
            this.label1.TabIndex = 1;
            this.label1.Text = "Наименование типа";
            // 
            // barabanName
            // 
            this.barabanName.Location = new System.Drawing.Point(18, 45);
            this.barabanName.Name = "barabanName";
            this.barabanName.Size = new System.Drawing.Size(276, 20);
            this.barabanName.TabIndex = 0;
            this.barabanName.TextChanged += new System.EventHandler(this.barabanName_TextChanged);
            // 
            // barabanTypeList
            // 
            this.barabanTypeList.AllowUserToAddRows = false;
            this.barabanTypeList.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
            | System.Windows.Forms.AnchorStyles.Left)
            | System.Windows.Forms.AnchorStyles.Right)));
            this.barabanTypeList.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.AllCells;
            this.barabanTypeList.BackgroundColor = System.Drawing.Color.LightGray;
            this.barabanTypeList.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.barabanTypeList.ColumnHeadersHeight = 41;
            this.barabanTypeList.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.baraban_type_id,
            this.name,
            this.Weight});
            dataGridViewCellStyle1.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle1.BackColor = System.Drawing.SystemColors.Window;
            dataGridViewCellStyle1.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            dataGridViewCellStyle1.ForeColor = System.Drawing.SystemColors.ControlText;
            dataGridViewCellStyle1.NullValue = "-";
            dataGridViewCellStyle1.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle1.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle1.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
            this.barabanTypeList.DefaultCellStyle = dataGridViewCellStyle1;
            this.barabanTypeList.GridColor = System.Drawing.SystemColors.ButtonShadow;
            this.barabanTypeList.Location = new System.Drawing.Point(12, 113);
            this.barabanTypeList.Name = "barabanTypeList";
            dataGridViewCellStyle2.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            dataGridViewCellStyle2.Padding = new System.Windows.Forms.Padding(7, 0, 0, 0);
            dataGridViewCellStyle2.SelectionBackColor = System.Drawing.Color.Teal;
            dataGridViewCellStyle2.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.barabanTypeList.RowsDefaultCellStyle = dataGridViewCellStyle2;
            this.barabanTypeList.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.barabanTypeList.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.barabanTypeList.Size = new System.Drawing.Size(596, 294);
            this.barabanTypeList.TabIndex = 1;
            // 
            // baraban_type_id
            // 
            this.baraban_type_id.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.ColumnHeader;
            this.baraban_type_id.DataPropertyName = "baraban_type_id";
            this.baraban_type_id.Frozen = true;
            this.baraban_type_id.HeaderText = "id";
            this.baraban_type_id.MinimumWidth = 100;
            this.baraban_type_id.Name = "baraban_type_id";
            this.baraban_type_id.ReadOnly = true;
            // 
            // name
            // 
            this.name.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            this.name.DataPropertyName = "baraban_type_name";
            this.name.FillWeight = 90.28419F;
            this.name.HeaderText = "Наименование типа";
            this.name.Name = "name";
            this.name.ReadOnly = true;
            // 
            // Weight
            // 
            this.Weight.DataPropertyName = "baraban_weight";
            this.Weight.HeaderText = "Вес, кг";
            this.Weight.MinimumWidth = 100;
            this.Weight.Name = "Weight";
            this.Weight.ReadOnly = true;
            // 
            // BarabanTypesControlForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(620, 420);
            this.Controls.Add(this.newBarabanType);
            this.Controls.Add(this.barabanTypeList);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "BarabanTypesControlForm";
            this.Text = "Типы барабанов";
            ((System.ComponentModel.ISupportInitialize)(this.barabanTypesDS)).EndInit();
            this.newBarabanType.ResumeLayout(false);
            this.newBarabanType.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.barabanTypeList)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Data.DataSet barabanTypesDS;
        private System.Windows.Forms.DataGridView barabanTypeList;
        private System.Windows.Forms.GroupBox newBarabanType;
        private System.Windows.Forms.Button addBarabanType;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox barabanName;
        private System.Windows.Forms.DataGridViewTextBoxColumn baraban_type_id;
        private System.Windows.Forms.DataGridViewTextBoxColumn name;
        private System.Windows.Forms.DataGridViewTextBoxColumn Weight;
        private System.Windows.Forms.TextBox textBox1;
    }
}