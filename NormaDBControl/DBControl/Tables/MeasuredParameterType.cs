using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NormaMeasure.DBControl.Tables
{
    [DBTable("measured_parameter_types", "db_norma_sac", OldDBName = "bd_cable", OldTableName = "ism_param")]
    public class MeasuredParameterType : BaseEntity
    {
        public MeasuredParameterType(DataRowBuilder builder) : base(builder)
        {
        }

        public static DBEntityTable get_all_as_table()
        {
            DBEntityTable t = new DBEntityTable(typeof(MeasuredParameterType));
            string select_cmd = $"{t.SelectQuery}";
            t.FillByQuery(select_cmd);
            return t;
        }

        [DBColumn("parameter_type_id", ColumnDomain.UInt, Order = 11, OldDBColumnName = "ParamInd", Nullable = false, IsPrimaryKey = true)]
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


        [DBColumn("parameter_name", ColumnDomain.Tinytext, Order = 12, OldDBColumnName = "ParamName", Nullable = true)]
        public string ParameterName
        {
            get
            {
                return this["parameter_name"].ToString();
            }
            set
            {
                this["parameter_name"] = value;
            }
        }

        [DBColumn("parameter_measure", ColumnDomain.Tinytext, Order = 13, OldDBColumnName = "Ed_izm", Nullable = true)]
        public string Measure
        {
            get
            {
                return this["parameter_measure"].ToString();
            }
            set
            {
                this["parameter_measure"] = value;
            }
        }

        [DBColumn("parameter_description", ColumnDomain.Tinytext, Order = 14, OldDBColumnName = "ParamOpis", Nullable = true)]
        public string Description
        {
            get
            {
                return this["parameter_description"].ToString();
            }
            set
            {
                this["parameter_description"] = value;
            }
        }
    }

}
