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
            if (cable!= null) fillCableInputs();
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
            cableNameTextField.Text = cable.Name;
            this.Text = $"{cable.id}";
        }
    }
}
