using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NormaMeasure.ProtocolBuilder
{
    public class ProtocolTable
    {
        public List<ProtocolTableColumn> Columns;
        public ProtocolTable()
        {
            Columns = new List<ProtocolTableColumn>();
        }
    }

    public class ProtocolTableColumn
    {
        public string title;
        public List<string> values;
        public ProtocolTableColumn(string _title)
        {
            title = _title;
            values = new List<string>();
        }
    }
}
