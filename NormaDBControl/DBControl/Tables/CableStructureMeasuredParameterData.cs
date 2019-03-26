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
            string select_cmd = $"{t.SelectQuery} LEFT OUTER JOIN {mdt.TableName} USING(measured_parameter_data_id) LEFT OUTER JOIN {frt.TableName} USING(frequency_range_id) LEFT OUTER JOIN {pt.TableName} USING(parameter_type_id) WHERE cable_structure_id = {structure_id}";
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
        #region Колонки типа измеряемого параметра
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
        #endregion


        public MeasuredParameterData MeasuredParameterData
        {
            get
            {
                if (measuredParameterData == null)
                {
                    measuredParameterData = MeasuredParameterData.find_by_id(MeasuredParameterDataId);
                }
                return measuredParameterData;
            }
        }

        private MeasuredParameterData measuredParameterData;

    }
}
