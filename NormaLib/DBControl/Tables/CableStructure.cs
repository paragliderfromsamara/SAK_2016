using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Diagnostics;

namespace NormaLib.DBControl.Tables
{
    [DBTable("cable_structures", "db_norma_measure", OldDBName = "bd_cable", OldTableName = "struktury_cab")]
    public class CableStructure : BaseEntity
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

        public static DBEntityTable get_LeadDiameterValues()
        {
            DBEntityTable t = new DBEntityTable(typeof(CableStructure), DBEntityTableMode.NoColumns);
            t.TableName = "lead_diameter_values";
            t.Columns.Add("value");
            string q = $"{t.SelectQuery} ORDER BY {LeadDiameter_ColumnName} ASC";
            string selectString = $" DISTINCT {LeadDiameter_ColumnName} AS value ";
            q = q.Replace("*", selectString);
            t.FillByQuery(q);
            return t;
        }

        public static DBEntityTable get_WaveResistanceValues()
        {
            DBEntityTable t = new DBEntityTable(typeof(CableStructure), DBEntityTableMode.NoColumns);
            t.TableName = "wave_resistance_values";
            t.Columns.Add("value");
            string q = $"{t.SelectQuery} ORDER BY {WaveResistance_ColumnName} ASC";
            string selectString = $" DISTINCT {WaveResistance_ColumnName} AS value ";
            q = q.Replace("*", selectString);
            t.FillByQuery(q);
            return t;
        }

        public void CopyFromStructure(CableStructure structure)
        {
            this.FillColsFromEntity(structure);
            this.AddMeasuredParameterDataFromStructure(structure);
        }

        private void AddMeasuredParameterDataFromStructure(CableStructure structure)
        {
            this.MeasuredParameters.Clear();
            foreach(CableStructureMeasuredParameterData data in structure.MeasuredParameters.Rows)
            {
                CableStructureMeasuredParameterData dta = (CableStructureMeasuredParameterData)MeasuredParameters.NewRow();
                dta.FillColsFromEntity(data);
                dta.CableStructureId = this.CableStructureId;
                
                this.MeasuredParameters.Rows.Add(dta);
            }

        }

        public override bool Destroy()
        {
            bool delFlag = true;
            if (!IsNewRecord())
            {
                delFlag = base.Destroy();
                if (delFlag) DeleteAllMeasuredParametersData(); //Удаляем неиспользуемые измеряемые параметры
                // System.Windows.Forms.MessageBox.Show(this.CableStructureId.ToString() + " ");

            }
            if (delFlag)
            {
                this.Delete();
            }
            return delFlag;
        }

        private void DeleteAllMeasuredParametersData()
        {
            this.MeasuredParameters.Rows.Clear();
            CableStructureMeasuredParameterData.DeleteUnusedFromStructure(this);
        }

        public override bool Save()
        {
            try
            {
                if (base.Save())
                {
                    return SaveMeasuredParameters();
                }
                else return false;
            }catch(DBEntityException ex)
            {
                System.Windows.Forms.MessageBox.Show(ex.Message, "Не удалось сохранить структуру", System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Warning);
                return false;
            }

        }

        protected bool SaveMeasuredParameters()
        {
            bool flag = true;
            foreach(CableStructureMeasuredParameterData cdmpd in MeasuredParameters.Rows)
            {
                
                if (cdmpd.RowState == DataRowState.Deleted) continue;
                Debug.WriteLine("");
                cdmpd.CableStructureId = this.CableStructureId;
                flag &= cdmpd.Save();
            }
            CableStructureMeasuredParameterData.DeleteUnusedFromStructure(this);
            return flag;
        }

        protected override void ValidateActions()
        {
            CheckLeadDiameter();
            CheckElementsNumber();
            CheckMeasuredParameters();
        }

