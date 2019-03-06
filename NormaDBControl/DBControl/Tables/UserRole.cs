using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NormaMeasure.DBControl.Tables
{
    [DBTable("user_roles", "db_norma_sac", OldDBName = "bd_system", OldTableName = "dolshnosti")]
    public class UserRole : BaseEntity
    {
        public UserRole(DataRowBuilder builder) : base(builder)
        {
        }

        public static DBEntityTable get_all_as_table()
        {
            DBEntityTable t = new DBEntityTable(typeof(UserRole));
            string select_cmd = $"{t.SelectQuery}";
            t.FillByQuery(select_cmd);
            return t;
        }


        [DBColumn("user_role_id", ColumnDomain.UInt, Order = 10, OldDBColumnName = "DolshNum", Nullable = true, IsPrimaryKey = true, AutoIncrement = true)]
        public uint UserRoleId
        {
            get
            {
                return tryParseUInt("user_role_id");
            }
            set
            {
                this["user_role_id"] = value;
            }
        }

        [DBColumn("user_role_name", ColumnDomain.Tinytext, Order = 11, OldDBColumnName = "Dolshnost", Nullable = true, IsPrimaryKey = false)]
        public string UserRoleName
        {
            get
            {
                return this["user_role_name"].ToString();
            }
            set
            {
                this["user_role_name"] = value;
            }
        }
    }

}
