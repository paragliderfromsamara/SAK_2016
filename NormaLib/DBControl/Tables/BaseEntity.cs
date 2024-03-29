﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using NormaLib.Utils;
using System.Diagnostics;
using System.Globalization;

namespace NormaLib.DBControl.Tables
{
    
    public abstract class BaseEntity : DataRow
    {

        public BaseEntity(DataRowBuilder builder) : base(builder)
        {

        }

        /// <summary>
        /// Синхронизация с ini файлом 
        /// </summary>
        /// <param name="isExport">Направление синхронизации</param>
        /// <param name="file">Исходный файл</param>
        /// <param name="section">Название секции. Если не указано то берется из названия таблицы</param>
        public void SyncWithIniFile(bool isExport, IniFile file, string section=null)
        {
            if (section == null) section = this.Table.TableName;
            foreach (DataColumn col in this.Table.Columns)
            {
                if (isExport)
                {
                    file.Write(col.ColumnName, this.dbColumnValue(col), this.Table.TableName);
                }
                else
                {
                    if (file.KeyExists(col.ColumnName, this.Table.TableName))
                    {
                        this[col.ColumnName] = file.Read(col.ColumnName, this.Table.TableName);
                    }
                    else
                    {
                        file.Write(col.ColumnName, this.dbColumnValue(col), this.Table.TableName);
                    }
                }
            }
        }

        /// <summary>
        /// Удаление из БД по заданному критерию
        /// </summary>
        /// <param name="criteria">Критерий удаления</param>
        /// <param name="entity_type">Тип сущности</param>
        public static void delete_by_criteria(string criteria, Type entity_type)
        {
            DBEntityTable t = new DBEntityTable(entity_type);
            string query = $"{t.DeleteQuery} WHERE {criteria}";
            t.WriteSingleQuery(query);

        }



        protected static DBEntityTable find_by_primary_key(uint primary_key_value, Type type)
        {
            DBEntityTable t = new DBEntityTable(type);
            string primary_key = t.PrimaryKey[0].ColumnName;
            string query = $"{t.SelectQuery} WHERE {primary_key} = {primary_key_value} LIMIT 1";
            t.FillByQuery(query);
            return t;
        }

        /// <summary>
        /// Поиск по критерию 
        /// </summary>
        /// <param name="criteria">Аргумент должен содержать типичный для SQL синтаксис WHERE. При этом само слово Where можно не указывать</param>
        /// <param name="type"></param>
        /// <returns></returns>
        protected static DBEntityTable find_by_criteria(string criteria, Type type)
        {
            DBEntityTable t = new DBEntityTable(type);
            if (!criteria.Contains("WHERE") && !criteria.Contains("where") && !String.IsNullOrWhiteSpace(criteria)) criteria = "WHERE "+ criteria; //на случай, если ключевого слова WHERE нет
            string query = $"{t.SelectQuery} {criteria}";
            t.FillByQuery(query);
            return t;
        }

        /// <summary>
        /// Поиск по критерию 
        /// </summary>
        /// <param name="criteria">Аргумент должен содержать типичный для SQL синтаксис WHERE. При этом само слово Where можно не указывать</param>
        /// <param name="type"></param>
        /// <returns></returns>
        protected static DBEntityTable find_by_criteria(string criteria, Type type, DBEntityTableMode init_table_mode)
        {
            DBEntityTable t = new DBEntityTable(type, init_table_mode);
            if (!criteria.Contains("WHERE") && !criteria.Contains("where") && !String.IsNullOrWhiteSpace(criteria)) criteria = "WHERE " + criteria; //на случай, если ключевого слова WHERE нет
            string query = $"{t.SelectQuery} {criteria}";
            t.FillByQuery(query);
            return t;
        }

        /// <summary>
        /// Поиск по заданному запросу с ключевым словом SELECT
        /// </summary>
        /// <param name="select_query"></param>
        /// <param name="type"></param>
        /// <returns></returns>
        protected static DBEntityTable find_by_query(string select_query, Type type)
        {
            DBEntityTable t = new DBEntityTable(type);
            t.FillByQuery(select_query);
            return t;
        }


        /// <summary>
        /// Достаёт все записи из таблицы
        /// </summary>
        /// <param name="type"></param>
        /// <returns></returns>
        protected static DBEntityTable get_all(Type type)
        {
            return find_by_criteria("", type);
        }