        private void CheckMeasuredParameters()
        {
            foreach(CableStructureMeasuredParameterData mpd in MeasuredParameters.Rows)
            {
                if(!mpd.Validate())
                {
                    foreach(string e in mpd.ErrorsAsArray)
                    {
                        ErrorsList.Add($"{mpd.ParameterName}: {e};");
                    }
                    
                }
            }

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
            s.CableStructureId = 666;
            s.CableId = cableId;
            s.LeadDiameter = 0.4f;
            s.LeadMaterialTypeId = 1;
            s.IsolationMaterialId = 1;
            s.DRBringingFormulaId = 1;
            s.DRFormulaId = 1;
            s.LeadDiameter = 0.1f;
            s.WaveResistance = 0;
            s.WorkCapacityGroup = false;
            s.LeadToLeadTestVoltage = 0;
            s.LeadToShieldTestVoltage = 0;
            s.GroupedAmount = 0;
            s.DisplayedAmount = 1;
            s.RealAmount = 1;
            
            return s;
        }

        public static DBEntityTable get_by_cable(Cable cable)
        {
            DBEntityTable cabStructures = find_by_criteria($"{Cable.CableId_ColumnName} = {cable.CableId}", typeof(CableStructure));
            foreach (CableStructure cs in cabStructures.Rows) cs.OwnCable = cable;
            return cabStructures;
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
            string select_cmd = $"{t.SelectQuery} ORDER BY {StructureId_ColumnName} DESC LIMIT 1;";
            t.FillByQuery(select_cmd);
            if (t.Rows.Count > 0) return (CableStructure)t.Rows[0];
            else return null;
        }

        public CableStructureMeasuredParameterData RizolNormaValue
        {
            get
            {
                CableStructureMeasuredParameterData[] rows = (CableStructureMeasuredParameterData[])MeasuredParameters.Select($"{MeasuredParameterType.ParameterTypeId_ColumnName} = {MeasuredParameterType.Risol1}");
                if (rows.Length > 0) return rows[0];
                else return null;
            }
        }
        public CableStructureMeasuredParameterData RizolTimeLimit
        {
            get
            {
                CableStructureMeasuredParameterData[] rows = (CableStructureMeasuredParameterData[])MeasuredParameters.Select($"{MeasuredParameterType.ParameterTypeId_ColumnName} = {MeasuredParameterType.Risol2}");
                if (rows.Length > 0) return rows[0];
                else return null;
            }
        }


        #region Колонки таблицы
        [DBColumn(StructureId_ColumnName, ColumnDomain.UInt, Order = 10, OldDBColumnName = "StruktInd", IsPrimaryKey = true, Nullable = true, AutoIncrement = true)]
        public uint CableStructureId
        {
            get
            {
                return tryParseUInt(StructureId_ColumnName);
            }
            set
            {
                if (CableStructureId != value) structureType = null;
                this[StructureId_ColumnName] = value;

            }
        }


        [DBColumn(Cable.CableId_ColumnName, ColumnDomain.UInt, Order = 11, OldDBColumnName = "CabNum", Nullable = false, ReferenceTo = "cables(cable_id) ON DELETE CASCADE")]
        public uint CableId
        {
            get
            {
                return tryParseUInt(Cable.CableId_ColumnName);
            }
            set
            {
                this[Cable.CableId_ColumnName] = value;
            }
        }

        /// <summary>
        /// Реальное количество элементов структуры
        /// </summary>
        [DBColumn(RealAmount_ColumnName, ColumnDomain.UInt, Order = 12, OldDBColumnName = "Kolvo", Nullable = false, DefaultValue = 1)]
        public uint RealAmount
        {
            get
            {
                return tryParseUInt(RealAmount_ColumnName);
            }
            set
            {
                this[RealAmount_ColumnName] = value;
            }
        }

        /// <summary>
        /// Номинальное количество элементов структуры
        /// </summary>
        [DBColumn(ShownAmount_ColumnName, ColumnDomain.UInt, Order = 13, OldDBColumnName = "Kolvo_ind", Nullable = false, DefaultValue = 1)]
        public uint DisplayedAmount
        {
            get
            {
                return tryParseUInt(ShownAmount_ColumnName);
            }
            set
            {
                this[ShownAmount_ColumnName] = value;
            }
        }

        /// <summary>
        /// ID типа структуры
        /// </summary>
        [DBColumn(CableStructureType.TypeId_ColumnName, ColumnDomain.UInt, Order = 14, OldDBColumnName = "PovivTip", Nullable = false)]
        public uint StructureTypeId
        {
            get
            {
                return tryParseUInt(CableStructureType.TypeId_ColumnName);
            }
            set
            {
                this[CableStructureType.TypeId_ColumnName] = value;
            }
        }

