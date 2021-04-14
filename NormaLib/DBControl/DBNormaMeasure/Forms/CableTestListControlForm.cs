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
using NormaLib.ProtocolBuilders;


namespace NormaLib.DBControl.DBNormaMeasure.Forms
{
    public partial class CableTestListControlForm : DBTableContolForm
    {
        private ToolStripMenuItem msWordImportButton;
        private ToolStripMenuItem msWordOpenProtocolButton;

        protected DBEntityTable CableTestsTable;
        public CableTestListControlForm() : base()
        {

        }

        protected override void InitDesign()
        {
            base.InitDesign();
            CreateAdditionaButtonsForContextMenu();
            dgEntities.DoubleClick += (o, s)=> OpenSelectedOnMSWord(false);
        }

        protected override void ApplyUserRights()
        {
            AllowAddEntity = false;
            AllowEditEntity = false;
            AllowRemoveEntity = SessionControl.SessionControl.AllowRemove_CableTest;
            base.ApplyUserRights();
        }

        private void CreateAdditionaButtonsForContextMenu()
        {
            List<ToolStripMenuItem> items = new List<ToolStripMenuItem>();
            msWordImportButton = new ToolStripMenuItem();
            msWordImportButton.Name = "msWordImportButton";
            msWordImportButton.Text = "Перезаписать MS Word";
            msWordImportButton.Click += (o, s) => { OpenSelectedOnMSWord(true);};

            msWordOpenProtocolButton = new ToolStripMenuItem();
            msWordOpenProtocolButton.Name = "msWordImportButton";
            msWordOpenProtocolButton.Text = "Открыть в MS Word";


            msWordOpenProtocolButton.Click += (o, s) => { OpenSelectedOnMSWord(false); };

            removeEntityToolStripItem.Text = "Удалить из Базы Данных";

            items.Add(msWordOpenProtocolButton);
            items.Add(msWordImportButton);
            
            foreach (ToolStripMenuItem item in contextTableMenu.Items) items.Add(item);

            contextTableMenu.Items.Clear();
            contextTableMenu.Items.AddRange(items.ToArray());
        }



        private void OpenSelectedOnMSWord(bool force_import)
        {
            CableTest test = GetSelectedTest();
            if (test == null) return;
            ProtocolViewer viewer = new ProtocolViewer(new ProtocolPathBuilder(test, NormaExportType.MSWORD), force_import);
        }

        protected override bool CheckToolStripAllow()
        {
            if (!HasSelectedRows) return false;
            CableTest selectedTest = GetSelectedTest();
            ProtocolPathBuilder wordProtocolBuilder = new ProtocolPathBuilder(selectedTest, NormaExportType.MSWORD);
            msWordImportButton.Visible = wordProtocolBuilder.ProtocolExists();
            msWordImportButton.Enabled = msWordOpenProtocolButton.Enabled = dgEntities.SelectedRows.Count == 1;
            return true;
        }

        private CableTest GetSelectedTest()
        {
            if (!HasSelectedRows) return null;
            return GetCableTestByDataGridRow(dgEntities.SelectedRows[0]);

        }

        private CableTest GetCableTestByDataGridRow(DataGridViewRow r)
        {
            CableTest c = null;
            uint c_id = 0;
            if (UInt32.TryParse(r.Cells[$"{CableTest.CableTestId_ColumnName}_column"].Value.ToString(), out c_id))
            {
                c = SelectCableTestInListById(c_id);
            }
            return c;
        }


        private CableTest SelectCableTestInListById(uint id)
        {
            DataRow[] r = CableTestsTable.Select($"{CableTest.CableTestId_ColumnName} = {id}");
            if (r.Length > 0)
            {
                return (CableTest)r[0];
            }
            else
            {
                return null;
            }
        }

        protected override DataTable FillDataSetAndGetDataGridTable()
        {
            CableTestsTable = CableTest.find_finished();
            AddOrMergeTableToFormDataSet(CableTestsTable);
            return CableTestsTable;
        }

        protected override List<DataGridViewColumn> BuildColumnsForDataGrid()
        {
            List<DataGridViewColumn> cols = base.BuildColumnsForDataGrid();
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

            cols.Add(BuildDataGridTextColumn(CableTest.CableTestId_ColumnName, "ID", true));
            cols.Add(BuildDataGridTextColumn(TestedCable.FullCableName_ColumnName, "Марка"));
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

            cols.Add(BuildDataGridTextColumn(CableTest.BruttoWeight_ColumnName, "Вес кабеля"));
            cols.Add(BuildDataGridTextColumn(CableTest.CableLength_ColumnName, "Длина кабеля, м", true));
            cols.Add(BuildDataGridTextColumn(ReleasedBaraban.BarabanId_ColumnName, "ID барабана"));
            cols.Add(BuildDataGridTextColumn(CableTest.OperatorId_ColumnName, "ID оператора"));
            cols.Add(BuildDataGridTextColumn(CableTest.VSVILeadLead_ColumnName, "Жила-Жила"));
            cols.Add(BuildDataGridTextColumn(CableTest.VSVILeadShield_ColumnName, "Жила-Экран"));
            cols.Add(BuildDataGridTextColumn(CableTest.TestStartedAt_ColumnName, "Время начала"));
            cols.Add(BuildDataGridTextColumn(CableTest.TestFinishedAt_ColumnName, "Дата испытания", true));
            cols.Add(BuildDataGridTextColumn(CableTestStatus.StatusId_ColumnName, "ID статуса"));
            cols.Add(BuildDataGridTextColumn(CableTest.SourceCableId_ColumnName, "ID исходного кабеля"));
            cols.Add(BuildDataGridTextColumn(CableTest.Temperature_ColumnName, "Температура"));
            cols.Add(BuildDataGridTextColumn(Cable.CableId_ColumnName + "1", "ID кабеля*"));
            cols.Add(BuildDataGridTextColumn(Cable.CableId_ColumnName, "ID кабеля"));
            cols.Add(BuildDataGridTextColumn(Cable.CableName_ColumnName, "Название кабеля"));
            cols.Add(BuildDataGridTextColumn(CableTest.NettoWeight_ColumnName, "Вес НЕТТО, кг"));
            cols.Add(BuildDataGridTextColumn(CableTest.TestLineNumber_ColumnName, "Номер линии"));
            cols.Add(BuildDataGridTextColumn(CableTest.TestLineTitle_ColumnName, "Линия", true));
            return cols;
        }

        protected override void RemoveEntityHandler(DataGridViewSelectedRowCollection selectedRows)
        {
            if (!HasSelectedRows) return;
            DialogResult dr = MessageBox.Show("Выбранное испытание будет удалено из Базы Данных безвозвратно. Продолжить?", "Вопрос...", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if (dr == DialogResult.Yes)
            {
                foreach(DataGridViewRow sr in dgEntities.SelectedRows)
                {
                    CableTest test = GetCableTestByDataGridRow(sr);
                    if (GetSelectedTest().Destroy())
                    {
                        CableTestsTable.Rows.Remove(test);
                    }
                }

                
            }

        }
    }
}
