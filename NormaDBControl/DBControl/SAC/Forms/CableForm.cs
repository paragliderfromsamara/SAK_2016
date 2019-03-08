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

        private void InitStructuresTabControl()
        {
            AddStructureDraftTab();
        }

        private void AddStructureDraftTab()
        {
            CableStructure draft = CableStructure.build_for_cable(cable.CableId);
            CableStructureTabPage tp = new CableStructureTabPage(draft);
            CableStructureTabs.TabPages.Add(tp);
            //CableStructureTabs.Refresh();
        }

        private void fillDBData()
        {
            fillCableMarks();
            fillDocuments();
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

        private void fillCableMarks()
        {
            cableFormDataSet.Tables.Add(Cable.get_cable_marks());
            CableMark_input.DisplayMember = CableMark_input.ValueMember = "cable_marks.cable_mark";
            CableMark_input.Refresh();
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
            fillFormByCable();
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
    }

    class CableStructureTabPage : TabPage
    {
        private CableStructure CableStructure;
        public CableStructureTabPage(CableStructure structure)
        {
            this.CableStructure = structure;
            DrawElements();
            InitTabPage();
        }

        private void InitTabPage()
        {
            this.Text = "Новая структура";
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
        }

        private void DrawDRFormulsElements()
        {
            DRFormulsGroupBox = new GroupBox();
            DRFormulsGroupBox.Parent = this;
            DRFormulsGroupBox.Text = "Омическая ассиметрия";
            DRFormulsGroupBox.Width = 210;
            DRFormulsGroupBox.Height = 70;

            DRFormulaComboBox = new ComboBox();
            DRFormulaComboBox.Parent = DRFormulsGroupBox;
            DRFormulaComboBox.Width = 90;
            DRFormulaComboBox.DropDownStyle = ComboBoxStyle.DropDownList;

            DRBringingFormulaComboBox = new ComboBox();
            DRBringingFormulaComboBox.Parent = DRFormulsGroupBox;
            DRBringingFormulaComboBox.Width = 90;
            DRBringingFormulaComboBox.DropDownStyle = ComboBoxStyle.DropDownList;

            DRFormulaLabel = new Label();
            DRFormulaLabel.Parent = DRFormulsGroupBox;
            DRFormulaLabel.Text = "вычисление";

            DRBringingFormulaLabel = new Label();
            DRBringingFormulaLabel.Parent = DRFormulsGroupBox;
            DRBringingFormulaLabel.Text = "приведение";

            DRFormulaLabel.Location = new System.Drawing.Point(7, 20);
            DRBringingFormulaLabel.Location = new System.Drawing.Point(107, 20);
            DRFormulaComboBox.Location = new System.Drawing.Point(10, 35);
            DRBringingFormulaComboBox.Location = new System.Drawing.Point(110, 35);

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
            ElementsInGroupNumericUpDown.Minimum = 1;
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
            LeadMaterialComboBox.Width = 100;
            LeadMaterialComboBox.DropDownStyle = ComboBoxStyle.DropDownList;

            LeadDiametersTextBox = new TextBox();
            LeadDiametersTextBox.Parent = this;
            LeadDiametersTextBox.Width = 100;

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
            structureTypeCB.Location = new System.Drawing.Point(x, y += 20);

            elementsAmountGroupBox.Location = new System.Drawing.Point(x, y+= 30);
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

            y += DRFormulsGroupBox.Height;

            GroupCapacityCheckBox.Location = new System.Drawing.Point(x, y+=10);
        }

        private void DrawStructureTypeCB()
        {
            structureTypeCB = new ComboBox();
            structureTypeCB.Parent = this;
            structureTypeCB.Width = 210;
            structureTypeCB.DropDownStyle = ComboBoxStyle.DropDownList;

            StructureTypeLabel = new Label();
            StructureTypeLabel.Parent = this;
            StructureTypeLabel.Text = "Тип структуры";
        }

        private GroupBox elementsAmountGroupBox;
        private NumericUpDown shownElementsAmount;
        private NumericUpDown realElementsAmount;

        private ComboBox structureTypeCB;

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
