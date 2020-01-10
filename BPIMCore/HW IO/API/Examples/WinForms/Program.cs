using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;

namespace BPIM_API_Use___WinForms_Examples
{
    static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(new ConnectAndSweepForm());
        }
    }
}
