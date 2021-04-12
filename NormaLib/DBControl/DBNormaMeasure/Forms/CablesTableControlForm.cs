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
    public partial class CablesTableControlForm : DBTableContolForm
    {
        protected DBEntityTable CablesTable;
        protected ToolStripMenuItem createFromCableToolStripItem;
        protected ToolStripMenuItem showCableToolStripItem;
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

        protected virtual void InitNewEntityForm(Cable template_cable)
        {
            base.InitNewEntityForm();
            CableForm form = new CableForm(template_cable);
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
            dgEntities.DoubleClick += (o, s) => { ShowSelectedCable(); };
            CreateAdditionaButtonsForContextMenu();
        }

        private void CreateAdditionaButtonsForContextMenu()
        {
            List<ToolStripMenuItem> items = new List<ToolStripMenuItem>();
            showCableToolStripItem = new ToolStripMenuItem();
            showCableToolStripItem.Name = "showCableToolStripMenuItem";
            showCableToolStripItem.Text = "Просмотр";
            createFromCableToolStripItem = new ToolStripMenuItem();
            createFromCableToolStripItem.Name = "createCableFromExist";
            createFromCableToolStripItem.Text = "Создать из...";
            createFromCableToolStripItem.Click += createCableFromExistItem_Click;
            items.Add(showCableToolStripItem);
            items.Add(createFromCableToolStripItem);
            showCableToolStripItem.Click += (o, s) => { ShowSelectedCable(); };
            foreach (ToolStripMenuItem item in contextTableMenu.Items) items.Add(item);

            contextTableMenu.Items.Clear();
            contextTableMenu.Items.AddRange(items.ToArray());



           // contextTableMenu.Items.Add(createFromCableToolStripItem);
        }

        private void ShowSelectedCable()
        {
            if (HasSelectedOneRow)
            {
                Cable cable = GetCableByDataGridRow(dgEntities.SelectedRows[0]);
                CableForm form = new CableForm(cable.CableId, false);
                form.ShowDialog();
            }
        }

        private void createCableFromExistItem_Click(object sender, EventArgs e)
        {
            if (HasSelectedOneRow)
            {
                InitNewEntityForm(GetCableByDataGridRow(dgEntities.SelectedRows[0]));
            }
        }

        protected override void RemoveEntityHandler(DataGridViewSelectedRowCollection selectedRows)
        {
            if (selectedRows.Count > 0)
            {
                string msg = selectedRows.Count == 1 ? "Выбранный кабель будет удалён безвозвратно.\n\nВы уверены?" : "Выбранные кабели будут удалены безвозвратно.\n\nВы уверены?";
                int i = 0;
                if (MessageBox.Show(msg, "Вопрос", MessageBoxButtons.YesNo) == DialogResult.Yes)
                {
                    foreach(DataGridViewRow r in selectedRows)
                    {
                        Cable c = GetCableByDataGridRow(r);
                        if (c != null)
                        { if (c.Destroy()) i++; }
                    }
                    base.RemoveEntityHandler(selectedRows);
                    if (i >= 1)
                        MessageBox.Show(i == 1 ? "Кабель успешно удалён" : "Кабели успешно удалены", "Успешно", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
                    else
                        MessageBox.Show(selectedRows.Count == 1 ? "Не удалось удалить кабель" : "Не удалось удалить выбранные кабели", "Ошибка", MessageBoxButtons.OK);
                    FillDataGridAsync();
                }
            }
        }

        protected override void ApplyUserRights()
        {
            AllowAddEntity = SessionControl.SessionControl.AllowAdd_Cable;
            AllowEditEntity = SessionControl.SessionControl.AllowEdit_Cable;
            AllowRemoveEntity = SessionControl.SessionControl.AllowRemove_Cable;
            createFromCableToolStripItem.Enabled = AllowAddEntity;
            base.ApplyUserRights();
        }

        protected override DataTable FillDataSetAndGetDataGridTable()
        {
            CablesTable = Cable.get_all_including_docs();
            AddOrMergeTableToFormDataSet(CablesTable);
            return CablesTable;
        }

        protected override bool CheckToolStripAllow()
        {
            createFromCableToolStripItem.Enabled = AllowEditEntity & HasSelectedOneRow;
            return createFromCableToolStripItem.Enabled | base.CheckToolStripAllow();
        }

        protected override List<DataGridViewColumn> BuildColumnsForDataGrid()
        {
            List < DataGridViewColumn > cols = base.BuildColumnsForDataGrid();
            DataGridViewColumn full_name_column = BuildDataGridTextColumn("full_cable_name", "Марка", true);
            DataGridViewColumn okp = BuildDataGridTextColumn("code_okp", "ОКП", true);
            DataGridViewColumn kch = BuildDataGridTextColumn("code_kch", "КЧ", true);
            DataGridViewColumn standart = BuildDataGridTextColumn("short_name", "Норматив", true);

            full_name_column.AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
            okp.AutoSizeMode = DataGridViewAutoSizeColumnMode.DisplayedCells;
            okp.MinimumWidth = 180;
            kch.AutoSizeMode = DataGridViewAutoSizeColumnMode.DisplayedCells;
            standart.AutoSizeMode = DataGridViewAutoSizeColumnMode.DisplayedCells;
            standart.MinimumWidth = 150;

            cols.Add(BuildDataGridTextColumn("cable_id", "ID", true));
            cols.Add(BuildDataGridTextColumn("name", "Марка"));
            cols.Add(BuildDataGridTextColumn("struct_name", "Структура"));
            cols.Add(full_name_column);
            cols.Add(BuildDataGridTextColumn("notes", "Примечание", true));
            cols.Add(standart);
            cols.Add(BuildDataGridTextColumn("full_name", "Норматив"));
            cols.Add(okp);
            cols.Add(kch);
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
