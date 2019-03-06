using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NormaMeasure.DBControl.Tables
{
    [DBTable("lead_test_statuses", "db_norma_sac", OldDBName = "bd_isp", OldTableName = "status_gil")]
    public class LeadTestStatus : BaseEntity
    {
        public LeadTestStatus(DataRowBuilder builder) : base(builder)
        {
        }

        [DBColumn("status_id", ColumnDomain.UInt, Order = 10, OldDBColumnName = "StatGil", Nullable = true, IsPrimaryKey = true)]
        public uint StatusId
        {
            get
            {
                return tryParseUInt("status_id");
            }
            set
            {
                this["status_id"] = value;
            }
        }

        [DBColumn("status_title", ColumnDomain.Tinytext, Order = 11, OldDBColumnName = "StatGilName", Nullable = true)]
        public string StatusTitle
        {
            get
            {
                return this["status_title"].ToString();
            }
            set
            {
                this["status_title"] = value;
            }
        }
    }
}
