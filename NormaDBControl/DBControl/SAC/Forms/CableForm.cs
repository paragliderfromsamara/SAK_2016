using System;
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
           Cable.BuildLength = v;
           refreshBuildLengthOnStructureMeasureParameters();
        }

        /// <summary>
        /// Обновляем длину приведения в измеряемых параметрах структур, в которых длина приведения - строительная длина кабеля
        /// </summary>
        private void refreshBuildLengthOnStructureMeasureParameters()
        {
            foreach(CableStructure structure in cable.CableStructures.Rows)
            {
                foreach(CableStructureMeasuredParameterData pd in structure.MeasuredParameters.Rows)
                {
                    if (pd.LengthBringingTypeId == LengthBringingType.ForBuildLength) pd.LengthBringing = Cable.BuildLength;
                    
                }
            }
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
            //DBEntityTable t = new DBEntityTable(typeof(CableStructure));
            //foreach (CableStructure cs in cable.CableStructures.Rows)
            //{
                s += $"{float.MinValue};";
            //}
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
            draft.OwnCable = cable;
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
            if (SaveSelectedStructure())
            {
                CableStructureTabPage tp = new CableStructureTabPage(structure, cableFormDataSet);
                tp.DeleteStructureButton.Click += DeleteStructureButton_Click;
                CableStructureTabs.TabPages.Add(tp);
                CableStructureTabs.SelectedTab = tp;
            }

        }

        /// <summary>
        /// Пытаемся сохранить текущую структуру
        /// </summary>
        /// <returns></returns>
        private bool SaveSelectedStructure()
        {
            if (CableStructureTabs.TabCount == 0) return true;
            CableStructureTabPage tp = CableStructureTabs.SelectedTab as CableStructureTabPage;
            return !tp.CableStructure.Save();
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

        private void CableStructureTabs_Deselecting(object sender, TabControlCancelEventArgs e)
        {
            //CableStructureTabPage page = e.TabPage as CableStructureTabPage;
            // MessageBox.Show(.CableStructure.StructureType.StructureTypeName);
            e.Cancel = !SaveSelectedStructure();
        }
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
        }

        /// <summary>
        /// Заполняем исходными данными ComboBox-ы 
        /// </summary>
        /// <param name="ds"></param>
        private void FillFromDataSet(DataSet ds)
        {
            DBEntityTable leadMaterialsTable = new DBEntityTable(typeof(LeadMaterial));
            DBEntityTable isolationMaterialsTable = new DBEntityTable(typeof(IsolationMaterial));
            DBEntityTable drBringingFormulsTable = new DBEntityTable(typeof(dRBringingFormula));
            DBEntityTable drFormulsTable = new DBEntityTable(typeof(dRFormula));
            DBEntityTable measuredParameterTypesTable = CableStructure.StructureType.MeasuredParameterTypes;
            DBEntityTable lengthBringingTypesTable = new DBEntityTable(typeof(LengthBringingType));

            if (ds.Tables.Contains(leadMaterialsTable.TableName)) fillLeadMaterialsComboBox(ds.Tables[leadMaterialsTable.TableName]);
            if (ds.Tables.Contains(isolationMaterialsTable.TableName)) fillIsolationMaterialsComboBox(ds.Tables[isolationMaterialsTable.TableName]);
            if (ds.Tables.Contains(drFormulsTable.TableName)) fill_dRFormulsComboBox(ds.Tables[drFormulsTable.TableName]);
            if (ds.Tables.Contains(drBringingFormulsTable.TableName)) fill_drBringingFormulsComboBox(ds.Tables[drBringingFormulsTable.TableName]);
            if (ds.Tables.Contains(lengthBringingTypesTable.TableName)) fill_lengthBringingContextMenu(ds.Tables[lengthBringingTypesTable.TableName]);

            fill_MeasuredParametersDataGrid(measuredParameterTypesTable);

            
        }

        private void fill_lengthBringingContextMenu(DataTable dataTable)
        {
            foreach(LengthBringingType type in dataTable.Rows)
            {
                ParameterTypesToolStripItem item = new ParameterTypesToolStripItem(type.TypeId, type.BringingName, type);
                lengthBringingColumnContextMenu.Items.Add(item);
            }
        }

        private void fill_MeasuredParametersDataGrid(DataTable dataTable)
        {
            DataGridViewBindingSource = new BindingSource();
            DataGridViewBindingSource.DataSource = CableStructure.MeasuredParameters;
            MeasuredParamsDataGridView.DataSource = DataGridViewBindingSource;
            SetMeasuredParameterDataGrid_ColumnsOrder();

            parameterTypesComboBox.BindingContext = new BindingContext();
            parameterTypesComboBox.ValueMember = "parameter_type_id";
            parameterTypesComboBox.DisplayMember = "parameter_name";
            parameterTypesComboBox.DataSource = dataTable;

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
            ParameterTypeNameCellStyle = new DataGridViewCellStyle();
            ParameterTypeNameCellStyle.ForeColor = ParameterTypeNameCellStyle.SelectionForeColor = System.Drawing.Color.MidnightBlue;
            ParameterTypeNameCellStyle.BackColor = ParameterTypeNameCellStyle.SelectionBackColor = System.Drawing.Color.MintCream;

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

            MeasuredParamsDataGridView = new DataGridView();
            MeasuredParamsDataGridView.Size = new System.Drawing.Size(600, 350);
            MeasuredParamsDataGridView.Parent = this;
            MeasuredParamsDataGridView.AllowUserToAddRows = false;
            MeasuredParamsDataGridView.AllowUserToResizeColumns = false;
            MeasuredParamsDataGridView.BackgroundColor = System.Drawing.Color.WhiteSmoke;
            MeasuredParamsDataGridView.AllowUserToOrderColumns = false;
            MeasuredParamsDataGridView.AutoGenerateColumns = false;
            MeasuredParamsDataGridView.MultiSelect = false;

            parameterTypeNameColumn = new DataGridViewTextBoxColumn();
            parameterTypeNameColumn.Name = "parameter_type_name_column";
            parameterTypeNameColumn.DataPropertyName = "parameter_name";
            parameterTypeNameColumn.HeaderText = "Параметр";
            parameterTypeNameColumn.AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
            parameterTypeNameColumn.CellTemplate = new DataGridViewTextBoxCell();
            parameterTypeNameColumn.CellTemplate.Style = ParameterTypeNameCellStyle;
            parameterTypeNameColumn.ReadOnly = true;


            parameterTypeIdColumn =new DataGridViewTextBoxColumn();
            parameterTypeIdColumn.Name = "parameter_type_id_column";
            parameterTypeIdColumn.DataPropertyName = "parameter_type_id";
            parameterTypeIdColumn.Visible = false;
            parameterTypeIdColumn.CellTemplate = new DataGridViewTextBoxCell();

            parameterTypeDescriptionColumn = new DataGridViewTextBoxColumn();
            parameterTypeDescriptionColumn.Name = "parameter_type_description_column";
            parameterTypeDescriptionColumn.DataPropertyName = "parameter_description";
            parameterTypeDescriptionColumn.Visible = false;


            parameterTypeMeasureColumn = new DataGridViewTextBoxColumn();
            parameterTypeMeasureColumn.Name = "parameter_type_measure_column";
            parameterTypeMeasureColumn.DataPropertyName = "parameter_measure";
            parameterTypeMeasureColumn.Visible = false;


            minValueColumn = new DataGridViewTextBoxColumn();
            minValueColumn.HeaderText = "Min";
            minValueColumn.DataPropertyName = "min_value";
            minValueColumn.Name = "min_value_column";
            minValueColumn.Width = 45;

            maxValueColumn = new DataGridViewTextBoxColumn();
            maxValueColumn.HeaderText = "Max";
            maxValueColumn.DataPropertyName = "max_value";
            maxValueColumn.Width = 45;
            maxValueColumn.Name = "max_value_column";


            percentColumn = new DataGridViewTextBoxColumn();
            percentColumn.Name = "percent_column";
            percentColumn.HeaderText = "%";
            percentColumn.DataPropertyName = "percent";
            percentColumn.Width = 35;

            measureColumn = new DataGridViewTextBoxColumn();
            measureColumn.HeaderText = "Ед. изм.";
            measureColumn.DataPropertyName = "result_measure";
            measureColumn.Name = "result_measure_column";
            measureColumn.Width = 90;
            measureColumn.CellTemplate = new DataGridViewTextBoxCell();
            measureColumn.CellTemplate.Style = ParameterTypeNameCellStyle;
            measureColumn.ReadOnly = true;

            bringingLengthColumn = new DataGridViewTextBoxColumn();
            bringingLengthColumn.HeaderText = "Lприв, м";
            bringingLengthColumn.DataPropertyName = "length_bringing";
            bringingLengthColumn.Name = "length_bringing_column_01";
            bringingLengthColumn.Width = 60;

            bool hasFreqParameters = CableStructure.HasFreqParameters;

            minFreqColumn = new DataGridViewTextBoxColumn();
            minFreqColumn.Name = "frequency_min_column";
            minFreqColumn.HeaderText = "fmin, кГц";
            minFreqColumn.DataPropertyName = "frequency_min";
            minFreqColumn.Width = 60;
            minFreqColumn.Visible = hasFreqParameters;

            maxFreqColumn = new DataGridViewTextBoxColumn();
            maxFreqColumn.HeaderText = "fmax, кГц";
            maxFreqColumn.DataPropertyName = "frequency_max";
            maxFreqColumn.Name = "frequency_max_column";
            maxFreqColumn.Width = 60;
            maxFreqColumn.Visible = hasFreqParameters;

            stepFreqColumn = new DataGridViewTextBoxColumn();
            stepFreqColumn.HeaderText = "fшаг, кГц";
            stepFreqColumn.DataPropertyName = "frequency_step";
            stepFreqColumn.Name = "frequency_step_column";
            stepFreqColumn.Width = 60;
            stepFreqColumn.Visible = hasFreqParameters;


            lengthBringingColumnContextMenu = new ContextMenuStrip();
            bringingLengthColumn.ContextMenuStrip = lengthBringingColumnContextMenu;
            lengthBringingColumnContextMenu.ItemClicked += LengthBringingColumnContextMenu_ItemClicked;
            lengthBringingColumnContextMenu.Opening += LengthBringingColumnContextMenu_Opening;
            lengthBringingColumnContextMenu.RenderMode = ToolStripRenderMode.System;


            freqRangeIdColumn = new DataGridViewTextBoxColumn();
            freqRangeIdColumn.DataPropertyName = "frequency_range_id";
            freqRangeIdColumn.Name = "frequency_range_id_column";
            freqRangeIdColumn.Visible = false;

            lengthBringingTypeIdColumn = new DataGridViewTextBoxColumn();
            lengthBringingTypeIdColumn.DataPropertyName = "length_bringing_type_id";
            lengthBringingTypeIdColumn.Name = "length_bringing_type_id_column";
            lengthBringingTypeIdColumn.Visible = false;

            measuredParameterDataIdColumn = new DataGridViewTextBoxColumn();
            measuredParameterDataIdColumn.DataPropertyName = "measured_parameter_data_id";
            measuredParameterDataIdColumn.Name = "measured_parameter_data_id_column";
            measuredParameterDataIdColumn.Visible = false;

            cableStructureIdColumn = new DataGridViewTextBoxColumn();
            cableStructureIdColumn.Name = "cable_structure_id_column";
            cableStructureIdColumn.DataPropertyName = "cable_structure_id";
            cableStructureIdColumn.Visible = false;

            lengthBringingMeasureTitleColumn = new DataGridViewTextBoxColumn();
            lengthBringingMeasureTitleColumn.Name = "bringing_length_measure_title_column";
            lengthBringingMeasureTitleColumn.DataPropertyName = "measure_title";
            lengthBringingMeasureTitleColumn.Visible = false;


            lengthBringtingMeasureNameColumn = new DataGridViewTextBoxColumn();
            lengthBringtingMeasureNameColumn.Name = "length_bringing_name_column";
            lengthBringtingMeasureNameColumn.DataPropertyName = "length_bringing_name";
            lengthBringtingMeasureNameColumn.Visible = false;

            deleteParameterTypeButtonColumn = new DataGridViewButtonColumn();
            deleteParameterTypeButtonColumn.Text = "X";
            deleteParameterTypeButtonColumn.Name = "delete_parameter_button_column";
            deleteParameterTypeButtonColumn.UseColumnTextForButtonValue = true;
            deleteParameterTypeButtonColumn.HeaderText = "";
            deleteParameterTypeButtonColumn.Width = 23;
            deleteParameterTypeButtonColumn.FlatStyle = FlatStyle.System;
            deleteParameterTypeButtonColumn.CellTemplate.ToolTipText = "Удалить";

            MeasuredParamsDataGridView.Columns.AddRange(new DataGridViewColumn[] {
                parameterTypeNameColumn,
                minValueColumn,
                maxValueColumn,
                percentColumn,
                measureColumn,
                bringingLengthColumn,
                parameterTypeMeasureColumn,
                minFreqColumn,
                stepFreqColumn,
                maxFreqColumn,
                freqRangeIdColumn,
                lengthBringingTypeIdColumn,
                measuredParameterDataIdColumn,
                cableStructureIdColumn,
                parameterTypeIdColumn,
                parameterTypeDescriptionColumn,
                lengthBringingMeasureTitleColumn,
                lengthBringtingMeasureNameColumn,
                deleteParameterTypeButtonColumn
            });

            
            //
            // MeasuredParamsDataGridView.CellValueChanged += MeasuredParamsDataGridView_CellValueChanged;
            //MeasuredParamsDataGridView.CurrentCellChanged += MeasuredParamsDataGridView_CurrentCellChanged;
            MeasuredParamsDataGridView.CurrentCellDirtyStateChanged += MeasuredParamsDataGridView_CurrentCellDirtyStateChanged;
            // MeasuredParamsDataGridView.ce
            MeasuredParamsDataGridView.RowsAdded += MeasuredParamsDataGridView_RowsAdded;
            MeasuredParamsDataGridView.CellMouseClick += MeasuredParamsDataGridView_CellMouseClick;
            MeasuredParamsDataGridView.RowsRemoved += MeasuredParamsDataGridView_RowsRemoved;

            parameterTypesComboBox = new ComboBox();
            parameterTypesComboBox.Parent = this;
            parameterTypesComboBox.Height = 25;
            parameterTypesComboBox.Width = 135;
            parameterTypesComboBox.DropDownStyle = ComboBoxStyle.DropDownList;
            parameterTypesComboBox.KeyPress += ParameterTypesComboBox_KeyPress;

            addParameterButton = new Button();
            addParameterButton.Parent = this;
            addParameterButton.Text = "Добавить";
            addParameterButton.Width = 80;//100;
            addParameterButton.Height = 23;
            addParameterButton.Click += AddParameterButton_Click;

            addAllAllowedParameterTypesButton = new Button();
            addAllAllowedParameterTypesButton.Parent = this;
            addAllAllowedParameterTypesButton.Text = "Добавить все";
            addAllAllowedParameterTypesButton.Width = 110;//100;
            addAllAllowedParameterTypesButton.Height = 23;
            addAllAllowedParameterTypesButton.Click += AddAllAllowedParameterTypesButton_Click;

            deleteAllMeasuredParametersDataButton = new Button();
            deleteAllMeasuredParametersDataButton.Parent = this;
            deleteAllMeasuredParametersDataButton.Text = "Удалить все";
            deleteAllMeasuredParametersDataButton.Width = 110;//100;
            deleteAllMeasuredParametersDataButton.Height = 23;
            deleteAllMeasuredParametersDataButton.Enabled = false;
            deleteAllMeasuredParametersDataButton.Click += DeleteAllMeasuredParametersDataButton_Click;

        }

        private void DeleteAllMeasuredParametersDataButton_Click(object sender, EventArgs e)
        {
            DialogResult r = MessageBox.Show("Вы уверены, что хотите удалить все измеряемые параметры для данной структуры кабеля?", "Вопрос...", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if (r == DialogResult.Yes)
            {
                while(MeasuredParamsDataGridView.Rows.Count > 0)
                {
                    MeasuredParamsDataGridView.Rows.RemoveAt(0);
                }
                CableStructure.MeasuredParameters.AcceptChanges();
            }
        }

        private void MeasuredParamsDataGridView_RowsRemoved(object sender, DataGridViewRowsRemovedEventArgs e)
        {
            deleteAllMeasuredParametersDataButton.Enabled = MeasuredParamsDataGridView.Rows.Count > 0;
        }

        private void MeasuredParamsDataGridView_CellMouseClick(object sender, DataGridViewCellMouseEventArgs e)
        {
            try
            {
                DataGridViewCell cell = MeasuredParamsDataGridView.Rows[e.RowIndex].Cells[e.ColumnIndex];
                if (e.Button == MouseButtons.Left)
                {
                    if (e.ColumnIndex == MeasuredParamsDataGridView.Columns[deleteParameterTypeButtonColumn.Name].Index)
                    {
                        MeasuredParamsDataGridView.Rows.Remove(MeasuredParamsDataGridView.Rows[e.RowIndex]);
                        CableStructure.MeasuredParameters.AcceptChanges();
                    }
                }
            }
            catch (ArgumentOutOfRangeException) { }
        }


        private void SetMeasuredParameterDataGrid_ColumnsOrder()
        {
            MeasuredParamsDataGridView.Columns[parameterTypeNameColumn.Name].DisplayIndex = 0; //Название параметра
            MeasuredParamsDataGridView.Columns[minValueColumn.Name].DisplayIndex = 1;          //Минимальное значение параметра
            MeasuredParamsDataGridView.Columns[maxValueColumn.Name].DisplayIndex = 2;         
            MeasuredParamsDataGridView.Columns[percentColumn.Name].DisplayIndex = 3;
            MeasuredParamsDataGridView.Columns[measureColumn.Name].DisplayIndex = 4;
            MeasuredParamsDataGridView.Columns[bringingLengthColumn.Name].DisplayIndex = 5;


            MeasuredParamsDataGridView.Columns[parameterTypeMeasureColumn.Name].DisplayIndex = 6;
            MeasuredParamsDataGridView.Columns[minFreqColumn.Name].DisplayIndex = 7;
            MeasuredParamsDataGridView.Columns[stepFreqColumn.Name].DisplayIndex = 8;
            MeasuredParamsDataGridView.Columns[maxFreqColumn.Name].DisplayIndex = 9;
            MeasuredParamsDataGridView.Columns[deleteParameterTypeButtonColumn.Name].DisplayIndex = 10;

            MeasuredParamsDataGridView.Columns[freqRangeIdColumn.Name].DisplayIndex = 11;
            MeasuredParamsDataGridView.Columns[lengthBringingTypeIdColumn.Name].DisplayIndex = 12;
            MeasuredParamsDataGridView.Columns[measuredParameterDataIdColumn.Name].DisplayIndex = 13;
            MeasuredParamsDataGridView.Columns[cableStructureIdColumn.Name].DisplayIndex = 14;
            MeasuredParamsDataGridView.Columns[parameterTypeIdColumn.Name].DisplayIndex = 15;
            MeasuredParamsDataGridView.Columns[parameterTypeDescriptionColumn.Name].DisplayIndex = 16;
            MeasuredParamsDataGridView.Columns[lengthBringingMeasureTitleColumn.Name].DisplayIndex = 17;
            MeasuredParamsDataGridView.Columns[lengthBringtingMeasureNameColumn.Name].DisplayIndex = 18;
            
        }

        private void LengthBringingColumnContextMenu_Opening(object sender, System.ComponentModel.CancelEventArgs e)
        {
            if (MeasuredParamsDataGridView.SelectedCells.Count != 1)
            {
                e.Cancel = true;
            }else
            {
                e.Cancel = !MeasuredParameterType.AllowBringingLength((uint)MeasuredParamsDataGridView.Rows[MeasuredParamsDataGridView.SelectedCells[0].RowIndex].Cells[parameterTypeIdColumn.Name].Value);//MeasuredParamsDataGridView.Rows[MeasuredParamsDataGridView.SelectedCells[0].RowIndex].Cells[bringingLengthColumn.Name].ReadOnly;
            }
            if (!e.Cancel)
            {
                DataGridViewCell c = MeasuredParamsDataGridView.Rows[MeasuredParamsDataGridView.SelectedCells[0].RowIndex].Cells[lengthBringingTypeIdColumn.Name];
                foreach(ParameterTypesToolStripItem item in lengthBringingColumnContextMenu.Items)
                {
                    LengthBringingType t = item.Entity as LengthBringingType;
                    if (t.TypeId.ToString() != c.Value.ToString())
                    {
                        item.Enabled = true;
                        item.BackColor = System.Drawing.Color.Transparent;
                    }
                    else
                    {
                        item.Enabled = false;
                        item.BackColor = EnabledCellStyle.SelectionBackColor;
                    }
                    
                }
            }
        }

        private void LengthBringingColumnContextMenu_ItemClicked(object sender, ToolStripItemClickedEventArgs e)
        {
            MeasuredParamsDataGridView.SelectedCells[0].OwningRow.Cells[lengthBringingTypeIdColumn.Name].Value = ((ParameterTypesToolStripItem)e.ClickedItem).ItemId;
            MeasuredParamsDataGridView.CommitEdit(DataGridViewDataErrorContexts.LeaveControl);
            MeasuredParamsDataGridView.Refresh();
            ReStyleBringingLengthColumnByBringingLengthType(MeasuredParamsDataGridView.SelectedCells[0].OwningRow);
         
        }

        private void ReStyleBringingLengthColumnByBringingLengthType(DataGridViewRow r)
        {
            uint blId = (uint)r.Cells[lengthBringingTypeIdColumn.Name].Value;
            uint pTypeId = (uint)r.Cells[parameterTypeIdColumn.Name].Value;
            DataGridViewCell cell = r.Cells[bringingLengthColumn.Name];
            if (MeasuredParameterType.AllowBringingLength(pTypeId))
            {
                cell.ReadOnly = blId != LengthBringingType.ForAnotherLengthInMeters;
                cell.Style = (blId == LengthBringingType.NoBringing) ? DisabledBringingLengthCellStyle : EnabledBringingLengthCellStyle;
            }else
            {
                cell.ReadOnly = true;
                cell.Style = DisabledCellStyle;
            }

        }

        private void AddAllAllowedParameterTypesButton_Click(object sender, EventArgs e)
        {

            foreach(MeasuredParameterType mpt in CableStructure.StructureType.MeasuredParameterTypes.Rows)
            {
                AddParameterDataToCableStructure(mpt);
            }
            RefreshDataGridView();
        }

        private void ParameterTypesComboBox_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == (char)Keys.Enter) AddParameterTypeToCableStructure();
        }

        private void MeasuredParamsDataGridView_RowsAdded(object sender, DataGridViewRowsAddedEventArgs e)
        {
             for(int i = 0; i< MeasuredParamsDataGridView.Rows.Count; i++)
              {
                    InitRowByParameterType(MeasuredParamsDataGridView.Rows[i]);
              }
            MeasuredParamsDataGridView.Sort(MeasuredParamsDataGridView.Columns[parameterTypeIdColumn.Name], System.ComponentModel.ListSortDirection.Ascending);
            deleteAllMeasuredParametersDataButton.Enabled = MeasuredParamsDataGridView.Rows.Count > 0;
        }

        private void AddParameterButton_Click(object sender, EventArgs e)
        {
            AddParameterTypeToCableStructure();
        }

        /// <summary>
        /// Добавляем тип параметра из чекбокса в DataGridView
        /// </summary>
        private void AddParameterTypeToCableStructure()
        {
            DataRow[] rows = CableStructure.StructureType.MeasuredParameterTypes.Select($"parameter_type_id = {parameterTypesComboBox.SelectedValue}");
            if (rows.Length > 0)
            {
                AddParameterDataToCableStructure((MeasuredParameterType)rows[0]);
                RefreshDataGridView();
            }
            else
            {
                MessageBox.Show("Не найден тип параметра", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        /// <summary>
        /// Обновляем данные DataGridView после изменения списка MeasuredParameters
        /// </summary>
        private void RefreshDataGridView()
        {
            DataGridViewBindingSource.ResetBindings(false);
        }

        /// <summary>
        /// Добавляет тип параметра в коллекцию CableStructure.MeasuredParameters
        /// </summary>
        /// <param name="parameter_type"></param>
        private void AddParameterDataToCableStructure(MeasuredParameterType parameter_type)
        {
            CableStructureMeasuredParameterData newPData = (CableStructureMeasuredParameterData)CableStructure.MeasuredParameters.NewRow();
            newPData.ParameterType = parameter_type;
            newPData.AssignedStructure = CableStructure;
            newPData.MeasuredParameterDataId = 0;
            newPData.LengthBringingTypeId = LengthBringingType.NoBringing;
            CableStructure.MeasuredParameters.Rows.Add(newPData);
        } 

        private void MeasuredParamsDataGridView_CurrentCellChanged(object sender, EventArgs e)
        {
            DataGridViewRow cngRow = MeasuredParamsDataGridView.Rows[MeasuredParamsDataGridView.CurrentCell.RowIndex];
            string cngdColName = cngRow.Cells[MeasuredParamsDataGridView.CurrentCell.ColumnIndex].OwningColumn.Name;
            if (cngdColName == parameterTypeNameColumn.Name) InitRowByParameterType(cngRow);
        }

        private void MeasuredParamsDataGridView_CurrentCellDirtyStateChanged(object sender, EventArgs e)
        {
            if (MeasuredParamsDataGridView.IsCurrentCellDirty)
            {
                MeasuredParamsDataGridView.CommitEdit(DataGridViewDataErrorContexts.CurrentCellChange);
            }
        }

        private void MeasuredParamsDataGridView_CellValueChanged(object sender, DataGridViewCellEventArgs e)
        {
            DataGridViewRow cngRow = MeasuredParamsDataGridView.Rows[e.RowIndex];
            string cngdColName = MeasuredParamsDataGridView.Rows[e.RowIndex].Cells[e.ColumnIndex].OwningColumn.Name;
            
            if (cngdColName == parameterTypeNameColumn.Name) InitRowByParameterType(cngRow);
        }

        private void InitRowByParameterType(DataGridViewRow r)
        {
            try
            {
                DataGridViewTextBoxCell cell = r.Cells[parameterTypeIdColumn.Name] as DataGridViewTextBoxCell;

                uint val = 0;
                uint.TryParse($"{cell.Value}", out val);

                bool isFreqParams = MeasuredParameterType.IsItFreqParameter(val);
                bool hasMaxValue = MeasuredParameterType.IsHasMaxLimit(val);
                bool hasMinValue = MeasuredParameterType.IsHasMinLimit(val);
                bool allowBringingLength = MeasuredParameterType.AllowBringingLength(val) && ((uint)r.Cells[lengthBringingTypeIdColumn.Name].Value == LengthBringingType.ForAnotherLengthInMeters);

                r.Cells[parameterTypeNameColumn.Name].ToolTipText = r.Cells[parameterTypeDescriptionColumn.Name].Value.ToString();

                r.Cells[maxFreqColumn.Name].ReadOnly = r.Cells[stepFreqColumn.Name].ReadOnly = r.Cells[minFreqColumn.Name].ReadOnly = !isFreqParams;
                r.Cells[minValueColumn.Name].ReadOnly = !hasMinValue;
                r.Cells[maxValueColumn.Name].ReadOnly = !hasMaxValue;
                r.Cells[percentColumn.Name].ReadOnly = false;
                r.Cells[lengthBringingTypeIdColumn.Name].ReadOnly = false;
                r.Cells[bringingLengthColumn.Name].ReadOnly = !allowBringingLength;
                refreshReadOnlyCellColor(r);

            }
            catch (NullReferenceException) { }

        }




        private void refreshReadOnlyCellColor(DataGridViewRow row)
        {
            string[] ExceptedColumns = new string[] {
                parameterTypeNameColumn.Name,
                measureColumn.Name
            };
            foreach (DataGridViewCell c in row.Cells)
            {
                if (!c.Visible || Array.IndexOf(ExceptedColumns, c.OwningColumn.Name) > -1) continue;
                else if (bringingLengthColumn.Name == c.OwningColumn.Name)
                {
                    ReStyleBringingLengthColumnByBringingLengthType(c.OwningRow);
                }else
                {
                    c.Style = c.ReadOnly ? DisabledCellStyle : EnabledCellStyle;
                }


            }
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
            DRFormulsGroupBox.Enabled = CableStructure.IsAllowParameterType(MeasuredParameterType.dR);


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
            parameterTypesComboBox.Location = new System.Drawing.Point(240, y+5);
            addParameterButton.Location = new System.Drawing.Point(parameterTypesComboBox.Location.X + parameterTypesComboBox.Width + 10, y+4);
            addAllAllowedParameterTypesButton.Location = new System.Drawing.Point(addParameterButton.Width + addParameterButton.Location.X + 5, addParameterButton.Location.Y);
            deleteAllMeasuredParametersDataButton.Location = new System.Drawing.Point(addAllAllowedParameterTypesButton.Width + addAllAllowedParameterTypesButton.Location.X + 5, addAllAllowedParameterTypesButton.Location.Y);

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
            StructureTypeLabel = new Label();
            StructureTypeLabel.Parent = this;
            StructureTypeLabel.Text = "Тип: ";
            StructureTypeLabel.Width = 210;
        }

        private GroupBox elementsAmountGroupBox;
        private NumericUpDown shownElementsAmount;
        private NumericUpDown realElementsAmount;


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
        private DataGridViewTextBoxColumn parameterTypeNameColumn;
        private DataGridViewTextBoxColumn minValueColumn;
        private DataGridViewTextBoxColumn maxValueColumn;
        private DataGridViewTextBoxColumn minFreqColumn;
        private DataGridViewTextBoxColumn maxFreqColumn;
        private DataGridViewTextBoxColumn stepFreqColumn;
        private DataGridViewTextBoxColumn percentColumn;
        private DataGridViewTextBoxColumn measureColumn;
        private DataGridViewTextBoxColumn bringingLengthColumn;
        private DataGridViewTextBoxColumn freqRangeIdColumn;
        private DataGridViewTextBoxColumn cableStructureIdColumn;
        private DataGridViewTextBoxColumn lengthBringingTypeIdColumn;
        private DataGridViewTextBoxColumn measuredParameterDataIdColumn;
        private DataGridViewTextBoxColumn parameterTypeIdColumn;
        private DataGridViewTextBoxColumn parameterTypeDescriptionColumn;
        private DataGridViewTextBoxColumn parameterTypeMeasureColumn;
        private DataGridViewTextBoxColumn lengthBringingMeasureTitleColumn;
        private DataGridViewTextBoxColumn lengthBringtingMeasureNameColumn;
        private DataGridViewButtonColumn deleteParameterTypeButtonColumn;

        private DataGridViewCellStyle DisabledCellStyle;
        private DataGridViewCellStyle EnabledCellStyle;

        private DataGridViewCellStyle EnabledBringingLengthCellStyle;
        private DataGridViewCellStyle DisabledBringingLengthCellStyle;

        private DataGridViewCellStyle ParameterTypeNameCellStyle;

        private ContextMenuStrip lengthBringingColumnContextMenu;


        private ComboBox parameterTypesComboBox;
        private Button addParameterButton;
        private Button addAllAllowedParameterTypesButton;
        private Button deleteAllMeasuredParametersDataButton;

        public Button DeleteStructureButton;

        public BindingSource DataGridViewBindingSource;
        #endregion
    }

    class ParameterTypesToolStripItem : ToolStripMenuItem
    {
        public ParameterTypesToolStripItem() : base() 
        {

        }

        public ParameterTypesToolStripItem(uint item_id, string item_text, BaseEntity base_entity) : this()
        {
            itemId = item_id;
            Text = item_text;
            entity = base_entity;
        }

        public uint ItemId => itemId;
        public BaseEntity Entity => entity;
        private uint itemId;
        private BaseEntity entity;
    }
}
