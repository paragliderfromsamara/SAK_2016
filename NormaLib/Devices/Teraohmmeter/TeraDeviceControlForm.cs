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
using NormaLib.Devices.Teraohmmeter;


namespace NormaLib.Devices.Teraohmmeter
{
    
    public partial class TeraDeviceControlForm : DeviceControlFormBase
    {

        TeraohmmeterTOmM_01 teraDevice;

        public TeraDeviceControlForm(DeviceBase _device) : base(_device)
        {
           
        }

        protected override void InitializeDesign()
        {
            base.InitializeDesign();
            InitializeComponent();
            RLModeButton.Click += SetRLMode;
            RModeButton.Click += SetRMode;
            pVModeButton.Click += Set_pVMode;
            pSButton.Click += Set_pSMode;
            Shown += (o, e) => { RModeButton.PerformClick(); };
        }



        private void Set_pSMode(object sender, EventArgs e)
        {
            SetActiveForm(new MeasureFormBase(), sender);
            //throw new NotImplementedException();
        }

        private void Set_pVMode(object sender, EventArgs e)
        {
            SetActiveForm(new pVTeraMeasureForm(teraDevice), sender);
            //throw new NotImplementedException();
        }

        private void SetRMode(object sender, EventArgs e)
        {
            SetActiveForm(new RTeraMeasureForm(teraDevice), sender);
            //throw new NotImplementedException();
        }

        private void SetRLMode(object sender, EventArgs e)
        {
            SetActiveForm(new RLTeraMeasureForm(teraDevice), sender);
            // throw new NotImplementedException();
        }

        protected override void SetDevice(DeviceBase _device)
        {
            base.SetDevice(_device);
            teraDevice = _device as TeraohmmeterTOmM_01;
            teraDevice.AssignToClient(0);
            this.FormClosing += TeraDeviceControlForm_FormClosing;
        }

        private void TeraDeviceControlForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            teraDevice.ReleaseDeviceFromClient();
        }

        

    }
}
