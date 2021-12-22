using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace PC
{
    static class Program
    {
        /// <summary>
        ///  The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            Application.SetHighDpiMode(HighDpiMode.SystemAware);
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);

            new Listener(1122).Start();
            

            Application.Run(new PcApplicationContext());
        }
    }

    class PcApplicationContext : ApplicationContext
    {


        public PcApplicationContext()
        {
            
        }
    }
}
