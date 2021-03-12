using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using NormaLib.DBControl.Tables;

namespace NormaLib.DBControl.DBNormaMeasure.Forms
{
    public partial class CableForm : Form
    {
        private bool isNew = false;
        public bool IsNew => isNew;
        protected Cable cable;
        public Cable Cable => cable;
        public CableForm()
        {
            isNew = true;
            InitDesign();
            cable = Cable.GetDraft();
            if (cable != null) InitFormByAssignedCable();
        }

        public CableForm(uint cable_id)
        {
            isNew = false;
            InitDesign();
            cable = Cable.find_by_cable_id(cable_id);
            if (cable != null) InitFormByAssignedCable();
        }

        public CableForm(Cable copiedCable)
        {
            isNew = true;
            InitDesign();
            cable = Cable.GetCableCopy(copiedCable);
            if (cable != null) InitFormByAssignedCable();
        }

        protected virtual void InitFormByAssignedCable()
        {
            FillDBData();
            fillFormByCable();
            initStructureTabPage();
        }



        protected virtual void fillFormByCable()
        {
            this.Text = (cable.IsDraft) ? "Новый кабель" : cable.FullName;
            CableMark_input.Text = cable.Name;
            CableStructures_input.Text = cable.StructName;
            //DocumentName_input.Text = cable.DocumentName;
            //DocumentNumber_input.Text = cable.DocumentNumber;
            BuildLength_input.Value = (decimal)cable.BuildLength;
            linearMass_input.Value = (decimal)cable.LinearMass;
            Ucover_input.Value = (decimal)cable.UCover;
            // Pmin_input.Value = (decimal)cable.PMin;
            // Pmax_input.Value = (decimal)cable.PMax;
            CodeOKP_input.Text = cable.CodeOKP;
            CodeKCH_input.Text = cable.CodeKCH;
            Notes_input.Text = cable.Notes;

            //////this.Text = $"{cable.id}";
        }


        protected virtual void InitDesign()
        {
            InitializeComponent();
        }

        protected virtual void FillDBData()
        {
            fillCableMarks();
            fillDocuments();
            fillStructureTypes();
            fillLeadMaterials();
            fillIsolationMaterials();
            fillDRBringingFormuls();
            fillDRFormuls();
            fillLengthBringingTypes();
        }


        #region Заполнение cableFormDataSet необходимыми таблицами

        private void fillLengthBringingTypes()
        {
            cableFormDataSet.Tables.Add(LengthBringingType.get_all_as_table());
        }

        private void fillDRFormuls()
        {
            cableFormDataSet.Tables.Add(dRFormula.get_all_as_table());
        }

        private void fillDRBringingFormuls()
        {
            cableFormDataSet.Tables.Add(dRBringingFormula.get_all_as_table());
        }

        private void fillIsolationMaterials()
        {
            cableFormDataSet.Tables.Add(IsolationMaterial.get_all_as_table());
        }

        private void fillLeadMaterials()
        {
            cableFormDataSet.Tables.Add(LeadMaterial.get_all_as_table());
        }

        protected virtual void fillStructureTypes()
        {
            DBEntityTable t = CableStructureType.get_all_as_table();
            DataTable forView;
            CableStructureType undefinedCableStructureType = (CableStructureType)t.NewRow();
            undefinedCableStructureType.StructureLeadsAmount = 0;
            undefinedCableStructureType.StructureTypeName = "Не выбран";
            undefinedCableStructureType.StructureMeasuredParameters = "";
            undefinedCableStructureType.StructureLeadsAmount = 0;
            undefinedCableStructureType.StructureTypeId = 0;
            t.Rows.Add(undefinedCableStructureType);
            cableFormDataSet.Tables.Add(t);
            DataView dv = t.DefaultView;
            dv.Sort = $"{CableStructureType.TypeId_ColumnName} ASC";
            forView = dv.ToTable(true, new string[] { CableStructureType.TypeName_ColumnName, CableStructureType.TypeId_ColumnName });
            cbStructureType.DataSource = forView;
            cbStructureType.DisplayMember = CableStructureType.TypeName_ColumnName;
            cbStructureType.ValueMember = CableStructureType.TypeId_ColumnName;
        }

