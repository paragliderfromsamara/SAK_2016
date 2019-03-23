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
    }
}
