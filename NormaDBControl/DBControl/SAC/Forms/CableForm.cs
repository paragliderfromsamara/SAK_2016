﻿using System;
using System.Windows.Forms;
using NormaMeasure.DBControl.Tables;
using System.Data;



namespace NormaMeasure.DBControl.SAC.Forms
{
    public partial class CableForm : Form
    {
        private bool isNew = false;
        public bool IsNew => isNew;
        private Cable cable;

        public Cable Cable => cable;
        public CableForm()
        {
            isNew = true;
            InitializeComponent();
            cable = Cable.GetDraft();
            if (cable != null) InitFormByAssignedCable();
        }

        public CableForm(uint cable_id)
        {
            isNew = false;
            InitializeComponent();
            cable = Cable.find_by_cable_id(cable_id);
            InitFormByAssignedCable();

        }

        private void InitFormByAssignedCable()
        {
            fillDBData();
            fillFormByCable();
            InitStructuresTabControl();
        }


        private void fillDBData()
        {
            fillCableMarks();
            fillDocuments();
            fillStructureTypes();
            fillLeadMaterials();
            fillIsolationMaterials();
            fillDRBringingFormuls();
            fillDRFormuls();
        }



        #region Заполнение cableFormDataSet необходимыми таблицами
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

        private void fillStructureTypes()
        {
            DBEntityTable t = CableStructureType.get_all_as_table();
            cableFormDataSet.Tables.Add(t);
            structureTypesComboBox.DataSource = t;
            structureTypesComboBox.ValueMember = "structure_type_id";
            structureTypesComboBox.DisplayMember = "structure_type_name";
        }

        private void fillDocuments()
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

        private void fillCableMarks()
        {
            cableFormDataSet.Tables.Add(Cable.get_cable_marks());
            CableMark_input.DisplayMember = CableMark_input.ValueMember = "cable_marks.cable_mark";
            CableMark_input.Refresh();
        }


        #endregion

        private string reloadDocumentsDS()
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



