﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Threading.Tasks;
using MySql.Data.MySqlClient;

namespace NormaMeasure.DBControl
{
    public abstract class DBEntityBase
    {
        protected uint _id = 0;
        protected DBTable _dbTable;
        public string getAllQuery;
        protected string getByIdQuery;
        protected string createQuery;
        protected string _byIdQuery;

        static protected string dbName = "default_db_name";
        static protected string selectString = "*";

        protected DataRow _dataRow = null;
        

        public uint id
        {
            get { return _id; }
        }


        //protected abstract void fillParametersFromRow(DataRow row);

       // protected abstract void setDefaultParameters();

        public bool Save()
        {

            return true;
        }

        protected bool _create()
        {
            return true;
        }

        protected bool _update()
        {
            return true;
        }

        public static void find(uint id)
        {
        }

        protected bool NeedLoadFromDB(DataRow row)
        {
            bool f = false;
            foreach (DBTableColumn col in _dbTable.columns)
            {
                f = row.IsNull(col.Name);
                if (f) break;
            }
            return f;
        }

        protected DataTable getFromDB(string query)
        {
            DataSet ds = _dbTable.TableDS;
            MySQLDBControl mySql = new MySQLDBControl(dbName);
            mySql.MyConn.Open();
            MySqlDataAdapter da = new MySqlDataAdapter(query, mySql.MyConn);
            ds.Tables[_dbTable.tableName].Rows.Clear();
            da.Fill(ds.Tables[_dbTable.tableName]);
            mySql.MyConn.Close();
            return ds.Tables[_dbTable.tableName];
        }

        protected long UpdateField(string tableName, string updVals, string condition)
        {
            MySQLDBControl mySql = new MySQLDBControl(dbName);
            string query = BuildUpdQuery(tableName, updVals, condition);
            long v;
            mySql.MyConn.Open();
            v = mySql.RunNoQuery(query);
            mySql.MyConn.Close();
            return v;
        }

        /// <summary>
        /// Отправляет список запросов в базу данных
        /// </summary>
        /// <param name="fields"></param>
        public static void SendQueriesList(string[] fields)
        {
            if (fields.Length == 0) return;
            MySQLDBControl mySql = new MySQLDBControl(dbName);
            mySql.MyConn.Open();
            foreach (string f in fields) mySql.RunNoQuery(f);
            mySql.MyConn.Close();
        }

        /// <summary>
        /// Отправляет одиночный запрос в базу данных
        /// </summary>
        /// <param name="query"></param>
        public static void SendQuery(string query)
        {
            MySQLDBControl mySql = new MySQLDBControl(dbName);
            mySql.MyConn.Open();
            mySql.RunNoQuery(query);
            mySql.MyConn.Close();
        }

        protected static string BuildDestroyQueryWithCriteria(string tableName, string condition)
        {
            return String.Format("DELETE FROM {0} WHERE {1}", tableName, condition);
        }


        protected static string BuildUpdQuery(string tableName, string updVals, string condition)
        {
            return String.Format("UPDATE {0} SET {1} WHERE {2}", tableName, updVals, condition);
        }


        protected bool GetById()
        {
            DataTable tab = getFromDB(getByIdQuery);
            DataRow val = tab.Rows.Count > 0 ? tab.Rows[0] : null;
            if (val != null) this._dataRow = val;
            return val != null;
        }

        protected object getValueFromDataRowByKey(string key)
        {
            if (_dataRow == null) return null;
            else
            {
                return _dataRow[key]; 
            }
        }

        protected string getStringValueFromDataRow(string key)
        {
            object val = getValueFromDataRowByKey(key);

            if (val == null)
            {
                return String.Empty;
            }
            else
            {
                return val.ToString();
            }
        }


        protected int getIntValueFromDataRow(string key)
        {
            object val = getValueFromDataRowByKey(key);

            if (val == null)
            {
                return 0;
            }
            else
            {
                return ServiceFunctions.convertToInt16(val);
            }
        }

        protected decimal getDecimalValueFromDataRow(string key)
        {
            object val = getValueFromDataRowByKey(key);
            if (val == null)
            {
                return 0;
            }
            else
            {
                return ServiceFunctions.convertToDecimal(val);
            }
        }

        protected DataTable GetAllFromDB()
        {
            return getFromDB(getAllQuery);
        }

        protected abstract void fillEntityFromReader(MySqlDataReader r);

    }
}
