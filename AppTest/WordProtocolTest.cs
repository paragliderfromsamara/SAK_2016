using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NormaMeasure.ProtocolBuilder.MSWord;
using NormaMeasure.ProtocolBuilder;
namespace AppTest
{
    class WordProtocolTest
    {
        public static void Start()
        {
            MSWordProtocol protocol = new MSWordProtocol();
            protocol.Init();
            for(int i=0; i<5; i++) protocol.AddTable(TestTable());
            protocol.Finalise();
        }

        private static ProtocolTable TestTable()
        {
            ProtocolTable table = new ProtocolTable();
            ProtocolTableRow row = new ProtocolTableRow();
            row.AddCell("Merged cell", 250);
            //row.AddCell("Merged cell");
            //row.AddCell("Merged cell");
            for (int i=0; i<10; i++)
            {
                string[] rowData = new string[] { $"{i} {0}", $"{i} {1}", $"{i} {2}", $"{i} {3}", $"{i} {4}" };
                table.AddRow(rowData);
            }
            table.AddRow(row);
            return table;
        }
    }
}
