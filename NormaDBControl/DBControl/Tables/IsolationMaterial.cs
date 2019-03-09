using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NormaMeasure.DBControl.Tables
{


    [DBTable("isolation_materials", "db_norma_sac", OldDBName = "bd_cable", OldTableName = "materialy_izol")]
    public class IsolationMaterial : BaseEntity
    {
        public IsolationMaterial(DataRowBuilder builder) : base(builder)
        {
        }

        public static DBEntityTable get_all_as_table()
        {
            DBEntityTable t = new DBEntityTable(typeof(IsolationMaterial));
            string select_cmd = $"{t.SelectQuery}";
            t.FillByQuery(select_cmd);
            return t;
        }

        [DBColumn("isolation_material_id", ColumnDomain.UInt, Order = 11, OldDBColumnName = "MaterInd", Nullable = false, IsPrimaryKey = true, AutoIncrement = true)]
        public uint MaterialId
        {
            get
            {
                return tryParseUInt("isolation_material_id");
            }
            set
            {
                this["isolation_material_id"] = value;
            }
        }

        [DBColumn("isolation_material_name", ColumnDomain.Tinytext, Order = 12, OldDBColumnName = "MaterName", Nullable = true)]
        public string MaterialName
        {
            get
            {
                return this["isolation_material_name"].ToString();
            }
            set
            {
                this["isolation_material_name"] = value;
            }
        }
    }
}
