using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NormaMeasure.DBControl.Tables
{
    [DBTable("length_bringing_types", "db_norma_sac", OldDBName = "bd_isp", OldTableName = "lpriv_tip")]
    public class LengthBringingType : BaseEntity
    {
        public LengthBringingType(DataRowBuilder builder) : base(builder)
        {
        }

        [DBColumn("length_bringing_type_id", ColumnDomain.UInt, Order = 10, OldDBColumnName = "LprivInd", Nullable = true, IsPrimaryKey = true)]
        public uint TypeId
        {
            get
            {
                return tryParseUInt("length_bringing_type_id");
            }
            set
            {
                this["length_bringing_type_id"] = value;
            }
        }

        [DBColumn("measure_title", ColumnDomain.Tinytext, Order = 11, OldDBColumnName = "Ed_izm", Nullable = false, IsPrimaryKey = false)]
        public string MeasureTitle
        {
            get
            {
                return this["measure_title"].ToString();
            }
            set
            {
                this["measure_title"] = value;
            }
        }

        [DBColumn("length_bringing_name", ColumnDomain.Tinytext, Order = 12, OldDBColumnName = "LprivName", Nullable = false, IsPrimaryKey = false)]
        public string BringingName
        {
            get
            {
                return this["length_bringing_name"].ToString();
            }
            set
            {
                this["length_bringing_name"] = value;
            }
        }

        internal static DataTable get_all_as_table()
        {
            return get_all(typeof(LengthBringingType));
        }

        /// <summary>
        /// Без приведения
        /// </summary>
        public static uint NoBringing => 0;
        /// <summary>
        /// К строительной длине
        /// </summary>
        public static uint ForBuildLength => 1;
        /// <summary>
        /// К одному километру
        /// </summary>
        public static uint ForOneKilometer => 2;
        /// <summary>
        /// Другая длина в метрах
        /// </summary>
        public static uint ForAnotherLengthInMeters => 3;
    }
}
