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
    public partial class CableTestListControlForm : DBTableContolForm
    {
        protected DBEntityTable CableTestsTable;
        public CableTestListControlForm() : base()
        {

        }


        protected override void ApplyUserRights()
        {
            AllowAddEntity = false;
            AllowEditEntity = true;
            AllowRemoveEntity = true;
            base.ApplyUserRights();
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
    }
}
