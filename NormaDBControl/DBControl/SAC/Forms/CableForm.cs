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
        public CableStructureTabPage()
        {
            
        }
    }

}