        protected virtual void fillDocuments()
        {
            string docsTableName = reloadDocumentsDS();
            DocumentNumber_input.ValueMember = $"{docsTableName}.document_id";
            DocumentNumber_input.DisplayMember = $"{docsTableName}.short_name";
            DocumentNumber_input.Refresh();
            //DocumentNumber_input.SelectedIndexChanged += DocumentNumberInput_Changed;
            DocumentNumber_input.TextChanged += DocumentNumberInput_Changed;
            if (cable.QADocument == null) cable.QADocument = getDocumentDraftFromDataTable();
            DocumentNumber_input.SelectedValue = cable.DocumentId;
            
        }

        protected virtual void fillCableMarks()
        {
            cableFormDataSet.Tables.Add(Cable.get_cable_marks());
            CableMark_input.DisplayMember = CableMark_input.ValueMember = "cable_marks.cable_mark";
            CableMark_input.Refresh();
            if (String.IsNullOrWhiteSpace(Cable.Name)) Cable.Name = CableMark_input.SelectedText;
        }


        #endregion

        protected virtual string reloadDocumentsDS()
        {
            DataTable docs = Document.get_all_as_table();
            //Добавляем новый документ в таблицу
            Document newDoc = (Document)docs.NewRow();
            newDoc.DocumentId = 0;
            newDoc.FullName = String.Empty;
            newDoc.ShortName = String.Empty;
            docs.Rows.Add(newDoc);
            if (cableFormDataSet.Tables.Contains(docs.TableName))
            {
                cableFormDataSet.Tables[docs.TableName].Merge(docs);
            }
            else
            {
                cableFormDataSet.Tables.Add(docs);
            }
            return docs.TableName;
        }

        private Document getDocumentDraftFromDataTable()
        {
            Document doc = Document.build();
            foreach (DataRow r in cableFormDataSet.Tables[doc.Table.TableName].Rows)
            {
                if (r.RowState == DataRowState.Added)
                {
                    return (Document)r;
                }
            }

            return doc;
        }

        protected void DocumentNumberInput_Changed(object sender, EventArgs e)
        {
            Document doc = getDocumentDraftFromDataTable();
            ComboBox cp = sender as ComboBox;
            string txt = cp.Text;
            //  MessageBox.Show(txt);
            foreach (DataRow r in cableFormDataSet.Tables[doc.Table.TableName].Rows)
            {
                if (((Document)r).ShortName == txt && r.RowState != DataRowState.Added)
                {
                    doc = (Document)r;
                    break;
                }
            }
            if (doc.RowState == DataRowState.Added)
            {
                doc.ShortName = txt;
            }
            cable.QADocument = doc;
            //cp.SelectedValue = cable.QADocument.DocumentId;
            DocumentName_input.Enabled = doc.RowState == DataRowState.Added;
            DocumentName_input.Text = cable.QADocument.FullName;
        }
        protected void DocumentName_input_TextChanged(object sender, EventArgs e)
        {
            TextBox tb = sender as TextBox;
            if (tb.Enabled)
            {
                Document d = getDocumentDraftFromDataTable();
                d.FullName = tb.Text;
                cable.QADocument = d;
            }
        }

        protected void CodeKCH_input_TextChanged(object sender, EventArgs e)
        {
            cable.CodeKCH = (sender as MaskedTextBox).Text;
        }

        protected void CodeOKP_input_TextChanged(object sender, EventArgs e)
        {
            cable.CodeOKP = (sender as MaskedTextBox).Text;
        }

