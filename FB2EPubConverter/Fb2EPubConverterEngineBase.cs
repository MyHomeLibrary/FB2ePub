using System;
using System.Collections.Generic;
using System.IO;
using System.Xml.Linq;
using EPubLibrary;
using FB2EPubConverter;
using FB2EPubConverter.FB2Loaders;
using FB2Library;
using TranslitRu;
using Fb2epubSettings;
using Logger = FB2EPubConverter.Logger;


namespace Fb2ePubConverter
{

    internal abstract class Fb2EPubConverterEngineBase
    {

        protected readonly ImageManager Images = new ImageManager();
        protected readonly List<FB2File> FB2Files = new List<FB2File>();



        /// <summary>
        /// Settings for the converter
        /// </summary>
        public ConverterSettings Settings;






        
        /// <summary>
        /// Loads and parses input file or archive to ePub in memory representation
        /// </summary>
        /// <param name="fileName">file to load</param>
        /// <returns></returns>
        public bool LoadAndCheckFB2Files(string fileName)
        {
            Logger.Log.InfoFormat("Starting to convert {0}", fileName);
            FB2Files.Clear();
            if (!File.Exists(fileName))
            {
                Logger.Log.ErrorFormat("Unable to locate file {0} on disk.", fileName);
                return false;
            }
            switch (FileTypeDetector.DetectFileType(fileName))
            {
                case FileTypeDetector.FileTypesEnum.FileTypeZIP:
                    Logger.Log.InfoFormat("Loading ZIP : {0}",fileName);
                    var fb2ZipLoader = new FB2ZipFileLoader();
                    try
                    {
                        FB2Files.AddRange(fb2ZipLoader.LoadFile(fileName, Settings.Fb2ImportSettings));
                    }
                    catch (Exception)
                    {
                        Logger.Log.ErrorFormat("Error loading ZIP {0} :",fileName);
                        return false;
                    }
                    break;
                case FileTypeDetector.FileTypesEnum.FileTypeFB2:
                    Logger.Log.InfoFormat("Processing {0} ...", fileName);
                    var fb2FileLoader = new FB2FileLoader();
                    try
                    {
                        FB2Files.AddRange(fb2FileLoader.LoadFile(fileName,Settings.Fb2ImportSettings));
                    }
                    catch (Exception)
                    {
                        Logger.Log.ErrorFormat("Error loading FB2 {0} :", fileName);
                        return false;
                    }
                    break;
                case FileTypeDetector.FileTypesEnum.FileTypeRAR:
                    Logger.Log.InfoFormat("Loading RAR : {0}", fileName);
                    var fb2RarLoader = new FB2RarLoader();
                    try
                    {
                        FB2Files.AddRange(fb2RarLoader.LoadFile(fileName,Settings.Fb2ImportSettings));
                    }
                    catch (Exception)
                    {
                        Logger.Log.ErrorFormat("Error loading RAR {0} :", fileName);
                        return false;
                    }
                    break;
                default:
                    Logger.Log.InfoFormat("File {0} is of unsupported type",fileName);
                    return false;
            }
            return true;
        }



        /// <summary>
        /// Saves the loaded FB2 file(s) to the destination as ePub
        /// </summary>
        /// <param name="fb2File"></param>
        /// <param name="epubFile"></param>
        protected abstract void ConvertContent(FB2File fb2File, IEpubFile epubFile);

        public void Save(string outFileName)
        {
            Logger.Log.DebugFormat("Saving {0}", outFileName);
            try
            {
                Logger.Log.DebugFormat("Saving totally {0} file(s)", FB2Files.Count);
                foreach (var fb2File in FB2Files)
                {
                    IEpubFile epubFile = CreateEpub();
                    Reset();
                    
                    SetTransliterationOptions(epubFile);

                    LoadImagesInMemory(fb2File);

                    ConvertContent(fb2File,epubFile);

                    string outFile = GenerateOutputFileName(fb2File, epubFile, outFileName);
                    epubFile.Generate(outFile);

                }
            }
            catch (Exception ex)
            {
                Logger.Log.ErrorFormat("Error saving file {0} : {1} - {2}", outFileName, ex.StackTrace, ex);
                throw;
            }

        }

        private void LoadImagesInMemory(FB2File fb2File)
        {
            Images.RemoveAlpha = Settings.Fb2ImportSettings.ConvertAlphaPng;
            Images.LoadFromBinarySection(fb2File.Images);
        }

        private string GenerateOutputFileName(FB2File fb2File, IEpubFile epubFile, string outFileName)
        {
            int count = 0;

            Logger.Log.DebugFormat("Transliteration of file names set to : {0}", Settings.CommonSettings.TransliterateFileName);
            if (Settings.CommonSettings.TransliterateFileName)
            {
                outFileName = Rus2Lat.Instance.Translate(outFileName, epubFile.TranslitMode);
                Logger.Log.DebugFormat("New transliterated file name : {0}", outFileName);
            }
            string outFile = outFileName;
            string outFolder = Path.GetDirectoryName(outFileName);
            if (string.IsNullOrEmpty(outFolder))
            {
                Logger.Log.DebugFormat("Using output folder : {0}", Settings.OutPutPath);
                outFolder = Settings.OutPutPath;
                if (!string.IsNullOrEmpty(outFolder))
                {
                    outFile = string.Format(@"{0}\{1}.epub", outFolder, Path.GetFileNameWithoutExtension(outFileName));
                }

            }
            while (File.Exists(outFile) && (fb2File != FB2Files[0]))
            {
                Logger.Log.DebugFormat("{0} file exists , incrementing", outFile);
                outFile = string.Format(@"{0}\{1}_{2}.epub", outFolder, Path.GetFileNameWithoutExtension(outFileName), count++);
            }
            Logger.Log.DebugFormat("Final output file name : {0}", outFile);
            return outFile;
        }

        private void SetTransliterationOptions(IEpubFile epubFile)
        {
            epubFile.TranslitMode.FileName = string.IsNullOrEmpty(Settings.ResourcesPath) ? @".\Translit\translit.xml" : string.Format(@"{0}\Translit\translit.xml", Settings.ResourcesPath);
            Logger.Log.DebugFormat("Using transliteration rule file : {0}", epubFile.TranslitMode.FileName);
            if (Settings.CommonSettings.Transliterate)
            {
                epubFile.TranslitMode.Mode = TranslitModeEnum.ExternalRuleFile;
                if (!File.Exists(epubFile.TranslitMode.FileName))
                {
                    Logger.Log.ErrorFormat(@"Unable to locate translation file {0}, switching transliteration off", epubFile.TranslitMode.FileName);
                    epubFile.TranslitMode.Mode = TranslitModeEnum.None;
                }
            }
            else
            {
                epubFile.TranslitMode.Mode = TranslitModeEnum.None;
            }
            epubFile.TranliterateToc = Settings.CommonSettings.TransliterateToc;
            Logger.Log.DebugFormat("Transliteration mode : {0}", epubFile.TranslitMode.Mode);
        }

        protected virtual void Reset()
        {
        }

        protected abstract IEpubFile CreateEpub();



        public void LoadFB2FileFromXML(XDocument fb2Document)
        {
            FB2Files.Clear();
            var file = new FB2File();
            try
            {
                file.Load(fb2Document, false);
                FB2Files.Add(file);
            }
            catch (Exception ex)
            {
                Logger.Log.ErrorFormat("Error loading XML document : {0}", ex);
            }
        }
    }
}
