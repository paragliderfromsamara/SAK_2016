﻿namespace SAK_2016
{
    partial class dbBarabansForm
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
            this.closeBut = new System.Windows.Forms.Button();
            this.newBarabanType = new System.Windows.Forms.GroupBox();
            this.addBarabanType = new System.Windows.Forms.Button();
            this.label2 = new System.Windows.Forms.Label();
            this.barabanWeight = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.barabanName = new System.Windows.Forms.TextBox();
            this.barabanTypeList = new System.Windows.Forms.DataGridView();
            this.barabanTypeDataSet = new System.Data.DataSet();
            this.id = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.name = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Weight = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.newBarabanType.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.barabanTypeList)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.barabanTypeDataSet)).BeginInit();
            this.SuspendLayout();
            // 
            // closeBut
            // 
            this.closeBut.Location = new System.Drawing.Point(33, 394);
            this.closeBut.Name = "closeBut";
            this.closeBut.Size = new System.Drawing.Size(75, 23);
            this.closeBut.TabIndex = 0;
            this.closeBut.Text = "Закрыть";
            this.closeBut.UseVisualStyleBackColor = true;
            this.closeBut.Click += new System.EventHandler(this.closeBut_Click);
            // 
            // newBarabanType
            // 
            this.newBarabanType.Controls.Add(this.addBarabanType);
            this.newBarabanType.Controls.Add(this.label2);
            this.newBarabanType.Controls.Add(this.barabanWeight);
            this.newBarabanType.Controls.Add(this.label1);
            this.newBarabanType.Controls.Add(this.barabanName);
            this.newBarabanType.Location = new System.Drawing.Point(33, 21);
            this.newBarabanType.Name = "newBarabanType";
            this.newBarabanType.Size = new System.Drawing.Size(605, 90);
            this.newBarabanType.TabIndex = 1;
            this.newBarabanType.TabStop = false;
            this.newBarabanType.Text = "Новый тип барабана";
            // 
            // addBarabanType
            // 
            this.addBarabanType.Location = new System.Drawing.Point(473, 51);
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
            this.label2.Location = new System.Drawing.Point(306, 37);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(43, 13);
            this.label2.TabIndex = 3;
            this.label2.Text = "Вес, кг";
            // 
            // barabanWeight
            // 
            this.barabanWeight.Location = new System.Drawing.Point(309, 53);
            this.barabanWeight.Name = "barabanWeight";
            this.barabanWeight.Size = new System.Drawing.Size(140, 20);
            this.barabanWeight.TabIndex = 2;
            this.barabanWeight.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.barabanWeight_KeyPress);
            this.barabanWeight.Leave += new System.EventHandler(this.barabanWeight_Leave);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(15, 37);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(109, 13);
            this.label1.TabIndex = 1;
            this.label1.Text = "Наименование типа";
            // 
            // barabanName
            // 
            this.barabanName.Location = new System.Drawing.Point(18, 53);
            this.barabanName.Name = "barabanName";
            this.barabanName.Size = new System.Drawing.Size(276, 20);
            this.barabanName.TabIndex = 0;
            // 
            // barabanTypeList
            // 
            this.barabanTypeList.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.AllCells;
            this.barabanTypeList.BackgroundColor = System.Drawing.Color.LightGray;
            this.barabanTypeList.ColumnHeadersHeight = 41;
            this.barabanTypeList.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.id,
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
            this.barabanTypeList.Location = new System.Drawing.Point(33, 136);
            this.barabanTypeList.Name = "barabanTypeList";
            dataGridViewCellStyle2.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            dataGridViewCellStyle2.Padding = new System.Windows.Forms.Padding(7, 0, 0, 0);
            dataGridViewCellStyle2.SelectionBackColor = System.Drawing.Color.Teal;
            dataGridViewCellStyle2.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.barabanTypeList.RowsDefaultCellStyle = dataGridViewCellStyle2;
            this.barabanTypeList.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.barabanTypeList.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.barabanTypeList.Size = new System.Drawing.Size(605, 229);
            this.barabanTypeList.TabIndex = 0;
            // 
            // barabanTypeDataSet
            // 
            this.barabanTypeDataSet.DataSetName = "NewDataSet";
            // 
            // id
            // 
            this.id.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.ColumnHeader;
            this.id.DataPropertyName = "id";
            this.id.Frozen = true;
            this.id.HeaderText = "id";
            this.id.MinimumWidth = 100;
            this.id.Name = "id";
            this.id.ReadOnly = true;
            // 
            // name
            // 
            this.name.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            this.name.DataPropertyName = "name";
            this.name.FillWeight = 90.28419F;
            this.name.HeaderText = "Наименование типа";
            this.name.Name = "name";
            this.name.ReadOnly = true;
            // 
            // Weight
            // 
            this.Weight.DataPropertyName = "weight";
            this.Weight.HeaderText = "Вес, кг";
            this.Weight.MinimumWidth = 100;
            this.Weight.Name = "Weight";
            this.Weight.ReadOnly = true;
            // 
            // dbBarabansForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(680, 443);
            this.Controls.Add(this.barabanTypeList);
            this.Controls.Add(this.newBarabanType);
            this.Controls.Add(this.closeBut);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow;
            this.Name = "dbBarabansForm";
            this.Text = "База типов барабанов";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.dbBarabansForm_FormClosing);
            this.newBarabanType.ResumeLayout(false);
            this.newBarabanType.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.barabanTypeList)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.barabanTypeDataSet)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button closeBut;
        private System.Windows.Forms.DataGridView barabanTypeList;
        private System.Windows.Forms.GroupBox newBarabanType;
        private System.Windows.Forms.Button addBarabanType;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox barabanWeight;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox barabanName;
        private System.Data.DataSet barabanTypeDataSet;
        private System.Windows.Forms.DataGridViewTextBoxColumn id;
        private System.Windows.Forms.DataGridViewTextBoxColumn name;
        private System.Windows.Forms.DataGridViewTextBoxColumn Weight;
    }
}