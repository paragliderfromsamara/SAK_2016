using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;

namespace NormaMeasure.DBControl.Tables
{
    
    public abstract class BaseEntity : DataRow
    {

        public BaseEntity(DataRowBuilder builder) : base(builder)
        {

        }

        /// <summary>
        /// Добавляет новый объект в БД
        /// </summary>
        /// <returns></returns>
        public bool Create()
        {
            string insertQuery = makeInsertQuery();
            DBEntityTable t = (DBEntityTable)this.Table;
            MySQLDBControl mySql = new MySQLDBControl(t.DBName);
            bool wasLoaded = false;
            mySql.MyConn.Open();
            wasLoaded = mySql.RunNoQuery(insertQuery) == 0;
            mySql.MyConn.Close();
            return wasLoaded;
        }


        /// <summary>
        /// Обновляет объект в БД 
        /// </summary>
        /// <returns></returns>
        public bool Update()
        {
            return true;
        }

        /// <summary>
        /// Удаляет объект из БД
        /// </summary>
        /// <returns></returns>
        public bool Destroy()
        {
            return true;
        }


        public string makeInsertQuery()
        {
            string q = ((DBEntityTable)this.Table).InsertQuery;
            string vals = String.Empty;
            string keys = String.Empty;
            int wasAdded = 0;
            foreach (DataColumn col in this.Table.Columns)
            {
                if (col.Table.PrimaryKey.Contains(col)) continue;
                if (wasAdded > 0)
                {
                    keys += ", ";
                    vals += ", ";
                }
                keys += col.ColumnName;
                if (col.DataType == typeof(string))
                {
                    vals += $"'{this[col.ColumnName].ToString()}'";
                }else vals += $"{this[col.ColumnName].ToString()}";
                wasAdded++;
            }

            return String.Format(q, keys, vals); // $"INSERT INTO {this.Table} ({keys}) VALUES ({vals})";
        }


        protected string makeSelectQueryWhere(string where)
        {
            return $"{makeSelectQuery()} WHERE {where}";
        }

        protected string makeSelectQuery()
        {
            return $"SELECT * FROM {Table.TableName}";
        }


        


        protected uint tryParseUInt(string column_name)
        {
            uint v = 0;
            uint.TryParse(this[column_name].ToString(), out v);
            return v;
        }

        protected int tryParseInt(string column_name)
        {
            int v = 0;
            int.TryParse(this[column_name].ToString(), out v);
            return v;
        }

        protected bool tryParseBoolean(string column_name, bool default_val)
        {
            bool v = default_val;
            bool.TryParse(this[column_name].ToString(), out v);
            return v;
        }

        protected float tryParseFloat(string column_name)
        {
            float v = 0;
            float.TryParse(this[column_name].ToString(), out v);
            return v;
        }
    }
}
