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
        public static Cable New()
        {
            DBEntityTable t = new DBEntityTable(typeof(Cable));
            Cable cable = (Cable)t.NewRow();
            return cable;
        }

        public static Cable GetDraft()
        {
            DBEntityTable t = new DBEntityTable(typeof(Cable));
            string query = t.SelectQuery + " WHERE is_draft = 1";
            
        }


        private static Cable findDraft()
        {
            DBEntityTable t = new DBEntityTable(typeof(Cable));
            string query = $"{_cable.DBTable.SelectQuery} WHERE is_draft = 1";
            DataTable dt = _cable.getFromDB(query);
            if (dt.Rows.Count != 0) return new CableOld(dt.Rows[0]);
            else return null;
        }


        public Cable(DataRowBuilder builder) : base(builder)
        {
        }

        [DBColumn("cable_id", ColumnDomain.UInt, Order = 10, OldDBColumnName ="CabNum", Nullable =true, IsPrimaryKey = true)]
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
                return (string)this["name"];
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
                return (string)this["struct_name"];
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
                return (string)this["notes"];
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
                return (string)this["code_okp"];
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
                return (string)this["code_kch"];
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
        public float BuilLength
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

    }
}
