using System;
using System.Collections.Generic;
using System.Configuration;
using System.Reflection;
using System.Threading.Tasks;
using Fb2ePubConverter;
using System.IO;
using FB2EPubConverter;
using log4net;
using ProcessStartInfo=System.Diagnostics.ProcessStartInfo;
using System.Diagnostics;
using System.Windows.Forms;
using Fb2epubSettings;
using FolderSettingsHelper;


// Configure log4net using the .config file
[assembly: log4net.Config.XmlConfigurator(Watch = true)]
// This will cause log4net to look for a configuration file
// called FB2EPUB.exe.config in the application base
// directory (i.e. the directory containing FB2EPUB.exe)


namespace Fb2ePub
{
    class Program
    {     
        enum PathSearchOptions
        {
            Fb2Only,
            Fb2WithArchives,
            WithAllArchives,
            All,
        }
        // Create a logger for use in this class
        
        private static ILog log;
        private static bool lookInSubFolders = false;
        private static PathSearchOptions _searchMask = PathSearchOptions.Fb2WithArchives;
        private static bool deleteSource = false;
        private static bool abortDeletion = false;
        private static string _outPath;

        const string Registrator2Run = "registerfb2epub.exe";

        static void Main(string[] args)
        {
            string logPath = Path.Combine(FolderLocator.GetLocalAppDataFolder(),"Lord KiRon\\");
            GlobalContext.Properties["LogName"] = logPath;
            log = LogManager.GetLogger(Assembly.GetExecutingAssembly().GetType());
            // Log an info level message
            log.Debug("Application [FB2EPUB] Start");
            Console.WriteLine("FB2 to EPUB command line converter by Lord KiRon");
            Console.WriteLine(string.Format("Logging to: {0}\\",GlobalContext.Properties["LogName"]));
                        Console.WriteLine();
            Configuration config =
            ConfigurationManager.OpenExeConfiguration(
            ConfigurationUserLevel.PerUserRoamingAndLocal);
            ConverterSettings settings = new ConverterSettings();
            log.InfoFormat("Local user config path: {0}", config.FilePath);
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
                        if ((options[0].ToLower() == "/r") || (options[0].ToLower() == "-r"))
                        {
                            RegisterShellExtension(false);
                            return;
                        }
                        if ((options[0].ToLower() == "/rall") || (options[0].ToLower() == "-rall"))
                        {
                            RegisterShellExtension(true);
                            return;
                        }
                        if ((options[0].ToLower() == "/u") || options[0].ToLower() == "-u")
                        {
                            UnregisterShellExtension();
                            return;
                        }
                        if ((options[0].ToLower() == "/settings") || options[0].ToLower() == "-settings")
                        {
                            ConverterSettingsForm settingsForm = new ConverterSettingsForm();
                            settingsForm.TopLevel = true;
                            settingsForm.ShowDialog();
                            return;
                        }
                    }
                    Console.WriteLine("Input file name missing. Exiting.");
                    log.Error("Input file name missing.");
                    if (log.IsInfoEnabled) log.Info("Application [FB2EPUB] End");
                    return;
                }
                ProcessSettings(settings);
                ProcessParameters(options, settings);
                Console.WriteLine(string.Format("Loading {0}...", fileParams[0]));
                List<string> filesInMask = new List<string>();
                bool folderExists = Directory.Exists(fileParams[0]);
                if (fileParams[0].Contains("*") || fileParams[0].Contains("?") || folderExists || fileParams[0].EndsWith("."))
                {
                    if (folderExists && !fileParams[0].EndsWith(Path.DirectorySeparatorChar.ToString()) && !fileParams[0].EndsWith(Path.AltDirectorySeparatorChar.ToString()) && !fileParams[0].EndsWith(".")) //just to make sure we have a folder
                    {
                        fileParams[0] += Path.DirectorySeparatorChar;
                    }
                    string fileName = Path.GetFileName(fileParams[0]);
                    // if empty file name or . we list folder
                    if (string.IsNullOrEmpty(fileName) || fileParams[0].EndsWith("."))
                    {
                        // based on search mask options
                        fileName = GetFileMask(_searchMask);
                        if (fileParams[0].EndsWith("."))
                        {
                            fileParams[0] = fileParams[0].Remove(fileParams[0].Length - 1);
                        }
                    }
                    foreach (var subMask in fileName.Split(','))
                    {
                        if (fileParams[0].Contains(Path.DirectorySeparatorChar.ToString()) ||
                            fileParams[0].Contains(Path.AltDirectorySeparatorChar.ToString()))
                            // if does not have dir. separators then it's current folder
                        {
                            filesInMask.AddRange(Directory.GetFiles(Path.GetDirectoryName(fileParams[0]),
                                                                    subMask,
                                                                    lookInSubFolders
                                                                        ? SearchOption.AllDirectories
                                                                        : SearchOption.TopDirectoryOnly));
                        }
                        else
                        {
                            filesInMask.AddRange(Directory.GetFiles(Directory.GetCurrentDirectory(),
                                                                    subMask,
                                                                    lookInSubFolders
                                                                        ? SearchOption.AllDirectories
                                                                        : SearchOption.TopDirectoryOnly));
                        }
                    }
                }
                else 
                {
                    filesInMask.Add(fileParams[0]);
                }

