using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace NormaLib.Devices.Teraohmmeter
{
    public partial class RTeraMeasureForm : TeraMeasureForm
    {
        public RTeraMeasureForm(TeraohmmeterTOmM_01 device) : base(device)
        {

        }

        protected override void InitializeDesign()
        {
            base.InitializeDesign();
            InitializeComponent();
        }
    }
}
