using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using NormaLib.UI;
using NormaLib.Devices;

namespace NormaLib.Devices.Teraohmmeter
{
    public partial class TeraMeasureForm : MeasureFormBase
    {
        protected TeraohmmeterTOmM_01 teraDevice;

        public TeraMeasureForm() : base()
        {

        }


        public TeraMeasureForm(TeraohmmeterTOmM_01 device) : this()
        {
            teraDevice = device;
        }

        protected override void InitializeDesign()
        {
            base.InitializeDesign();
            InitializeComponent();
        }

    }
}
