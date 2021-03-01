using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NormaMeasure.DBControl.Tables
{
    [DBTable("cab_struct_meas_params_data", "db_norma_measure")]
    public class CableStructureMeasuredParameterData : BaseEntity
    {
        public CableStructureMeasuredParameterData(DataRowBuilder builder) : base(builder)
        {
            this.Table.ColumnChanged += Table_ColumnChanged;
        }

        public bool Validate()
        {
            ClearErrors();
            ValidateMinMax();
            ValidatePercent();
            ValidateBringingLength();
            if (ParameterType.IsFreqParameter) ValidateFrequencyRanges();
            return ErrorsList.Count == 0;
        }

        private void ValidateFrequencyRanges()
        {
            bool minFreqIsTooLow = FrequencyMin < 0.8 && FrequencyMin < 10;
            bool max_freq_is_upper_than_limit = FrequencyMax > 2000;
            bool min_freq_is_lower_than_limit = FrequencyMin < 10 && FrequencyMin != 0.8;
            if (minFreqIsTooLow)
            {
                ErrorsList.Add("Введите корректную минимальную частоту измерения. \n(Допустимые для ввода частоты: 0.8кГц, от 10 до 2000 кГц)");
            } else if (FrequencyMax < FrequencyMin && FrequencyMax != 0) ErrorsList.Add("Максимальная частота диапазона должна быть больше минимальной!");
        }

        private void ValidateBringingLength()
        {
            if (LengthBringingTypeId != LengthBringingType.NoBringing && LengthBringing <= 0) ErrorsList.Add("Длина приведения должна быть больше 0!");
        } 

        private void ValidatePercent()
        {
            if (Percent < 0) ErrorsList.Add("Значение допустимого процента брака должно быть не меньше 0%!");
            if (Percent > 100) ErrorsList.Add("Значение допустимого процента брака должно быть не больше 100%!");
        }

        private void ValidateMinMax()
        {
            bool hasMin, hasMax;
            hasMax = MeasuredParameterType.IsHasMaxLimit(ParameterTypeId);
            hasMin = MeasuredParameterType.IsHasMinLimit(ParameterTypeId);
            if (hasMin && hasMax)
            {
                if (MaxValue < MinValue) ErrorsList.Add("Максимальное допустимое значение не должно быть меньше минимального!");
            }
            if (hasMin && MinValue < MeasuredParameterData.MinValueDefault) ErrorsList.Add( $"Минимальное допустимое значение не должно быть меньше {MeasuredParameterData.MinValueDefault}");
            if (hasMax && MaxValue > MeasuredParameterData.MaxValueDefault) ErrorsList.Add($"Максимальное допустимое значение не должно быть больше {MeasuredParameterData.MaxValueDefault}");

        }

        public override bool Save()
        {
            this.MeasuredParameterDataId = MeasuredParameterData.GetByParameters(this).MeasureParameterDataId;
            this.find_or_create();
            return true;
        }

        protected void find_or_create()
        {

            DBEntityTable t = find_by_criteria(makeWhereQueryForAllColumns() + " LIMIT 1",typeof(CableStructureMeasuredParameterData), DBEntityTableMode.OwnColumns);
            
            if (t.Rows.Count == 0)
            {
                this.AcceptChanges();
                this.SetAdded();
                base.Save();
                this.AcceptChanges();
            }
        }


        private void Table_ColumnChanged(object sender, DataColumnChangeEventArgs e)
        {
            try
            {
                switch (e.Column.ColumnName)
                {
                    case LengthBringingType.BringingId_ColumnName:
                        SetBringingLengthByTypeId();
                        RefreshResultMeasure();
                        break;
                    case MeasuredParameterData.LengthBringing_ColumnName:
                        RefreshResultMeasure();
                        break;
                    case FrequencyRange.FreqMax_ColumnName:
                        if (!skipFreqChangeEvent) CheckMaxFreqChange();
                        break;
                    case FrequencyRange.FreqMin_ColumnName:
                        if (!skipFreqChangeEvent) CheckFreqMinChange();
                        break;
                    case FrequencyRange.FreqStep_ColumnName:
                        if (!skipFreqChangeEvent) CheckFreqStepChange();
                        break;
                }
            }
            catch(RowNotInTableException)
            {
                e.Row.Table.ColumnChanged -= Table_ColumnChanged;
            }

        }

        private void CheckMaxFreqChange()
        {
            float newFrStep = Math.Abs(FrequencyMax - FrequencyMin);
            float min = FrequencyMin;
            float max = FrequencyMax;
            float step = FrequencyStep;
            string msg = String.Empty;
            skipFreqChangeEvent = true; //Чтобы не произошло зацикливания при срабатывании
            if (min == max && max >= 10)
            {
                FrequencyStep = 0;
                FrequencyMax = 0;
            }
            else if (min >= 10 && (step == 0 || step > newFrStep) && max > min)
            {
                FrequencyStep = newFrStep;
            }
            skipFreqChangeEvent = false;
        }

        private void CheckFreqStepChange()
        {

        }
        private void CheckFreqMinChange()
        {
            float newFrStep = Math.Abs(FrequencyMax - FrequencyMin);
            float min = FrequencyMin;
            float max = FrequencyMax;
            float step = FrequencyStep;
            string msg = String.Empty;
            skipFreqChangeEvent = true; //Чтобы не произошло зацикливания при срабатывании
            if ((min == 0.8 || min == 1) || (min == max && max >= 10))
            {
                FrequencyStep = 0;
                FrequencyMax = 0;
            }
            else if (min >= 10 && (step == 0 || step > newFrStep) && max > min)
            {
                FrequencyStep = newFrStep;
            }
            //FrequencyStep = newFrStep;
            skipFreqChangeEvent = false;
        }


        public static void DeleteUnusedFromStructure(CableStructure cable_structure)
        {
            string ids = String.Empty;
            DBEntityTable t = new DBEntityTable(typeof(CableStructureMeasuredParameterData));
            DBEntityTable mpdTable = new DBEntityTable(typeof(MeasuredParameterData));
            string query = t.DeleteQuery + $" WHERE {cable_structure.Table.PrimaryKey[0].ColumnName} = {cable_structure.CableStructureId}";
            foreach (uint id in cable_structure.MeasuredParameters_ids)
            {
                if (!string.IsNullOrWhiteSpace(ids)) ids += ",";
                ids += id.ToString();
            }
            if (!string.IsNullOrWhiteSpace(ids)) query += $" AND NOT {mpdTable.PrimaryKey[0].ColumnName} IN ({ids})";
            t.WriteSingleQuery(query);
        }

        public static DBEntityTable get_structure_measured_parameters(uint structure_id)
        {
            DBEntityTable mdt = new DBEntityTable(typeof(MeasuredParameterData));
            DBEntityTable frt = new DBEntityTable(typeof(FrequencyRange));
            DBEntityTable t = new DBEntityTable(typeof(CableStructureMeasuredParameterData));
            DBEntityTable pt = new DBEntityTable(typeof(MeasuredParameterType));
            DBEntityTable lbt = new DBEntityTable(typeof(LengthBringingType));
            DBEntityTable cs = new DBEntityTable(typeof(CableStructure));
            string selectQuery = t.SelectQuery.Replace("*", $"*, {pt.TableName}.parameter_measure AS result_measure");
            selectQuery = $"{selectQuery} LEFT OUTER JOIN {mdt.TableName} USING({mdt.PrimaryKey[0].ColumnName}) LEFT OUTER JOIN {frt.TableName} USING({frt.PrimaryKey[0].ColumnName}) LEFT OUTER JOIN {pt.TableName} USING({pt.PrimaryKey[0].ColumnName}) LEFT OUTER JOIN {lbt.TableName} USING ({lbt.PrimaryKey[0].ColumnName}) WHERE {cs.PrimaryKey[0].ColumnName} = {structure_id}";
            return find_by_query(selectQuery, typeof(CableStructureMeasuredParameterData));
        } 


        public static DBEntityTable get_structure_measured_parameters(CableStructure cable_structure)
        {
            DBEntityTable t = get_structure_measured_parameters(cable_structure.CableStructureId);
            foreach(CableStructureMeasuredParameterData md in t.Rows)
            {
                md.AssignedStructure = cable_structure;
                md.AcceptChanges();
            }
            return t;
        }

        [DBColumn(CableStructure.StructureId_ColumnName, ColumnDomain.UInt, Order = 10, Nullable = false, ReferenceTo = "cable_structures("+ CableStructure.StructureId_ColumnName + ") ON DELETE CASCADE")]
        public uint CableStructureId
        {
            get
            {
                return tryParseUInt(CableStructure.StructureId_ColumnName);
            }
            set
            {
                this[CableStructure.StructureId_ColumnName] = value;
            }
        }

        [DBColumn(MeasuredParameterData.DataId_ColumnName, ColumnDomain.UInt, Order = 11, Nullable = false, ReferenceTo = "measured_parameter_data("+ MeasuredParameterData.DataId_ColumnName + ")")]
        public uint MeasuredParameterDataId
        {
            get
            {
                return tryParseUInt(MeasuredParameterData.DataId_ColumnName);
            }
            set
            {
                this[MeasuredParameterData.DataId_ColumnName] = value;
            }
        }
        #region Колонки типа измеряемого параметра (ParameterType)



        [DBColumn(MeasuredParameterType.ParameterTypeId_ColumnName, ColumnDomain.UInt, Order = 12, IsVirtual = true)]
        public uint ParameterTypeId
        {
            get
            {
                return tryParseUInt(MeasuredParameterType.ParameterTypeId_ColumnName);
            }
            set
            {
                this[MeasuredParameterType.ParameterTypeId_ColumnName] = value;
            }
        }

        [DBColumn(MeasuredParameterType.ParameterName_ColumnName, ColumnDomain.Tinytext, Order = 13, IsVirtual = true)]
        public string ParameterName
        {
            get
            {
                return this[MeasuredParameterType.ParameterName_ColumnName].ToString();
            }
            set
            {
                this[MeasuredParameterType.ParameterName_ColumnName] = value;
            }
        }

        [DBColumn(MeasuredParameterType.ParameterMeasure_ColumnName, ColumnDomain.Tinytext, Order = 14, IsVirtual = true)]
        public string ParameterMeasure
        {
            get
            {
                return this[MeasuredParameterType.ParameterMeasure_ColumnName].ToString();
            }
            set
            {
                this[MeasuredParameterType.ParameterMeasure_ColumnName] = value;
            }
        }

        [DBColumn(MeasuredParameterType.ParameterDescription_ColumnName, ColumnDomain.Tinytext, Nullable = true, Order = 15, IsVirtual = true)]
        public string ParameterDescription
        {
            get
            {
                return this[MeasuredParameterType.ParameterDescription_ColumnName].ToString();
            }
            set
            {
                this[MeasuredParameterType.ParameterDescription_ColumnName] = value;
            }
        }

        #endregion

        #region колонки значений измеряемого параметра (MeasuredParameters)


        [DBColumn(LengthBringingType.BringingId_ColumnName, ColumnDomain.UInt, Order = 16, Nullable = true, DefaultValue =0, IsVirtual = true)]
        public uint LengthBringingTypeId
        {
            get
            {
                return tryParseUInt(LengthBringingType.BringingId_ColumnName);
            }
            set
            {
                this[LengthBringingType.BringingId_ColumnName] = value;

                //System.Windows.Forms.MessageBox.Show(value.ToString());
            }
        }

        [DBColumn(MeasuredParameterData.LengthBringing_ColumnName, ColumnDomain.Float, Nullable = true, Order = 17, DefaultValue = 1000, IsVirtual = true)]
        public float LengthBringing
        {
            get
            {
                return tryParseFloat(MeasuredParameterData.LengthBringing_ColumnName);
            }
            set
            {
                this[MeasuredParameterData.LengthBringing_ColumnName] = value;
                //RefreshResultMeasure();
            }
        }

        [DBColumn(MeasuredParameterData.MinValue_ColumnName, ColumnDomain.Float, Order = 18, Nullable = true, DefaultValue = float.MinValue, IsVirtual = true)]
        public float MinValue
        {
            get
            {
                return tryParseFloat(MeasuredParameterData.MinValue_ColumnName);
            }
            set
            {
                this[MeasuredParameterData.MinValue_ColumnName] = value;
            }
        }

        [DBColumn(MeasuredParameterData.MaxValue_ColumnName, ColumnDomain.Float, Order = 19, Nullable = true, DefaultValue = float.MaxValue, IsVirtual = true)]
        public float MaxValue
        {
            get
            {
                return tryParseFloat(MeasuredParameterData.MaxValue_ColumnName);
            }
            set
            {
                this[MeasuredParameterData.MaxValue_ColumnName] = value;
            }
        }

        [DBColumn(MeasuredParameterData.Percent_ColumnName, ColumnDomain.Float, Order = 20, Nullable = true, DefaultValue = 100, IsVirtual = true)]
        public uint Percent
        {
            get
            {
                return tryParseUInt(MeasuredParameterData.Percent_ColumnName);
            }
            set
            {
                this[MeasuredParameterData.Percent_ColumnName] = value;
            }
        }
        #endregion

        #region Парметры частоты 

        [DBColumn(FrequencyRange.FreqMin_ColumnName, ColumnDomain.Float, Order = 21, Nullable = true, IsVirtual = true)]
        public float FrequencyMin
        {
            get
            {
                return tryParseFloat(FrequencyRange.FreqMin_ColumnName);
            }
            set
            {
                this[FrequencyRange.FreqMin_ColumnName] = value;
            }
        }

        [DBColumn(FrequencyRange.FreqMax_ColumnName, ColumnDomain.Float, Order = 22,  Nullable = true, IsVirtual = true)]
        public float FrequencyMax
        {
            get
            {
                return tryParseFloat(FrequencyRange.FreqMax_ColumnName);
            }
            set
            {
                this[FrequencyRange.FreqMax_ColumnName] = value;
            }
        }

        [DBColumn(FrequencyRange.FreqStep_ColumnName, ColumnDomain.Float, Order = 23, Nullable = true, IsVirtual = true)]
        public float FrequencyStep
        {
            get
            {
                return tryParseFloat(FrequencyRange.FreqStep_ColumnName);
            }
            set
            {
                this[FrequencyRange.FreqStep_ColumnName] = value;
            }
        }

        [DBColumn(FrequencyRange.FreqRangeId_ColumnName, ColumnDomain.UInt, Nullable = true, Order = 24, DefaultValue = 0, IsVirtual = true)]
        public uint FrequencyRangeId
        {
            get
            {
                return tryParseUInt(FrequencyRange.FreqRangeId_ColumnName);
            }
            set
            {
                this[FrequencyRange.FreqRangeId_ColumnName] = value;
            }
        }

        #endregion

        #region Тип приведения результата 


        [DBColumn(LengthBringingType.BringingMeasure_ColumnName, ColumnDomain.Tinytext, Order = 25, Nullable = true, IsVirtual = true)]
        public string MeasureLengthTitle
        {
            get
            {
                return this[LengthBringingType.BringingMeasure_ColumnName].ToString();
            }
            set
            {
                this[LengthBringingType.BringingMeasure_ColumnName] = value;
            }
        }

        [DBColumn(LengthBringingType.BringingName_ColumnName, ColumnDomain.Tinytext, Order = 26, Nullable = true, IsVirtual = true)]
        public string LengthBringingName
        {
            get
            {
                return this[LengthBringingType.BringingName_ColumnName].ToString();
            }
            set
            {
                this[LengthBringingType.BringingName_ColumnName] = value;
            }
        }

        #endregion

        [DBColumn("result_measure", ColumnDomain.Tinytext, Order = 27, Nullable = true, IsVirtual = true)]
        public string ResultMeasure
        {
            get
            {
                return this["result_measure"].ToString();
            }
            set
            {
                this["result_measure"] = value;
            }
        }

        public MeasuredParameterData MeasuredParameterData
        {
            get
            {
                if (measuredParameterData == null)
                {
                    MeasuredParameterData mpd = MeasuredParameterData.find_by_id(MeasuredParameterDataId);
                    if (mpd != null) MeasuredParameterData = mpd;
                }
                return measuredParameterData;
            }
            set
            {
                MeasuredParameterData mpd = value as MeasuredParameterData;
                MinValue = mpd.MinValue;
                MaxValue = mpd.MaxValue;
                Percent = mpd.Percent;
                LengthBringing = mpd.LengthBringing;
                LengthBringingTypeId = mpd.LngthBringingTypeId;
                measuredParameterData = mpd;
            }
        }

        private void SetBringingLengthByTypeId()
        {
            if (LengthBringingTypeId == LengthBringingType.ForBuildLength)
            {
                LengthBringing = AssignedStructure.OwnCable.BuildLength;
            }else if (LengthBringingTypeId == LengthBringingType.ForOneKilometer)
            {
                LengthBringing = 1000;
            }
        }

        private void RefreshResultMeasure()
        {
            string delimiter = ParameterType.IsIsolationResistance ? "•" : "/";
            if (!MeasuredParameterType.AllowBringingLength(ParameterTypeId)) return;


            if (LengthBringingTypeId == LengthBringingType.ForOneKilometer)
            {
                ResultMeasure = $"{ParameterMeasure}{delimiter}км";
            }
            else if (LengthBringingTypeId == LengthBringingType.ForBuildLength)
            {
                ResultMeasure = $"{ParameterMeasure}{delimiter}Lстр";
            }
            else if (LengthBringingTypeId == LengthBringingType.ForAnotherLengthInMeters)
            {
                ResultMeasure = $"{ParameterMeasure}{delimiter}{LengthBringing}м";
            }
            else
            {
                ResultMeasure = ParameterMeasure;
            }
        }

        public bool IsFreqParameter
        {
            get
            {
                return MeasuredParameterType.IsItFreqParameter(ParameterTypeId);
            }
        }

        public bool HasMaxLimit
        {
            get
            {
                return MeasuredParameterType.IsHasMaxLimit(ParameterTypeId);
            }
        }

        public bool HasMinLimit
        {
            get
            {
                return MeasuredParameterType.IsHasMinLimit(ParameterTypeId);
            }
        }

        public CableStructure AssignedStructure
        {
            get
            {
                if (cableStructure == null)
                {
                    AssignedStructure = CableStructure.find_by_structure_id(CableStructureId);
                }
                return cableStructure;
            }
            set
            {
                cableStructure = value;
                if (cableStructure != null)
                {
                    this.CableStructureId = cableStructure.CableStructureId;
                    if (ParameterType.ParameterTypeId == MeasuredParameterType.dR) ResultMeasure = cableStructure.DRFormula.ResultMeasure;
                   // else RefreshResultMeasure();
                }
            }
        }

        public MeasuredParameterType ParameterType
        {
            get
            {
                if (parameterType == null) ParameterType = MeasuredParameterType.find_by_parameter_type_id(ParameterTypeId);
                return parameterType;
            }
            set
            {
                parameterType = value;
                if(parameterType != null)
                {
                    ParameterTypeId = parameterType.ParameterTypeId;
                    ParameterName = parameterType.ParameterName;
                    ParameterDescription = parameterType.Description;
                    ParameterMeasure = parameterType.Measure;
                    if (IsNewRecord()) SetDefaultsByParameterType();
                }
            }
        }

        public void SetDefaultsByParameterType()
        {
            if (ParameterType.ParameterTypeId != MeasuredParameterType.dR) ResultMeasure = ParameterType.Measure;
            if (ParameterType.HasMaxLimit) MaxValue = 10;
            if (ParameterType.HasMinLimit) MinValue = 0;
            if (ParameterType.IsFreqParameter)
            {
                FrequencyMin = 0;
                FrequencyMax = 0;
                FrequencyStep = 0;
            }
        }


        public LengthBringingType LengthBringingType
        {
            get
            {
               if  (lengthBringingType == null)
                {
                    LengthBringingType t = LengthBringingType.find_by_lengt_bringing_type_id(LengthBringingTypeId);
                    if (t != null) LengthBringingType = t;
                }
                return lengthBringingType;
            }
            set
            {
                lengthBringingType = value;
                LengthBringingTypeId = lengthBringingType.TypeId;
                LengthBringingName = lengthBringingType.BringingName;
                MeasureLengthTitle = lengthBringingType.MeasureTitle;
            }
        }

        private MeasuredParameterData measuredParameterData;
        private CableStructure cableStructure;
        private MeasuredParameterType parameterType;
        private LengthBringingType lengthBringingType;
        private bool skipFreqChangeEvent = false;
    }


    [DBTable("tested_cable_structure_measured_parameters", "db_norma_measure", OldDBName = "bd_isp", OldTableName = "structury_cab")]
    public class TestedStructureMeasuredParameterData : CableStructureMeasuredParameterData
    {
        public TestedStructureMeasuredParameterData(DataRowBuilder builder) : base(builder)
        {
        }

        [DBColumn("cable_structure_id", ColumnDomain.UInt, Order = 10, Nullable = false, ReferenceTo = "tested_cable_structures(cable_structure_id) ON DELETE CASCADE")]
        public new uint CableStructureId
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

        public static DBEntityTable get_tested_structure_measured_parameters(uint structure_id)
        {
            DBEntityTable mdt = new DBEntityTable(typeof(MeasuredParameterData));
            DBEntityTable frt = new DBEntityTable(typeof(FrequencyRange));
            DBEntityTable t = new DBEntityTable(typeof(TestedStructureMeasuredParameterData));
            DBEntityTable pt = new DBEntityTable(typeof(MeasuredParameterType));
            DBEntityTable lbt = new DBEntityTable(typeof(LengthBringingType));
            DBEntityTable cs = new DBEntityTable(typeof(TestedCableStructure));
            string selectQuery = t.SelectQuery.Replace("*", $"*, {pt.TableName}.parameter_measure AS result_measure");
            selectQuery = $"{selectQuery} LEFT OUTER JOIN {mdt.TableName} USING({mdt.PrimaryKey[0].ColumnName}) LEFT OUTER JOIN {frt.TableName} USING({frt.PrimaryKey[0].ColumnName}) LEFT OUTER JOIN {pt.TableName} USING({pt.PrimaryKey[0].ColumnName}) LEFT OUTER JOIN {lbt.TableName} USING ({lbt.PrimaryKey[0].ColumnName}) WHERE {cs.PrimaryKey[0].ColumnName} = {structure_id}";
            return find_by_query(selectQuery, typeof(TestedStructureMeasuredParameterData));
        }


        public static DBEntityTable get_tested_structure_measured_parameters(TestedCableStructure cable_structure)
        {
            DBEntityTable t = get_structure_measured_parameters(cable_structure.CableStructureId);
            foreach (TestedStructureMeasuredParameterData md in t.Rows)
            {
                md.AssignedStructure = cable_structure;
                md.AcceptChanges();
            }
            return t;
        }

    }
}
