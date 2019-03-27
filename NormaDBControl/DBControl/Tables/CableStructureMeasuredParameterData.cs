using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NormaMeasure.DBControl.Tables
{
    [DBTable("cab_struct_meas_params_data", "db_norma_sac")]
    class CableStructureMeasuredParameterData : BaseEntity
    {
        public CableStructureMeasuredParameterData(DataRowBuilder builder) : base(builder)
        {
        }



        public static DBEntityTable get_structure_measured_parameters(uint structure_id)
        {
            DBEntityTable mdt = new DBEntityTable(typeof(MeasuredParameterData));
            DBEntityTable frt = new DBEntityTable(typeof(FrequencyRange));
            DBEntityTable t = new DBEntityTable(typeof(CableStructureMeasuredParameterData));
            DBEntityTable pt = new DBEntityTable(typeof(MeasuredParameterType));
            DBEntityTable lbt = new DBEntityTable(typeof(LengthBringingType));
            string select_cmd = $"{t.SelectQuery} LEFT OUTER JOIN {mdt.TableName} USING(measured_parameter_data_id) LEFT OUTER JOIN {frt.TableName} USING(frequency_range_id) LEFT OUTER JOIN {pt.TableName} USING(parameter_type_id) LEFT OUTER JOIN {lbt.TableName} USING (length_bringing_type_id) WHERE cable_structure_id = {structure_id}";
            t.FillByQuery(select_cmd);
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

        [DBColumn("parameter_description", ColumnDomain.Tinytext, Order = 15, IsVirtual = true)]
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


        [DBColumn("length_bringing_type_id", ColumnDomain.UInt, Order = 16, Nullable = true, DefaultValue =0, IsVirtual = true)]
        public uint LngthBringingTypeId
        {
            get
            {
                return tryParseUInt("length_bringing_type_id");
            }
            set
            {
                this["length_bringing_type_id"] = value;
            }
        }

        [DBColumn("length_bringing", ColumnDomain.Float, Order = 17, DefaultValue = 1000, IsVirtual = true)]
        public uint LngthBringing
        {
            get
            {
                return tryParseUInt("length_bringing");
            }
            set
            {
                this["length_bringing"] = value;
            }
        }

        [DBColumn("min_value", ColumnDomain.Float, Order = 18, DefaultValue = -9999999, IsVirtual = true)]
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

        [DBColumn("max_value", ColumnDomain.Float, Order = 19, DefaultValue = 9999999, IsVirtual = true)]
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

        [DBColumn("percent", ColumnDomain.Float, Order = 20, DefaultValue = 100, IsVirtual = true)]
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

        [DBColumn("frequency_range_id", ColumnDomain.UInt, Order = 24, DefaultValue = 0, IsVirtual = true)]
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
        public uint ResultMeasure
        {
            get
            {
                return tryParseUInt("result_measure");
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
                LngthBringing = mpd.LngthBringing;
                LngthBringingTypeId = mpd.LngthBringingTypeId;
                measuredParameterData = mpd;
            }
        }

        public bool IsFreqParameter
        {
            get
            {
                return MeasuredParameterType.IsFreqParameter(ParameterTypeId);
            }
        }

        public bool HasMaxLimit
        {
            get
            {
                return MeasuredParameterType.HasMaxLimit(ParameterTypeId);
            }
        }

        public bool HasMinLimit
        {
            get
            {
                return MeasuredParameterType.HasMinLimit(ParameterTypeId);
            }
        }

        public CableStructure CableStructure
        {
            get
            {
                if (cableStructure == null)
                {
                    CableStructure = CableStructure.find_by_structure_id(CableStructureId);
                }
                return cableStructure;
            }
            set
            {
                cableStructure = value;
                if (cableStructure != null)
                {
                    this.CableStructureId = cableStructure.CableStructureId;
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
                }
            }
        }

        private MeasuredParameterData measuredParameterData;
        private CableStructure cableStructure;
        private MeasuredParameterType parameterType;
    }
}
