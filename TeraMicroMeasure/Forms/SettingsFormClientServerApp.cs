using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using NormaLib.SocketControl.TCPControlLib;
using NormaLib.UI;
using System.Diagnostics;
using NormaLib.ProtocolBuilders;

namespace TeraMicroMeasure.Forms
{
    public partial class SettingsFormClientServerApp : ChildFormTabControlled
    {
        

        public SettingsFormClientServerApp() : base()
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
                    return new TCPSettingsForm(new TCPSettingsController(SettingsControl.IsServerApp));
                case 1:
                    return new DataBaseSettingsForm();
                case 2:
                    return new ProtocolSettingsForm();
                default:
                    return new NormaLib.UI.ChildForms.BlankForm();
            }
        }
    }
}
