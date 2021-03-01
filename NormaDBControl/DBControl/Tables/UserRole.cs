using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NormaMeasure.DBControl.Tables
{
    [DBTable("user_roles", "db_norma_measure", OldDBName = "bd_system", OldTableName = "dolshnosti")]
    public class UserRole : BaseEntity
    {
        public UserRole(DataRowBuilder builder) : base(builder)
        {
        }

        public static UserRole find_by_role_id(uint _roleId)
        {
            DBEntityTable t = find_by_primary_key(_roleId, typeof(UserRole));
            if (t.Rows.Count > 0) return (UserRole)t.Rows[0];
            else return null;
        }

        public static DBEntityTable get_all_as_table()
        {
            return get_all(typeof(UserRole));
        }


        [DBColumn(RoleId_ColumnName, ColumnDomain.UInt, Order = 10, OldDBColumnName = "DolshNum", Nullable = true, IsPrimaryKey = true, AutoIncrement = true)]
        public uint UserRoleId
        {
            get
            {
                return tryParseUInt(RoleId_ColumnName);
            }
            set
            {
                this[RoleId_ColumnName] = value;
            }
        }

        [DBColumn(RoleName_ColumnName, ColumnDomain.Tinytext, Order = 11, OldDBColumnName = "Dolshnost", Nullable = true, IsPrimaryKey = false)]
        public string UserRoleName
        {
            get
            {
                return this[RoleName_ColumnName].ToString();
            }
            set
            {
                this[RoleName_ColumnName] = value;
            }
        }

        /// <summary>
        /// Администратор БД
        /// </summary>
        public const uint DBAdmin = 1;
        /// <summary>
        /// Метролог
        /// </summary>
        public const uint Metrolog = 2;
        /// <summary>
        /// Мастер
        /// </summary>
        public const uint Master = 3;
        /// <summary>
        /// Оператор
        /// </summary>
        public const uint Operator = 4;
        /// <summary>
        /// Опрессовщик
        /// </summary>
        public const uint PleasureTester = 5;
        /// <summary>
        /// Перемотчик
        /// </summary>
        public const uint Rewinder = 6;


        public const string RoleId_ColumnName = "user_role_id";
        public const string RoleName_ColumnName = "user_role_name";
    }

}
