using System;
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
        protected DataRow _dataRow;
        protected DBTable _dbTable;
        public string TableName => _dbTable.tableName;
        public string PrimaryKey => _dbTable.primaryKey;
        public DBTable DBTable => _dbTable;

        protected Dictionary<string, string> colValuesToDB
        {
            get
            {
                Dictionary<string, string> val = new Dictionary<string, string>();
                foreach(DBTableColumn col in _dbTable.columns)
                {
                    string v = getPropertyValueByColumnName(col.Name);
                    if (v != null)val[col.Name] = v;
                }
                return val;
            }
        }

        public virtual void OnColumnChanged()
        {

        }



        /// <summary>
        /// Достаёт свойства для заливки в БД
        /// </summary>
        /// <param name="name"></param>
        /// <returns></returns>
        protected abstract string getPropertyValueByColumnName(string name);
        

        public uint id => _id; 

        //protected abstract void fillParametersFromRow(DataRow row);

       // protected abstract void setDefaultParameters();

        public bool Save()
        {
            bool status;
            beforeSaveAction();
            if (_id == 0) status =  _create();
            else status = _update();
            afterSaveAction();
            return status;
        }

        protected bool _create()
        {
            MySQLDBControl mySql = new MySQLDBControl(_dbTable.dbName);
            string insertQuery = _dbTable.InsertQuery(colValuesToDB);
            string getLastQuery = _dbTable.SelectLastAddedQuery;
            DataTable dt = _dbTable.TableDS;
            bool wasLoaded = false;
            mySql.MyConn.Open();
            if (wasLoaded = mySql.RunNoQuery(insertQuery) == 0)
            {
                MySqlDataAdapter da = new MySqlDataAdapter(getLastQuery, mySql.MyConn);
                dt.Rows.Clear();
                da.Fill(dt);
            }
            mySql.MyConn.Close();
            if (dt.Rows.Count > 0 && wasLoaded) FillFromDataRow(dt.Rows[0]);
            return wasLoaded;
        }

        protected bool _update()
        {
            string query = _dbTable.UpdateQuery(colValuesToDB);
            query = $"{query} WHERE {_dbTable.tableName}.{_dbTable.primaryKey} = {_id} LIMIT 1";
            return SendQuery(query) == 0;
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
            DataTable dt = _dbTable.TableDS;
            MySQLDBControl mySql = new MySQLDBControl(_dbTable.dbName);
            mySql.MyConn.Open();
            MySqlDataAdapter da = new MySqlDataAdapter(query, mySql.MyConn);
            dt.Rows.Clear();
            da.Fill(dt);
            mySql.MyConn.Close();
            return dt;
        }

        protected void beforeSaveAction()
        {

        }

        protected void afterSaveAction()
        {

        }

        protected bool doValidation()
        {
            return true;
        }

        /// <summary>
        /// Отправляет список запросов в базу данных
        /// </summary>
        /// <param name="fields"></param>
        public  void SendQueriesList(string[] fields)
        {
            if (fields.Length == 0) return;
            MySQLDBControl mySql = new MySQLDBControl(_dbTable.dbName);
            mySql.MyConn.Open();
            foreach (string f in fields) mySql.RunNoQuery(f);
            mySql.MyConn.Close();
        }

        /// <summary>
        /// Отправляет одиночный запрос в базу данных
        /// </summary>
        /// <param name="query"></param>
        public long SendQuery(string query)
        {
            MySQLDBControl mySql = new MySQLDBControl(_dbTable.dbName);
            long v;
            mySql.MyConn.Open();
            v = mySql.RunNoQuery(query);
            mySql.MyConn.Close();
            return v;
        }



        protected bool GetById()
        {
            DataTable tab = getFromDB(_dbTable.selectByIdQuery);
            DataRow val = tab.Rows.Count > 0 ? tab.Rows[0] : null;
            if (val != null) FillFromDataRow(val);
            return val != null;
        }

        
        public DataTable GetAllFromDB()
        {
            return getFromDB(_dbTable.SelectAllQuery);
        }


        internal void FillFromDataRow(DataRow r)
        {
            try
            {
                foreach (string colName in _dbTable.GetColumnTitlesIncludeJoined(true))
                {
                    if (colName == _dbTable.primaryKey)
                    {
                        _id = ServiceFunctions.convertToUInt(r[colName]);
                    }
                    else
                    {
                        if (!setPropertyByColumnName(r[colName], colName)) throw new DBEntityException($"В setPropertyByColumnName класса {this.GetType().Name} отсутствует поле {colName}");
                    }
                }
            }catch(DBEntityException ex)
            {
                System.Windows.Forms.MessageBox.Show(ex.Message);
            }

        }

        protected abstract bool setPropertyByColumnName(object value, string colName);

        protected abstract void setDefaultProperties();

        protected abstract void initEntity();

    }

    public class DBEntityException : Exception
    {
        public DBEntityException(string err_text) : base(err_text)
        {
        }
    }
}