        public virtual bool Save()
        {
            bool isCompleted;
            Validate();
            if (IsNewRecord())
            {
                isCompleted = Create();
            }else
            {
                isCompleted = Update();
            }
            if (isCompleted && this.RowState != DataRowState.Detached)
                this.AcceptChanges(); //Меняем обновляем RowState
            return isCompleted;
        }

        protected virtual void BeforeValidation()
        {
            ErrorsList.Clear();
        }
        
        private void Validate()
        {
            BeforeValidation();
            ValidateActions();
            AfterValidation();
        }

        /// <summary>
        /// В этом методе прописываются функции проверки вводимых данных
        /// </summary>
        protected virtual void ValidateActions()
        {

        }

        protected virtual void AfterValidation()
        {
            if (HasErrors()) ValidationException();
        }
        /// <summary>
        /// Добавляет новый объект в БД
        /// </summary>
        /// <returns></returns>
        public bool Create()
        {
            bool wasAdded = false;
            if (this.Table.PrimaryKey.Length > 0)
            {
                Debug.WriteLine("Create with primary key");
                wasAdded = createWithPrimaryKey();
            }else
            {
                Debug.WriteLine("Create no primary key");
                wasAdded = createWithoutPrimaryKey();
            }
            string insertQuery = makeInsertQuery();
            return wasAdded;// ((DBEntityTable)this.Table).WriteSingleQuery(insertQuery);
        }

        private bool createWithPrimaryKey()
        {
            long prevId;
            long afterId;
            long status = 0;
            bool completed;
            DBEntityTable t = ((DBEntityTable)this.Table);
            string lastIdQuery = makeSelectQuery(t.PrimaryKey[0].ColumnName) + $" ORDER BY {t.PrimaryKey[0].ColumnName} DESC LIMIT 1;";
            string insertQuery = makeInsertQuery();
            t.OpenDB();
            prevId = t.GetScalarValueFromDB(lastIdQuery);
            status = t.GetScalarValueFromDB(insertQuery);
            afterId = t.GetScalarValueFromDB(lastIdQuery);
            t.CloseDB();
            //System.Windows.Forms.MessageBox.Show($"after {afterId} prev {prevId} status {status} {this.GetType().Name}");

            completed = (afterId > prevId) && status == 0;
            if (completed) this[t.PrimaryKey[0].ColumnName] = (uint)afterId;
            Debug.WriteLine($"{t.PrimaryKey[0].ColumnName} = {this[t.PrimaryKey[0].ColumnName]}");
            return afterId > prevId;
        }

        private bool createWithoutPrimaryKey()
        {
            long prevCount;
            long afterCount;
            long status = 0;
            bool completed;
            string countQuery = makeSelectQuery("COUNT(*)");
            string insertQuery = makeInsertQuery();
            DBEntityTable t = ((DBEntityTable)this.Table);
            t.OpenDB();
            prevCount = t.GetScalarValueFromDB(countQuery);
            status = t.GetScalarValueFromDB(insertQuery);
            afterCount = t.GetScalarValueFromDB(countQuery);
            t.CloseDB();
            completed = (afterCount > prevCount) && status == 0;

            return afterCount > prevCount;

        }


        protected string makeWhereQueryForAllColumns()
        {
            string vals = String.Empty;
            int wasAdded = 0;
            string[] selfColumns = ((DBEntityTable)this.Table).getNotVirtualColumnNames();
            foreach (DataColumn col in this.Table.Columns)
            {
                if (!selfColumns.Contains(col.ColumnName)) continue; //Проверяем принадлежит ли данная колонка данному типу
                if (this.Table.PrimaryKey.Contains(col)) continue;
                if (wasAdded > 0)
                {
                    vals += " AND ";
                }
                vals += $"{col.ColumnName} = {dbColumnValue(col)}";
                wasAdded++;
            }
            return vals;
        }


        /// <summary>
        /// Обновляет объект в БД 
        /// </summary>
        /// <returns></returns>
        public bool Update()
        {
            string query = string.Empty;
            try
            {
                string criteria = primaryKeysAsCriteria();
                query = makeUpdateQuery(criteria);
                return ((DBEntityTable)this.Table).WriteSingleQuery(query);
            }catch(Exception ex)
            {
                Debug.WriteLine(query);
                Debug.WriteLine(ex.Message);
                throw (ex);
            }

        }

