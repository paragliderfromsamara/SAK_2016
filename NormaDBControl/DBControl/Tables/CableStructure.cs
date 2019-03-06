using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NormaMeasure.DBControl.Tables
{
    [DBTable("cable_structures", "db_norma_sac", OldDBName = "bd_cable", OldTableName = "struktury_cab")]
    class CableStructure : BaseEntity
    {
        public CableStructure(DataRowBuilder builder) : base(builder)
        {
        }

        [DBColumn("cable_structure_id", ColumnDomain.UInt, Order = 10, OldDBColumnName = "StruktInd", Nullable = true, IsPrimaryKey = true, AutoIncrement = true)]
        public uint CableStructureId
        {
            get
            {
                return tryParseUInt("cable_structure_id");
            }
            set
            {
                this["cable_structure_id"] = value;
            }
        }


        [DBColumn("cable_id", ColumnDomain.UInt, Order = 11, OldDBColumnName = "CabNum", Nullable = false)]
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

        /// <summary>
        /// Реальное количество элементов структуры
        /// </summary>
        [DBColumn("real_amount", ColumnDomain.UInt, Order = 12, OldDBColumnName = "Kolvo", Nullable = false, DefaultValue = 1)]
        public uint RealAmount
        {
            get
            {
                return tryParseUInt("real_amount");
            }
            set
            {
                this["real_amount"] = value;
            }
        }

        /// <summary>
        /// Номинальное количество элементов структуры
        /// </summary>
        [DBColumn("shown_amount", ColumnDomain.UInt, Order = 13, OldDBColumnName = "Kolvo_ind", Nullable = false, DefaultValue = 1)]
        public uint DisplayedAmount
        {
            get
            {
                return tryParseUInt("shown_amount");
            }
            set
            {
                this["shown_amount"] = value;
            }
        }

        /// <summary>
        /// ID типа структуры
        /// </summary>
        [DBColumn("structure_type_id", ColumnDomain.UInt, Order = 14, OldDBColumnName = "PovivTip", Nullable = false)]
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

        /// <summary>
        /// ID типа материала токопроводящей жилы
        /// </summary>
        [DBColumn("lead_material_id", ColumnDomain.UInt, Order = 15, OldDBColumnName = "MaterGil", Nullable = false)]
        public uint LeadMaterialTypeId
        {
            get
            {
                return tryParseUInt("lead_material_id");
            }
            set
            {
                this["lead_material_id"] = value;
            }
        }

        /// <summary>
        /// Диаметр жилы
        /// </summary>
        [DBColumn("lead_diameter", ColumnDomain.Float, Order = 16, OldDBColumnName = "DiamGil", Nullable = false)]
        public float LeadDiameter
        {
            get
            {
                return tryParseFloat("lead_diameter");
            }
            set
            {
                this["lead_diameter"] = value;
            }
        }

        /// <summary>
        /// ID типа материала изоляции жил структуры
        /// </summary>
        [DBColumn("isolation_material_id", ColumnDomain.UInt, Order = 17, OldDBColumnName = "MaterIsol", Nullable = false)]
        public uint IsolationMaterialId
        {
            get
            {
                return tryParseUInt("isolation_material_id");
            }
            set
            {
                this["isolation_material_id"] = value;
            }
        }

        /// <summary>
        /// Волновое сопротивление кабеля
        /// </summary>
        [DBColumn("wave_resistance", ColumnDomain.Float, Order = 18, OldDBColumnName = "Zwave", Nullable = false)]
        public float WaveResistance
        {
            get
            {
                return tryParseFloat("wave_resistance");
            }
            set
            {
                this["wave_resistance"] = value;
            }
        }

        /// <summary>
        /// Количество элементов в пучке
        /// </summary>
        [DBColumn("grouped_amount", ColumnDomain.UInt, Order = 19, OldDBColumnName = "Puchek", Nullable = false, DefaultValue = 0)]
        public uint GroupedAmount
        {
            get
            {
                return tryParseUInt("grouped_amount");
            }
            set
            {
                this["grouped_amount"] = value;
            }
        }

        /// <summary>
        /// Испытательное напряжение прочности оболочик жила-жила
        /// </summary>
        [DBColumn("ll_test_voltage", ColumnDomain.UInt, Order = 20, OldDBColumnName = "U_gil_gil", Nullable = false, DefaultValue = 0)]
        public uint LeadToLeadTestVoltage
        {
            get
            {
                return tryParseUInt("ll_test_voltage");
            }
            set
            {
                this["ll_test_voltage"] = value;
            }
        }

        /// <summary>
        /// Испытательное напряжение прочности оболочик жила-экран
        /// </summary>
        [DBColumn("ls_test_voltage", ColumnDomain.UInt, Order = 21, OldDBColumnName = "U_gil_ekr", Nullable = false, DefaultValue = 0)]
        public uint LeadToShieldTestVoltage
        {
            get
            {
                return tryParseUInt("ls_test_voltage");
            }
            set
            {
                this["ls_test_voltage"] = value;
            }
        }

        /// <summary>
        /// Рабочее затухание группы
        /// </summary>
        [DBColumn("work_capacity_group", ColumnDomain.Boolean, Order = 22, OldDBColumnName = "Cr_grup", Nullable = true, DefaultValue = 0)]
        public bool WorkCapacityGroup
        {
            get
            {
                return tryParseBoolean("work_capacity_group", false);
            }
            set
            {
                this["work_capacity_group"] = value;
            }
        }


    }



}
