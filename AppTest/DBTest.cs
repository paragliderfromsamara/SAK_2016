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
            Program.PrintTitle("Список таблиц базы данных");
            Console.WriteLine();
            outputTable(typeof (CableRow));
            Console.WriteLine();
            Program.PrintTitle("Конец");
        }

        private static void outputTable(System.Type t)
        {
            Dictionary<string, SortedList<int, string>> d = DBTablesMigration.OutputTable(t);
            if (d.Keys.Count == 1)
            {
                SortedList<int, string> s = d[d.Keys.First()];
                Console.WriteLine(d.Keys.First());
                foreach(int k in s.Keys)
                {
                    Console.WriteLine($"{k-10} {s[k]}");
                }
            }

        }

    }


}
