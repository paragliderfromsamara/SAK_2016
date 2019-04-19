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

        public override bool Save()
        {
            try
            {
                return base.Save();
            }
            catch(DBEntityException ex)
            {
                System.Windows.Forms.MessageBox.Show(ex.Message, "Не удалось добавить нормативный документ...", System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Warning);
                return false;
            }
        }

        protected override void ValidateActions()
        {
            base.ValidateActions();
            CheckShortNameUniquiness();
            CheckShortNameNotBlank();
        }

        protected void CheckShortNameNotBlank()
        {
            if (String.IsNullOrWhiteSpace(ShortName))
            {
                this.ErrorsList.Add("Номер нормативного документа не может быть пустым");
            }

        }

        protected void CheckShortNameUniquiness()
        {
            DBEntityTable t = new DBEntityTable(typeof(Document));
            string select_cmd = $"{t.SelectQuery} WHERE NOT {DocumentId_ColumnName} = {this.DocumentId} AND {ShortName_ColumnName} = '{this.ShortName}'";
            t.FillByQuery(select_cmd);
            if (t.Rows.Count > 0)
            {
                this.ErrorsList.Add("Номер нормативного документа должен быть уникальным");
            }
        }



        public static Document find_by_document_id(uint id)
        {
            DBEntityTable t = new DBEntityTable(typeof(Document));
            string select_cmd = $"{t.SelectQuery} WHERE {DocumentId_ColumnName} = {id}";
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


        #region Колонки таблицы
        [DBColumn(DocumentId_ColumnName, ColumnDomain.UInt, Order = 10, OldDBColumnName = "DocInd", IsPrimaryKey = true, AutoIncrement = true)]
        public uint DocumentId
        {
            get
            {
                return tryParseUInt(DocumentId_ColumnName);
            }set
            {
                this[DocumentId_ColumnName] = value;
            }
        }

        [DBColumn(ShortName_ColumnName, ColumnDomain.Tinytext, Order = 11, OldDBColumnName = "DocNum", Nullable = true)]
        public string ShortName
        {
            get
            {
                return this[ShortName_ColumnName].ToString();
            }
            set
            {
                this[ShortName_ColumnName] = value;
            }
        }

        [DBColumn(FullName_ColumnName, ColumnDomain.Varchar, Size = 1000, Order = 12, OldDBColumnName = "DocName", Nullable = true)]
        public string FullName
        {
            get
            {
                return this[FullName_ColumnName].ToString();
            }
            set
            {
                this[FullName_ColumnName] = value;
            }
        }

        public const string DocumentId_ColumnName = "document_id";
        public const string ShortName_ColumnName = "short_name";
        public const string FullName_ColumnName = "full_name";

        #endregion

    }
}
