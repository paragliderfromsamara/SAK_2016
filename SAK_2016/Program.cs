using System;
using System.Diagnostics;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Windows.Forms;

namespace SAK_2016
{

    static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]

        

        static void Main()
        {
            string name = "SAK_2016";
            Process[] pr2 = Process.GetProcesses();
            Process cur_proc;
            int matches = 0;
            for (int i = 0; i < pr2.Length; i++)
            {
                if (pr2[i].ProcessName == name || pr2[i].ProcessName == name + ".exe")
                {
                    matches++;
                    if (matches == 1)
                    {
                        cur_proc = pr2[i];
                    }
                }
            }
            if (matches > 1) return;

            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);

            Application.Run(new mainForm());
            

        }
    }
   
}
