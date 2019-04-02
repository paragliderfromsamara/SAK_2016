using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NormaMeasure.DBControl.Tables
{
    [DBTable("measured_parameter_data", "db_norma_sac", OldDBName = "bd_isp", OldTableName = "param_data")]
    public class MeasuredParameterData : BaseEntity
    {
        public MeasuredParameterData(DataRowBuilder builder) : base(builder)
        {
        }

        public static MeasuredParameterData GetByParameters(CableStructureMeasuredParameterData cab_struct_data)
        {
            Random r = new Random();
            MeasuredParameterData mpd = build_with_data(cab_struct_data);
            //mpd.MeasureParameterDataId = (uint)r.Next();
            mpd.find_or_create();
            return mpd;

        }

        public static MeasuredParameterData build_with_data(CableStructureMeasuredParameterData cab_struct_data)
        {
            MeasuredParameterData data = build();
            data.ParameterTypeId = cab_struct_data.ParameterTypeId;

            data.MinValue =  MeasuredParameterType.IsHasMinLimit(data.ParameterTypeId) ? cab_struct_data.MinValue : MinValueDefault;
            data.MaxValue = MeasuredParameterType.IsHasMaxLimit(data.ParameterTypeId) ? cab_struct_data.MaxValue : MaxValueDefault;
            data.Percent = cab_struct_data.Percent;
            data.LngthBringingTypeId = cab_struct_data.LngthBringingTypeId;
            data.LengthBringing =  LengthBringingType.NoBringing == data.LngthBringingTypeId ? LengthBringingDefault :  cab_struct_data.LengthBringing;

            data.FrequencyRangeId = MeasuredParameterType.IsItFreqParameter(data.ParameterTypeId) ? FrequencyRange.get_by_frequencies(cab_struct_data.FrequencyMin, cab_struct_data.FrequencyStep, cab_struct_data.FrequencyMax).FrequencyRangeId : 1;
            data.find_or_create();
            return data;
        }

        protected void find_or_create()
        {
            DBEntityTable t = find_by_criteria(makeWhereQueryForAllColumns(), typeof(MeasuredParameterData));
            if (t.Rows.Count > 0)
            {
                this.MeasureParameterDataId = (t.Rows[0] as MeasuredParameterData).MeasureParameterDataId;
                //System.Windows.Forms.MessageBox.Show($"find_or_create() on measured_parameter_data {MeasureParameterDataId}");
            }
            else
            {
                this.Save();
            }
        }

        public static MeasuredParameterData build()
        {
            DBEntityTable t = new DBEntityTable(typeof(MeasuredParameterData));
            MeasuredParameterData mpd = (MeasuredParameterData)t.NewRow();
            return mpd;
        }


        public static MeasuredParameterData find_by_id(uint id)
        {
            DBEntityTable t = new DBEntityTable(typeof(MeasuredParameterData));
            string select_cmd = $"{t.SelectQuery} WHERE measured_parameter_data_id = {id}";
            t.FillByQuery(select_cmd);
            if (t.Rows.Count > 0) return (MeasuredParameterData)t.Rows[0];
            else
            {
                return null;
            }
        }

        [DBColumn("measured_parameter_data_id", ColumnDomain.UInt, Order = 10, IsPrimaryKey = true, AutoIncrement = true)]
        public uint MeasureParameterDataId
        {
            get
            {
                return tryParseUInt("measured_parameter_data_id");
            }
            set
            {
                this["measured_parameter_data_id"] = value;
            }
        }

        [DBColumn("parameter_type_id", ColumnDomain.UInt, Order = 11, ReferenceTo = "measured_parameter_types(parameter_type_id)")]
        public uint ParameterTypeId
        {
            get
            {
                return tryParseUInt("parameter_type_id");
            }
            set
            {
                this["parameter_type_id"] = value;
            }
        }

        [DBColumn("frequency_range_id", ColumnDomain.UInt, Order = 12, OldDBColumnName = "FreqDiapInd", DefaultValue = 1, ReferenceTo = "frequency_ranges(frequency_range_id)")]
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

        [DBColumn("length_bringing_type_id", ColumnDomain.UInt, Order = 13, DefaultValue = 0, ReferenceTo = "length_bringing_types(length_bringing_type_id)")]
        public uint LngthBringingTypeId
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

        [DBColumn("length_bringing", ColumnDomain.Float, Order = 14, DefaultValue = LengthBringingDefault)]
        public float LengthBringing
        {
            get
            {
                return tryParseFloat("length_bringing");
            }
            set
            {
                this["length_bringing"] = value;
            }
        }

        [DBColumn("min_value", ColumnDomain.Float, Order = 15, DefaultValue = MinValueDefault)]
        public float MinValue
        {
            get
            {
                return tryParseFloat("min_value");
            }
            set
            {
                this["min_value"] = value;
            }
        }

        [DBColumn("max_value", ColumnDomain.Float, Order = 16, DefaultValue = MaxValueDefault)]
        public float MaxValue
        {
            get
            {
                return tryParseFloat("max_value");
            }
            set
            {
                this["max_value"] = value;
            }
        }

        [DBColumn("percent", ColumnDomain.Float, Order = 17, DefaultValue = PercentDefault)]
        public uint Percent
        {
            get
            {
                return tryParseUInt("percent");
            }
            set
            {
                this["percent"] = value;
            }
        }

        public const float MinValueDefault = -1000000000;
        public const float MaxValueDefault = 1000000000;
        public const float LengthBringingDefault = 1000;
        public const float PercentDefault = 100;
    }
}