        /// <summary>
        /// ID типа материала токопроводящей жилы
        /// </summary>
        [DBColumn(LeadMaterial.MaterialId_ColumnName, ColumnDomain.UInt, Order = 15, OldDBColumnName = "MaterGil", Nullable = false)]
        public uint LeadMaterialTypeId
        {
            get
            {
                return tryParseUInt(LeadMaterial.MaterialId_ColumnName);
            }
            set
            {
                this[LeadMaterial.MaterialId_ColumnName] = value;
            }
        }

        /// <summary>
        /// Диаметр жилы
        /// </summary>
        [DBColumn(LeadDiameter_ColumnName, ColumnDomain.Float, Order = 16, OldDBColumnName = "DiamGil", Nullable = false)]
        public float LeadDiameter
        {
            get
            {
                return tryParseFloat(LeadDiameter_ColumnName);
            }
            set
            {
                this[LeadDiameter_ColumnName] = value;
            }
        }

        /// <summary>
        /// ID типа материала изоляции жил структуры
        /// </summary>
        [DBColumn(IsolationMaterial.MaterialId_ColumnName, ColumnDomain.UInt, Order = 17, OldDBColumnName = "MaterIsol", Nullable = false)]
        public uint IsolationMaterialId
        {
            get
            {
                return tryParseUInt(IsolationMaterial.MaterialId_ColumnName);
            }
            set
            {
                this[IsolationMaterial.MaterialId_ColumnName] = value;
            }
        }

        /// <summary>
        /// Измерительное напряжение Rизол для данной структуры
        /// </summary>
        [DBColumn(IsolationResistanceVoltage_ColumnName, ColumnDomain.Int, Order = 18, OldDBColumnName = "", Nullable = true, DefaultValue = 100)]
        public int IsolationResistanceVoltage
        {
            get
            {
                return tryParseInt(IsolationResistanceVoltage_ColumnName);
            }
            set
            {
                this[IsolationResistanceVoltage_ColumnName] = value;
            }
        }

        /// <summary>
        /// Волновое сопротивление кабеля
        /// </summary>
        [DBColumn(WaveResistance_ColumnName, ColumnDomain.Float, Order = 19, OldDBColumnName = "Zwave", Nullable = false)]
        public float WaveResistance
        {
            get
            {
                return tryParseFloat(WaveResistance_ColumnName);
            }
            set
            {
                this[WaveResistance_ColumnName] = value;
            }
        }

        /// <summary>
        /// Количество элементов в пучке
        /// </summary>
        [DBColumn(GroupedAmount_ColumnName, ColumnDomain.UInt, Order = 20, OldDBColumnName = "Puchek", Nullable = false, DefaultValue = 0)]
        public uint GroupedAmount
        {
            get
            {
                return tryParseUInt(GroupedAmount_ColumnName);
            }
            set
            {
                this[GroupedAmount_ColumnName] = value;
            }
        }

        /// <summary>
        /// Испытательное напряжение прочности оболочик жила-жила
        /// </summary>
        [DBColumn(LeadLeadVoltage_ColumnName, ColumnDomain.UInt, Order = 21, OldDBColumnName = "U_gil_gil", Nullable = false, DefaultValue = 0)]
        public uint LeadToLeadTestVoltage
        {
            get
            {
                return tryParseUInt(LeadLeadVoltage_ColumnName);
            }
            set
            {
                this[LeadLeadVoltage_ColumnName] = value;
            }
        }

        /// <summary>
        /// Испытательное напряжение прочности оболочик жила-экран
        /// </summary>
        [DBColumn(LeadShieldVoltage_ColumnName, ColumnDomain.UInt, Order = 22, OldDBColumnName = "U_gil_ekr", Nullable = false, DefaultValue = 0)]
        public uint LeadToShieldTestVoltage
        {
            get
            {
                return tryParseUInt(LeadShieldVoltage_ColumnName);
            }
            set
            {
                this[LeadShieldVoltage_ColumnName] = value;
            }
        }

        /// <summary>
        /// Рабочая ёмкость группы
        /// </summary>
        [DBColumn(WorkCapGroup_ColumnName, ColumnDomain.Boolean, Order = 23, OldDBColumnName = "Cr_grup", Nullable = true, DefaultValue = 0)]
        public bool WorkCapacityGroup
        {
            get
            {
                return tryParseBoolean(WorkCapGroup_ColumnName, false);
            }
            set
            {
                this[WorkCapGroup_ColumnName] = value;
            }
        }

