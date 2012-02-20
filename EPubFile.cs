using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Xml;
using System.Xml.Linq;
using EPubLibrary.Container;
using EPubLibrary.Content;
using EPubLibrary.CSS_Items;
using EPubLibrary.Template;
using EPubLibrary.TOC;
using EPubLibrary.XHTML_Items;
using FontsSettings;
using ICSharpCode.SharpZipLib.Zip;
using TranslitRu;
using XHTMLClassLibrary.BaseElements;
using XHTMLClassLibrary.BaseElements.InlineElements;

namespace EPubLibrary
{
    static public class Logger
    {
        // Create a logger for use in this class
        public static readonly log4net.ILog log = log4net.LogManager.GetLogger(Assembly.GetExecutingAssembly().GetType());       
    }

    internal static class XNodetemExtender
    {
        public static long EstimateSize(this XNode node)
        {
            MemoryStream stream = new MemoryStream();
            using (var writer = XmlWriter.Create(stream))
            {
                node.WriteTo(writer);
            }
            return stream.Length;
        }
    }
    public class EPubFile
    {
        private readonly ZipEntryFactory zipFactory = new ZipEntryFactory();
        private readonly EPubTitleSettings title = new EPubTitleSettings();
        private string coverImage = null;
        private readonly List<Font> fontObjects = new List<Font>();
        private readonly CSSFile mainCSS = new CSSFile() { FileName = "main.css", ID = "mainCSS", FileExtPath = @"CSS\main.css" };
        private readonly AdobeTemplate adobeTemplate = new AdobeTemplate();
        private readonly List<CSSFile> cssFiles = new List<CSSFile>();
        private readonly List<BookDocument> sections = new List<BookDocument>();
        private readonly TOCFile tableOfContentFile = new TOCFile();
        private readonly ContentFile content = new ContentFile();
        private readonly Rus2Lat rule = new Rus2Lat();
        private readonly List<string> allSequences = new List<string>();
        private readonly List<string> about_texts = new List<string>();
        private readonly List<string> about_links = new List<string>();


        private readonly Dictionary<string, string> fontFamiliesMap = new Dictionary<string, string>();
        

        private readonly Dictionary<string,EPUBImage> images = new Dictionary<string ,EPUBImage>();

        public List<BookDocument> BookDocuments { get { return sections; } }

        public Rus2Lat Transliterator { get { return rule; } }

        /// <summary>
        /// Get/Set if adobe template XPGT file should be added to resulting file
        /// </summary>
        public bool UseAdobeTemplate { get; set; }

        /// <summary>
        /// Get/Set Path to Adobe template XPGT file
        /// </summary>
        public string AdobeTemplatePath{get;set;}

        /// <summary>
        /// Get/Set "flat" mode , when flat mode is set no subfolders created inside the ZIP
        /// used to work aroun bugs in some readers
        /// </summary>
        public bool FlatStructure { get; set; }

        public TranslitModeEnum TranslitMode = TranslitModeEnum.ExternalRuleFile;

        // All sequences in the book
        public List<string> AllSequences { get { return allSequences; } }

        //public List<Font> FontObjects { get { return fontObjects; } }


        public List<CSSFile> CSSFiles { get { return cssFiles; } }


        /// <summary>
        /// Get access to main CSS file included in all 
        /// xhtml book files 
        /// </summary>
        public CSSFile MainCSS { get { return mainCSS; } }

        /// <summary>
        /// Get access to book's title data
        /// </summary>
        public EPubTitleSettings Title
        {
            get { return title; }
        }

        /// <summary>
        /// Get/Set embeding styles into xHTML files instead of referencing style files
        /// </summary>
        public bool EmbedStyles { get; set; }

        public Dictionary<string,EPUBImage> Images
        {
            get { return images; }
        }

        public TitlePageFile TitlePage { get; set; }

        public AnnotationPageFile AnnotationPage { get; set; }

        public bool TranliterateToc { set; get; }

        /// <summary>
        /// Strings added to about page
        /// </summary>
        public List<string> AboutTexts
        {
            get
            {
                return about_texts;
            }
        }

