using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;

namespace NormaMeasure.DBControl
{
    
    public abstract class BaseRow : DataRow
    {
        public BaseRow(DataRowBuilder builder) : base(builder)
        {
            
        }
    }
}