        /// <summary>
        /// Удаляет объект из БД
        /// </summary>
        /// <returns></returns>
        public virtual bool Destroy()
        {
            string query = makeDestroyQuery();
            return ((DBEntityTable)this.Table).WriteSingleQuery(query);
        }

        public string makeDestroyQuery()
        {
            string q = ((DBEntityTable)this.Table).DeleteQuery;
            int wasAdded = 0;
            string vals = String.Empty;
            string[] keys = primaryKeysColumnsAndValues();
            if (keys.Length > 0)
            {
                foreach (string k in keys)
                {
                    if (wasAdded > 0)
                    {
                        vals += " AND ";
                    }
                    vals += k;
                    wasAdded++;
                }
            }else
            {
                vals = makeWhereQueryForAllColumns();
                /*
                foreach (DataColumn col in this.Table.Columns)
                {
                    if (wasAdded > 0)
                    {
                        vals += " AND ";
                    }
                    vals += $"{col.ColumnName} = {dbColumnValue(col)}";
                    wasAdded++;
                }
                */
            }
            q += $" WHERE {vals}";
            return q;
        }
        public bool IsNewRecord()
        {
            return this.RowState == DataRowState.Added || this.RowState == DataRowState.Detached;
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
            string[] selfColumns = ((DBEntityTable)this.Table).getNotVirtualColumnNames();
            foreach (DataColumn col in this.Table.Columns)
            {
                if (!selfColumns.Contains(col.ColumnName)) continue; //Проверяем принадлежит ли данная колонка данному типу
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
            string[] selfColumns = ((DBEntityTable)this.Table).getNotVirtualColumnNames();
            if (String.IsNullOrWhiteSpace(upd_criteria)) throw new DBEntityException($"Отсутствует критерий для Update() объекта {this.GetType().Name}");
            foreach(DataColumn dc in this.Table.Columns)
            {
                if (!selfColumns.Contains(dc.ColumnName)) continue; //Проверяем принадлежит ли данная колонка данному типу
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
        /// Копирует все колонки сущности из аргумента
        /// </summary>
        /// <param name="entity"></param>
        public void FillColsFromEntity(BaseEntity entity)
        {
            DBEntityTable t = entity.Table as DBEntityTable;
            foreach(DataColumn col in t.Columns)
            {
                 if (t.PrimaryKey.Contains(col)) continue;
                 this[col.ColumnName] = entity[col.ColumnName];
            }
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

        protected string makeSelectQuery(string colName)
        {
            return makeSelectQuery().Replace("*", colName);
        }

        protected string dbColumnValue(DataColumn col)
        {
            return DBEntityTable.DBColumnValue(col, this);
            //return col.DataType == typeof(string) ? $"'{this[col.ColumnName].ToString()}'" : this[col.ColumnName].ToString();
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

        protected DateTime tryParseDateTime(string column_name)
        {
            DateTime t = DateTime.MinValue;
            DateTime.TryParse(this[column_name].ToString(), out t);
            return t;
        }





        public string[] ErrorsAsArray => ErrorsList.ToArray();

        public new bool HasErrors()
        {
            return ErrorsList.Count > 0;
        }
        
        public new void ClearErrors()
        {
            ErrorsList.Clear();
        }

        public void ValidationException()
        {
            throw new DBEntityException(ErrorsAsNumericList);
        }

        protected List<string> ErrorsList = new List<string>();
        public string ErrorsAsNumericList
        {
            get
            {
                string s = string.Empty;
                for(int i=0; i<ErrorsList.Count; i++)
                {
                    s += $"{i+1}) {ErrorsList[i]}; \n";
                }
                return s;
            }
        }

        public string ErrorsAsMarkerList
        {
            get
            {
                string s = string.Empty;
                for (int i = 0; i < ErrorsList.Count; i++)
                {
                    s += $" • {ErrorsList[i]}; \n";
                }
                return s;
            }
        }
        public string EntityRuName => entityRuName;
        protected string entityRuName; 


    }

    public class DBEntityException : Exception
    {
        int errorcode;
        public int ErrorCode => errorcode;
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
