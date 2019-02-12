using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NormaMeasure.DBControl.Tables
{
    [DBTable("documents", "db_norma_sac", OldDBName = "bd_cable", OldTableName = "norm_docum")]
    public class Document : BaseEntity
    {
        public Document(DataRowBuilder builder) : base(builder)
        {
        }

        public static Document find_by_document_id(uint id)
        {
            DBEntityTable t = new DBEntityTable(typeof(Document));
            string select_cmd = $"{t.SelectQuery} WHERE document_id = {id}";
            t.FillByQuery(select_cmd);
            if (t.Rows.Count > 0) return (Document)t.Rows[0];
            else
            {
                return null;
            }
        }

        public static Document build()
        {
            DBEntityTable t = new DBEntityTable(typeof(Document));
            return (Document)t.NewRow();
        }

        public static DBEntityTable get_all_as_table()
        {
            DBEntityTable t = new DBEntityTable(typeof(Document));
            string select_cmd = $"{t.SelectQuery}";
            t.FillByQuery(select_cmd);
            return t;
        }

        [DBColumn("document_id", ColumnDomain.UInt, Order = 10, OldDBColumnName = "DocInd", IsPrimaryKey = true)]
        public uint DocumentId
        {
            get
            {
                return tryParseUInt("document_id");
            }set
            {
                this["document_id"] = value;
            }
        }

        [DBColumn("short_name", ColumnDomain.Tinytext, Order = 11, OldDBColumnName = "DocNum", Nullable = true)]
        public string ShortName
        {
            get
            {
                return this["short_name"].ToString();
            }
            set
            {
                this["short_name"] = value;
            }
        }

        [DBColumn("full_name", ColumnDomain.Varchar, Size = 1000, Order = 12, OldDBColumnName = "DocName", Nullable = true)]
        public string FullName
        {
            get
            {
                return this["full_name"].ToString();
            }
            set
            {
                this["full_name"] = value;
            }
        }

    }
}
