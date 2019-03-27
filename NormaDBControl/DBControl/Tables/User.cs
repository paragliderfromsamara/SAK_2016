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
        private UserRole _role;
        
        public UserRole Role
        {
            get
            {
                if (_role == null)
                {
                    _role = UserRole.find_by_role_id(this.RoleId);
                }
                return _role;
            }
        }
         
        public User(DataRowBuilder builder) : base(builder)
        {
        }

        public static User SignIn(User u)
        {
            User user = null;
            string query = $"last_name = '{u.LastName}' AND employee_number = '{u.EmployeeNumber}' AND password = '{u.Password}' AND is_active = 1 LIMIT 1";
            DBEntityTable t = find_by_criteria(query, typeof(User)); // new DBEntityTable();
            if (t.Rows.Count > 0) user = (User)t.Rows[0];
            return user;
        }

        public override bool Save()
        {
            try
            {
                return base.Save();
            }catch(DBEntityException ex)
            {
                System.Windows.Forms.MessageBox.Show(ex.Message, "Не удалось сохранить пользователя...", System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Warning);
                return false;
            }
        }

        protected override void ValidateActions()
        {
            base.ValidateActions();
            CheckLastNamePrescence();
            CheckEmployeeNumber();
        }

        private void CheckEmployeeNumber()
        {
            if (String.IsNullOrWhiteSpace(EmployeeNumber))
            {
                this.ErrorsList.Add("Не указан табельный номер пользователя");
            }
            else
            {
                string query = $"employee_number = '{EmployeeNumber}' AND is_active = 1 AND NOT user_id = {UserId}"; 
                DBEntityTable t = find_by_criteria(query, typeof(User));
                if (t.Rows.Count > 0)
                {
                    this.ErrorsList.Add("Указанный табельный номер принадлежит другому пользователю");
                }
            }
        }

        private void CheckLastNamePrescence()
        {
            if (String.IsNullOrWhiteSpace(LastName))
            {
                this.ErrorsList.Add("Невозможно сохранить пользователя без фамилии");
            }
        }

        public static User build()
        {
            DBEntityTable t = new DBEntityTable(typeof(User));
            User u = (User)t.NewRow();
            u.RoleId = 4;
            u.LastName = String.Empty;
            u.FirstName = String.Empty;
            u.ThirdName = String.Empty;
            u.EmployeeNumber = String.Empty;
            u.Password = String.Empty;
            return u;
        }

        public static DBEntityTable get_all_as_table()
        {
            DBEntityTable rolesTable = new DBEntityTable(typeof(UserRole));
            string select_cmd = $"LEFT OUTER JOIN {rolesTable.TableName} USING({rolesTable.PrimaryKey[0]}) WHERE is_active = 1 AND NOT user_id = 1"; // 
            DBEntityTable t = find_by_criteria(select_cmd, typeof(User));//new DBEntityTable(typeof(User));
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
