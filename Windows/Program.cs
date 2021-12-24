using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using Windows.Properties;

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
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(new PcApplicationContext());
        }
    }

    class PcApplicationContext : ApplicationContext
    {
        private NotifyIcon trayIcon;
        private MenuItem startStopMenuItem;
        private Listener listener;

        public PcApplicationContext()
        {
            listener = new Listener(1122); 
            listener.Start();

            startStopMenuItem = new MenuItem("Stop", ToggleApp);

            trayIcon = new NotifyIcon()
            {
                Icon = Resources.Icon,
                ContextMenu = new ContextMenu(new MenuItem[] {
                    startStopMenuItem,
                    new MenuItem("Exit", (sender, e) => { Application.Exit(); })
                }),

                Visible = true
            };
        }

        private void ToggleApp(object sender, EventArgs e)
        {
            if (listener.Server.IsTCPOnline)
            {
                listener.Server.Stop();
                startStopMenuItem.Text = "Start";
            }

            else
            {
                listener.Server.Start();
                startStopMenuItem.Text = "Stop";
            }
        }
    }
}
