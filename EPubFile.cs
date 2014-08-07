using System;
using System.Collections.Generic;
using System.IO;
using System.Reflection;
using System.Text;
using System.Xml;
using System.Xml.Linq;
using EPubLibrary.Container;
using EPubLibrary.Content;
using EPubLibrary.Content.CalibreMetadata;
using EPubLibrary.CSS_Items;
using EPubLibrary.PathUtils;
using EPubLibrary.Template;
using EPubLibrary.TOC;
using EPubLibrary.XHTML_Items;
using FontsSettings;
using ICSharpCode.SharpZipLib.Zip;
using TranslitRu;
using XHTMLClassLibrary;
using XHTMLClassLibrary.BaseElements;
using XHTMLClassLibrary.BaseElements.InlineElements;
using EPubLibrary.AppleEPubV2Extensions;

namespace EPubLibrary
{
    /// <summary>
    /// Used to connect to logger
    /// </summary>
    static public class Logger
    {
        // Create a logger for use in this class
        public static readonly log4net.ILog Log = log4net.LogManager.GetLogger(Assembly.GetExecutingAssembly().GetType());       
    }

    /// <summary>
    /// Extends XNode class with function that estimates size of generated data
    /// </summary>
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

    /// <summary>
    /// This class represent the actual ePub file to be generated
    /// </summary>
    public class EPubFile
    {
        #region readonly_private_propeties
        private readonly ZipEntryFactory _zipFactory = new ZipEntryFactory();
        protected readonly EPubTitleSettings _title = new EPubTitleSettings();
        protected readonly EPubCollections  _collections = new EPubCollections();
        protected CSSFile _mainCss = new CSSFile { ID = "mainCSS",  FileName = "main.css" };
        private readonly AdobeTemplate _adobeTemplate = new AdobeTemplate();
        private readonly List<CSSFile> _cssFiles = new List<CSSFile>();
        protected List<BookDocument> _sections = new List<BookDocument>();
        protected TOCFile _tableOfContentFile = new TOCFile();
        protected Rus2Lat _rule = new Rus2Lat();
        private readonly List<string> _allSequences = new List<string>();
        protected List<string> _aboutTexts = new List<string>();
        protected List<string> _aboutLinks = new List<string>();
        private readonly CSSFontSettingsCollection _fontSettings = new CSSFontSettingsCollection();
        private readonly AppleDisplayOptionsFile _appleOptionsFile = new AppleDisplayOptionsFile();
        protected Dictionary<string,EPUBImage> _images = new Dictionary<string ,EPUBImage>();
        protected readonly CalibreMetadataObject _calibreMetadata =  new CalibreMetadataObject();
        #endregion

        #region private_properties
        protected bool _flatStructure;
        protected string _coverImage;
        protected ContentFile _content = new ContentFile();
        #endregion

        #region public_properties


        /// <summary>
        /// Used to set creator software string
        /// </summary>
        public string CreatorSoftwareString {
            set { _content.CreatorSoftwareString = value; }
        }

        /// <summary>
        /// Return Calibre's metadata object
        /// </summary>
        public CalibreMetadataObject CalibreMetadata { get { return _calibreMetadata; }}

        /// <summary>
        /// Controls id Calibre metadata is to be added to content metadata
        /// </summary>
        public bool AddCalibreMetadata
        {
            get; set;
        } 
        /// <summary>
        /// Return reference to the list of the contained "book documents" - book content objects
        /// </summary>
        public List<BookDocument> BookDocuments { get { return _sections; } }

        /// <summary>
        /// Controls if Lord Kiron's license need to be added to file
        /// </summary>
        public bool InjectLKRLicense { get; set; }

        /// <summary>
        /// Return transliteration rule object
        /// </summary>
        public Rus2Lat Transliterator { get { return _rule; } }

        /// <summary>
        /// Get/Set if adobe template XPGT file should be added to resulting file
        /// </summary>
        public virtual bool UseAdobeTemplate { get; set; }

        /// <summary>
        /// Get/Set Path to Adobe template XPGT file
        /// </summary>
        public string AdobeTemplatePath{get;set;}

