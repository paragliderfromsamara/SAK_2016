using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NormaMeasure.DBControl.Tables
{
    [DBTable("length_bringing_types", "db_norma_sac", OldDBName = "bd_isp", OldTableName = "lpriv_tip")]
    public class LengthBringingType : BaseEntity
    {
        public LengthBringingType(DataRowBuilder builder) : base(builder)
        {
        }

        internal static DataTable get_all_as_table()
        {
            return get_all(typeof(LengthBringingType));
        }

        internal static LengthBringingType find_by_lengt_bringing_type_id(uint id)
        {
            DBEntityTable t = find_by_primary_key(id, typeof(LengthBringingType));
            if (t.Rows.Count > 0) return (LengthBringingType)t.Rows[0];
            else return null;
        }

        /// <summary>
        /// Без приведения
        /// </summary>
        public const uint NoBringing = 0;
        /// <summary>
        /// К строительной длине
        /// </summary>
        public const uint ForBuildLength = 1;
        /// <summary>
        /// К одному километру
        /// </summary>
        public const uint ForOneKilometer = 2;
        /// <summary>
        /// Другая длина в метрах
        /// </summary>
        public const uint ForAnotherLengthInMeters = 3;


        #region Колонки таблицы
        [DBColumn(BringingId_ColumnName, ColumnDomain.UInt, Order = 10, OldDBColumnName = "LprivInd", Nullable = true, IsPrimaryKey = true)]
        public uint TypeId
        {
            get
            {
                return tryParseUInt(BringingId_ColumnName);
            }
            set
            {
                this[BringingId_ColumnName] = value;
            }
        }

        [DBColumn(BringingMeasure_ColumnName, ColumnDomain.Tinytext, Order = 11, OldDBColumnName = "Ed_izm", Nullable = false, IsPrimaryKey = false)]
        public string MeasureTitle
        {
            get
            {
                return this[BringingMeasure_ColumnName].ToString();
            }
            set
            {
                this[BringingMeasure_ColumnName] = value;
            }
        }

        [DBColumn(BringingName_ColumnName, ColumnDomain.Tinytext, Order = 12, OldDBColumnName = "LprivName", Nullable = false, IsPrimaryKey = false)]
        public string BringingName
        {
            get
            {
                return this[BringingName_ColumnName].ToString();
            }
            set
            {
                this[BringingName_ColumnName] = value;
            }
        }


        public const string BringingId_ColumnName = "length_bringing_type_id";
        public const string BringingMeasure_ColumnName = "measure_title";
        public const string BringingName_ColumnName = "length_bringing_name";
        #endregion


    }
}
