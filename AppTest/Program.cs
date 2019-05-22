using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

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
            WordProtocolTest.Start();

            Console.ReadLine();
            

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

        
    }
}
