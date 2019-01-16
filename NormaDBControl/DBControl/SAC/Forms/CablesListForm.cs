using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace NormaMeasure.DBControl.SAC.Forms
{
    public partial class CablesListForm : Form
    {
        private CableForm cableForm;
        public CablesListForm()
        {
            InitializeComponent();
        }

        private void newCableFormButton_Click(object sender, EventArgs e)
        {
            cableForm = new CableForm();
            if (cableForm.Cable == null)
            {
                cableForm.Close();
            }else
            {
                cableForm.MdiParent = this.MdiParent;
                cableForm.Show();
                this.Enabled = false;
                initCableFormEvents();
            }

        }

        private void initCableFormEvents()
        {
            cableForm.FormClosed += new FormClosedEventHandler(cableForm_Closed);
        }

        private void cableForm_Closed(object sender, EventArgs e)
        {
            this.Enabled = true;
            cableForm = null;
        }
    }
}
