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
    public partial class ProcessBox : Form
    {
        int counter = 3;
        public ProcessBox(string process_name)
        {
            InitializeComponent();
            processNameLbl.Text = process_name;
            processTimer.Interval = 500;
            processTimer.Tick += (s, a) => {
                string str = string.Empty;
                for(int i = 0; i<counter%3; i++)
                {
                    str += "• ";
                }
                
                processIndicator.Text = str.Trim();
            };
            Load += (s, a) => { processTimer.Start(); };
            Closing += (s, a) => { processTimer.Stop(); };

        }


    }
}
