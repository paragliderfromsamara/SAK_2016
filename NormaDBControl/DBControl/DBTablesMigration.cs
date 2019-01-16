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
        protected static string dbName;
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
            //string txt = "";
            //string[] listNames = _dbControl.GetDBList(); 
            //foreach(string s in listNames) txt += s + "; \n";            
            //MessageBox.Show(txt);
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
            return sendQueryToDB(dbName, _query);
        }

        private MySqlDataReader getDataFromCurrentDB()
        {
            return getDataFromDB(dbName, _query);
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
            _query = "DROP DATABASE IF EXISTS " + dbName;
            sendQueryToCurrentDB();
        }


        private void fillTableSeeds(DBTable table)
        {
            string seedsStr = table.FillSeedsQuery;
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
            _query = "CREATE DATABASE IF NOT EXISTS " + dbName + " DEFAULT CHARACTER SET cp1251";
            sendQueryToCurrentDB();
            _query = "USE " + dbName;
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
            _query = table.AddTableQuery;
            sendQueryToCurrentDB();
        }

        private void procMySqlException(MySqlException ex)
        {
            MessageBox.Show(ex.Message, "Ошибка SQL запроса", MessageBoxButtons.OK, MessageBoxIcon.Error);
        }
        
    }


    public struct DBTable
    {
        public string dbName;

        private string _selectQuery;

        /// <summary>
        /// Имя таблицы в БД текущей версии
        /// </summary>
        public string tableName;

        /// <summary>
        /// Название сущности преставленной в текущей таблице в единственном числе
        /// </summary>
        public string entityName;

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

        public string selectString;

        public string joinString;

        public string selectAllQuery;

        public string selectByIdQuery;

        public string UpdateQuery(Dictionary<string, string> colVals)
        {
            string setVals = string.Empty;
            int wasAdded = 0;
            foreach (DBTableColumn col in columns)
            {
                if (colVals.ContainsKey(col.Name))
                {
                    if (wasAdded > 0) setVals += ", ";
                    setVals += $"{col.Name} = {colVals[col.Name]}";
                    wasAdded++;
                }
            }
            return $"UPDATE {tableName} SET {setVals}";
        }
        public string InsertQuery(Dictionary<string, string> colVals)
        {
            string cols = string.Empty;
            string vals = string.Empty;
            int wasAdded = 0;
            foreach (DBTableColumn col in columns)
            {
                if (colVals.ContainsKey(col.Name))
                {
                    if (wasAdded > 0)
                    {
                        cols += ", ";
                        vals += ", ";
                    }
                    cols += col.Name;
                    vals += colVals[col.Name];
                    wasAdded++;
                }
            }
            return $"INSERT INTO {tableName} ({cols}) VALUES ({vals})";
        }


        public string AddTableQuery
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

        private DBTableColumn[] getJoinedTableColumns()
        {
            List<DBTableColumn> jTableCols = new List<DBTableColumn>();
            foreach(DBTableColumn col in columns)
            {
                if (col.JoinedTable.HasValue) jTableCols.Add(col);
            }
            return jTableCols.ToArray();
        }


        private string buildSelectString()
        {
            string selStr = "";
            string[] colsArr = GetColumnTitlesIncludeJoined(false);
            for(int i=0; i<colsArr.Length; i++)
            {
                if (i > 0 && i < colsArr.Length) selStr += ", ";
                selStr += colsArr[i];
            }
            return selStr;
        }
        private string buildJoinString()
        {
            string joinStr = "";
            DBTableColumn[] joined = getJoinedTableColumns();
            if (joined.Length > 0)
            {
                joinStr = " ";
                foreach (DBTableColumn col in joined)
                {
                    joinStr += string.Format("LEFT OUTER JOIN {0} ON {1}.{2} = {0}.{3} ", col.JoinedTable.Value.tableName, tableName, col.Name, col.JoinedTable.Value.primaryKey);
                }
            }
            return joinStr;
        }

        public bool HasJoinedTable
        {
            get
            {
                foreach (DBTableColumn col in columns) if (col.JoinedTable.HasValue) return true;
                return false;
            }
        }

        public string SelectQuery
        {
            get
            {
                if (string.IsNullOrWhiteSpace(_selectQuery))
                {
                    string select = buildSelectString();
                    string join = buildJoinString();
                    _selectQuery = string.Format("SELECT {0} FROM {1} {2}", select, tableName, join);
                }
                return _selectQuery;
            }
        }

        /// <summary>
        /// Создаёт запрос для импорта данных из старой БД
        /// </summary>
        public string GetDataFromOldTableQuery
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

        /// <summary>
        /// Формирует DataSet соответствующий данной таблице
        /// </summary>
        public DataTable TableDS
        {
            get
            {
                DataTable dt = new DataTable(tableName);
                MySqlCommandBuilder cb = new MySqlCommandBuilder();
                string[] columnsArr = GetColumnTitlesIncludeJoined(true);
                foreach (string colName in columnsArr)
                {
                    dt.Columns.Add(colName); 
                }
                return dt;
            }
        }
        public string ClearTableQuery
        {
            get
            {
                return String.Format("DELETE FROM {0}", tableName);
            }
        }

        public string DeleteByCriteriaQuery
        {
            get
            {
                return String.Format("DELETE FROM {0} WHERE ", tableName);
            }
        }

        public string BuildDeleteByCriteriaQuery(string criteria)
        {
            return string.Format("{0} WHERE {2}", DeleteByCriteriaQuery, criteria);
        }

        /// <summary>
        /// Создает запрос для заполнения исходных данных
        /// </summary>
        public string FillSeedsQuery
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

        public string[] GetColumnTitlesIncludeJoined(bool isForDataSet)
        {
            List<string> cols = new List<string>();
            if (isForDataSet)
            {
                foreach (DBTableColumn tCol in columns)
                {
                    cols.Add(tCol.Name);
                    if (tCol.JoinedTable.HasValue)
                    {
                        foreach (DBTableColumn jtCol in tCol.JoinedTable.Value.columns)
                        {
                            if(jtCol.Name != tCol.JoinedTable.Value.primaryKey) //Выключаем основной ключ соединительной таблицы из выборки
                            {
                                cols.Add(string.Format("{0}_{1}", tCol.JoinedTable.Value.entityName, jtCol.Name));
                            }
                        }
                    }
                }
            }
            else
            {
                foreach (DBTableColumn tCol in columns)
                {
                    cols.Add(string.Format("{0}.{1} AS {1}", tableName, tCol.Name));
                    if (tCol.JoinedTable.HasValue)
                    {
                        foreach (DBTableColumn jtCol in tCol.JoinedTable.Value.columns)
                        {
                            if (jtCol.Name != tCol.JoinedTable.Value.primaryKey) //Выключаем основной ключ соединительной таблицы из выборки
                            {
                                cols.Add(string.Format("{0}.{1} AS {2}_{1}", tCol.JoinedTable.Value.tableName, jtCol.Name, tCol.JoinedTable.Value.entityName));
                            }
                        }
                    }
                }
            }

            return cols.ToArray();
        }

    }

    public struct DBTableColumn
    {
        private string _name;
        private string _oldName;
        private string _columnType;
        private string _defaultValue;
        private DBTable? _joinedTable;
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

        public DBTable? JoinedTable
        {
            get { return _joinedTable; }
            set { _joinedTable = value; }
        }
        private bool hasDefaultValue { get { return !String.IsNullOrEmpty(_defaultValue); } }

    }



}
