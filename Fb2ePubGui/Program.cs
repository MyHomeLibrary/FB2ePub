using System;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Windows.Forms;
using System.Xml.Linq;
using log4net;
using FolderSettingsHelper;

// Configure log4net using the .config file
using XHTMLClassLibrary.BaseElements;
using XHTMLClassLibrary.BaseElements.InlineElements.TextBasedElements;

[assembly: log4net.Config.XmlConfigurator(Watch = true)]

namespace Fb2ePubGui
{
    static class Program
    {
        public static ILog Log;
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            //var item = (Anchor)ElementFactory.CreateElement(new XElement("a"), HTMLElementType.HTML5);
            //item.Rel.Value = Anchor.RelAttributeOptions.Author;
            //var test = item.Generate();
            string logPath = Path.Combine(FolderLocator.GetLocalAppDataFolder(), @"Lord_KiRon\");
            GlobalContext.Properties["LogName"] = logPath;
            Log = LogManager.GetLogger(Assembly.GetExecutingAssembly().GetType());

            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);

            
            Application.Run(new FormGUI());
        }
    }
}