        /// <summary>
        /// Links added to about page
        /// </summary>
        public List<string> AboutLinks
        {
            get
            {
                return about_links;
            }
        }

        public bool IsValid()
        {
            if (!title.IsValid())
            {
                return false;
            }
            return true;
        }

        public  void AddFontObject(Font fontObject)
        {
            fontObjects.Add(fontObject);

            FontDefinition font = new FontDefinition();
            if (fontObject.Style != FontStylesEnum.None)
            {
                font.FontStyle = fontObject.Style.ToString();                
            }
            StringBuilder builder = new StringBuilder();
            foreach (var destination in fontObject.Destinations)
            {
                if (builder.Length != 0)
                {
                    builder.Append(", ");
                }
                switch (destination.Type)
                {
                    case DestinationTypeEnum.External:
                        builder.AppendFormat(@" url({0}) ", destination.Path);
                        break;
                    case DestinationTypeEnum.Local:
                        builder.AppendFormat(" local(\"{0}\") ", destination.Path);
                        break;
                    case DestinationTypeEnum.Embedded:
                        if (!EmbedStyles)
                        {
                            builder.AppendFormat(FlatStructure ? @" url({0}) " : @" url(../fonts/{0}) ", Path.GetFileName(destination.Path));                            
                        }
                        else
                        {
                            builder.AppendFormat(FlatStructure ? @" url(../{0}) " : @" url(fonts/{0}) ", Path.GetFileName(destination.Path));                            
                        }
                        break;
                    default:
                        Logger.log.ErrorFormat("Unknown font destination type : {0}", destination.Type);
                        break;
                }
            }

            bool generatedName = false;
            if (string.IsNullOrEmpty(fontObject.FamilyName))
            {
                if (fontFamiliesMap.ContainsKey(string.Empty))
                {
                    font.Family = fontFamiliesMap[string.Empty];
                }
                else
                {
                    font.Family = string.Format("Font_{0}", Guid.NewGuid());
                    fontFamiliesMap.Add(string.Empty, font.Family);
                }
                generatedName = true;
            }
            else
            {
                font.Family = fontObject.FamilyName;
            }

            if (fontObject.DecorateFamilyName && !generatedName)
            {
                if (fontFamiliesMap.ContainsKey(font.Family))
                {
                    font.Family = fontFamiliesMap[font.Family];
                }
                else
                {
                    string baseName = font.Family;
                    if ((Title != null) && (Title.Identifiers.Count != 0) && !string.IsNullOrEmpty(Title.Identifiers[0].ID))
                    {
                        font.Family  = string.Format("{0}_{1}", baseName,
                                                        Title.Identifiers[0].IdentifierName);
                    }
                    else
                    {
                        font.Family = string.Format("{0}_{1}", baseName, Guid.NewGuid());
                    }
                    fontFamiliesMap.Add(baseName, font.Family);
                }
            }

            font.FontSrc = builder.ToString();
            font.FontWidth = ConvertFromFontWidth(fontObject.Boldness);
            mainCSS.AddFont(font);

            foreach (var target in fontObject.CSSTargets)
            {
                BaseCSSItem cssItem = new BaseCSSItem();
                cssItem.Name = target;
                cssItem.Parameters.Add("font-family", string.Format("\"{0}\"", font.Family));
                mainCSS.AddTarget(cssItem);
            }

        }

        private string ConvertFromFontWidth(FontBoldnessEnum fontBoldnessEnum)
        {
            switch (fontBoldnessEnum)
            {
                case FontBoldnessEnum.B100:
                    return "100";
                case FontBoldnessEnum.B200:
                    return "200";
                case FontBoldnessEnum.B300:
                    return "300";
                case FontBoldnessEnum.B400:
                    return "normal";
                case FontBoldnessEnum.B500:
                    return "500";
                case FontBoldnessEnum.B600:
                    return "600";
                case FontBoldnessEnum.B700:
                    return "bold";
                case FontBoldnessEnum.B800:
                    return "800";
                case FontBoldnessEnum.B900:
                    return "900";
                case FontBoldnessEnum.Lighter:
                    return "lighter";
                case FontBoldnessEnum.Bolder:
                    return "bolder";
            }
            return string.Empty;

        }


