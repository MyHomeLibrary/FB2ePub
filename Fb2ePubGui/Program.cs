using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Windows.Forms;
using log4net;

// Configure log4net using the .config file
[assembly: log4net.Config.XmlConfigurator(Watch = true)]

namespace Fb2ePubGui
{
    static class Program
    {
        private static ILog log;
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            string logPath = Application.LocalUserAppDataPath;
            if (string.IsNullOrEmpty(logPath))
            {
                logPath = Application.UserAppDataPath;
                if (string.IsNullOrEmpty(logPath))
                {
                    logPath = "logs";
                }
            }
            GlobalContext.Properties["LogName"] = logPath;
            log = LogManager.GetLogger(Assembly.GetExecutingAssembly().GetType());

            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(new FormGUI());
        }
    }
}
