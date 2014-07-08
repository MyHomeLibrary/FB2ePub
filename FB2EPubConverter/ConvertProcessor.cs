using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Xml;
using System.Xml.Linq;
using Fb2ePubConverter;
using FB2EPubConverter.Interfaces;
using System.Runtime.InteropServices;
using Fb2epubSettings;
using System.Configuration;
using log4net.Config;
using FolderSettingsHelper;


namespace FB2EPubConverter
{
    [Guid("0FF011AD-18A5-4CF2-8AB1-011AA9AA2BDF"),ComVisible(true)]
    public class ConvertProcessor : IEPubConverterInterface
    {
        internal static class Logger
        {
            static Logger()
            {
                // in case we run from DLL
                if (Assembly.GetEntryAssembly() == null)
                {
                    // detect assembly path, the config file should be right near it
                    string path = Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location);
                    if (!string.IsNullOrEmpty(path))
                    {
                        // set the path to location were we wish to log to
                        log4net.GlobalContext.Properties["LogName"] = Path.Combine(FolderLocator.GetLocalAppDataFolder(), @"Lord_KiRon\");
                        // load .config file containing loggers
                        XmlConfigurator.Configure(new FileInfo(path + @"\FB2EPubConverter.dll.config"));
                    }
                }
            }

            // Create a logger for use in this class
            public static readonly log4net.ILog Log = log4net.LogManager.GetLogger(Assembly.GetExecutingAssembly().GetType());

        }

        private readonly ConvertProcessorSettings _processorSettings = new ConvertProcessorSettings();
        private  CancellationTokenSource _cts = new CancellationTokenSource();

        public ConvertProcessorSettings ProcessorSettings { get { return _processorSettings; } }


        public void PerformConvertOperation(string[] filesInMask, string outputFileName)
        {
            List<string> files = new List<string>();
            foreach (var mask in filesInMask)
            {
                files.Add(mask);
            }
            PerformConvertOperation(files, outputFileName);
        }

        /// <summary>
        /// Performs conversion of list of files 
        /// </summary>
        /// <param name="filesInMask">list of files to convert</param>
        /// <param name="outputFileName">output file name in case provided and single file is to be converted </param>
        public void PerformConvertOperation(List<string> filesInMask, string outputFileName)
        {
            int filesCount = filesInMask.Count;
            int successfullyConverted = 0;

            Logger.Log.InfoFormat("Using resources from: {0}",_processorSettings.Settings.ResourcesPath);
            ProgressUpdateWrapper progressReporter = new ProgressUpdateWrapper(_processorSettings.ProgressCallbacks);
            progressReporter.ConvertStarted(filesCount);
            Logger.Log.InfoFormat("Conversion process started at {0}",DateTime.Now.ToString("f"));

            ParallelOptions po = new ParallelOptions();
            po.CancellationToken = _cts.Token;
            po.MaxDegreeOfParallelism = Environment.ProcessorCount;
            try
            {
                Parallel.ForEach(filesInMask, po, (file) =>
                {
                    po.CancellationToken.ThrowIfCancellationRequested();
                    int Id;
                    lock (_processorSettings)
                    {
                        Id = successfullyConverted++;
                    }

                    Fb2EPubConverterEngine converter = new Fb2EPubConverterEngine()
                    {
                        Settings = _processorSettings.Settings
                    };
                    try
                    {
                        progressReporter.ProcessingStarted(file);
                        if (!converter.ConvertFile(file))
                        {
                            Logger.Log.Error(string.Format("Conversion of a file {0} failed", file));
                            progressReporter.SkippedDueError(file);
                            return;
                        }
                    }
                    catch (Exception ex)
                    {
                        Logger.Log.Error("Conversion error", ex);
                        progressReporter.SkippedDueError(file);
                        return;
                    }
                    string fileName = BuildNewFileName(file, outputFileName);
                    Logger.Log.InfoFormat("Saving {0}...", fileName);
                    progressReporter.ProcessingSaving(fileName);
                    SaveAndCleanUp(converter, fileName, file);
                    progressReporter.Processed(fileName);
                });
            }
            catch (OperationCanceledException e)
            {
                Logger.Log.Info("Operation was canceled");
                _cts = new CancellationTokenSource();
            }
            catch(Exception ex)
            {
                Logger.Log.Error("Error converting file, exception: ",ex);
            }
            finally
            {
                progressReporter.ConvertFinished(successfullyConverted);
                Logger.Log.Info("Conversion process finished");
            }

        }

