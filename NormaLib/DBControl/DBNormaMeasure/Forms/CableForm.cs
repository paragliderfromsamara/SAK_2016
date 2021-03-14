﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using NormaLib.DBControl.Tables;
using System.Diagnostics;

namespace NormaLib.DBControl.DBNormaMeasure.Forms
{
    public partial class CableForm : Form
    {
        private bool isOnInitForm;
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
            isOnInitForm = true;
            FillDBData();
            fillFormByCable();
            initStructureTabPage();
            isOnInitForm = false;

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
            SetParametersDataGridViewStyle();
        }

        private void SetParametersDataGridViewStyle()
        {
            System.Windows.Forms.DataGridViewCellStyle parameterNameCellStyle = new DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle parameterNameHeaderStyle = new DataGridViewCellStyle();
            parameterNameCellStyle.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            parameterNameCellStyle.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(65)))), ((int)(((byte)(109)))));
            parameterNameCellStyle.Font = new System.Drawing.Font("Tahoma", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            parameterNameCellStyle.ForeColor = System.Drawing.Color.Gainsboro;
            parameterNameCellStyle.NullValue = "-";
            parameterNameCellStyle.Padding = new System.Windows.Forms.Padding(3);
            parameterNameCellStyle.SelectionBackColor = parameterNameCellStyle.BackColor;
            parameterNameCellStyle.SelectionForeColor = System.Drawing.Color.Gainsboro; 
            parameterNameCellStyle.WrapMode = System.Windows.Forms.DataGridViewTriState.True;



            parameter_type_name_column.CellTemplate.Style = BuildParameterNameCellStyle();
            parameter_type_name_column.HeaderCell.Style = BuildParameterNameHeaderStyle();
    
            parameter_type_name_column.HeaderCell.ContextMenuStrip = addMeasurerParameterContextMenu;

            dgMeasuredParameters.CellClick += DgMeasuredParameters_CellClick;
            dgMeasuredParameters.CellMouseMove += DgMeasuredParameters_CellMouseMove;

            delButtonColumn.CellTemplate.Style = delButtonColumn.HeaderCell.Style = BuildDelButtonCellStyle();
            delButtonColumn.HeaderCell.ToolTipText = "Удалить все";

            DisabledCellStyle = new DataGridViewCellStyle();
            DisabledCellStyle.BackColor = System.Drawing.Color.Moccasin;
            DisabledCellStyle.SelectionBackColor = System.Drawing.Color.Moccasin;
            DisabledCellStyle.ForeColor = System.Drawing.Color.Moccasin;
            DisabledCellStyle.SelectionForeColor = System.Drawing.Color.Moccasin;

            EnabledCellStyle = new DataGridViewCellStyle();
            EnabledCellStyle.BackColor = System.Drawing.Color.White;
            EnabledCellStyle.SelectionBackColor = System.Drawing.Color.PowderBlue;
            EnabledCellStyle.ForeColor = System.Drawing.Color.Black;
            EnabledCellStyle.SelectionForeColor = System.Drawing.Color.MidnightBlue;


            DisabledBringingLengthCellStyle = new DataGridViewCellStyle();
            DisabledBringingLengthCellStyle.ForeColor = EnabledCellStyle.BackColor;
            DisabledBringingLengthCellStyle.SelectionForeColor = EnabledCellStyle.SelectionBackColor;

            EnabledBringingLengthCellStyle = new DataGridViewCellStyle();
            EnabledBringingLengthCellStyle.ForeColor = EnabledCellStyle.ForeColor;
            EnabledBringingLengthCellStyle.SelectionForeColor = EnabledCellStyle.SelectionForeColor;

            EnabledBringingLengthCellStyle.BackColor = DisabledBringingLengthCellStyle.BackColor = EnabledCellStyle.BackColor;
            EnabledBringingLengthCellStyle.SelectionBackColor = DisabledBringingLengthCellStyle.SelectionBackColor = EnabledCellStyle.SelectionBackColor;

        }

        private void DgMeasuredParameters_CellMouseMove(object sender, DataGridViewCellMouseEventArgs e)
        {
           if (e.ColumnIndex == 0 && e.RowIndex == -1 || e.ColumnIndex == delButtonColumn.Index)
            {
                Cursor.Current = Cursors.Hand;
            }
            else
            {
                Cursor.Current = Cursors.Arrow;
            }
        }

        private void DgMeasuredParameters_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex == -1 && e.ColumnIndex == 0)
            {
                parameter_type_name_column.HeaderCell.ContextMenuStrip.Show(Cursor.Position);
            }else if (e.ColumnIndex == delButtonColumn.Index)
            {
                MessageBox.Show("Вы уверены что хотите удалить?");
                RemoveParameterDataByIndex(e.RowIndex);
            }

        }



        protected virtual void FillDBData()
        {
            fillCableMarks();
            fillDocuments();
            fillMeasuredParameterTypes();
            fillStructureTypes();
            fillLeadMaterials();
            fillIsolationMaterials();
            fillDRBringingFormuls();
            fillDRFormuls();
            fillLengthBringingTypes();
        }

        private void fillMeasuredParameterTypes()
        {
            List<ToolStripMenuItem> mItems = new List<ToolStripMenuItem>();
            DBEntityTable t = MeasuredParameterType.get_all_as_table_for_cable_structure_form();
            cableFormDataSet.Tables.Add(t);
            foreach (var dr in t.Rows) mItems.Add(BuildParameterTypesContextMenuItem(dr as MeasuredParameterType));
            addMeasurerParameterContextMenu.Items.AddRange(mItems.ToArray());
        }


        #region Заполнение cableFormDataSet необходимыми таблицами

        private void fillLengthBringingTypes()
        {
            cableFormDataSet.Tables.Add(LengthBringingType.get_all_as_table());
        }

        private void fillDRFormuls()
        {
            DBEntityTable t = dRFormula.get_all_as_table();
            cableFormDataSet.Tables.Add(t);
            cbDRCalculatingFormula.DataSource = t;
            cbDRCalculatingFormula.DisplayMember = dRFormula.FormulaDescription_ColumnName;
            cbDRCalculatingFormula.ValueMember = dRFormula.FormulaId_ColumnName;
        }

        private void fillDRBringingFormuls()
        {
            DBEntityTable t = dRBringingFormula.get_all_as_table();
            cableFormDataSet.Tables.Add(t);
            cbDRBringingFormula.DataSource = t;
            cbDRBringingFormula.DisplayMember = dRBringingFormula.FormulaDescription_ColumnName;
            cbDRBringingFormula.ValueMember = dRBringingFormula.FormulaId_ColumnName;
        }

        private void fillIsolationMaterials()
        {
            DBEntityTable t = IsolationMaterial.get_all_as_table();
            cableFormDataSet.Tables.Add(IsolationMaterial.get_all_as_table());
            cbIsolationMaterial.DataSource = t;
            cbIsolationMaterial.DisplayMember = IsolationMaterial.MaterialName_ColumnName;
            cbIsolationMaterial.ValueMember = IsolationMaterial.MaterialId_ColumnName;
        }

        private void fillLeadMaterials()
        {
            DBEntityTable t = LeadMaterial.get_all_as_table();
            cableFormDataSet.Tables.Add(LeadMaterial.get_all_as_table());
            cbLeadMaterial.DataSource = t;
            cbLeadMaterial.DisplayMember = LeadMaterial.MaterialName_ColumnName;
            cbLeadMaterial.ValueMember = LeadMaterial.MaterialId_ColumnName;
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
        CableStructure draftStructure;
        CableStructure currentStructure => currentStructureId < tabControl1.TabCount - 1 ? (CableStructure)(Cable.CableStructures.Rows[currentStructureId]) : draftStructure; 

        private void cbStructureType_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (isOnInitForm || cbStructureType.SelectedIndex == selIndexWas) return;
            selIndexWas = cbStructureType.SelectedIndex;
            Debug.WriteLine("cbStructureType_SelectedIndexChanged");
            currentStructure.StructureType = GetSelectedCableStructureType();
            tabControl1.SelectedTab.Text = currentStructure.StructureTitle;
            if (currentStructure == draftStructure && currentStructure.StructureTypeId != 0)
            {
                //Добавляем новую структуру
                if (draftStructure.Save())
                {
                    Cable.CableStructures.Rows.Add(draftStructure);
                    MakeDraftStructure();
                    btnRemoveCurrentStructure.Visible = currentStructure != draftStructure;
                    fillMeasuredParametersData();
                }
            }
            RefreshAddMeasureParametersContextMenu();
            if (ExcludedParameterTypes.Length > 0) DeleteExcludedMeasureParametersFromCableStructure();
  
        }

        private void RefreshAddMeasureParametersContextMenu()
        {
            IEnumerable<uint> excludedParams = ExcludedParameterTypes.Select((v) => ((MeasuredParameterType)v).ParameterTypeId);
            foreach (ToolStripMenuItem menuItem in addMeasurerParameterContextMenu.Items)
            {
                if (menuItem.Tag.GetType().Name == typeof(MeasuredParameterType).Name) menuItem.Visible = menuItem.Enabled = !excludedParams.Contains((menuItem.Tag as MeasuredParameterType).ParameterTypeId);
            } 
        }

        private void DeleteExcludedMeasureParametersFromCableStructure()
        {
            //throw new NotImplementedException();
            IEnumerable<string> ids = ExcludedParameterTypes.Select((ept) => ((MeasuredParameterType)ept).ParameterTypeId.ToString());
            DataRow[] rData = currentStructure.MeasuredParameters.Select($"{MeasuredParameterType.ParameterTypeId_ColumnName} IN ({string.Join(",", ids)})");
            foreach(CableStructureMeasuredParameterData csmpd in rData)
            {
                if (csmpd.Destroy()) currentStructure.MeasuredParameters.Rows.Remove(csmpd);
            }
            if (rData.Length > 0) RefreshDataGridView();
        }

        /// <summary>
        /// Возвращает 
        /// </summary>
        /// <returns></returns>
        private CableStructureType GetSelectedCableStructureType()
        {
            DataRow[] rows = cableFormDataSet.Tables["cable_structure_types"].Select($"{CableStructureType.TypeId_ColumnName} = {(uint)cbStructureType.SelectedValue}");
            
            return (rows.Length > 0) ? (CableStructureType)rows[0] : null;
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
            InitParameterTypesContextMenu();
            btnRemoveCurrentStructure.Visible = currentStructure != draftStructure;
        }

        private void InitParameterTypesContextMenu()
        {
            RefreshExcludedParameterTypes();
            RefreshAddMeasureParametersContextMenu();
        }

        private ToolStripMenuItem BuildParameterTypesContextMenuItem(MeasuredParameterType pType)
        {
            ToolStripMenuItem item = new ToolStripMenuItem(pType.ParameterName);
            item.ForeColor = Color.Gainsboro;
            item.Tag = pType;
            item.Click += Item_Click;
            item.Visible = item.Enabled = false;
            item.ToolTipText = pType.Description;
            return item;
        }

        private void Item_Click(object sender, EventArgs e)
        {
            ToolStripMenuItem i = sender as ToolStripMenuItem;
            if (i.Tag.GetType() == typeof(MeasuredParameterType))
                AddMeasuredParameterData(i.Tag as MeasuredParameterType);
            else
            {
                List<MeasuredParameterType> pTypes = new List<MeasuredParameterType>();
                foreach (ToolStripMenuItem item in addMeasurerParameterContextMenu.Items)
                {
                    if (item.Tag.GetType().Name == typeof(MeasuredParameterType).Name && item.Enabled)
                    {
                        pTypes.Add(item.Tag as MeasuredParameterType);
                    }
                }
                AddMeasuredParameterDataRange(pTypes.ToArray());  
            }
        }

        private void AddMeasuredParameterData(MeasuredParameterType pType)
        {
            MeasuredParameterType[] t = { pType };
            AddMeasuredParameterDataRange(t);
        }

        private void AddMeasuredParameterDataRange(MeasuredParameterType[] pTypes)
        {
           foreach(var pt in pTypes)
            {
                CableStructureMeasuredParameterData pData = (CableStructureMeasuredParameterData)currentStructure.MeasuredParameters.NewRow();
                pData.ParameterType = pt;
                pData.AssignedStructure = currentStructure;
                pData.MeasuredParameterDataId = 0;
                pData.LengthBringingTypeId = LengthBringingType.NoBringing;
                currentStructure.MeasuredParameters.Rows.Add(pData);
            }
            RefreshDataGridView();
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
            InitParameterTypesContextMenu();
        }

        private void FillStructurePageForCurrentRow()
        {
            btnRemoveCurrentStructure.Visible = currentStructure != draftStructure;
            cbStructureType.SelectedValue = currentStructure.StructureTypeId;
            cbLeadMaterial.SelectedValue = currentStructure.LeadMaterialTypeId;
            cbIsolationMaterial.SelectedValue = currentStructure.IsolationMaterialId;
            cbDRCalculatingFormula.SelectedValue = currentStructure.DRFormulaId;
            cbDRBringingFormula.SelectedValue = currentStructure.DRBringingFormulaId;
            tbLeadDiameter.Text = currentStructure.LeadDiameter.ToString();
            nudRrealElementsAmount.Value = currentStructure.RealAmount;
            nudDisplayedElementAmount.Value = currentStructure.DisplayedAmount;
            nudLeadToLeadVoltage.Value = currentStructure.LeadToLeadTestVoltage;
            nudLeadToShieldVoltage.Value = currentStructure.LeadToShieldTestVoltage;
            cbGroupCapacity.Checked = currentStructure.WorkCapacityGroup;
            nudNumberInGroup.Value = currentStructure.GroupedAmount;
            nudWaveResistance.Value = (decimal)currentStructure.WaveResistance;

            fillMeasuredParametersData();
            selIndexWas = cbStructureType.SelectedIndex;
        }

        private void fillMeasuredParametersData()
        {
            MeasuredParametersBindingSource.DataSource = currentStructure.MeasuredParameters;
            RefreshDataGridView();
        }

        /// <summary>
        /// Обновляем данные DataGridView после изменения списка MeasuredParameters
        /// </summary>
        private void RefreshDataGridView()
        {
            MeasuredParametersBindingSource.ResetBindings(false);
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

        private void cbLeadMaterial_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (isOnInitForm) return;
            currentStructure.LeadMaterialTypeId = (uint)cbLeadMaterial.SelectedValue;
        }

        private void cbIsolationMaterial_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (isOnInitForm) return;
             currentStructure.IsolationMaterialId = (uint)cbIsolationMaterial.SelectedValue;
        }

        private void cbDRCalculatingFormula_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (isOnInitForm) return;
            currentStructure.DRFormulaId = (uint)cbDRCalculatingFormula.SelectedValue;
        }

        private void cbDRBringingFormula_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (isOnInitForm) return;
            currentStructure.DRBringingFormulaId = (uint)cbDRBringingFormula.SelectedValue;
        }

        private void tbLeadDiameter_KeyPress(object sender, KeyPressEventArgs e)
        {
            e.Handled = !ServiceFunctions.DoubleCharChecker(e.KeyChar);
        }

        private void tbLeadDiameter_TextChanged(object sender, EventArgs e)
        {
            float v = currentStructure.LeadDiameter;
            if (String.IsNullOrWhiteSpace(tbLeadDiameter.Text)) return;
            if (float.TryParse(tbLeadDiameter.Text, out v))
            {
                currentStructure.LeadDiameter = v;
            }else
            {
                tbLeadDiameter.Text = v.ToString();
                MessageBox.Show("Неверный формат величины диаметра жил", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        private void nudRrealElementsAmount_ValueChanged(object sender, EventArgs e)
        {
            uint real = (uint)nudRrealElementsAmount.Value;
            uint nominal = (uint)nudDisplayedElementAmount.Value;
            currentStructure.RealAmount = real;
            if (real < nominal)
            {
                nudDisplayedElementAmount.Value = real;
            }
        }

        private void nudDisplayedElementAmount_ValueChanged(object sender, EventArgs e)
        {
            uint real = (uint)nudRrealElementsAmount.Value;
            uint nominal = (uint)nudDisplayedElementAmount.Value;
            currentStructure.DisplayedAmount = nominal;
            if (nominal > real)
            {
                nudRrealElementsAmount.Value = nominal;
            }
        }

        private uint ProcWrittenVoltage(object sender, uint valWas)
        {
            NumericUpDown input = sender as NumericUpDown;
            uint val = (uint)input.Value;
            
            if (val > 0 && val < 500)
            {
                input.Value = valWas < val ? 500 : 0;
                return (uint)input.Value;
            }
            else
            {
                return val;
            }
        }

        private void numericUpDown5_ValueChanged(object sender, EventArgs e)
        {
            currentStructure.LeadToLeadTestVoltage = ProcWrittenVoltage(sender, currentStructure.LeadToLeadTestVoltage);
        }

        private void numericUpDown6_ValueChanged(object sender, EventArgs e)
        {
            currentStructure.LeadToShieldTestVoltage = ProcWrittenVoltage(sender, currentStructure.LeadToShieldTestVoltage);
        }

        private void cbGroupCapacity_CheckedChanged(object sender, EventArgs e)
        {
            currentStructure.WorkCapacityGroup = cbGroupCapacity.Checked;
        }

        private void nudWaveResistance_ValueChanged(object sender, EventArgs e)
        {
            currentStructure.WaveResistance = (uint)nudWaveResistance.Value;
        }

        private void nudNumberInGroup_ValueChanged(object sender, EventArgs e)
        {
            currentStructure.GroupedAmount = (uint)nudNumberInGroup.Value;
        }

        private void dgMeasuredParameters_RowsAdded(object sender, DataGridViewRowsAddedEventArgs e)
        {
             for(int i = 0; i< dgMeasuredParameters.Rows.Count; i++)
              {
                    InitRowByParameterType(dgMeasuredParameters.Rows[i]);
              }
            dgMeasuredParameters.Sort(dgMeasuredParameters.Columns[parameter_type_id_column.Name], System.ComponentModel.ListSortDirection.Ascending);
            //deleteAllMeasuredParametersDataButton.Enabled = MeasuredParamsDataGridView.Rows.Count > 0;
        }



        private void InitRowByParameterType(DataGridViewRow r)
        {
            try
            {
                DataGridViewTextBoxCell cell = r.Cells[parameter_type_id_column.Name] as DataGridViewTextBoxCell;

                uint val = 0;
                uint.TryParse($"{cell.Value}", out val);

                bool isFreqParams = MeasuredParameterType.IsItFreqParameter(val);
                bool hasMaxValue = MeasuredParameterType.IsHasMaxLimit(val);
                bool hasMinValue = MeasuredParameterType.IsHasMinLimit(val);
                bool allowBringingLength = MeasuredParameterType.AllowBringingLength(val) && ((uint)r.Cells[lengthBringingTypeIdColumn.Name].Value == LengthBringingType.ForAnotherLengthInMeters);

                r.Cells[parameter_type_name_column.Name].ToolTipText = r.Cells[parameterTypeDescriptionColumn.Name].Value.ToString();
                r.Cells[freqMaxColumn.Name].ReadOnly = r.Cells[freqStepColumn.Name].ReadOnly = r.Cells[frequencyMinColumn.Name].ReadOnly = !isFreqParams;


                r.Cells[minValueColumn.Name].ReadOnly = !hasMinValue;
                r.Cells[maxValueColumn.Name].ReadOnly = !hasMaxValue;
                r.Cells[percentColumn.Name].ReadOnly = false;
                r.Cells[lengthBringingTypeIdColumn.Name].ReadOnly = false;
                r.Cells[lengthBringingColumn.Name].ReadOnly = !allowBringingLength;

                refreshReadOnlyCellColor(r);

            }
            catch (NullReferenceException) { }

        }

        private void refreshReadOnlyCellColor(DataGridViewRow row)
        {
            string[] ExceptedColumns = new string[] {
                parameter_type_name_column.Name,
                parameterTypeMeasureColumn.Name
            };
            foreach (DataGridViewCell c in row.Cells)
            {
                if (!c.Visible || Array.IndexOf(ExceptedColumns, c.OwningColumn.Name) > -1) continue;
                else if (lengthBringingColumn.Name == c.OwningColumn.Name)
                {
                    ReStyleBringingLengthColumnByBringingLengthType(c.OwningRow);
                }
                else
                {
                    c.Style = c.ReadOnly ? DisabledCellStyle : EnabledCellStyle;
                }
            }
        }

        private void ReStyleBringingLengthColumnByBringingLengthType(DataGridViewRow r)
        {
            uint blId = (uint)r.Cells[lengthBringingTypeIdColumn.Name].Value;
            uint pTypeId = (uint)r.Cells[parameter_type_id_column.Name].Value;
            DataGridViewCell cell = r.Cells[lengthBringingColumn.Name];
            if (MeasuredParameterType.AllowBringingLength(pTypeId))
            {
                cell.ReadOnly = blId != LengthBringingType.ForAnotherLengthInMeters;
                cell.Style = (blId == LengthBringingType.NoBringing) ? DisabledBringingLengthCellStyle : EnabledBringingLengthCellStyle;
            }
            else
            {
                cell.ReadOnly = true;
                cell.Style = DisabledCellStyle;
            }
   
        }

        private void RemoveParameterDataByIndex(int rowIndex)
        {
            int start = rowIndex == -1 ? 0 : rowIndex;
            int end = rowIndex == -1 ? currentStructure.MeasuredParameters.Rows.Count-1 : rowIndex;
            bool willDelete = rowIndex == -1 ? MessageBox.Show("Вы уверены, что хотите удалить все измеряемые параметры из текущей структуры?") == DialogResult.OK : true;
            if (!willDelete) return;
            MessageBox.Show($"{start} - {end} from {currentStructure.MeasuredParameters.Rows.Count}");
            for(int i = start; i <= end; i++)
            {
                CableStructureMeasuredParameterData mpd = currentStructure.MeasuredParameters.Rows[i] as CableStructureMeasuredParameterData;
                MessageBox.Show(i.ToString());
                willDelete = (!mpd.IsNewRecord()) ? mpd.Destroy() : true;
                if (willDelete) currentStructure.MeasuredParameters.Rows.Remove(mpd);
            }
        }

        private System.Windows.Forms.DataGridViewCellStyle BuildParameterNameCellStyle()
        {
            System.Windows.Forms.DataGridViewCellStyle parameterNameCellStyle = new DataGridViewCellStyle();
            parameterNameCellStyle.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            parameterNameCellStyle.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(65)))), ((int)(((byte)(109)))));
            parameterNameCellStyle.Font = new System.Drawing.Font("Tahoma", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            parameterNameCellStyle.ForeColor = System.Drawing.Color.Gainsboro;
            parameterNameCellStyle.NullValue = "-";
            parameterNameCellStyle.Padding = new System.Windows.Forms.Padding(3);
            parameterNameCellStyle.SelectionBackColor = parameterNameCellStyle.BackColor;
            parameterNameCellStyle.SelectionForeColor = System.Drawing.Color.Gainsboro;
            parameterNameCellStyle.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            return parameterNameCellStyle;
        }

        private System.Windows.Forms.DataGridViewCellStyle BuildParameterNameHeaderStyle()
        {
            System.Windows.Forms.DataGridViewCellStyle style = new DataGridViewCellStyle();
            style.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            style.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(36)))), ((int)(((byte)(201)))), ((int)(((byte)(0)))));
            style.Font = new System.Drawing.Font("Tahoma", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            style.ForeColor = System.Drawing.Color.Gainsboro;
            style.NullValue = "-";
            style.Padding = new System.Windows.Forms.Padding(3);
       
            style.SelectionBackColor = System.Drawing.SystemColors.Highlight; 
            style.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            style.WrapMode = System.Windows.Forms.DataGridViewTriState.True;

            return style;
        }

        private System.Windows.Forms.DataGridViewCellStyle BuildDelButtonCellStyle()
        {
            System.Windows.Forms.DataGridViewCellStyle style = new DataGridViewCellStyle();
            style.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            style.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))));
            style.Font = new System.Drawing.Font("Tahoma", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            style.ForeColor = System.Drawing.Color.Gainsboro;
            style.NullValue = "x";
            style.Padding = new System.Windows.Forms.Padding(3);

            style.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            style.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            style.WrapMode = System.Windows.Forms.DataGridViewTriState.True;

            return style;
        }

        private void addMeasurerParameterContextMenu_Opening(object sender, CancelEventArgs e)
        {
            if (cbStructureType.SelectedIndex == 0)
            {
               MessageBox.Show("Чтобы добавить измеряемые параметры, необходимо выбрать тип структуры", "Не выбран тип структуры", MessageBoxButtons.OK, MessageBoxIcon.Information);
               e.Cancel = true;
            }
        }

        int selIndexWas = 0;
        private void cbStructureType_SelectionChangeCommitted(object sender, EventArgs e)
        {
            Debug.WriteLine($"cbStructureType_SelectionChangeCommitted {cbStructureType.SelectedValue}");
           // int nextStructureId = (int)cbStructureType.SelectedValue;
           if (!GetAgreementToStructureTypeChanged())
            {
                cbStructureType.SelectedIndex = selIndexWas;
            }
        }

        private bool GetAgreementToStructureTypeChanged()
        {
            CableStructureType t = GetSelectedCableStructureType();
            if (selIndexWas == 0)
            {
                if (cbStructureType.SelectedIndex != 0)
                {
                    RefreshExcludedParameterTypes();
                    return true;
                }
                else
                {
                    return false;
                } 
            }else
            {
                if (cbStructureType.SelectedIndex != 0)
                {
                    RefreshExcludedParameterTypes();
                    return HasAgreeToChangeAStructureType();
                }
                else
                {
                    return false;
                }
            }
        }

        private bool HasAgreeToChangeAStructureType()
        {
            string measuredParametersToDestroy = string.Empty;
            FillMeasuredParametersToDelete(out measuredParametersToDestroy);
            Debug.WriteLine("Check agreement HasAgreeToChangeAStructureType");
            if (!string.IsNullOrWhiteSpace(measuredParametersToDestroy))
            {
                return MessageBox.Show($"При изменении типа структуры из списка измеряемых параметров будут исключены:\n\n{measuredParametersToDestroy}\n\nВы согласны?", "Вопрос", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes;
            } else return true;
        }

        private void FillMeasuredParametersToDelete(out string deletedParameters)
        {
            List<string> names = new List<string>();
            deletedParameters = "";
            if (ExcludedParameterTypes.Length > 0 && currentStructure.MeasuredParameters.Rows.Count > 0)
            {
                IEnumerable<string> val = ExcludedParameterTypes.Select((v) => (v as MeasuredParameterType).ParameterTypeId.ToString());
                DataRow[] rows = currentStructure.MeasuredParameters.Select($"{MeasuredParameterType.ParameterTypeId_ColumnName} IN ({string.Join(",", val)})");
                val = rows.Select((v) => (v as CableStructureMeasuredParameterData).ParameterName);
                if (val.Count() > 0)deletedParameters = string.Join(", ", val) + ";";
            }
        }

        DataRow[] ExcludedParameterTypes;
        private void RefreshExcludedParameterTypes()
        {
            CableStructureType t = GetSelectedCableStructureType();
            string[] nextStructureTypeParamIDs = t.StructureMeasuredParameters.Split(',');
            List<string> allStructureTypeParamIDs = new List<string>();
            foreach (MeasuredParameterType mpt in cableFormDataSet.Tables["measured_parameter_types"].Rows) allStructureTypeParamIDs.Add(mpt.ParameterTypeId.ToString());
            IEnumerable<string> v = allStructureTypeParamIDs.ToArray().Except(nextStructureTypeParamIDs);
            if (v.Count<string>() > 0)
            {
                ExcludedParameterTypes = cableFormDataSet.Tables["measured_parameter_types"].Select($"{MeasuredParameterType.ParameterTypeId_ColumnName} IN ({string.Join(", ", v)})");
            }
            else
            {
                ExcludedParameterTypes = new DataRow[] { };
            }
        }


    }
}