        [DBColumn(dRBringingFormula.FormulaId_ColumnName, ColumnDomain.UInt, Order = 24, OldDBColumnName = "Delta_R", Nullable = true, DefaultValue = 1)]
        public uint DRBringingFormulaId
        {
            get
            {
                return tryParseUInt(dRBringingFormula.FormulaId_ColumnName);
            }
            set
            {
                this[dRBringingFormula.FormulaId_ColumnName] = value;
            }
        }

        [DBColumn(dRFormula.FormulaId_ColumnName, ColumnDomain.UInt, Order = 25, OldDBColumnName = "DRPrivInd", Nullable = true, DefaultValue = 1)]
        public uint DRFormulaId
        {
            get
            {
                return tryParseUInt(dRFormula.FormulaId_ColumnName);
            }
            set
            {
                this[dRFormula.FormulaId_ColumnName] = value;
                loadDRFormula();
                refreshDRMeasureData();
            }
        }

        public const string StructureId_ColumnName = "cable_structure_id";
        public const string RealAmount_ColumnName = "real_amount";
        public const string ShownAmount_ColumnName = "shown_amount";
        public const string LeadDiameter_ColumnName = "lead_diameter";
        public const string WaveResistance_ColumnName = "wave_resistance";
        public const string GroupedAmount_ColumnName = "grouped_amount";
        public const string LeadLeadVoltage_ColumnName = "ll_test_voltage";
        public const string LeadShieldVoltage_ColumnName = "ls_test_voltage";
        public const string WorkCapGroup_ColumnName = "work_capacity_group";
        public const string IsolationResistanceVoltage_ColumnName = "risol_voltage";


        #endregion 
        private void refreshDRMeasureData()
        {
            foreach(CableStructureMeasuredParameterData mpd in MeasuredParameters.Rows)
            {
                if (mpd.ParameterTypeId == MeasuredParameterType.dR) mpd.ResultMeasure = drFormula.ResultMeasure;
            }
            
        }

        public string StructureTitle
        {
            get
            {
                return $"{((StructureType == null) ? "0" : StructureType.StructureLeadsAmount.ToString())}x{DisplayedAmount}x{LeadDiameter}";
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
                this.StructureTypeId = (structureType == null) ? 0 : this.structureType.StructureTypeId;
            }
        }

