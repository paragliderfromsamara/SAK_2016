using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NormaMeasure.DBControl;
using NormaMeasure.DBControl.SAC;

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
        }

        private static void outputTable(DBTable d)
        {
            Console.WriteLine(d.tableName);
            Console.WriteLine();
            foreach (DBTableColumn c in d.columns)
            {
                //Console.WriteLine($"{c.Name} {c.Type}");
                Console.WriteLine(c.AddColumnText);
            }
            Console.WriteLine();
        }

    }


}
