using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NormaLib.DBControl.Tables
{
    [DBTable("released_barabans", "db_norma_measure", OldDBName = "bd_isp", OldTableName = "barabany")]
    public class ReleasedBaraban : BaseEntity
    {
        public ReleasedBaraban(DataRowBuilder builder) : base(builder)
        {
        }

        public static ReleasedBaraban find_by_id(uint id)
        {
            DBEntityTable t = find_by_primary_key(id, typeof(ReleasedBaraban));
            if (t.Rows.Count > 0)
            {
                return (ReleasedBaraban)t.Rows[0];
            }
            else return null;
        }

        public BarabanType BarabanType
        {
            get
            {
                if (barabanType==null)
                {
                    barabanType = BarabanType.find_by_id(BarabanTypeId);
                }
                return barabanType;
            }
        }


        private BarabanType barabanType;

        #region Колонки БД
        [DBColumn(BarabanId_ColumnName, ColumnDomain.UInt, Order = 10, OldDBColumnName = "BarabanInd", Nullable = true, IsPrimaryKey = true, AutoIncrement = true)]
        public uint BarabanId
        {
            get
            {
                return tryParseUInt(BarabanId_ColumnName);
            }
            set
            {
                this[BarabanId_ColumnName] = value;
            }
        }

        [DBColumn(BarabanType.TypeId_ColumnName, ColumnDomain.UInt, Order = 11, OldDBColumnName = "TipInd", Nullable = false, IsPrimaryKey = false)]
        public uint BarabanTypeId
        {
            get
            {
                return tryParseUInt(BarabanType.TypeId_ColumnName);
            }
            set
            {
                this[BarabanType.TypeId_ColumnName] = value;
            }
        }

        [DBColumn(BarabanSerialNumber_ColumnName, ColumnDomain.Tinytext, Order = 12, OldDBColumnName = "BarabanNum", Nullable = true, IsPrimaryKey = false)]
        public string SerialNumber
        {
            get
            {
                return this[BarabanSerialNumber_ColumnName].ToString();
            }
            set
            {
                this[BarabanSerialNumber_ColumnName] = value;
            }
        }

        
        public const string BarabanId_ColumnName = "baraban_id";
        public const string BarabanSerialNumber_ColumnName = "serial_number";
        #endregion
    }
}