        public DBEntityTable MeasuredParameters_was => measuredParameters_was;
        public virtual DBEntityTable MeasuredParameters
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
                        measuredParameters = CableStructureMeasuredParameterData.get_structure_measured_parameters(this);
                    }
                    measuredParameters_was = measuredParameters.Clone() as DBEntityTable;
                }
                return measuredParameters;
            }
        }


        /// <summary>
        /// Достает MeasuredParameterDataId используемые текущей структурой
        /// </summary>
        public uint[] MeasuredParameters_ids
        {
            get
            {
                List<uint> idsList = new List<uint>();
                foreach(CableStructureMeasuredParameterData csmpd in MeasuredParameters.Rows)
                {
                    idsList.Add(csmpd.MeasuredParameterDataId);
                }
                return idsList.ToArray();
            }
        }

        /// <summary>
        /// Достает MeasuredParameterTypeId используемые текущей структурой
        /// </summary>
        public uint[] MeasuredParameterTypes_ids
        {
            get
            {
                List<uint> idsList = new List<uint>();
                foreach (CableStructureMeasuredParameterData csmpd in MeasuredParameters.Rows)
                {
                    idsList.Add(csmpd.ParameterTypeId);
                }
                
                return idsList.ToArray();
            }
        }

        public bool HasFreqParameters
        {
            get
            {
                try
                {
                    DBEntityTable t = new DBEntityTable(typeof(MeasuredParameterType));
                    return StructureType.MeasuredParameterTypes.Select($"{t.PrimaryKey[0].ColumnName} IN ({MeasuredParameterType.al}, {MeasuredParameterType.Ao}, {MeasuredParameterType.Az})").Count() > 0;
                }
                catch(NullReferenceException)
                {
                    return false;
                }
              
            }
        }

        public bool HasFreqMeasuredParameterData
        {
            get
            {
                try
                {
                    return MeasuredParameters.Select($"{MeasuredParameterType.ParameterTypeId_ColumnName} IN ({MeasuredParameterType.al}, {MeasuredParameterType.Ao}, {MeasuredParameterType.Az})").Count() > 0;
                }
                catch (NullReferenceException)
                {
                    return false;
                }
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

        public dRFormula DRFormula
        {
            get
            {
                if (drFormula == null) loadDRFormula();
                return drFormula;
            }
            set
            {
                drFormula = value;
            }
        }

        private void loadDRFormula()
        {
            drFormula = dRFormula.find_drFormula_by_id(DRFormulaId);
        }

        /// <summary>
        /// Может ли тип параметра измеряться на данном типе структуры
        /// </summary>
        /// <param name="parameter_type_id"></param>
        /// <returns></returns>
        public bool IsAllowParameterType(uint parameter_type_id)
        {
            DBEntityTable t = new DBEntityTable(typeof(MeasuredParameterType));
            return StructureType.MeasuredParameterTypes.Select($"{t.PrimaryKey[0].ColumnName} = {parameter_type_id}").Length > 0 ;
        }

        public bool HasMeasuredParameters
        {
            get
            {
                return MeasuredParameters.Rows.Count > 0;
            }
        }


        protected CableStructureType structureType;
        protected DBEntityTable measuredParameters;
        protected DBEntityTable measuredParameters_was;
        protected Cable ownCable;
        protected dRFormula drFormula;



    }

    [DBTable("tested_cable_structures", "db_norma_measure", OldDBName = "bd_isp", OldTableName = "structury_cab")]
    public class TestedCableStructure : CableStructure
    {
        public TestedCableStructure(DataRowBuilder builder) : base(builder)
        {
        }

        public new static TestedCableStructure find_by_structure_id(uint structure_id)
        {
            DBEntityTable t = find_by_primary_key(structure_id, typeof(TestedCableStructure));
            TestedCableStructure structure = null;
            if (t.Rows.Count > 0) structure = (TestedCableStructure)t.Rows[0];
            return structure;
        }

        public static DBEntityTable get_by_cable(TestedCable cable)
        {
            DBEntityTable t = new DBEntityTable(typeof(TestedCable));
            DBEntityTable cabStructures = find_by_criteria($"{t.PrimaryKey[0].ColumnName} = {cable.CableId}", typeof(TestedCableStructure));
            foreach (TestedCableStructure cs in cabStructures.Rows) cs.OwnCable = cable;

            return cabStructures;
        }

        public new void CopyFromStructure(CableStructure structure)
        {
            this.FillColsFromEntity(structure);
            this.AddMeasuredParameterDataFromStructure(structure);
        }

        private void AddMeasuredParameterDataFromStructure(CableStructure structure)
        {
            this.MeasuredParameters.Clear();
            foreach (CableStructureMeasuredParameterData data in structure.MeasuredParameters.Rows)
            {
                TestedStructureMeasuredParameterData dta = (TestedStructureMeasuredParameterData)MeasuredParameters.NewRow();
                dta.FillColsFromEntity(data);
                dta.CableStructureId = this.CableStructureId;

                this.MeasuredParameters.Rows.Add(dta);
            }

        }

        [DBColumn(Cable.CableId_ColumnName, ColumnDomain.UInt, Order = 11, OldDBColumnName = "CabNum", Nullable = false, ReferenceTo = "tested_cables("+ Cable.CableId_ColumnName +") ON DELETE CASCADE")]
        public new uint CableId
        {
            get
            {
                return tryParseUInt(Cable.CableId_ColumnName);
            }
            set
            {
                this[Cable.CableId_ColumnName] = value;
            }
        }

        public override DBEntityTable MeasuredParameters
        {
            get
            {

                if (measuredParameters == null)
                {
                    if (IsNewRecord())
                    {
                        measuredParameters = TestedStructureMeasuredParameterData.get_tested_structure_measured_parameters(0);

                    }
                    else
                    {
                        measuredParameters = TestedStructureMeasuredParameterData.get_tested_structure_measured_parameters(this);
                        
                    }
                }
                return measuredParameters;
            }
        }


    }




}
