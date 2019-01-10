using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace NormaMeasure.SAC_APP
{
    public partial class autoTestForm : Form
    {
        public mainForm mainForm = null;
        public autoTestForm(mainForm f)
        {
            this.mainForm = f;
            InitializeComponent();
            
        }

        private void closeButton_Click(object sender, EventArgs e)
        {
            this.mainForm.switchMenuStripItems(true);
            this.mainForm.autoTestForm = null;
            this.Close();
            this.Dispose();
        }

    }
}
