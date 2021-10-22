using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using NormaLib.Devices;
using NormaLib.UI;

namespace TeraMicroMeasure.Forms
{
    public partial class DeviceControlForm : UIMainForm
    {
        private DeviceBase device;
        public DeviceControlForm(DeviceBase _device)
        {
            device = _device;
            deviceNameLabel.Text = device.TypeNameFull;
            serialLabel.Text = $"Зав.№ {device.Serial}";
            InitByDevice();
        }



        private void InitByDevice()
        {
            //throw new NotImplementedException();
        }

        #region Инициализация дизайна
        protected override void InitializeDesign()
        {
            base.InitializeDesign();
            InitializeComponent();

            ReorederMenuItems();

        }



        private void ReorederMenuItems()
        {
            this.panelMenu.Controls.Clear();
            this.panelMenu.Controls.Add(this.btnSettings);
            this.panelMenu.Controls.Add(this.btnMeasure);
            this.panelMenu.Controls.Add(this.panelHeader);
        }

        protected override Form GetSettingsForm()
        {
            return new Form();
        }

        protected override Form GetDataBaseForm()
        {
            return new Form();
        }


        protected override Form GetMeasureForm()
        {
            Form f = new Form();
            // f.OnMeasureStateChanged += RefreshMeasureStateHandler;
            //f.Load += (s, a) => { f.SetXmlDeviceList(xmlDevices); };
            return f;
        }

        /// <summary>
        /// Ищем открытую форму измерений
        /// </summary>
        /// <returns></returns>
        private MeasureForm FindOpened_MeasureForm()
        {
            if (currentForm.GetType() == typeof(MeasureForm))
                return (MeasureForm)currentForm;
            else
                return null;
        }

        private bool MeasureIsActive()
        {
            MeasureForm mf = FindOpened_MeasureForm();
            if (mf == null) return false;
            return mf.MeasureState.MeasureStartFlag;
        }



        //private MeasureForm getMeasureFormByClientId(int client_id)
        //{
        //    MeasureForm f = null;
        //    if (measureFormsList.ContainsKey(client_id)) f = measureFormsList[client_id];
        //    return f;
        //}

        #endregion


    }
}
