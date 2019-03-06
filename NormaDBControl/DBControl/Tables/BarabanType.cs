using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NormaMeasure.DBControl.Tables
{
    [DBTable("baraban_types", "db_norma_sac", OldDBName = "bd_isp", OldTableName = "tipy_baraban")]
    public class BarabanType : BaseEntity
    {
        public BarabanType(DataRowBuilder builder) : base(builder)
        {
        }

        [DBColumn("baraban_type_id", ColumnDomain.UInt, Order = 10, OldDBColumnName = "TipInd", Nullable = true, IsPrimaryKey = true)]
        public uint TypeId
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

        [DBColumn("baraban_type_name", ColumnDomain.Tinytext, Order = 11, OldDBColumnName = "TipName", Nullable = true, IsPrimaryKey = false)]
        public string TypeName
        {
            get
            {
                return this["baraban_type_name"].ToString();
            }
            set
            {
                this["baraban_type_name"] = value;
            }
        }

        [DBColumn("baraban_weight", ColumnDomain.Float, Order = 12, OldDBColumnName = "Massa", Nullable = true, IsPrimaryKey = false)]
        public float BarabanWeight
        {
            get
            {
                return tryParseFloat("baraban_weight");
            }
            set
            {
                this["baraban_weight"] = value;
            }
        }

    }
}
