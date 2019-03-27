using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NormaMeasure.DBControl.Tables
{
    [DBTable("measured_parameter_types", "db_norma_sac", OldDBName = "bd_cable", OldTableName = "ism_param")]
    public class MeasuredParameterType : BaseEntity
    {
        public MeasuredParameterType(DataRowBuilder builder) : base(builder)
        {
        }

        public static MeasuredParameterType find_by_parameter_type_id(uint parameter_type_id)
        {
            DBEntityTable t = find_by_primary_key(parameter_type_id, typeof(MeasuredParameterType));
            MeasuredParameterType parameter_type = null;
            if (t.Rows.Count > 0) parameter_type = (MeasuredParameterType)t.Rows[0];
            return parameter_type;
        }


        public static DBEntityTable get_all_as_table_for_cable_structure_form(string ids)
        {
            return find_by_criteria($"WHERE parameter_type_id > 1 AND parameter_type_id < 18 AND parameter_type_id IN ({ids})", typeof(MeasuredParameterType));
        }

        [DBColumn("parameter_type_id", ColumnDomain.UInt, Order = 11, OldDBColumnName = "ParamInd", Nullable = false, IsPrimaryKey = true)]
        public uint ParameterTypeId
        {
            get
            {
                return tryParseUInt("parameter_type_id");
            }
            set
            {
                this["parameter_type_id"] = value;
            }
        }


        [DBColumn("parameter_name", ColumnDomain.Tinytext, Order = 12, OldDBColumnName = "ParamName", Nullable = true)]
        public string ParameterName
        {
            get
            {
                return this["parameter_name"].ToString();
            }
            set
            {
                this["parameter_name"] = value;
            }
        }

        [DBColumn("parameter_measure", ColumnDomain.Tinytext, Order = 13, OldDBColumnName = "Ed_izm", Nullable = true)]
        public string Measure
        {
            get
            {
                return this["parameter_measure"].ToString();
            }
            set
            {
                this["parameter_measure"] = value;
            }
        }

        [DBColumn("parameter_description", ColumnDomain.Tinytext, Order = 14, OldDBColumnName = "ParamOpis", Nullable = true)]
        public string Description
        {
            get
            {
                return this["parameter_description"].ToString();
            }
            set
            {
                this["parameter_description"] = value;
            }
        }

        /// <summary>
        /// Возвращает список id типов частотных парметров
        /// </summary>
        public static uint[] FrequencyParameterIds
        {
            get
            {
                return new uint[] { al, Ao, Az };
            }
        }

        public static bool IsItFreqParameter(uint parameter_type_id)
        {
            return (parameter_type_id == al || parameter_type_id == Ao || parameter_type_id == Az);
        }

        public static bool IsHasMaxLimit(uint parameter_type_id)
        {
            uint[] notAllowed = new uint[] {Calling, Ao, Az};
            foreach(uint v in notAllowed)
            {
                if (v == parameter_type_id) return false;
            }
            return true;
        }

        /// <summary>
        /// Есть ли максимальное ограничение
        /// </summary>
        public bool HasMaxLimit => IsHasMaxLimit(ParameterTypeId);

        /// <summary>
        /// Есть ли минимальное ограничение
        /// </summary>
        public bool HasMinLimit => IsHasMinLimit(ParameterTypeId);

        /// <summary>
        /// Является ли текущий параметр частотным
        /// </summary>
        public bool IsFreqParameter => IsItFreqParameter(ParameterTypeId);


        public static bool IsHasMinLimit(uint parameter_type_id)
        {
            uint[] notAllowed = new uint[] { Calling, Risol2, Risol4, al, dCp, dR };
            foreach (uint v in notAllowed)
            {
                if (v == parameter_type_id) return false;
            }
            return true;
        }

        public static bool AllowBringingLength(uint parameter_type_id)
        {
            bool f = false;
            f |= parameter_type_id == Rleads;
            f |= parameter_type_id == Risol1;
            f |= parameter_type_id == Risol3;
            f |= parameter_type_id == Cp;
            f |= parameter_type_id == Az;
            f |= parameter_type_id == Ao;
            f |= parameter_type_id == al;
            return f;
        }

        public static bool AllowBringingLength(MeasuredParameterType parameter_type)
        {
            return AllowBringingLength(parameter_type.ParameterTypeId);
        }


        public static uint Calling => 1;
        public static uint Rleads => 2;
        public static uint dR => 3;
        public static uint Risol1 => 4;
        public static uint Risol2 => 5;
        public static uint Risol3 => 6;
        public static uint Risol4 => 7;
        public static uint Cp => 8;
        public static uint dCp => 9;
        public static uint Co => 10;
        public static uint Ea => 11;
        public static uint K1 => 12;
        public static uint K23 => 13;
        public static uint K9_12 => 14;
        public static uint al => 15;
        public static uint Ao => 16;
        public static uint Az => 17;
        public static uint K2 => 18;
        public static uint K3 => 19;
        public static uint K9 => 20;
        public static uint K10 => 21;
        public static uint K11 => 22;
        public static uint K12 => 23;

    }


}
