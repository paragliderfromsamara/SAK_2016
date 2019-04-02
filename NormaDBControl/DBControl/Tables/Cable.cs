using System;
using System.Collections.Generic;
using System.Data;
using MySql.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NormaMeasure.DBControl;

namespace NormaMeasure.DBControl.Tables
{
    [DBTable("cables", "db_norma_sac", OldDBName = "bd_cable", OldTableName = "cables")]
    public class Cable : BaseEntity
    {

        public static DBEntityTable get_all_including_docs()
        {
            DBEntityTable docsTable = new DBEntityTable(typeof(Document));
            string select_cmd = $"LEFT OUTER JOIN {docsTable.TableName} USING({docsTable.PrimaryKey[0]}) WHERE is_deleted = 0 AND is_draft = 0";
            return find_by_criteria(select_cmd, typeof(Cable));
        }

        public static Cable find_by_cable_id(uint id)
        {
            DBEntityTable t = find_by_primary_key(id, typeof(Cable));//new DBEntityTable(typeof(Cable));
            if (t.Rows.Count > 0) return (Cable)t.Rows[0];
            else
            {
                return null;
            }
        }
        public static Cable build()
        {
            DBEntityTable t = new DBEntityTable(typeof(Cable));
            Cable cable = (Cable)t.NewRow();
            return cable;
        }

        /// <summary>
        /// Выдаёт таблицу марок кабеля
        /// </summary>
        /// <returns></returns>
        public static DBEntityTable get_cable_marks()
        {
            DBEntityTable t = new DBEntityTable(typeof(Cable), DBEntityTableMode.NoColumns);
            t.TableName = "cable_marks";
            t.Columns.Add("cable_mark");
            string q = $"{t.SelectQuery} WHERE is_draft = 0 AND is_deleted = 0 ORDER BY name ASC";
            string selectString = " DISTINCT name AS cable_mark ";
            q = q.Replace("*", selectString);
            t.FillByQuery(q);
            return t;    
        }

        public static Cable[] get_all_as_array()
        {
            DBEntityTable t = get_all_as_table();
            List<Cable> cables = new List<Cable>();
            if (t.Rows.Count > 0)
            {
                foreach (DataRow r in t.Rows) cables.Add((Cable)r);
            }
            return cables.ToArray();
        }

        public static DBEntityTable get_all_as_table()
        {
            return find_by_criteria("is_draft = 0 AND is_deleted = 0", typeof(Cable));
        }

        /// <summary>
        /// Выдает черновик кабеля
        /// </summary>
        /// <returns></returns>
        public static Cable GetDraft()
        {
            Cable draft = findDraft();
            if (draft == null) draft = createDraft();
            return draft;
        }


        /// <summary>
        /// Ищет черновик кабеля
        /// </summary>
        /// <returns></returns>
        private static Cable findDraft()
        {
            DBEntityTable t = find_by_criteria("is_draft = 1", typeof(Cable));
            if (t.Rows.Count != 0) return (Cable)t.Rows[0];
            else return null;
        }

        /// <summary>
        /// Создаёт черновик кабеля
        /// </summary>
        /// <returns></returns>
        private static Cable createDraft()
        {
            Cable draft = build();
            draft.IsDraft = true;
            if (draft.Create()) return findDraft();
            else return null;
        }

        public override bool Save()
        {
            
            return base.Save();
        }


        public Cable(DataRowBuilder builder) : base(builder)
        {
        }


        [DBColumn("cable_id", ColumnDomain.UInt, Order = 10, OldDBColumnName ="CabNum", Nullable =true, IsPrimaryKey = true, AutoIncrement = true)]
        public uint CableId
        {
            get
            {
                return tryParseUInt("cable_id");
            }
            set
            {
                this["cable_id"] = value;
            }
        }


        [DBColumn("name", ColumnDomain.Tinytext, OldDBColumnName ="name", Order = 11, Nullable = true)]
        public string Name
        {
            get
            {
                return this["name"].ToString();
            }
            set
            {
                this["name"] = value;
            }
        }

        [DBColumn("struct_name", ColumnDomain.Tinytext, OldDBColumnName = "CabNameStruct", Order = 12, DefaultValue = "", Nullable = true)]
        public string StructName
        {
            get
            {
                return  this["struct_name"].ToString();
            }
            set
            {
                this["struct_name"] = value;
            }
        }

        [DBColumn("notes", ColumnDomain.Tinytext, OldDBColumnName = "TextPrim", Order = 13, DefaultValue = "", Nullable = true)]
        public string Notes
        {
            get
            {
                return this["notes"].ToString();
            }
            set
            {
                this["notes"] = value;
            }
        }

