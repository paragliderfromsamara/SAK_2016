using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NormaMeasure.DBControl.Tables
{
    [DBTable("cable_structures", "db_norma_sac", OldDBName = "bd_cable", OldTableName = "cables")]
    class CableStructure : BaseEntity
    {
        public CableStructure(DataRowBuilder builder) : base(builder)
        {
        }
    }
}
