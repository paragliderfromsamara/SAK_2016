﻿using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NormaMeasure.DBControl.Tables
{
    [DBTable("measured_parameter_types", "db_norma_measure", OldDBName = "bd_cable", OldTableName = "ism_param")]
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

        public const uint Calling = 1;
        public const uint Rleads = 2;
        public const uint dR = 3;
        public const uint Risol1 = 4;
        public const uint Risol2 = 5;
        public const uint Risol3 = 6;
        public const uint Risol4 = 7;
        public const uint Cp = 8;
        public const uint dCp = 9;
        public const uint Co = 10;
        public const uint Ea = 11;
        public const uint K1 = 12;
        public const uint K23 = 13;
        public const uint K9_12 = 14;
        public const uint al = 15;
        public const uint Ao = 16;
        public const uint Az = 17;
        public const uint K2 = 18;
        public const uint K3 = 19;
        public const uint K9 = 20;
        public const uint K10 = 21;
        public const uint K11 = 22;
        public const uint K12 = 23;


        /// <summary>
        /// Выборка для ручных испытаний
        /// </summary>
        /// <returns></returns>
        public static DBEntityTable for_a_manual_test_form()
        {
            return find_by_criteria($"WHERE {ParameterTypeId_ColumnName} < {K2} AND NOT {ParameterTypeId_ColumnName} IN ({Risol2}, {Risol3}, {Risol4}, {Calling}, {dCp}, {dR}) ", typeof(MeasuredParameterType));

        }

        /// <summary>
        /// Выборка типов измеряемых параметров для формы испытания кабеля
        /// </summary>
        /// <returns></returns>
        public static DBEntityTable get_for_a_program_test()
        {
            return find_by_criteria($"WHERE {ParameterTypeId_ColumnName} < {K2} AND NOT {ParameterTypeId_ColumnName} IN ({Risol2}, {Risol3}, {Risol4}) ", typeof(MeasuredParameterType));
        }

        public static DBEntityTable get_all_as_table_for_cable_structure_form(string ids)
        {
            return find_by_criteria($"WHERE {ParameterTypeId_ColumnName} > 1 AND {ParameterTypeId_ColumnName} < {K2} AND {ParameterTypeId_ColumnName} IN ({ids})", typeof(MeasuredParameterType));
        }

        public static MeasuredParameterType[] get_all_by_ids_as_array(uint[] ids)
        {
            List<MeasuredParameterType> types = new List<MeasuredParameterType>();
            DBEntityTable t = get_all_by_ids(ids);
            foreach (MeasuredParameterType type in t.Rows) types.Add(type);
            return types.ToArray();

        }

        public static DBEntityTable get_all_by_ids(uint[] ids)
        {
            string idsStr = String.Empty;
            foreach(uint id in ids)
            {
                if (!String.IsNullOrWhiteSpace(idsStr)) idsStr += ", ";
                idsStr += id.ToString();
            }
            return find_by_criteria($"WHERE {ParameterTypeId_ColumnName} IN ({idsStr})", typeof(MeasuredParameterType));
        }


        public static MeasuredParameterType find_by_id(uint id)
        {
            DBEntityTable t = find_by_primary_key(id, typeof(MeasuredParameterType));
            if (t.Rows.Count > 0) return (MeasuredParameterType)t.Rows[0];
            else return null;
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

        public bool IsIsolationResistance => IsItIsolationaResistance(ParameterTypeId);

        public static bool IsItIsolationaResistance(uint parameterTypeId)
        {
            bool isIt = false;
            isIt |= parameterTypeId == Risol1;
            isIt |= parameterTypeId == Risol2;
            isIt |= parameterTypeId == Risol3;
            isIt |= parameterTypeId == Risol4;
            return isIt;
           
        }

        /// <summary>
        /// Относится ли параметр с указанным id к параметрам Ea, K1, K2, K3, K9-K12
        /// </summary>
        /// <param name="parameter_type_id"></param>
        /// <returns></returns>
        public static bool IsEKParameter(uint parameter_type_id)
        {
            uint[] parameters = new uint[]
            {
                K1, K2, K3, K23, K9, K10, K11, K12, Ea
            };
            return parameters.Contains(parameter_type_id);
        }

        public bool IsEK()
        {
            return IsEKParameter(this.ParameterTypeId);
        }

        public static bool IsHasMinLimit(uint parameter_type_id)
        {
            uint[] notAllowed = new uint[] { Calling, Risol2, Risol4, al, dCp, dR };
            return !notAllowed.Contains(parameter_type_id);
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

        public string RefText => $"parameter_{ParameterTypeId}";



        #region Колонки таблицы
        [DBColumn(ParameterTypeId_ColumnName, ColumnDomain.UInt, Order = 11, OldDBColumnName = "ParamInd", Nullable = false, IsPrimaryKey = true)]
        public uint ParameterTypeId
        {
            get
            {
                return tryParseUInt(ParameterTypeId_ColumnName);
            }
            set
            {
                this[ParameterTypeId_ColumnName] = value;
            }
        }


        [DBColumn(ParameterName_ColumnName, ColumnDomain.Tinytext, Order = 12, OldDBColumnName = "ParamName", Nullable = true)]
        public string ParameterName
        {
            get
            {
                return this[ParameterName_ColumnName].ToString();
            }
            set
            {
                this[ParameterName_ColumnName] = value;
            }
        }

        [DBColumn(ParameterMeasure_ColumnName, ColumnDomain.Tinytext, Order = 13, OldDBColumnName = "Ed_izm", Nullable = true)]
        public string Measure
        {
            get
            {
                return this[ParameterMeasure_ColumnName].ToString();
            }
            set
            {
                this[ParameterMeasure_ColumnName] = value;
            }
        }

        [DBColumn(ParameterDescription_ColumnName, ColumnDomain.Tinytext, Order = 14, OldDBColumnName = "ParamOpis", Nullable = true)]
        public string Description
        {
            get
            {
                return this[ParameterDescription_ColumnName].ToString();
            }
            set
            {
                this[ParameterDescription_ColumnName] = value;
            }
        }


        public const string ParameterTypeId_ColumnName = "parameter_type_id";
        public const string ParameterName_ColumnName = "parameter_name";
        public const string ParameterMeasure_ColumnName = "parameter_measure";
        public const string ParameterDescription_ColumnName = "parameter_description";
        #endregion
    }

    public enum CallingSubModes
    {
        Open = 0,
        Short = 1,
        BPair = 2
    }
}
