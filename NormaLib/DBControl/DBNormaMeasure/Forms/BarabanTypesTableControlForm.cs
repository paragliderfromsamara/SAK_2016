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
using NormaLib.DBControl.Tables;

namespace NormaLib.DBControl.DBNormaMeasure.Forms
{
    public partial class BarabanTypesTableControlForm : DBTableContolForm
    {
        protected DBEntityTable BarabanTypes;
        public BarabanTypesTableControlForm() : base()
        {
            
        }

        protected override void InitDesign()
        {
            base.InitDesign();
            InitializeComponent();
        }

        protected override void ApplyUserRights()
        {
            AllowAddEntity = true;
            AllowEditEntity = false;
            AllowRemoveEntity = false;
            base.ApplyUserRights();
        }

        protected override List<DataGridViewColumn> BuildColumnsForDataGrid()
        {
            List < DataGridViewColumn > list = base.BuildColumnsForDataGrid();
            list.Add(BuildDataGridTextColumn("baraban_type_id", "ID", true));
            list.Add(BuildDataGridTextColumn("baraban_type_name", "Наименование барабана", true));
            list.Add(BuildDataGridTextColumn("baraban_weight", "Вес, кг", true));
            return list;
        }

        protected override DataTable FillDataSetAndGetDataGridTable()
        {
            BarabanTypes = BarabanType.get_all_as_table();
            AddOrMergeTableToFormDataSet(BarabanTypes);
            return BarabanTypes;
        }
    }
}
