using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using NormaMeasure.Devices.SAC;

namespace NormaMeasure.MeasureControl.SACMeasureForms
{
    public partial class SACMeasureForm : Form
    {
        protected SAC_Device sac_device;
        public SACMeasureForm(SAC_Device device)
        {
            InitializeComponent();
            sac_device = device;
        }
    }
}
