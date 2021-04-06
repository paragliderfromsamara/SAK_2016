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
using NormaLib.DBControl.DBNormaMeasure.Forms;

namespace TeraMicroMeasure.Forms
{
    public partial class DataBaseTablesControlForm : ChildFormTabControlled
    {
        public DataBaseTablesControlForm() : base()
        {
           
        }

        protected override void InitDesign()
        {
            base.InitDesign();
            InitializeComponent();
        }

        protected override Form GetCurrentTabForm(int idx)
        {
            switch(idx)
            {
                case 0:
                    return new UsersTableControlForm();//UsersList();
                case 1:
                    return new BarabanTypesTableControlForm();//BarabanTypesControlForm();
                case 2:
                    return new CablesTableControlForm();//new CablesListForm(CABLE_FORM_TYPE.TERA_MICRO);
                case 3:
                    return new CableTestListControlForm();
                default:
                    return new NormaLib.UI.ChildForms.BlankForm();
            }
        }
    }
}