        [DBColumn("code_okp", ColumnDomain.Char, OldDBColumnName = "KodOKP", Nullable =true,Size = 12, Order = 14, DefaultValue = "")]
        public string CodeOKP
        {
            get
            {
                return this["code_okp"].ToString();
            }set
            {
                this["code_okp"] = value;
            }
        }

        [DBColumn("code_kch", ColumnDomain.Char, OldDBColumnName = "KodOKP_KCH", Size = 2, Order = 15, DefaultValue = "", Nullable = true)]
        public string CodeKCH
        {
            get
            {
                return this["code_kch"].ToString();
            }
            set
            {
                this["code_kch"] = value;
            }
        }

        [DBColumn("linear_mass", ColumnDomain.Float, OldDBColumnName = "PogMass", Order = 16, DefaultValue = 0, Nullable = true)]
        public float LinearMass
        {
            get
            {
                return tryParseFloat("linear_mass");
            }
            set
            {
                this["linear_mass"] = value;
            }
        }

        [DBColumn("build_length", ColumnDomain.Float, OldDBColumnName = "StrLengt", Order = 17, DefaultValue = 1000, Nullable = true)]
        public float BuildLength
        {
            get
            {
                return tryParseFloat("build_length");
            }
            set
            {
                this["build_length"] = value;
            }
        }

        [DBColumn("document_id", ColumnDomain.UInt, OldDBColumnName = "DocInd", Order = 18, DefaultValue = 0, Nullable = true)]
        public uint DocumentId
        {
            get
            {
                return tryParseUInt("document_id");
            }
            set
            {
                this["document_id"] = value;
            }
        }

        [DBColumn("u_cover", ColumnDomain.UInt, OldDBColumnName = "U_Obol", Order = 19, DefaultValue = 0, Nullable = true)]
        public float UCover
        {
            get
            {
                return tryParseFloat("u_cover");
            }
            set
            {
                this["u_cover"] = value;
            }
        }

        [DBColumn("p_min", ColumnDomain.UInt, OldDBColumnName = "P_min", Order =20, DefaultValue = 2400, Nullable = true)]
        public float PMin
        {
            get
            {
                return tryParseFloat("p_min");
            }
            set
            {
                this["p_min"] = value;
            }
        }

        [DBColumn("p_max", ColumnDomain.UInt, OldDBColumnName = "P_max", Order = 21, DefaultValue =0, Nullable =true)]
        public float PMax
        {
            get
            {
                return tryParseFloat("p_max");
            }
            set
            {
                this["p_max"] = value;
            }
        }

        [DBColumn("is_draft", ColumnDomain.Boolean, Order = 22, DefaultValue = false)]
        public bool IsDraft
        {
            get
            {
                return tryParseBoolean("is_draft", false);
            }
            set
            {
                this["is_draft"] = value;
            }
        }

        [DBColumn("is_deleted", ColumnDomain.Boolean,  Order = 23, DefaultValue =false)]
        public bool IsDeleted
        {
            get
            {
                return tryParseBoolean("is_deleted", false);
            }
            set
            {
                this["is_delete"] = value;
            }
        }

        /// <summary>
        /// Выдает нормативный документ по данному кабелю
        /// </summary>
        public Document QADocument
        {
            get
            {
                if (cableNormDocument == null)
                {
                    cableNormDocument = loadDocument();
                }
                return cableNormDocument;
            }
            set
            {
                cableNormDocument = value;
                this.DocumentId = value.DocumentId;
            }
        }

        /// <summary>
        /// Подгружаем документ по качеству
        /// </summary>
        /// <returns></returns>
        private Document loadDocument()
        {
            if (DocumentId == 0) return null;
            else
            {
                return Document.find_by_document_id(DocumentId);
            }
        }

       // private bool SaveDocumentIfItNeccessary()
       // {
       //     if (this.documentIsValid())
       // }

        private bool documentWasAssigned()
        {
            if (DocumentId == 0 && this.QADocument == null)
            {
                NoDocumentException();
                return false;
            }
            else return true;
            
        }

        private void NoDocumentException()
        {
            throw new DBEntityException("Не был выбран норматив для кабеля");
        }


        protected Document cableNormDocument;

        public DBEntityTable CableStructures
        {
            get
            {
                if (cableStructures == null)
                {
                    cableStructures = CableStructure.get_by_cable(this);
                }
                return cableStructures;
            }
        }
        protected DBEntityTable cableStructures;

        


    }
}