        /// <summary>
        /// Get/Set "flat" mode , when flat mode is set no subfolders created inside the ZIP
        /// used to work around bugs in some readers
        /// </summary>
        public bool FlatStructure
        {
            get { return _flatStructure; }
            set
            {
                _content.FlatStructure = value;
                _flatStructure = value;
            }
        }

        /// <summary>
        /// Transliteration mode
        /// </summary>
        public TranslitModeEnum TranslitMode = TranslitModeEnum.ExternalRuleFile;

        // All sequences in the book
        public List<string> AllSequences { get { return _allSequences; } }

        /// <summary>
        /// Return reference to the list of the CSS style files
        /// </summary>
        public List<CSSFile> CSSFiles { get { return _cssFiles; } }

        /// <summary>
        /// Get access to main CSS file included in all 
        /// xhtml book files 
        /// </summary>
        public CSSFile MainCSS { get { return _mainCss; } }

        /// <summary>
        /// Get access to book's title data
        /// </summary>
        public EPubTitleSettings Title
        {
            get { return _title; }
        }

        /// <summary>
        /// Get access to the list of collections (series) book belong to
        /// </summary>
        public EPubCollections Collections
        {
            get { return _collections; }    
        }

        /// <summary>
        /// Get/Set embedding styles into xHTML files instead of referencing style files
        /// </summary>
        public bool EmbedStyles { get; set; }

        /// <summary>
        /// Return reference to the list of images
        /// </summary>
        public Dictionary<string,EPUBImage> Images
        {
            get { return _images; }
        }

        /// <summary>
        /// Set/Get title page object
        /// </summary>
        public TitlePageFile TitlePage { get; set; }

        /// <summary>
        /// Set/get Annotation object
        /// </summary>
        public AnnotationPageFile AnnotationPage { get; set; }

        /// <summary>
        /// Set/get it Table of Content (TOC) entries should be transliterated
        /// </summary>
        public bool TranliterateToc { set; get; }

        /// <summary>
        /// Strings added to about page
        /// </summary>
        public List<string> AboutTexts
        {
            get
            {
                return _aboutTexts;
            }
        }

        /// <summary>
        /// Links added to about page
        /// </summary>
        public List<string> AboutLinks
        {
            get
            {
                return _aboutLinks;
            }
        }

        /// <summary>
        /// Returns reference to apple options file
        /// </summary>
        public AppleDisplayOptionsFile AppleOptions
        {
            get { return _appleOptionsFile; }
        }

        #endregion

        #region public_functions

        /// <summary>
        /// Assign image (by id) to be cover image
        /// </summary>
        /// <param name="imageRef">image reference name</param>
        public void AddCoverImage(string imageRef)
        {
            _coverImage = imageRef;
        }

        /// <summary>
        /// Adds (creates) a new empty document in a list of book content documents
        /// </summary>
        /// <param name="id">id - title to assign to the new document</param>
        /// <returns></returns>
        public virtual BookDocument AddDocument(string id)
        {
            BookDocument section = new BookDocument(XHTMRulesEnum.EPUBCompatible) { PageTitle = id };
            section.StyleFiles.Add(_mainCss);
            if (UseAdobeTemplate)
            {
                section.StyleFiles.Add(_adobeTemplate);
            }

            _sections.Add(section);
            return section;
        }


        /// <summary>
        /// Assign ePub fonts from set of Fonts settings
        /// </summary>
        /// <param name="fonts">font settings to use</param>
        /// <param name="resourcesPath">path to program's "resources"</param>
        /// <param name="decorateFontNames">add or not random decoration to the font names</param>
        public void SetEPubFonts(EPubFontSettings fonts, string resourcesPath, bool decorateFontNames)
        {
            _fontSettings.ResourceMask = resourcesPath;
            _fontSettings.Load(fonts, decorateFontNames ? Title.Identifiers[0].IdentifierName : string.Empty);

            AddFontsToCSS(_fontSettings.Fonts);
            AddCssElementsToCSS(_fontSettings.CssElements);
        }


