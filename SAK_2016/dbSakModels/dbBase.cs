using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using MySql.Data.MySqlClient;
using System.Windows.Forms;
using System.Xml;
using System.IO;
using System.IO.IsolatedStorage;
using System.Data;

namespace NormaMeasure.SAC_APP
{
    public class dbBase
    {
        private DBControl mySql = new DBControl(Properties.dbSakQueries.Default.dbName);
        protected string tableName;
        public long id;
        protected DataRowCollection dbParams;
        public bool isExistsInDB;


        /// <summary>
        /// Проверяет наличие записи с данным id в БД и устанавливает значение атрибута isExists в true/false
        /// </summary>
        protected void getFromDB()
        {
            DataSet ds = new DataSet();
            string q = String.Format("SELECT * FROM {0} WHERE id={1}", this.tableName, id);
            MySqlDataAdapter da = new MySqlDataAdapter(q, mySql.MyConn);
            mySql.MyConn.Open();
            da.Fill(ds);
            mySql.MyConn.Close();
            if (ds.Tables[0].Rows.Count > 0)
            {
                this.dbParams = ds.Tables[0].Rows;
                this.isExistsInDB = true;
            } else isExistsInDB = false;
        }

        /// <summary>
        /// Ищет строку по запросу переданному параметром query
        /// </summary>
        /// <param name="query"></param>
        protected void getFromDB(string query)
        {
            DataSet ds = new DataSet();
            MySqlDataAdapter da = new MySqlDataAdapter(query, mySql.MyConn);
            mySql.MyConn.Open();
            da.Fill(ds);
            mySql.MyConn.Close();
            if (ds.Tables[0].Rows.Count > 0)
            {
                this.dbParams = ds.Tables[0].Rows;
                this.isExistsInDB = true;
            }
            else isExistsInDB = false;
        }

        protected void initFromDb()
        {
            this.getFromDB();
            if (this.isExistsInDB)
            {
                fillMainParameters();
            }
        }

        protected void initFromDb(string query)
        {
            this.getFromDB(query);
            if (this.isExistsInDB)
            {
                fillMainParameters();
            }
        }

        /// <summary>
        /// Собирает параметры cущностей, для каждого класса определяется отдельно
        /// </summary>
         protected virtual void fillMainParameters() { MessageBox.Show("Создай функцию в дочернем классе!!!"); }
      

    }


}