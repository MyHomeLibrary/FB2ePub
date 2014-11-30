using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Xml;
using System.Xml.Linq;
using EPubLibrary;
using FB2EPubConverter;
using FB2Library;
using FB2Library.Elements;
using FB2Library.HeaderItems;
using ICSharpCode.SharpZipLib.Zip;
using TranslitRu;
using XMLFixerLibrary;
using ZipEntry=ICSharpCode.SharpZipLib.Zip.ZipEntry;
using FB2Fix;
using NUnrar.Archive;
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
                    if (!LoadFb2ZipFile(fileName))
                    {
                        Logger.Log.ErrorFormat("Error loading ZIP {0} :",fileName);
                        return false;
                    }
                    break;
                case FileTypesEnum.FileTypeFB2:
                    Logger.Log.InfoFormat("Processing {0} ...", fileName);
                    if (!LoadFb2File(fileName))
                    {
                        Logger.Log.ErrorFormat("Error loading FB2 {0} :", fileName);
                        return false;
                    }
                    break;
                case FileTypesEnum.FileTypeRAR:
                    Logger.Log.InfoFormat("Loading RAR : {0}", fileName);
                    if (!LoadFb2RarFile(fileName))
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
        /// Loads FB2 from RAR files
        /// </summary>
        /// <param name="fileName"></param>
        /// <returns></returns>
        private bool LoadFb2RarFile(string fileName)
        {
            bool fb2FileFound = false;
            bool fb2FileLoaded = false;
            try
            {
                RarArchive rarFile = RarArchive.Open(fileName);

                int n = rarFile.Entries.Count;
                Logger.Log.DebugFormat("Detected {0} entries in RAR file", n);
                foreach(var entry in rarFile.Entries)
                {
                    if (entry.IsDirectory) 
                    {
                        Logger.Log.DebugFormat("{0} is not file but folder", fileName);
                        continue;
                    }
                    var extension = Path.GetExtension(entry.FilePath);
                    if (extension != null && extension.ToUpper() == ".FB2")
                    {
                        fb2FileFound = true;
                        try
                        {
                            string tempPath = Path.GetTempPath();
                            entry.WriteToDirectory(tempPath);
                            string fileNameOnly = Path.GetFileName(entry.FilePath);
                            Logger.Log.InfoFormat("Processing {0} ...", fileNameOnly);
                            if (!LoadFb2File(string.Format(@"{0}\{1}", tempPath, fileNameOnly)))
                            {
                                Logger.Log.ErrorFormat("Unable to load {0}", fileNameOnly);
                                //continue;
                            }
                            else
                            {
                                fb2FileLoaded = true;
                            }
                        }
                        catch (Exception ex)
                        {
                            Logger.Log.ErrorFormat("Unable to unrar file entry {0} : {1}", entry.FilePath, ex);
                            //continue;
                        }
                    }
                    else
                    {
                        Logger.Log.InfoFormat("{0} is not FB2 file", entry.FilePath);
                        //continue;                        
                    }
                    
                }
            }
            catch (Exception ex)
            {
                Logger.Log.ErrorFormat("Error loading RAR file : {0}",ex);
                return false;
            }
            return fb2FileFound && fb2FileLoaded;
        }

        /// <summary>
        /// Loads FB2 file
        /// </summary>
        /// <param name="fileName"></param>
        /// <returns></returns>
        private bool LoadFb2File(string fileName)
        {
            try
            {
                Stream s = File.OpenRead(fileName);

                if (Settings.Fb2ImportSettings.FixMode == FixOptions.Fb2FixAlways)
                {
                    LoadFB2StreamWithFix(s, ReadFb2FileStream);
                }
                else
                {
                    try
                    {
                        try
                        {
                            ReadFb2FileStream(s);
                        }
                        catch (XmlException)
                        {
                            if (Settings.Fb2ImportSettings.FixMode == FixOptions.DoNotFix)
                            {
                                Logger.Log.ErrorFormat("Error in file, not fixing ");
                                return false;
                            }
                            Logger.Log.Info("Error loading file - invalid XML content - attempting to repair...");
                            // try to read broken XML
                            s.Seek(0, SeekOrigin.Begin);
                            ReadBrokenXmlFb2FileStream(s);
                        }
                        s.Close();
                    }
                    catch (XmlException)
                    {
                        if (Settings.Fb2ImportSettings.FixMode == FixOptions.MinimalFix)
                        {
                            Logger.Log.ErrorFormat("Error in file, not fixing ");
                            return false;
                        }
                        Logger.Log.Info("Repair attempt failed - attempting to repair using Fb2Fix...");
                        // try to read broken XML
                        s.Seek(0, SeekOrigin.Begin);
                        LoadFB2StreamWithFix(s, ReadFb2FileStream);
                    }
                }
            }
            catch (Exception ex)
            {
                Logger.Log.ErrorFormat("Error loading FB2 file : {0}", ex);
                return false;
            }

            return true;
        }

        private void LoadFB2StreamWithFix(Stream s, Action<Stream> streamLoader)
        {
            var options = new Fb2FixArguments
            {
                incversion = true,
                regenerateId = false,
                indentBody = false,
                indentHeader = true,
                mapGenres = true,
                encoding = Encoding.UTF8
            };

            using (Stream output = Fb2Fix.Process(s, options))
            {
                streamLoader(output);
            }
            
        }

        private void ReadFb2FileStream(Stream s)
        {
            Logger.Log.Debug("Starting to load FB2 stream");
            var settings = new XmlReaderSettings
                                             {
                                                 ValidationType = ValidationType.None,
                                                 DtdProcessing = DtdProcessing.Prohibit,
                                                 CheckCharacters = false
                                                 
            };
            XDocument fb2Document;
            try
            {
                    using (XmlReader reader = XmlReader.Create(s, settings))
                    {
                        fb2Document = XDocument.Load(reader, LoadOptions.PreserveWhitespace);
                        reader.Close();
                    }

            }
            catch(XmlException) // we handle this on top
            {
               throw;
            }
            catch (Exception ex)
            {
                Logger.Log.ErrorFormat("Error loading file : {0}", ex);
                throw;
            }
            var file = new FB2File();
            try
            {
                file.Load(fb2Document,false);
                FB2Files.Add(file);
            }
            catch (Exception ex)
            {
                Logger.Log.ErrorFormat("Error loading file : {0}",ex);
            }
            Logger.Log.Debug("FB2 stream loaded");
        }

        private bool LoadFb2ZipFile(string fileName)
        {
            Logger.Log.DebugFormat("Starting to load ZIP file {0}",fileName);
            try
            {
                Exception returnEx = null;
                bool fb2FileFound = false;
                bool fb2FileLoaded = false;
                using (var s = new ZipInputStream(File.OpenRead(fileName)))
                {
                    try
                    {
                        ZipEntry theEntry;
                        while ((theEntry = s.GetNextEntry()) != null)
                        {
                            if (!theEntry.IsFile || !theEntry.CanDecompress)
                            {
                                Logger.Log.InfoFormat("{0} is not file or not decompresable",fileName);
                                continue;
                            }
                            Logger.Log.InfoFormat("Processing {0} ...", theEntry.Name);
                            var extension = Path.GetExtension(theEntry.Name);
                            if (extension != null && extension.ToUpper() == ".FB2")
                            {
                                fb2FileFound = true;

                                if (Settings.Fb2ImportSettings.FixMode == FixOptions.Fb2FixAlways)
                                {
                                    using (var s1 = new ZipInputStream(File.OpenRead(fileName)))
                                    {
                                        // reach the same position in ZIP
                                        while (theEntry.ToString() != s1.GetNextEntry().ToString())
                                        {
                                        }
                                        LoadFB2StreamWithFix(s1, ReadFb2FileStream);
                                        fb2FileLoaded = true;
                                    }
                                }
                                else
                                {
                                    try
                                    {
                                        ReadFb2FileStream(s);
                                        fb2FileLoaded = true;
                                    }
                                    catch (XmlException) // broken/malformed Xml detected
                                    {
                                        if (Settings.Fb2ImportSettings.FixMode == FixOptions.DoNotFix)
                                        {
                                            Logger.Log.ErrorFormat("Error in file, not fixing ");
                                            continue;
                                        }
                                        // try to run work around case 
                                        Logger.Log.Info(
                                            "Error loading file - invalid XML content - attempting to repair...");
                                        using (var s1 = new ZipInputStream(File.OpenRead(fileName)))
                                        {
                                            // reach the same position in ZIP
                                            while (theEntry.ToString() != s1.GetNextEntry().ToString())
                                            {
                                            }
                                            // try to read broken XML
                                            try
                                            {
                                                ReadBrokenXmlFb2FileStream(s1);
                                                fb2FileLoaded = true;
                                            }
                                            catch (XmlException)
                                            {
                                                if (Settings.Fb2ImportSettings.FixMode == FixOptions.MinimalFix)
                                                {
                                                    Logger.Log.ErrorFormat("Error in file, not fixing ");
                                                    continue;
                                                }
                                                s1.Close();
                                                using (var s2 = new ZipInputStream(File.OpenRead(fileName)))
                                                {
                                                    // reach the same position in ZIP
                                                    while (theEntry.ToString() != s2.GetNextEntry().ToString())
                                                    {
                                                    }

                                                    Logger.Log.Info(
                                                        "Repair attempt failed - attempting to repair using Fb2Fix...");
                                                    // try to read broken XML
                                                    try
                                                    {
                                                        try
                                                        {
                                                            LoadFB2StreamWithFix(s2, ReadFb2FileStream);
                                                            fb2FileLoaded = true;
                                                        }
                                                        catch (XmlException)
                                                        {
                                                            Logger.Log.ErrorFormat("Error in file - unable to fix");
                                                            //continue;
                                                        }
                                                    }
                                                    catch (Exception ex)
                                                    {
                                                        Logger.Log.ErrorFormat("Error in file - Fb2Fix crashes - unable to fix. \nError {0}",ex.Message);
                                                        //continue;
                                                    }
                                                }
                                            }
                                        }
                                    }
                                }
                            }
                        }
                    }
                    catch(ZipException ze)
                    {
                        Logger.Log.ErrorFormat("{0} - problem decompressing the file, UnZip error: {1}", fileName,ze.Message);
                        s.Close();
                        returnEx = ze;
                    }
                    catch (Exception ex)
                    {
                       Logger.Log.ErrorFormat("{0} - problem decompressing the file, error: {1}", fileName, ex);
                        s.Close();
                        returnEx = ex;
                    }
                    s.Close();
                }
                Logger.Log.DebugFormat("ZIP file {0} loaded successfully", fileName);
                if (returnEx != null)
                {
                    throw returnEx;
                }
                return fb2FileFound && fb2FileLoaded;
            }
            catch (Exception ex)
            {
                Logger.Log.ErrorFormat("Error loading ZIP file {0} : {1}",fileName,ex);
            }
            return false;
            
        }

        private void ReadBrokenXmlFb2FileStream(Stream stream)
        {
            Logger.Log.Debug("Starting to load FB2 stream");
            try
            {
                using (var ms = new MemoryStream())
                {
                    var fixer = new XmlRepair();
                    fixer.Repair(stream,ms);
                    ms.Seek(0, SeekOrigin.Begin);
                    ReadFb2FileStream(ms);
                }

            }
            catch(XmlException xex)
            {
                Logger.Log.WarnFormat("Error loading file - invalid XML content : {0} \nRepair attempt failed", xex);
                throw;

            }
            catch (Exception ex)
            {
                Logger.Log.ErrorFormat("Error loading file : {0}", ex);
                throw;
            }
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
                        if (IsZipFile(fileName))
                        {
                            Logger.Log.Debug("The file is ZIP file");
                            return FileTypesEnum.FileTypeZIP;                        
                        }
                        if (IsRarFile(fileName))
                        {
                            Logger.Log.Debug("The file is RAR file");
                            return FileTypesEnum.FileTypeRAR;
                        }
                        if (IsFB2File(fileName))
                        {
                            Logger.Log.Debug("The file is FB2 file");
                            return FileTypesEnum.FileTypeFB2;                       
                        }
                        break;
                }
            Logger.Log.Debug("The file is unknown file type");
            return FileTypesEnum.FileTypeUnknown;
        }

        private static bool IsFB2File(string fileName)
        {
            using (var s = File.OpenRead(fileName))
            {
                var settings = new XmlReaderSettings
                                                 {
                                                     ValidationType = ValidationType.None,
                                                     DtdProcessing = DtdProcessing.Prohibit,
                                                     CheckCharacters = false
                                                 
                };
                try
                {
                        using (XmlReader reader = XmlReader.Create(s, settings))
                        {
                            XNamespace fb2Namespace = "http://www.gribuser.ru/xml/fictionbook/2.0";
                            XDocument fb2Document= XDocument.Load(reader, LoadOptions.PreserveWhitespace);
                            if (fb2Document.Root != null &&
                                fb2Document.Root.Name.LocalName == "FictionBook" &&
                                fb2Document.Root.Name.Namespace == fb2Namespace) 
                            {
                                reader.Close();
                                return true;
                            }
                            reader.Close();
                        }

                }
                catch (Exception )
                {
                    return false;
                }
            }
            return false;
        }

        private static bool IsRarFile(string fileName)
        {
            try
            {
                return RarArchive.IsRarFile(fileName);
            }
            catch (Exception ex)
            {
                Logger.Log.Debug(ex.Message);
            }
            return false;
        }

        /// <summary>
        /// Checks if input file is a ZIP archive
        /// </summary>
        /// <param name="fileName">file to check</param>
        /// <returns>true if file is ZIP archive, false otherwise</returns>
        private static bool IsZipFile(string fileName)
        {
            try
            {
                using (new ZipFile(fileName))
                {
                }
            }
            catch (Exception)
            {
                return false;
            }
            return true;
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




        protected string GenerateFileAsString(AuthorType author)
        {
            var processor = new ProcessAuthorFormat {Format = Settings.CommonSettings.FileAsFormat};
            return processor.GenerateAuthorString(author);

        }

        protected string GenerateAuthorString(AuthorType author)
        {
            var processor = new ProcessAuthorFormat {Format = Settings.CommonSettings.AuthorFormat};
            return processor.GenerateAuthorString(author);
        }



        protected string FormatBookTitle(ItemTitleInfo titleInfo)
        {
            var formatTitle = new ProcessSeqFormatString
            {
                BookTitleFormatSeqNum = Settings.CommonSettings.SequenceFormat,
                BookTitleFormatNoSeqNum = Settings.CommonSettings.NoSequenceFormat,
                BookTitleFormatNoSeries = Settings.CommonSettings.NoSeriesFormat
            };

            String rc;
            if ((titleInfo.Sequences.Count > 0) && Settings.CommonSettings.AddSeqToTitle)
            {
                rc = formatTitle.GenerateBookTitle(titleInfo.BookTitle.Text, titleInfo.Sequences[0].Name,
                                                   titleInfo.Sequences[0].Number);
            }
            else
            {
                rc = formatTitle.GenerateBookTitle(titleInfo.BookTitle.Text, "", 0);
            }
            return rc;
        }


        protected static List<string> GetSequencesAsStrings(SequenceType seq)
        {
            var allSequences =  new List<string>();
            if (seq != null)
            {
                if (!string.IsNullOrEmpty(seq.Name))
                {
                    string sequence = seq.Name;
                    if (seq.Number.HasValue && seq.Number != 0)
                    {
                        sequence = string.Format("{0} - {1}", seq.Name, seq.Number);
                    }
                    allSequences.Add(sequence);
                }
                if (seq.SubSections != null)
                {
                    allSequences.AddRange(seq.SubSections.SelectMany(GetSequencesAsStrings));
                }
            }
            return allSequences;
        }


        protected static string Fb2GenreToDescription(string genre)
        {
            // TODO: implement real description conversion
            return genre;
        }


        protected virtual void ConvertSequences(FB2File fb2File, EPubFile epubFile)
        {
            foreach (var seq in fb2File.TitleInfo.Sequences)
            {
                List<string> allSequences = GetSequencesAsStrings(seq);
                if (allSequences.Count != 0)
                {
                    foreach (var sequence in allSequences)
                    {
                        epubFile.TitlePage.Series.Add(sequence);
                        epubFile.AllSequences.Add(sequence);
                    }
                }
            }
        }

        protected virtual void ConvertAuthors(FB2File fb2File, EPubFile epubFile)
        {
            foreach (var author in fb2File.TitleInfo.BookAuthors)
            {
                var person = new PersoneWithRole();
                string authorString = GenerateAuthorString(author);
                person.PersonName = epubFile.Transliterator.Translate(authorString, epubFile.TranslitMode);
                person.FileAs = GenerateFileAsString(author);
                person.Role = RolesEnum.Author;
                person.Language = fb2File.TitleInfo.Language;
                epubFile.Title.Creators.Add(person);

                // add authors to Title page
                epubFile.TitlePage.Authors.Add(authorString);
            }
        }

        protected virtual void ConvertTranslators(FB2File fb2File, EPubFile epubFile)
        {
            foreach (var translator in fb2File.TitleInfo.Translators)
            {
                var person = new PersoneWithRole
                {
                    PersonName = epubFile.Transliterator.Translate(GenerateAuthorString(translator),
                        epubFile.TranslitMode),
                    FileAs = GenerateFileAsString(translator),
                    Role = RolesEnum.Translator,
                    Language = fb2File.TitleInfo.Language
                };
                epubFile.Title.Contributors.Add(person);
            }
        }

        protected virtual void ConvertGenres(FB2File fb2File, EPubFile epubFile)
        {
            foreach (var genre in fb2File.TitleInfo.Genres)
            {
                var item = new Subject
                {
                    SubjectInfo = epubFile.Transliterator.Translate(Fb2GenreToDescription(genre.Genre),
                        epubFile.TranslitMode)
                };
                epubFile.Title.Subjects.Add(item);
            }
        }


        protected abstract void ConvertAnnotation(FB2File fb2File, EPubFile epubFile);

        protected virtual void ConvertMainTitle(FB2File fb2File, EPubFile epubFile)
        {
            if (fb2File.TitleInfo.BookTitle != null &&
                (Settings.CommonSettings.IgnoreTitle != IgnoreTitleOptions.IgnoreMainTitle) &&
                (Settings.CommonSettings.IgnoreTitle != IgnoreTitleOptions.IgnoreMainAndPublish) &&
                Settings.CommonSettings.IgnoreTitle != IgnoreTitleOptions.IgnoreMainAndSource)
            {

                var bookTitle = new Title
                {
                    TitleName = epubFile.Transliterator.Translate(FormatBookTitle(fb2File.TitleInfo),
                        epubFile.TranslitMode),
                    Language = string.IsNullOrEmpty(fb2File.TitleInfo.BookTitle.Language)
                        ? fb2File.TitleInfo.Language
                        : fb2File.TitleInfo.BookTitle.Language,
                    TitleType = TitleType.Main
                };
                epubFile.Title.BookTitles.Add(bookTitle);
                // add main title language
                epubFile.Title.Languages.Add(fb2File.TitleInfo.Language);
            }
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
