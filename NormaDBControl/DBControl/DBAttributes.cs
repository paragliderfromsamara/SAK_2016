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
        public object DefaultValue;
    }

    public enum ColumnDomain
    {
        UInt,
        Int,
        Float,
        String,
        Boolean,
        DateTime
    }
}
