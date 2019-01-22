using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AppTest
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WindowWidth = 100;
            DBTest.Start();


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

        
    }
}
