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

        [DBColumn("frequency_range_id", ColumnDomain.UInt, Order = 12, OldDBColumnName = "FreqDiapInd", DefaultValue = 0, ReferenceTo = "frequency_ranges(frequency_range_id)")]
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

        [DBColumn("length_bringing", ColumnDomain.Float, Order = 14, DefaultValue = 1000)]
        public uint LngthBringing
        {
            get
            {
                return tryParseUInt("length_bringing");
            }
            set
            {
                this["length_bringing"] = value;
            }
        }

        [DBColumn("min_value", ColumnDomain.Float, Order = 15, DefaultValue = -9999999)]
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

        [DBColumn("max_value", ColumnDomain.Float, Order = 16, DefaultValue = 9999999)]
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

        [DBColumn("percent", ColumnDomain.Float, Order = 17, DefaultValue = 100)]
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



    }
}
