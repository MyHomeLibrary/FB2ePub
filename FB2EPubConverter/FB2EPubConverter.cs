using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Runtime.InteropServices;
using System.Security.Permissions;
using System.Text;
using System.Text.RegularExpressions;
using System.Xml;
using System.Xml.Linq;
using System.Xml.Schema;
using Chilkat;
using EPubLibrary;
using EPubLibrary.Content.Guide;
using EPubLibrary.CSS_Items;
using EPubLibrary.XHTML_Items;
using FB2EPubConverter;
using FB2Library;
using FB2Library.Elements;
using FB2Library.Elements.Poem;
using FB2Library.Elements.Table;
using FB2Library.HeaderItems;
using FontsSettings;
using ICSharpCode.SharpZipLib.Zip;
using TranslitRu;
using XHTMLClassLibrary.BaseElements;
using XHTMLClassLibrary.BaseElements.BlockElements;
using XHTMLClassLibrary.BaseElements.InlineElements;
using XHTMLClassLibrary.BaseElements.TableElements;
using XMLFixerLibrary;
using EPubLibrary.ReferenceUtils;
using ZipEntry=ICSharpCode.SharpZipLib.Zip.ZipEntry;
using FB2Fix;

namespace Fb2ePubConverter
{


    internal static class Logger
    {
        // Create a logger for use in this class
        public static readonly log4net.ILog Log = log4net.LogManager.GetLogger(Assembly.GetExecutingAssembly().GetType());
        
    }

    internal static class XhtmlItemExtender
    {
        public static long EstimateSize(this IXHTMLItem item)
        {
            MemoryStream stream = new MemoryStream();
            XNode node = item.Generate();
            XDocument doc = new XDocument();
            doc.Add(node);
            using (var writer = XmlWriter.Create(stream))
            {
                //node.WriteTo(writer);
                doc.WriteTo(writer);
            }
            return stream.Length;            
        }
    }


    public class Fb2EPubConverterEngine
    {
        private long _maxSize = 245 * 1024;

        private int _sectionCounter = 0;

        private enum ParagraphConvTargetEnum
        {
            Paragraph,
            H1,
            H2,
            H3,
            H4,
            H5,
            H6
        }


        private readonly ImageManager images = new ImageManager();

        private readonly HRefManager referencesManager = new HRefManager();

        private enum FileTypesEnum
        {
            FileTypeZIP,
            FileTypeFB2,
            FileTypeRAR,
            FileTypeUnknown,
        }

        public enum FixOptions
        {
            DoNotFix,
            MinimalFix,
            UseFb2Fix,
            Fb2FixAlways,
        }

        private readonly List<FB2File> fb2Files = new List<FB2File>();


        /// <summary>
        /// Get/Set if sequences abbreviations should be added to title
        /// </summary>
        public bool AddSeqToTitle { get; set; }


        /// <summary>
        /// Get/Set embeding styles into xHTML files instead of referencing style files
        /// </summary>
        public bool EmbedStyles { get; set; }

        /// <summary>
        /// Set/Get if alpha channel palette bitmaps need to be converted
        /// </summary>
        public bool ConvertAlphaPng { get; set; }

        /// <summary>
        /// Get/Set "flat" mode , when flat mode is set no subfolders created inside the ZIP
        /// used to work aroun bugs in some readers
        /// </summary>
        public bool Flat { get; set; }

        /// <summary>
        /// Get/Set max document size in bytes
        /// </summary>
        public long MaxSize
        {
            get { return _maxSize; }
            set 
            { 
                if ( value <= 0)
                {
                    throw new ArgumentException("value");
                }
                _maxSize = value;
            }
        }

        [DllImport("kernel32.dll", CharSet = CharSet.Auto)]
        public static extern int GetShortPathName(
            [MarshalAs(UnmanagedType.LPTStr)]
        string path,
            [MarshalAs(UnmanagedType.LPTStr)]
        StringBuilder shortPath,
            int shortPathLength
            );

        public FixOptions FixMode { get; set; }

        /// <summary>
        /// Get/Set Fonts settings
        /// </summary>
        public FontsSettings.FontSettings Fonts { get; set; }

        /// <summary>
        /// Get/Set output path used in case output file name does not includes path
        /// </summary>
        public string OutPutPath { get; set; }


        /// <summary>
        /// Get/Set if program should generate FB2 info page
        /// </summary>
        public bool Fb2Info { get; set; }

        /// <summary>
        /// Get/Set if data outside the text body needs to be transliterated 
        /// (used in case device does not support cyrillic fonts)
        /// </summary>
        public bool Transliterate { get; set; }

        /// <summary>
        /// Get/Set transliteration of the output file name(s)
        /// </summary>
        public bool TransliterateFileName { get; set; }

        /// <summary>
        /// Get set transliteration of TOC
        /// </summary>
        public bool TransliterateToc { get; set; }

        /// <summary>
        /// Get/Set path to conversion resources location (css, fonts etc)
        /// </summary>
        public string ResourcesPath { get; set; }


        /// <summary>
        /// Get/Set book title sequence format
        /// </summary>
        public string SequenceFormat { get; set; }

        /// <summary>
        /// Get/Set book title with no sequence format
        /// </summary>
        public string NoSequenceFormat { get; set; }

        /// <summary>
        /// Get/Set book title with no series format
        /// </summary>
        public string NoSeriesFormat { get; set; }

        /// <summary>
        /// Get/Set Author format string
        /// </summary>
        public string AuthorFormat { get; set; }


        /// <summary>
        /// Get/Set FileAs format string
        /// </summary>
        public string FileAsFormat { get; set; }
        
