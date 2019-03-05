using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.Sql;

namespace NormaMeasure.DBControl
{
    [AttributeUsage (AttributeTargets.Class, Inherited =false, AllowMultiple =false)]
    public class DBTableAttribute : Attribute
    {
        
        public DBTableAttribute(string tableName, string dbName)
        {

            TableName = tableName;
            DBName = dbName;
        }

        public readonly string TableName;
        public readonly string DBName;
        public string OldTableName;
        public string OldDBName;
        public string Seeds;
        public DBTable TableStruct
        {
            get
            {
                DBTable t = new DBTable() { tableName = TableName, dbName = DBName, oldTableName = OldTableName, oldDbName = OldDBName };
                return t;
            }
        }
        
    }

    public class DBColumnAttribute : Attribute
    {
        public DBColumnAttribute(string columnName, ColumnDomain dataType)
        {
            ColumnName = columnName;
            DataType = dataType;
            Order = orderNumber;
        }

        public static int GenerateNextOrderNumber()
        {
            return orderNumber++;
        }

        public static int orderNumber = 100;

        public readonly string ColumnName;
        public string OldDBColumnName = null;
        public readonly ColumnDomain DataType;
        public bool Nullable = false;
        public int Size;
        public int Order;
        public bool IsPrimaryKey = false;
        public object DefaultValue = null;
        public string SetTypeValue = null;

        public DBTableColumn ColumnStruct
        {
            get
            {
                DBTableColumn c = new DBTableColumn() { DefaultValue = DefaultValue, SetTypeValue = SetTypeValue,ColumnType = DataType, Size = Size, Name = ColumnName, OldName = OldDBColumnName, IsPrimaryKey = IsPrimaryKey, Nullable = Nullable};
                return c;
            }
        }

    }


}