        /// <summary>
        /// Writes (generates) file to disk
        /// </summary>
        /// <param name="outFileName"></param>
        public void Generate(string outFileName)
        {
            Logger.Log.DebugFormat("Generating file : {0}", outFileName);
            if (!IsValid())
            {
                Logger.Log.Error("File data not valid. Can't generate. Aborting.");
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
                Logger.Log.ErrorFormat("Error creating folder {0}, exception thrown : {1}", Path.GetDirectoryName(outFileName), ex);
            }
            try
            {
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
                Logger.Log.ErrorFormat("Error generating file, exception thrown : {0}", ex);
                File.Delete(outFileName);
            }
        }
        #endregion 

        #region private_functions

        /// <summary>
        /// Check if epub file data is valid (ready to be generated)
        /// </summary>
        /// <returns></returns>
        private bool IsValid()
        {
            if (!_title.IsValid())
            {
                return false;
            }
            return true;
        }

        /// <summary>
        /// Adds content of META-INF subfolder
        /// </summary>
        /// <param name="stream"></param>
        private void AddMetaData(ZipOutputStream stream)
        {
            AddMetaDataFile(stream);
            AddAppleOptionsFile(stream); // generate apple options file with custom fonts option allowed
        }

        /// <summary>
        /// Add file containing options for Apple based readers
        /// </summary>
        /// <param name="stream"></param>
        private void AddAppleOptionsFile(ZipOutputStream stream)
        {
            stream.SetLevel(9);
            CreateFileEntryInZip(stream, _appleOptionsFile);
            _appleOptionsFile.Write(stream);
            stream.CloseEntry();
        }

        /// <summary>
        /// Adds container.xml
        /// </summary>
        /// <param name="stream"></param>
        private void AddMetaDataFile(ZipOutputStream stream)
        {
            stream.SetLevel(9);
            ContainerFile container;
            CreateContainer(out container);
            CreateFileEntryInZip(stream, container);
            container.Write(stream);
            stream.CloseEntry();
        }

        protected virtual void CreateContainer(out ContainerFile container)
        {
            container = new ContainerFile {FlatStructure = _flatStructure, ContentFilePath = _content};
        }


        /// <summary>
        /// Adds actual book "context" 
        /// </summary>
        /// <param name="stream"></param>
        protected virtual void AddBookData(ZipOutputStream stream)
        {
            if (InjectLKRLicense)
            {
                AddLicenseFile(stream);
            }
            AddImages(stream);
            AddFontFiles(stream);
            AddAdditionalFiles(stream);
            AddTOCFile(stream);
            AddContentFile(stream);
        }

        /// <summary>
        /// Adds "license" file 
        /// </summary>
        /// <param name="stream"></param>
        protected virtual void AddLicenseFile(ZipOutputStream stream)
        {
            stream.SetLevel(9);
            LicenseFile licensePage = new LicenseFile(XHTMRulesEnum.EPUBCompatible)
            {
                FlatStructure = FlatStructure, 
                EmbedStyles = EmbedStyles, 
            };
            CreateFileEntryInZip(stream, licensePage);
            PutPageToFile(stream,licensePage);
            _content.AddXHTMLTextItem(licensePage);          
        }

        /// <summary>
        /// Adds embedded font files
        /// </summary>
        /// <param name="stream"></param>
        protected void AddFontFiles(ZipOutputStream stream)
        {
            if (_fontSettings.NumberOfEmbededFiles > 0)
            {
                stream.SetLevel(9);
                foreach (var embededFileLocation in _fontSettings.EmbededFilesLocations)
                {
                    FontOnStorage fontFile = new FontOnStorage(embededFileLocation, _fontSettings.GetMediaType(embededFileLocation));
                    CreateFileEntryInZip(stream,fontFile);
                    try
                    {
                        using (var reader = new BinaryReader(File.OpenRead(embededFileLocation)))
                        {
                            int iCount;
                            Byte[] buffer = new Byte[2048];
                            while ((iCount = reader.Read(buffer, 0, 2048)) != 0)
                            {
                                stream.Write(buffer, 0, iCount);
                            }
                        }
                    }
                    catch (Exception ex)
                    {
                        Logger.Log.ErrorFormat("Error loading font file {0} : {1}", embededFileLocation, ex);
                        continue;
                    }
                    _content.AddFontFile(fontFile);
                }
            }
        }


