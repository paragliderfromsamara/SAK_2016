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

namespace SAK_2016
{
    public class dbBase
    {
        private DBControl mySql = new DBControl(Properties.dbSakQueries.Default.dbName);
        protected string tableName { set; get; }
        public long id;
        protected DataRow dbParams;
        public bool isExistsInDB;
        /// <summary>
        /// Проверяет наличие записи с данным id в БД и устанавливает значение атрибута isExists в true/false
        /// </summary>
        protected void getFromDB()
        {
            DataSet ds = new DataSet();
            string q = String.Format("SELECT * FROM {0} WHERE {1} LIMIT 1", tableName, id);
            MySqlDataAdapter da = new MySqlDataAdapter(q, mySql.MyConn);
            mySql.MyConn.Open();
            da.Fill(ds);
            mySql.MyConn.Close();
            if (ds.Tables[0].Rows.Count > 0)
            {
                dbParams = ds.Tables[0].Rows[0];
                isExistsInDB = true;
            } else isExistsInDB = true;
        }
      

    }


}