using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NormaMeasure.DBControl.Tables
{
    [DBTable("cable_test_statuses", "db_norma_sac", OldDBName = "bd_isp", OldTableName = "status_isp")]
    public class CableTestStatus : BaseEntity
    {
        public CableTestStatus(DataRowBuilder builder) : base(builder)
        {
        }


        [DBColumn(StatusId_ColumnName, ColumnDomain.UInt, Order = 10, OldDBColumnName = "Status", Nullable = true, IsPrimaryKey = true, AutoIncrement = true)]
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

        [DBColumn(StatusText_ColumnName, ColumnDomain.Tinytext, Order = 11, OldDBColumnName = "StatusName")]
        public string Description
        {
            get
            {
                return this[StatusText_ColumnName].ToString();
            }
            set
            {
                this[StatusText_ColumnName] = value;
            }
        }


        public const string StatusId_ColumnName = "cable_test_status_id";
        public const string StatusText_ColumnName = "description";


        public const uint NotStarted = 1;
        public const uint Started = 2;
        public const uint Finished = 3;
        public const uint StopedOutOfNorma = 4;
        public const uint StopedByOperator = 5;
    }
}
