using System;
using System.Windows.Forms;
using NormaMeasure.DBControl.SAC.DBEntities;



namespace NormaMeasure.DBControl.SAC.Forms
{
    public partial class CableForm : Form
    {
        private CableOld cable;
        public CableOld Cable => cable;
        public CableForm()
        {
            InitializeComponent();
            cable = CableOld.GetDraft();
            if (cable == null) return;
            fillDBData();
            fillFormByCable();
        }

        private void fillDBData()
        {
            fillCableMarks();
            fillDocuments();
        }

        private void fillDocuments()
        {
            QADocument doc = new QADocument();
            cableFormDataSet.Tables.Add(doc.GetAllFromDB());
            DocumentNumber_input.ValueMember = $"{doc.TableName}.{doc.PrimaryKey}";
            DocumentNumber_input.DisplayMember = $"{doc.TableName}.short_name";
            DocumentNumber_input.Refresh();
            //DocumentNumber_input.SelectedIndexChanged += DocumentNumberInput_Changed;
            DocumentNumber_input.TextChanged += DocumentNumberInput_Changed;
        }

        private void fillCableMarks()
        {
            cableFormDataSet.Tables.Add(CableOld.GetCableMarks());
            CableMark_input.DisplayMember = CableMark_input.ValueMember = $"{cable.TableName}.name";
            CableMark_input.Refresh();
        }

        public CableForm(uint cable_id)
        {
            InitializeComponent();
            cable = new CableOld(cable_id);
        }

        private void saveCableButton_Click(object sender, System.EventArgs e)
        {
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

        private void fillFormByCable()
        {
            this.Text = (cable.IsDraft) ? "Новый кабель" : cable.FullName;
            CableMark_input.Text = cable.Name;
            CableStructures_input.Text = cable.StructName;
            DocumentName_input.Text = cable.DocumentName;
            DocumentNumber_input.Text = cable.DocumentNumber;
            BuildLength_input.Value = cable.BuildLength;
            linearMass_input.Value = cable.LinearMass;
            Ucover_input.Value = cable.UCover;
            Pmin_input.Value = cable.PMin;
            Pmax_input.Value = cable.PMax;
            CodeOKP_input.Text = cable.CodeOKP;
            CodeKCH_input.Text = cable.CodeKCH;
            Notes_input.Text = cable.Notes;

            //////this.Text = $"{cable.id}";
        }

        private void BuildLength_input_ValueChanged(object sender, System.EventArgs e)
        {
            cable.BuildLength = (sender as NumericUpDown).Value;
        }

        private void linearMass_input_ValueChanged(object sender, System.EventArgs e)
        {
            cable.LinearMass = (sender as NumericUpDown).Value;
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



        private void CodeOKP_input_MaskInputRejected(object sender, MaskInputRejectedEventArgs e)
        {
            cable.CodeOKP = (sender as MaskedTextBox).Text;
        }

        private void CodeKCH_input_MaskInputRejected(object sender, MaskInputRejectedEventArgs e)
        {
            cable.CodeKCH = (sender as MaskedTextBox).Text;
        }

        private void DocumentNumberInput_Changed(object sender, EventArgs e)
        {
            QADocument doc = new QADocument();
            ComboBox cp = sender as ComboBox;
            string txt = cp.Text;
            MessageBox.Show(txt);
            foreach (System.Data.DataRow r in cableFormDataSet.Tables[doc.TableName].Rows)
            {
                QADocument _d = new QADocument(r);
                if (_d.ShortName == txt)
                {
                    doc = _d;
                    break;
                }
            }
            DocumentName_input.Text = doc.FullName;
            Cable.DocumentId = doc.id;
        }

    }
}
