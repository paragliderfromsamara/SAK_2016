using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Runtime.InteropServices;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace TeraMicroMeasure
{
    static class Program
    {
        [DllImport("user32.dll")]
        static extern bool SetForegroundWindow(IntPtr hWnd);
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            Process[] procs = Process.GetProcessesByName(Process.GetCurrentProcess().ProcessName);
            if (procs.Length > 1)
            {
                SetForegroundWindow(procs[0].MainWindowHandle);
                return;
            }
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
  
            Application.Run(new AppForm());

            
        }

    }
}
