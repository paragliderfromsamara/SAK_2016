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

        public static FrequencyRange get_by_frequencies(float min_freq, float step_freq, float max_freq)
        {
            DBEntityTable t = new DBEntityTable(typeof(FrequencyRange));
            FrequencyRange fr = t.NewRow() as FrequencyRange;
            fr.FrequencyMin = min_freq;
            fr.FrequencyMax = max_freq;
            fr.FrequencyStep = step_freq;
            fr.find_or_create();
            return fr;
        }

        protected void find_or_create()
        {
            DBEntityTable t = find_by_criteria(makeWhereQueryForAllColumns(), typeof(FrequencyRange));
            if (t.Rows.Count > 0)
            {
                this.FrequencyRangeId = (t.Rows[0] as FrequencyRange).FrequencyRangeId;
            }else
            {
                base.Save();
            }
        }

        [DBColumn(FreqRangeId_ColumnName, ColumnDomain.UInt, Order = 10, OldDBColumnName = "FreqDiapInd", Nullable = true, IsPrimaryKey = true, AutoIncrement = true)]
        public uint FrequencyRangeId
        {
            get
            {
                return tryParseUInt(FreqRangeId_ColumnName);
            }
            set
            {
                this[FreqRangeId_ColumnName] = value;
            }
        }

        [DBColumn("frequency_min", ColumnDomain.Float, Order = 11, OldDBColumnName = "FreqMin", Nullable = true)]
        public float FrequencyMin
        {
            get
            {
                return tryParseFloat("frequency_min");
            }
            set
            {
                this["frequency_min"] = value;
            }
        }

        [DBColumn("frequency_max", ColumnDomain.Float, Order = 12, OldDBColumnName = "FreqMax", Nullable = true)]
        public float FrequencyMax
        {
            get
            {
                return tryParseFloat("frequency_max");
            }
            set
            {
                this["frequency_max"] = value;
            }
        }

        [DBColumn("frequency_step", ColumnDomain.Float, Order = 13, OldDBColumnName = "FreqStep", Nullable = true)]
        public float FrequencyStep
        {
            get
            {
                return tryParseFloat("frequency_step");
            }
            set
            {
                this["frequency_step"] = value;
            }
        }

        public const string FreqRangeId_ColumnName = "frequency_range_id";

        
    }
}
