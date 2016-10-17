using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using MySql.Data.MySqlClient;
using System.Windows.Forms;
using System.Xml;
using System.IO;
using System.IO.IsolatedStorage;

namespace SAK_2016
{
    public class dbBase
    {
        protected string tableName { set; get; }
        protected dbColumn[] dbCols { set; get; }
        protected selectQueries[] selectQueriesList {set; get;}

        /// <summary>
        /// Облачает текст в ковычки. Необходимо для преобразования строковых значений в формат sql запросов
        /// </summary>
        /// <param name="s"></param>
        /// <returns></returns>
       
        public static string qVal(dbColumn r)
        {
            return r.type == "string" ? String.Format("\"{0}\"", r.value) : r.value;
        }
        /// <summary>
        /// Отправляет запрос в БД
        /// </summary>
        /// <param name="query"></param>
        /// <returns></returns>
        protected int sendQuery(string query)
        {
            int sts = 0;
            try
            {
                MySqlConnection dbCon = new MySqlConnection(Properties.Settings.Default.rootConnectionString);
                MySqlCommand cmd = new MySqlCommand(query, dbCon);
                MySqlCommand useDb = new MySqlCommand("USE " + Properties.Settings.Default.dbName, dbCon);
                dbCon.Open();
                useDb.ExecuteNonQuery();
                sts = cmd.ExecuteNonQuery();
                dbCon.Close();
                dbCon.Dispose();
                cmd.Dispose();
                return sts;
            }
            catch (MySqlException ex)
            {
                MessageBox.Show(ex.Message, "Ошибка SQL запроса", MessageBoxButtons.OK);
                return sts;
            }

        }

        /// <summary>
        /// Добавляет объект в БД
        /// </summary>
        /// <returns></returns>
        public bool Create()
        {
            prepareForLoadToDB();
            string query = "INSERT INTO " + tableName + " VALUE " + makeValsQuery("create");
            int sts = sendQuery(query);
            return (sts > 0) ? true : false;
        }



        public void CreateIfNotExist(string[] criteria, dbColumn[] attrs)
        {
            
        }
        public bool Update(dbColumn[] col)
        {
            return true;
        }

        public void selectOne(dbColumn[] criterias)
        {
            //string q = String.Format("SELECT * FROM {0} WHERE id={1} LIMIT(1)", tableName, id);
        }
        public void setDbAttrs(string[][] key_vals)
        {

        }

        public static string makeWhere(dbColumn[] cls)
        {
            cls = prepareForLoadToDB(cls);
            string q = "";
            for(int i=0; i<cls.Length; i++)
            {
                q += cls[i].key + "=" + cls[i].value;
                if (i < cls.Length - 1) q += " AND ";
            }
            return q;

        }
        /// <summary>
        /// Подготавливает значения колонок для загрузки в БД
        /// </summary>
        protected void prepareForLoadToDB()
        {
            for (int i = 0; i < dbCols.Length; i++)
            {
                dbCols[i].value = (String.IsNullOrWhiteSpace(dbCols[i].value)) ? "Default" : qVal(dbCols[i]);
            }
        }
        public static dbColumn[] prepareForLoadToDB(dbColumn[] arr)
        {
            for (int i = 0; i < arr.Length; i++)
            {
                arr[i].value = (String.IsNullOrWhiteSpace(arr[i].value)) ? "Default" : qVal(arr[i]);
            }
            return arr;
        }
        /// <summary>
        /// Создает строку значений в зависимости от типа действия
        /// </summary>
        /// <param name="action"></param>
        /// <returns></returns>
        public string makeValsQuery(string action)
        {
            string val = "";
            if (action == "update")
            {
                for (int i = 0; i < dbCols.Length; i++)
                {
                    val += String.Format("{0}={1}", dbCols[i].key, dbCols[i].value);
                    if (i< dbCols.Length-1) val += ", ";
                }
                return val;
            }
            else
            {
                for (int i = 0; i < dbCols.Length; i++)
                {
                    val += String.Format("{0}", dbCols[i].value);
                    if (i < dbCols.Length - 1) val += ", ";
                }
                return "("+val+")";
            }
        }
        public string makeValsQuery(dbColumn[] cols, string action)
        {
            string val = "";
            if (action == "update")
            {
                for (int i = 0; i < cols.Length; i++)
                {
                    val += String.Format("{0}={1}", cols[i].key, cols[i].value);
                    if (i < cols.Length - 1) val += ", ";
                }
                return val;
            }
            else
            {
                for (int i = 0; i < cols.Length; i++)
                {
                    val += String.Format("{0}", cols[i].value);
                    if (i < cols.Length - 1) val += ", ";
                }
                return "(" + val + ")";
            }
        }
        protected string convertByteArrayToString(string s)
            {
        
            string r = "";
                byte[] v = Encoding.ASCII.GetBytes(s);
                foreach(byte b in v)
                {
                r += String.Format("{0}", b); ;
                } 
                return r;
            }
        /// <summary>
        /// Представляет собой структуру описывающую колонки таблицы
        /// </summary>
        public struct dbColumn
        {
            public string key, value, type;
            public string table_name;
            public dbColumn(string k, string t)
            {
                key = k;
                value = "";
                type = t;
                table_name = "null";

            }
            public dbColumn(string k, string v, string t, string tabName)
            {
                key = k;
                value = v;
                type = t;
                table_name = tabName;
            }


        }

        public struct selectQueries
        {
            public string qName;
            public string qString;
            public selectQueries(string qName, string qString)
            {
                this.qName = qName;
                this.qString = qString;

            }
        }

        protected string GetSQLCommand(string ncom)
        {
            try
            {
                XmlReaderSettings settings = new XmlReaderSettings();
                settings.ConformanceLevel = ConformanceLevel.Fragment;
                settings.IgnoreWhitespace = true;
                settings.IgnoreComments = true;
                XmlReader reader = XmlReader.Create("sql_queries.xml", settings);
                string scom = "";
                while (reader.Read()) if (reader.Name == ncom) { scom = reader.ReadString(); break; }
                reader.Close();
                if (scom == "") throw new DBException("Не найдена команда:'" + ncom + "' в sql_queries.xml!  ");
                return scom;
            }
            catch (FileNotFoundException)
            {
                MessageBox.Show("Не найден файл 'sql_queries.xml'! Повторная установка приложения поможет решить эту проблему!  ", "Ошибка...", MessageBoxButtons.OK, MessageBoxIcon.Error);
                throw new DBException("");
            }
            catch (XmlException ex)
            {
                MessageBox.Show(ex.Message, "Ошибка...", MessageBoxButtons.OK, MessageBoxIcon.Error);
                throw new DBException("");
            }
        }

    }


}