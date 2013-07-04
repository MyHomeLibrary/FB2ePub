using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Xml;
using System.Xml.Linq;
using Fb2ePubConverter;
using FB2EPubConverter.Interfaces;
using System.Runtime.InteropServices;
using Fb2epubSettings;
using System.Configuration;
using Microsoft.Win32;

namespace FB2EPubConverter
{
    [Guid("0FF011AD-18A5-4CF2-8AB1-011AA9AA2BDF"),ComVisible(true)]
    public class ConvertProcessor : IEPubConverterInterface
    {
        internal static class Logger
        {
            // Create a logger for use in this class
            public static readonly log4net.ILog Log = log4net.LogManager.GetLogger(Assembly.GetExecutingAssembly().GetType());

        }

        private readonly ConvertProcessorSettings _processorSettings = new ConvertProcessorSettings();
        //private string _settingsFileName = @"c:\settings.xml";

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

            if (_processorSettings.ProgressCallbacks != null)
            {
                _processorSettings.ProgressCallbacks.ConvertStarted(filesCount);
            }

            try
            {
                Parallel.ForEach(filesInMask, (file) =>
                                                  {
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
                                                          if (_processorSettings.ProgressCallbacks != null)
                                                          {
                                                              _processorSettings.ProgressCallbacks.ProcessingStarted(file, Id, filesCount);
                                                          }

                                                          if (!converter.ConvertFile(file))
                                                          {
                                                              if (_processorSettings.ProgressCallbacks != null)
                                                              {
                                                                  _processorSettings.ProgressCallbacks.SkippedDueError(file);
                                                              }
                                                              return;
                                                          }
                                                      }
                                                      catch (Exception ex)
                                                      {
                                                          if (_processorSettings.ProgressCallbacks != null)
                                                          {
                                                              _processorSettings.ProgressCallbacks.SkippedDueError(file);
                                                          }
                                                          Logger.Log.Error(ex);
                                                          return;
                                                      }
                                                      string fileName = BuildNewFileName(file, outputFileName);
                                                      Console.WriteLine(string.Format("Saving {0}...", fileName));
                                                      if (_processorSettings.ProgressCallbacks != null)
                                                      {
                                                          _processorSettings.ProgressCallbacks.ProcessingSaving(fileName, Id, filesCount);
                                                      }
                                                      SaveAndCleanUp(converter, fileName, file);
                                                      if (_processorSettings.ProgressCallbacks != null)
                                                      {
                                                          _processorSettings.ProgressCallbacks.Processed(fileName, Id, filesCount);
                                                      }
                                                  });
            }
            catch(Exception ex)
            {
                Logger.Log.Error(ex.Message);
            }
            finally
            {
                if (_processorSettings.ProgressCallbacks != null)
                {
                    _processorSettings.ProgressCallbacks.ConvertFinished(successfullyConverted);
                }
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
            string fileName = string.Empty;
            if (!string.IsNullOrEmpty(outputFileName) && !_processorSettings.LookInSubFolders)
            {
                fileName = outputFileName.ToLower();
                if (Path.GetExtension(fileName) != ".epub")
                {
                    fileName = string.Format("{0}.epub", fileName);
                }
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
                if (!string.IsNullOrEmpty(_processorSettings.Settings.OutPutPath))
                {
                    fileLocation = _processorSettings.Settings.OutPutPath;
                }
                // in case .fb2.zip remove the "fb2" part
                string fileNameWithExtension = GenerateProperExtension(fileNameWithoutExtension);
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
        /// <param name="lookInSubFolders"></param>
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
            string detectionFolder = Environment.GetFolderPath(Environment.SpecialFolder.CommonApplicationData) + @"\Lord_KiRon\FB2ePub";
            if (Directory.Exists(detectionFolder))
            {
                return detectionFolder;
            }
            return Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location);
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

        public void ConvertXml(XDocument doc, string outFileName, IProgressUpdateInterface progress)
        {
            LoadSettings();
            _processorSettings.DeleteSource = false;
            int successfullyConverted = 0;

            if (_processorSettings.ProgressCallbacks != null)
            {
                _processorSettings.ProgressCallbacks.ConvertStarted(1);
            }


            Fb2EPubConverterEngine converter = new Fb2EPubConverterEngine()
            {
                Settings = _processorSettings.Settings
            };

            try
            {
                if (_processorSettings.ProgressCallbacks != null)
                {
                    _processorSettings.ProgressCallbacks.ProcessingStarted(outFileName, 1, 1);
                }
                converter.ConvertXml(doc);

            }
            catch (Exception ex)
            {
                Logger.Log.Error(ex);
                return;
            }
            if (_processorSettings.ProgressCallbacks != null)
            {
                _processorSettings.ProgressCallbacks.ProcessingSaving(outFileName, 1, 1);
            }
            SaveAndCleanUp(converter, outFileName, "");
            if (_processorSettings.ProgressCallbacks != null)
            {
                _processorSettings.ProgressCallbacks.Processed(outFileName, 1, 1);
            }


            if (_processorSettings.ProgressCallbacks != null)
            {
                _processorSettings.ProgressCallbacks.ConvertFinished(successfullyConverted);
            }

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
