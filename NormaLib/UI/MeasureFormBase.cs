using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace NormaLib.UI
{
    public partial class MeasureFormBase : Form
    {
        public MeasureFormBase()
        {
            InitializeDesign();
        }

        protected virtual void InitializeDesign()
        {
            InitializeComponent();
        }

        protected virtual void startMeasureButton_Click(object sender, EventArgs e)
        {
        }

        protected virtual void buttonNextPoint_Click(object sender, EventArgs e)
        {

        }

        protected virtual void buttonNextElement_Click(object sender, EventArgs e)
        {

        }

        protected virtual void buttonPrevElement_Click(object sender, EventArgs e)
        {

        }

        protected virtual void buttonPrevPoint_Click(object sender, EventArgs e)
        {

        }
    }
}