        /// <summary>
        /// Writes (generates) file to disk
        /// </summary>
        /// <param name="outFileName"></param>
        public void Generate(string outFileName)
        {
            Logger.log.DebugFormat("Generating file : {0}", outFileName);
            if (!IsValid())
            {
                Logger.log.Error("File data not valid. Can't generate. Aborting.");
                return;
            }
            try
            {
                string folder = Path.GetDirectoryName(outFileName);
                if (!string.IsNullOrEmpty(folder))
                {
                    Directory.CreateDirectory(folder);
                }
            }
            catch (Exception ex)
            {
                Logger.log.ErrorFormat("Error creating folder {0}, exception thrown : {0}", Path.GetDirectoryName(outFileName), ex.ToString());
            }
            try
            {
                //foreach (var cssFile in CSSFiles)
                //{
                //    cssFile.EPubFilePath = string.Format(FlatStructure ? "{0}" : @"css\{0}",
                //                            Path.GetFileName(cssFile.FileExtPath));
                //}
                mainCSS.EPubFilePath = string.Format(FlatStructure ? "{0}" : @"css\{0}",
                                                     Path.GetFileName(mainCSS.FileExtPath));
                using (var fileStream = File.Create(outFileName))
                {
                    using (var s = new ZipOutputStream(fileStream))
                    {
                        // The EPub does not like 64 bit "patched" headers due to mimetype entry
                        s.UseZip64 = UseZip64.Off;
                        AddMimeTypeEntry(s);
                        AddBookData(s);
                        AddMetaData(s);
                        s.Finish();
                    }
                    fileStream.Close();
                }
            }
            catch (Exception ex)
            {
                Logger.log.ErrorFormat("Error generating file, exception thrown : {0}", ex.ToString());
                File.Delete(outFileName);
            }
        }

        private void AddMetaData(ZipOutputStream stream)
        {
            AddMetaDataFolder(stream);
            AddMetaDataFile(stream);
        }

        private void AddMetaDataFile(ZipOutputStream stream)
        {
            stream.SetLevel(9);
            ZipEntry metaDataFile = zipFactory.MakeFileEntry(@"META-INF\container.xml",false);
            stream.PutNextEntry(metaDataFile);
            ContainerFile container = new ContainerFile{ FlatStructure = FlatStructure};
            container.Write(stream);
            stream.CloseEntry();
        }


        private void AddMetaDataFolder(ZipOutputStream stream)
        {
            stream.SetLevel(9);
            ZipEntry entry = zipFactory.MakeDirectoryEntry("META-INF", false);
            stream.PutNextEntry(entry);
            stream.CloseEntry();
        }

        private void AddBookData(ZipOutputStream stream)
        {
            AddBookFolder(stream);
            AddImages(stream);
            AddFontFiles(stream);
            AddFiles(stream);
            AddTOCFile(stream);
            AddContentFile(stream);
        }

        private void AddFontFiles(ZipOutputStream stream)
        {
            if (fontObjects.Count > 0)
            {
                AddFontsFolder(stream);
                stream.SetLevel(9);
                List<string> fontFilesUsed = new List<string>();
                foreach (var fontFile in fontObjects)
                {
                    foreach (var destination in fontFile.Destinations)
                    {
                        if (destination.Type == DestinationTypeEnum.Embedded )
                        {
                            string filePath = string.Format(FlatStructure ? @"{0}" : @"OEBPS\fonts\{0}", Path.GetFileName(destination.Path));
                            if (fontFilesUsed.Contains(filePath))
                            {
                                continue;
                            }
                            ZipEntry entry = zipFactory.MakeFileEntry(filePath, false);
                            stream.PutNextEntry(entry);
                            try
                            {
                                using (var reader = new BinaryReader(File.OpenRead(destination.Path)))
                                {
                                    int iCount;
                                    Byte[] buffer = new Byte[2048];
                                    while ((iCount = reader.Read(buffer, 0, 2048)) != 0)
                                    {
                                        stream.Write(buffer, 0, iCount);
                                    }
                                }
                                fontFilesUsed.Add(filePath);
                            }
                            catch (Exception ex)
                            {
                                Logger.log.ErrorFormat("Error loading font file {0} : {1}", destination.Path, ex.ToString());
                                continue;
                            }
                            content.AddFontFile(string.Format(FlatStructure ? "{0}" : @"fonts/{0}", Path.GetFileName(destination.Path)),
                                                Path.GetFileNameWithoutExtension(destination.Path));
                        }
                    }

                }
            }
        }

