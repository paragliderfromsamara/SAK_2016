using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NormaLib.DBControl.Tables
{
    [DBTable("users", "db_norma_measure", OldDBName = "bd_system", OldTableName = "familija_imja_ot")]
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
           // FillFullName();
        }

        protected void FillFullName()
        {
            if (this.RowState == DataRowState.Deleted) return;
            try
            {
                this[FullName_ColumnName] = FirstName;
            }
            catch(System.Reflection.TargetInvocationException)
            {

            }
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
            }catch(DBEntityException)
            {
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

        /// <summary>
        /// Поиск пользователей которых можно выбрать в роли оператора
        /// </summary>
        /// <returns></returns>
        public static DBEntityTable get_allowed_for_cable_test()
        {
            string query = $"SELECT *, {SelectQuery_WithFullNameShort} FROM users WHERE {UserRole.RoleId_ColumnName} = {UserRole.Operator} AND {IsActiveFlag_ColumnName} = 1 AND NOT {UserId_ColumnName} = 1";
            return find_by_query(query, typeof(User));
        }

        public static DBEntityTable get_all_as_table()
        {
            DBEntityTable rolesTable = new DBEntityTable(typeof(UserRole));

            string select_cmd = $"SELECT *, {SelectQuery_WithFullNameShort} FROM users LEFT OUTER JOIN {rolesTable.TableName} USING({rolesTable.PrimaryKey[0]}) WHERE {IsActiveFlag_ColumnName} = 1 AND NOT {UserId_ColumnName} = 1"; // 
            DBEntityTable t = find_by_query(select_cmd, typeof(User));//new DBEntityTable(typeof(User));
            return t;
        }


        public string FullNameShort
        {
            get
            {
                if (!Table.Columns.Contains(FirstName_ColumnName) || !Table.Columns.Contains(LastName_ColumnName) || !Table.Columns.Contains(ThirdName_ColumnName)) return "";
                string _nameFull = LastName;
                if (!String.IsNullOrWhiteSpace(FirstName)) _nameFull += $" {FirstName.ToUpper()[0]}.";
                if (!String.IsNullOrWhiteSpace(ThirdName)) _nameFull += $" {ThirdName.ToUpper()[0]}.";
                return _nameFull;
            }
        }

        public bool DeleteUser()
        {
            this.IsActive = false;
            return this.Save();
        }


        [DBColumn(UserId_ColumnName, ColumnDomain.UInt, Order = 10, OldDBColumnName = "UserNum", Nullable = true, IsPrimaryKey = true, AutoIncrement = true)]
        public uint UserId
        {
            get
            {
                return tryParseUInt(UserId_ColumnName);
            }
            set
            {
                this[UserId_ColumnName] = value;
            }
        }

        [DBColumn(LastName_ColumnName, ColumnDomain.Tinytext, Order = 11, OldDBColumnName = "familija", Nullable = true, IsPrimaryKey = false)]
        public string LastName
        {
            get
            {
                return this[LastName_ColumnName].ToString();
            }
            set
            {
                this[LastName_ColumnName] = value;
            }
        }

        [DBColumn(FirstName_ColumnName, ColumnDomain.Tinytext, Order = 12, OldDBColumnName = "imja", Nullable = true, IsPrimaryKey = false)]
        public string FirstName
        {
            get
            {
                return this[FirstName_ColumnName].ToString();
            }
            set
            {
                this[FirstName_ColumnName] = value;
            }
        }

        [DBColumn(ThirdName_ColumnName, ColumnDomain.Tinytext, Order = 13, OldDBColumnName = "Otchestvo", Nullable = true, IsPrimaryKey = false)]
        public string ThirdName
        {
            get
            {
                return this[ThirdName_ColumnName].ToString();
            }
            set
            {
                this[ThirdName_ColumnName] = value;
            }
        }

        [DBColumn(EmployeeNumber_ColumnName, ColumnDomain.Tinytext, Order = 14, OldDBColumnName = "TabNum", Nullable = true, IsPrimaryKey = false)]
        public string EmployeeNumber
        {
            get
            {
                return this[EmployeeNumber_ColumnName].ToString();
            }
            set
            {
                this[EmployeeNumber_ColumnName] = value;
            }
        }

        [DBColumn(UserRole.RoleId_ColumnName, ColumnDomain.Tinytext, Order = 15, OldDBColumnName = "Dolshnost", Nullable = false, IsPrimaryKey = false)]
        public uint RoleId
        {
            get
            {
                return tryParseUInt(UserRole.RoleId_ColumnName);
            }
            set
            {
                this[UserRole.RoleId_ColumnName] = value;
            }
        }

        [DBColumn(Password_ColumnName, ColumnDomain.Tinytext, Order = 16, OldDBColumnName = "Pass", Nullable = true, IsPrimaryKey = false)]
        public string Password
        {
            get
            {
                return this[Password_ColumnName].ToString();
            }
            set
            {
                this[Password_ColumnName] = value;
            }
        }

        [DBColumn(IsActiveFlag_ColumnName, ColumnDomain.Boolean, Order = 17, OldDBColumnName = "Activ", DefaultValue = true, Nullable = true, IsPrimaryKey = false)]
        public bool IsActive
        {
            get
            {
                return tryParseBoolean(IsActiveFlag_ColumnName, true);
            }
            set
            {
                this[IsActiveFlag_ColumnName] = value;
            }
        }

        [DBColumn(FullName_ColumnName, ColumnDomain.Tinytext, Order = 18, Nullable = true, IsVirtual = true)]
        public string FullName
        {
            get
            {
                return this[FullName_ColumnName].ToString();
            }
        }

        public static string SelectQuery_WithFullNameShort
        {
            get
            {

                string firstNameCond = $"IF(TRIM({FirstName_ColumnName}) != '', CONCAT(' ', SUBSTRING({FirstName_ColumnName}, 1, 1), '.'), '')"; //, (IF {FirstName_ColumnName} = '' THEN CONCAT(SUBSTRING('Assd' ,1 , 1), '.') END IF) 
                string thirdNameCond = $"IF(TRIM({ThirdName_ColumnName}) != '', CONCAT(' ', SUBSTRING({ThirdName_ColumnName}, 1, 1), '.'), '')";
                string query = $"CONCAT({ LastName_ColumnName}, { firstNameCond}, { thirdNameCond} ) AS { FullName_ColumnName}";
                return query;
            }
        }

        public const string UserId_ColumnName = "user_id";
        public const string LastName_ColumnName = "last_name";
        public const string FirstName_ColumnName = "first_name";
        public const string ThirdName_ColumnName = "third_name";
        public const string IsActiveFlag_ColumnName = "is_active";
        public const string FullName_ColumnName = "full_name";
        public const string EmployeeNumber_ColumnName = "employee_number";
        public const string Password_ColumnName = "password";

    }
}
