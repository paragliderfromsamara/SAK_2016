using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace NormaMeasure.DBControl
{
    class DBTablesMigration
    {
        protected static DBTable[] _tablesList;
        protected static string _dbName;
        /// <summary>
        /// Список таблиц содержащийся в текущей БД
        /// </summary>
        public static DBTable[] tablesList
        {
            get
            {
                return _tablesList;
            }
        } 
    }

    struct DBTable
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
        public string[][] columns;
        /// <summary>
        /// Базовые данные вносимые в таблицу после её создания
        /// </summary>
        public string[][] seeds;
        /// <summary>
        /// Заголовок первичного ключа таблицы в текущей БД
        /// </summary>
        public string primaryKey;
    }

}
