using System.Windows.Forms;
using NormaMeasure.DBControl.SAC.DBEntities;



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
            initForm();
            fillCableInputs();

        }

        public CableForm(uint cable_id)
        {
            InitializeComponent();
            cable = new Cable(cable_id);
        }

        private void saveCableButton_Click(object sender, System.EventArgs e)
        {
            cable.Save();

        }

        private void cableNameTextField_TextChanged(object sender, System.EventArgs e)
        {
            TextBox txt = sender as TextBox;
            cable.Name = txt.Text;
        }

        private void fillCableInputs()
        {
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

        private void CodeOKP_input_TextChanged(object sender, System.EventArgs e)
        {
            cable.CodeOKP = (sender as TextBox).Text;
        }

        private void CodeKCH_input_TextChanged(object sender, System.EventArgs e)
        {
            cable.CodeKCH = (sender as TextBox).Text;
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

        private void initForm()
        {
            this.Text = (cable.IsDraft) ? "Новый кабель" : cable.FullName;
        }
    }
}
