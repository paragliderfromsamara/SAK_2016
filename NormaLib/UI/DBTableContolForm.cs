using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace NormaLib.UI
{
    public partial class DBTableContolForm : Form
    {
        protected string EmptyListText = "Список пуст";
        protected bool AllowAddEntity;
        protected bool AllowEditEntity;
        protected bool AllowRemoveEntity;

        protected bool WillShowDBException = true;
        protected bool HasSelectedRows => dgEntities.SelectedRows.Count > 0;
        protected bool HasSelectedOneRow => dgEntities.SelectedRows.Count == 1;

        private bool VisibleToolStrip => CheckToolStripAllow();



        public DBTableContolForm()
        {
            InitDesign();
            InitDataGridView();
        }

        /// <summary>
        /// Применение прав пользователя к текущей форме
        /// При переопределении в дочерних классах вызывать метод родительского класса в конце
        /// </summary>
        protected virtual void ApplyUserRights()
        {
            //для контекстного меню флаги применяются при 
            btnNewRecordFormInit.Visible = AllowAddEntity;
            removeEntityToolStripItem.Visible = AllowRemoveEntity;
            editEntityTooStripButton.Visible = AllowEditEntity;

            headerPanel.Visible = AllowAddEntity || AllowEditEntity || AllowRemoveEntity;
        }

        protected virtual void InitDesign()
        {
            InitializeComponent();
            emptyEntitiesList.Text = EmptyListText;
            dgEntities.RowsRemoved += (o, s) => { CheckListIsEmpty(); };

        }

        private void InitDataGridView()
        {
            List<DataGridViewColumn> columns = BuildColumnsForDataGrid();
            foreach(var c in columns) dgEntities.Columns.Add(c);
        }

        protected virtual DataTable FillDataSetAndGetDataGridTable()
        {
            return new DataTable();
            //throw new NotImplementedException();
        }

        protected void AddOrMergeTableToFormDataSet(DataTable t, bool force_replace = false)
        {
            if (entitiesDataSet.Tables.Contains(t.TableName))
            {
                if (force_replace) entitiesDataSet.Tables[t.TableName].Clear();
                entitiesDataSet.Tables[t.TableName].Merge(t);
            }
            else
            {
                entitiesDataSet.Tables.Add(t);
            }
        }



        protected virtual List<DataGridViewColumn> BuildColumnsForDataGrid()
        {
            return new List<DataGridViewColumn>();
            //throw new NotImplementedException();
        }

        private void btnNewRecordFormInit_Click(object sender, EventArgs e)
        {
            InitNewEntityForm();
        }

        protected virtual void InitNewEntityForm()
        {

        }

        protected virtual void EditEntityHandler(DataGridViewRow selectedRow)
        {

        }

        private void DBTableContolForm_Load(object sender, EventArgs e)
        {
            //MessageBox.Show(btnNewRecordFormInit.Visible.ToString());
            ApplyUserRights();
            FillDataGridAsync();

        }

        protected virtual void HideOnBeforeLoadModeEntities()
        {
            dgEntities.Visible = false;
            btnNewRecordFormInit.Visible = false;
        }

        protected virtual void ShowAfterLoadModeEntities()
        {
            dgEntities.Visible = true;
            btnNewRecordFormInit.Visible = AllowAddEntity;
        }

        protected virtual void BeforeDataLoad()
        {
            InitLoadStatusLabelAnimation();
        }

        protected virtual void AfterDataLoad()
        {
            DeinitLoadStatusLabelAnimation();
            ShowAfterLoadModeEntities();
            dgEntities.ClearSelection();// foreach (DataGridViewRow r in dgEntities.SelectedRows) r.Selected = false;
            CheckListIsEmpty();
        }

        protected void CheckListIsEmpty()
        {
            emptyEntitiesList.Visible = dgEntities.Rows.Count == 0;
            dgEntities.Visible = dgEntities.Rows.Count > 0;
        }

        async protected void FillDataGridAsync()
        {
            DataTable dt = new DataTable();
            bool flag = false;
            BeforeDataLoad();
            await Task.Run((Action)(() => {
                try
                {
                    dt = this.FillDataSetAndGetDataGridTable();
                    flag = true;
                }catch(Exception ex)
                {
                    ShowDBExceptionMessage(ex, "Не удалось загрузить данные из базы данных.");
                }
            }));
            if (flag)
            {
                this.Enabled = true;
                dgEntities.DataSource = dt;
                dgEntities.Refresh();
            }else
            {
                this.Enabled = false;
            }

            AfterDataLoad();
        }

        protected void ShowDBExceptionMessage(Exception ex, string start_text)
        {
            if (WillShowDBException) MessageBox.Show($"{start_text}\n\n{ex.Message}", "Ошибка связи с базой данных", MessageBoxButtons.OK, MessageBoxIcon.Error);
        }

        #region load_status_label_animation
        int loadStatusLabelPointsCounter = 3;
        string loadStatusText = "Идёт загрузка"; 
        protected void DeinitLoadStatusLabelAnimation()
        {
            loadStatusLabelTimer.Enabled = false;
            loadStatusLabel.Visible = false;
        }

        protected void InitLoadStatusLabelAnimation()
        {
            loadStatusLabelPointsCounter = 2;
            loadStatusLabel.Text = loadStatusText;
            loadStatusLabelTimer.Enabled = true;
        }

        private void loadStatusLabelTimer_Tick(object sender, EventArgs e)
        {
            string points = string.Empty;
            loadStatusLabelPointsCounter++;
            if (loadStatusLabelPointsCounter == 4)
            {
                HideOnBeforeLoadModeEntities();
                loadStatusLabel.Visible = true;

            }
            if (loadStatusLabelPointsCounter >= 4)
            {
                for (int i = 0; i < loadStatusLabelPointsCounter % 4; i++)
                {
                    points += ".";
                }
                loadStatusLabel.Text = $"{loadStatusText}{points}";
            }


        }
        #endregion

        #region ColumnBuilders
        protected DataGridViewColumn BuildDataGridTextColumn(string on_db_column_name, string column_header, bool visible = false, bool read_only = true)
        {
            DataGridViewTextBoxColumn tbc = new DataGridViewTextBoxColumn();
            tbc.DataPropertyName = on_db_column_name;
            tbc.Name = $"{on_db_column_name}_column";
            tbc.Visible = visible;
            tbc.ReadOnly = read_only;
            tbc.HeaderText = column_header;
            if (on_db_column_name.Contains("_id"))
            {
                tbc.AutoSizeMode = DataGridViewAutoSizeColumnMode.None;
                tbc.Width = 40;
            }
            return tbc;
        }
        #endregion

        #region ContextMenuControl
        private void contextTableMenu_Opening(object sender, CancelEventArgs e)
        {
            e.Cancel = !CheckToolStripAllow();
        }

        private void editEntityTooStripButton_Click(object sender, EventArgs e)
        {
            EditEntityHandler(dgEntities.SelectedRows[0]);
        }

        private void removeEntityToolStripItem_Click(object sender, EventArgs e)
        {
            RemoveEntityHandler(dgEntities.SelectedRows);
        }

        protected virtual void RemoveEntityHandler(DataGridViewSelectedRowCollection selectedRows)
        {
        }

        /// <summary>
        /// Проверка целесообразности показа всплывающего контекстного меню для dgEntities
        /// При переопределении в дочерних классах вызывать метод базового класса в начале
        /// </summary>
        /// <returns></returns>
        protected virtual bool CheckToolStripAllow()
        {
            editEntityTooStripButton.Enabled = AllowEditEntity & HasSelectedOneRow;
            removeEntityToolStripItem.Enabled = AllowRemoveEntity & HasSelectedRows;
            return editEntityTooStripButton.Enabled | removeEntityToolStripItem.Enabled;
        }
        #endregion
    }

}
