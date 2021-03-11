using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using System.Reflection;
using MySql.Data.MySqlClient;

namespace NormaLib.DBControl
{
    public class DBEntityTable : DataTable
    {


        public DBEntityTable(Type entityType) : base()
        {
            entity_type = entityType;
            SetTableName();
            ConstructColumns(DBEntityTableMode.OwnAndVirtualColumns);
            InitDBControl();
        }

        public DBEntityTable(Type entityType, DBEntityTableMode initMode) : base()
        {
            entity_type = entityType;
            SetTableName();
            ConstructColumns(initMode);
            InitDBControl();
        }

        public long GetScalarValueFromDB(string query)
        {
            return MySqlControl.RunNoQuery(query);
        }
        private MySqlConnection DBConnection
        {
            get
            {
                return MySqlControl.MyConn;
            }
        }
        private void InitDBControl()
        {
            MySqlControl = new MySQLDBControl(this.DBName);
        }

        public void OpenDB()
        {
            MySqlControl.MyConn.Open();
        }
        public void CloseDB()
        {
            MySqlControl.MyConn.Close();
        }

        private void SetTableName()
        {
            object[] tableAttrs = entity_type.GetCustomAttributes(typeof(DBTableAttribute), true);
            if (tableAttrs.Length == 1)
            {
                DBTableAttribute a = (DBTableAttribute)tableAttrs[0];
                this.TableName = a.TableName;
                this.DBName = a.DBName;
                this.SelectQuery = $"SELECT * FROM {a.TableName}";
                this.InsertQuery = $"INSERT INTO {a.TableName} " + "({0}) VALUES {1}";
                this.UpdateQuery = $"UPDATE {a.TableName}" + " SET {0} WHERE {1}";
                this.DeleteQuery = $"DELETE FROM {a.TableName}";

            }
        }
        
        public DataRow[] RowsAsArray()
        {
            List<DataRow> rowsList = new List<DataRow>();
            foreach (DataRow row in Rows) rowsList.Add(row);
            return rowsList.ToArray();
        }

        /// <summary>
        /// Созда
        /// </summary>
        /// <param name="ignorePrimaryKeys"></param>
        /// <returns></returns>
        internal bool CreateRowsToDB(bool ignorePrimaryKeys = false)
        {
            //string query = fillInsertQueryForAllRows(ignorePrimaryKeys);
            //return WriteSingleQuery(query);
            //DataRow[] rowsArray = RowsAsArray();
            bool flag = true;
            int butchSize = 100;
            List<DataRow> rowsTMPList = new List<DataRow>();
            int notSavedAmount = Rows.Count;
            for(int idx = 0, butchTmp = butchSize; idx < Rows.Count; idx++, butchSize--)
            {
                rowsTMPList.Add(Rows[idx]);
                if (butchTmp == 0 || idx == Rows.Count-1)
                {
                    butchTmp = butchSize;
                    flag &= CreateRowsToDB(rowsTMPList.ToArray(), ignorePrimaryKeys);
                    rowsTMPList.Clear();
                }
            }
            return flag;
        }


        internal bool CreateRowsToDB(DataRow[] rows, bool ignorePrimaryKeys)
        {
            string query = fillInsertQueryForAllRows(ignorePrimaryKeys, rows);
            return WriteSingleQuery(query);
        } 
        

        private string fillInsertQueryForAllRows(bool ignorePrimaryKeys, DataRow[] rowsList)
        {
            string vals = String.Empty;
            string keys = String.Empty;
            string[] columns = getNotVirtualColumnNames();
            foreach (string colName in columns)
            {
                DataColumn col = this.Columns[colName];
                if (ignorePrimaryKeys && this.PrimaryKey.Contains(col)) continue;
                if (!String.IsNullOrEmpty(keys))
                {
                    keys += ", ";
                }
                keys += col.ColumnName;
            }
            foreach(DataRow row in this.Rows)
            {
                string rowVals = "";
                foreach (string colName in columns)
                {
                    DataColumn col = this.Columns[colName];
                    if (ignorePrimaryKeys && this.PrimaryKey.Contains(col)) continue;
                    if (!String.IsNullOrEmpty(rowVals)) rowVals += ", ";
                    rowVals += DBColumnValue(col, row);
                }
                if (!String.IsNullOrEmpty(vals)) vals += ", ";
                vals += $"({rowVals})";
            }
            return String.Format(InsertQuery, keys, vals); // $"INSERT INTO {this.Table} ({keys}) VALUES ({vals})";
        }