        /// <summary>
        /// Convert input file or archive to ePub(s)
        /// </summary>
        /// <param name="fileName"></param>
        /// <returns></returns>
        public bool ConvertFile(string fileName)
        {
            referencesManager.FlatStructure = Flat;
            Logger.Log.InfoFormat("Starting to convert {0}", fileName);
            fb2Files.Clear();
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
            try
            {
                Rar rarFile = new Rar();
                if (fileName.Contains(' ')) // This to work around unrar bug with filenames containing spaces
                {
                    StringBuilder shortPath = new StringBuilder(255);
                    GetShortPathName(fileName, shortPath, shortPath.Capacity);
                    fileName = shortPath.ToString();
                }
                if (!rarFile.Open(fileName))
                {
                    Logger.Log.ErrorFormat("Unable to open {0} file",fileName);
                    return false;
                }

                int n = rarFile.NumEntries;
                Logger.Log.DebugFormat("Detected {0} entries in RAR file");
                for (int i = 0; i <= n - 1; i++)
                {
                    RarEntry entry = rarFile.GetEntryByIndex(i);
                    if (entry.IsDirectory) 
                    {
                        Logger.Log.DebugFormat("{0} is not file but folder", fileName);
                        continue;
                    }
                    if (Path.GetExtension(entry.Filename).ToUpper() == ".FB2")
                    {
                        try
                        {
                            string tempPath = Path.GetTempPath();
                            if (!entry.Unrar(tempPath))
                            {
                                throw new Exception(entry.LastErrorText);
                            }
                            Logger.Log.InfoFormat("Processing {0} ...",entry.Filename);
                            if (!LoadFb2File(string.Format(@"{0}\{1}",tempPath,entry.Filename)))
                            {
                                Logger.Log.ErrorFormat("Unable to load {0}", entry.Filename);
                                continue;
                            }
                        }
                        catch (Exception ex)
                        {
                            Logger.Log.ErrorFormat("Unable to unrar file entry {0} : {1}", entry.Filename, ex.ToString());
                            continue;
                        }
                    }
                    else
                    {
                        Logger.Log.InfoFormat("{0} is not FB2 file", entry.Filename);
                        continue;                        
                    }
                    
                }
                rarFile.Close();
            }
            catch (Exception ex)
            {
                Logger.Log.ErrorFormat("Error loading RAR file : {0}",ex.ToString());
                return false;
            }
            return true;
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

                Fb2FixArguments options = new Fb2FixArguments();
                options.incversion = true;
                options.regenerateId = false;
                options.indentBody = false;
                options.indentHeader = true;
                options.mapGenres = true;
                options.encoding = Encoding.UTF8;


                if (FixMode == FixOptions.Fb2FixAlways)
                {
                    using (Stream output = Fb2Fix.Process(s, options))
                    {
                        ReadFb2FileStream(output);
                    }
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
                            if (FixMode == FixOptions.DoNotFix)
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
                        if (FixMode == FixOptions.MinimalFix)
                        {
                            Logger.Log.ErrorFormat("Error in file, not fixing ");
                            return false;
                        }
                        Logger.Log.Info("Repair attempt failed - attempting to repair using Fb2Fix...");
                        // try to read broken XML
                        s.Seek(0, SeekOrigin.Begin);
                        using (Stream output = Fb2Fix.Process(s, options))
                        {
                            ReadFb2FileStream(output);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                Logger.Log.ErrorFormat("Error loading FB2 file : {0}", ex.ToString());
                return false;
            }

            return true;
        }

        private void ReadFb2FileStream(Stream s)
        {
            Logger.Log.Debug("Starting to load FB2 stream");
            XmlReaderSettings settings = new XmlReaderSettings
                                             {
                                                 ValidationType = ValidationType.None,
                                                 ProhibitDtd = true,
                                                 CheckCharacters = false,
                                                 
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
                Logger.Log.ErrorFormat("Error loading file : {0}", ex.ToString());
                throw;
            }
            FB2File file = new FB2File();
            try
            {
                file.Load(fb2Document,false);
                fb2Files.Add(file);
            }
            catch (Exception ex)
            {
                Logger.Log.ErrorFormat("Error loading file : {0}",ex.ToString());
            }
            Logger.Log.Debug("FB2 stream loaded");
        }

        private bool LoadFb2ZipFile(string fileName)
        {
            Logger.Log.DebugFormat("Starting to load ZIP file {0}",fileName);
            try
            {
                Exception returnEx = null;
                using (var s = new ZipInputStream(File.OpenRead(fileName)))
                {
                    ZipEntry theEntry;
                    try
                    {
                        while ((theEntry = s.GetNextEntry()) != null)
                        {
                            if (!theEntry.IsFile || !theEntry.CanDecompress)
                            {
                                Logger.Log.InfoFormat("{0} is not file or not decompresable",fileName);
                                continue;
                            }
                            Logger.Log.InfoFormat("Processing {0} ...", theEntry.Name);
                            if (Path.GetExtension(theEntry.Name).ToUpper() == ".FB2")
                            {
                                Fb2FixArguments options = new Fb2FixArguments();
                                options.incversion = true;
                                options.regenerateId = false;
                                options.indentBody = false;
                                options.indentHeader = true;
                                options.mapGenres = true;
                                options.encoding = Encoding.UTF8;

                                if (FixMode == FixOptions.Fb2FixAlways)
                                {
                                    using (var s1 = new ZipInputStream(File.OpenRead(fileName)))
                                    {
                                        // reach the same position in ZIP
                                        while (theEntry.ToString() != s1.GetNextEntry().ToString())
                                        {
                                        }
                                        using (Stream output = Fb2Fix.Process(s1, options))
                                        {
                                            ReadFb2FileStream(output);
                                        }
                                    }
                                }
                                else
                                {
                                    try
                                    {
                                        ReadFb2FileStream(s);
                                    }
                                    catch (XmlException) // broken/mailformed Xml detected
                                    {
                                        if (FixMode == FixOptions.DoNotFix)
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
                                            }
                                            catch (XmlException)
                                            {
                                                if (FixMode == FixOptions.MinimalFix)
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
                                                    using (Stream output = Fb2Fix.Process(s2, options))
                                                    {
                                                        try
                                                        {
                                                            ReadFb2FileStream(output);
                                                        }
                                                        catch (XmlException)
                                                        {
                                                            Logger.Log.ErrorFormat("Error in file - unable to fix");
                                                            continue;
                                                        }
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
                       Logger.Log.ErrorFormat("{0} - problem decompressing the file, error: {1}", fileName, ex.ToString());
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
                return true;
            }
            catch (Exception ex)
            {
                Logger.Log.ErrorFormat("Error loading ZIP file {0} : {1}",fileName,ex.ToString());
            }
            return false;
            
        }

        private void ReadBrokenXmlFb2FileStream(Stream stream)
        {
            Logger.Log.Debug("Starting to load FB2 stream");
            try
            {
                using (MemoryStream ms = new MemoryStream())
                {
                    XmlRepair fixer = new XmlRepair();
                    fixer.Repair(stream,ms);
                    ms.Seek(0, SeekOrigin.Begin);
                    ReadFb2FileStream(ms);
                }

            }
            catch(XmlException xex)
            {
                Logger.Log.WarnFormat("Error loading file - invalid XML content : {0} \nRepair attempt failed", xex.ToString());
                throw;

            }
            catch (Exception ex)
            {
                Logger.Log.ErrorFormat("Error loading file : {0}", ex.ToString());
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
            switch (Path.GetExtension(fileName).ToUpper())
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
                    Logger.Log.Debug("Can't use extension - attemting to detect");
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
                    break;
            }
            Logger.Log.Debug("The file is unknown file type");
            return FileTypesEnum.FileTypeUnknown;
        }

        private static bool IsRarFile(string fileName)
        {
            Rar file = new Rar();
            if( file.Open(fileName)) 
            {
                file.Close();
                return true;
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
                ZipFile file = new ZipFile(fileName);
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
        /// <param name="outFileName"></param>
        public void Save(string outFileName)
        {
            Logger.Log.DebugFormat("Saving {0}",outFileName);
            try
            {
                EPubFile epubFile;
                int count = 0;
                Logger.Log.DebugFormat("Saving totaly {0} file(s)",fb2Files.Count);
                foreach (var fb2File in fb2Files)
                {
                    epubFile = new EPubFile{ FlatStructure = Flat, EmbedStyles = EmbedStyles};
                    Reset();
                    if (string.IsNullOrEmpty(ResourcesPath))
                    {
                        epubFile.Transliterator.RuleFile = @".\Translit\translit.xml";
                    }
                    else
                    {
                        epubFile.Transliterator.RuleFile = string.Format(@"{0}\Translit\translit.xml",ResourcesPath);
                    }
                    Logger.Log.DebugFormat("Using transliteration rule file : {0}", epubFile.Transliterator.RuleFile);
                    if (Transliterate)
                    {
                        epubFile.TranslitMode = TranslitModeEnum.ExternalRuleFile;
                        if (!File.Exists(epubFile.Transliterator.RuleFile))
                        {
                            Console.WriteLine(string.Format("Unable to locate translation file {0}",epubFile.Transliterator.RuleFile));
                        }
                    }
                    else
                    {
                        epubFile.TranslitMode = TranslitModeEnum.None;
                    }
                    epubFile.TranliterateToc = TransliterateToc;
                    Logger.Log.DebugFormat("Transliteration mode : {0}", epubFile.TranslitMode);
                    images.RemoveAlpha = ConvertAlphaPng;
                    images.LoadFromBinarySection(fb2File.Images);
                    PassHeaderDataFromFb2ToEpub(epubFile, fb2File);
                    SetupCSS(epubFile);
                    SetupFonts(epubFile);
                    PassTextFromFb2ToEpub(epubFile, fb2File);
                    if (Fb2Info)
                    {
                        PassFb2InfoToEpub(epubFile, fb2File);
                    }
                    PassImagesDataFromFb2ToEpub(epubFile, fb2File);
                    Logger.Log.DebugFormat("Transliteration of sile names set to : {0}",TransliterateFileName);
                    if (TransliterateFileName)
                    {
                        outFileName = epubFile.Transliterator.Translate(outFileName, epubFile.TranslitMode);
                        Logger.Log.DebugFormat("New transliterated file name : {0}",outFileName);
                    }
                    string outFile = outFileName;
                    string outFolder = Path.GetDirectoryName(outFileName);
                    if (string.IsNullOrEmpty(outFolder))
                    {
                        Logger.Log.DebugFormat("Using output folder : {0}",OutPutPath);
                        outFolder = OutPutPath;
                        if (!string.IsNullOrEmpty(outFolder))
                        {
                            outFile = string.Format(@"{0}\{1}.epub", outFolder, Path.GetFileNameWithoutExtension(outFileName));    
                        }
                        
                    }
                    while (File.Exists(outFile) && (fb2File != fb2Files[0]))
                    {
                        Logger.Log.DebugFormat("{0} file exists , incrementing",outFile);
                        outFile = string.Format(@"{0}\{1}_{2}.epub", outFolder, Path.GetFileNameWithoutExtension(outFileName), count++);
                    }
                    //outFile.Replace('?','1' );
                    Logger.Log.DebugFormat("Final output file name : {0}",outFile);
                    Assembly asm = Assembly.GetEntryAssembly();
                    string version = "???";
                    if (asm != null)
                    {
                        version = asm.GetName().Version.ToString();
                    }
                    epubFile.AboutTexts.Add(string.Format("This book was generated by Lord KiRon's FB2EPUB converter version {0}.", version));
                    epubFile.AboutTexts.Add(string.Format("Эта книга создана при помощи конвертера FB2EPUB версии {0} написанного Lord KiRon.", version));
                    epubFile.AboutLinks.Add(@"http://www.fb2epub.net");
                    epubFile.AboutLinks.Add(@"https://code.google.com/p/fb2epub/");
                    epubFile.Generate(outFile);
                }
            }
            catch (Exception ex)
            {
                Logger.Log.ErrorFormat("Error saving file {0} : {1} - {2}", outFileName,ex.StackTrace, ex.ToString());
                throw;
            }
        }

        /// <summary>
        /// Passes FB2 info to the EPub file to be added at the end of the book
        /// </summary>
        /// <param name="epubFile">destination epub object</param>
        /// <param name="fb2File">source fb2 object</param>
        private void PassFb2InfoToEpub(EPubFile epubFile, FB2File fb2File)
        {
            BookDocument infoDocument = epubFile.AddDocument("FB2 Info");
            Fb2EpubInfoConverter infoConverter = new Fb2EpubInfoConverter();
            infoDocument.Content = infoConverter.ConvertInfo(fb2File);
            infoDocument.FileName = "fb2info.xhtml";
            infoDocument.DocumentType = GuideTypeEnum.Notes;
            infoDocument.Type = SectionTypeEnum.Text;
            infoDocument.NotPartOfNavigation = true;
        }


        private void SetupCSS(EPubFile epubFile)
        {
            string pathPreffix = @".\";
            if (!string.IsNullOrEmpty(ResourcesPath))
            {
                pathPreffix = ResourcesPath;
            }
            epubFile.CSSFiles.Add(new CSSFile { FileExtPath = string.Format(@"{0}\{1}", pathPreffix, @"CSS\default.css"), FileName = "default.css" });
        }

        private void SetupFonts(EPubFile epubFile)
        {
            if (Fonts == null)
            {
                Logger.Log.Warn("No fonts defined in configuration file.");
                return;
            }
            foreach (var font in Fonts)
            {
                foreach (var destination in font.Destinations)
                {
                    if (destination.Path == null)
                    {
                        destination.Path = string.Empty;
                    }
                    if (destination.Type == DestinationTypeEnum.Embedded && (destination.Path.Contains("%ResourceFolder%")))
                    {
                       destination.Path = destination.Path.Replace("%ResourceFolder%", ResourcesPath);                      
                    }
                }
                epubFile.AddFontObject(font);
            }
        }


        private void PassTextFromFb2ToEpub(EPubFile epubFile, FB2File fb2File)
        {
            // create second title page
            if ((fb2File.MainBody.Title!=null) &&(!string.IsNullOrEmpty(fb2File.MainBody.Title.ToString())))
            {
                string docTitle = string.Empty;
                docTitle = fb2File.MainBody.Title.ToString();
                Logger.Log.DebugFormat("Adding section : {0}", docTitle);
                BookDocument addTitlePage = epubFile.AddDocument(docTitle);
                addTitlePage.DocumentType = GuideTypeEnum.TitlePage;
                addTitlePage.Content = new Div();
                if (fb2File.MainBody.Title != null)
                {
                    addTitlePage.Content.Add(ConvertFromFb2TitleElement(fb2File.MainBody.Title, 2));
                }
                addTitlePage.NavigationParent = null;
                addTitlePage.FileName = string.Format("section{0}.xhtml", ++_sectionCounter);
                addTitlePage.NotPartOfNavigation = true;
            }

            BookDocument MainDocument = null;
            if (!string.IsNullOrEmpty(fb2File.MainBody.Name))
            {
                string docTitle = string.Empty;
                docTitle = fb2File.MainBody.Name;
                Logger.Log.DebugFormat("Adding section : {0}", docTitle);
                MainDocument = epubFile.AddDocument(docTitle);
                MainDocument.DocumentType = GuideTypeEnum.Text;
                MainDocument.Content = new Div();
                MainDocument.NavigationParent = null;
                MainDocument.FileName = string.Format("section{0}.xhtml", ++_sectionCounter);
            }

            if ((fb2File.MainBody.ImageName!= null) && !string.IsNullOrEmpty(fb2File.MainBody.ImageName.HRef))
            {
                if (MainDocument == null)
                {
                    string newDocTitle = ((fb2File.MainBody.Title!=null)&&(!string.IsNullOrEmpty(fb2File.MainBody.Title.ToString())))?fb2File.MainBody.Title.ToString():"main";
                    MainDocument = epubFile.AddDocument(newDocTitle);
                    MainDocument.DocumentType = GuideTypeEnum.Text;
                    MainDocument.Content = new Div();
                    MainDocument.NavigationParent = null;
                    MainDocument.FileName = string.Format("section{0}.xhtml", ++_sectionCounter);
                }
                if (fb2File.MainBody.ImageName.HRef != null)
                {
                    if (images.IsImageIdReal(fb2File.MainBody.ImageName.HRef))
                    {
                        Div enclosing = new Div(); // we use the enclosing so the user can style center it
                        enclosing.Add(ConvertFromFb2ImageElement(fb2File.MainBody.ImageName));
                        enclosing.Class.Value = "body_image";
                        MainDocument.Content.Add(enclosing);
                    }
                }

                
            }
            foreach (var ep in fb2File.MainBody.Epigraphs)
            {
                if (MainDocument == null)
                {
                    string newDocTitle = ((fb2File.MainBody.Title != null) && (!string.IsNullOrEmpty(fb2File.MainBody.Title.ToString()))) ? fb2File.MainBody.Title.ToString() : "main";
                    MainDocument = epubFile.AddDocument(newDocTitle);
                    MainDocument.DocumentType = GuideTypeEnum.Text;
                    MainDocument.Content = new Div();
                    MainDocument.NavigationParent = null;
                    MainDocument.FileName = string.Format("section{0}.xhtml", ++_sectionCounter);
                }
                MainDocument.Content.Add(ConvertFromFb2EpigraphElement(ep, 1));
            }

            Logger.Log.Debug("Adding main sections");
            foreach (var section in fb2File.MainBody.Sections)
            {
                AddSection(epubFile, section, MainDocument);
            }

            Logger.Log.Debug("Adding secondary bodies");
            foreach (var bodyItem in fb2File.Bodies)
            {
                if (bodyItem == fb2File.MainBody)
                {
                    continue;
                }
                AddSecondaryBody(epubFile,bodyItem);
            }
            referencesManager.RemoveInvalidAnchors();
            referencesManager.RemoveInvalidImages(fb2File.Images);
            referencesManager.RemapAnchors(epubFile);
        }

        private void AddSecondaryBody(EPubFile epubFile, BodyItem bodyItem)
        {
            GuideTypeEnum docType = GuideTypeEnum.Text;
            SectionTypeEnum sectionType = SectionTypeEnum.Text;
            bool notPartOfNavigation = false;
            if (bodyItem.Name == "notes" || bodyItem.Name == "footnotes") // treat "standard" FBE created notes
            {
                docType = GuideTypeEnum.Glossary;
                sectionType = SectionTypeEnum.Links;
                notPartOfNavigation = true;

            }
            string docTitle = string.Empty;
            if (string.IsNullOrEmpty(bodyItem.Name))
            {
                if (bodyItem.Title != null)
                {
                    docTitle = bodyItem.Title.ToString();
                }
            }
            else
            {
                docTitle = bodyItem.Name;
            }
            Logger.Log.DebugFormat("Adding section : {0}", docTitle);
            BookDocument sectionDocument = null;
            sectionDocument = epubFile.AddDocument(docTitle);
            sectionDocument.DocumentType = docType;
            sectionDocument.Type = sectionType;
            sectionDocument.Content = new Div();
            if (bodyItem.Title != null)
            {
                sectionDocument.Content.Add(ConvertFromFb2TitleElement(bodyItem.Title, 1));
            }
            sectionDocument.NavigationParent = null;
            sectionDocument.NotPartOfNavigation = notPartOfNavigation;
            sectionDocument.FileName = string.Format("section{0}.xhtml", ++_sectionCounter);
            Logger.Log.Debug("Adding sub-sections");
            foreach (var section in bodyItem.Sections)
            {
                AddSection(epubFile, section, sectionDocument);
            }
        }

        private List<IXHTMLItem> ConvertFromFb2SecondaryBody(BodyItem bodyItem)
        {
            long documentSize = 0;
            List<IXHTMLItem> list2Return = new List<IXHTMLItem>();
            Div body = new Div();
            foreach (var section in bodyItem.Sections)
            {
                foreach (var element in ConvertFromFb2SectionElement(section, 2, true))
                {
                    long itemSize = element.EstimateSize();
                    if (documentSize + itemSize >= MaxSize)
                    {
                        list2Return.Add(body);
                        body = new Div();
                        documentSize = 0;
                    }
                    if (itemSize < MaxSize)
                    {
                        documentSize += itemSize;
                        body.Add(element);
                    }
                    else
                    {
                        if (element is Div) // if the item that bigger than max size is Div block
                        {
                            foreach (var splitedItem in SplitDiv(element as Div, documentSize))
                            {
                                itemSize = splitedItem.EstimateSize();
                                if (documentSize + itemSize >= MaxSize)
                                {
                                    IBlockElement oldContent = body;
                                    list2Return.Add(body);
                                    body = new Div();
                                    body.Class.Value = oldContent.Class.Value;
                                    body.Language.Value = oldContent.Language.Value;
                                    documentSize = 0;
                                }
                                if (itemSize < MaxSize) // if we can "fit" element into a max size XHTML document
                                {
                                    documentSize += itemSize;
                                    body.Add(splitedItem);
                                }
                            }
                        }

                    }
                }
            }
            list2Return.Add(body);
            //return body;
            return list2Return;
        }


        private void AddSection(EPubFile epubFile, SectionItem section,BookDocument navParent)
        {
            string docTitle = string.Empty;
            if (section.Title != null)
            {
                docTitle = section.Title.ToString();
            }
            Logger.Log.DebugFormat("Adding section : {0}", docTitle);
            BookDocument sectionDocument = null;
            bool firstDocumentOfSplit = true;
            foreach (var subitem in ConvertFromFb2SectionElement(section, 1,false))
            {
                sectionDocument = epubFile.AddDocument(docTitle);
                sectionDocument.DocumentType = (navParent==null)?GuideTypeEnum.Text:navParent.DocumentType;
                sectionDocument.Type = (navParent == null) ? SectionTypeEnum.Text: navParent.Type;
                sectionDocument.Content= subitem;
                sectionDocument.NavigationParent = navParent;
                sectionDocument.FileName = string.Format("section{0}.xhtml",++_sectionCounter);
                if (!firstDocumentOfSplit || (sectionDocument.Type == SectionTypeEnum.Links))
                {
                    sectionDocument.NotPartOfNavigation = true;
                }
                firstDocumentOfSplit = false;
            }
            Logger.Log.Debug("Adding sub-sections");
            foreach (var subSection in section.SubSections)
            {
                AddSection(epubFile,subSection,sectionDocument);
            }
        }


        private void PassImagesDataFromFb2ToEpub(EPubFile epubFile, FB2File fb2File)
        {
            images.ConvertFb2ToEpubImages(fb2File.Images,epubFile.Images);
        }

        private void PassHeaderDataFromFb2ToEpub(EPubFile epubFile, FB2File fb2File)
        {
            epubFile.Title.Languages.Clear();
            epubFile.Title.Creators.Clear();
            epubFile.Title.Contributors.Clear();
            epubFile.Title.Subjects.Clear();
            epubFile.Title.Identifiers.Clear();

            if (fb2File.MainBody == null)
            {
                throw new NullReferenceException("MainBody section of the file passed is null");
            }

            Logger.Log.Debug("Passing header data from FB2 to EPUB");
            // cReate new Title page
            epubFile.TitlePage = new TitlePageFile();

            // in case main body title is not defined (empty) 
            if ((fb2File.TitleInfo != null) && (fb2File.TitleInfo.BookTitle != null))
            {
                epubFile.TitlePage.BookTitle = fb2File.TitleInfo.BookTitle.Text;
            }


            // Pass all sequences 
            epubFile.AllSequences.Clear();
            Title bookTitle = new Title();

            if (fb2File.TitleInfo != null)
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

                // Getting information from FB2 Title section
                if (fb2File.TitleInfo.BookTitle != null)
                {
                    bookTitle.TitleName =
                        //epubFile.Transliterator.Translate(AddSequencesAbb(epubFile, fb2File.TitleInfo),
                        //                                  epubFile.TranslitMode);
                    epubFile.Transliterator.Translate(FormatBookTitle( fb2File.TitleInfo),
                                                          epubFile.TranslitMode);
                    bookTitle.Language = string.IsNullOrEmpty(fb2File.TitleInfo.BookTitle.Language)
                                             ? fb2File.TitleInfo.Language
                                             : fb2File.TitleInfo.BookTitle.Language;
                }
                epubFile.Title.BookTitles.Add(bookTitle);

                epubFile.Title.Languages.Add(fb2File.TitleInfo.Language);

                if (fb2File.TitleInfo.Annotation != null)
                {
                    epubFile.Title.Description = fb2File.TitleInfo.Annotation.ToString();
                    epubFile.AnnotationPage = new AnnotationPageFile();
                    epubFile.AnnotationPage.BookAnnotation =
                        ConvertFromFb2AnnotationElement(fb2File.TitleInfo.Annotation, 1);
                }


                // add authors
                foreach (var author in fb2File.TitleInfo.BookAuthors)
                {
                    PersoneWithRole person = new PersoneWithRole();
                    string authorString = GenerateAuthorString(author);
                    person.PersonName = epubFile.Transliterator.Translate(authorString, epubFile.TranslitMode);
                    person.FileAs = GenerateFileAsString(author);
                    person.Role = RolesEnum.Author;
                    person.Language = fb2File.TitleInfo.Language;
                    epubFile.Title.Creators.Add(person);

                    // add authors to Title page
                    epubFile.TitlePage.Authors.Add(authorString);
                }


                foreach (var translator in fb2File.TitleInfo.Translators)
                {
                    PersoneWithRole person = new PersoneWithRole();
                    person.PersonName = epubFile.Transliterator.Translate(GenerateAuthorString(translator),
                                                                          epubFile.TranslitMode);
                    person.FileAs = GenerateFileAsString(translator);
                    person.Role = RolesEnum.Translator;
                    person.Language = fb2File.TitleInfo.Language;
                    epubFile.Title.Contributors.Add(person);
                }

                foreach (var genre in fb2File.TitleInfo.Genres)
                {
                    Subject item = new Subject();
                    item.SubjectInfo = epubFile.Transliterator.Translate(Fb2GenreToDescription(genre.Genre),
                                                                         epubFile.TranslitMode);
                    epubFile.Title.Subjects.Add(item);
                }
            }

            // Getting information from FB2 document section
            Identifier bookId = new Identifier();
            if (!string.IsNullOrEmpty(fb2File.DocumentInfo.ID))
            {
                bookId.IdentifierName = fb2File.DocumentInfo.ID;
            }
            else
            {
                bookId.IdentifierName = Guid.NewGuid().ToString();
            }
            bookId.ID = "FB2BookID";
            bookId.Scheme = "URI";
            epubFile.Title.Identifiers.Add(bookId);

            if ((fb2File.DocumentInfo.SourceOCR != null) && !string.IsNullOrEmpty(fb2File.DocumentInfo.SourceOCR.Text))
            {
                epubFile.Title.Source = new Source() {SourceData = fb2File.DocumentInfo.SourceOCR.Text};
            }

            foreach (var docAuthor in fb2File.DocumentInfo.DocumentAuthors)
            {
                PersoneWithRole person = new PersoneWithRole();
                person.PersonName = epubFile.Transliterator.Translate(GenerateAuthorString(docAuthor), epubFile.TranslitMode);
                person.FileAs = GenerateFileAsString(docAuthor);
                person.Role = RolesEnum.Adapter;
                if (fb2File.TitleInfo != null)
                {
                    person.Language = fb2File.TitleInfo.Language;                    
                }
                epubFile.Title.Contributors.Add(person);                
            }

            // Getting information from FB2 Source Title Info section
            if ((fb2File.SourceTitleInfo.BookTitle != null) && !string.IsNullOrEmpty(fb2File.SourceTitleInfo.BookTitle.Text))
            {
                bookTitle = new Title();
                bookTitle.TitleName= epubFile.Transliterator.Translate(fb2File.SourceTitleInfo.BookTitle.Text,epubFile.TranslitMode);
                bookTitle.Language = string.IsNullOrEmpty(fb2File.SourceTitleInfo.BookTitle.Language)&&(fb2File.TitleInfo != null) ? fb2File.TitleInfo.Language : fb2File.SourceTitleInfo.BookTitle.Language;
                epubFile.Title.BookTitles.Add(bookTitle);

                epubFile.Title.Languages.Add(fb2File.SourceTitleInfo.Language);
            }

            // add authors
            foreach (var author in fb2File.SourceTitleInfo.BookAuthors)
            {
                PersoneWithRole person = new PersoneWithRole();
                person.PersonName = epubFile.Transliterator.Translate(string.Format("{0} {1} {2}", author.FirstName, author.MiddleName, author.LastName),epubFile.TranslitMode);
                person.FileAs = GenerateFileAsString(author);
                person.Role = RolesEnum.Author;
                person.Language = fb2File.SourceTitleInfo.Language;
                epubFile.Title.Creators.Add(person);
            }

            foreach (var translator in fb2File.SourceTitleInfo.Translators)
            {
                PersoneWithRole person = new PersoneWithRole();
                person.PersonName = epubFile.Transliterator.Translate(string.Format("{0} {1} {2}", translator.FirstName, translator.MiddleName, translator.LastName),epubFile.TranslitMode);
                person.FileAs = GenerateFileAsString(translator);
                person.Role = RolesEnum.Translator;
                person.Language = fb2File.SourceTitleInfo.Language;
                epubFile.Title.Contributors.Add(person);
            }


            foreach (var genre in fb2File.SourceTitleInfo.Genres)
            {
                Subject item = new Subject();
                item.SubjectInfo = epubFile.Transliterator.Translate(Fb2GenreToDescription(genre.Genre),epubFile.TranslitMode);
                if (epubFile.Title.Subjects.Contains(item))
                {
                    epubFile.Title.Subjects.Add(item);                    
                }

            }


            if (fb2File.PublishInfo.BookName != null)
            {
                bookTitle = new Title();
                bookTitle.TitleName = epubFile.Transliterator.Translate(fb2File.PublishInfo.BookName.Text,epubFile.TranslitMode);
                bookTitle.Language = !string.IsNullOrEmpty(fb2File.PublishInfo.BookName.Language) ? fb2File.PublishInfo.BookName.Language : fb2File.TitleInfo.Language;
                epubFile.Title.BookTitles.Add(bookTitle);
            }


            if (fb2File.PublishInfo.ISBN != null)
            {
                bookId = new Identifier
                             {
                                 IdentifierName = fb2File.PublishInfo.ISBN.Text,
                                 ID = "BookISBN",
                                 Scheme = "ISBN"
                             };
                epubFile.Title.Identifiers.Add(bookId);
            }


            if (fb2File.PublishInfo.Publisher != null )
            {
                epubFile.Title.Publisher.PublisherName = epubFile.Transliterator.Translate(fb2File.PublishInfo.Publisher.Text,epubFile.TranslitMode);
            }


            try
            {
                if (fb2File.PublishInfo.Year.HasValue)
                {
                    DateTime date = new DateTime(fb2File.PublishInfo.Year.Value, 1, 1);
                    epubFile.Title.DatePublished = date;
                }
            }
            catch (FormatException ex)
            {
                Logger.Log.DebugFormat("Date reading format exeception: {0}",ex.ToString());
            }
            catch (Exception exAll)
            {
                Logger.Log.ErrorFormat("Date reading exeception: {0}", exAll.ToString());
            }

            // if we have at least one coverpage image
            if ((fb2File.TitleInfo.Cover != null) && (fb2File.TitleInfo.Cover.HasImages()) && (fb2File.TitleInfo.Cover.CoverpageImages[0].HRef != null))
            {
                // we add just first one 
                epubFile.AddCoverImage(fb2File.TitleInfo.Cover.CoverpageImages[0].HRef);
                images.ImageIdUsed(fb2File.TitleInfo.Cover.CoverpageImages[0].HRef);
            }
                        
            epubFile.Title.DateFileCreation = DateTime.Now;


        }

        private string GenerateFileAsString(AuthorType author)
        {
            ProcessAuthorFormat processor = new ProcessAuthorFormat();
            processor.Format = FileAsFormat;
            return processor.GenerateAuthorString(author);

        }

        private string GenerateAuthorString(AuthorType author)
        {
            ProcessAuthorFormat processor = new ProcessAuthorFormat();
            processor.Format = AuthorFormat;
            return processor.GenerateAuthorString(author);
        }



        private string FormatBookTitle(ItemTitleInfo titleInfo)
        {
            ProcessSeqFormatString formatTitle = new ProcessSeqFormatString();
            formatTitle.BookTitleFormatSeqNum = SequenceFormat;
            formatTitle.BookTitleFormatNoSeqNum = NoSequenceFormat;
            formatTitle.BookTitleFormatNoSeries = NoSeriesFormat;

            String rc;
            if ((titleInfo.Sequences.Count > 0) && AddSeqToTitle)
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


        private static List<string> GetSequencesAsStrings(SequenceType seq)
        {
            List<string> allSequences =  new List<string>();
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
                    foreach (var subSection in seq.SubSections)
                    {
                        foreach (var asString in GetSequencesAsStrings(subSection))
                        {
                            allSequences.Add(asString);
                        }
                    }
                }
            }
            return allSequences;
        }


        private static string Fb2GenreToDescription(string genre)
        {
            // TODO: implement real description conversion
            return genre;
        }


        /// <summary>
        /// Convert from FB2 section element into EPUB data structures
        /// </summary>
        /// <param name="section">FB2 section</param>
        /// <param name="recursionLevel">level of section inclusion 1 - top</param>
        /// <param name="linkSection">true if this is a special link section like "notes"</param>
        /// <returns></returns>
        private List<IXHTMLItem> ConvertFromFb2SectionElement(SectionItem section, int recursionLevel, bool linkSection)
        {
            List<IXHTMLItem> resList = new List<IXHTMLItem>();
            Logger.Log.Debug("Convering section");

            IBlockElement content = new Div();
            long documentSize = 0;

            content.Class.Value = string.Format("section{0}", recursionLevel);

            content.ID.Value = referencesManager.AddIdUsed(section.ID, content);

            if (section.Lang != null)
            {
                content.Language.Value = section.Lang;
            }


            // Load Title
            if (section.Title != null)
            {
                IXHTMLItem titleItem = null;
                if (!linkSection)
                {
                    titleItem = ConvertFromFb2TitleElement(section.Title, recursionLevel+1);
                }
                else
                {
                    titleItem = ConvertFromFb2LinkSection(section);
                }
                if (titleItem != null)
                {
                    long itemSize = titleItem.EstimateSize();
                    if (documentSize + itemSize >= MaxSize)
                    {
                        IBlockElement oldContent = content;
                        resList.Add(content);
                        content = new Div();
                        content.Class.Value = oldContent.Class.Value;
                        content.Language.Value = oldContent.Language.Value;
                        documentSize = 0;
                    }
                    if (itemSize < MaxSize) // if we can "fit" element into a max size XHTML document
                    {
                        documentSize += itemSize;
                        content.Add(titleItem);                        
                    }
                    else
                    {
                        if (titleItem is Div) // if the item that bigger than max size is Div block
                        {
                            foreach (var splitedItem in SplitDiv(titleItem as Div, documentSize))
                            {
                                itemSize = splitedItem.EstimateSize();
                                if (documentSize + itemSize >= MaxSize)
                                {
                                    IBlockElement oldContent = content;
                                    resList.Add(content);
                                    content = new Div();
                                    content.Class.Value = oldContent.Class.Value;
                                    content.Language.Value = oldContent.Language.Value;
                                    documentSize = 0;
                                }
                                if (itemSize < MaxSize) // if we can "fit" element into a max size XHTML document
                                {
                                    documentSize += itemSize;
                                    content.Add(splitedItem);
                                }
                            }
                        }
                    }
                }
            }

            // Load epigraphs
            foreach (var epigraph in section.Epigraphs)
            {

                IXHTMLItem epigraphItem = ConvertFromFb2EpigraphElement(epigraph, recursionLevel + 1);
                long itemSize = epigraphItem.EstimateSize();
                if (documentSize + itemSize >= MaxSize)
                {
                    IBlockElement oldContent = content;
                    resList.Add(content);
                    content = new Div();
                    content.Class.Value = oldContent.Class.Value;
                    content.Language.Value = oldContent.Language.Value;
                    documentSize = 0;
                }
                if (itemSize < MaxSize) // if we can "fit" element into a max size XHTML document
                {
                    documentSize += itemSize;
                    content.Add(epigraphItem);
                }
                else
                {
                    if (epigraphItem is Div) // if the item that bigger than max size is Div block
                    {
                        foreach (var splitedItem in SplitDiv(epigraphItem as Div, documentSize))
                        {
                            itemSize = splitedItem.EstimateSize();
                            if (documentSize + itemSize >= MaxSize)
                            {
                                IBlockElement oldContent = content;
                                resList.Add(content);
                                content = new Div();
                                content.Class.Value = oldContent.Class.Value;
                                content.Language.Value = oldContent.Language.Value;
                                documentSize = 0;
                            }
                            if (itemSize < MaxSize) // if we can "fit" element into a max size XHTML document
                            {
                                documentSize += itemSize;
                                content.Add(splitedItem);
                            }
                        }
                    }
                }
            }

            // Load section image
            if ((section.SectionImage != null) && images.HasRealImages())
            {
                if (section.SectionImage.HRef != null)
                {
                    if (images.IsImageIdReal(section.SectionImage.HRef))
                    {
                        Div container = new Div();
                        Image sectionImagemage = new Image();
                        if (section.SectionImage.AltText != null)
                        {
                            sectionImagemage.Alt.Value = section.SectionImage.AltText;
                        }
                        sectionImagemage.Source.Value = referencesManager.AddImageRefferenced(section.SectionImage, sectionImagemage);
                        sectionImagemage.ID.Value = referencesManager.AddIdUsed(section.SectionImage.ID,
                                                                                sectionImagemage);
                        if (section.SectionImage.Title != null)
                        {
                            sectionImagemage.Title.Value = section.SectionImage.Title;
                        }
                        container.Class.Value = "section_image";
                        container.Add(sectionImagemage);
                        long itemSize = container.EstimateSize();
                        if (documentSize + itemSize >= MaxSize)
                        {
                            IBlockElement oldContent = content;
                            resList.Add(content);
                            content = new Div();
                            content.Class.Value = oldContent.Class.Value;
                            content.Language.Value = oldContent.Language.Value;
                            documentSize = 0;
                        }
                        documentSize += itemSize;
                        content.Add(container);
                        images.ImageIdUsed(section.SectionImage.HRef);
                    }
                }
            }

            // Load annotations
            if (section.Annotation != null)
            {
                IXHTMLItem annotationItem = ConvertFromFb2AnnotationElement(section.Annotation,
                                                                                   recursionLevel + 1);
                long itemSize = annotationItem.EstimateSize();
                if (documentSize + itemSize >= MaxSize)
                {
                    IBlockElement oldContent = content;
                    resList.Add(content);
                    content = new Div();
                    content.Class.Value = oldContent.Class.Value;
                    content.Language.Value = oldContent.Language.Value;
                    documentSize = 0;
                }
                if (itemSize < MaxSize) // if we can "fit" element into a max size XHTML document
                {
                    documentSize += itemSize;
                    content.Add(annotationItem);
                }
                else
                {
                    if (annotationItem is Div) // if the item that bigger than max size is Div block
                    {
                        foreach (var splitedItem in SplitDiv(annotationItem as Div, documentSize))
                        {
                            itemSize = splitedItem.EstimateSize();
                            if (documentSize + itemSize >= MaxSize)
                            {
                                IBlockElement oldContent = content;
                                resList.Add(content);
                                content = new Div();
                                content.Class.Value = oldContent.Class.Value;
                                content.Language.Value = oldContent.Language.Value;
                                documentSize = 0;
                            }
                            if (itemSize < MaxSize) // if we can "fit" element into a max size XHTML document
                            {
                                documentSize += itemSize;
                                content.Add(splitedItem);
                            }
                        }
                    }
                }
            }

            // Parse all elements only if section has no sub section
            if (section.SubSections.Count == 0)
            {
                foreach (var item in section.Content)
                {
                    IXHTMLItem newItem = null;
                    if (item is SubTitleItem)
                    {
                        newItem = ConvertFromFb2SubtitleElement(item as SubTitleItem, recursionLevel + 1);
                    }
                    else if (item is ParagraphItem)
                    {
                        newItem = ConvertFromFb2ParagraphElement(item as ParagraphItem,
                                                                   ParagraphConvTargetEnum.Paragraph);
                    }
                    else if (item is PoemItem)
                    {
                        newItem = ConvertFromFb2PoemElement(item as PoemItem, recursionLevel + 1);
                    }
                    else if (item is CiteItem)
                    {
                        newItem = ConvertFromFb2Citation(item as CiteItem, recursionLevel + 1);
                    }
                    else if (item is EmptyLineItem)
                    {
                        newItem = ConvertToEmptyLine();
                    }
                    else if (item is TableItem)
                    {
                        newItem = ConvertFromFb2TableElement(item as TableItem);
                    }
                    else if ((item is ImageItem) && (item != section.SectionImage) && images.HasRealImages())
                    {
                        ImageItem fb2Img = item as ImageItem;
                        if (fb2Img.HRef != null)
                        {
                            if (images.IsImageIdReal(fb2Img.HRef))
                            {
                                Div enclosing = new Div(); // we use the enclosing so the user can style center it
                                enclosing.Add(ConvertFromFb2ImageElement(fb2Img));
                                enclosing.Class.Value = "section_image";
                                newItem = enclosing;
                            }
                        }
                    }
                    if (newItem != null)
                    {
                        long itemSize = newItem.EstimateSize();
                        if (documentSize + itemSize >= MaxSize)
                        {
                            IBlockElement oldContent = content;
                            resList.Add(content);
                            content = new Div();
                            content.Class.Value = oldContent.Class.Value;
                            content.Language.Value = oldContent.Language.Value;
                            documentSize = 0;
                        }
                        if (itemSize < MaxSize) // if we can "fit" element into a max size XHTML document
                        {
                            documentSize += itemSize;
                            content.Add(newItem);
                        }
                        else
                        {
                            if (newItem is Div) // if the item that bigger than max size is Div block
                            {
                                foreach (var splitedItem in SplitDiv(newItem as Div, documentSize)) 
                                {
                                    itemSize = splitedItem.EstimateSize();
                                    if (documentSize + itemSize >= MaxSize)
                                    {
                                        IBlockElement oldContent = content;
                                        resList.Add(content);
                                        content = new Div();
                                        content.Class.Value = oldContent.Class.Value;
                                        content.Language.Value = oldContent.Language.Value;
                                        documentSize = 0;
                                    }
                                    if (itemSize < MaxSize) // if we can "fit" element into a max size XHTML document
                                    {
                                        documentSize += itemSize;
                                        content.Add(splitedItem);
                                    }                                    
                                }
                            }   
                        }
                    }
                }
            }

            resList.Add(content);
            return resList;
        }

        private IXHTMLItem ConvertFromFb2ImageElement(ImageItem fb2Img)
        {
            Image image = new Image();
            if (fb2Img.AltText != null)
            {
                image.Alt.Value = fb2Img.AltText;
            }
            image.Source.Value = referencesManager.AddImageRefferenced(fb2Img, image);

            image.ID.Value = referencesManager.AddIdUsed(fb2Img.ID, image);
            if (fb2Img.Title != null)
            {
                image.Title.Value = fb2Img.Title;
            }
            images.ImageIdUsed(fb2Img.HRef);
            return image;
        }

        private List<IXHTMLItem> SplitDiv(Div div,long documentSize)
        {
            List<IXHTMLItem> resList = new List<IXHTMLItem>();
            long newDocumentSize = documentSize;
            Div container = new Div();
            container.ID.Value = div.ID.Value;
            foreach (var element in div.SubElements())
            {
                long elementSize = element.EstimateSize();
                if (elementSize + newDocumentSize >= MaxSize)
                {
                    resList.Add(container);
                    container = new Div();
                    container.Class.Value = div.Class.Value;
                    container.Language.Value = div.Language.Value;
                    newDocumentSize = 0;
                }
                if (elementSize < MaxSize)
                {
                    container.Add(element);
                    newDocumentSize += elementSize;                    
                }
            }
            resList.Add(container);
            return resList;
        }


        /// <summary>
        /// Converts FB2 Title to EPub Title page
        /// </summary>
        /// <param name="titleItem"></param>
        /// <param name="titleLevel"></param>
        /// <returns></returns>
        private Div ConvertFromFb2TitleElement(TitleItem titleItem, int titleLevel)
        {
            Div title = new Div();
            foreach (var fb2TextItem in titleItem.TitleData)
            {
                if (fb2TextItem is ParagraphItem)
                {
                    ParagraphConvTargetEnum paragraphStyle = GetParagraphStyleByLevel(titleLevel);
                    title.Add(ConvertFromFb2ParagraphElement((ParagraphItem)fb2TextItem, paragraphStyle));
                }
                else if (fb2TextItem is EmptyLineItem)
                {
                    title.Add(ConvertToEmptyLine());
                }
                else
                {
                    Debug.WriteLine(string.Format("invalid type in Title - {0}", fb2TextItem.GetType()));
                }
            }
            string itemClass = string.Format("title{0}", titleLevel);
            title.Class.Value = itemClass;
            return title;
        }

        private static IXHTMLItem ConvertToEmptyLine()
        {
            Paragraph el = new Paragraph();
            el.Add(new SimpleEPubText { Text = "\u00A0" });
            el.Class.Value = "empty-line";
            return el;
        }

        private static ParagraphConvTargetEnum GetParagraphStyleByLevel(int titleLevel)
        {
            ParagraphConvTargetEnum paragraphStyle = ParagraphConvTargetEnum.H6;
            switch (titleLevel)
            {
                case 1:
                    paragraphStyle = ParagraphConvTargetEnum.H1;
                    break;
                case 2:
                    paragraphStyle = ParagraphConvTargetEnum.H2;
                    break;
                case 3:
                    paragraphStyle = ParagraphConvTargetEnum.H3;
                    break;
                case 4:
                    paragraphStyle = ParagraphConvTargetEnum.H4;
                    break;
                case 5:
                    paragraphStyle = ParagraphConvTargetEnum.H5;
                    break;
            }
            return paragraphStyle;
        }

        /// <summary>
        /// Converts FB2 Paragraph to EPUB paragraph
        /// </summary>
        /// <param name="fb2ParagraphItem"></param>
        /// <param name="resultType">type of the resulting block container in EPUB</param>
        /// <returns></returns>
        private IBlockElement ConvertFromFb2ParagraphElement(ParagraphItem fb2ParagraphItem, ParagraphConvTargetEnum resultType)
        {
            IBlockElement paragraph = CreateBlock(resultType);

            foreach (var item in fb2ParagraphItem.ParagraphData)
            {
                if (item is SimpleText)
                {
                    foreach (var s in ConvertFromFb2SimpleTextElement(item))
                    {
                        paragraph.Add(s);                        
                    }
                }
                else if (item is InlineImageItem)
                {
                    // if no image data - do not insert link
                    if (images.HasRealImages())
                    {
                        InlineImageItem inlineItem = item as InlineImageItem;
                        if (images.IsImageIdReal(inlineItem.HRef))
                        {
                            paragraph.Add(ConvertFromFb2InlineImageElement(inlineItem));
                        }
                        images.ImageIdUsed(inlineItem.HRef);
                    }
                }
                else if (item is InternalLinkItem)
                {
                    foreach (var s in ConvertFromFb2InternalLinkElement((InternalLinkItem)item))
                    {
                        paragraph.Add(s);    
                    }
                }
            }

            paragraph.ID.Value = referencesManager.AddIdUsed(fb2ParagraphItem.ID, paragraph);

            return paragraph;
        }


        /// <summary>
        /// Creates block element based on paragraph type
        /// </summary>
        /// <param name="type"></param>
        /// <returns></returns>
        private static IBlockElement CreateBlock(ParagraphConvTargetEnum type)
        {
            IBlockElement paragraph;
            switch (type)
            {
                case ParagraphConvTargetEnum.H1:
                    paragraph = new H1();
                    break;
                case ParagraphConvTargetEnum.H2:
                    paragraph = new H2();
                    break;
                case ParagraphConvTargetEnum.H3:
                    paragraph = new H3();
                    break;
                case ParagraphConvTargetEnum.H4:
                    paragraph = new H4();
                    break;
                case ParagraphConvTargetEnum.H5:
                    paragraph = new H5();
                    break;
                case ParagraphConvTargetEnum.H6:
                    paragraph = new H6();
                    break;
                default: // Paragraph or anything else
                    paragraph = new Paragraph();
                    break;

            }
            return paragraph;
        }

        /// <summary>
        /// Converts FB2 simple text 
        /// ( simple text is normal text or text with one of the "styles")
        /// </summary>
        /// <param name="styleItem"></param>
        /// <returns></returns>
        private List<IXHTMLItem> ConvertFromFb2SimpleTextElement(StyleType styleItem)
        {

            if (styleItem == null)
            {
                throw new ArgumentNullException("styleItem");
            }

            List<IXHTMLItem> list = new List<IXHTMLItem>();

            if (styleItem is SimpleText)
            {
                SimpleText text = styleItem as SimpleText;
                switch (text.Style)
                {
                    case FB2Library.Elements.TextStyles.Normal:
                        if (text.HasChildren)
                        {
                            foreach (var child in text.Children)
                            {
                                foreach (var item in ConvertFromFb2SimpleTextElement(child))
                                {
                                    list.Add(item);                                    
                                }
                            }
                        }
                        else
                        {
                            list.Add(new SimpleEPubText {Text = text.Text});
                        }
                        break;
                    case FB2Library.Elements.TextStyles.Code:
                        CodeText code = new CodeText();
                        if (text.HasChildren)
                        {
                            foreach (var child in text.Children)
                            {
                                foreach (var item in ConvertFromFb2SimpleTextElement(child))
                                {
                                    code.Add(item);                                    
                                }

                            }
                        }
                        else
                        {
                            code.Add(new SimpleEPubText() {Text = text.Text});
                        }
                        list.Add(code);
                        break;
                    case FB2Library.Elements.TextStyles.Emphasis:
                        EmphasisedText emph = new EmphasisedText();
                        if (text.HasChildren)
                        {
                            foreach (var child in text.Children)
                            {
                                foreach (var item in ConvertFromFb2SimpleTextElement(child))
                                {
                                    emph.Add(item);                                    
                                }
                            }
                        }
                        else
                        {
                            emph.Add(new SimpleEPubText() {Text = text.Text});
                        }
                        list.Add(emph);
                        break;
                    case FB2Library.Elements.TextStyles.Strong:
                        Strong str = new Strong();
                        if (text.HasChildren)
                        {
                            foreach (var child in text.Children)
                            {
                                foreach (var item in ConvertFromFb2SimpleTextElement(child))
                                {
                                    str.Add(item);                                    
                                }
                            }
                        }
                        else
                        {
                            str.Add(new SimpleEPubText() {Text = text.Text});
                        }
                        list.Add(str);
                        break;
                    case FB2Library.Elements.TextStyles.Sub:
                        Sub sub = new Sub();
                        if (text.HasChildren)
                        {
                            foreach (var child in text.Children)
                            {
                                foreach (var item in ConvertFromFb2SimpleTextElement(child))
                                {
                                    sub.Add(item);        
                                }
                            }
                        }
                        else
                        {
                            sub.Add(new SimpleEPubText() {Text = text.Text});
                        }
                        list.Add(sub);
                        break;
                    case FB2Library.Elements.TextStyles.Sup:
                        Sup sup = new Sup();
                        if (text.HasChildren)
                        {
                            foreach (var child in text.Children)
                            {
                                foreach (var item in ConvertFromFb2SimpleTextElement(child))
                                {
                                    sup.Add(item);    
                                }
                            }
                        }
                        else
                        {
                            sup.Add(new SimpleEPubText() {Text = text.Text});
                        }
                        list.Add(sup);
                        break;
                }
            }
            else if (styleItem is InternalLinkItem)
            {
                foreach (var item in ConvertFromFb2InternalLinkElement(styleItem as InternalLinkItem))
                {
                    list.Add(item);    
                }
            }
            else if (styleItem is InlineImageItem)
            {
                list.Add(ConvertFromFb2InlineImageElement(styleItem as InlineImageItem));
            }

            return list;
        }

        /// <summary>
        /// Converts FB2 inline image
        /// </summary>
        /// <param name="item"></param>
        /// <returns></returns>
        private IXHTMLItem ConvertFromFb2InlineImageElement(InlineImageItem item)
        {
            Image img = new Image();
            if (item.AltText != null)
            {
                img.Alt.Value = item.AltText;
            }

            img.Source.Value = referencesManager.AddImageRefferenced(item,img);

            img.Class.Value = "int_image";
            return img;
        }

        /// <summary>
        /// Convert FB2 internal link 
        /// </summary>
        /// <param name="item"></param>
        /// <returns></returns>
        private List<IXHTMLItem> ConvertFromFb2InternalLinkElement(InternalLinkItem item)
        {
            List<IXHTMLItem> list = new List<IXHTMLItem>();
            if (!string.IsNullOrEmpty(item.HRef) )
            {
                Anchor anchor = new Anchor();
                bool internalLink = false;
                if (!ReferencesUtils.IsExternalLink(item.HRef))
                {
                    if (item.HRef.StartsWith("#"))
                    {
                        item.HRef = item.HRef.Substring(1);
                    }
                    item.HRef = referencesManager.EnsureGoodID(item.HRef);
                    internalLink = true;
                }              
                anchor.HRef.Value = item.HRef;
                if (internalLink)
                {
                    referencesManager.AddReference(item.HRef, anchor);                    
                }
                if (item.LinkText != null)
                {
                    foreach (var s in ConvertFromFb2SimpleTextElement(item.LinkText))
                    {
                        s.Parent = anchor;
                        anchor.Content.Add(s);    
                    }
                }
                list.Add(anchor);
                return list;
            }
            // deal with the case of invalid InternalLinkItem element without hReference
            // in this case we convert link to paragraph
            //Paragraph p = new Paragraph();
            //p.Add(ConvertFromFb2SimpleTextElement(item.LinkText));
            //return p;
            return ConvertFromFb2SimpleTextElement(item.LinkText);
        }



        private IXHTMLItem ConvertFromFb2LinkSection(SectionItem section)
        {
            IBlockElement content = new Paragraph();
            Anchor a = new Anchor();
            foreach (var item in ConvertFromFb2TitleElement(section.Title, 7).SubElements())
            {
                if (item is IInlineItem)
                {
                    a.Add(item);
                }
                else if (item is SimpleEPubText)
                {
                    a.Add(item);
                }
                else
                {
                    foreach (var subElement in item.SubElements())
                    {
                        a.Add(subElement);
                    }
                }
            }
            string newId = referencesManager.EnsureGoodID(section.ID);
            a.HRef.Value = string.Format("{0}_back", newId);
            if (a.HRef.Value.StartsWith( "_back") == false)
            {
                //a.ID.Value = newId; // no need since the Id usualy located on section level
                a.Class.Value = "note_anchor";
                referencesManager.AddBackReference(a.HRef.Value, a);
                content.Add(a);                
            }
            content.Class.Value = "note_section";
            return content;

        }

        private IXHTMLItem ConvertFromFb2PoemElement(PoemItem poemItem, int level)
        {
            Div poemContent = new Div();

            if (poemItem.Title != null)
            {
                poemContent.Add(ConvertFromFb2TitleElement(poemItem.Title, level));
            }

            foreach (var epigraph in poemItem.Epigraphs)
            {
                poemContent.Add(ConvertFromFb2EpigraphElement(epigraph, level + 1));
            }

            foreach (var stanza in poemItem.Content)
            {
                if (stanza is StanzaItem)
                {
                    poemContent.Add(ConvertFromFb2Stanza(stanza as StanzaItem, level + 1));
                }
            }

            foreach (var author in poemItem.Authors)
            {
                poemContent.Add(ConvertFb2CitationAuthor(author));
            }

            if (poemItem.Date != null)
            {
                Paragraph date = new Paragraph();
                date.Add(new SimpleEPubText { Text = poemItem.Date.Text });
                date.Class.Value = "poemdate";
                poemContent.Add(date);
            }

            poemContent.ID.Value = referencesManager.AddIdUsed(poemItem.ID, poemContent);

            if (poemItem.Lang != null)
            {
                poemContent.Language.Value = poemItem.Lang;
            }
            poemContent.Class.Value = "poem";
            return poemContent;
        }

        private IXHTMLItem ConvertFromFb2Stanza(StanzaItem stanza, int level)
        {
            Div stanzaSection = new Div();

            if (stanza.Title != null)
            {
                stanzaSection.Add(ConvertFromFb2TitleElement(stanza.Title, level));
            }

            if (stanza.SubTitle != null)
            {
                stanzaSection.Add(ConvertFromFb2SubtitleElement(stanza.SubTitle, level + 1));
            }

            foreach (var line in stanza.Lines)
            {
                stanzaSection.Add(ConvertFromFb2VElement(line));
            }

            if (stanza.Lang != null)
            {
                stanzaSection.Language.Value = stanza.Lang;
            }

            stanzaSection.Class.Value = "stanza";
            return stanzaSection;
        }

        private IXHTMLItem ConvertFromFb2VElement(VPoemParagraph paragraph)
        {
            if (paragraph == null)
            {
                throw new ArgumentNullException("paragraph");
            }
            IBlockElement item = ConvertFromFb2ParagraphElement(paragraph, ParagraphConvTargetEnum.Paragraph);

            item.Class.Value = "v";

            item.ID.Value = referencesManager.AddIdUsed(paragraph.ID, item);

            return item;
        }

        private Div ConvertFromFb2AnnotationElement(AnnotationItem annotation, int level)
        {
            Div resAnnotation = new Div();

            foreach (var element in annotation.Content)
            {
                if (element is ParagraphItem)
                {
                    resAnnotation.Add(ConvertFromFb2ParagraphElement(element as ParagraphItem, ParagraphConvTargetEnum.Paragraph));
                }
                else if (element is PoemItem)
                {
                    resAnnotation.Add(ConvertFromFb2PoemElement(element as PoemItem, level + 1));
                }
                else if (element is CiteItem)
                {
                    resAnnotation.Add(ConvertFromFb2Citation(element as CiteItem, level + 1));
                }
                else if (element is SubTitleItem)
                {
                    resAnnotation.Add(ConvertFromFb2SubtitleElement(element as SubTitleItem, level + 1));
                }
                else if (element is TableItem)
                {
                    resAnnotation.Add(ConvertFromFb2TableElement(element as TableItem));
                }
                else if (element is EmptyLineItem)
                {
                    resAnnotation.Add(ConvertToEmptyLine());
                }
            }

            resAnnotation.ID.Value = referencesManager.AddIdUsed(annotation.ID, resAnnotation);

            resAnnotation.Class.Value = "annotation";
            return resAnnotation;
        }



        /// <summary>
        /// 
        /// </summary>
        /// <param name="epigraph"></param>
        /// <returns></returns>
        private Div ConvertFromFb2EpigraphElement(EpigraphItem epigraph, int level)
        {
            Div content = new Div();

            foreach (var element in epigraph.EpigraphData)
            {
                if (element is ParagraphItem)
                {
                    content.Add(ConvertFromFb2ParagraphElement(element as ParagraphItem, ParagraphConvTargetEnum.Paragraph));
                }
                if (element is PoemItem)
                {
                    content.Add(ConvertFromFb2PoemElement(element as PoemItem, level + 1));
                }
                if (element is CiteItem)
                {
                    content.Add(ConvertFromFb2Citation(element as CiteItem, level + 1));
                }
                if (element is EmptyLineItem)
                {
                    content.Add(ConvertToEmptyLine());
                }
            }

            foreach (var author in epigraph.TextAuthors)
            {
                IBlockElement epAuthor = ConvertFromFb2EpigraphAuthor(author as TextAuthorItem);
                epAuthor.Class.Value = "epigraph_author";
                content.Add(epAuthor);
            }

            content.Class.Value = "epigraph";

            content.ID.Value = referencesManager.AddIdUsed(epigraph.ID, content);

            return content;
        }

        private IBlockElement ConvertFromFb2EpigraphAuthor(TextAuthorItem simpleText)
        {
            Div epigraphAuthor = new Div();
            epigraphAuthor.Add(ConvertFromFb2ParagraphElement(simpleText, ParagraphConvTargetEnum.Paragraph));
            return epigraphAuthor;
        }

        private IXHTMLItem ConvertFb2CitationAuthor(ParagraphItem author)
        {
            Citation cite = new Citation();

            cite.Add(new SimpleEPubText{Text = author.ToString()});

            cite.Class.Value = "citation_author";
            return cite;
        }

        private Div ConvertFromFb2SubtitleElement(SubTitleItem subTitleItem, int p)
        {
            Div subtitle = new Div();
            IBlockElement internalData = ConvertFromFb2ParagraphElement(subTitleItem, ParagraphConvTargetEnum.Paragraph);
            internalData.Class.Value = "subtitle";
            subtitle.Add(internalData);
            subtitle.Class.Value = "subtitle";
            return subtitle;
        }

        private Div ConvertFromFb2Citation(CiteItem citeItem, int level)
        {
            Div citation = new Div();
            foreach (var item in citeItem.CiteData)
            {
                if (item is SubTitleItem)
                {
                    citation.Add(ConvertFromFb2SubtitleElement(item as SubTitleItem, level));
                }
                else if (item is ParagraphItem)
                {
                    citation.Add(ConvertFromFb2ParagraphElement(item as ParagraphItem, ParagraphConvTargetEnum.Paragraph));
                }
                else if (item is PoemItem)
                {
                    citation.Add(ConvertFromFb2PoemElement(item as PoemItem, level + 1));
                }
                else if (item is EmptyLineItem)
                {
                    citation.Add(ConvertToEmptyLine());
                }
                else if (item is TableItem)
                {
                    citation.Add(ConvertFromFb2TableElement(item as TableItem));
                }
            }

            foreach (var author in citeItem.TextAuthors)
            {
                citation.Add(ConvertFb2CitationAuthor(author));
            }

            citation.ID.Value = referencesManager.AddIdUsed(citeItem.ID, citation);

            if (citeItem.Lang != null)
            {
                citation.Language.Value = citeItem.Lang;
            }
            citation.Class.Value = "citation";
            return citation;
        }

        private IXHTMLItem ConvertFromFb2TableElement(TableItem tableItem)
        {
            Table table = new Table();

            foreach (var row in tableItem.Rows)
            {
                table.Add(ConvertFromFb2Row(row));
            }

            table.ID.Value = referencesManager.AddIdUsed(tableItem.ID, table);

            return table;
        }

        private IXHTMLItem ConvertFromFb2Row(TableRowItem row)
        {
            TableRow tableRow = new TableRow();

            foreach (var element in row.Cells)
            {
                if (element is TableHeadingItem)
                {
                    TableHeadingItem th = element as TableHeadingItem;
                    HeaderCell cell = new HeaderCell();
                    IBlockElement cellData = ConvertFromFb2ParagraphElement(th, ParagraphConvTargetEnum.Paragraph);
                    if (cellData.SubElements() != null)
                    {
                        foreach (var subElement in cellData.SubElements())
                        {
                            cell.Add(subElement);
                        }
                    }
                    else
                    {
                        cell.Add(cellData);
                    }
                    //cell.Add(new SimpleEPubText { Text = th.Text });
                    if (th.ColSpan.HasValue)
                    {
                        cell.ColSpan.Value = th.ColSpan.ToString();
                    }
                    if (th.RowSpan.HasValue)
                    {
                        cell.RowSpan.Value = th.RowSpan.ToString();
                    }
                    switch (th.Align)
                    {
                        case TableAlignmentsEnum.Center:
                            cell.Align.Value = "center";
                            break;
                        case TableAlignmentsEnum.Left:
                            cell.Align.Value = "left";
                            break;
                        case TableAlignmentsEnum.Right:
                            cell.Align.Value = "right";
                            break;
                    }
                    switch (th.VAlign)
                    {
                        case TableVAlignmentsEnum.Top:
                            cell.VerticalAlign.Value = "top";
                            break;
                        case TableVAlignmentsEnum.Middle:
                            cell.VerticalAlign.Value = "middle";
                            break;
                        case TableVAlignmentsEnum.Bottom:
                            cell.VerticalAlign.Value = "bottom";
                            break;
                    }
                    tableRow.Add(cell);
                }
                else if (element is TableCellItem)
                {
                    TableCellItem td = element as TableCellItem;
                    TableData cell = new TableData();
                    IBlockElement cellData = ConvertFromFb2ParagraphElement(td, ParagraphConvTargetEnum.Paragraph);                 
                    if (cellData.SubElements() != null)
                    {
                        foreach (var subElement in cellData.SubElements())
                        {
                            cell.Add(subElement);  
                        }
                    }
                    else
                    {
                        cell.Add(cellData);
                    }
                    //cell.Add(new SimpleEPubText { Text = td.Text });
                    if (td.ColSpan.HasValue)
                    {
                        cell.ColSpan.Value = td.ColSpan.ToString();
                    }
                    if (td.RowSpan.HasValue)
                    {
                        cell.RowSpan.Value = td.RowSpan.ToString();
                    }
                    switch (td.Align)
                    {
                        case TableAlignmentsEnum.Center:
                            cell.Align.Value = "center";
                            break;
                        case TableAlignmentsEnum.Left:
                            cell.Align.Value = "left";
                            break;
                        case TableAlignmentsEnum.Right:
                            cell.Align.Value = "right";
                            break;
                    }
                    switch (td.VAlign)
                    {
                        case TableVAlignmentsEnum.Top:
                            cell.VerticalAlign.Value = "top";
                            break;
                        case TableVAlignmentsEnum.Middle:
                            cell.VerticalAlign.Value = "middle";
                            break;
                        case TableVAlignmentsEnum.Bottom:
                            cell.VerticalAlign.Value = "bottom";
                            break;
                    }
                    tableRow.Add(cell);
                }
                else
                {
                    // invalid structure , we ignore
                    Logger.Log.ErrorFormat("Invalid/unexpected table row sub element type : {0}", element.GetType().ToString());
                    continue;
                }
            }
            tableRow.Align.Value = row.Align ?? "left";
            return tableRow;
        }


        /// <summary>
        /// Reset the object to default (nothing converted) state
        /// </summary>
        private void Reset()
        {
            _sectionCounter = 0;
            referencesManager.Reset();
        }


    }
}
