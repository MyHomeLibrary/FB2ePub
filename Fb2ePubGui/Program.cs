using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Windows.Forms;
using Fb2epubSettings;
using FontsSettings;
using log4net;
using FolderSettingsHelper;

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
            string logPath = Path.Combine(FolderLocator.GetLocalAppDataFolder(), "Lord KiRon\\");
            GlobalContext.Properties["LogName"] = logPath;
            log = LogManager.GetLogger(Assembly.GetExecutingAssembly().GetType());

            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);

        /*    Fb2Epub.Default.Fonts = new FontSettings();
            CSSFontFamily family1 = new CSSFontFamily();
            
            CSSFont font1 = new CSSFont();
            font1.FontStyle = FontStylesEnum.Normal;
            font1.FontVariant = FontVaiantEnum.Normal;
            font1.FontWidth = FontBoldnessEnum.Normal;
            font1.Sources.Add(new FontSource { Type = SourceTypes.Embedded, Location = @"%ResourceFolder%\Fonts/LiberationSerif-Regular.ttf" });
            family1.Fonts.Add(font1);

            CSSFont font2 = new CSSFont();
            font2.FontStyle = FontStylesEnum.Italic;
            font2.FontVariant = FontVaiantEnum.Normal;
            font2.FontWidth = FontBoldnessEnum.Normal;
            font2.Sources.Add(new FontSource{ Type = SourceTypes.Embedded, Location = @"%ResourceFolder%\Fonts/LiberationSerif-Italic.ttf" });
            family1.Fonts.Add(font2);


            CSSFont font3 = new CSSFont();
            font3.FontStyle = FontStylesEnum.Normal;
            font3.FontVariant = FontVaiantEnum.Normal;
            font3.FontWidth = FontBoldnessEnum.Bold;
            font3.Sources.Add(new FontSource { Type = SourceTypes.Embedded, Location = @"%ResourceFolder%\Fonts/LiberationSerif-Bold.ttf" });
            family1.Fonts.Add(font3);

            CSSFont font4 = new CSSFont();
            font4.FontStyle = FontStylesEnum.Italic;
            font4.FontVariant = FontVaiantEnum.Normal;
            font4.FontWidth = FontBoldnessEnum.Bold;
            font4.Sources.Add(new FontSource { Type = SourceTypes.Embedded, Location = @"%ResourceFolder%\Fonts/LiberationSerif-BoldItalic.ttf" });
            family1.Fonts.Add(font4);


            Fb2Epub.Default.Fonts.FontFamilies.Add(family1);



            CSSStylableElement elm1 = new CSSStylableElement();
            elm1.Name = "body";
            elm1.AssignedFontFamilies.Add(family1.Name);
            Fb2Epub.Default.Fonts.CssElements.Add(elm1);


            CSSStylableElement elm2 = new CSSStylableElement();
            elm2.Name = "code";
            elm2.AssignedFontFamilies.Add(family1.Name);
            Fb2Epub.Default.Fonts.CssElements.Add(elm2);


            CSSStylableElement elm3 = new CSSStylableElement();
            elm3.Class = "epub";
            elm3.AssignedFontFamilies.Add(family1.Name);
            Fb2Epub.Default.Fonts.CssElements.Add(elm3);

            Fb2Epub.Default.Save();      */              

            
            Application.Run(new FormGUI());
        }
    }
}
