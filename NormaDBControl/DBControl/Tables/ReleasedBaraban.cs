using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NormaMeasure.DBControl.Tables
{
    [DBTable("released_barabans", "db_norma_sac", OldDBName = "bd_isp", OldTableName = "barabany")]
    public class ReleasedBaraban : BaseEntity
    {
        public ReleasedBaraban(DataRowBuilder builder) : base(builder)
        {
        }

        [DBColumn(BarabanId_ColumnName, ColumnDomain.UInt, Order = 10, OldDBColumnName = "BarabanInd", Nullable = true, IsPrimaryKey = true, AutoIncrement = true)]
        public uint BarabanId
        {
            get
            {
                return tryParseUInt(BarabanId_ColumnName);
            }
            set
            {
                this[BarabanId_ColumnName] = value;
            }
        }

        [DBColumn(BarabanType.TypeId_ColumnName, ColumnDomain.UInt, Order = 11, OldDBColumnName = "TipInd", Nullable = false, IsPrimaryKey = false)]
        public uint BarabanTypeId
        {
            get
            {
                return tryParseUInt(BarabanType.TypeId_ColumnName);
            }
            set
            {
                this[BarabanType.TypeId_ColumnName] = value;
            }
        }

        [DBColumn(BarabanSerialNumber_ColumnName, ColumnDomain.Tinytext, Order = 12, OldDBColumnName = "BarabanNum", Nullable = true, IsPrimaryKey = false)]
        public float SerialNumber
        {
            get
            {
                return tryParseFloat(BarabanSerialNumber_ColumnName);
            }
            set
            {
                this[BarabanSerialNumber_ColumnName] = value;
            }
        }


        public const string BarabanId_ColumnName = "serial_number";
        public const string BarabanSerialNumber_ColumnName = "serial_number";
    }
}
