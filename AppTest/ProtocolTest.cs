using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NormaLib.DBControl.Tables;
//using NormaMeasure.ProtocolBuilder.MSWord;
//using NormaMeasure.ProtocolBuilder;

using NormaLib.ProtocolBuilders;
using NormaLib.DBControl;
using NormaLib.SessionControl;

namespace AppTest
{
    class ProtocolTest
    {
        public static void Start()
        {
            Console.WriteLine("---- ProtocolTest ----");
            TestCableProtocolBuilder();

            /*
            MSWordProtocol protocol = new MSWordProtocol();
            protocol.Init();
            for(int i=0; i<5; i++) protocol.AddTable(TestTable());
            protocol.Finalise();
            */
        }

        private static void TestCableProtocolBuilder()
        {
            //try
            //{
                DBEntityTable tests = CableTest.find_all();
                if (tests.Rows.Count == 0) throw new Exception("Отсутствуют записи в таблице испытаний кабеля");
                TestCableProtocolBuilderToMsWord(tests.Rows[0] as CableTest);
                Console.WriteLine("TestCableProtocolBuilder Ok");
            //}
            //catch(Exception ex)
            //{
            //    Console.WriteLine("TestCableProtocolBuilder Error");
            //    Console.WriteLine("------------------------------");
            //    Console.WriteLine(ex.Message);
            //    Console.WriteLine("------------------------------");
            //}

        }

        private static void TestCableProtocolBuilderToMsWord(CableTest test)
        {
            ProtocolPathBuilder pathBuilder = new ProtocolPathBuilder(test, NormaExportType.MSWORD);
            User user = User.get_all_as_table().Rows[0] as User;
            SessionControl.SignIn(user);
            ProtocolViewer v = new ProtocolViewer(new ProtocolExport(new ProtocolPathBuilder(test, NormaExportType.MSWORD)));
            //ProtocolExport.ExportTo(test, NormaExportType.MSWORD);
        }

        //private static ProtocolTable TestTable()
        //{
        /*
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
        */
        //}
    }
}
