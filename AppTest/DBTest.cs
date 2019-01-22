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
            object[] tableAttrs = t.GetCustomAttributes(typeof(DBTableAttribute), true);
            if (tableAttrs.Length == 1)
            {
                DBTableAttribute a = (DBTableAttribute)tableAttrs[0];
                Console.WriteLine($"{a.DBName}.{a.TableName} ({a.OldDBName}.{a.OldTableName})");
            }
        }

    }


}
