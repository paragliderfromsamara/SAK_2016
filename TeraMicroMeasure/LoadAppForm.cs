using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace TeraMicroMeasure
{
    public partial class LoadAppForm : Form
    {
        public LoadAppForm()
        {
            InitializeComponent();
        }



    
        public void SetTaskLabelsValue(string primary, string secondary = null)
        {
            primaryTaskLabel.Text = primary;
            subTaskLabel.Text = string.IsNullOrWhiteSpace(secondary) ? "" : secondary;
            this.Update();
           // Thread.Sleep(1000);
        }
    }
}
