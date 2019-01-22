using System;
using System.Collections.Generic;
using System.Data;
using MySql.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NormaMeasure.DBControl;

namespace NormaMeasure.DBControl.SAC
{
    [DBTable("cables", "db_norma_sac", OldDBName = "bd_cable", OldTableName = "cable")]
    public class CableRow : BaseRow
    {
        public CableRow(DataRowBuilder builder) : base(builder)
        {
        }

        [DBColumn("cable_id", ColumnDomain.UInt, Order = 10, OldDBColumnName ="CabNum", IsPrimaryKey = true)]
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


        [DBColumn("name", ColumnDomain.String, OldDBColumnName ="name", Order = 11)]
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

        [DBColumn("struct_name", ColumnDomain.String, OldDBColumnName = "CabNameStruct", Order = 12)]
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

        [DBColumn("notes", ColumnDomain.String, OldDBColumnName = "TextPrim", Order = 13)]

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

        [DBColumn("code_okp", ColumnDomain.String, OldDBColumnName = "KodOKP", Size = 12, Order = 14)]
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

        [DBColumn("code_kch", ColumnDomain.String, OldDBColumnName = "KodOKP_KCH", Size = 2, Order = 15)]

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

        [DBColumn("linear_mass", ColumnDomain.Float, OldDBColumnName = "PogMass", Order = 16)]
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

        [DBColumn("build_length", ColumnDomain.Float, OldDBColumnName = "StrLengt", Order = 17)]
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

        [DBColumn("document_id", ColumnDomain.UInt, OldDBColumnName = "DocInd", Order = 18)]
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

        [DBColumn("u_cover", ColumnDomain.UInt, OldDBColumnName = "U_Obol", Order = 19)]
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

        [DBColumn("p_min", ColumnDomain.UInt, OldDBColumnName = "P_min", Order =20)]
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

        [DBColumn("p_max", ColumnDomain.UInt, OldDBColumnName = "P_max", Order = 21)]
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
