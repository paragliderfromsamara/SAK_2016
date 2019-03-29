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

        public static CableStructure find_by_structure_id(uint structure_id)
        {
            DBEntityTable t = find_by_primary_key(structure_id, typeof(CableStructure));
            CableStructure structure = null;
            if (t.Rows.Count > 0) structure = (CableStructure)t.Rows[0];
            return structure;
        }


        public override bool Destroy()
        {
            bool delFlag = true;
            if (!IsNewRecord())
            {
                delFlag = base.Destroy();
            }
            if (delFlag == true)
            {
                this.Delete();
            }
            return delFlag;
        }
        public override bool Save()
        {
            try
            {
                return base.Save();
            }catch(DBEntityException ex)
            {
                System.Windows.Forms.MessageBox.Show(ex.Message, "Не удалось сохранить структуру", System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Warning);
                return false;
            }

        }

        protected override void ValidateActions()
        {
            CheckLeadDiameter();
            CheckElementsNumber();
        }

        private void CheckElementsNumber()
        {
            if (RealAmount <= 0)
            {
                ErrorsList.Add("Реальное количество элементов структуры должно быть больше 0");
            }
            else if (RealAmount < DisplayedAmount) ErrorsList.Add("Реальное количество элементов структуры должно быть не меньше номинального");
        }

        private void CheckLeadDiameter()
        {
            if (LeadDiameter <= 0) ErrorsList.Add("Диаметр жилы должен быть больше 0");
        }

        public static CableStructure build_for_cable(uint cableId)
        {
            DBEntityTable t = new DBEntityTable(typeof(CableStructure));
            CableStructure s = (CableStructure)t.NewRow();
            s.CableId = cableId;
            s.LeadDiameter = 0.1f;
            return s;
        }

        public static DBEntityTable get_by_cable_id(uint cable_id)
        {
            return find_by_criteria($"cable_id = {cable_id}", typeof(CableStructure));
        }

        public static uint get_last_structure_id()
        {
            CableStructure s = get_last_cable_structure();
            if (s == null) return 0;
            else return s.CableStructureId; 
        }

        public static CableStructure get_last_cable_structure()
        {
            DBEntityTable t = new DBEntityTable(typeof(CableStructure));
            string select_cmd = $"{t.SelectQuery} ORDER BY cable_structure_id DESC LIMIT 1;";
            t.FillByQuery(select_cmd);
            if (t.Rows.Count > 0) return (CableStructure)t.Rows[0];
            else return null;
        }


        [DBColumn("cable_structure_id", ColumnDomain.UInt, Order = 10, OldDBColumnName = "StruktInd", IsPrimaryKey = true, Nullable = true, AutoIncrement = true)]
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


        [DBColumn("cable_id", ColumnDomain.UInt, Order = 11, OldDBColumnName = "CabNum", Nullable = false, ReferenceTo = "cables(cable_id) ON DELETE CASCADE")]
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
        /// Рабочая ёмкость группы
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

        [DBColumn("dr_bringing_formula_id", ColumnDomain.UInt, Order = 23, OldDBColumnName = "Delta_R", Nullable = true, DefaultValue = 1)]
        public uint DRBringingFormulaId
        {
            get
            {
                return tryParseUInt("dr_bringing_formula_id");
            }
            set
            {
                this["dr_bringing_formula_id"] = value;
            }
        }

        [DBColumn("dr_formula_id", ColumnDomain.UInt, Order = 24, OldDBColumnName = "DRPrivInd", Nullable = true, DefaultValue = 1)]
        public uint DRFormulaId
        {
            get
            {
                return tryParseUInt("dr_formula_id");
            }
            set
            {
                this["dr_formula_id"] = value;
            }
        }


        public string StructureTitle
        {
            get
            {
                return $"{StructureType.StructureLeadsAmount}x{DisplayedAmount}x{LeadDiameter}";
            }
        }
        public CableStructureType StructureType
        {
            get
            {
                if (structureType == null)
                {
                    this.structureType = CableStructureType.get_by_id(this.StructureTypeId);
                }
                return this.structureType;
            }
            set
            {
                this.structureType = value;
                this.StructureTypeId = this.structureType.StructureTypeId;
            }
        }

        public DBEntityTable MeasuredParameters
        {
            get
            {
                if (measuredParameters == null)
                {
                    if (IsNewRecord())
                    {
                        measuredParameters = CableStructureMeasuredParameterData.get_structure_measured_parameters(0);
                    }
                    else
                    {
                        measuredParameters = CableStructureMeasuredParameterData.get_structure_measured_parameters(this.CableStructureId);
                    }
                }
                return measuredParameters;
            }
        }

        public bool HasFreqParameters
        {
            get
            {
               return StructureType.MeasuredParameterTypes.Select($"parameter_type_id IN ({MeasuredParameterType.al}, {MeasuredParameterType.Ao}, {MeasuredParameterType.Az})").Count() > 0;
            }
        }

        public Cable OwnCable
        {
            get
            {
                if (ownCable == null)
                {
                    Cable c = Cable.find_by_cable_id(CableId);
                    if (c != null) OwnCable = c;
                }
                return ownCable;
            }
            set
            {
                ownCable = value;
                CableId = ownCable.CableId;
            }
        }

        /// <summary>
        /// Может ли тип параметра измеряться на данном типе структуры
        /// </summary>
        /// <param name="parameter_type_id"></param>
        /// <returns></returns>
        public bool IsAllowParameterType(uint parameter_type_id)
        {
            return StructureType.MeasuredParameterTypes.Select($"parameter_type_id = {parameter_type_id}").Length > 0 ;
        }

        private CableStructureType structureType;
        private DBEntityTable measuredParameters;
        private Cable ownCable;

    }



}