        private void cableNameTextField_TextChanged(object sender, System.EventArgs e)
        {
            TextBox txt = sender as TextBox;
            cable.Name = txt.Text;
            
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

        private void fillFormByCable()
        {
            this.Text = (cable.IsDraft) ? "Новый кабель" : cable.Name ;
            CableMark_input.Text = cable.Name;
            CableStructures_input.Text = cable.StructName;
            //DocumentName_input.Text = cable.DocumentName;
            //DocumentNumber_input.Text = cable.DocumentNumber;
            BuildLength_input.Value = (decimal)cable.BuildLength;
            linearMass_input.Value = (decimal)cable.LinearMass;
            Ucover_input.Value = (decimal)cable.UCover;
            Pmin_input.Value = (decimal)cable.PMin;
            Pmax_input.Value = (decimal)cable.PMax;
            CodeOKP_input.Text = cable.CodeOKP;
            CodeKCH_input.Text = cable.CodeKCH;
            Notes_input.Text = cable.Notes;

            //////this.Text = $"{cable.id}";
        }

        private void BuildLength_input_ValueChanged(object sender, System.EventArgs e)
        {
           float v = 0f;
           float.TryParse((sender as NumericUpDown).Value.ToString(), out v);
           cable.BuildLength = v;
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

        private void Pmin_input_ValueChanged(object sender, System.EventArgs e)
        {
            cable.PMin = (int)(sender as NumericUpDown).Value;
        }

        private void Pmax_input_ValueChanged(object sender, System.EventArgs e)
        {
            cable.PMax = (int)(sender as NumericUpDown).Value;
        }


        private void CableMark_input_SelectedIndexChanged(object sender, System.EventArgs e)
        {
            cable.Name = (sender as ComboBox).Text;
            //MessageBox.Show(cable.Name);
        }

        private void Notes_input_TextChanged(object sender, System.EventArgs e)
        {
            cable.Notes = (sender as RichTextBox).Text;
        }

        private void CableStructures_input_TextChanged(object sender, System.EventArgs e)
        {
            cable.StructName = (sender as TextBox).Text;
        }



        private void DocumentNumberInput_Changed(object sender, EventArgs e)
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


        private void button1_Click(object sender, EventArgs e)
        {
            //fillFormByCable();
            string s = String.Empty;
            DBEntityTable t = new DBEntityTable(typeof(CableStructure));
            foreach (CableStructure cs in cable.CableStructures.Rows)
            {
                s += $"{cs.DisplayedAmount};";
            }
            MessageBox.Show(s);
        }

        private void CodeKCH_input_TextChanged(object sender, EventArgs e)
        {
            cable.CodeKCH = (sender as MaskedTextBox).Text;
        }

        private void CodeOKP_input_TextChanged(object sender, EventArgs e)
        {
            cable.CodeOKP = (sender as MaskedTextBox).Text;
        }

        private void DocumentName_input_TextChanged(object sender, EventArgs e)
        {
            TextBox tb = sender as TextBox;
            if (tb.Enabled)
            {
                Document d = getDocumentDraftFromDataTable();
                d.FullName = tb.Text;
                cable.QADocument = d;
            }
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

        #region Управление структурами кабеля
        private void addStructureButton_Click(object sender, EventArgs e)
        {
            Random r = new Random();
            uint strTypeId = (uint)structureTypesComboBox.SelectedValue;
            CableStructure draft = (CableStructure)cable.CableStructures.NewRow();
            draft.CableStructureId = (uint)r.Next(9000000, 10000000); //(cable.CableStructures.Rows.Count > 0) ? ((CableStructure)cable.CableStructures.Rows[cable.CableStructures.Rows.Count-1]).CableStructureId + 1 : CableStructure.get_last_structure_id() + 1;
            draft.CableId = cable.CableId;
            draft.StructureTypeId = strTypeId;
            draft.LeadMaterialTypeId = 1;
            draft.IsolationMaterialId = 1;
            draft.LeadDiameter = 0.1f;
            draft.WaveResistance = 0;
            draft.LeadToLeadTestVoltage = 0;
            draft.LeadToShieldTestVoltage = 0;
            draft.DRBringingFormulaId = 1;
            draft.DRFormulaId = 1;
            cable.CableStructures.Rows.Add(draft);
            AddCableStructureTabPage(draft);
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

        private void InitStructuresTabControl()
        {
            CableStructureTabs.TabPages.Clear();
            foreach(CableStructure s in cable.CableStructures.Rows)
            {
                if (s.RowState != DataRowState.Deleted || s.RowState == DataRowState.Detached)AddCableStructureTabPage(s);
            }
           // CableStructureTabs.re
        }


        private void AddCableStructureTabPage(CableStructure structure)
        {
            CableStructureTabPage tp = new CableStructureTabPage(structure, cableFormDataSet);
            tp.DeleteStructureButton.Click += DeleteStructureButton_Click;
            CableStructureTabs.TabPages.Add(tp);
            CableStructureTabs.SelectedIndex = CableStructureTabs.TabPages.Count - 1;
            CableStructureTabs.SelectedTab = tp;
            CableStructureTabs.Refresh();
        }

        private void DeleteStructureButton_Click(object sender, EventArgs e)
        {
            CableStructureTabPage tp = (CableStructureTabPage)(sender as Button).Parent;
            DialogResult r = MessageBox.Show($"Вы уверены, что хотите удалить структуру кабеля {tp.CableStructure.StructureTitle}?", "Вопрос...", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if (r == DialogResult.Yes)
            {
                // MessageBox.Show(tp.CableStructure.makeDestroyQuery());
                if (tp.CableStructure.Destroy())
                {
                    CableStructureTabs.TabPages[CableStructureTabs.SelectedIndex].Dispose();
                    CableStructureTabs.Refresh();
                }
                else MessageBox.Show("Не удалось удалить структуру кабеля!");
            }
            
        }
        #endregion


    }

    class CableStructureTabPage : TabPage
    {
        public CableStructure CableStructure;
        public CableStructureTabPage(CableStructure structure, DataSet cableFormDS)
        {
            this.CableStructure = structure;
            DrawElements();
            FillFromDataSet(cableFormDS);
            FillCableStructureData();
            InitTabPage();
        }


        /// <summary>
        /// Заполняем страницу структуры в соответствии с данными структуры
        /// </summary>
        private void FillCableStructureData()
        {
            StructureTypeLabel.Text += CableStructure.StructureType.StructureTypeName;
            StructureTypeLabel.Refresh();

            realElementsAmount.Value = CableStructure.RealAmount;
            realElementsAmount.ValueChanged += RealElementsAmount_ValueChanged;

            shownElementsAmount.Value = CableStructure.DisplayedAmount;
            shownElementsAmount.ValueChanged += ShownElementsAmount_ValueChanged;

            LeadMaterialComboBox.SelectedValue = CableStructure.LeadMaterialTypeId;
            LeadMaterialComboBox.SelectedValueChanged += LeadMaterialComboBox_SelectedValueChanged;

            LeadDiametersTextBox.Text = CableStructure.LeadDiameter.ToString();
            LeadDiametersTextBox.TextChanged += LeadDiametersTextBox_TextChanged;

            IsolationMaterialComboBox.SelectedValue = CableStructure.IsolationMaterialId;
            IsolationMaterialComboBox.SelectedValueChanged += IsolationMaterialComboBox_SelectedValueChanged;

            WaveResistanceComboBox.Text = CableStructure.WaveResistance.ToString();
            WaveResistanceComboBox.TextChanged += WaveResistanceComboBox_TextChanged;

            ElementsInGroupNumericUpDown.Value = CableStructure.GroupedAmount;
            ElementsInGroupNumericUpDown.ValueChanged += ElementsInGroupNumericUpDown_ValueChanged;

            TestVoltageLeadLeadNumericUpDown.Value = CableStructure.LeadToLeadTestVoltage;
            TestVoltageLeadLeadNumericUpDown.ValueChanged += TestVoltageLeadLeadNumericUpDown_ValueChanged;

            TestVoltageLeadShieldNumericUpDown.Value = CableStructure.LeadToShieldTestVoltage;
            TestVoltageLeadShieldNumericUpDown.ValueChanged += TestVoltageLeadShieldNumericUpDown_ValueChanged;

            DRFormulaComboBox.SelectedValue = CableStructure.DRFormulaId;
            DRFormulaComboBox.SelectedValueChanged += DRFormulaComboBox_SelectedValueChanged;

            DRBringingFormulaComboBox.SelectedValue = CableStructure.DRBringingFormulaId;
            DRBringingFormulaComboBox.SelectedValueChanged += DRBringingFormulaComboBox_SelectedValueChanged;


            GroupCapacityCheckBox.Checked = CableStructure.WorkCapacityGroup;
            GroupCapacityCheckBox.CheckedChanged += GroupCapacityCheckBox_CheckedChanged;

            
            

        }

        private void GroupCapacityCheckBox_CheckedChanged(object sender, EventArgs e)
        {
            CableStructure.WorkCapacityGroup = (sender as CheckBox).Checked;
        }

        private void DRBringingFormulaComboBox_SelectedValueChanged(object sender, EventArgs e)
        {
            CableStructure.DRBringingFormulaId = (uint)(sender as ComboBox).SelectedValue;
        }

        private void DRFormulaComboBox_SelectedValueChanged(object sender, EventArgs e)
        {
            CableStructure.DRFormulaId = (uint)(sender as ComboBox).SelectedValue;
        }

        private void TestVoltageLeadShieldNumericUpDown_ValueChanged(object sender, EventArgs e)
        {
            CableStructure.LeadToShieldTestVoltage = ProcWrittenVoltage(sender, CableStructure.LeadToShieldTestVoltage);
        }

        private void TestVoltageLeadLeadNumericUpDown_ValueChanged(object sender, EventArgs e)
        {
             CableStructure.LeadToLeadTestVoltage = ProcWrittenVoltage(sender, CableStructure.LeadToLeadTestVoltage);
        }

        private uint ProcWrittenVoltage(object sender, uint valWas)
        {
            NumericUpDown input = sender as NumericUpDown;
            uint val = (uint)input.Value;
            if (val > 0 && val < 500)
            {
                input.Value = valWas < 500 ? 500 : 0;
                return valWas;
            }
            else
            {
                return val;
            }
        }



        private void ElementsInGroupNumericUpDown_ValueChanged(object sender, EventArgs e)
        {
            CableStructure.GroupedAmount = (uint)(sender as NumericUpDown).Value;
        }

        private void WaveResistanceComboBox_TextChanged(object sender, EventArgs e)
        {
            float val = 0;
            ComboBox cb = sender as ComboBox;
            if (String.IsNullOrWhiteSpace(cb.Text)) return;
            if (float.TryParse(cb.Text, out val))
            {
                CableStructure.WaveResistance = val;
            }
            else
            {
                cb.Text = CableStructure.WaveResistance.ToString();
                MessageBox.Show("Неверный формат числа", "Ошибка ввода", MessageBoxButtons.OK, MessageBoxIcon.Stop);
            }
            
        }

        private void IsolationMaterialComboBox_SelectedValueChanged(object sender, EventArgs e)
        {
            CableStructure.IsolationMaterialId = (uint)(sender as ComboBox).SelectedValue;
        }

        private void LeadDiametersTextBox_TextChanged(object sender, EventArgs e)
        {
            float val = 0;
            TextBox tb = sender as TextBox;
            if (String.IsNullOrWhiteSpace(tb.Text)) return;
            if (float.TryParse(tb.Text, out val))
            {
                CableStructure.LeadDiameter = val;
            }else
            {
                tb.Text = CableStructure.LeadDiameter.ToString();
                MessageBox.Show("Неверный формат числа", "Ошибка ввода", MessageBoxButtons.OK, MessageBoxIcon.Stop);
            }
        }

        private void LeadMaterialComboBox_SelectedValueChanged(object sender, EventArgs e)
        {
            CableStructure.LeadMaterialTypeId = (uint)(sender as ComboBox).SelectedValue;
        }

        private void ShownElementsAmount_ValueChanged(object sender, EventArgs e)
        {
            uint real = (uint)realElementsAmount.Value;
            uint nominal = (uint)shownElementsAmount.Value;
            CableStructure.DisplayedAmount = nominal;
            if (nominal > real)
            {
                realElementsAmount.Value = nominal;
            }
        }

        private void RealElementsAmount_ValueChanged(object sender, EventArgs e)
        {
            uint real = (uint)realElementsAmount.Value;
            uint nominal = (uint)shownElementsAmount.Value;
            CableStructure.RealAmount= real;
            if (real < nominal)
            {
                shownElementsAmount.Value = real;
            }
            //shownElementsAmount.Maximum = real;


        }

        /// <summary>
        /// Заполняем исходными данными ComboBox-ы 
        /// </summary>
        /// <param name="ds"></param>
        private void FillFromDataSet(DataSet ds)
        {
            //DBEntityTable structureTypesTable = new DBEntityTable(typeof(CableStructureType));
            DBEntityTable leadMaterialsTable = new DBEntityTable(typeof(LeadMaterial));
            DBEntityTable isolationMaterialsTable = new DBEntityTable(typeof(IsolationMaterial));
            DBEntityTable drBringingFormulsTable = new DBEntityTable(typeof(dRBringingFormula));
            DBEntityTable drFormulsTable = new DBEntityTable(typeof(dRFormula));
            DBEntityTable measuredParameterTypesTable = MeasuredParameterType.get_all_as_table_for_cable_structure_form(CableStructure.StructureType.StructureMeasuredParameters);

            //if (ds.Tables.Contains(structureTypesTable.TableName)) fillStructureTypeComboBox(ds.Tables[structureTypesTable.TableName]);
            if (ds.Tables.Contains(leadMaterialsTable.TableName)) fillLeadMaterialsComboBox(ds.Tables[leadMaterialsTable.TableName]);
            if (ds.Tables.Contains(isolationMaterialsTable.TableName)) fillIsolationMaterialsComboBox(ds.Tables[isolationMaterialsTable.TableName]);
            if (ds.Tables.Contains(drFormulsTable.TableName)) fill_dRFormulsComboBox(ds.Tables[drFormulsTable.TableName]);
            if (ds.Tables.Contains(drBringingFormulsTable.TableName)) fill_drBringingFormulsComboBox(ds.Tables[drBringingFormulsTable.TableName]);

            fill_MeasuredParametersDataGrid(measuredParameterTypesTable);
        }

        private void fill_MeasuredParametersDataGrid(DataTable dataTable)
        {
            //DataRow[] currentStructureParams = dataTable.Select($"parameter_type_id IN ({CableStructure.StructureType.StructureMeasuredParameters})");
            
            //foreach (DataRow r in currentStructureParams) parameterNameColumn.Items.Add();

            parameterNameColumn.DisplayMember = "parameter_name";
            parameterNameColumn.ValueMember  = "parameter_type_id";

            parameterNameColumn.DataSource = dataTable; 
            //throw new NotImplementedException();
        }

        private void fill_drBringingFormulsComboBox(DataTable dt)
        {
            DataTable t = new DataTable();
            t.Merge(dt);
            DRBringingFormulaComboBox.BindingContext = new BindingContext();
            DRBringingFormulaComboBox.DataSource = t;
            DRBringingFormulaComboBox.ValueMember = "dr_bringing_formula_id";
            DRBringingFormulaComboBox.DisplayMember = "dr_bringing_formula_description";
        }

        private void fill_dRFormulsComboBox(DataTable dt)
        {
            DataTable t = new DataTable();
            t.Merge(dt);
            DRFormulaComboBox.BindingContext = new BindingContext();
            DRFormulaComboBox.DataSource = t;
            DRFormulaComboBox.ValueMember = "dr_formula_id";
            DRFormulaComboBox.DisplayMember = "dr_formula_description";
        }

        private void fillIsolationMaterialsComboBox(DataTable dt)
        {
            DataTable t = new DataTable();
            t.Merge(dt);
            IsolationMaterialComboBox.BindingContext = new BindingContext();
            IsolationMaterialComboBox.DataSource = t;
            IsolationMaterialComboBox.ValueMember = "isolation_material_id";
            IsolationMaterialComboBox.DisplayMember = "isolation_material_name";
        }

        private void fillLeadMaterialsComboBox(DataTable dt)
        {
            DataTable t = new DataTable();
            t.Merge(dt);
            LeadMaterialComboBox.BindingContext = new BindingContext();
            LeadMaterialComboBox.DataSource = t;
            LeadMaterialComboBox.ValueMember = "lead_material_id";
            LeadMaterialComboBox.DisplayMember = "lead_material_name";
        }

        private void InitTabPage()
        {
            this.Text = CableStructure.StructureTitle;
        }

        #region Инициализация элементов формы
        private void DrawElements()
        {
            DrawStructureTypeCB();
            DrawStructureElementsAmount();
            DrawLeadInfoElements();
            DrawIsolatonMaterialElements();
            DrawWaveResistanceAndInGroupAmount();
            DrawTestVoltageElements();
            DrawDRFormulsElements();
            DrawGroupCapacityElements();
            DrawDeleteButton();
            DrawMeasuredParametersDataGrid();

            SetElementsLocations();
        }

        private void DrawMeasuredParametersDataGrid()
        {
            MeasuredParamsDataGridView = new DataGridView();
            MeasuredParamsDataGridView.Size = new System.Drawing.Size(560, 350);
            MeasuredParamsDataGridView.Parent = this;

            parameterNameColumn = new DataGridViewComboBoxColumn();
            parameterNameColumn.DataPropertyName = "parameter_name";
            parameterNameColumn.HeaderText = "Параметр";
            //parameterNameColumn.Width = 60;
            parameterNameColumn.AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;

            minValueColumn = new DataGridViewTextBoxColumn();
            minValueColumn.HeaderText = "Min";
            minValueColumn.DataPropertyName = "min_value";
            minValueColumn.Width = 60;

            maxValueColumn = new DataGridViewTextBoxColumn();
            maxValueColumn.HeaderText = "Min";
            maxValueColumn.DataPropertyName = "max_value";
            maxValueColumn.Width = 60;

            minFreqColumn = new DataGridViewTextBoxColumn();
            minFreqColumn.HeaderText = "fmin, кГц";
            minFreqColumn.DataPropertyName = "min_freq";
            minFreqColumn.Width = 60;

            maxFreqColumn = new DataGridViewTextBoxColumn();
            maxFreqColumn.HeaderText = "fmax, кГц";
            maxFreqColumn.DataPropertyName = "max_freq";
            maxFreqColumn.Width = 60;

            stepFreqColumn = new DataGridViewTextBoxColumn();
            stepFreqColumn.HeaderText = "fшаг, кГц";
            stepFreqColumn.DataPropertyName = "step_freq";
            stepFreqColumn.Width = 60;

            percentColumn = new DataGridViewTextBoxColumn();
            percentColumn.HeaderText = "%";
            percentColumn.DataPropertyName = "in_norma_percent";
            percentColumn.Width = 35;

            measureColumn = new DataGridViewTextBoxColumn();
            measureColumn.HeaderText = "Ед. изм.";
            measureColumn.DataPropertyName = "result_measure";
            measureColumn.Width = 60;

            bringingLengthColumn = new DataGridViewTextBoxColumn();
            bringingLengthColumn.HeaderText = "Lприв, м";
            bringingLengthColumn.DataPropertyName = "bringing_length";
            bringingLengthColumn.Width = 60;


            MeasuredParamsDataGridView.Columns.Add(parameterNameColumn);
            MeasuredParamsDataGridView.Columns.Add(minValueColumn);
            MeasuredParamsDataGridView.Columns.Add(maxValueColumn);
            MeasuredParamsDataGridView.Columns.Add(percentColumn);
            MeasuredParamsDataGridView.Columns.Add(measureColumn);
            MeasuredParamsDataGridView.Columns.Add(bringingLengthColumn);
            MeasuredParamsDataGridView.Columns.Add(minFreqColumn);
            MeasuredParamsDataGridView.Columns.Add(stepFreqColumn);
            MeasuredParamsDataGridView.Columns.Add(maxFreqColumn);

        }

        private void DrawDeleteButton()
        {
            DeleteStructureButton = new Button();
            DeleteStructureButton.Size = new System.Drawing.Size(30, 30);
            DeleteStructureButton.Text = "X";
            DeleteStructureButton.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            DeleteStructureButton.Parent = this;
            DeleteStructureButton.UseVisualStyleBackColor = true;
            DeleteStructureButton.Cursor = Cursors.Hand;

        }

        private void DrawGroupCapacityElements()
        {
            GroupCapacityCheckBox = new CheckBox();
            GroupCapacityCheckBox.Parent = this;
            GroupCapacityCheckBox.Text = "Cр группы";
            GroupCapacityCheckBox.Checked = false;
            GroupCapacityCheckBox.CheckAlign = System.Drawing.ContentAlignment.BottomCenter;
            GroupCapacityCheckBox.AutoSize = true;
        }

        private void DrawDRFormulsElements()
        {
            DRFormulsGroupBox = new GroupBox();
            DRFormulsGroupBox.Parent = this;
            DRFormulsGroupBox.Text = "Омическая ассиметрия";
            DRFormulsGroupBox.Width = 155;
            DRFormulsGroupBox.Height = 110;


           
            DRFormulaComboBox = new ComboBox();
            DRFormulaComboBox.Parent = DRFormulsGroupBox;
            DRFormulaComboBox.Width = 135;
            DRFormulaComboBox.DropDownStyle = ComboBoxStyle.DropDownList;
  

            DRBringingFormulaComboBox = new ComboBox();
            DRBringingFormulaComboBox.Parent = DRFormulsGroupBox;
            DRBringingFormulaComboBox.Width = 135;
            DRBringingFormulaComboBox.DropDownStyle = ComboBoxStyle.DropDownList;


            DRFormulaLabel = new Label();
            DRFormulaLabel.Parent = DRFormulsGroupBox;
            DRFormulaLabel.Text = "вычисление";

            DRBringingFormulaLabel = new Label();
            DRBringingFormulaLabel.Parent = DRFormulsGroupBox;
            DRBringingFormulaLabel.Text = "приведение";

            DRFormulaLabel.Location = new System.Drawing.Point(7, 20);
            DRBringingFormulaLabel.Location = new System.Drawing.Point(7, 60);
            DRFormulaComboBox.Location = new System.Drawing.Point(10, 35);
            DRBringingFormulaComboBox.Location = new System.Drawing.Point(10, 75);

        }

        private void DrawTestVoltageElements()
        {
            TestVoltagesGroupBox = new GroupBox();
            TestVoltagesGroupBox.Parent = this;
            TestVoltagesGroupBox.Text = "Испытательные напряжения, В";
            TestVoltagesGroupBox.Width = 210;
            TestVoltagesGroupBox.Height = 70;

            TestVoltageLeadLeadNumericUpDown = new NumericUpDown();
            TestVoltageLeadLeadNumericUpDown.Parent = TestVoltagesGroupBox;
            TestVoltageLeadLeadNumericUpDown.Width = 90;
            TestVoltageLeadLeadNumericUpDown.Minimum = 0;
            TestVoltageLeadLeadNumericUpDown.Maximum = 3000;
            TestVoltageLeadLeadNumericUpDown.Value = 0;

            TestVoltageLeadShieldNumericUpDown = new NumericUpDown();
            TestVoltageLeadShieldNumericUpDown.Parent = TestVoltagesGroupBox;
            TestVoltageLeadShieldNumericUpDown.Width = 90;
            TestVoltageLeadShieldNumericUpDown.Minimum = 0;
            TestVoltageLeadShieldNumericUpDown.Maximum = 3000;
            TestVoltageLeadShieldNumericUpDown.Value = 0;

            TestVoltageLeadLeadLabel = new Label();
            TestVoltageLeadLeadLabel.Parent = TestVoltagesGroupBox;
            TestVoltageLeadLeadLabel.Text = "жила-жила";

            TestVoltageLeadShieldLabel = new Label();
            TestVoltageLeadShieldLabel.Parent = TestVoltagesGroupBox;
            TestVoltageLeadShieldLabel.Text = "жила-экран";

            TestVoltageLeadLeadLabel.Location = new System.Drawing.Point(7, 20);
            TestVoltageLeadShieldLabel.Location = new System.Drawing.Point(107, 20);
            TestVoltageLeadLeadNumericUpDown.Location = new System.Drawing.Point(10, 35);
            TestVoltageLeadShieldNumericUpDown.Location = new System.Drawing.Point(110, 35);


        }

        private void DrawWaveResistanceAndInGroupAmount()
        {
            WaveResistanceComboBox = new ComboBox();
            WaveResistanceComboBox.Parent = this;
            WaveResistanceComboBox.Width = 150;

            WaveResistanceLabel = new Label();
            WaveResistanceLabel.Parent = this;
            WaveResistanceLabel.Width = 160;
            WaveResistanceLabel.Text = "Волновое сопротивление, Ом";

            ElementsInGroupNumericUpDown = new NumericUpDown();
            ElementsInGroupNumericUpDown.Parent = this;
            ElementsInGroupNumericUpDown.Width = 50;
            ElementsInGroupNumericUpDown.Minimum = 0;
            ElementsInGroupNumericUpDown.Maximum = 10000;

            ElementsInGroupAmountLabel = new Label();
            ElementsInGroupAmountLabel.Parent = this;
            ElementsInGroupAmountLabel.Text = "В пучке";
            ElementsInGroupAmountLabel.Width = 50;


        }

        private void DrawIsolatonMaterialElements()
        {
            IsolationMaterialComboBox = new ComboBox();
            IsolationMaterialComboBox.Parent = this;
            IsolationMaterialComboBox.Width = 210;
            IsolationMaterialComboBox.DropDownStyle = ComboBoxStyle.DropDownList;

            IsolationMaterialLabel = new Label();
            IsolationMaterialLabel.Parent = this;
            IsolationMaterialLabel.Width = 210;
            IsolationMaterialLabel.Text = "Материал изоляции";
        }

        private void DrawLeadInfoElements()
        {
            LeadMaterialComboBox = new ComboBox();
            LeadMaterialComboBox.Parent = this;
            LeadMaterialComboBox.Width = 110;
            LeadMaterialComboBox.DropDownStyle = ComboBoxStyle.DropDownList;

            LeadDiametersTextBox = new TextBox();
            LeadDiametersTextBox.Parent = this;
            LeadDiametersTextBox.Width = 90;

            LeadMaterialLabel = new Label();
            LeadMaterialLabel.Parent = this;
            LeadMaterialLabel.Text = "Материал жил";

            LeadDiameterLabel = new Label();
            LeadDiameterLabel.Parent = this;
            LeadDiameterLabel.Text = "Диаметр жил, мм";

        }

        private void DrawStructureElementsAmount()
        {
            elementsAmountGroupBox = new GroupBox();
            elementsAmountGroupBox.Parent = this;
            elementsAmountGroupBox.Text = "Количество элементов структуры";
            elementsAmountGroupBox.Height = 70;
            elementsAmountGroupBox.Width = 210;


            realElementsAmount = new NumericUpDown();
            realElementsAmount.Parent = elementsAmountGroupBox;
            realElementsAmount.Minimum = 1;
            realElementsAmount.Maximum = 10000;
            realElementsAmount.Width = 90;
            realElementsAmount.Location = new System.Drawing.Point(110,35);


            shownElementsAmount = new NumericUpDown();
            shownElementsAmount.Parent = elementsAmountGroupBox;
            shownElementsAmount.Minimum = 1;
            shownElementsAmount.Maximum = 10000;
            shownElementsAmount.Width = 90;
            shownElementsAmount.Location = new System.Drawing.Point(10, 35);
            RealElementsAmountLabel = new Label();
            RealElementsAmountLabel.Parent = elementsAmountGroupBox;
            RealElementsAmountLabel.Text = "Реальное";
            RealElementsAmountLabel.Location = new System.Drawing.Point(107, 20);

            ShownElementsAmountLabel = new Label();
            ShownElementsAmountLabel.Parent = elementsAmountGroupBox;
            ShownElementsAmountLabel.Text = "Номинальное";
            ShownElementsAmountLabel.Location = new System.Drawing.Point(7, 20);




        }

        /// <summary>
        /// Расставляет элементы по TabConrol
        /// </summary>
        private void SetElementsLocations()
        {
            int x=10, y=10;

            StructureTypeLabel.Location = new System.Drawing.Point(x-5, y);
            //structureTypeComboBox.Location = new System.Drawing.Point(x, y += 20);
            
            MeasuredParamsDataGridView.Location = new System.Drawing.Point(240, y+40);
            DeleteStructureButton.Location = new System.Drawing.Point(MeasuredParamsDataGridView.Location.X + MeasuredParamsDataGridView.Width-DeleteStructureButton.Width, y);

            elementsAmountGroupBox.Location = new System.Drawing.Point(x, y+= 20);
            y += elementsAmountGroupBox.Height;

            LeadMaterialLabel.Location = new System.Drawing.Point(x-3, y+=10);
            LeadDiameterLabel.Location = new System.Drawing.Point(x+LeadMaterialComboBox.Width+7, y);
            LeadMaterialComboBox.Location = new System.Drawing.Point(x, y+=20);
            LeadDiametersTextBox.Location = new System.Drawing.Point(x + LeadMaterialComboBox.Width + 10, y);

            y += LeadDiametersTextBox.Height;

            IsolationMaterialLabel.Location = new System.Drawing.Point(x - 3, y += 10);
            IsolationMaterialComboBox.Location = new System.Drawing.Point(x, y += 20);

            y += IsolationMaterialComboBox.Height;

            WaveResistanceLabel.Location = new System.Drawing.Point(x-3, y += 10);
            ElementsInGroupAmountLabel.Location = new System.Drawing.Point(x+WaveResistanceComboBox.Width+7, y);
            WaveResistanceComboBox.Location = new System.Drawing.Point(x, y += 20);
            ElementsInGroupNumericUpDown.Location = new System.Drawing.Point(x + WaveResistanceComboBox.Width + 10, y);

            y += WaveResistanceComboBox.Height;

            TestVoltagesGroupBox.Location = new System.Drawing.Point(x, y+=10);

            y += TestVoltagesGroupBox.Height;

            DRFormulsGroupBox.Location = new System.Drawing.Point(x, y+=10);

            GroupCapacityCheckBox.Location = new System.Drawing.Point(x+DRFormulsGroupBox.Width, y+=10);
        }

        private void DrawStructureTypeCB()
        {
            /*
            structureTypeComboBox = new ComboBox();
            structureTypeComboBox.Parent = this;
            structureTypeComboBox.Width = 210;
            structureTypeComboBox.DropDownStyle = ComboBoxStyle.DropDownList;
            */
            StructureTypeLabel = new Label();
            StructureTypeLabel.Parent = this;
            StructureTypeLabel.Text = "Тип: ";
            StructureTypeLabel.Width = 210;
        }

        private GroupBox elementsAmountGroupBox;
        private NumericUpDown shownElementsAmount;
        private NumericUpDown realElementsAmount;

        private ComboBox structureTypeComboBox;

        private ComboBox LeadMaterialComboBox;
        private TextBox LeadDiametersTextBox;

        private NumericUpDown ElementsInGroupNumericUpDown;

        private ComboBox IsolationMaterialComboBox;
        private ComboBox WaveResistanceComboBox;

        private GroupBox DRFormulsGroupBox;
        private ComboBox DRBringingFormulaComboBox;
        private ComboBox DRFormulaComboBox;


        private GroupBox TestVoltagesGroupBox;
        private NumericUpDown TestVoltageLeadLeadNumericUpDown;
        private NumericUpDown TestVoltageLeadShieldNumericUpDown;

        private CheckBox GroupCapacityCheckBox;

        private Label IsolationMaterialLabel;
        private Label LeadDiameterLabel;
        private Label LeadMaterialLabel;
        private Label StructureTypeLabel;
        private Label RealElementsAmountLabel;
        private Label ShownElementsAmountLabel;
        private Label WaveResistanceLabel;
        private Label ElementsInGroupAmountLabel;
        private Label TestVoltageLeadLeadLabel;
        private Label TestVoltageLeadShieldLabel;
        private Label DRBringingFormulaLabel;
        private Label DRFormulaLabel;

        private DataGridView MeasuredParamsDataGridView;
        private DataGridViewComboBoxColumn parameterNameColumn;
        private DataGridViewTextBoxColumn minValueColumn;
        private DataGridViewTextBoxColumn maxValueColumn;
        private DataGridViewTextBoxColumn minFreqColumn;
        private DataGridViewTextBoxColumn maxFreqColumn;
        private DataGridViewTextBoxColumn stepFreqColumn;
        private DataGridViewTextBoxColumn percentColumn;
        private DataGridViewTextBoxColumn measureColumn;
        private DataGridViewTextBoxColumn bringingLengthColumn;



        public Button DeleteStructureButton;
        
        #endregion
    }

}
