using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using MySql.Data.MySqlClient;
using System.Windows.Forms;

namespace NormaMeasure.DBControl
{
    public class DBTablesMigration
    {
        protected DBTable[] _tablesList;
        protected string _dbName;
        protected string _dbUserName="root";
        protected string _dbServer="localhost";
        protected string _dbPassword="";
        protected string _query;
        protected MySqlConnection _dbConnection;
        private MySQLDBControl _dbControl;

        public DBTablesMigration()
        {
            _dbControl = new MySQLDBControl() { UserName = _dbUserName, UserPassword = _dbPassword, Server = _dbServer };
        }

        /// <summary>
        /// Список таблиц содержащийся в текущей БД
        /// </summary>
        public DBTable[] tablesList
        {
            get
            {
                return _tablesList;
            }
        }

        public void InitDataBase()
        {
            dropDB();
            checkAndCreateDB();
            CreateTables();
            FillSeeds();
            MigrateData();
        }

        public void MigrateData()
        {
            string txt = "";
            string[] listNames = _dbControl.GetDBList(); 
            foreach(string s in listNames) txt += s + "; \n";            
            MessageBox.Show(txt);
            //foreach(DBTable table in tablesList)
            //{
                //if table.oldTableName 
            //}
        }

        public void CreateTables()
        {
            foreach(DBTable table in tablesList)
            {
                checkAndAddTable(table);
            }
        }

        public void FillSeeds()
        {
            foreach (DBTable table in tablesList)
            {
                fillTableSeeds(table);
            }
        }

        private long sendQueryToCurrentDB()
        {
            return sendQueryToDB(_dbName, _query);
        }

        private MySqlDataReader getDataFromCurrentDB()
        {
            return getDataFromDB(_dbName, _query);
        }

        public long sendQueryToDB(string db_name, string query)
        {
            long status = 0;
            _dbControl.ConnectToDB(db_name);
            _dbControl.MyConn.Open();
            status = _dbControl.RunNoQuery(query);
            _dbControl.MyConn.Close();
            return status;
        }

        public MySqlDataReader getDataFromDB(string db_name, string query)
        {
            _dbControl.ConnectToDB(db_name);
            _dbControl.MyConn.Open();
            MySqlDataReader data = _dbControl.GetReader(query);
            _dbControl.MyConn.Close();
            return data;
        }


        /// <summary>
        /// Удаление БД. Функция необходимая на момент отладки программы.
        /// </summary>
        private void dropDB()
        {
            _query = "DROP DATABASE IF EXISTS " + _dbName;
            sendQueryToCurrentDB();
        }


        private void fillTableSeeds(DBTable table)
        {
            string seedsStr = table.FillSeedsString;
            if (!string.IsNullOrWhiteSpace(seedsStr))
            {
                _query = "select * from " + table.tableName;
                long sts = sendQueryToCurrentDB();
                if (sts <= 0)
                {
                    _query = seedsStr;
                    _query = String.Format("INSERT IGNORE INTO {0} VALUES {1}", table.tableName, _query);
                    sendQueryToCurrentDB();
                }
            }
        }

        /// <summary>
        /// Проверяет наличие БД на данном компьютере, если нет то создает ее и выбирает её
        /// </summary>
        /// <returns></returns>
        private string checkAndCreateDB()
        {
            string message = "Создаём базу данных испытаний с кодовой страницей cp1251, если она не создана";
            _query = "CREATE DATABASE IF NOT EXISTS " + _dbName + " DEFAULT CHARACTER SET cp1251";
            sendQueryToCurrentDB();
            _query = "USE " + _dbName;
            sendQueryToCurrentDB();
            return message;
        }

        /// <summary>
        /// Универсальный метод для проверки и добавления таблиц в БД
        /// </summary>
        /// <param name="tableName"></param>
        /// <param name="columns"></param>
        private void checkAndAddTable(DBTable table)
        {
            _query = table.AddTableString;
            sendQueryToCurrentDB();
        }

        private void procMySqlException(MySqlException ex)
        {
            MessageBox.Show(ex.Message, "Ошибка SQL запроса", MessageBoxButtons.OK, MessageBoxIcon.Error);
        }
        
    }

    public struct DBTable
    {
        /// <summary>
        /// Имя таблицы в БД текущей версии
        /// </summary>
        public string tableName;
        /// <summary>
        /// Имя таблицы в БД старой версии
        /// </summary>
        public string oldTableName;
        /// <summary>
        /// Имя старой БД, из которой необходимо смигрировать данные таблицы
        /// </summary>
        public string oldDbName;
        /// <summary>
        /// Массив колонок таблицы: 
        /// 0-имя в текущей БД, 
        /// 1-описание колонки в текущей БД
        /// 2-имя колонки в старой БД
        /// </summary>
        public DBTableColumn[] columns;
        /// <summary>
        /// Базовые данные вносимые в таблицу после её создания
        /// </summary>
        public string[][] seeds;
        /// <summary>
        /// Заголовок первичного ключа таблицы в текущей БД
        /// </summary>
        public string primaryKey;

        public string AddTableString
        {
            get
            {
                string colsTxt = "";
                if (columns != null)
                {
                    for (int i = 0; i < columns.Length; i++)
                    {
                        colsTxt += String.Format("{0}, ", columns[i].AddColumnText);
                    }
                    colsTxt += "PRIMARY KEY ("+primaryKey+")";
                }
                return String.Format("CREATE TABLE IF NOT EXISTS {0} ({1})", tableName, colsTxt);
            }
        }

        public string GetDataFromOldTableString
        {
            get
            {
                string str = "";
                for(int i=0; i<columns.Length; i++)
                {
                    if (string.IsNullOrWhiteSpace(columns[i].OldName))
                    {
                        if (!string.IsNullOrWhiteSpace(str)) str += ", ";
                        str += string.Format("{0}.{1} AS {2}", oldTableName, columns[i].OldName, columns[i].Name);
                    } 
                }
                if (!string.IsNullOrWhiteSpace(str))
                {
                    str = string.Format("SELECT {0} FROM {1}", str, oldTableName);
                }
                return str;
            }
        }

        public string FillSeedsString
        {
            get
            {
                string str = string.Empty;
                if (seeds != null)
                {
                    str = "";
                    for (int i = 0; i < seeds.Length; i++)
                    {
                        if (seeds[i] == null) break;
                        string tmp = string.Empty;
                        for(int j=0; j<seeds[i].Length; j++)
                        {
                            tmp += seeds[i][j];
                            if (j != seeds[i].Length - 1) tmp += ", ";
                        }
                        str += "(" + tmp + ")";
                        if (i != seeds.Length - 1) str += ", ";
                    }
                }
                return str;
            }
        }
    }

    public struct DBTableColumn
    {
        private string _name;
        private string _oldName;
        private string _columnType;
        private string _defaultValue;

        public string Name
        {
            get { return _name; }
            set { _name = value; }
        }

        public string OldName
        {
            get { return _oldName; }
            set { _oldName = value; }
        }

        public string Type
        {
            get { return _columnType; }
            set { _columnType = value; }
        }

        public string DefaultValue
        {
            get
            {
                return _defaultValue;
            }
            set { _defaultValue = value; }
        }

        public string AddColumnText
        {
            get
            {
                string txt;
                txt = String.Format("{0} {1}", Name, Type);
                if (hasDefaultValue) txt = String.Format("{0} DEFAULT {1}", txt, DefaultValue);
                return txt;
            }
        }
        private bool hasDefaultValue { get { return !String.IsNullOrEmpty(_defaultValue); } }

    }

}