        private void AddFontsFolder(ZipOutputStream stream)
        {
            if (FlatStructure)
            {
                return;
            }
            stream.SetLevel(9);
            ZipEntry entry = zipFactory.MakeDirectoryEntry(@"OEBPS\fonts", false);
            stream.PutNextEntry(entry);
            stream.CloseEntry();
        }

        private void AddFiles(ZipOutputStream stream)
        {
            AddAdobeTemplate(stream);
            AddCSSFiles(stream);
            AddCover(stream);
            AddTitle(stream);
            AddAnnotation(stream);
            AddBookContent(stream);
            if (about_texts.Count >0 || about_links.Count > 0)
            {
                AddAbout(stream);                
            }
        }

        private void AddAdobeTemplate(ZipOutputStream stream)
        {
            if (!UseAdobeTemplate || string.IsNullOrEmpty(AdobeTemplatePath))
            {
                return;
            }
            if (!FlatStructure)
            {
                ZipEntry file = zipFactory.MakeDirectoryEntry(@"OEBPS\Template", false);
                stream.PutNextEntry(file);
            }
            stream.SetLevel(9);
            adobeTemplate.TemplateFileInputPath = AdobeTemplatePath;
            try
            {
                adobeTemplate.Load();
                string fileNameFormat = string.Format(FlatStructure ? "{0}" : @"Template\{0}", adobeTemplate.TemplateFileOutputName);
                ZipEntry templateFile = zipFactory.MakeFileEntry(@"OEBPS\"+fileNameFormat, false);
                stream.PutNextEntry(templateFile);
                adobeTemplate.Write(stream);
                content.AddXPGTTemplate(fileNameFormat, adobeTemplate.TemplateFileOutputName);
            }
            catch (Exception)
            {             
                Logger.log.ErrorFormat("Exception adding template, template will not be added");
            }
        }

        private void AddAnnotation(ZipOutputStream stream)
        {
            if (AnnotationPage != null)
            {

                // for test let's just create one file
                stream.SetLevel(9);
                ZipEntry file = zipFactory.MakeFileEntry(string.Format(@"OEBPS\{0}", AnnotationPage.FileName), false);
                stream.PutNextEntry(file);


                AnnotationPage.FlatStructure = FlatStructure;
                AnnotationPage.EmbedStyles = EmbedStyles;
                AnnotationPage.StyleFiles.Add(mainCSS);
                if (UseAdobeTemplate)
                {
                    AnnotationPage.StyleFiles.Add(adobeTemplate);
                }

                AnnotationPage.Write(stream);

                const string fileID = "annotation";
                content.AddXHTMLTextItem(FlatStructure?string.Format("OEBPS\\{0}",AnnotationPage.FileName):AnnotationPage.FileName, fileID, AnnotationPage.DocumentType);

            }
        }

        private void AddCSSFiles(ZipOutputStream stream)
        {
            foreach (var cssFile in CSSFiles)
            {
                mainCSS.Load(cssFile.FileExtPath, true);
            }
            if (EmbedStyles)
            {
                return;
            }
            if (!FlatStructure)
            {
                ZipEntry file = zipFactory.MakeDirectoryEntry(@"OEBPS\css", false);
                stream.PutNextEntry(file);               
            }
            AddMainCSS(stream);
        }


        private void AddMainCSS(ZipOutputStream stream)
        {
            stream.SetLevel(9);
            ZipEntry file = zipFactory.MakeFileEntry(string.Format(FlatStructure?"{0}":@"OEBPS\{0}", mainCSS.EPubFilePath), false);
            stream.PutNextEntry(file);
            mainCSS.Write(stream);

            content.AddCSS(mainCSS.EPubFilePath, mainCSS.ID);
        }

        private void AddTitle(ZipOutputStream stream)
        {
            if (TitlePage != null)
            {
                TitlePage.FlatStructure = FlatStructure;
                TitlePage.EmbedStyles = EmbedStyles;

                // for test let's just create one file
                stream.SetLevel(9);
                ZipEntry file = zipFactory.MakeFileEntry(string.Format(@"OEBPS\{0}", TitlePage.FileName), false);
                stream.PutNextEntry(file);


                //foreach (var cssFile in CSSFiles)
                //{
                //    TitlePage.StyleFiles.Add(cssFile);
                //}
                TitlePage.StyleFiles.Add(mainCSS);
                if (UseAdobeTemplate)
                {
                    TitlePage.StyleFiles.Add(adobeTemplate);
                }

                TitlePage.Write(stream);

                const string fileID = "title";
                content.AddXHTMLTextItem(FlatStructure ? string.Format("OEBPS\\{0}", TitlePage.FileName) : TitlePage.FileName, fileID, TitlePage.DocumentType);

            }

        }

        private void AddAbout(ZipOutputStream stream)
        {
            const string fileName = "about.xhtml";
            // for test let's just create one file
            stream.SetLevel(9);
            ZipEntry file = zipFactory.MakeFileEntry(string.Format(@"OEBPS\{0}", fileName),false);
            stream.PutNextEntry(file);


            AboutPageFile aboutPage = new AboutPageFile{ FlatStructure = FlatStructure, EmbedStyles = EmbedStyles,AboutLinks = about_links, AboutTexts = about_texts};
            aboutPage.Create();
            aboutPage.StyleFiles.Add(mainCSS);
            if (UseAdobeTemplate)
            {
                aboutPage.StyleFiles.Add(adobeTemplate);
            }

            aboutPage.Write(stream);

            const string fileID = "about";

            content.AddXHTMLTextItem(FlatStructure ? string.Format("OEBPS\\{0}", fileName) : fileName, fileID, aboutPage.DocumentType);
        }

        /// <summary>
        /// Writes book content to the stream
        /// </summary>
        /// <param name="stream">stream to write to</param>
        private void AddBookContent(ZipOutputStream stream)
        {
            int count = 1;

            stream.SetLevel(9);

            foreach (var section in sections)
            {
                section.FlatStructure = FlatStructure;
                section.EmbedStyles = EmbedStyles;
                if (string.IsNullOrEmpty(section.FileName)) // if file name not defined yet create our own (not converter case)
                {
                    section.FileName = string.Format("section{0}.xhtml", count);                    
                }
                XDocument document = section.Generate();
                long docSize = document.EstimateSize();
                if ( docSize >= BookDocument.MaxSize )
                {
                    // This case is not for converter
                    // after converter the files should be in right size already
                    int subCount = 0;
                    foreach( var subsection in section.Split() )
                    {
                        subsection.FlatStructure = FlatStructure;
                        subsection.EmbedStyles = EmbedStyles;
                        subsection.FileName = string.Format("{0}_{1}.xhtml", Path.GetFileNameWithoutExtension(section.FileName), subCount);
                        ZipEntry file = zipFactory.MakeFileEntry(string.Format(@"OEBPS\{0}", subsection.FileName), false);
                        stream.PutNextEntry(file);
                        document = subsection.Generate();
                        WriteDocumentToStream(document,stream);
                        AddBookContentSection(subsection,count,subCount);
                        subCount++;
                    }
                    count++;                        
                }
                else
                {
                    ZipEntry file = zipFactory.MakeFileEntry(string.Format(@"OEBPS\{0}", section.FileName), false);
                    stream.PutNextEntry(file);
                    WriteDocumentToStream(document, stream);
                    AddBookContentSection(section, count,0);
                    count++;                    
                }
                
            }

            // remove navigation leaf end points with empty names
            tableOfContentFile.Consolidate();

            // to be valid we need at least one NAVPoint
            if ( tableOfContentFile.IsNavMapEmpty() && (sections.Count > 0) )
            {
                tableOfContentFile.AddNavPoint(FlatStructure?string.Format("OEBPS\\{0}",sections[0].FileName):sections[0].FileName, rule.Translate(title.BookTitles[0].TitleName, TranliterateToc ? TranslitMode : TranslitModeEnum.None));                        
            }
        }

        private void WriteDocumentToStream(XDocument document, ZipOutputStream stream)
        {
            XmlWriterSettings settings = new XmlWriterSettings();
            settings.CloseOutput = false;
            settings.Encoding = Encoding.UTF8;
            settings.Indent = true;

            using (var writer = XmlWriter.Create(stream, settings))
            {
                document.WriteTo(writer);
            }            
        }

        private void AddBookContentSection(BookDocument subsection, int count,int subcount)
        {
            string fileId = string.Format("bookcontent{0}_{1}", count,subcount); // generate unique ID
            content.AddXHTMLTextItem(FlatStructure ? string.Format("OEBPS\\{0}", subsection.FileName) : subsection.FileName, fileId, subsection.DocumentType);
            if (!subsection.NotPartOfNavigation /*&& !string.IsNullOrEmpty(subsection.PageTitle)*/)
            {
                if (subsection.NavigationLevel <= 1)
                {
                    tableOfContentFile.AddNavPoint(FlatStructure?string.Format("OEBPS\\{0}",subsection.FileName): subsection.FileName, rule.Translate(subsection.PageTitle, TranliterateToc? TranslitMode : TranslitModeEnum.None));
                }
                else
                {
                    tableOfContentFile.AddSubNavPoint(FlatStructure ? string.Format("OEBPS\\{0}", subsection.NavigationParent.FileName) : subsection.NavigationParent.FileName, FlatStructure ? string.Format("OEBPS\\{0}", subsection.FileName) : subsection.FileName, rule.Translate(subsection.PageTitle, TranliterateToc ? TranslitMode : TranslitModeEnum.None));
                }
            }
        }



        private void AddCover(ZipOutputStream stream)
        {
            if (string.IsNullOrEmpty(coverImage) )
            {
                // if no cover image - no cover
                return;
            }
            EPUBImage eImage;
            // also image need to be in list of the images we have (check in case of invalid input)
            if (!images.TryGetValue(coverImage, out eImage))
            {
                return;
            }
            string  fileName = "cover.xhtml";
            // for test let's just create one file
            stream.SetLevel(9);
            ZipEntry file = zipFactory.MakeFileEntry(string.Format(@"OEBPS\{0}", fileName),false);
            stream.PutNextEntry(file);

            CoverPageFile cover = new CoverPageFile() {  CoverFileName = GetCoverImageName(eImage) };
            cover.FlatStructure = FlatStructure;
            cover.EmbedStyles = EmbedStyles;
            //foreach (var cssFile in CSSFiles)
            //{
            //    cover.StyleFiles.Add(cssFile);
            //}
            cover.StyleFiles.Add(mainCSS);
            if (UseAdobeTemplate)
            {
                cover.StyleFiles.Add(adobeTemplate);                
            }



            cover.Write(stream);

            if (!string.IsNullOrEmpty(eImage.ID))
            {
                content.CoverId = eImage.ID;                
            }


            const string fileID = "cover";

            content.AddXHTMLTextItem(FlatStructure ? string.Format("OEBPS\\{0}", fileName) : fileName, fileID, cover.DocumentType);
        }

        private string GetCoverImageName(EPUBImage eImage)
        {
            int count = 0;
            foreach (var image in images)
            {
                if (image.Key == coverImage)
                {
                    return FlatStructure ? eImage.ID : string.Format("images/{0}",eImage.ID);
                }
                count++;
            }
            return FlatStructure ? "cover.jpg" : "images/cover.jpg";
        }

        private void AddTOCFile(ZipOutputStream stream)
        {
            stream.SetLevel(9);
            ZipEntry tocFile = zipFactory.MakeFileEntry(FlatStructure ? "toc.ncx" : @"OEBPS\toc.ncx", false);
            stream.PutNextEntry(tocFile);
            tableOfContentFile.ID = title.Identifiers[0].ID;
            tableOfContentFile.Title = rule.Translate(title.BookTitles[0].TitleName,TranslitMode);
            tableOfContentFile.Write(stream);
            content.AddTOC("toc.ncx","ncx");
        }



        private void AddContentFile(ZipOutputStream stream)
        {
            stream.SetLevel(9);
            ZipEntry contentFile = zipFactory.MakeFileEntry(FlatStructure ? "Content.opf" : @"OEBPS\Content.opf", false);
            stream.PutNextEntry(contentFile);
            content.Title = title;
            content.Write(stream);
        }




        private void AddImages(ZipOutputStream stream)
        {
            if (images.Count > 0)
            {
                AddImagesFolder(stream);
                AddImagesFiles(stream);               
            }
        }

        private void AddImagesFiles(ZipOutputStream stream)
        {
            stream.SetLevel(9);
            //int number = 0;
            foreach (var epubImage in images)
            {
                string filename = epubImage.Value.ID;
                string filePath = string.Format(FlatStructure ? @"OEBPS\{0}" : @"OEBPS\images\{0}", filename);
                ZipEntry entry = zipFactory.MakeFileEntry(filePath, false);
                stream.PutNextEntry(entry);
                stream.Write(epubImage.Value.ImageData, 0, epubImage.Value.ImageData.Length);
                content.AddImage(string.Format(FlatStructure ? "OEBPS\\{0}" : @"images/{0}", filename), epubImage.Value.ID, epubImage.Value.ImageType);
            }

        }

        private void AddImagesFolder(ZipOutputStream stream)
        {
            if (FlatStructure)
            {
                return;
            }
            stream.SetLevel(9);
            ZipEntry entry = zipFactory.MakeDirectoryEntry(@"OEBPS\images", false);
            stream.PutNextEntry(entry);
            stream.CloseEntry();
        }

        private void AddBookFolder(ZipOutputStream stream)
        {
            stream.SetLevel(9);
            ZipEntry entry = zipFactory.MakeDirectoryEntry("OEBPS", false);
            stream.PutNextEntry(entry);
            stream.CloseEntry();
        }

        private void AddMimeTypeEntry(ZipOutputStream s)
        {
            s.SetLevel(0);
            ZipEntry entry = zipFactory.MakeFileEntry("mimetype",false);
            entry.CompressionMethod = CompressionMethod.Stored;
            entry.IsUnicodeText = false;
            entry.ZipFileIndex = 0;
            s.PutNextEntry(entry);
            const string mimetype = "application/epub+zip";
            ASCIIEncoding encoding = new ASCIIEncoding();
            byte[] buffer = encoding.GetBytes(mimetype);
            s.Write(buffer, 0, buffer.Length);
            s.CloseEntry();
        }


        public void AddCoverImage(string imageRef)
        {
            coverImage = imageRef;            
        }

        public  BookDocument AddDocument(string ID)
        {
            BookDocument section = new BookDocument {PageTitle = ID};
            section.StyleFiles.Add(mainCSS);
            if (UseAdobeTemplate)
            {
                section.StyleFiles.Add(adobeTemplate);
            }

            sections.Add(section);
            return section;
        }
    }

    public static class XHTMLExtensions
    {
        public static bool IsIdPresent(this IXHTMLItem iXHTMLItem, string value)
        {
            if (iXHTMLItem.SubElements() == null)
            {
                return false;
            }
            foreach (var s in iXHTMLItem.SubElements())
            {
                if (s is Anchor)
                {
                    Anchor a = s as Anchor;
                    if (a.ID != null)
                    {
                        if (a.ID.Value == value)
                        {
                            return true;
                        }
                    }
                }
                else 
                {
                    if( IsIdPresent(s,value) )
                    {
                        return true;
                    }
                }
            }
            return false;
        }
    }
}
