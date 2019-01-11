using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MySql.Data.MySqlClient;
using System.Windows.Forms;

namespace NormaMeasure.DBControl
{
    public class DBTablesMigration
    {
        protected DBTable[] _tablesList;
        protected string _dbName;
        protected string _query;
        protected MySqlConnection _dbConnection;

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

        private int sendQuery()
        {
            int sts = 0;
            try
            {
                MySqlCommand cmd = new MySqlCommand(_query, _dbConnection);
                _dbConnection.Open();
                sts = cmd.ExecuteNonQuery();
                _dbConnection.Close();
                return sts;
            }
            catch (MySqlException ex)
            {
                MessageBox.Show(ex.Message, "Ошибка SQL запроса", MessageBoxButtons.OK);
                return sts;
            }
        }

        /// <summary>
        /// Удаление БД. Функция необходимая на момент отладки программы.
        /// </summary>
        private void dropDB()
        {
            _query = "DROP DATABASE IF EXISTS " + _dbName;
            sendQuery();
        }


        private void fillTableSeeds(DBTable table)
        {
            string seedsStr = table.FillSeedsString;
            if (!string.IsNullOrWhiteSpace(seedsStr))
            {
                _query = "select * from " + table.tableName;
                int sts = sendQuery();
                if (sts <= 0)
                {
                    _query = seedsStr;
                    _query = String.Format("INSERT IGNORE INTO {0} VALUES {1}", table.tableName, _query);
                    sendQuery();
                }
            }
        }

        /// <summary>
        /// Проверяет наличие БД на данном компьютере, если нет то создает ее и выбирает её
        /// </summary>
        /// <returns></returns>
        private string checkAndCreateDB()
        {
            try
            {
                string message = "Создаём базу данных испытаний с кодовой страницей cp1251, если она не создана";
                _query = "CREATE DATABASE IF NOT EXISTS " + _dbName + " DEFAULT CHARACTER SET cp1251";
                sendQuery();
                _query = "USE " + _dbName;
                sendQuery();
                return message;
            }
            catch (MySqlException ex)
            {
                MessageBox.Show(ex.Message, "Ошибка SQL запроса", MessageBoxButtons.OK);
                return ex.Message;
            }

        }


        /// <summary>
        /// Универсальный метод для проверки и добавления таблиц в БД
        /// </summary>
        /// <param name="tableName"></param>
        /// <param name="columns"></param>
        private void checkAndAddTable(DBTable table)
        {
            _query = table.AddTableString;
            sendQuery();
        }


        private void 
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
