using System;
using System.Collections.Generic;
using System.Text;
using System.IO;
using System.IO.IsolatedStorage;
using System.Windows.Forms;
using System.Xml;
using System.Threading;
using MySql.Data.MySqlClient;

namespace NormaMeasure.DBControl
{
    class MySQLDBControl : IDisposable
    {
        #region Данные класса DBControl
        public MySqlConnection MyConn;
        MySqlCommand MC;
        private string _db_name = "";
        private string _userName = "root";
        private string _userPassword = "";
        private string _server = "localhost";

        public string DBName
        {
            get
            {
                return _db_name;
            }
        }
        public string UserName
        {  
            get
            {
                return _userName;
            }
            set
            {
                this._userName = value;
            }

        }

        public string Server
        {
            get
            {
                return _server;
            }
            set
            {
                _server = value;
            }
        }
        public string UserPassword
        {
            get
            {
                return _userPassword;
            }
            set
            {
                this._userPassword = value;
            }
        }
        public string ConnectionString
        {
            get
            {
                return String.Format("UserId={0};Server={1};Password={2}; CharacterSet=cp1251;", UserName, Server, UserPassword);
            }
        }


        #endregion
        //------------------------------------------------------------------------------------------------------------------------

        public MySQLDBControl()
        {
            setSQLServerConnection();
        }

        public MySQLDBControl(string cb) : this()
        {
            connectToDB(cb);
        }

        private void setSQLServerConnection()
        {
            try
            { 
               MyConn = new MySqlConnection(ConnectionString);
               MC = new MySqlCommand() { Connection = MyConn };
            }
            catch (MySqlException ex)
            {
                MessageBox.Show(ex.Message, "Ошибка...", MessageBoxButtons.OK, MessageBoxIcon.Error);
                //throw new DBException(ex.ErrorCode, "MySQL сервер не доступен!  ");
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Ошибка...", MessageBoxButtons.OK, MessageBoxIcon.Error);
                //throw new DBException(0, "MySQL сервер не доступен!  ");
            }
        }

        public bool ConnectToDB(string db_name)
        {
            bool flag = false;
            if (!string.IsNullOrWhiteSpace(db_name))
            {
                if (db_name == _db_name) flag = true;
                else
                {
                    if (IsDBExists(db_name)) flag = connectToDB(db_name);
                }
            }
            return flag;
        }

        private bool connectToDB(string db_name)
        {
            bool flag = false;
            try
            {
                MyConn.Open();
                MC.CommandText = "USE " + db_name;
                MC.ExecuteScalar();
                MyConn.Close();
                _db_name = db_name;
                flag = true;
            }
            catch (MySqlException ex)
            {
                MessageBox.Show(ex.Message, "Ошибка...", MessageBoxButtons.OK, MessageBoxIcon.Error);
                //throw new DBException(ex.ErrorCode, "MySQL сервер не доступен!  ");
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Ошибка...", MessageBoxButtons.OK, MessageBoxIcon.Error);
                //throw new DBException(0, "MySQL сервер не доступен!  ");
            }
            return flag;
        }
        //------------------------------------------------------------------------------------------------------------------------
        //KRA Functions  

        public static string setDbDefaultIfNull(string str)
        {
            if (String.IsNullOrWhiteSpace(str))
                return "DEFAULT";
            else
                return str;
        }
        /// <summary>
        /// Возвращает количество записей из таблицы с названием tableName. Необходимо наличие в файле с запросами запроса с названием tableName_count
        /// </summary>
        /// <param name="tableName"></param>
        /// <returns></returns>
        public long Count(string tableName)
        {
            return RunNoQuery(GetSQLCommand(tableName + "_count"));
        }

        public string[] GetDBList()
        {
            string query = "SHOW DATABASES";
            System.Collections.Generic.List<string> list = new List<string>();
            MySqlDataReader r;
            MyConn.Open();
            r = GetReader(query);
            while (r.Read()) list.Add(r.GetString("database"));
            r.Close();
            MyConn.Close();

            
            
            return list.ToArray();
        }

        public bool IsDBExists(string db_name)
        {
            string[] dbList = GetDBList();
            bool flag = false;
            foreach(string s in dbList)
            {
                if (flag = (s == db_name)) break;
            }
            return flag;
        }

        //------------------------------------------------------------------------------------------------------------------------
        public void Dispose()
        {
            MyConn.Close();
            MyConn.Dispose();
            MC.Dispose();
        }
        //------------------------------------------------------------------------------------------------------------------------
        public string GetSQLCommand(string ncom)
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
        //------------------------------------------------------------------------------------------------------------------------
        public MySqlDataReader GetReaderXML(string comtp)
        {
            string sc = GetSQLCommand(comtp);
            return GetReader(sc);
        }