        /// <summary>
        /// Builds a new file name for the output file (with .epub extension)
        /// </summary>
        /// <param name="file"></param>
        /// <param name="outputFileName"></param>
        /// <returns></returns>
        private string BuildNewFileName(string file, string outputFileName)
        {
            string fileName = outputFileName;
            if (!string.IsNullOrEmpty(outputFileName) && !_processorSettings.LookInSubFolders) // if output file name specified
            {
                if (Path.GetExtension(fileName).ToLower() != ".epub") // if output file name already has "ePub" extension - do nothing, if not - add it
                {
                    fileName = string.Format("{0}.epub", outputFileName);
                }
            }
            else // if no output file name specified , means "outputFileName" empty and as result "fileName" empty at this point
            {
                string fileNameWithoutExtension =
                    Path.GetFileNameWithoutExtension(file); // get input file name without extension 
                if (fileNameWithoutExtension == null) // if file starts with "." 
                {
                    fileNameWithoutExtension = "$tmp$"; //create temporary name for it
                }

                string fileLocation = fileLocation = _processorSettings.Settings.OutPutPath;

                // if output file path in settings not set we need to build it
                // creating from the input file path   
                if (string.IsNullOrEmpty(_processorSettings.Settings.OutPutPath))
                {
                    fileLocation = string.Format("{0}\\",
                                                    Path.GetDirectoryName(
                                                        Path.GetFullPath(file))); // 
                }
                // in case .fb2.zip etc. remove the "fb2" part (as .zip was removed earlier) 
                string fileNameWithExtension = GenerateProperExtension(fileNameWithoutExtension);
                // build new file location from path and name
                fileName = Path.Combine(fileLocation,
                                               fileNameWithExtension);
            }
            return fileName;
        }

        private string GenerateProperExtension(string fileNameWithoutExtension)
        {
            int position = fileNameWithoutExtension.LastIndexOf('.');
            if (position > 0)
            {
                string ext = fileNameWithoutExtension.Substring(position);
                if (String.Compare(ext.ToUpper(), ".FB2", StringComparison.Ordinal) == 0)
                {
                    fileNameWithoutExtension = fileNameWithoutExtension.Remove(position);
                }
            }
            return string.Format("{0}.epub",fileNameWithoutExtension);           
        }

        private void SaveAndCleanUp(Fb2EPubConverterEngine converter, string fileName, string inFileName)
        {
            try
            {
                converter.Save(fileName);
            }
            catch (Exception)
            {
                return;
            }
            if (_processorSettings.DeleteSource)
            {
                try
                {
                    File.Delete(inFileName);
                }
                catch (Exception ex)
                {
                    Logger.Log.ErrorFormat("Unable to delete file {0}, exception: {1}.", inFileName, ex);
                }
            }
        }


