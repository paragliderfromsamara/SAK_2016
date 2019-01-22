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

        protected uint tryParseUInt(string column_name)
        {
            uint v = 0;
            uint.TryParse(this[column_name].ToString(), out v);
            return v;
        }

        protected int tryParseInt(string column_name)
        {
            int v = 0;
            int.TryParse(this[column_name].ToString(), out v);
            return v;
        }

        protected bool tryParseBoolean(string column_name, bool default_val)
        {
            bool v = default_val;
            bool.TryParse(this[column_name].ToString(), out v);
            return v;
        }

        protected float tryParseFloat(string column_name)
        {
            float v = 0;
            float.TryParse(this[column_name].ToString(), out v);
            return v;
        }
    }
}
