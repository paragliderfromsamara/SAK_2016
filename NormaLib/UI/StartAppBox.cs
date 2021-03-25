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
    public partial class StartAppBox : Form
    {
        int counter = 4;
        public StartAppBox(string process_name)
        {
            InitializeComponent();
            processNameLbl.Text = process_name;
            processTimer.Interval = 900;
            processTimer.Tick += (s, a) => {
                string str = string.Empty;
                for(int i = 0; i<counter % 4; i++)
                {
                    str += "•";
                }
                counter++;
                statusText.Text = str.Trim();
            };
            Load += (s, a) => { processTimer.Start(); };
            Closing += (s, a) => { processTimer.Stop(); processTimer.Dispose(); };

        }


    }
}
