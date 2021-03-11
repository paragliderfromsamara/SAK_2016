using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using NormaLib.DBControl.Tables;

namespace NormaLib.DBControl.DBNormaMeasure.Forms
{
    public partial class CableFormTeraMicro : CableForm
    {
        public CableFormTeraMicro() : base()
        {
            
        }


        public CableFormTeraMicro(uint cable_id) : base(cable_id)
        {
        }

        public CableFormTeraMicro(Cable copiedCable) : base(copiedCable)
        {

        }

        protected override void InitDesign()
        {
            base.InitDesign();
            InitializeComponent();
        }
    }
}
