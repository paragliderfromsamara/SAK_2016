using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NormaMeasure.DBControl.Tables
{
    [DBTable("frequency_ranges", "db_norma_sac", OldDBName = "bd_isp", OldTableName = "freq_diap")]
    public class FrequencyRange : BaseEntity
    {
        public FrequencyRange(DataRowBuilder builder) : base(builder)
        {
        }

        [DBColumn("frequency_range_id", ColumnDomain.UInt, Order = 10, OldDBColumnName = "FreqDiapInd", Nullable = true, IsPrimaryKey = true, AutoIncrement = true)]
        public uint FrequencyRangeId
        {
            get
            {
                return tryParseUInt("frequency_range_id");
            }
            set
            {
                this["frequency_range_id"] = value;
            }
        }

        [DBColumn("frequency_min", ColumnDomain.UInt, Order = 11, OldDBColumnName = "FreqMin", Nullable = true)]
        public uint FrequencyMin
        {
            get
            {
                return tryParseUInt("frequency_min");
            }
            set
            {
                this["frequency_min"] = value;
            }
        }

        [DBColumn("frequency_max", ColumnDomain.UInt, Order = 12, OldDBColumnName = "FreqMax", Nullable = true)]
        public uint FrequencyMax
        {
            get
            {
                return tryParseUInt("frequency_max");
            }
            set
            {
                this["frequency_max"] = value;
            }
        }

        [DBColumn("frequency_step", ColumnDomain.UInt, Order = 13, OldDBColumnName = "FreqStep", Nullable = true)]
        public uint FrequencyStep
        {
            get
            {
                return tryParseUInt("frequency_step");
            }
            set
            {
                this["frequency_step"] = value;
            }
        }
    }
}
