using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NormaMeasure.DBControl.Tables
{
    [DBTable("lead_materials", "db_norma_sac", OldDBName = "bd_cable", OldTableName = "materialy_gil")]
    public class LeadMaterial : BaseEntity
    {
        public LeadMaterial(DataRowBuilder builder) : base(builder)
        {
        }

        public static DBEntityTable get_all_as_table()
        {
            DBEntityTable t = new DBEntityTable(typeof(LeadMaterial));
            string select_cmd = $"{t.SelectQuery}";
            t.FillByQuery(select_cmd);
            return t;
        }

        [DBColumn("lead_material_id", ColumnDomain.UInt, Order = 11, OldDBColumnName = "MaterInd", Nullable = false, IsPrimaryKey = true, AutoIncrement = true)]
        public uint MaterialId
        {
            get
            {
                return tryParseUInt("lead_material_id");
            }
            set
            {
                this["lead_material_id"] = value;
            }
        }

        [DBColumn("lead_material_name", ColumnDomain.Tinytext, Order = 12, OldDBColumnName = "MaterName", Nullable = true)]
        public string MaterialName
        {
            get
            {
                return this["lead_material_name"].ToString();
            }
            set
            {
                this["lead_material_name"] = value;
            }
        }

        [DBColumn("lead_material_tkc", ColumnDomain.Float, Order = 13, OldDBColumnName = "TKC_1", Nullable = true)]
        public float MaterialTKC
        {
            get
            {
                return tryParseFloat("lead_material_tkc");
            }
            set
            {
                this["lead_material_tkc"] = value;
            }
        }
    }
}
