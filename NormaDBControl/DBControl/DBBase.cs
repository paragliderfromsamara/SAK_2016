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
        protected static DBTable dbTable;

        protected Dictionary<string, string> colValuesToDB
        {
            get
            {
                Dictionary<string, string> val = new Dictionary<string, string>();
                foreach(DBTableColumn col in dbTable.columns)
                {
                    string v = getPropertyValueByColumnName(col.Name);
                    if (v != null)val[col.Name] = v;
                }
                return val;
            }
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
            if (_id == 0) return _create();
            else return _update();
        }

        protected bool _create()
        {
            string query = dbTable.InsertQuery(colValuesToDB);
            return SendQuery(query) == 0;
        }

        protected bool _update()
        {
            string query = dbTable.UpdateQuery(colValuesToDB);
            query = $"{query} WHERE {dbTable.tableName}.{dbTable.primaryKey} = {_id} LIMIT 1";
            return SendQuery(query) == 0;
        }

        public static void find(uint id)
        {
        }

        protected bool NeedLoadFromDB(DataRow row)
        {
            bool f = false;
            foreach (DBTableColumn col in dbTable.columns)
            {
                f = row.IsNull(col.Name);
                if (f) break;
            }
            return f;
        }

        protected static DataTable getFromDB(string query)
        {
            DataTable dt = dbTable.TableDS;
            MySQLDBControl mySql = new MySQLDBControl(dbTable.dbName);
            mySql.MyConn.Open();
            MySqlDataAdapter da = new MySqlDataAdapter(query, mySql.MyConn);
            dt.Rows.Clear();
            da.Fill(dt);
            mySql.MyConn.Close();
            return dt;
        }


        /// <summary>
        /// Отправляет список запросов в базу данных
        /// </summary>
        /// <param name="fields"></param>
        public static void SendQueriesList(string[] fields)
        {
            if (fields.Length == 0) return;
            MySQLDBControl mySql = new MySQLDBControl();
            mySql.MyConn.Open();
            foreach (string f in fields) mySql.RunNoQuery(f);
            mySql.MyConn.Close();
        }

        /// <summary>
        /// Отправляет одиночный запрос в базу данных
        /// </summary>
        /// <param name="query"></param>
        public static long SendQuery(string query)
        {
            MySQLDBControl mySql = new MySQLDBControl();
            long v;
            mySql.MyConn.Open();
            v = mySql.RunNoQuery(query);
            mySql.MyConn.Close();
            return v;
        }



        protected bool GetById()
        {
            DataTable tab = getFromDB(dbTable.selectByIdQuery);
            DataRow val = tab.Rows.Count > 0 ? tab.Rows[0] : null;
            if (val != null) fillEntityFromDataRow(val);
            return val != null;
        }

        
        protected static DataTable GetAllFromDB()
        {
            return getFromDB(dbTable.selectAllQuery);
        }



        protected void fillEntityFromDataRow(DataRow r)
        {
            try
            {
                foreach (string colName in dbTable.GetColumnTitlesIncludeJoined(true))
                {
                    if (colName == dbTable.primaryKey)
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

        

    }

    public class DBEntityException : Exception
    {
        public DBEntityException(string err_text) : base(err_text)
        {
        }
    }
}
