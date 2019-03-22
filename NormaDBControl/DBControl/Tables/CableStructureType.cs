using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NormaMeasure.DBControl.Tables
{
    [DBTable("cable_structure_types", "db_norma_sac", OldDBName = "bd_cable", OldTableName = "tipy_poviv")]
    public class CableStructureType : BaseEntity
    {
        public CableStructureType(DataRowBuilder builder) : base(builder)
        {
        }

        public static CableStructureType get_by_id(uint structure_type_id)
        {
            DBEntityTable t = new DBEntityTable(typeof(CableStructureType));
            string select_cmd = $"{t.SelectQuery} WHERE structure_type_id = {structure_type_id}";
            t.FillByQuery(select_cmd);
            if (t.Rows.Count > 0) return (CableStructureType)t.Rows[0];
            else return null;
        }

        public static DBEntityTable get_all_as_table()
        {
            DBEntityTable t = new DBEntityTable(typeof(CableStructureType));
            string select_cmd = $"{t.SelectQuery}";
            t.FillByQuery(select_cmd);
            return t;
        }

        [DBColumn("structure_type_id", ColumnDomain.UInt, Order = 10, OldDBColumnName = "StruktNum", Nullable = true, IsPrimaryKey = true)]
        public uint StructureTypeId
        {
            get
            {
                return tryParseUInt("structure_type_id");
            }
            set
            {
                this["structure_type_id"] = value;
            }
        }

        [DBColumn("structure_type_name", ColumnDomain.Varchar, Size = 120, Order = 11, OldDBColumnName = "StruktTip", Nullable = true)]
        public string StructureTypeName
        {
            get
            {
                return this["structure_type_name"].ToString();
            }
            set
            {
                this["structure_type_name"] = value;
            }
        }

        [DBColumn("lead_amount", ColumnDomain.Int, Size =3, Order = 12, OldDBColumnName = "ColvoGil", Nullable = true)]
        public int StructureLeadsAmount
        {
            get
            {
                return tryParseInt("lead_amount");
            }
            set
            {
                this["lead_amount"] = value;
            }
        }

        [DBColumn("structure_measured_parameters", ColumnDomain.Set, SetTypeValue = "'1', '2', '3', '4', '5', '6', '7', '8', '9', '10', '11', '12', '13', '14', '15', '16', '17'", Order = 13, OldDBColumnName = "Set_ParamInd", DefaultValue = "1", Nullable = true)]
        public string StructureMeasuredParameters
        {
            get
            {

                return this["structure_measured_parameters"].ToString();
            }
            set
            {
                this["structure_measured_parameters"] = value;
            }
        }

        public List<uint> StructureMeasuredParametersIdsAsList
        {
            get
            {
                if (measuredParamsIds == null)
                {
                    string strParams = StructureMeasuredParameters;
                    measuredParamsIds = new List<uint>();
                    if (!String.IsNullOrWhiteSpace(strParams))
                    {
                        string[] strArr = strParams.Split(',');
                        for (int i = 0; i < strArr.Length; i++)
                        {
                            uint v = 0;
                            uint.TryParse(strArr[i], out v);
                            measuredParamsIds.Add(v);
                        }
                    }
                }
                return measuredParamsIds;

            }
        }

        private List<uint> measuredParamsIds;
    }
}
