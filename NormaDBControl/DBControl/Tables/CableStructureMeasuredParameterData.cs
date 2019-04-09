﻿using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NormaMeasure.DBControl.Tables
{
    [DBTable("cab_struct_meas_params_data", "db_norma_sac")]
    public class CableStructureMeasuredParameterData : BaseEntity
    {
        public CableStructureMeasuredParameterData(DataRowBuilder builder) : base(builder)
        {
            this.Table.ColumnChanged += Table_ColumnChanged;
        }

        protected void Validate()
        {
            ValidateMinMax();
            ValidatePercent();
            ValidateBringingLength();
            ValidateFrequencyRanges();
        }

        private void ValidateFrequencyRanges()
        {
        }

        private void ValidateBringingLength()
        {
            if (LengthBringingTypeId != LengthBringingType.NoBringing && LengthBringing <= 0) ErrorsList.Add("Длина приведения должна быть больше 0!");
        }

        private void ValidatePercent()
        {
            if (Percent < 0) ErrorsList.Add("Допустимый процент брака должен быть не меньше 0%!");
            if (Percent > 100) ErrorsList.Add("Допустимый процент брака должен быть не больше 100%!");
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
                    case "length_bringing_type_id":
                        SetBringingLengthByTypeId();
                        RefreshResultMeasure();
                        break;
                    case "length_bringing":
                        RefreshResultMeasure();
                        break;
                }
            }
            catch(RowNotInTableException)
            {
                e.Row.Table.ColumnChanged -= Table_ColumnChanged;
            }

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

        [DBColumn("cable_structure_id", ColumnDomain.UInt, Order = 10, Nullable = false, ReferenceTo = "cable_structures(cable_structure_id) ON DELETE CASCADE")]
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

        [DBColumn("measured_parameter_data_id", ColumnDomain.UInt, Order = 11, Nullable = false, ReferenceTo = "measured_parameter_data(measured_parameter_data_id)")]
        public uint MeasuredParameterDataId
        {
            get
            {
                return tryParseUInt("measured_parameter_data_id");
            }
            set
            {
                this["measured_parameter_data_id"] = value;
            }
        }
        #region Колонки типа измеряемого параметра (ParameterType)



        [DBColumn("parameter_type_id", ColumnDomain.UInt, Order = 12, IsVirtual = true)]
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

        [DBColumn("parameter_name", ColumnDomain.Tinytext, Order = 13, IsVirtual = true)]
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

        [DBColumn("parameter_measure", ColumnDomain.Tinytext, Order = 14, IsVirtual = true)]
        public string ParameterMeasure
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

        [DBColumn("parameter_description", ColumnDomain.Tinytext, Nullable = true, Order = 15, IsVirtual = true)]
        public string ParameterDescription
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

        #endregion

        #region колонки значений измеряемого параметра (MeasuredParameters)


        [DBColumn(" ", ColumnDomain.UInt, Order = 16, Nullable = true, DefaultValue =0, IsVirtual = true)]
        public uint LengthBringingTypeId
        {
            get
            {
                return tryParseUInt("length_bringing_type_id");
            }
            set
            {
                this["length_bringing_type_id"] = value;

                //System.Windows.Forms.MessageBox.Show(value.ToString());
            }
        }

        [DBColumn("length_bringing", ColumnDomain.Float, Nullable = true, Order = 17, DefaultValue = 1000, IsVirtual = true)]
        public float LengthBringing
        {
            get
            {
                return tryParseFloat("length_bringing");
            }
            set
            {
                this["length_bringing"] = value;
                //RefreshResultMeasure();
            }
        }

        [DBColumn("min_value", ColumnDomain.Float, Order = 18, Nullable = true, DefaultValue = float.MinValue, IsVirtual = true)]
        public float MinValue
        {
            get
            {
                return tryParseFloat("min_value");
            }
            set
            {
                this["min_value"] = value;
            }
        }

        [DBColumn("max_value", ColumnDomain.Float, Order = 19, Nullable = true, DefaultValue = float.MaxValue, IsVirtual = true)]
        public float MaxValue
        {
            get
            {
                return tryParseFloat("max_value");
            }
            set
            {
                this["max_value"] = value;
            }
        }

        [DBColumn("percent", ColumnDomain.Float, Order = 20, Nullable = true, DefaultValue = 100, IsVirtual = true)]
        public uint Percent
        {
            get
            {
                return tryParseUInt("percent");
            }
            set
            {
                this["percent"] = value;
            }
        }
        #endregion

        #region Парметры частоты 

        [DBColumn("frequency_min", ColumnDomain.UInt, Order = 21, Nullable = true, IsVirtual = true)]
        public uint FrequencyMin
        {
            get
            {
                return tryParseUInt("frequency_min");
            }
            set
            {
                this["frequency_min"] = value;
            }
        }

        [DBColumn("frequency_max", ColumnDomain.UInt, Order = 22,  Nullable = true, IsVirtual = true)]
        public uint FrequencyMax
        {
            get
            {
                return tryParseUInt("frequency_max");
            }
            set
            {
                this["frequency_max"] = value;
            }
        }

        [DBColumn("frequency_step", ColumnDomain.UInt, Order = 23, Nullable = true, IsVirtual = true)]
        public uint FrequencyStep
        {
            get
            {
                return tryParseUInt("frequency_step");
            }
            set
            {
                this["frequency_step"] = value;
            }
        }

        [DBColumn("frequency_range_id", ColumnDomain.UInt, Nullable = true, Order = 24, DefaultValue = 0, IsVirtual = true)]
        public uint FrequencyRangeId
        {
            get
            {
                return tryParseUInt("frequency_range_id");
            }
            set
            {
                this["frequency_range_id"] = value;
            }
        }

        #endregion

        #region Тип приведения результата 


        [DBColumn("measure_title", ColumnDomain.Tinytext, Order = 25, Nullable = true, IsVirtual = true)]
        public string MeasureLengthTitle
        {
            get
            {
                return this["measure_title"].ToString();
            }
            set
            {
                this["measure_title"] = value;
            }
        }

        [DBColumn("length_bringing_name", ColumnDomain.Tinytext, Order = 26, Nullable = true, IsVirtual = true)]
        public string LengthBringingName
        {
            get
            {
                return this["length_bringing_name"].ToString();
            }
            set
            {
                this["length_bringing_name"] = value;
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
            if (ParameterType.HasMaxLimit) MaxValue = 102;
            if (ParameterType.HasMinLimit) MinValue = 33;
            if (ParameterType.IsFreqParameter)
            {
                FrequencyMin = 40;
                FrequencyMax = 1000;
                FrequencyStep = 8;
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
    }
}
