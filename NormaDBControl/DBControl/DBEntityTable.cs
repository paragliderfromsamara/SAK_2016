﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using System.Reflection;
using MySql.Data.MySqlClient;

namespace NormaMeasure.DBControl
{
    public class DBEntityTable : DataTable
    {

        public DBEntityTable(Type entityType, bool build_columns) : base()
        {
            entity_type = entityType;
            SetTableName();
            if (build_columns) ConstructColumns();
            InitDBControl();
        }

        public DBEntityTable(Type entityType) : base()
        {
            entity_type = entityType;
            SetTableName();
            ConstructColumns();
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
        
        internal bool CreateRowsToDB(bool ignorePrimaryKeys)
        {
            string query = fillInsertQueryForAllRows(ignorePrimaryKeys);
            return WriteSingleQuery(query);
        }

        

        private string fillInsertQueryForAllRows(bool ignorePrimaryKeys)
        {
            string vals = String.Empty;
            string keys = String.Empty;
            foreach (DataColumn col in this.Columns)
            {
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
                foreach (DataColumn col in this.Columns)
                {
                    if (ignorePrimaryKeys && this.PrimaryKey.Contains(col)) continue;
                    if (!String.IsNullOrEmpty(rowVals)) rowVals += ", ";
                    if (col.DataType == typeof(string))
                    {
                        rowVals += $"'{row[col.ColumnName].ToString()}'";
                    }
                    else rowVals += $"{row[col.ColumnName].ToString()}";
                }
                if (!String.IsNullOrEmpty(vals)) vals += ", ";
                vals += $"({rowVals})";
            }
            return String.Format(InsertQuery, keys, vals); // $"INSERT INTO {this.Table} ({keys}) VALUES ({vals})";
        }

        public void FillByQuery(string select_query)
        {
            MySqlDataAdapter a = new MySqlDataAdapter(select_query, DBConnection);
            OpenDB();
            a.Fill(this);
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

        private void ConstructColumns()
        {
            SortedList<int, DataColumn> columns = new SortedList<int, DataColumn>();
            List<DataColumn> primaryKeys = new List<DataColumn>();
            foreach (PropertyInfo prop in entity_type.GetProperties())
            {
                object[] columnAttributes = prop.GetCustomAttributes(typeof(DBColumnAttribute), true);
                if (columnAttributes.Length == 1)
                {
                    DBColumnAttribute dca = columnAttributes[0] as DBColumnAttribute;
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

        Type EntityType => entity_type;

        private Type entity_type;
        private MySQLDBControl MySqlControl;
        public string DBName;
        public string SelectQuery;
        public string InsertQuery;
        public string UpdateQuery;
        public string DeleteQuery;
    }
}
