using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace WoodClub
{
    static class Program
    {
        private static readonly log4net.ILog log = log4net.LogManager.GetLogger
                  (System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);
        private static Form Form1 = null;
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            
            if (Form1 == null)
            {
                Form1 = new Form1();
                Application.Run(Form1);
            }           
        }
    }
}
