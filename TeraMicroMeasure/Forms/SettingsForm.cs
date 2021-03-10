using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using NormaMeasure.SocketControl.TCPControlLib;
using NormaMeasure.UI;
using System.Diagnostics;

namespace TeraMicroMeasure.Forms
{
    public partial class SettingsForm : ChildFormTabControlled
    {
        

        public SettingsForm() : base()
        {
        }

        protected override void InitDesign()
        {
            base.InitDesign();
            InitializeComponent();
        }

        protected override Form GetCurrentTabForm(int idx)
        {
            switch (idx)
            {
                case 0:
                    return new TCPSettingsForm(new TCPSettingsController(Properties.Settings.Default.IsServerApp));
                case 1:
                    return new DataBaseSettingsForm();
                default:
                    return new NormaMeasure.UI.ChildForms.BlankForm();
            }
        }
    }
}
