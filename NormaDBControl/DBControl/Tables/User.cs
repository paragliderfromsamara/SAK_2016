using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NormaMeasure.DBControl.Tables
{
    [DBTable("users", "db_norma_sac", OldDBName = "bd_system", OldTableName = "familija_imja_ot")]
    public class User : BaseEntity
    {
        public User(DataRowBuilder builder) : base(builder)
        {
        }

        public static User build()
        {
            DBEntityTable t = new DBEntityTable(typeof(User));
            User u = (User)t.NewRow();
            u.RoleId = 4;
            return u;
        }

        public static DBEntityTable get_all_as_table()
        {
            DBEntityTable t = new DBEntityTable(typeof(User));
            string select_cmd = $"{t.SelectQuery} LEFT OUTER JOIN user_roles USING(user_role_id) WHERE is_active = 1 AND NOT user_id = 1"; // 
            t.FillByQuery(select_cmd);
            return t;
        }

        public string FullNameShort
        {
            get
            {
                string _nameFull = LastName;
                if (!String.IsNullOrWhiteSpace(FirstName)) _nameFull += $"{FirstName.ToUpper()[0]}.";
                if (!String.IsNullOrWhiteSpace(ThirdName)) _nameFull += $"{ThirdName.ToUpper()[0]}.";
                return _nameFull;
            }
        }

        public bool DeleteUser()
        {
            this.IsActive = false;
            return this.Save();
        }


        [DBColumn("user_id", ColumnDomain.UInt, Order = 10, OldDBColumnName = "UserNum", Nullable = true, IsPrimaryKey = true, AutoIncrement = true)]
        public uint UserId
        {
            get
            {
                return tryParseUInt("user_id");
            }
            set
            {
                this["user_id"] = value;
            }
        }

        [DBColumn("last_name", ColumnDomain.Tinytext, Order = 11, OldDBColumnName = "familija", Nullable = true, IsPrimaryKey = false)]
        public string LastName
        {
            get
            {
                return this["last_name"].ToString();
            }
            set
            {
                this["last_name"] = value;
            }
        }

        [DBColumn("first_name", ColumnDomain.Tinytext, Order = 12, OldDBColumnName = "imja", Nullable = true, IsPrimaryKey = false)]
        public string FirstName
        {
            get
            {
                return this["first_name"].ToString();
            }
            set
            {
                this["first_name"] = value;
            }
        }

        [DBColumn("third_name", ColumnDomain.Tinytext, Order = 13, OldDBColumnName = "Otchestvo", Nullable = true, IsPrimaryKey = false)]
        public string ThirdName
        {
            get
            {
                return this["third_name"].ToString();
            }
            set
            {
                this["third_name"] = value;
            }
        }

        [DBColumn("employee_number", ColumnDomain.Tinytext, Order = 14, OldDBColumnName = "TabNum", Nullable = true, IsPrimaryKey = false)]
        public string EmployeeNumber
        {
            get
            {
                return this["employee_number"].ToString();
            }
            set
            {
                this["employee_number"] = value;
            }
        }

        [DBColumn("user_role_id", ColumnDomain.Tinytext, Order = 15, OldDBColumnName = "Dolshnost", Nullable = false, IsPrimaryKey = false)]
        public uint RoleId
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

        [DBColumn("password", ColumnDomain.Tinytext, Order = 16, OldDBColumnName = "Pass", Nullable = true, IsPrimaryKey = false)]
        public string Password
        {
            get
            {
                return this["password"].ToString();
            }
            set
            {
                this["password"] = value;
            }
        }

        [DBColumn("is_active", ColumnDomain.Boolean, Order = 17, OldDBColumnName = "Activ", DefaultValue = true, Nullable = true, IsPrimaryKey = false)]
        public bool IsActive
        {
            get
            {
                return tryParseBoolean("is_active", true);
            }
            set
            {
                this["is_active"] = value;
            }
        }
    }
}
