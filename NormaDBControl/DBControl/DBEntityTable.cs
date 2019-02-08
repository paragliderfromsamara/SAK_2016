using System;
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
        public DBEntityTable(Type entityType) : base()
        {
            entity_type = entityType;
            SetTableName();
            ConstructColumns();
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
                this.InsertQuery = $"INSERT INTO {a.TableName} " + "({0}) VALUES ({1})";
            }
        }

        internal bool SaveRowsToDB()
        {
            if (this.Rows.Count > 0)
            {
                foreach(DataRow r in this.Rows)
                {
                    
                }
            }
            return true;
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
        public string DBName;
        public string SelectQuery;
        public string InsertQuery;
    }
}