        //------------------------------------------------------------------------------------------------------------------------
        public MySqlDataReader GetReader(string comm)
        {
            try
            {
                MC.CommandText = comm;
                return MC.ExecuteReader();
            }
            catch (MySqlException ex)
            {
                MessageBox.Show(ex.Message + " №" + ex.ErrorCode.ToString() + " SQL команда: " + comm, "Ошибка...", MessageBoxButtons.OK, MessageBoxIcon.Error);
                throw new DBException("");
            }
        }
        //------------------------------------------------------------------------------------------------------------------------
        public long RunNoQuery(string comm)
        {
            repeat:
            try
            {
                MC.CommandText = comm;
                object or = MC.ExecuteScalar();
                long ret;
                if (or != null) long.TryParse(or.ToString(), out ret);
                else ret = 0;
                return ret;
            }
            catch (MySqlException ex)
            {
                //MessageBox.Show($"{DBName} {ex.ErrorCode} {ex.Number}");
                if (ex.Number == 1046 && ex.ErrorCode == -2147467259 && !string.IsNullOrWhiteSpace(DBName))
                {
                   // MessageBox.Show($"{DBName} was connected");
                    MC.CommandText = "USE " + DBName;
                    MC.ExecuteScalar();
                    goto repeat;
                }else
                {
                    MessageBox.Show(ex.Message + " №" + ex.ErrorCode.ToString() + " " + ex.Number.ToString() + " SQL команда: " + comm, "Ошибка...", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    throw new DBException("");
                }
            }
        }
        /// <summary>
        /// Проверяет наличие записи в таблице по названию таблицы и условиям в основноей БД
        /// </summary>
        /// <param name="tabName">Наименование </param>
        /// <param name="conditions"></param>
        /// <returns></returns>
        public bool checkFieldExistingInDb(string tabName, string conditions)
        {
            string qe = String.Format("SELECT * FROM {0} WHERE {1};", tabName, conditions);
            return (RunNoQuery(qe) > 0) ? true : false;
        }
        /// <summary>
        /// Проверяет наличие записи в таблице по названию таблицы и условиям в основноей БД
        /// </summary>
        /// <param name="tabName">Наименование </param>
        /// <param name="conditions"></param>
        /// <returns></returns>
        public bool checkFieldExistingInDb(string tabName)
        {
            string qe = String.Format("SELECT * FROM {0}", tabName);
            return (RunNoQuery(qe) > 0) ? true : false;
        }
        //------------------------------------------------------------------------------------------------------------------------
        public string GetOneValue(string com)
        {
            MySqlDataReader msdr = GetReader(com);
            string ins = "";
            if (!msdr.Read()) return ins;
            ins = msdr[0].ToString();
            msdr.Close();
            return ins;
        }
        //------------------------------------------------------------------------------------------------------------------------
        public string GetOneText(string com)
        {
            MySqlDataReader msdr = GetReader(com);
            string ins = "";
            if (!msdr.Read()) return ins;
            ins = msdr[0].ToString();
            while (msdr.Read())
                if (msdr[0].ToString() != ins) { msdr.Close(); return "#Разное#"; }
                else ins = msdr[0].ToString();
            msdr.Close();
            return ins;

        }
        //------------------------------------------------------------------------------------------------------------------------
        public string GetMultiCommand(string fromXML, List<string> ellist)
        {
            string sc = GetSQLCommand(fromXML);
            int pos;
            pos = sc.IndexOf('#');
            if (pos < 0) return "";
            sc = sc.Remove(pos, 1);
            StringBuilder sb = new StringBuilder(ellist.Count * ellist[0].Length);
            foreach (string el in ellist) sb.Append(el + ",");
            sb = sb.Remove(sb.Length - 1, 1);
            sc = sc.Insert(pos, sb.ToString());
            return sc;
        }
        //------------------------------------------------------------------------------------------------------------------------
        public List<string> GetListXML(string com)
        {
            com = GetSQLCommand(com);
            return GetList(com);
        }
        //------------------------------------------------------------------------------------------------------------------------
        public List<string> GetList(string com)
        {
            MySqlDataReader msdr = GetReader(com);
            List<string> ls = new List<string>();
            while (msdr.Read()) ls.Add(msdr[0].ToString());
            msdr.Close();
            return ls;
        }
        //------------------------------------------------------------------------------------------------------------------------
        public string GetOneCommand(string comXML, string val)
        {
            string str = GetSQLCommand(comXML);
            if (val != null && val != "")
            {
                int pos;
                while ((pos = str.IndexOf('#')) >= 0)
                {
                    str = str.Remove(pos, 1);
                    str = str.Insert(pos, val);
                }
                return str;
            }
            return str;
        }
        //------------------------------------------------------------------------------------------------------------------------        
        public void GetTwoListsXML(string comXML, List<string> Ids, List<string> CSumms)
        {
            MySqlDataReader msdr = GetReaderXML(comXML);
            Ids.Clear();
            CSumms.Clear();
            while (msdr.Read()) { Ids.Add(msdr[0].ToString()); CSumms.Add(msdr[1].ToString()); }
            msdr.Close();
        }
    }
    //\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\
    public class DBException : Exception
    {
        int errorcode;
        public int ErrorCode { get { return errorcode; } }
        public DBException(string mess)
            : base(mess)
        {
            errorcode = 0;
        }
        public DBException(int errcode)
        {
            errorcode = errcode;
        }
        public DBException(int errcode, string mess)
            : base(mess)
        {
            errorcode = errcode;
        }
    }
    //\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\
    //\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\
}

