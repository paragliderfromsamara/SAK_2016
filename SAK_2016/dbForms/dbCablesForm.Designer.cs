namespace SAK_2016
{
    partial class dbCablesForm
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
            this.components = new System.ComponentModel.Container();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle1 = new System.Windows.Forms.DataGridViewCellStyle();
            this.closeBut = new System.Windows.Forms.Button();
            this.dbCablesDataSet = new System.Data.DataSet();
            this.cablesList = new System.Windows.Forms.DataGridView();
            this.id = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.CabName = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.CabNameStruct = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.DocNum = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.TextPrim = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.KodOKP = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.KodOKP_KCH = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.openCableFormBut = new System.Windows.Forms.Button();
            this.cableListContextMenuStrip = new System.Windows.Forms.ContextMenuStrip(this.components);
            ((System.ComponentModel.ISupportInitialize)(this.dbCablesDataSet)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.cablesList)).BeginInit();
            this.SuspendLayout();
            // 
            // closeBut
            // 
            this.closeBut.Location = new System.Drawing.Point(12, 468);
            this.closeBut.Name = "closeBut";
            this.closeBut.Size = new System.Drawing.Size(75, 23);
            this.closeBut.TabIndex = 0;
            this.closeBut.Text = "Закрыть";
            this.closeBut.UseVisualStyleBackColor = true;
            this.closeBut.Click += new System.EventHandler(this.closeBut_Click);
            // 
            // dbCablesDataSet
            // 
            this.dbCablesDataSet.DataSetName = "NewDataSet";
            // 
            // cablesList
            // 
            this.cablesList.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.cablesList.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.cablesList.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.id,
            this.CabName,
            this.CabNameStruct,
            this.DocNum,
            this.TextPrim,
            this.KodOKP,
            this.KodOKP_KCH});
            this.cablesList.Location = new System.Drawing.Point(12, 56);
            this.cablesList.Name = "cablesList";
            dataGridViewCellStyle1.SelectionBackColor = System.Drawing.Color.Teal;
            this.cablesList.RowsDefaultCellStyle = dataGridViewCellStyle1;
            this.cablesList.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.cablesList.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.cablesList.Size = new System.Drawing.Size(943, 390);
            this.cablesList.StandardTab = true;
            this.cablesList.TabIndex = 1;
            // 
            // id
            // 
            this.id.DataPropertyName = "id";
            this.id.HeaderText = "id";
            this.id.Name = "id";
            this.id.ReadOnly = true;
            // 
            // CabName
            // 
            this.CabName.DataPropertyName = "name";
            this.CabName.HeaderText = "Марка кабеля";
            this.CabName.Name = "CabName";
            this.CabName.ReadOnly = true;
            // 
            // CabNameStruct
            // 
            this.CabNameStruct.DataPropertyName = "struct_name";
            this.CabNameStruct.HeaderText = "Структура кабеля";
            this.CabNameStruct.Name = "CabNameStruct";
            this.CabNameStruct.ReadOnly = true;
            // 
            // DocNum
            // 
            this.DocNum.DataPropertyName = "document_name";
            this.DocNum.HeaderText = "Норматив";
            this.DocNum.Name = "DocNum";
            this.DocNum.ReadOnly = true;
            // 
            // TextPrim
            // 
            this.TextPrim.DataPropertyName = "notes";
            this.TextPrim.HeaderText = "Примечание";
            this.TextPrim.Name = "TextPrim";
            this.TextPrim.ReadOnly = true;
            // 
            // KodOKP
            // 
            this.KodOKP.DataPropertyName = "code_okp";
            this.KodOKP.HeaderText = "Код ОКП";
            this.KodOKP.Name = "KodOKP";
            this.KodOKP.ReadOnly = true;
            // 
            // KodOKP_KCH
            // 
            this.KodOKP_KCH.DataPropertyName = "code_kch";
            this.KodOKP_KCH.HeaderText = "КЧ";
            this.KodOKP_KCH.Name = "KodOKP_KCH";
            this.KodOKP_KCH.ReadOnly = true;
            // 
            // openCableFormBut
            // 
            this.openCableFormBut.Location = new System.Drawing.Point(93, 468);
            this.openCableFormBut.Name = "openCableFormBut";
            this.openCableFormBut.Size = new System.Drawing.Size(150, 23);
            this.openCableFormBut.TabIndex = 2;
            this.openCableFormBut.Text = "Добавить тип кабеля";
            this.openCableFormBut.UseVisualStyleBackColor = true;
            this.openCableFormBut.Click += new System.EventHandler(this.openCableFormBut_Click);
            // 
            // cableListContextMenuStrip
            // 
            this.cableListContextMenuStrip.Name = "cableListContextMenuStrip";
            this.cableListContextMenuStrip.Size = new System.Drawing.Size(61, 4);
            // 
            // dbCablesForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(967, 503);
            this.Controls.Add(this.openCableFormBut);
            this.Controls.Add(this.cablesList);
            this.Controls.Add(this.closeBut);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow;
            this.Name = "dbCablesForm";
            this.Text = "База данных кабелей";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.dbCablesForm_FormClosing);
            ((System.ComponentModel.ISupportInitialize)(this.dbCablesDataSet)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.cablesList)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button closeBut;
        private System.Data.DataSet dbCablesDataSet;
        private System.Windows.Forms.DataGridView cablesList;
        private System.Windows.Forms.Button openCableFormBut;
        private System.Windows.Forms.DataGridViewTextBoxColumn id;
        private System.Windows.Forms.DataGridViewTextBoxColumn CabName;
        private System.Windows.Forms.DataGridViewTextBoxColumn CabNameStruct;
        private System.Windows.Forms.DataGridViewTextBoxColumn DocNum;
        private System.Windows.Forms.DataGridViewTextBoxColumn TextPrim;
        private System.Windows.Forms.DataGridViewTextBoxColumn KodOKP;
        private System.Windows.Forms.DataGridViewTextBoxColumn KodOKP_KCH;
        private System.Windows.Forms.ContextMenuStrip cableListContextMenuStrip;
    }
}