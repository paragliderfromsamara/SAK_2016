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

        public bool Save()
        {
            bool isCompleted;
            if (IsNewRecord())
            {
                isCompleted = Create();
            }else
            {
                isCompleted = Update();
            }
            if (isCompleted) this.AcceptChanges(); //Меняем обновляем RowState
            return isCompleted;
        }

        /// <summary>
        /// Добавляет новый объект в БД
        /// </summary>
        /// <returns></returns>
        public bool Create()
        {
            string insertQuery = makeInsertQuery();
            return ((DBEntityTable)this.Table).WriteSingleQuery(insertQuery);
        }


        /// <summary>
        /// Обновляет объект в БД 
        /// </summary>
        /// <returns></returns>
        public bool Update()
        {
            string criteria = primaryKeysAsCriteria();
            string query = makeUpdateQuery(criteria);
            return ((DBEntityTable)this.Table).WriteSingleQuery(query);
        }

        /// <summary>
        /// Удаляет объект из БД
        /// </summary>
        /// <returns></returns>
        public bool Destroy()
        {
            return true;
        }

        public bool IsNewRecord()
        {
            return this.RowState == DataRowState.Added;
        }

        /// <summary>
        /// Формирует строку запроса для создания в базе данных текущей строки
        /// </summary>
        /// <returns></returns>
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
                vals += dbColumnValue(col);

                wasAdded++;
            }
            return String.Format(q, keys, $"({vals})"); // $"INSERT INTO {this.Table} ({keys}) VALUES ({vals})";
        }

        /// <summary>
        /// Формирует строку запроса обновления текущей строки по заданным критериям 
        /// </summary>
        /// <param name="upd_criteria"></param>
        /// <returns></returns>
        public string makeUpdateQuery(string upd_criteria)
        {
            string q = ((DBEntityTable)this.Table).UpdateQuery;
            string keyAndVals = String.Empty;
            if (String.IsNullOrWhiteSpace(upd_criteria)) throw new DBEntityException($"Отсутствует критерий для Update() объекта {this.GetType().Name}");
            foreach(DataColumn dc in this.Table.Columns)
            {
                if (!String.IsNullOrEmpty(keyAndVals)) keyAndVals += ", ";
                keyAndVals += $"{dc.ColumnName} = {dbColumnValue(dc)}"; 
            }
            return String.Format(q, keyAndVals, upd_criteria);    
        }

        private string[] primaryKeysColumnsAndValues()
        {
            List<string> keys = new List<string>();
            if (this.Table.PrimaryKey.Length > 0)
            {
                foreach (DataColumn dc in this.Table.PrimaryKey) keys.Add($"{dc.ColumnName} = {dbColumnValue(dc)}");
            }
            return keys.ToArray();
        }

        /// <summary>
        /// Выдаёт первичные ключи ввиде критерия с разделителем AND если первичных ключей несколько
        /// </summary>
        /// <returns></returns>
        protected string primaryKeysAsCriteria()
        {
            string[] primaryKeyNames = primaryKeysColumnsAndValues();
            string v = String.Empty;
            if (primaryKeyNames.Length > 0)
            {
                for(int i =0; i<primaryKeyNames.Length; i++)
                {
                    if (i > 0) v += " AND ";
                    v += primaryKeyNames[i];
                }
            }
            return v;

        }


        /// <summary>
        /// Формирует строку запроса Select по критерию Where
        /// </summary>
        /// <param name="where"></param>
        /// <returns></returns>
        protected string makeSelectQueryWhere(string where)
        {
            return $"{makeSelectQuery()} WHERE {where}";
        }

        protected string makeSelectQuery()
        {
            return $"SELECT * FROM {Table.TableName}";
        }

        protected string dbColumnValue(DataColumn col)
        {
            return col.DataType == typeof(string) ? $"'{this[col.ColumnName].ToString()}'" : this[col.ColumnName].ToString();
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

        protected virtual bool IsValid() { return true; }


    }

    public class DBEntityException : Exception
    {
        int errorcode;
        public int ErrorCode { get { return errorcode; } }
        public DBEntityException(string mess)
            : base(mess)
        {
            errorcode = 0;
        }
        public DBEntityException(int errcode)
        {
            errorcode = errcode;
        }
        public DBEntityException(int errcode, string mess)
            : base(mess)
        {
            errorcode = errcode;
        }
    }
}
