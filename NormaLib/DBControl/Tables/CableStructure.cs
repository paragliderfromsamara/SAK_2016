using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Diagnostics;
using NormaLib.Measure;

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

        private Dictionary<int, uint> affected_elements = new Dictionary<int, uint>();
        public Dictionary<int, uint> AffectedElements
        {
            get
            {
                return affected_elements;
            }set
            {
                affected_elements = value;
            }
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

        public int GetRisolTimeLimit(CableStructureMeasuredParameterData cur_data)
        {
            if (!MeasuredParameterType.IsItIsolationResistance(cur_data.ParameterTypeId)) return 60;
            if (cur_data.IsItRisolTimeParameterData) return (int)cur_data.MaxValue;
            else
            {
                CableStructureMeasuredParameterData pData = null;
                pData = cur_data.GetRisolTimeLimit();
                if (pData == null) return 60;
                else return (int)pData.MaxValue;
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
            }set
            {
                if (measuredParameters != null) measuredParameters_was = measuredParameters.Clone() as DBEntityTable;
                measuredParameters = value;
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


        public MeasuredParameterType[] MeasuredParameterTypes => MeasuredParameterType.get_all_by_ids_as_array(MeasuredParameterTypes_ids);

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

        #region Results 
        private DBEntityTable testResults = null;

        public DBEntityTable TestResults
        {
            get
            {
                if (testResults == null)
                {
                    testResults = new DBEntityTable(typeof(CableTestResult));
                    (OwnCable as TestedCable).CableTest.TestResults.GetForStructure(CableStructureId).CopyToDataTable(testResults, LoadOption.Upsert);
                    CableTestResult[] affected = (CableTestResult[])testResults.Select($"{MeasuredParameterType.ParameterTypeId_ColumnName} = {MeasuredParameterType.Calling}");
                    foreach (CableTestResult r in affected) if (!AffectedElements.ContainsKey((int)r.ElementNumber)) AffectedElements.Add((int)r.ElementNumber, (uint)r.Result);
                }
                return testResults;
            }
        }

        private uint[] testedElements = null;
        public uint[] TestedElements
        {
            get
            {
                if (testedElements == null)
                {
                    List<uint> elNumbers = new List<uint>();
                    foreach (CableTestResult r in TestResults.Rows) elNumbers.Add(r.ElementNumber);
                    IEnumerable<uint> vals = elNumbers.Distinct().OrderBy(x => x);
                    testedElements = vals.ToArray();
                }
                return testedElements;
            }
        }
        /*
        private Dictionary<uint, Dictionary<int, double>> ConvertedResultsDictionary = new Dictionary<uint, Dictionary<int, double>>();



        public Dictionary<int, double> GetConvertedResultsByMeasureParameterData(TestedStructureMeasuredParameterData parameter_data)
        {
            if (ConvertedResultsDictionary.ContainsKey(parameter_data.MeasuredParameterDataId)) return ConvertedResultsDictionary[parameter_data.MeasuredParameterDataId];
            else
            {
                Dictionary<int, double> vals = new Dictionary<int, double>();
                MeasurePointMap map = new MeasurePointMap(this, parameter_data.ParameterTypeId);
                string NotAffectedElements = AffectedElements.Count > 0 ? $" AND {CableTestResult.StructElementNumber_ColumnName} NOT IN ({string.Join(",", AffectedElements.Keys)})" : string.Empty;
                CableTestResult[] results = (CableTestResult[])TestResults.Select($"{MeasuredParameterType.ParameterTypeId_ColumnName} = {parameter_data.ParameterTypeId}{NotAffectedElements}");
                foreach(CableTestResult r in results)
                {
                    float cableLength = (OwnCable as TestedCable).CableTest.CableLength;
                    MeasureResultConverter converter = new MeasureResultConverter((double)r.Result, parameter_data, cableLength);
                    int point = map.GetPointIndex(r.ElementNumber, r.MeasureNumber);
                    vals.Add(point, converter.ConvertedValueRounded);
                }
                ConvertedResultsDictionary.Add(parameter_data.MeasuredParameterDataId, vals);
                return vals;
            }
        }

        public double GetValueByParameterData(TestedStructureMeasuredParameterData parameter_data, uint element_number, uint measure_number)
        {
            Dictionary<int, double> results = GetConvertedResultsByMeasureParameterData(parameter_data);
            MeasurePointMap map = new MeasurePointMap(this, parameter_data.ParameterTypeId);
            int point = map.GetPointIndex(element_number, measure_number);
            if (results.ContainsKey(point)) return results[point];
            else return double.NaN;
        }

        public double MaxValueOfParameterData(TestedStructureMeasuredParameterData parameter_data)
        {
            Dictionary<int, double> results = GetConvertedResultsByMeasureParameterData(parameter_data);
            return results.Values.Max();
        }

        public double MinValueOfParameterData(TestedStructureMeasuredParameterData parameter_data)
        {
            Dictionary<int, double> results = GetConvertedResultsByMeasureParameterData(parameter_data);
            return results.Values.Min();
        }

        public double AverageValueOfParameterData(TestedStructureMeasuredParameterData parameter_data)
        {
            Dictionary<int, double> results = GetConvertedResultsByMeasureParameterData(parameter_data);
            return results.Values.Average();
        }





        */
        public TestedStructureMeasuredParameterData[] GetMeasureParameterDatasByParameterType(uint parameter_type_id)
        {
            return (TestedStructureMeasuredParameterData[])MeasuredParameters.Select($"{MeasuredParameterType.ParameterTypeId_ColumnName} = {parameter_type_id}");
        }
        #endregion

        private uint[] testedParametersIds = null;
        public uint[] TestedParametersIds
        {
            get
            {
                if (testedParametersIds == null)
                {
                    List<uint> elNumbers = new List<uint>();
                    foreach (CableTestResult r in TestResults.Rows) elNumbers.Add(r.ParameterTypeId);
                    IEnumerable<uint> vals = elNumbers.Distinct().OrderBy(x => x);
                    testedParametersIds = vals.ToArray();
                }
                return testedParametersIds;
            }
        }

        public DBEntityTable tested_parameter_types = null;
        public DBEntityTable TestedParameterTypes
        {
            get
            {
                if (tested_parameter_types == null)
                {
                    tested_parameter_types = MeasuredParameterType.get_all_by_ids(TestedParametersIds);
                }
                return tested_parameter_types;
            }
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

        /// <summary>
        /// Id исходной структуры, из которой создавалась структура для тестов
        /// </summary>
        public uint SourceStructureId = 0;

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


        public int NormalElementsAmount
        {
            get
            {
                IEnumerable<uint> temp = new List<uint>();
                for (uint i = 0; i < RealAmount; i++) ((List<uint>)temp).Add(i+1);               
                
                foreach (MeasuredParameterType pType in TestedParameterTypes.Rows)
                {
                    if (pType.ParameterTypeId == MeasuredParameterType.Risol3 || pType.ParameterTypeId == MeasuredParameterType.Risol4) continue;
                    TestedStructureMeasuredParameterData[] data = GetMeasureParameterDatasByParameterType(pType.ParameterTypeId);
                    foreach(TestedStructureMeasuredParameterData d in data)
                    {
                        if (d.GoodElements.Length > 0)
                        {
                            temp = temp.Intersect(d.GoodElements.ToList());
                            //Debug.WriteLine($"{string.Join(", ", temp)}");
                        }
                    }
                    //Debug.WriteLine($"{string.Join(", ", temp)}");
                }
                
                return temp.Count();
            }
        } 
    }




}
