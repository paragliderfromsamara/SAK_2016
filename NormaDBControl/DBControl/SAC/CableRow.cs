using System;
using System.Collections.Generic;
using System.Data;
using MySql.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NormaMeasure.DBControl;

namespace NormaMeasure.DBControl.SAC
{
    [DBTable("cable", "db_norma_sac", OldDBName = "bd_cable", OldTableName = "cable")]
    public class CableRow : BaseRow
    {
        public CableRow(DataRowBuilder builder) : base(builder)
        {
        }

        [DBColumn("cable_id", ColumnDomain.Int, Order =10, OldDBColumnName ="CabNum")]
        public int CableId
        {
            get
            {
                return (int)this["cable_id"];
            }
            set
            {
                this["cable_id"] = value;
            }
        }


        [DBColumn("name", ColumnDomain.String, OldDBColumnName ="name")]
        public string Name
        {
            get
            {
                return (string)this["name"];
            }
            set
            {
                this["name"] = value;
            }
        }

        [DBColumn("struct_name", ColumnDomain.String, OldDBColumnName = "CabNameStruct")]
        public string StructName
        {
            get
            {
                return (string)this["struct_name"];
            }
            set
            {
                this["struct_name"] = value;
            }
        }
    }
}
