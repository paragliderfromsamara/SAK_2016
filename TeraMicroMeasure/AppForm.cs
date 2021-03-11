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
using TeraMicroMeasure.Forms;

namespace TeraMicroMeasure
{
    public partial class AppForm : UIMainForm
    {
        public AppForm() : base()
        {
            
        }

        protected override void InitializeDesign()
        {
            base.InitializeDesign();
            InitializeComponent();
        }

        protected override Form GetSettingsForm()
        {
            return new SettingsForm();
        }

        protected override Form GetDataBaseForm()
        {
            return new DataBaseTablesControlForm();
        }
    }
}