        /// <summary>
        /// Adds different additional generated files like cover, annotation etc.
        /// </summary>
        /// <param name="stream"></param>
        protected void AddAdditionalFiles(ZipOutputStream stream)
        {
            AddAdobeTemplate(stream);
            AddCSSFiles(stream);
            AddCover(stream);
            AddTitle(stream);
            AddAnnotation(stream);
            AddBookContent(stream);
            if (_aboutTexts.Count >0 || _aboutLinks.Count > 0)
            {
                AddAbout(stream);                
            }
        }


        /// <summary>
        /// Adds Adobe XGTP template
        /// </summary>
        /// <param name="stream"></param>
        private void AddAdobeTemplate(ZipOutputStream stream)
        {
            if (!UseAdobeTemplate || string.IsNullOrEmpty(AdobeTemplatePath))
            {
                return;
            }
            stream.SetLevel(9);
            _adobeTemplate.TemplateFileInputPath = AdobeTemplatePath;
            try
            {
                _adobeTemplate.Load();
                CreateFileEntryInZip(stream,_adobeTemplate);
                _adobeTemplate.Write(stream);
                _content.AddXPGTTemplate(_adobeTemplate);
            }
            catch (Exception)
            {             
                Logger.Log.ErrorFormat("Exception adding template, template will not be added");
            }
        }

        /// <summary>
        /// Adds annotation page file
        /// </summary>
        /// <param name="stream"></param>
        private void AddAnnotation(ZipOutputStream stream)
        {
            if (AnnotationPage != null)
            {
                stream.SetLevel(9);
                CreateFileEntryInZip(stream, AnnotationPage);
                PutPageToFile(stream, AnnotationPage);
                _content.AddXHTMLTextItem(AnnotationPage);
            }
        }

        /// <summary>
        /// Adds CSS styles files
        /// </summary>
        /// <param name="stream"></param>
        private void AddCSSFiles(ZipOutputStream stream)
        {
            foreach (var cssFile in CSSFiles)
            {
                _mainCss.Load(cssFile.FilePathOnDisk, true);
            }
            if (EmbedStyles)
            {
                return;
            }
            AddMainCSS(stream);
        }


        /// <summary>
        /// Add CSS file to ZIP stream
        /// </summary>
        /// <param name="stream"></param>
        private void AddMainCSS(ZipOutputStream stream)
        {
            stream.SetLevel(9);
            CreateFileEntryInZip(stream, _mainCss);
            _mainCss.Write(stream);
            _content.AddCSS(_mainCss);
        }

        /// <summary>
        /// Create a file in the ZIP stream
        /// </summary>
        /// <param name="stream"></param>
        /// <param name="pathObject"></param>
        protected void CreateFileEntryInZip(ZipOutputStream stream,IEPubPath pathObject)
        {
            ZipEntry file = _zipFactory.MakeFileEntry(pathObject.PathInEPUB.GetFilePathInZip(_flatStructure), false);
            file.CompressionMethod = CompressionMethod.Deflated; // as defined by ePub stndard
            stream.PutNextEntry(file);
        }

        /// <summary>
        /// Adds title page file
        /// </summary>
        /// <param name="stream"></param>
        private void AddTitle(ZipOutputStream stream)
        {
            if (TitlePage != null)
            {
                stream.SetLevel(9);
                CreateFileEntryInZip(stream, TitlePage);
                PutPageToFile(stream,TitlePage);
                _content.AddXHTMLTextItem(TitlePage);
            }

        }

