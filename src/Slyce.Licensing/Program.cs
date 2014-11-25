using System;
using System.Collections.Generic;
using System.Windows.Forms;

namespace Slyce.Licensing
{
    static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            if (License.Status.Licensed)
            {
                Environment.Exit(1);
            }
            else
            {
                Application.EnableVisualStyles();
                Application.SetCompatibleTextRenderingDefault(false);
                Application.Run(new frmMain());
                int exitCode = Slyce.Licensing.Licenser.ContinueWithTrial ? 1 : 0;
                Environment.Exit(exitCode);
            }
        }
    }
}