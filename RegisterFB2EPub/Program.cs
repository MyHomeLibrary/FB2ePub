using System;
using System.Collections.Generic;
using System.IO;
using System.Reflection;
using System.Windows.Forms;
using log4net;

// Configure log4net using the .config file
[assembly: log4net.Config.XmlConfigurator(Watch = true)]


namespace RegisterFB2EPub
{
    class Program
    {
        private static ILog Log;
        private const string FileName = "Fb2EpubExt.dll";
        private const string FileName64 = "Fb2EpubExt_x64.dll";

        [STAThread]
        static void Main(string[] args)
        {
            GlobalContext.Properties["LogName"] = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData), "Lord KiRon\\");
            Log = LogManager.GetLogger(Assembly.GetExecutingAssembly().GetType());
            // Log an info level message
            Log.Info("FB2EPUB shell extension registration utility by Lord KiRon");
            List<string> options = new List<string>();

            try
            {
                foreach (var param in args)
                {
                    if (IsOptionParameter(param))
                    {
                        options.Add(param);
                    }
                }

                ExtRegistrator registrator = new ExtRegistrator();

                if (options.Count > 0)
                {
                    string fileName = GetDllFileName();
                    if ((options[0].ToLower() == "/r") || (options[0].ToLower() == "-r"))
                    {
                        string filePath = LocationDetector.DetectLocation(fileName);
                        if (string.IsNullOrEmpty(filePath))
                        {
                            Log.ErrorFormat("Unable to locate {0}", fileName);
                            return;
                        }
                        registrator.Path = filePath;
                        Log.InfoFormat("Registering {0}",filePath);
                        registrator.Register(ExtRegistrator.RegistrationExtensionEnum.BaseSet);
                        return;
                    }
                    if ((options[0].ToLower() == "/rall") || (options[0].ToLower() == "-rall"))
                    {
                        string filePath = LocationDetector.DetectLocation(fileName);
                        if (string.IsNullOrEmpty(filePath))
                        {
                            Log.ErrorFormat("Unable to locate {0}", fileName);
                            return;
                        }
                        registrator.Path = filePath;
                        Log.InfoFormat("Registering {0}", filePath);
                        registrator.Register(ExtRegistrator.RegistrationExtensionEnum.All);
                        return;
                    }
                    if ((options[0].ToLower() == "/u") || options[0].ToLower() == "-u")
                    {
                        Log.Info("Unregistering");
                        registrator.Unregister();
                        return;
                    }
                }
                else
                {
                    Application.Run(new MainForm());
                }

            }
            catch (Exception ex)
            {
                Log.Error(ex);
            }
        }

        private static string GetDllFileName()
        {
            if (IntPtr.Size == 8)
            {
                return FileName64;
            }
            return FileName;
        }


        private static bool IsOptionParameter(string param)
        {
            string lowParam = param.ToLower();
            if (lowParam.StartsWith("-") || lowParam.StartsWith("/"))
            {
                return true;
            }
            return false;
        }

    }
}
