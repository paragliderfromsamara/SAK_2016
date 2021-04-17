using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.IO;
using NormaLib.DBControl;
using NormaLib.DBControl.Tables;
using System.ComponentModel;
using MySql.Data.MySqlClient;
using NormaLib.Measure;

namespace AppTest
{
    class Program
    {
        static void Main(string[] args)
        {
            Thread.CurrentThread.CurrentCulture = new CultureInfo("my");
            Thread.CurrentThread.CurrentCulture.NumberFormat.NumberDecimalSeparator = ".";
            Console.WindowWidth = 100;
            //Console.WriteLine(DateTime.());
            //ExperimentFunc();
            //DBTest.Start();
            //DeviceTest.Start();
            //ProtocolTest.Start();
            //GetTkcIzol();
            //GetTablesList();
            //
            TestOfCableTest();
            //CreateDump();
            Console.ReadLine();
        }

        private static void CreateDump()
        {
            MySQLDBControl dbc = new MySQLDBControl("db_norma_measure");
            dbc.MakeDump(@"dumps", "db_norma_measure.sql");
        }
        private static void GetTablesList()
        {
            MySQLDBControl dbc = new MySQLDBControl("db_norma_measure");
            string s = string.Join("\n", dbc.GetTablesList());
            Console.WriteLine(s);
           // File.WriteAllText(@"ass_hole.txt", string.Join("\n", values));//fs.WriteAsync()(values.ToArray(), 0, values.Count);
            dbc.MyConn.Close();
        }

        private static void GetTkcIzol()
        {
            MySQLDBControl dbc = new MySQLDBControl("bd_cable");
            dbc.MyConn.Open();
            MySql.Data.MySqlClient.MySqlDataReader r = dbc.GetReader("SELECT * FROM tkc_izol");
            List<string> values = new List<string>();
            ///FileStream fs = new FileStream(@"ass_hole.txt", FileMode.Create);
            while (r.Read())
            {
                string s = "new string[] {%},";
                string vals = $"\"{r[0]}\", \"{r[1]}\", \"{r[2]}\"";
                s = s.Replace("%", vals);
                values.Add(s);
                Console.WriteLine(s);
            }

            //File.WriteAllText(@"ass_hole.txt", string.Join("\n", values));//fs.WriteAsync()(values.ToArray(), 0, values.Count);
            dbc.MyConn.Close();
        }

        public static void PrintTitle(string title)
        {
            int titleLength = 100;
            string r = String.Empty;
            int textStart = (titleLength / 2) - title.Length / 2;
            int afterText = 0;
            for (int i = 0; i < textStart; i++) r += "-";
            r += title;
            afterText = titleLength - r.Length;
            for (int i = 0; i < afterText; i++) r += "-";

            Console.WriteLine(r);
        }

        static void ExperimentFunc()
        {
            string s = "1,2,3,4,5,7,8,9,10";
            string[] arrs = s.Split(',');
            foreach(string c in arrs)
            {
                Console.WriteLine(c);
            }


        } 

        static void TestOfCableTest()
        {
            CableTest test;
            ClearTests();
            DBEntityTable cables_table = Cable.get_all_as_table();
            Random r = new Random();
            for(int time = 0; time < 1; time++)
            {
                foreach (Cable cable in cables_table.Rows)
                {
                    CableTestIni f = new CableTestIni(r.Next(0, 4));
                    f.TestedCableLength = r.Next(250, 3000);
                    f.SourceCable = cable;
                    f.OperatorID = (User.get_all_as_table().Rows[0] as User).UserId;

                    MeasurePointsHandler handler = new MeasurePointsHandler((point) => {
                        CableStructure s = ((CableStructure[])cable.CableStructures.Select($"{CableStructure.StructureId_ColumnName} = {point.StructureId}"))[0];
                        CableStructureMeasuredParameterData data = ((CableStructureMeasuredParameterData[])s.MeasuredParameters.Select($"{MeasuredParameterType.ParameterTypeId_ColumnName} = {point.ParameterTypeId}"))[0];
                        int startValue = data.HasMinLimit ? (int)data.MinValue : 0;
                        int endValue = data.HasMaxLimit ? (int)data.MaxValue : (int)data.MinValue * 2;
                        f.SetMeasurePointValue(point, (float)r.Next(startValue, endValue));
                    });

                    handler.ProcessCable(f.SourceCable);

                    foreach (CableStructure s in cable.CableStructures.Rows)
                    {

                        MeasurePointMap map = new MeasurePointMap(s, MeasuredParameterType.Calling);
                        if (s.RealAmount > 4)
                        {
                            for (uint i = LeadTestStatus.Ragged; i < LeadTestStatus.Broken; i++)
                            {
                                int v = r.Next(0, 10);
                                if (v > 9)
                                {
                                    int e = r.Next(1, (int)s.RealAmount);
                                    int m = map.MeasurePointsPerElement > 1 ? r.Next(1, map.MeasurePointsPerElement) : 1;
                                    map.SetMeasurePoint(e - 1, m - 1);
                                    f.SetLeadStatusOfPoint(map.CurrentPoint, (int)i);
                                }
                            }
                        }

                    }
                    f.SaveTest(out test);
                }
                Console.WriteLine($"{time}");
            }

            Console.WriteLine("TestOfCableTest Completed!");
        }

        static void ClearTests()
        {
            DBEntityTable t = CableTest.find_all();
            foreach (CableTest test in t.Rows) test.Destroy();

        }
    }
}
