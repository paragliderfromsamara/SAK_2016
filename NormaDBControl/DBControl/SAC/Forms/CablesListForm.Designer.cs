namespace NormaMeasure.DBControl.SAC.Forms
{
    partial class CablesListForm
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
            this.newCableFormButton = new System.Windows.Forms.Button();
            this.cablesList = new System.Windows.Forms.DataGridView();
            this.cableListContextMenu = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.редактироватьToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.создатьИзToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.удалитьToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.cable_id = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.CabName = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.CabNameStruct = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.DocNum = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.TextPrim = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.KodOKP = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.KodOKP_KCH = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.is_draft = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.is_deleted = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.p_min = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.p_max = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.document_id = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.build_length = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.u_cover = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.linear_mass = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.full_name = new System.Windows.Forms.DataGridViewTextBoxColumn();
            ((System.ComponentModel.ISupportInitialize)(this.cablesList)).BeginInit();
            this.cableListContextMenu.SuspendLayout();
            this.SuspendLayout();
            // 
            // newCableFormButton
            // 
            this.newCableFormButton.Location = new System.Drawing.Point(12, 12);
            this.newCableFormButton.Name = "newCableFormButton";
            this.newCableFormButton.Size = new System.Drawing.Size(94, 26);
            this.newCableFormButton.TabIndex = 0;
            this.newCableFormButton.Text = "Добавить";
            this.newCableFormButton.UseVisualStyleBackColor = true;
            this.newCableFormButton.Click += new System.EventHandler(this.newCableFormButton_Click);
            // 
            // cablesList
            // 
            this.cablesList.AllowUserToAddRows = false;
            this.cablesList.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.cablesList.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.cablesList.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.cable_id,
            this.CabName,
            this.CabNameStruct,
            this.DocNum,
            this.TextPrim,
            this.KodOKP,
            this.KodOKP_KCH,
            this.is_draft,
            this.is_deleted,
            this.p_min,
            this.p_max,
            this.document_id,
            this.build_length,
            this.u_cover,
            this.linear_mass,
            this.full_name});
            this.cablesList.Location = new System.Drawing.Point(12, 53);
            this.cablesList.Name = "cablesList";
            dataGridViewCellStyle1.SelectionBackColor = System.Drawing.Color.Teal;
            this.cablesList.RowsDefaultCellStyle = dataGridViewCellStyle1;
            this.cablesList.RowTemplate.ContextMenuStrip = this.cableListContextMenu;
            this.cablesList.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.cablesList.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.cablesList.Size = new System.Drawing.Size(943, 390);
            this.cablesList.StandardTab = true;
            this.cablesList.TabIndex = 2;
            // 
            // cableListContextMenu
            // 
            this.cableListContextMenu.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.редактироватьToolStripMenuItem,
            this.создатьИзToolStripMenuItem,
            this.удалитьToolStripMenuItem});
            this.cableListContextMenu.Name = "cableListContextMenu";
            this.cableListContextMenu.Size = new System.Drawing.Size(155, 70);
            // 
            // редактироватьToolStripMenuItem
            // 
            this.редактироватьToolStripMenuItem.Name = "редактироватьToolStripMenuItem";
            this.редактироватьToolStripMenuItem.Size = new System.Drawing.Size(154, 22);
            this.редактироватьToolStripMenuItem.Text = "Редактировать";
            this.редактироватьToolStripMenuItem.Click += new System.EventHandler(this.editCableToolStripMenuItem_Click);
            // 
            // создатьИзToolStripMenuItem
            // 
            this.создатьИзToolStripMenuItem.Name = "создатьИзToolStripMenuItem";
            this.создатьИзToolStripMenuItem.Size = new System.Drawing.Size(154, 22);
            this.создатьИзToolStripMenuItem.Text = "Создать из...";
            // 
            // удалитьToolStripMenuItem
            // 
            this.удалитьToolStripMenuItem.Name = "удалитьToolStripMenuItem";
            this.удалитьToolStripMenuItem.Size = new System.Drawing.Size(154, 22);
            this.удалитьToolStripMenuItem.Text = "Удалить";
            // 
            // cable_id
            // 
            this.cable_id.DataPropertyName = "cable_id";
            this.cable_id.HeaderText = "id";
            this.cable_id.Name = "cable_id";
            this.cable_id.ReadOnly = true;
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
            this.DocNum.DataPropertyName = "short_name";
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
            // is_draft
            // 
            this.is_draft.DataPropertyName = "is_draft";
            this.is_draft.HeaderText = "Черновик";
            this.is_draft.Name = "is_draft";
            this.is_draft.Visible = false;
            // 
            // is_deleted
            // 
            this.is_deleted.DataPropertyName = "is_deleted";
            this.is_deleted.HeaderText = "Удалён";
            this.is_deleted.Name = "is_deleted";
            this.is_deleted.Visible = false;
            // 
            // p_min
            // 
            this.p_min.DataPropertyName = "p_min";
            this.p_min.HeaderText = "Мин. давление";
            this.p_min.Name = "p_min";
            this.p_min.Visible = false;
            // 
            // p_max
            // 
            this.p_max.DataPropertyName = "p_max";
            this.p_max.HeaderText = "Макс. давление";
            this.p_max.Name = "p_max";
            this.p_max.Visible = false;
            // 
            // document_id
            // 
            this.document_id.DataPropertyName = "document_id";
            this.document_id.HeaderText = "document_id";
            this.document_id.Name = "document_id";
            this.document_id.Visible = false;
            // 
            // build_length
            // 
            this.build_length.DataPropertyName = "build_length";
            this.build_length.HeaderText = "Строительная длина, м";
            this.build_length.Name = "build_length";
            this.build_length.Visible = false;
            // 
            // u_cover
            // 
            this.u_cover.DataPropertyName = "u_cover";
            this.u_cover.HeaderText = "Исп. напряжение прочности оболочки";
            this.u_cover.Name = "u_cover";
            this.u_cover.Visible = false;
            // 
            // linear_mass
            // 
            this.linear_mass.DataPropertyName = "linear_mass";
            this.linear_mass.HeaderText = "Погонный вес";
            this.linear_mass.Name = "linear_mass";
            this.linear_mass.Visible = false;
            // 
            // full_name
            // 
            this.full_name.DataPropertyName = "full_name";
            this.full_name.HeaderText = "Полное наименование документа";
            this.full_name.Name = "full_name";
            this.full_name.Visible = false;
            // 
            // CablesListForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(966, 458);
            this.Controls.Add(this.cablesList);
            this.Controls.Add(this.newCableFormButton);
            this.Name = "CablesListForm";
            this.Text = "CablesListForm";
            ((System.ComponentModel.ISupportInitialize)(this.cablesList)).EndInit();
            this.cableListContextMenu.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button newCableFormButton;
        private System.Windows.Forms.DataGridView cablesList;
        private System.Windows.Forms.ContextMenuStrip cableListContextMenu;
        private System.Windows.Forms.ToolStripMenuItem редактироватьToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem создатьИзToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem удалитьToolStripMenuItem;
        private System.Windows.Forms.DataGridViewTextBoxColumn cable_id;
        private System.Windows.Forms.DataGridViewTextBoxColumn CabName;
        private System.Windows.Forms.DataGridViewTextBoxColumn CabNameStruct;
        private System.Windows.Forms.DataGridViewTextBoxColumn DocNum;
        private System.Windows.Forms.DataGridViewTextBoxColumn TextPrim;
        private System.Windows.Forms.DataGridViewTextBoxColumn KodOKP;
        private System.Windows.Forms.DataGridViewTextBoxColumn KodOKP_KCH;
        private System.Windows.Forms.DataGridViewTextBoxColumn is_draft;
        private System.Windows.Forms.DataGridViewTextBoxColumn is_deleted;
        private System.Windows.Forms.DataGridViewTextBoxColumn p_min;
        private System.Windows.Forms.DataGridViewTextBoxColumn p_max;
        private System.Windows.Forms.DataGridViewTextBoxColumn document_id;
        private System.Windows.Forms.DataGridViewTextBoxColumn build_length;
        private System.Windows.Forms.DataGridViewTextBoxColumn u_cover;
        private System.Windows.Forms.DataGridViewTextBoxColumn linear_mass;
        private System.Windows.Forms.DataGridViewTextBoxColumn full_name;
    }
}