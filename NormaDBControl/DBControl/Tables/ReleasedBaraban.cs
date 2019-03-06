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

        [DBColumn("baraban_id", ColumnDomain.UInt, Order = 10, OldDBColumnName = "BarabanInd", Nullable = true, IsPrimaryKey = true, AutoIncrement = true)]
        public uint BarabanId
        {
            get
            {
                return tryParseUInt("baraban_id");
            }
            set
            {
                this["baraban_id"] = value;
            }
        }

        [DBColumn("baraban_type_id", ColumnDomain.UInt, Order = 11, OldDBColumnName = "TipInd", Nullable = false, IsPrimaryKey = false)]
        public uint BarabanTypeId
        {
            get
            {
                return tryParseUInt("baraban_type_id");
            }
            set
            {
                this["baraban_type_id"] = value;
            }
        }

        [DBColumn("serial_number", ColumnDomain.Tinytext, Order = 12, OldDBColumnName = "BarabanNum", Nullable = true, IsPrimaryKey = false)]
        public float SerialNumber
        {
            get
            {
                return tryParseFloat("serial_number");
            }
            set
            {
                this["serial_number"] = value;
            }
        }
    }
}
