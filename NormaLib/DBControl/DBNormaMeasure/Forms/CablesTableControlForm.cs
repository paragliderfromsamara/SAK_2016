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
    public partial class CablesTableControlForm : DBTableContolForm
    {
        protected DBEntityTable CablesTable;
        public CablesTableControlForm() : base()
        {
            
        }

        protected override void InitNewEntityForm()
        {
            base.InitNewEntityForm();
            Cable cable = Cable.GetDraft();
            CableForm form = new CableForm(cable.CableId);
            if (form.ShowDialog() == DialogResult.OK)
            {
                FillDataGridAsync();
            }
        }

        protected override void EditEntityHandler(DataGridViewRow selectedRow)
        {
            Cable editCable = GetCableByDataGridRow(selectedRow);
            if (editCable != null)
            {
                CableForm f = new CableForm(editCable.CableId);
                if (f.ShowDialog() == DialogResult.OK)
                {
                    FillDataGridAsync();
                }
            }
            
        }

        private Cable GetCableByDataGridRow(DataGridViewRow r)
        {
            Cable c = null;
            uint c_id = 0;
            if (UInt32.TryParse(r.Cells[$"{Cable.CableId_ColumnName}_column"].Value.ToString(), out c_id))
            {
                c = SelectCableInListById(c_id);
            }
            return c;
        }


        private Cable SelectCableInListById(uint id)
        {
            DataRow[] r = CablesTable.Select($"{Cable.CableId_ColumnName} = {id}");
            if (r.Length > 0)
            {
                return (Cable)r[0];
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
            AllowAddEntity = true;
            AllowEditEntity = true;
            AllowRemoveEntity = true;
            base.ApplyUserRights();
        }

        protected override DataTable FillDataSetAndGetDataGridTable()
        {
            CablesTable = Cable.get_all_including_docs();
            AddOrMergeTableToFormDataSet(CablesTable);
            return CablesTable;
        }

        protected override List<DataGridViewColumn> BuildColumnsForDataGrid()
        {
            List < DataGridViewColumn > cols = base.BuildColumnsForDataGrid();
            cols.Add(BuildDataGridTextColumn("cable_id", "ID", true));
            cols.Add(BuildDataGridTextColumn("name", "Марка"));
            cols.Add(BuildDataGridTextColumn("struct_name", "Структура"));
            cols.Add(BuildDataGridTextColumn("full_cable_name", "Марка", true));
            cols.Add(BuildDataGridTextColumn("notes", "Примечание", true));
            cols.Add(BuildDataGridTextColumn("short_name", "Норматив", true));
            cols.Add(BuildDataGridTextColumn("full_name", "Норматив"));
            cols.Add(BuildDataGridTextColumn("code_okp", "ОКП", true));
            cols.Add(BuildDataGridTextColumn("code_kch", "КЧ", true));
            cols.Add(BuildDataGridTextColumn("linear_mass", "Погонная масса, кг"));
            cols.Add(BuildDataGridTextColumn("build_length", "Строительная длина, км"));
            cols.Add(BuildDataGridTextColumn("document_id", "ID документа"));
            cols.Add(BuildDataGridTextColumn("u_cover", "Испытательное напряжение оболочки, В"));
            cols.Add(BuildDataGridTextColumn("p_min", "Минимальное испытательное давление"));
            cols.Add(BuildDataGridTextColumn("p_max", "Максимальное испытательное давление"));
            cols.Add(BuildDataGridTextColumn("is_draft", "Черновик"));
            return cols;
        }
    }
}
