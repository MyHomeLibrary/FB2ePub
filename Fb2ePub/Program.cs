using System;
using System.Collections.Generic;
using System.Reflection;
using System.IO;
using ConverterContracts;
using FB2EPubConverter;
using log4net;
using ConverterContracts.Settings;
using Fb2epubSettings;
using FolderSettingsHelper;
using TranslitRuContracts;


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
        
        private static ILog _log;


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
                    Console.WriteLine(@"Input file name missing. Exiting.");
                    _log.Error("Input file name missing.");
                    if (_log.IsInfoEnabled) _log.Info("Application [FB2EPUB] End");
                    return;
                }
                var processor = new ConvertProcessor();
                PreProcessParameters(options, processor.ProcessorSettings);
                ProcessSettings(processor);
                ProcessParameters(options, processor.ProcessorSettings);
                Console.WriteLine(@"Loading {0}...", fileParams[0]);
                var filesInMask = new List<string>();
                processor.DetectFilesToProcess(fileParams, ref filesInMask);
                string outputFileName = (fileParams.Count > 1)?fileParams[1]:null;
                processor.PerformConvertOperation(filesInMask,  outputFileName);
                Console.WriteLine(@"Done.");
            }
            else
            {
                ShowHelp();
            }
            _log.Debug("Application [FB2EPUB] End");
        }

        /// <summary>
        /// called before loading settings to process parameters that affect loading
        /// </summary>
        /// <param name="options"></param>
        /// <param name="convertProcessorSettings"></param>
        private static void PreProcessParameters(List<string> options, IConvertProcessorSettings convertProcessorSettings)
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
            Console.WriteLine(@"FB2 to EPUB command line converter by Lord KiRon");
            string logPath = Path.Combine(FolderLocator.GetLocalAppDataFolder(), @"Lord_KiRon\fb2epub.log");
            SetNewLogPath(logPath);
            _log = LogManager.GetLogger(Assembly.GetExecutingAssembly().GetType());
            // Log an info level message
            _log.Debug("Application [FB2EPUB] Start");
        }

        private static void SetNewLogPath(string pathName)
        {
            string logPath;
            if (!Directory.Exists(pathName))
            {
                if (Path.HasExtension(pathName))
                {
                    logPath = pathName;
                }
                else
                {
                    Directory.CreateDirectory(pathName);
                    logPath = Path.Combine(pathName, "fb2epub.log");
                }
            }
            else // in case folder passed
            {
                logPath = Path.Combine(pathName, "fb2epub.log");
            }
            GlobalContext.Properties["LogFileName"] = logPath;
            log4net.Config.XmlConfigurator.Configure();
            Console.WriteLine(@"Logging to: {0}\", pathName);
            Console.WriteLine();
        }


        private static void ProcessSettings(ConvertProcessor processor)
        {
            processor.LoadSettings();
            processor.ProcessorSettings.Settings.ResourcesPath = ConvertProcessor.GetResourcesPath();
        }

        private static void ProcessParameters(List<string> options, IConvertProcessorSettings settings)
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
                            settings.Settings.CommonSettings.TransliterateToc = false;
                            settings.Settings.ConversionSettings.TransliterationSettings.CopyFrom(
                                new TransliterationSettingsImp {Mode = TranslitModeEnum.None});
                            settings.Settings.ConversionSettings.TransliterateFileName = false;
                        }
                        else if (value == 1)
                        {
                            settings.Settings.CommonSettings.TransliterateToc = true;
                            settings.Settings.ConversionSettings.TransliterationSettings.CopyFrom(
                                new TransliterationSettingsImp { Mode = TranslitModeEnum.None });
                            settings.Settings.ConversionSettings.TransliterateFileName = false;
                        }
                        else if (value == 2)
                        {
                            settings.Settings.CommonSettings.TransliterateToc = false;
                            settings.Settings.ConversionSettings.TransliterationSettings.CopyFrom(
                                new TransliterationSettingsImp { Mode = TranslitModeEnum.None });
                            settings.Settings.ConversionSettings.TransliterateFileName = true;
                        }
                        else if (value == 3)
                        {
                            settings.Settings.CommonSettings.TransliterateToc = true;
                            settings.Settings.ConversionSettings.TransliterationSettings.CopyFrom(
                                new TransliterationSettingsImp { Mode = TranslitModeEnum.TranslitRu});
                            settings.Settings.ConversionSettings.TransliterateFileName = true;
                        }
                        else if (value == 4)
                        {
                            settings.Settings.CommonSettings.TransliterateToc = false;
                            settings.Settings.ConversionSettings.TransliterationSettings.CopyFrom(
                                new TransliterationSettingsImp { Mode = TranslitModeEnum.TranslitRu });
                            settings.Settings.ConversionSettings.TransliterateFileName = false;
                        }
                        else if (value == 5)
                        {
                            settings.Settings.CommonSettings.TransliterateToc = false;
                            settings.Settings.ConversionSettings.TransliterationSettings.CopyFrom(
                                new TransliterationSettingsImp { Mode = TranslitModeEnum.TranslitRu });
                            settings.Settings.ConversionSettings.TransliterateFileName = true;
                        }
                        else
                        {
                           _log.InfoFormat("Invalid -t: parameter value {0}.",commandValue);
                        }
                    }
                    else
                    {
                        _log.InfoFormat("Invalid -t: parameter value {0}.", commandValue);
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
                            settings.Settings.ConversionSettings.Fb2Info = false;
                        }
                        else if (value == 1)
                        {
                            settings.Settings.ConversionSettings.Fb2Info = true;
                        }
                        else
                        {
                            _log.InfoFormat("Invalid -f2i: parameter value {0}.", commandValue);
                        }
                    }
                    else
                    {
                        _log.InfoFormat("Invalid -f2i: parameter value {0}.", commandValue);
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
                            _log.InfoFormat("Invalid -m: parameter value {0}.", value);
                        }
                    }
                    else
                    {
                        _log.InfoFormat("Invalid -m: parameter value {0}.", commandValue);
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
                            settings.Settings.FB2ImportSettings.FixMode = FixOptions.DoNotFix;
                        }
                        else if (value == 1)
                        {
                            settings.Settings.FB2ImportSettings.FixMode = FixOptions.MinimalFix;
                        }
                        else if (value == 2)
                        {
                            settings.Settings.FB2ImportSettings.FixMode = FixOptions.UseFb2Fix;
                        }
                        else if (value == 3)
                        {
                            settings.Settings.FB2ImportSettings.FixMode = FixOptions.Fb2FixAlways;
                        }
                        else
                        {
                            _log.InfoFormat("Invalid -fix: parameter value {0}.", value);
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
                            settings.Settings.ConversionSettings.AddSeqToTitle = false;
                        }
                        else if (value == 1)
                        {
                            settings.Settings.ConversionSettings.AddSeqToTitle = true;
                        }
                        else
                        {
                            _log.InfoFormat("Invalid -seqadd: parameter value {0}.", value);
                        }
                    }                    
                }
                else if (command.StartsWith("seqformat:"))
                {
                    string commandValue = command.Substring(10);
                    if (!string.IsNullOrEmpty(commandValue))
                        settings.Settings.ConversionSettings.SequenceFormat = commandValue;
                    else
                        _log.InfoFormat("Invalid -seqformat: parameter value is empty.");
                }
                else if (command.StartsWith("nseqformat:"))
                {
                    string commandValue = command.Substring(10);
                    if (!string.IsNullOrEmpty(commandValue))
                        settings.Settings.ConversionSettings.NoSequenceFormat = commandValue;
                    else
                        _log.InfoFormat("Invalid -nseqformat: parameter value is empty.");
                }
                else if (command.StartsWith("nnseqformat:"))
                {
                    string commandValue = command.Substring(10);
                    if (!string.IsNullOrEmpty(commandValue))
                        settings.Settings.ConversionSettings.NoSeriesFormat = commandValue;
                    else
                        _log.InfoFormat("Invalid -nnseqformat: parameter value is empty.");
                }
                else if (command.StartsWith("aformat:"))
                {
                    string commandValue = command.Substring(10);
                    if (!string.IsNullOrEmpty(commandValue))
                        settings.Settings.ConversionSettings.AuthorFormat = commandValue;
                    else
                        _log.InfoFormat("Invalid -aformat: parameter value is empty.");
                }
                else if (command.StartsWith("svformat:"))
                {
                    string commandValue = command.Substring(10);
                    if (!string.IsNullOrEmpty(commandValue))
                        settings.Settings.ConversionSettings.FileAsFormat = commandValue;
                    else
                        _log.InfoFormat("Invalid -svformat: parameter value is empty.");
                }
                else if (command.StartsWith("flat:"))
                {
                    string commandValue = command.Substring(5);
                    int value;
                    if (int.TryParse(commandValue, out value))
                    {
                        if (value == 0)
                        {
                            settings.Settings.CommonSettings.FlatStructure = false;
                        }
                        else if (value == 1)
                        {
                            settings.Settings.CommonSettings.FlatStructure = true;
                        }
                        else
                        {
                            _log.InfoFormat("Invalid -flat: parameter value {0}.", value);
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
                            settings.Settings.CommonSettings.EmbedStyles = false;
                        }
                        else if (value == 1)
                        {
                            settings.Settings.CommonSettings.EmbedStyles = true;
                        }
                        else
                        {
                            _log.InfoFormat("Invalid -emstyles: parameter value {0}.", value);
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
                            settings.Settings.FB2ImportSettings.ConvertAlphaPng = false;
                        }
                        else if (value == 1)
                        {
                            settings.Settings.FB2ImportSettings.ConvertAlphaPng = true;
                        }
                        else
                        {
                            _log.InfoFormat("Invalid -apng: parameter value {0}.", value);
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
                            settings.Settings.CommonSettings.CapitalDrop = false;
                        }
                        else if (value == 1)
                        {
                            settings.Settings.CommonSettings.CapitalDrop = true;
                        }
                        else
                        {
                            _log.InfoFormat("Invalid -cap: parameter value {0}.", value);
                        }
                    }
                }
                else if (command.StartsWith("noabout"))
                {
                    settings.Settings.ConversionSettings.SkipAboutPage = true;
                }
                else if (command.StartsWith("xpgt:"))
                {
                    string commandValue = command.Substring(5);
                    int value;
                    if (int.TryParse(commandValue, out value))
                    {
                        if (value == 0)
                        {
                            settings.Settings.V2Settings.EnableAdobeTemplate = false;
                        }
                        else if (value == 1)
                        {
                            settings.Settings.V2Settings.EnableAdobeTemplate = true;
                        }
                        else
                        {
                            _log.InfoFormat("Invalid -xpgt: parameter value {0}.", value);
                        }
                    }                   
                }
                else if (command.StartsWith("xpgtpath:"))
                {
                    settings.Settings.V2Settings.AdobeTemplatePath = command.Substring(9);
                }
                else if (command.StartsWith("ignoretitle:"))
                {
                    string commandValue = command.Substring(12);
                    int value;
                    if (int.TryParse(commandValue, out value))
                    {
                        if (value == 0)
                        {
                            settings.Settings.ConversionSettings.IgnoreTitle = IgnoreInfoSourceOptions.IgnoreNothing;
                        }
                        else if (value == 1)
                        {
                            settings.Settings.ConversionSettings.IgnoreTitle = IgnoreInfoSourceOptions.IgnoreMainTitle;
                        }
                        else if (value == 2)
                        {
                            settings.Settings.ConversionSettings.IgnoreTitle = IgnoreInfoSourceOptions.IgnoreSourceTitle;
                        }
                        else if (value == 3)
                        {
                            settings.Settings.ConversionSettings.IgnoreTitle = IgnoreInfoSourceOptions.IgnorePublishTitle;
                        }
                        else if (value == 4)
                        {
                            settings.Settings.ConversionSettings.IgnoreTitle = IgnoreInfoSourceOptions.IgnoreMainAndSource;
                        }
                        else if (value == 5)
                        {
                            settings.Settings.ConversionSettings.IgnoreTitle = IgnoreInfoSourceOptions.IgnoreMainAndPublish;
                        }
                        else if (value == 6)
                        {
                            settings.Settings.ConversionSettings.IgnoreTitle = IgnoreInfoSourceOptions.IgnoreSourceAndPublish;
                        }
                        else
                        {
                            _log.InfoFormat("Invalid -ignoretitle: parameter value {0}.", value);
                        }
                    }                                       
                }
                else if (command.StartsWith("calibremeta:"))
                {
                    string commandValue = command.Substring(12);
                    int value;
                    if (int.TryParse(commandValue, out value))
                    {
                        if (value == 0)
                        {
                            settings.Settings.V2Settings.AddCalibreMetadata = false;
                        }
                        else if (value == 1)
                        {
                            settings.Settings.V2Settings.AddCalibreMetadata = true;
                        }
                        else
                        {
                            _log.InfoFormat("Invalid -calibremeta: parameter value {0}.", value);
                        }
                    }
                }
                else if (command.StartsWith("log:"))
                {
                    string commandValue = command.Substring(4);
                    SetNewLogPath(commandValue);  
                }
                else
                {
                    Console.WriteLine(@"Invalid option {0}.", param);
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
            conWriter.WriteLine("-log:<log_file_path> - redirects logging output to the file specified");
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
