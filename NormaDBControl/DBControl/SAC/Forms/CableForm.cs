using System;
using System.Windows.Forms;
using NormaMeasure.DBControl.Tables;
using System.Data;



namespace NormaMeasure.DBControl.SAC.Forms
{
    public partial class CableForm : Form
    {
        private Cable cable;

        public Cable Cable => cable;
        public CableForm()
        {
            InitializeComponent();
            cable = Cable.GetDraft();
            if (cable == null) return;
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



        public CableForm(uint cable_id)
        {
            InitializeComponent();
           // cable = new CableOld(cable_id);
        }

        private void saveCableButton_Click(object sender, System.EventArgs e)
        {
            if (!checkSelectedDocument()) return;
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
            uint strTypeId = (uint)structureTypesComboBox.SelectedValue;
            CableStructure draft = (CableStructure)cable.CableStructures.NewRow();
            draft.CableStructureId = (cable.CableStructures.Rows.Count > 0) ? ((CableStructure)cable.CableStructures.Rows[cable.CableStructures.Rows.Count-1]).CableStructureId + 1 : CableStructure.get_last_structure_id() + 1;
            draft.CableId = cable.CableId;
            draft.StructureTypeId = strTypeId;
            draft.LeadMaterialTypeId = 1;
            draft.IsolationMaterialId = 1;
            draft.LeadDiameter = 0.1f;
            draft.WaveResistance = 0;
            draft.LeadToLeadTestVoltage = 0;
            draft.LeadToShieldTestVoltage = 0;
            draft.DRBringingFormulaId = 0;
            draft.DRFormulaId = 0;
            cable.CableStructures.Rows.Add(draft);
            AddCableStructureTabPage(draft);
        }

        private void InitStructuresTabControl()
        {
            foreach(CableStructure s in cable.CableStructures.Rows)
            {
                AddCableStructureTabPage(s);
            }
        }

        private void AddStructureDraftTab()
        {

            //CableStructureTabs.Refresh();
        }

        private void AddCableStructureTabPage(CableStructure structure)
        {
            CableStructureTabPage tp = new CableStructureTabPage(structure, cableFormDataSet);
            CableStructureTabs.TabPages.Add(tp);
            CableStructureTabs.SelectedIndex = CableStructureTabs.TabPages.Count - 1;
            CableStructureTabs.Refresh();
        }
        #endregion
    }

    class CableStructureTabPage : TabPage
    {
        private CableStructure CableStructure;
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
            shownElementsAmount.Value = CableStructure.DisplayedAmount;
            LeadMaterialComboBox.SelectedValue = CableStructure.LeadMaterialTypeId;
            LeadDiametersTextBox.Text = CableStructure.LeadDiameter.ToString();
            IsolationMaterialComboBox.SelectedValue = CableStructure.IsolationMaterialId;
            WaveResistanceComboBox.Text = CableStructure.WaveResistance.ToString();
            ElementsInGroupNumericUpDown.Value = CableStructure.GroupedAmount;
            TestVoltageLeadLeadNumericUpDown.Value = CableStructure.LeadToLeadTestVoltage;
            TestVoltageLeadShieldNumericUpDown.Value = CableStructure.LeadToShieldTestVoltage;
            DRFormulaComboBox.SelectedValue = CableStructure.DRFormulaId;
            DRBringingFormulaComboBox.SelectedValue = CableStructure.DRBringingFormulaId;
            GroupCapacityCheckBox.Checked = CableStructure.WorkCapacityGroup;

            realElementsAmount.ValueChanged += RealElementsAmount_ValueChanged;

        }

        private void RealElementsAmount_ValueChanged(object sender, EventArgs e)
        {
            CableStructure.DisplayedAmount = (uint)(sender as NumericUpDown).Value;
        }

        /// <summary>
        /// Заполняем исходными данными ComboBox-ы 
        /// </summary>
        /// <param name="ds"></param>
        private void FillFromDataSet(DataSet ds)
        {
            DBEntityTable structureTypesTable = new DBEntityTable(typeof(CableStructureType));
            DBEntityTable leadMaterialsTable = new DBEntityTable(typeof(LeadMaterial));
            DBEntityTable isolationMaterialsTable = new DBEntityTable(typeof(IsolationMaterial));
            DBEntityTable drBringingFormulsTable = new DBEntityTable(typeof(dRBringingFormula));
            DBEntityTable drFormulsTable = new DBEntityTable(typeof(dRFormula));

            //if (ds.Tables.Contains(structureTypesTable.TableName)) fillStructureTypeComboBox(ds.Tables[structureTypesTable.TableName]);
            if (ds.Tables.Contains(leadMaterialsTable.TableName)) fillLeadMaterialsComboBox(ds.Tables[leadMaterialsTable.TableName]);
            if (ds.Tables.Contains(isolationMaterialsTable.TableName)) fillIsolationMaterialsComboBox(ds.Tables[isolationMaterialsTable.TableName]);
            if (ds.Tables.Contains(drFormulsTable.TableName)) fill_dRFormulsComboBox(ds.Tables[drFormulsTable.TableName]);
            if (ds.Tables.Contains(drBringingFormulsTable.TableName)) fill_drBringingFormulsComboBox(ds.Tables[drBringingFormulsTable.TableName]);
        }

        private void fill_drBringingFormulsComboBox(DataTable dt)
        {
            DRBringingFormulaComboBox.DataSource = dt;
            DRBringingFormulaComboBox.ValueMember = "dr_bringing_formula_id";
            DRBringingFormulaComboBox.DisplayMember = "dr_bringing_formula_description";
            DRBringingFormulaComboBox.Refresh();
        }

        private void fill_dRFormulsComboBox(DataTable dt)
        {
            DRFormulaComboBox.DataSource = dt;
            DRFormulaComboBox.ValueMember = "dr_formula_id";
            DRFormulaComboBox.DisplayMember = "dr_formula_description";
            DRFormulaComboBox.Refresh();
        }

        private void fillIsolationMaterialsComboBox(DataTable dt)
        {
            IsolationMaterialComboBox.DataSource = dt;
            IsolationMaterialComboBox.ValueMember = "isolation_material_id";
            IsolationMaterialComboBox.DisplayMember = "isolation_material_name";
            IsolationMaterialComboBox.Refresh();
        }

        private void fillLeadMaterialsComboBox(DataTable dt)
        {
            LeadMaterialComboBox.DataSource = dt;
            LeadMaterialComboBox.ValueMember = "lead_material_id";
            LeadMaterialComboBox.DisplayMember = "lead_material_name";
            LeadMaterialComboBox.Refresh();
        }

        private void fillStructureTypeComboBox(DataTable dt)
        {
            structureTypeComboBox.DataSource = dt;
            structureTypeComboBox.ValueMember = "structure_type_id";
            structureTypeComboBox.DisplayMember = "structure_type_name";
            structureTypeComboBox.Refresh();
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

            SetElementsLocations();
        }

        private void DrawGroupCapacityElements()
        {
            GroupCapacityCheckBox = new CheckBox();
            GroupCapacityCheckBox.Parent = this;
            GroupCapacityCheckBox.Text = "Cр группы";
            GroupCapacityCheckBox.Checked = false;
            //GroupCapacityCheckBox.TextAlign = System.Drawing.ContentAlignment.TopLeft;
            GroupCapacityCheckBox.CheckAlign = System.Drawing.ContentAlignment.BottomCenter;
            //GroupCapacityCheckBox.Width = 40;
            //GroupCapacityCheckBox.Height = 50;
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

        #endregion
    }

}