        protected virtual void RefreshStructuresName()
        {
            string strName = String.Empty;
            foreach (CableStructure s in cable.CableStructures.Rows)
            {
                if (s.RowState != DataRowState.Deleted) strName += $"{s.StructureTitle} ";
            }
            CableStructures_input.Text = strName;
        }

        protected void CableStructures_input_TextChanged(object sender, System.EventArgs e)
        {
            cable.StructName = (sender as TextBox).Text;
        }

        private void cableNameTextField_TextChanged(object sender, System.EventArgs e)
        {
            ComboBox cb = sender as ComboBox;
            cable.Name = cb.Text;
        }

        private void BuildLength_input_ValueChanged(object sender, System.EventArgs e)
        {
            float v = 0f;
            float.TryParse((sender as NumericUpDown).Value.ToString(), out v);
            Cable.BuildLength = v;
            refreshBuildLengthOnStructureMeasureParameters();
        }

        /// <summary>
        /// Обновляем длину приведения в измеряемых параметрах структур, в которых длина приведения - строительная длина кабеля
        /// </summary>
        protected virtual void refreshBuildLengthOnStructureMeasureParameters()
        {
            
        }


        private void CableMark_input_SelectedIndexChanged(object sender, System.EventArgs e)
        {

        }

        private void Notes_input_TextChanged(object sender, System.EventArgs e)
        {
            cable.Notes = (sender as RichTextBox).Text;
        }

        private void linearMass_input_ValueChanged(object sender, System.EventArgs e)
        {
            float v = 0f;
            float.TryParse((sender as NumericUpDown).Value.ToString(), out v);
            cable.LinearMass = v;
        }

        private void Ucover_input_ValueChanged(object sender, System.EventArgs e)
        {
            cable.UCover = (int)(sender as NumericUpDown).Value;

        }

        private void closeNoSaveButton_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void saveCableButton_Click(object sender, System.EventArgs e)
        {
            if (!checkSelectedDocument()) return;
            if (!saveCableStructures()) return;
            cable.IsDraft = false;
            if (cable.Save())
            {
                fillFormByCable();
                MessageBox.Show("Кабель успешно сохранён!", "Сохранено", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
            }
        }





        private bool checkSelectedDocument()
        {
            bool f = true;
            string docName = cable.QADocument.ShortName;
            if (cable.QADocument.RowState == DataRowState.Added)
            {
                string tableName = string.Empty;
                f = cable.QADocument.Save();
                if (f)
                {
                    tableName = reloadDocumentsDS();
                    DocumentNumber_input.Refresh();
                    cable.DocumentId = cable.QADocument.DocumentId;
                    foreach (DataRow r in cableFormDataSet.Tables[tableName].Rows)
                    {
                        Document d = (Document)r;
                        if (d.ShortName == docName)
                        {
                            DocumentNumber_input.SelectedValue = d.DocumentId;
                            break;
                        }
                    }
                }
            }
            return f;
        }

        private bool saveCableStructures()
        {
            bool isSave = true;
            foreach (CableStructure s in cable.CableStructures.Rows)
            {
                if (s.RowState != DataRowState.Deleted) isSave &= s.Save();
            }
            return isSave;
        }

        int currentStructureId = 0;
        int nextStrucureId = 0;
        bool firstInit = false;
        CableStructure draftStructure;
        CableStructure currentStructure => currentStructureId < tabControl1.TabCount - 1 ? (CableStructure)(Cable.CableStructures.Rows[currentStructureId]) : draftStructure; 

        private void cbStructureType_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (!firstInit)
            {
                firstInit = true;
                return;
            }
            DataRow[] rows = cableFormDataSet.Tables["cable_structure_types"].Select($"{CableStructureType.TypeId_ColumnName} = {(uint)cbStructureType.SelectedValue}");
            currentStructure.StructureType = (rows.Length > 0) ? (CableStructureType)rows[0] : null;
            tabControl1.SelectedTab.Text = currentStructure.StructureTitle;
            if (currentStructure == draftStructure && currentStructure.StructureTypeId != 0)
            {
                Cable.CableStructures.Rows.Add(draftStructure);
                MakeDraftStructure();
                btnRemoveCurrentStructure.Visible = currentStructure != draftStructure;
            }
        }