        /// <summary>
        /// Detects all files we need to process 
        /// </summary>
        /// <param name="fileParams">list of parameters containing command line pointing to files to process</param>
        /// <param name="filesInMask">outputs actual list of files that need to be processed</param>
        public void DetectFilesToProcess(List<string> fileParams, ref List<string> filesInMask)
        {
            filesInMask.Clear();
            bool folderExists = Directory.Exists(fileParams[0]);
            if (fileParams[0].Contains("*") || fileParams[0].Contains("?") || folderExists || fileParams[0].EndsWith("."))
            {
                if (folderExists && !fileParams[0].EndsWith(Path.DirectorySeparatorChar.ToString()) && !fileParams[0].EndsWith(Path.AltDirectorySeparatorChar.ToString()) && !fileParams[0].EndsWith(".")) //just to make sure we have a folder
                {
                    fileParams[0] += Path.DirectorySeparatorChar;
                }
                string fileName = Path.GetFileName(fileParams[0]);
                // if empty file name or '.' we list folder
                if (string.IsNullOrEmpty(fileName) || fileParams[0].EndsWith("."))
                {
                    // based on search mask options
                    fileName = GetFileMask(_processorSettings.SearchMask);
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
                                                                _processorSettings.LookInSubFolders
                                                                    ? SearchOption.AllDirectories
                                                                    : SearchOption.TopDirectoryOnly));
                    }
                    else
                    {
                        filesInMask.AddRange(Directory.GetFiles(Directory.GetCurrentDirectory(),
                                                                subMask,
                                                                _processorSettings.LookInSubFolders
                                                                    ? SearchOption.AllDirectories
                                                                    : SearchOption.TopDirectoryOnly));
                    }
                }
            }
            else
            {
                filesInMask.Add(fileParams[0]);
            }

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

        #region Implementation of IEPubConverterInterface

        public void ConvertPath(string inputPath, string outputFolder, IProgressUpdateInterface progress)
        {
            LoadSettings();
            ProcessorSettings.ProgressCallbacks = progress;
            ProcessorSettings.Settings.OutPutPath = outputFolder;
            List<string> fileParams = new List<string>();
            fileParams.Add(inputPath);
            ProcessorSettings.LookInSubFolders = true;
            ProcessorSettings.Settings.ResourcesPath = GetResourcesPath();
            List<string> filesInMask = new List<string>();
            DetectFilesToProcess(fileParams, ref filesInMask);
            PerformConvertOperation(filesInMask, null);
        }

        public static string GetResourcesPath()
        {
            return DefaultSettingsLocatorHelper.GetResourcesPath();
        }

        public void ConvertList(string[] files, string outputFolder, IProgressUpdateInterface progress)
        {
            LoadSettings();
            ProcessorSettings.Settings.OutPutPath = outputFolder;
            ProcessorSettings.ProgressCallbacks = progress;
            ProcessorSettings.Settings.ResourcesPath = GetResourcesPath();
            PerformConvertOperation(files, null);

        }

        public void ConvertSingleFile(string inputPath, string outputName, IProgressUpdateInterface progress)
        {
            LoadSettings();
            ProcessorSettings.ProgressCallbacks = progress;
            ProcessorSettings.Settings.ResourcesPath = GetResourcesPath(); 
            List<string> filesInMask = new List<string>();
            filesInMask.Add(inputPath);
            PerformConvertOperation(filesInMask, outputName);

        }

        public void AbortConversion()
        {
            _cts.Cancel();
        }

        bool IEPubConverterInterface.ShowSettingsDialog(IntPtr parent)
        {
            IWin32Window w = Control.FromHandle(parent);
            return ShowSettingsDialog(w);
        }

        public void ConvertXml(XDocument doc, string outFileName, IProgressUpdateInterface progress)
        {
            LoadSettings();
            _processorSettings.DeleteSource = false;
            int successfullyConverted = 0;

            ProgressUpdateWrapper progressReporter = new ProgressUpdateWrapper(_processorSettings.ProgressCallbacks);
            progressReporter.ConvertStarted(1);


            Fb2EPubConverterEngine converter = new Fb2EPubConverterEngine()
            {
                Settings = _processorSettings.Settings
            };

            try
            {
                progressReporter.ProcessingStarted(outFileName);
                converter.ConvertXml(doc);
            }
            catch (Exception ex)
            {
                Logger.Log.Error(ex);
                return;
            }
            progressReporter.ProcessingSaving(outFileName);
            SaveAndCleanUp(converter, outFileName, "");
            progressReporter.Processed(outFileName);
            progressReporter.ConvertFinished(successfullyConverted);
        }

        #endregion

        public static bool ShowSettingsDialog(IWin32Window parent)
        {
            ConverterSettingsForm settingsForm = new ConverterSettingsForm();
            settingsForm.TopLevel = true;
            return (settingsForm.ShowDialog(parent) == DialogResult.OK);

        }

        /// <summary>
        /// Load settings from one of the default settings locations or file specified
        /// </summary>
        public void LoadSettings()
        {
            ConverterSettingsFile file = new ConverterSettingsFile();
            if (string.IsNullOrEmpty(_processorSettings.SettingsFileToUse) || !File.Exists(ProcessorSettings.SettingsFileToUse))
            {
                string filePath = null;
                if ( !string.IsNullOrEmpty(_processorSettings.SettingsFileToUse) )
                {
                    Logger.Log.ErrorFormat("LoadSettings - the specified settings file \"{0}\" not found, going to use standard settings file",_processorSettings.SettingsFileToUse);
                }
                DefaultSettingsLocatorHelper.EnsureDefaultSettingsFilePresent(out filePath, file.Settings);
                ProcessorSettings.SettingsFileToUse = filePath;
            }
            try
            {
                Logger.Log.InfoFormat("Loading settings from {0}", _processorSettings.SettingsFileToUse);
                file.Load(ProcessorSettings.SettingsFileToUse);
            }
            catch (Exception ex)
            {
                Logger.Log.ErrorFormat("LoadSettings - unable to load file {0} , exception: {1}", _processorSettings.SettingsFileToUse, ex.ToString());
            }
            try
            {
                _processorSettings.Settings.CopyFrom(file.Settings);
            }
            catch (Exception ex)
            {
                Logger.Log.ErrorFormat("LoadSettings - unable to copy settings , exception: {0}",  ex.ToString());
            }
        }
    }
}
