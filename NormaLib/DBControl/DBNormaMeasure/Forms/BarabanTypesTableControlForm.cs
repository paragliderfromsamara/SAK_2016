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
using NormaLib.SessionControl;

namespace NormaLib.DBControl.DBNormaMeasure.Forms
{
    public partial class BarabanTypesTableControlForm : DBTableContolForm
    {
        protected DBEntityTable BarabanTypes;
        public BarabanTypesTableControlForm() : base()
        {
            
        }

        protected override void InitNewEntityForm()
        {
            BarabanType t = BarabanType.build();
            BarabanTypeForm f = new BarabanTypeForm(t, BarabanTypes);
            if (f.ShowDialog() == DialogResult.OK)
            {
                FillDataGridAsync();
            }
        }

        protected override void EditEntityHandler(DataGridViewRow selectedRow)
        {
            base.EditEntityHandler(selectedRow);
            BarabanType t = GetBarabanTypeByDataGridRow(selectedRow);
            BarabanTypeForm f = new BarabanTypeForm(t);
            if (f.ShowDialog() == DialogResult.OK)
            {
                FillDataGridAsync();
            }
        }

        protected override void RemoveEntityHandler(DataGridViewSelectedRowCollection selectedRows)
        {
            if (!HasSelectedRows) return;
            string msg = selectedRows.Count > 1 ? "Типы барабанов будут удалены безвозвратно. \n\nПродолжить?" : "Тип барабана будет удалён безвозвратно. \n\nПродолжить?";
            DialogResult dr = MessageBox.Show(msg, "Вопрос", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if (dr != DialogResult.Yes) return;
            base.RemoveEntityHandler(selectedRows);
            foreach(DataGridViewRow row in selectedRows)
            {
                BarabanType t = GetBarabanTypeByDataGridRow(row);
                if (t.Destroy()) dgEntities.Rows.Remove(row);
            }
            dgEntities.ClearSelection();
           
        }

        private BarabanType GetBarabanTypeByDataGridRow(DataGridViewRow r)
        {
            BarabanType u = null;
            uint bt_id = 0;
            if (UInt32.TryParse(r.Cells["baraban_type_id_column"].Value.ToString(), out bt_id))
            {
                u = SelectBarabanTypeInListById(bt_id);
            }
            return u;
        }


        private BarabanType SelectBarabanTypeInListById(uint id)
        {
            DataRow[] r = BarabanTypes.Select($"baraban_type_id = {id}");
            if (r.Length > 0)
            {
                return (BarabanType)r[0];
            }
            else
            {
                return null;
            }
        }

        protected override void InitDesign()
        {
            base.InitDesign();
            InitializeComponent();
        }

        protected override void ApplyUserRights()
        {
            AllowAddEntity = SessionControl.SessionControl.AllowAdd_BarabanType;
            AllowEditEntity = SessionControl.SessionControl.AllowEdit_BarabanType;
            AllowRemoveEntity = SessionControl.SessionControl.AllowRemove_BarabanType;
            base.ApplyUserRights();
        }

        protected override List<DataGridViewColumn> BuildColumnsForDataGrid()
        {
            List < DataGridViewColumn > list = base.BuildColumnsForDataGrid();
            list.Add(BuildDataGridTextColumn("baraban_type_id", "ID", true));
            list.Add(BuildDataGridTextColumn("baraban_type_name", "Наименование типа барабана", true));
            list.Add(BuildDataGridTextColumn("baraban_weight", "Вес, кг", true));
            return list;
        }

        protected override DataTable FillDataSetAndGetDataGridTable()
        {
            BarabanTypes = BarabanType.get_all_active_as_table();
            AddOrMergeTableToFormDataSet(BarabanTypes);
            return BarabanTypes;
        }
    }
}
