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
            //ExperimentFunc();
            //DBTest.Start();
            //DeviceTest.Start();
            //WordProtocolTest.Start();
            //GetTkcIzol();
            //GetTablesList();
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

            File.WriteAllText(@"ass_hole.txt", string.Join("\n", values));//fs.WriteAsync()(values.ToArray(), 0, values.Count);
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
            CableTestIni f = new CableTestIni(1);
            f.SourceCable = Cable.get_all_as_table().Rows[0] as Cable;
            Random r = new Random();
            MeasurePointsHandler handler = new MeasurePointsHandler((point)=> { f.SetMeasurePointValue(point, (float)r.Next(100, 125)); });
            handler.ProcessCable(f.SourceCable);
        }

        
    }
}
