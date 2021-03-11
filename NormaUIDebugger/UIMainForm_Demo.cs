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
namespace NormaUIDebugger
{
    public partial class UIMainForm_Demo : UIMainForm
    {
        public UIMainForm_Demo() : base()
        {
           
        }
        protected override void InitializeDesign()
        {
            base.InitializeDesign();
            InitializeComponent();
        }
    }
}