        protected void PutPageToFile(ZipOutputStream stream, IBaseXHTMLFile xhtmlFile)
        {
            xhtmlFile.FlatStructure = FlatStructure;
            xhtmlFile.EmbedStyles = EmbedStyles;
            xhtmlFile.StyleFiles.Add(_mainCss);
            if (UseAdobeTemplate)
            {
                xhtmlFile.StyleFiles.Add(_adobeTemplate);
            }
            xhtmlFile.Write(stream);
        }

        /// <summary>
        /// Adds "About" page file
        /// </summary>
        /// <param name="stream"></param>
        protected virtual void AddAbout(ZipOutputStream stream)
        {
            stream.SetLevel(9);

            AboutPageFile aboutPage = new AboutPageFile(XHTMRulesEnum.EPUBCompatible)
            {
                FlatStructure = FlatStructure, 
                EmbedStyles = EmbedStyles,
                AboutLinks = _aboutLinks, 
                AboutTexts = _aboutTexts
            };

            CreateFileEntryInZip(stream,aboutPage);
            PutPageToFile(stream,aboutPage);

            _content.AddXHTMLTextItem(aboutPage);
        }

        /// <summary>
        /// Writes book content to the stream
        /// </summary>
        /// <param name="stream">stream to write to</param>
        protected virtual void AddBookContent(ZipOutputStream stream)
        {
            int count = 1;

            stream.SetLevel(9);

            foreach (var section in _sections)
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
                        CreateFileEntryInZip(stream,subsection);
                        subsection.Write(stream);
                        AddBookContentSection(subsection,count,subCount);
                        subCount++;
                    }
                    count++;                        
                }
                else
                {
                    CreateFileEntryInZip(stream,section);
                    section.Write(stream);
                    AddBookContentSection(section, count,0);
                    count++;                    
                }
                
            }

            // remove navigation leaf end points with empty names
            _tableOfContentFile.Consolidate();

            // to be valid we need at least one NAVPoint
            if ( _tableOfContentFile.IsNavMapEmpty() && (_sections.Count > 0) )
            {
                _tableOfContentFile.AddNavPoint(_sections[0], _rule.Translate(_title.BookTitles[0].TitleName, TranliterateToc ? TranslitMode : TranslitModeEnum.None));                        
            }
        }


        protected  virtual void AddBookContentSection(BookDocument subsection, int count,int subcount)
        {
            subsection.Id = string.Format("bookcontent{0}_{1}", count, subcount); // generate unique ID
            _content.AddXHTMLTextItem(subsection);
            if (!subsection.NotPartOfNavigation)
            {
                if (subsection.NavigationLevel <= 1)
                {
                    _tableOfContentFile.AddNavPoint(subsection, _rule.Translate(subsection.PageTitle, TranliterateToc? TranslitMode : TranslitModeEnum.None));
                }
                else
                {
                    _tableOfContentFile.AddSubNavPoint(subsection.NavigationParent, subsection, _rule.Translate(subsection.PageTitle, TranliterateToc ? TranslitMode : TranslitModeEnum.None));
                }
            }
        }



        protected virtual void AddCover(ZipOutputStream stream)
        {
            if (string.IsNullOrEmpty(_coverImage) )
            {
                // if no cover image - no cover
                return;
            }
            EPUBImage eImage;
            // also image need to be in list of the images we have (check in case of invalid input)
            if (!_images.TryGetValue(_coverImage, out eImage))
            {
                return;
            }
            // for test let's just create one file
            stream.SetLevel(9);

            CoverPageFile cover = new CoverPageFile(XHTMRulesEnum.EPUBCompatible)
            {
                CoverFileName = GetCoverImageName(eImage),
            };

            CreateFileEntryInZip(stream,cover);
            PutPageToFile(stream,cover);

            if (!string.IsNullOrEmpty(eImage.ID))
            {
                _content.CoverId = eImage.ID;                
            }


            _content.AddXHTMLTextItem(cover);
        }

        protected ImageOnStorage GetCoverImageName(EPUBImage eImage)
        {
            foreach (var image in _images)
            {
                if (image.Key == _coverImage)
                {
                    return new ImageOnStorage(eImage) {FileName = eImage.ID};
                }
            }
            return new ImageOnStorage(eImage) { FileName = "cover.jpg" };
        }

        protected void AddTOCFile(ZipOutputStream stream)
        {
            stream.SetLevel(9);
            CreateFileEntryInZip(stream, _tableOfContentFile);
            _tableOfContentFile.ID = _title.Identifiers[0].ID;
            _tableOfContentFile.Title = _rule.Translate(_title.BookTitles[0].TitleName,TranslitMode);
            _tableOfContentFile.Write(stream);
            _content.AddTOC();
        }



        protected virtual void AddContentFile(ZipOutputStream stream)
        {
            stream.SetLevel(9);
            CreateFileEntryInZip(stream,_content);
            _content.Title = _title;
            if (AddCalibreMetadata)
            {
                _content.CalibreData = _calibreMetadata;
            }
            _content.Write(stream);
        }



        protected void AddImages(ZipOutputStream stream)
        {
            if (_images.Count > 0)
            {
                AddImagesFiles(stream);               
            }
        }

        private void AddImagesFiles(ZipOutputStream stream)
        {
            stream.SetLevel(9);
            foreach (var epubImage in _images)
            {
                ImageOnStorage imageFile = new ImageOnStorage(epubImage.Value) {FileName = epubImage.Value.ID};
                CreateFileEntryInZip(stream,imageFile);
                stream.Write(epubImage.Value.ImageData, 0, epubImage.Value.ImageData.Length);
                _content.AddImage(imageFile);
            }

        }



        private void AddMimeTypeEntry(ZipOutputStream s)
        {
            s.SetLevel(0);
            ZipEntry entry = _zipFactory.MakeFileEntry("mimetype",false);
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



        private void AddCssElementsToCSS(Dictionary<string, Dictionary<string, List<CSSFontFamily>>> cssElements)
        {
            // Now add the elements
            foreach (var elementName in cssElements.Keys)
            {
                foreach (var elementClass in cssElements[elementName].Keys)
                {
                    BaseCSSItem cssItem = new BaseCSSItem();
                    StringBuilder sb = new StringBuilder();
                    sb.Append(elementName);
                    if (!string.IsNullOrEmpty(elementClass))
                    {
                        sb.AppendFormat(".{0}",elementClass);
                    }
                    cssItem.Name = sb.ToString();

                    // now build a list of fonts
                    sb.Clear();
                    int counter = 0;
                    foreach (var fontFamily in cssElements[elementName][elementClass])
                    {
                        sb.AppendFormat("\"{0}\"",fontFamily.Name);
                        if (counter!= 0)
                        {
                            sb.Append(", ");
                        }
                        counter++;
                    }
                    cssItem.Parameters.Add("font-family", sb.ToString());
                    _mainCss.AddTarget(cssItem);
                }
            }
        }


        private void AddFontsToCSS(Dictionary<string, CSSFontFamily> fontsFamilies)
        {
            // Add the fonts to CSS
            foreach (var cssFontFamily in fontsFamilies)
            {
                foreach (var subFont in cssFontFamily.Value.Fonts)
                {
                    CssFontDefinition cssFont = new CssFontDefinition();
                    cssFont.Family = cssFontFamily.Key;
                    cssFont.FontStyle = CssFontDefinition.FromStyle(subFont.FontStyle);
                    cssFont.FontWidth = CssFontDefinition.FromWidth(subFont.FontWidth);
                    List<string> sources = new List<string>();
                    foreach (var fontSource in subFont.Sources)
                    {
                        sources.Add(CssFontDefinition.ConvertToSourceString(fontSource, EmbedStyles, FlatStructure));
                    }
                    cssFont.FontSrcs = sources;
                    _mainCss.AddFont(cssFont);
                }
            }
        }
        #endregion
    }


    /// <summary>
    /// Extends IXHTMLItem class with functionality
    /// </summary>
    public static class XHTMLExtensions
    {
        /// <summary>
        /// Checks if specific ID present in class list of elements (any depth)
        /// </summary>
        /// <param name="iXHTMLItem"></param>
        /// <param name="value"></param>
        /// <returns></returns>
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
