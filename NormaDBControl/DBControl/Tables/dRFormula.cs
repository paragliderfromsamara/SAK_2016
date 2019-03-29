using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NormaMeasure.DBControl.Tables
{
    [DBTable("dr_formuls", "db_norma_sac", OldDBName = "bd_cable", OldTableName = "dr_formuls")]
    public class dRFormula : BaseEntity
    {
        public dRFormula(DataRowBuilder builder) : base(builder)
        {
        }


        public static dRFormula find_drFormula_by_id(uint dr_formula_id)
        {
            DBEntityTable r = find_by_primary_key(dr_formula_id, typeof(dRFormula));
            if (r.Rows.Count > 0) return (dRFormula)r.Rows[0];
            else return null;
        }

        public static DBEntityTable get_all_as_table()
        {
            DBEntityTable t = new DBEntityTable(typeof(dRFormula));
            string select_cmd = $"{t.SelectQuery}";
            t.FillByQuery(select_cmd);
            return t;
        }

        [DBColumn("dr_formula_id", ColumnDomain.UInt, Order = 10, OldDBColumnName = "DRInd", Nullable = true, IsPrimaryKey = true)]
        public uint FormulaId
        {
            get
            {
                return tryParseUInt("dr_formula_id");
            }
            set
            {
                this["dr_formula_id"] = value;
            }
        }


        [DBColumn("dr_formula_name", ColumnDomain.Tinytext, Order = 11, OldDBColumnName = "DRName", Nullable = true)]
        public string FormulaName
        {
            get
            {
                return this["dr_formula_name"].ToString();
            }
            set
            {
                this["dr_formula_name"] = value;
            }
        }

        [DBColumn("dr_formula_result_measure", ColumnDomain.Tinytext, Order = 12, OldDBColumnName = "ResEd", Nullable = true)]
        public string ResultMeasure
        {
            get
            {
                return this["dr_formula_result_measure"].ToString();
            }
            set
            {
                this["dr_formula_result_measure"] = value;
            }
        }

        [DBColumn("dr_formula_description", ColumnDomain.Tinytext, Order = 13, OldDBColumnName = "DROpis", Nullable = true)]
        public string Formula
        {
            get
            {
                return this["dr_formula_description"].ToString();
            }
            set
            {
                this["dr_formula_description"] = value;
            }
        }
    }
}
