using System;
using System.Collections.Generic;
using System.IO;
using System.Reflection;
using System.Xml.Linq;
using EPubLibrary;
using EPubLibrary.CSS_Items;
using FB2EPubConverter;
using FB2EPubConverter.FB2Loaders;
using FB2Library;
using TranslitRu;
using Fb2epubSettings;


namespace Fb2ePubConverter
{


    internal static class Logger
    {
        // Create a logger for use in this class
        public static readonly log4net.ILog Log = log4net.LogManager.GetLogger(Assembly.GetExecutingAssembly().GetType());
        
    }



    internal abstract class Fb2EPubConverterEngineBase
    {
        /// <summary>
        /// Represent acceptable input file types
        /// </summary>
        private enum FileTypesEnum
        {
            FileTypeZIP,
            FileTypeFB2,
            FileTypeRAR,
            FileTypeUnknown,
        }

        protected readonly ImageManager Images = new ImageManager();
        protected readonly HRefManager ReferencesManager = new HRefManager();
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
            ReferencesManager.FlatStructure = Settings.CommonSettings.Flat;
            Logger.Log.InfoFormat("Starting to convert {0}", fileName);
            FB2Files.Clear();
            if (!File.Exists(fileName))
            {
                Logger.Log.ErrorFormat("Unable to locate file {0} on disk.", fileName);
                return false;
            }
            switch (DetectFileType(fileName))
            {
                case FileTypesEnum.FileTypeZIP:
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
                case FileTypesEnum.FileTypeFB2:
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
                case FileTypesEnum.FileTypeRAR:
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
        /// Detects file type of the input file
        /// </summary>
        /// <param name="fileName"></param>
        /// <returns></returns>
        private static FileTypesEnum DetectFileType(string fileName)
        {
            Logger.Log.DebugFormat("Detecting file type for {0}",fileName);
            var extension = Path.GetExtension(fileName);
            if (extension != null)
                switch (extension.ToUpper())
                {
                    case ".FB2":
                        Logger.Log.Debug("The file is FB2 file");
                        return FileTypesEnum.FileTypeFB2;
                    case ".ZIP":
                        Logger.Log.Debug("The file is ZIP file");
                        return FileTypesEnum.FileTypeZIP;
                    case ".RAR":
                        Logger.Log.Debug("The file is RAR file");
                        return FileTypesEnum.FileTypeRAR;
                    default:
                        Logger.Log.Debug("Can't use extension - attempting to detect");
                        if (FB2ZipFileLoader.IsZipFile(fileName))
                        {
                            Logger.Log.Debug("The file is ZIP file");
                            return FileTypesEnum.FileTypeZIP;                        
                        }
                        if (FB2RarLoader.IsRarFile(fileName))
                        {
                            Logger.Log.Debug("The file is RAR file");
                            return FileTypesEnum.FileTypeRAR;
                        }
                        if (FB2FileLoader.IsFB2File(fileName))
                        {
                            Logger.Log.Debug("The file is FB2 file");
                            return FileTypesEnum.FileTypeFB2;                       
                        }
                        break;
                }
            Logger.Log.Debug("The file is unknown file type");
            return FileTypesEnum.FileTypeUnknown;
        }

        /// <summary>
        /// Saves the loaded FB2 file(s) to the destination as ePub
        /// </summary>
        /// <param name="fb2File"></param>
        /// <param name="epubFile"></param>
        protected abstract void ConvertContent(FB2File fb2File, EPubFile epubFile);

        public void Save(string outFileName)
        {
            Logger.Log.DebugFormat("Saving {0}", outFileName);
            try
            {
                Logger.Log.DebugFormat("Saving totally {0} file(s)", FB2Files.Count);
                foreach (var fb2File in FB2Files)
                {
                    EPubFile epubFile = CreateEpub();
                    Reset();
                    
                    SetTransliterationOptions(epubFile);

                    LoadImagesInMemory(fb2File);

                    ConvertContent(fb2File,epubFile);

                    string outFile = GenerateOutputFileName(fb2File, epubFile, outFileName);
                    AddAboutInformation(epubFile);
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

        private void AddAboutInformation(EPubFile epubFile)
        {
            Assembly asm = Assembly.GetAssembly(GetType());
            string version = "???";
            if (asm != null)
            {
                version = asm.GetName().Version.ToString();
            }
            epubFile.InjectLKRLicense = true;
            epubFile.CreatorSoftwareString = string.Format(@"Fb2epub v{0} [http://www.fb2epub.net]", version);

            if (!Settings.CommonSettings.SkipAboutPage)
            {
                epubFile.AboutTexts.Add(
                    string.Format("This file was generated by Lord KiRon's FB2EPUB converter version {0}.",
                                  version));
                epubFile.AboutTexts.Add("(This book might contain copyrighted material, author of the converter bears no responsibility for it's usage)");
                epubFile.AboutTexts.Add(
                    string.Format("Этот файл создан при помощи конвертера FB2EPUB версии {0} написанного Lord KiRon.",
                        version));
                epubFile.AboutTexts.Add("(Эта книга может содержать материал который защищен авторским правом, автор конвертера не несет ответственности за его использование)");
                epubFile.AboutLinks.Add(@"http://www.fb2epub.net");
                epubFile.AboutLinks.Add(@"https://code.google.com/p/fb2epub/");
            }
        }

        private string GenerateOutputFileName(FB2File fb2File, EPubFile epubFile, string outFileName)
        {
            int count = 0;

            Logger.Log.DebugFormat("Transliteration of file names set to : {0}", Settings.CommonSettings.TransliterateFileName);
            if (Settings.CommonSettings.TransliterateFileName)
            {
                outFileName = epubFile.Transliterator.Translate(outFileName, epubFile.TranslitMode);
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

        private void SetTransliterationOptions(EPubFile epubFile)
        {
            epubFile.Transliterator.RuleFile = string.IsNullOrEmpty(Settings.ResourcesPath) ? @".\Translit\translit.xml" : string.Format(@"{0}\Translit\translit.xml", Settings.ResourcesPath);
            Logger.Log.DebugFormat("Using transliteration rule file : {0}", epubFile.Transliterator.RuleFile);
            if (Settings.CommonSettings.Transliterate)
            {
                epubFile.TranslitMode = TranslitModeEnum.ExternalRuleFile;
                if (!File.Exists(epubFile.Transliterator.RuleFile))
                {
                    Console.WriteLine(@"Unable to locate translation file {0}", epubFile.Transliterator.RuleFile);
                }
            }
            else
            {
                epubFile.TranslitMode = TranslitModeEnum.None;
            }
            epubFile.TranliterateToc = Settings.CommonSettings.TransliterateToc;
            Logger.Log.DebugFormat("Transliteration mode : {0}", epubFile.TranslitMode);
        }

        protected virtual void Reset()
        {
            ReferencesManager.Reset();           
        }

        private EPubFile CreateEpub()
        {
            if (Settings.StandardVersion == EPubVersion.VePub30)
            {

                return new EPubFileV3(Settings.V3Settings.V3SubStandard == EPubV3SubStandard.V30 ? V3Standard.V30 : V3Standard.V301)
                {
                    FlatStructure = Settings.CommonSettings.Flat,
                    EmbedStyles = Settings.CommonSettings.EmbedStyles
                };
            }
            return new EPubFile { FlatStructure = Settings.CommonSettings.Flat, EmbedStyles = Settings.CommonSettings.EmbedStyles };
        }



        protected void SetupCSS(EPubFile epubFile)
        {
            Assembly asm = Assembly.GetAssembly(GetType());
            string pathPreffix = Path.GetDirectoryName(asm.Location);
            if (!string.IsNullOrEmpty(Settings.ResourcesPath))
            {
                pathPreffix = Settings.ResourcesPath;
            }
            epubFile.CSSFiles.Add(new CSSFile { FilePathOnDisk = string.Format(@"{0}\{1}", pathPreffix, @"CSS\default.css"), FileName = "default.css" });
        }


        protected void SetupFonts(EPubFile epubFile)
        {
            if (Settings.CommonSettings.Fonts == null)
            {
                Logger.Log.Warn("No fonts defined in configuration file.");
                return;
            }
            epubFile.SetEPubFonts(Settings.CommonSettings.Fonts, Settings.ResourcesPath, Settings.CommonSettings.DecorateFontNames);
        }

        public void ConvertXml(XDocument fb2Document)
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
