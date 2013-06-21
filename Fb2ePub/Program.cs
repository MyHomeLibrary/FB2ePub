using System;
using System.Collections.Generic;
using System.Configuration;
using System.Reflection;
using System.Threading.Tasks;
using System.Windows.Forms;
using Fb2ePubConverter;
using System.IO;
using FB2EPubConverter;
using log4net;
using ProcessStartInfo=System.Diagnostics.ProcessStartInfo;
using System.Diagnostics;
using FolderSettingsHelper;
using Fb2epubSettings;



// Configure log4net using the .config file
[assembly: log4net.Config.XmlConfigurator(Watch = true)]
// This will cause log4net to look for a configuration file
// called FB2EPUB.exe.config in the application base
// directory (i.e. the directory containing FB2EPUB.exe)


namespace Fb2ePub
{
    class Program 
    {     
        // Create a logger for use in this class
        
        private static ILog log;


        [STAThread]
        static void Main(string[] args)
        {
            SetupLogAndData();
            if (args.Length > 0)
            {
                List<string> options = new List<string>();
                List<string> fileParams = new List<string>();
                foreach (var param in args)
                {
                    if (IsOptionParameter(param))
                    {
                        options.Add(param);
                    }
                    else
                    {
                        fileParams.Add(param);
                    }
                }
                if ( fileParams.Count == 0)
                {
                    if ( options.Count == 1)
                    {
                        // Process single option command (registration etc.) , exit if it's a valid command
                        if( ProcessSingleOptionCommand(options[0]) )
                        {
                            return;
                        }
                    }
                    Console.WriteLine("Input file name missing. Exiting.");
                    log.Error("Input file name missing.");
                    if (log.IsInfoEnabled) log.Info("Application [FB2EPUB] End");
                    return;
                }
                //ConverterSettings settings = new ConverterSettings();
                ConvertProcessor processor = new ConvertProcessor();
                PreProcessParameters(options, processor.ProcessorSettings);
                ProcessSettings(processor);
                ProcessParameters(options, processor.ProcessorSettings);
                Console.WriteLine(string.Format("Loading {0}...", fileParams[0]));
                List<string> filesInMask = new List<string>();
                processor.DetectFilesToProcess(fileParams, ref filesInMask);
                string outputFileName = (fileParams.Count > 1)?fileParams[1]:null;
                processor.PerformConvertOperation(filesInMask,  outputFileName);
                Console.WriteLine("Done.");
            }
            else
            {
                ShowHelp();
            }
            log.Debug("Application [FB2EPUB] End");
        }

        /// <summary>
        /// called before loading settings to process parameters that affect loading
        /// </summary>
        /// <param name="options"></param>
        /// <param name="convertProcessorSettings"></param>
        private static void PreProcessParameters(List<string> options, ConvertProcessorSettings convertProcessorSettings)
        {
            foreach (var param in options)
            {
                string command = param.ToLower().Substring(1);
                if (command.StartsWith("cfg:")) //transliterate
                {
                    string commandValue = command.Substring(4);
                    convertProcessorSettings.SettingsFileToUse = commandValue;
                }
            }            
        }


        /// <summary>
        /// Processes single option command like settings ort registration that does not require actual converting
        /// </summary>
        /// <param name="singleOptionCommand">the option command to process</param>
        /// <returns>true if proper known command was processed, false if unrecognized command</returns>
        private static bool ProcessSingleOptionCommand(string singleOptionCommand)
        {
            if ((singleOptionCommand.ToLower() == "/settings") || singleOptionCommand.ToLower() == "-settings")
            {
                ConvertProcessor.ShowSettingsDialog(null);
                return true;
            }
            return false;
        }   


