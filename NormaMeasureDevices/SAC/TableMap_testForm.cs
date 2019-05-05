using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Diagnostics;

namespace NormaMeasure.Devices.SAC
{
    public partial class TableMap_testForm : Form
    {

        public TableMap_testForm()
        {
            InitializeComponent();
            TableMap map = new TableMap(this);
            map.Location = new Point(25, 25);
            this.Size = new Size(map.Width+50, map.Height+70);
            this.BackColor = map.BackColor;
            map.Parent = this;
            map.OnPair_Click += Map_OnPair_Click;
            // DrawCommUnits();
            //Paint += TableMap_Paint;
            
        }

        private void Map_OnPair_Click(ModulePair pair)
        {
            //MessageBox.Show($"{pair.PairOnTableNumber}");
        }
    }
}
