using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NormaLib.DBControl.Tables
{
    [DBTable("cable_structure_types", "db_norma_measure", OldDBName = "bd_cable", OldTableName = "tipy_poviv")]
    public class CableStructureType : BaseEntity
    {
        public CableStructureType(DataRowBuilder builder) : base(builder)
        {
        }

        public static CableStructureType get_by_id(uint structure_type_id)
        {
            DBEntityTable t = find_by_primary_key(structure_type_id, typeof(CableStructureType));// new DBEntityTable(typeof(CableStructureType));
            if (t.Rows.Count > 0) return (CableStructureType)t.Rows[0];
            else return null;
        }

        public static DBEntityTable get_all_as_table()
        {
            return get_all(typeof(CableStructureType));
        }

        #region Колонки таблицы
        [DBColumn(TypeId_ColumnName, ColumnDomain.UInt, Order = 10, OldDBColumnName = "StruktNum", Nullable = true, IsPrimaryKey = true)]
        public uint StructureTypeId
        {
            get
            {
                return tryParseUInt(TypeId_ColumnName);
            }
            set
            {
                this[TypeId_ColumnName] = value;
            }
        }

        /// <summary>
        /// Название типа структуры: Жила, Пара, Тройка, Четвёрка
        /// </summary>
        [DBColumn(TypeName_ColumnName, ColumnDomain.Varchar, Size = 120, Order = 11, OldDBColumnName = "StruktTip", Nullable = true)]
        public string StructureTypeName
        {
            get
            {
                return this[TypeName_ColumnName].ToString();
            }
            set
            {
                this[TypeName_ColumnName] = value;
            }
        }

        /// <summary>
        /// Количество жил структуры
        /// </summary>
        [DBColumn(LeadAmount_ColumnName, ColumnDomain.Int, Size =3, Order = 12, OldDBColumnName = "ColvoGil", Nullable = true)]
        public int StructureLeadsAmount
        {
            get
            {
                return tryParseInt(LeadAmount_ColumnName);
            }
            set
            {
                this[LeadAmount_ColumnName] = value;
            }
        }

        /// <summary>
        /// Список id измеряемых параметров доступных для данного типа структуры, в виде строки
        /// </summary>
        [DBColumn(MeasuredParametersList_ColumnName, ColumnDomain.Set, SetTypeValue = "'1', '2', '3', '4', '5', '6', '7', '8', '9', '10', '11', '12', '13', '14', '15', '16', '17'", Order = 13, OldDBColumnName = "Set_ParamInd", DefaultValue = "1", Nullable = true)]
        public string StructureMeasuredParameters
        {
            get
            {

                return this[MeasuredParametersList_ColumnName].ToString();
            }
            set
            {
                this[MeasuredParametersList_ColumnName] = value;
            }
        }

        public const string TypeId_ColumnName = "structure_type_id";
        public const string TypeName_ColumnName = "structure_type_name";
        public const string LeadAmount_ColumnName = "lead_amount";
        public const string MeasuredParametersList_ColumnName = "structure_measured_parameters";
        #endregion

        public DBEntityTable MeasuredParameterTypes
        {
            get
            {
                if (measuredParameterTypes == null)
                {
                    measuredParameterTypes = MeasuredParameterType.get_all_as_table_for_cable_structure_form(StructureMeasuredParameters);
                }
                return measuredParameterTypes;
            }
        }

        private DBEntityTable measuredParameterTypes;


        public const uint Lead = 1;
        public const uint Pair = 2;
        public const uint Triplet = 3;
        public const uint Quattro = 4;
        public const uint HightFreqQuattro = 5; 
    }
}