        private static void SetupLogAndData()
        {
            string logPath = Path.Combine(FolderLocator.GetLocalAppDataFolder(), @"Lord_KiRon\");
            GlobalContext.Properties["LogName"] = logPath;
            log = LogManager.GetLogger(Assembly.GetExecutingAssembly().GetType());
            // Log an info level message
            log.Debug("Application [FB2EPUB] Start");
            Console.WriteLine("FB2 to EPUB command line converter by Lord KiRon");
            Console.WriteLine(string.Format("Logging to: {0}\\", GlobalContext.Properties["LogName"]));
            Console.WriteLine();
        }





        private static int RunCommand(string command,string argument)
        {
            int ExitCode = -1;
            ProcessStartInfo ProcessInfo;
            Process Process;

            ProcessInfo = new ProcessStartInfo(command,argument);
            ProcessInfo.CreateNoWindow = false;
            ProcessInfo.UseShellExecute = true;
            Process = Process.Start(ProcessInfo);
            if (Process != null)
            {
                Process.WaitForExit();
                ExitCode = Process.ExitCode;
                Process.Close();
            }

            return ExitCode;
        }

        private static void ProcessSettings(ConvertProcessor processor)
        {
            processor.LoadSettings();
            processor.ProcessorSettings.Settings.ResourcesPath = Path.GetDirectoryName(Assembly.GetEntryAssembly().Location);
        }

        private static void ProcessParameters(List<string> options, ConvertProcessorSettings settings)
        {
            foreach (var param in options)
            {
                string command = param.ToLower().Substring(1);
                if (command.StartsWith("t:")) //transliterate
                {
                    string commandValue = command.Substring(2);
                    int value;
                    if (int.TryParse(commandValue,out value))
                    {
                        if (value == 0)
                        {
                            settings.Settings.Transliterate = false;
                            settings.Settings.TransliterateFileName = false;
                            settings.Settings.TransliterateToc = false;
                        }
                        else if (value == 1)
                        {
                            settings.Settings.Transliterate = true;
                            settings.Settings.TransliterateToc = true;
                            settings.Settings.TransliterateFileName = false;
                        }
                        else if (value == 2)
                        {
                            settings.Settings.Transliterate = false;
                            settings.Settings.TransliterateToc = false;
                            settings.Settings.TransliterateFileName = true;
                        }
                        else if (value == 3)
                        {
                            settings.Settings.Transliterate = true;
                            settings.Settings.TransliterateToc = true;
                            settings.Settings.TransliterateFileName = true;
                        }
                        else if (value == 4)
                        {
                            settings.Settings.Transliterate = true;
                            settings.Settings.TransliterateToc = false;
                            settings.Settings.TransliterateFileName = false;
                        }
                        else if (value == 5)
                        {
                            settings.Settings.Transliterate = true;
                            settings.Settings.TransliterateToc = false;
                            settings.Settings.TransliterateFileName = true;
                        }
                        else
                        {
                           log.InfoFormat("Invalid -t: parameter value {0}.",commandValue);
                        }
                    }
                    else
                    {
                        log.InfoFormat("Invalid -t: parameter value {0}.", commandValue);
                    }
                }
                else if (command.StartsWith("o:")) // output 
                {
                    if (command.EndsWith("\"") && command.Length > 4)
                    {
                        settings.Settings.OutPutPath = command.Substring(2, command.Length - 3);
                    }
                    else
                    {
                        settings.Settings.OutPutPath = command.Substring(2);
                    }
                }
                else if (command.StartsWith("f2i:")) // generate additional info
                {
                    string commandValue = command.Substring(4);
                    int value;
                    if (int.TryParse(commandValue, out value))
                    {
                        if (value == 0)
                        {
                            settings.Settings.Fb2Info = false;
                        }
                        else if (value == 1)
                        {
                            settings.Settings.Fb2Info = true;
                        }
                        else
                        {
                            log.InfoFormat("Invalid -f2i: parameter value {0}.", commandValue);
                        }
                    }
                    else
                    {
                        log.InfoFormat("Invalid -f2i: parameter value {0}.", commandValue);
                    }
                }
                else if (command == "s") // subfolders
                {
                    settings.LookInSubFolders = true;
                }
                else if (command.StartsWith("m:")) // mask when searching
                {
                    string commandValue = command.Substring(2);
                    int value;
                    if (int.TryParse(commandValue, out value))
                    {
                        if (value == 0)
                        {
                            settings.SearchMask = PathSearchOptions.Fb2Only;
                        }
                        else if (value == 1)
                        {
                            settings.SearchMask = PathSearchOptions.Fb2WithArchives;
                        }
                        else if (value == 2)
                        {
                            settings.SearchMask = PathSearchOptions.WithAllArchives;
                        }
                        else if (value == 3)
                        {
                            settings.SearchMask = PathSearchOptions.All;
                        }
                        else
                        {
                            log.InfoFormat("Invalid -m: parameter value {0}.", value);
                        }
                    }
                    else
                    {
                        log.InfoFormat("Invalid -m: parameter value {0}.", commandValue);
                    }
                }
                else if (command == "deletesource")
                {
                    settings.DeleteSource = true;
                }
                else if (command.StartsWith("fix:"))
                {
                    string commandValue = command.Substring(4);
                    int value;
                    if (int.TryParse(commandValue, out value))
                    {
                        if (value == 0)
                        {
                            settings.Settings.FixMode = FixOptions.DoNotFix;
                        }
                        else if (value == 1)
                        {
                            settings.Settings.FixMode = FixOptions.MinimalFix;
                        }
                        else if (value == 2)
                        {
                            settings.Settings.FixMode = FixOptions.UseFb2Fix;
                        }
                        else if (value == 3)
                        {
                            settings.Settings.FixMode = FixOptions.Fb2FixAlways;
                        }
                        else
                        {
                            log.InfoFormat("Invalid -fix: parameter value {0}.", value);
                        }
                    }
                }
                else if (command.StartsWith("seqadd:"))
                {
                    string commandValue = command.Substring(7);
                    int value;
                    if (int.TryParse(commandValue, out value))
                    {
                        if (value == 0)
                        {
                            settings.Settings.AddSeqToTitle = false;
                        }
                        else if (value == 1)
                        {
                            settings.Settings.AddSeqToTitle = true;
                        }
                        else
                        {
                            log.InfoFormat("Invalid -seqadd: parameter value {0}.", value);
                        }
                    }                    
                }
                else if (command.StartsWith("seqformat:"))
                {
                    string commandValue = command.Substring(10);
                    if (!string.IsNullOrEmpty(commandValue))
                        settings.Settings.SequenceFormat = commandValue;
                    else
                        log.InfoFormat("Invalid -seqformat: parameter value is empty.");
                }
                else if (command.StartsWith("nseqformat:"))
                {
                    string commandValue = command.Substring(10);
                    if (!string.IsNullOrEmpty(commandValue))
                        settings.Settings.NoSequenceFormat = commandValue;
                    else
                        log.InfoFormat("Invalid -nseqformat: parameter value is empty.");
                }
                else if (command.StartsWith("nnseqformat:"))
                {
                    string commandValue = command.Substring(10);
                    if (!string.IsNullOrEmpty(commandValue))
                        settings.Settings.NoSeriesFormat= commandValue;
                    else
                        log.InfoFormat("Invalid -nnseqformat: parameter value is empty.");
                }
                else if (command.StartsWith("aformat:"))
                {
                    string commandValue = command.Substring(10);
                    if (!string.IsNullOrEmpty(commandValue))
                        settings.Settings.AuthorFormat = commandValue;
                    else
                        log.InfoFormat("Invalid -aformat: parameter value is empty.");
                }
                else if (command.StartsWith("svformat:"))
                {
                    string commandValue = command.Substring(10);
                    if (!string.IsNullOrEmpty(commandValue))
                        settings.Settings.FileAsFormat = commandValue;
                    else
                        log.InfoFormat("Invalid -svformat: parameter value is empty.");
                }
                else if (command.StartsWith("flat:"))
                {
                    string commandValue = command.Substring(5);
                    int value;
                    if (int.TryParse(commandValue, out value))
                    {
                        if (value == 0)
                        {
                            settings.Settings.Flat = false;
                        }
                        else if (value == 1)
                        {
                            settings.Settings.Flat = true;
                        }
                        else
                        {
                            log.InfoFormat("Invalid -flat: parameter value {0}.", value);
                        }
                    }
                }
                else if (command.StartsWith("emstyles:"))
                {
                    string commandValue = command.Substring(9);
                    int value;
                    if (int.TryParse(commandValue, out value))
                    {
                        if (value == 0)
                        {
                            settings.Settings.EmbedStyles = false;
                        }
                        else if (value == 1)
                        {
                            settings.Settings.EmbedStyles = true;
                        }
                        else
                        {
                            log.InfoFormat("Invalid -emstyles: parameter value {0}.", value);
                        }
                    }
                }
                else if (command.StartsWith("apng:"))
                {
                    string commandValue = command.Substring(5);
                    int value;
                    if (int.TryParse(commandValue, out value))
                    {
                        if (value == 0)
                        {
                            settings.Settings.ConvertAlphaPng = false;
                        }
                        else if (value == 1)
                        {
                            settings.Settings.ConvertAlphaPng = true;
                        }
                        else
                        {
                            log.InfoFormat("Invalid -apng: parameter value {0}.", value);
                        }
                    }                                        
                }
                else if (command.StartsWith("cap:"))
                {
                    string commandValue = command.Substring(4);
                    int value;
                    if (int.TryParse(commandValue, out value))
                    {
                        if (value == 0)
                        {
                            settings.Settings.CapitalDrop = false;
                        }
                        else if (value == 1)
                        {
                            settings.Settings.CapitalDrop = true;
                        }
                        else
                        {
                            log.InfoFormat("Invalid -cap: parameter value {0}.", value);
                        }
                    }
                }
                else if (command.StartsWith("noabout"))
                {
                    settings.Settings.SkipAboutPage = true;
                }
                else if (command.StartsWith("xpgt:"))
                {
                    string commandValue = command.Substring(5);
                    int value;
                    if (int.TryParse(commandValue, out value))
                    {
                        if (value == 0)
                        {
                            settings.Settings.EnableAdobeTemplate = false;
                        }
                        else if (value == 1)
                        {
                            settings.Settings.EnableAdobeTemplate = true;
                        }
                        else
                        {
                            log.InfoFormat("Invalid -xpgt: parameter value {0}.", value);
                        }
                    }                   
                }
                else if (command.StartsWith("xpgtpath:"))
                {
                    settings.Settings.AdobeTemplatePath =  command.Substring(9);
                }
                else if (command.StartsWith("ignoretitle:"))
                {
                    string commandValue = command.Substring(12);
                    int value;
                    if (int.TryParse(commandValue, out value))
                    {
                        if (value == 0)
                        {
                            settings.Settings.IgnoreTitle = IgnoreTitleOptions.IgnoreNothing;
                        }
                        else if (value == 1)
                        {
                            settings.Settings.IgnoreTitle = IgnoreTitleOptions.IgnoreMainTitle;
                        }
                        else if (value == 2)
                        {
                            settings.Settings.IgnoreTitle = IgnoreTitleOptions.IgnoreSourceTitle;
                        }
                        else if (value == 3)
                        {
                            settings.Settings.IgnoreTitle = IgnoreTitleOptions.IgnorePublishTitle;
                        }
                        else if (value == 4)
                        {
                            settings.Settings.IgnoreTitle = IgnoreTitleOptions.IgnoreMainAndSource;
                        }
                        else if (value == 5)
                        {
                            settings.Settings.IgnoreTitle = IgnoreTitleOptions.IgnoreMainAndPublish;
                        }
                        else if (value == 6)
                        {
                            settings.Settings.IgnoreTitle = IgnoreTitleOptions.IgnoreSourceAndPublish;
                        }
                        else
                        {
                            log.InfoFormat("Invalid -ignoretitle: parameter value {0}.", value);
                        }
                    }                                       
                }
                else
                {
                    Console.WriteLine(string.Format("Invalid option {0}.", param));
                }
            }
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

        private static void ShowHelp()
        {
            ConsoleWriter conWriter = new ConsoleWriter();
            conWriter.WriteLine("\nUsage: ");
            conWriter.WriteLine();
            conWriter.WriteLine("FB2EPUB <input_fb2_file_name> [-options]");
            conWriter.WriteLine("\t or");
            conWriter.WriteLine("FB2EPUB <input_fb2_file> <output_epub_file_name> [-options]");
            conWriter.WriteLine("\t or");
            conWriter.WriteLine("FB2EPUB <input_path> [-options]");
            conWriter.WriteLine("\t or");
            conWriter.WriteLine("FB2EPUB [-registration_options]");
            conWriter.WriteLine();
            conWriter.WriteLine();
            conWriter.WriteLine("Possible options are :");
            conWriter.WriteLine();
            conWriter.WriteLine("-t:[0/1/2/3/4/5] - transliterate title and chapter names , \n\t-t:0 disable transliteration (default) , \n\t-t:1 enable transliteration (used for devices that do not support cyrillic fonts like not russified Sony readers)\n\t 2 - transliterate file name, 3 - transliterate both file name and file metadata");
            conWriter.WriteLine("\n\t-t:4 (Kindle mode) transliterates book metadata without TOC, \n\t-t:5 transliterates book metadata and filename but not TOC");
            conWriter.WriteLine();
            conWriter.WriteLine("-o:<output_path> - set output path, the resulting file will be generated in folder prtovided (example: \"fb2epub mybook.fb2 /o:c:\\books\\\")");
            conWriter.WriteLine();
            conWriter.WriteLine("-r/-u registration options used to register/unregister right click shell extension");
            conWriter.WriteLine();
            conWriter.WriteLine("-f2i:[0/1] - enable (1) or disable (0) generation of FB2 information page in target EPUB");
            conWriter.WriteLine();
            conWriter.WriteLine("-s - searches for files to convert in subfolders");
            conWriter.WriteLine();
            conWriter.WriteLine("-m:[0/1/2/3] - when searching in folders search either *.fb (0) or  *.fb2;*.fb2.zip;*.fb2.rar (1 - default) or *.zip;*.rar;*.fb2 (2) or *.* (3) files");
            conWriter.WriteLine();
            conWriter.WriteLine("-deletesource - deletes the source file upon successful conversion");
            conWriter.WriteLine();
            conWriter.WriteLine("-seqadd:[0/1] - enable (1, default) or disable (0) adding sequences abbreviations to book title name");
            conWriter.WriteLine();
            conWriter.WriteLine("-seqformat:[format_string] - controls book descriiption title formating is seq number present");
            conWriter.WriteLine();
            conWriter.WriteLine("-nseqformat:[format_string] - controls book descriiption title formating in no seq. number");
            conWriter.WriteLine();
            conWriter.WriteLine("-nnseqformat:[format_string] - controls book descriiption title formating in no series");
            conWriter.WriteLine();
            conWriter.WriteLine("-aformat:[format_string] - controls author name formating in description");
            conWriter.WriteLine();
            conWriter.WriteLine("-svformat:[format_string] - controls author 'save as' name formating in description");
            conWriter.WriteLine();
            conWriter.WriteLine("-flat:[0/1] - enable () or disable (0, default) \"flat structure mode\" - mode when no subfolders created inside ePUB archive, workaround of bug on some readers");
            conWriter.WriteLine();
            conWriter.WriteLine("-emstyle:[0/1] - enable (1) or disable (0, default) embeding CCS styles into XHTML files instead referensing them");
            conWriter.WriteLine();
            conWriter.WriteLine("-xpgt:[0/1] - enable (1) or disable (0, default) embeding of Adobe XPGT template into resulting ePub File");
            conWriter.WriteLine();
            conWriter.WriteLine("-xpgtPath:[file_path] - Forces converter to use Adobe XPGT template file specified by this parameter instead of default XPGT template \"template.xpgt\" located in \"Template\" subfolder");
            conWriter.WriteLine();
            conWriter.WriteLine();
            conWriter.WriteLine("Press any key to exit...");
            Console.ReadKey();
        }

    }

}
