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
using NormaMeasure.DBControl.Tables;

namespace NormaMeasure.MeasureControl.SACMeasureForms
{
    public partial class SACHandMeasureForm : Form
    {
        private SAC_Device sac_device;
        public SACHandMeasureForm(SAC_Device sac)
        {
            InitializeComponent();
            sac_device = sac;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            MeasuredParameterType rLeads = MeasuredParameterType.find_by_id(MeasuredParameterType.Rleads);
            sac_device.MeasureParameter(rLeads);
        }
    }
}
