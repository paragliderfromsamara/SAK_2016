﻿using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NormaLib.DBControl.Tables
{
    [DBTable("frequency_ranges", "db_norma_measure", OldDBName = "bd_isp", OldTableName = "freq_diap")]
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

        public static FrequencyRange find_by_id(uint id)
        {
            DBEntityTable t = find_by_primary_key(id, typeof(FrequencyRange));
            if (t.Rows.Count > 0)
            {
                return (t.Rows[0] as FrequencyRange);
            }
            else
            {
                return null;
            }
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

        #region Колонки таблицы
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

        [DBColumn(FreqMin_ColumnName, ColumnDomain.Float, Order = 11, OldDBColumnName = "FreqMin", Nullable = true)]
        public float FrequencyMin
        {
            get
            {
                return tryParseFloat(FreqMin_ColumnName);
            }
            set
            {
                this[FreqMin_ColumnName] = value;
            }
        }

        [DBColumn(FreqMax_ColumnName, ColumnDomain.Float, Order = 12, OldDBColumnName = "FreqMax", Nullable = true)]
        public float FrequencyMax
        {
            get
            {
                return tryParseFloat(FreqMax_ColumnName);
            }
            set
            {
                this[FreqMax_ColumnName] = value;
            }
        }

        [DBColumn(FreqStep_ColumnName, ColumnDomain.Float, Order = 13, OldDBColumnName = "FreqStep", Nullable = true)]
        public float FrequencyStep
        {
            get
            {
                return tryParseFloat(FreqStep_ColumnName);
            }
            set
            {
                this[FreqStep_ColumnName] = value;
            }
        }

        public const string FreqRangeId_ColumnName = "frequency_range_id";
        public const string FreqMin_ColumnName = "frequency_min";
        public const string FreqMax_ColumnName = "frequency_max";
        public const string FreqStep_ColumnName = "frequency_step";

        #endregion

    }
}