        public static string DBColumnValue(DataColumn col, DataRow row)
        {
            if (col.DataType == typeof(string))
            {
                return $"'{row[col.ColumnName].ToString()}'";
            }
            else
            {
                if (String.IsNullOrWhiteSpace(row[col.ColumnName].ToString())) return "NULL";
                else return row[col.ColumnName].ToString();
            }
        }

        public void FillByQuery(string select_query)
        {
            MySqlDataAdapter a = new MySqlDataAdapter(select_query, DBConnection);
            OpenDB();
            try
            {
                a.Fill(this);
            }
            catch(System.Data.ConstraintException ex)
            {
                System.Windows.Forms.MessageBox.Show(ex.Message + "\n" + select_query, $"Ошибка заполнения таблицы {this.TableName}", System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Error);
            }
            catch(MySql.Data.MySqlClient.MySqlException ex)
            {
                System.Windows.Forms.MessageBox.Show(ex.Message + "\n" + select_query, $"Ошибка заполнения таблицы {this.TableName}", System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Error);

            }
            CloseDB();
        }

        public bool WriteSingleQuery(string query)
        {
            int c = writeQueries(new string[] { query });
            return c == 1;
        }

        private int writeQueries(string[] queries)
        {
            int counter = 0;
            OpenDB();

            foreach(string q in queries)
            {
                if (MySqlControl.RunNoQuery(q) == 0) counter++;
            }
            CloseDB();
            return counter;
        }

        /// <summary>
        /// Получает список колонок таблицы с атрибутом IsVirtual = false
        /// </summary>
        /// <returns></returns>
        public string[] getNotVirtualColumnNames()
        {
            List<string> cols = new List<string>();
            foreach (PropertyInfo prop in entity_type.GetProperties())
            {
                object[] columnAttributes = prop.GetCustomAttributes(typeof(DBColumnAttribute), true);
                if (columnAttributes.Length == 1)
                {
                    DBColumnAttribute dca = columnAttributes[0] as DBColumnAttribute;
                    if (!dca.IsVirtual) cols.Add(dca.ColumnName);
                }
            }
            return cols.ToArray();
        }

        private void ConstructColumns(DBEntityTableMode init_mode)
        {
            if (init_mode == DBEntityTableMode.NoColumns) return; //Выскакиваем если режим без колонок

            SortedList<int, DataColumn> columns = new SortedList<int, DataColumn>();
            List<DataColumn> primaryKeys = new List<DataColumn>();

            foreach (PropertyInfo prop in entity_type.GetProperties())
            {
                object[] columnAttributes = prop.GetCustomAttributes(typeof(DBColumnAttribute), true);
                if (columnAttributes.Length == 1)
                {
                    DBColumnAttribute dca = columnAttributes[0] as DBColumnAttribute;
                    if (init_mode == DBEntityTableMode.OwnColumns && dca.IsVirtual) continue;
                    else if (init_mode == DBEntityTableMode.VirtualColumns && !dca.IsVirtual) continue;
                    DataColumn dc = new DataColumn(dca.ColumnName, prop.PropertyType);

                    dc.AllowDBNull = dca.Nullable;
                    dc.DefaultValue = dca.DefaultValue;
                    columns.Add(dca.Order, dc);
                    if (dca.IsPrimaryKey) primaryKeys.Add(dc);
                }
            }

            foreach(DataColumn c in columns.Values)
            {
                this.Columns.Add(c);
            }
            if (primaryKeys.Count > 0) this.PrimaryKey = primaryKeys.ToArray();
        }



        protected override Type GetRowType()
        {
            return entity_type;
        }


        protected override DataRow NewRowFromBuilder(DataRowBuilder builder)
        {
            return (DataRow)Activator.CreateInstance(GetRowType(), new object[1] { builder });
        }

        public override DataTable Clone()
        {
            DBEntityTable tb = new DBEntityTable(EntityType);
            tb.Merge(this);
            return tb;
        }

        Type EntityType => entity_type;

        private Type entity_type;
        private MySQLDBControl MySqlControl;
        public string DBName;
        public string SelectQuery;
        public string InsertQuery;
        public string UpdateQuery;
        public string DeleteQuery;
    }

    public enum DBEntityTableMode
    {
        NoColumns,
        VirtualColumns,
        OwnColumns,
        OwnAndVirtualColumns
    }
}
