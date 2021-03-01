using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NormaMeasure.DBControl;
using NormaMeasure.DBControl.SAC;
using NormaMeasure.DBControl.Tables;
using MySql.Data.MySqlClient;
using System.Data;

namespace AppTest
{
    class DBTest
    {
        public static void Start()
        {
            DBSACTablesMigration stm = new DBSACTablesMigration();
            stm.InitDataBase();
            Program.PrintTitle("Список таблиц базы данных");
            Console.WriteLine();
            for(int i=0; i< stm.TablesList.Length; i++)
            {
                outputTable(stm.TablesList[i]);
            }
            Program.PrintTitle("Конец");
            TestInsertCable();
        }



        private static void outputTable(DBTable d)
        {
            Console.WriteLine(d.AddTableQuery);
            Console.WriteLine();
            //foreach (DBTableColumn c in d.columns)
            //{
                //Console.WriteLine($"{c.Name} {c.Type}");
            //    Console.WriteLine(c.AddColumnText);
            //}
            Console.WriteLine();
        }


        private static void TestInsertCable()
        {
            DBEntityTable t = new DBEntityTable(typeof(Cable));
            t.PrimaryKey = new System.Data.DataColumn[] { t.Columns[0] };
            string cs = String.Format("UserId={0};Server={1};Password={2}; CharacterSet=cp1251; Database=db_norma_measure;", "root", "localhost", "");
            MySqlConnection Conn = new MySqlConnection(cs);
            MySqlCommand cmd = new MySqlCommand("SELECT * FROM cables");
            MySqlDataAdapter ad = new MySqlDataAdapter("SELECT * FROM cables", Conn);

            DataSet ds = new DataSet("db_norma_measure");

            ds.Tables.Add(t);

            Cable cable = Cable.build();

            cable.CableId = 0;
            cable.Name = "Cable 1";
            cable.DocumentId = 0;
            cable.LinearMass = 200;
            cable.PMax = 100;
            cable.PMin = 10;
            cable.Notes = "Notes";
            cable.Create();

            Cable.GetDraft();
            Console.WriteLine(cable.makeInsertQuery());
            //t.Rows.Add(cable);



           // ad.InsertCommand = new MySqlCommand($"INSERT ({colNames}) INTO cables VALUES({cols})", Conn);
           // try
           // {
           //     ad.Update(t);
           // }
           // catch(Exception ex)
           // {
           //     Console.WriteLine(ex.Message);
           // }


            
        }

    }


}
