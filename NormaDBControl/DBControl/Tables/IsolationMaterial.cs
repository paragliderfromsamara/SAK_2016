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

        [DBColumn("isolation_material_id", ColumnDomain.UInt, Order = 11, OldDBColumnName = "MaterInd", Nullable = false, IsPrimaryKey = true)]
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
