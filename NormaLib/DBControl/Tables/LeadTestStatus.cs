﻿using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NormaLib.DBControl.Tables
{
    [DBTable("lead_test_statuses", "db_norma_measure", OldDBName = "bd_isp", OldTableName = "status_gil")]
    public class LeadTestStatus : BaseEntity
    {
        public LeadTestStatus(DataRowBuilder builder) : base(builder)
        {
        }

        public static DBEntityTable get_all_as_table()
        {
            return get_all(typeof(LeadTestStatus));
        }

        /// <summary>
        /// Годна
        /// </summary>
        public const uint Ok = 0;
        /// <summary>
        /// Оборвана
        /// </summary>
        public const uint Ragged = 1;
        /// <summary>
        /// Замкнута
        /// </summary>
        public const uint Closured = 2;
        /// <summary>
        /// Пробита
        /// </summary>
        public const uint Broken = 3;

        #region Колонки таблицы
        [DBColumn(StatusId_ColumnName, ColumnDomain.UInt, Order = 10, OldDBColumnName = "StatGil", Nullable = true, IsPrimaryKey = true)]
        public uint StatusId
        {
            get
            {
                return tryParseUInt(StatusId_ColumnName);
            }
            set
            {
                this[StatusId_ColumnName] = value;
            }
        }

        [DBColumn(StatusTitle_ColumnName, ColumnDomain.Tinytext, Order = 11, OldDBColumnName = "StatGilName", Nullable = true)]
        public string StatusTitle
        {
            get
            {
                return this[StatusTitle_ColumnName].ToString();
            }
            set
            {
                this[StatusTitle_ColumnName] = value;
            }
        }

        public static string StatusTitle_Short(uint status_id)
        {
            switch (status_id)
            {
                case Ok:
                    return "Годн.";
                case Ragged:
                    return "Обр.";
                case Closured:
                    return "Зам.";
                case Broken:
                    return "Проб.";
                default:
                    return "-";
            }
        }

        public const string StatusId_ColumnName = "status_id";
        public const string StatusTitle_ColumnName = "status_title";




        #endregion
    }
}