                string resourcesPath = Path.GetDirectoryName(Assembly.GetEntryAssembly().Location);
                Parallel.ForEach(filesInMask, (file) =>
                                              //foreach (var file in filesInMask)
                                                  {
                                                      Fb2EPubConverterEngine converter = new Fb2EPubConverterEngine() { ResourcesPath =  resourcesPath,OutPutPath = _outPath,
                                                      Settings = settings};
                                                      converter.Fonts = Fb2epubSettings.Fb2Epub.Default.Fonts;

                                                      abortDeletion = false;
                                                      try
                                                      {
                                                          if (!converter.ConvertFile(file))
                                                          {
                                                              abortDeletion = true;
                                                              //continue;
                                                              return;
                                                          }
                                                      }
                                                      catch (Exception ex)
                                                      {
                                                          log.Error(ex);
                                                          //continue;
                                                          return;
                                                      }
                                                      if (fileParams.Count > 1 && !lookInSubFolders)
                                                      {
                                                          string fileName = fileParams[1].ToLower();
                                                          if (Path.GetExtension(fileName) != ".epub")
                                                          {
                                                              fileName = string.Format("{0}.epub", fileName);
                                                          }
                                                          Console.WriteLine(string.Format("Saving {0}...", fileName));
                                                          Convert(converter, fileName, file);
                                                      }
                                                      else
                                                      {
                                                          string fileNameWithoutExtension =
                                                              Path.GetFileNameWithoutExtension(file);
                                                          if (fileNameWithoutExtension == null)
                                                          {
                                                              fileNameWithoutExtension = "$tmp$";
                                                          }
                                                          string fileLocation = string.Format("{0}\\",
                                                                                              Path.GetDirectoryName(
                                                                                                  Path.GetFullPath(file)));
                                                          if (!string.IsNullOrEmpty(converter.OutPutPath))
                                                          {
                                                              fileLocation = converter.OutPutPath;
                                                          }
                                                          // in case fb2.zip remove the "fb2" part
                                                          if (fileNameWithoutExtension.ToLower().EndsWith(".fb2"))
                                                          {
                                                              fileNameWithoutExtension =
                                                                  Path.GetFileNameWithoutExtension(
                                                                      fileNameWithoutExtension);
                                                          }
                                                          string fileName = string.Format("{2}{0}.{1}",
                                                                                          fileNameWithoutExtension,
                                                                                          "epub", fileLocation);
                                                          Console.WriteLine(string.Format("Saving {0}...", fileName));
                                                          Convert(converter, fileName, file);
                                                      }
                                                  });
                Console.WriteLine("Done.");
            }
            else
            {
                ShowHelp();
            }
            log.Debug("Application [FB2EPUB] End");
        }

        /// <summary>
        /// Return default file mask based on options
        /// </summary>
        /// <returns></returns>
        private static string GetFileMask(PathSearchOptions searchMask)
        {
            string fileName = "*.*";
            if (searchMask == PathSearchOptions.Fb2Only)
            {
                fileName = "*.fb2";
            }
            else if (searchMask == PathSearchOptions.Fb2WithArchives)
            {
                fileName = "*.fb2,*.fb2.zip,*.fb2.rar";
            }
            else if (searchMask == PathSearchOptions.WithAllArchives)
            {
                fileName = "*.fb2,*.zip,*.rar";
            }
            return fileName;
        }

        private static void Convert(Fb2EPubConverterEngine converter, string fileName,string inFileName)
        {
            try
            {
                converter.Save(fileName);
            }
            catch (Exception)
            {
                return;
            }
            if (deleteSource && !abortDeletion)
            {
                try
                {
                    File.Delete(inFileName);
                }
                catch (Exception ex)
                {
                    log.ErrorFormat("Unable to delete file {0}, exception: {1}.", inFileName,ex.ToString());
                }
            }
        }

        private static void UnregisterShellExtension()
        {
            if(!File.Exists(Registrator2Run) )
            {
                log.ErrorFormat("Unable to locate {0}, please ensure that  this file located in the running folder",Registrator2Run);
                return;   
            }
            if ( RunCommand(Registrator2Run, "/u") != 0 )
            {
                log.Error("Unregistration failed");
                return;
            }
            log.Info("Unregistration succeeded");
        }

        private static void RegisterShellExtension(bool all)
        {
            if (!File.Exists(Registrator2Run))
            {
                log.ErrorFormat("Unable to locate {0}, please ensure that  this file located in the running folder", Registrator2Run);
                return;
            }
            string param = "/r";
            if (all)
            {
                param = "/rall";
            }
            if (RunCommand(Registrator2Run, param) != 0)
            {
                log.Error("Registration failed");
                return;
            }
            log.Info("Registration succeeded");
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

        private static void ProcessSettings(ConverterSettings settings)
        {
            settings.Transliterate = Fb2epubSettings.Fb2Epub.Default.Transliterate;
            settings.TransliterateFileName = Fb2epubSettings.Fb2Epub.Default.TransliterateFileName;
            settings.TransliterateToc = Fb2epubSettings.Fb2Epub.Default.TransliterateTOC;
            settings.Fb2Info = Fb2epubSettings.Fb2Epub.Default.FB2Info;
            settings.FixMode = (FixOptions)Fb2epubSettings.Fb2Epub.Default.FixMode;
            settings.AddSeqToTitle = Fb2epubSettings.Fb2Epub.Default.AddSequences;
            settings.SequenceFormat = Fb2epubSettings.Fb2Epub.Default.SequenceFormat;
            settings.NoSequenceFormat = Fb2epubSettings.Fb2Epub.Default.NoSequenceFormat;
            settings.NoSeriesFormat = Fb2epubSettings.Fb2Epub.Default.NoSeriesFormat;
            settings.Flat = Fb2epubSettings.Fb2Epub.Default.FlatStructure;
            settings.ConvertAlphaPng = Fb2epubSettings.Fb2Epub.Default.ConvertAlphaPNG;
            settings.EmbedStyles = Fb2epubSettings.Fb2Epub.Default.EmbedStyles;
            settings.AuthorFormat = Fb2epubSettings.Fb2Epub.Default.AuthorFormat;
            settings.FileAsFormat = Fb2epubSettings.Fb2Epub.Default.FileAsFormat;
            settings.CapitalDrop = Fb2epubSettings.Fb2Epub.Default.Capitalize;
            settings.SkipAboutPage = Fb2epubSettings.Fb2Epub.Default.SkipAboutPage;
            settings.EnableAdobeTemplate = Fb2epubSettings.Fb2Epub.Default.UseAdobeTemplate;
            settings.AdobeTemplatePath = Fb2epubSettings.Fb2Epub.Default.AdobeTemplatePath;
            settings.DecorateFontNames = Fb2epubSettings.Fb2Epub.Default.DecorateFontNames;
            settings.IgnoreTitle = (IgnoreTitleOptions) Fb2epubSettings.Fb2Epub.Default.IgnoreTitle;
            //Fb2Epub.Default.Save();
        }

        private static void ProcessParameters(List<string> options, ConverterSettings settings)
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
                            settings.Transliterate = false;
                            settings.TransliterateFileName = false;
                            settings.TransliterateToc = false;
                        }
                        else if (value == 1)
                        {
                            settings.Transliterate = true;
                            settings.TransliterateToc = true;
                            settings.TransliterateFileName = false;
                        }
                        else if (value == 2)
                        {
                            settings.Transliterate = false;
                            settings.TransliterateToc = false;
                            settings.TransliterateFileName = true;
                        }
                        else if (value == 3)
                        {
                            settings.Transliterate = true;
                            settings.TransliterateToc = true;
                            settings.TransliterateFileName = true;
                        }
                        else if (value == 4)
                        {
                            settings.Transliterate = true;
                            settings.TransliterateToc = false;
                            settings.TransliterateFileName = false;
                        }
                        else if (value == 5)
                        {
                            settings.Transliterate = true;
                            settings.TransliterateToc = false;
                            settings.TransliterateFileName = true;
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
                        _outPath = command.Substring(2, command.Length - 3);
                    }
                    else
                    {
                        _outPath = command.Substring(2);
                    }
                    if (!_outPath.EndsWith("\\") && !_outPath.EndsWith("/"))
                    {
                        _outPath += "\\";
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
                            settings.Fb2Info = false;
                        }
                        else if (value == 1)
                        {
                            settings.Fb2Info = true;
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
                    lookInSubFolders = true;
                }
                else if (command.StartsWith("m:")) // mask when searching
                {
                    string commandValue = command.Substring(2);
                    int value;
                    if (int.TryParse(commandValue, out value))
                    {
                        if (value == 0)
                        {
                            _searchMask = PathSearchOptions.Fb2Only;
                        }
                        else if (value == 1)
                        {
                            _searchMask = PathSearchOptions.Fb2WithArchives;
                        }
                        else if (value == 2)
                        {
                            _searchMask = PathSearchOptions.WithAllArchives;
                        }
                        else if (value == 3)
                        {
                            _searchMask = PathSearchOptions.All;
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
                    deleteSource = true;
                }
                else if (command.StartsWith("fix:"))
                {
                    string commandValue = command.Substring(4);
                    int value;
                    if (int.TryParse(commandValue, out value))
                    {
                        if (value == 0)
                        {
                            settings.FixMode = FixOptions.DoNotFix;
                        }
                        else if (value == 1)
                        {
                            settings.FixMode = FixOptions.MinimalFix;
                        }
                        else if (value == 2)
                        {
                            settings.FixMode = FixOptions.UseFb2Fix;
                        }
                        else if (value == 3)
                        {
                            settings.FixMode = FixOptions.Fb2FixAlways;
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
                            settings.AddSeqToTitle = false;
                        }
                        else if (value == 1)
                        {
                            settings.AddSeqToTitle = true;
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
                        settings.SequenceFormat = commandValue;
                    else
                        log.InfoFormat("Invalid -seqformat: parameter value is empty.");
                }
                else if (command.StartsWith("nseqformat:"))
                {
                    string commandValue = command.Substring(10);
                    if (!string.IsNullOrEmpty(commandValue))
                        settings.NoSequenceFormat = commandValue;
                    else
                        log.InfoFormat("Invalid -nseqformat: parameter value is empty.");
                }
                else if (command.StartsWith("nnseqformat:"))
                {
                    string commandValue = command.Substring(10);
                    if (!string.IsNullOrEmpty(commandValue))
                        settings.NoSeriesFormat= commandValue;
                    else
                        log.InfoFormat("Invalid -nnseqformat: parameter value is empty.");
                }
                else if (command.StartsWith("aformat:"))
                {
                    string commandValue = command.Substring(10);
                    if (!string.IsNullOrEmpty(commandValue))
                        settings.AuthorFormat = commandValue;
                    else
                        log.InfoFormat("Invalid -aformat: parameter value is empty.");
                }
                else if (command.StartsWith("svformat:"))
                {
                    string commandValue = command.Substring(10);
                    if (!string.IsNullOrEmpty(commandValue))
                        settings.FileAsFormat = commandValue;
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
                            settings.Flat = false;
                        }
                        else if (value == 1)
                        {
                            settings.Flat = true;
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
                            settings.EmbedStyles = false;
                        }
                        else if (value == 1)
                        {
                            settings.EmbedStyles = true;
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
                            settings.ConvertAlphaPng = false;
                        }
                        else if (value == 1)
                        {
                            settings.ConvertAlphaPng = true;
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
                            settings.CapitalDrop = false;
                        }
                        else if (value == 1)
                        {
                            settings.CapitalDrop = true;
                        }
                        else
                        {
                            log.InfoFormat("Invalid -cap: parameter value {0}.", value);
                        }
                    }
                }
                else if (command.StartsWith("noabout"))
                {
                    settings.SkipAboutPage = true;
                }
                else if (command.StartsWith("xpgt:"))
                {
                    string commandValue = command.Substring(5);
                    int value;
                    if (int.TryParse(commandValue, out value))
                    {
                        if (value == 0)
                        {
                            settings.EnableAdobeTemplate = false;
                        }
                        else if (value == 1)
                        {
                            settings.EnableAdobeTemplate = true;
                        }
                        else
                        {
                            log.InfoFormat("Invalid -xpgt: parameter value {0}.", value);
                        }
                    }                   
                }
                else if (command.StartsWith("xpgtpath:"))
                {
                    settings.AdobeTemplatePath =  command.Substring(9);
                }
                else if (command.StartsWith("ignoretitle:"))
                {
                    string commandValue = command.Substring(12);
                    int value;
                    if (int.TryParse(commandValue, out value))
                    {
                        if (value == 0)
                        {
                            settings.IgnoreTitle = IgnoreTitleOptions.IgnoreNothing;
                        }
                        else if (value == 1)
                        {
                            settings.IgnoreTitle = IgnoreTitleOptions.IgnoreMainTitle;
                        }
                        else if (value == 2)
                        {
                            settings.IgnoreTitle = IgnoreTitleOptions.IgnoreSourceTitle;
                        }
                        else if (value == 3)
                        {
                            settings.IgnoreTitle = IgnoreTitleOptions.IgnorePublishTitle;
                        }
                        else if (value == 4)
                        {
                            settings.IgnoreTitle = IgnoreTitleOptions.IgnoreMainAndSource;
                        }
                        else if (value == 5)
                        {
                            settings.IgnoreTitle = IgnoreTitleOptions.IgnoreMainAndPublish;
                        }
                        else if (value == 6)
                        {
                            settings.IgnoreTitle = IgnoreTitleOptions.IgnoreSourceAndPublish;
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
