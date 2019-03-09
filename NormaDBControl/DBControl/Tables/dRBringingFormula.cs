using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NormaMeasure.DBControl.Tables
{
    [DBTable("dr_bringing_formuls", "db_norma_sac", OldDBName = "bd_cable", OldTableName = "dr_priv_formuls")]
    public class dRBringingFormula : BaseEntity
    {
        public dRBringingFormula(DataRowBuilder builder) : base(builder)
        {
        }

        public static DBEntityTable get_all_as_table()
        {
            DBEntityTable t = new DBEntityTable(typeof(dRBringingFormula));
            string select_cmd = $"{t.SelectQuery}";
            t.FillByQuery(select_cmd);
            return t;
        }

        [DBColumn("dr_bringing_formula_id", ColumnDomain.UInt, Order = 10, OldDBColumnName = "DRPrivInd", Nullable = true, IsPrimaryKey = true)]
        public uint FormulaId
        {
            get
            {
                return tryParseUInt("dr_bringing_formula_id");
            }
            set
            {
                this["dr_bringing_formula_id"] = value;
            }
        }


        [DBColumn("dr_bringing_formula_name", ColumnDomain.Tinytext, Order = 11, OldDBColumnName = "DRPrivName", Nullable = true)]
        public string FormulaName
        {
            get
            {
                return this["dr_bringing_formula_name"].ToString();
            }
            set
            {
                this["dr_bringing_formula_name"] = value;
            }
        }

        [DBColumn("dr_bringing_formula_description", ColumnDomain.Tinytext, Order = 12, OldDBColumnName = "DRPrivOpis", Nullable = true)]
        public string Formula
        {
            get
            {
                return this["dr_bringing_formula_description"].ToString();
            }
            set
            {
                this["dr_bringing_formula_description"] = value;
            }
        }
    }
}