        private void initStructureTabPage()
        {
            if (cable.CableStructures.Rows.Count > 0)
            {
                tabControl1.TabPages.Clear();
                foreach (CableStructure s in cable.CableStructures.Rows)
                {
                    AddTabPage(s.StructureTitle);
                }
                AddTabPage("Новая структура");
                ReplacePanelToCurrentPage();
                FillStructurePageForCurrentRow();
            }
            MakeDraftStructure();
            btnRemoveCurrentStructure.Visible = currentStructure != draftStructure;
        }

        private CableStructure MakeDraftStructure()
        {
            draftStructure = (CableStructure)Cable.CableStructures.NewRow();
            draftStructure.CableStructureId = 0;
            draftStructure.CableId = Cable.CableId;
            draftStructure.LeadDiameter = 0.4f;
            draftStructure.LeadMaterialTypeId = 1;
            draftStructure.IsolationMaterialId = 1;
            draftStructure.DRBringingFormulaId = 1;
            draftStructure.DRFormulaId = 1;
            draftStructure.LeadDiameter = 0.1f;
            draftStructure.WaveResistance = 0;
            draftStructure.WorkCapacityGroup = false;
            draftStructure.LeadToLeadTestVoltage = 0;
            draftStructure.LeadToShieldTestVoltage = 0;
            draftStructure.GroupedAmount = 0;
            draftStructure.DisplayedAmount = 1;
            draftStructure.RealAmount = 1;
          
            if (tabControl1.TabPages.Count == Cable.CableStructures.Rows.Count)
            {
                AddTabPage("Новая структура");
            }
            return draftStructure;
        }

        bool delTabFlag = false;
        private void tabControl1_Selecting(object sender, TabControlCancelEventArgs e)
        {
            if (currentStructure != draftStructure && !delTabFlag) e.Cancel = !currentStructure.Save();
        }

        private void tabControl1_SelectedIndexChanged(object sender, EventArgs e)
        {
            currentStructureId = tabControl1.SelectedIndex;
            ReplacePanelToCurrentPage();
            FillStructurePageForCurrentRow();
        }

        private void FillStructurePageForCurrentRow()
        {
            btnRemoveCurrentStructure.Visible = currentStructure != draftStructure;
            cbStructureType.SelectedValue = currentStructure.StructureTypeId;
        }

        private void ReplacePanelToCurrentPage()
        {
            tabControl1.TabPages[tabControl1.SelectedIndex].Controls.Add(structureDataContainer);
        }

        private void btnRemoveCurrentStructure_Click(object sender, EventArgs e)
        {
            if (tabControl1.TabCount == 1) return;
            CableStructure delStruct = currentStructure;
            int delStructIndex = currentStructureId;
            int nextStructureId;
            nextStructureId = (currentStructureId > 0) ? currentStructureId - 1 : 1;
            delTabFlag = true;
            tabControl1.SelectedIndex = nextStructureId;
            delTabFlag = Cable.RemoveStructure(delStruct);
            if (delTabFlag) tabControl1.TabPages[delStructIndex].Dispose();
            delTabFlag = false;
            currentStructureId = tabControl1.SelectedIndex;
        }

        private void AddTabPage(string text)
        {
            TabPage tp = new TabPage(text);
            tp.Location = new System.Drawing.Point(4, 27);
            tp.Padding = new System.Windows.Forms.Padding(3);
            tp.Size = new System.Drawing.Size(862, 371);
            tp.TabIndex = 0;
            tp.UseVisualStyleBackColor = true;
            tabControl1.TabPages.Add(tp);
        }
    }
}
