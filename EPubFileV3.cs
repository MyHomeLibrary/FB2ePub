using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Xml.Linq;
using EPubLibrary.Container;
using EPubLibrary.Content;
using EPubLibrary.Content.Collections;
using EPubLibrary.Content.NavigationManagement;
using EPubLibrary.CSS_Items;
using EPubLibrary.PathUtils;
using EPubLibrary.XHTML_Items;
using FontSettingsContracts;
using FontsSettings;
using ICSharpCode.SharpZipLib.Zip;
using TranslitRu;
using XHTMLClassLibrary.BaseElements;

namespace EPubLibrary
{

    public enum V3Standard
    {
        V30,
        V301,
    }

    /// <summary>
    /// Implements v3 of ePub standard
    /// </summary>
    public class EPubFileV3 : IEpubFile
    {
        #region readonly_private_propeties
        private readonly V3Standard _standard;
        private readonly ZipEntryFactory _zipFactory = new ZipEntryFactory();
        private readonly EPubTitleSettings _title = new EPubTitleSettings();
        private readonly CSSFile _mainCss = new CSSFile { ID = "mainCSS", FileName = "main.css" };
        private readonly List<CSSFile> _cssFiles = new List<CSSFile>();
        private readonly List<BookDocument> _sections = new List<BookDocument>();
        private readonly List<string> _allSequences = new List<string>();
        private readonly List<string> _aboutTexts = new List<string>();
        private readonly List<string> _aboutLinks = new List<string>();
        private readonly CSSFontSettingsCollection _fontSettings = new CSSFontSettingsCollection();
        private readonly Dictionary<string, EPUBImage> _images = new Dictionary<string, EPUBImage>();
        private readonly ContentFileV3 _content;
        private readonly NavigationManagerV3 _navigationManager = new NavigationManagerV3();
        #endregion

        #region private_properties
        private bool _flatStructure;
        private bool _generateCompatibleTOC;
        private TransliterationSettings _translitMode = new TransliterationSettings { Mode = TranslitModeEnum.ExternalRuleFile};
        private string _coverImage;
        #endregion


        #region File creation related properties

        /// <summary>
        /// Max size of content (xhtml) file, 0 means no limit
        /// </summary>
        public ulong ContentFileLimit { get; set; }

        #endregion

        #region Transliteration_common_properties
        /// <summary>
        /// Transliteration mode
        /// </summary>
        public TransliterationSettings TranslitMode
        {
            get { return _translitMode; }
            set { _translitMode = value; }
        }

        /// <summary>
        /// Set/get it Table of Content (TOC) entries should be transliterated
        /// </summary>
        public bool TranliterateToc { set; get; }

        public List<BookDocument> BookDocuments { get { return _sections; } }

        #endregion 



        public V3Standard EPubV3Standard
        {
            get { return _standard; }
        }

        public EPubFileV3(V3Standard standard)
        {
            _standard = standard;
            _content = new ContentFileV3(standard);
        }



        public bool GenerateCompatibleTOC
        {
            get { return _generateCompatibleTOC; }
            set { _content.GenerateCompatibleTOC = _generateCompatibleTOC = value; }
        }

        /// <summary>
        /// Adds new series collection to ePub
        /// </summary>
        /// <param name="collection"></param>
        public void AddNewCollection(SeriesCollectionMember collection)
        {
            _content.SeriesCollections.CollectionMembers.Add(collection);
        }


        protected void CreateContainer(out ContainerFile container)
        {
            container = new ContainerFileV3 { FlatStructure = _flatStructure, ContentFilePath = _content };
        }


        protected void AddContentFile(ZipOutputStream stream)
        {
            stream.SetLevel(9);
            CreateFileEntryInZip(stream, _content);
            _content.Title = _title;
            _content.Write(stream);
        }

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
        public BookDocument AddDocument(string id)
        {
            var section = new BookDocument(HTMLElementType.HTML5) { PageTitle = id };
            section.StyleFiles.Add(_mainCss);

            _sections.Add(section);
            return section;
        }

        /// <summary>
        /// Adds "license" file 
        /// </summary>
        /// <param name="stream"></param>
        protected void AddLicenseFile(ZipOutputStream stream)
        {
            stream.SetLevel(9);
            var licensePage = new LicenseFile(HTMLElementType.HTML5)
            {
                FlatStructure = FlatStructure,
                EmbedStyles = EmbedStyles,
            };
            CreateFileEntryInZip(stream, licensePage);
            PutPageToFile(stream, licensePage);
            _content.AddXHTMLTextItem(licensePage);
        }


        /// <summary>
        /// Adds "About" page file
        /// </summary>
        /// <param name="stream"></param>
        protected void AddAbout(ZipOutputStream stream)
        {
            stream.SetLevel(9);

            var aboutPage = new AboutPageFile(HTMLElementType.HTML5)
            {
                FlatStructure = FlatStructure,
                EmbedStyles = EmbedStyles,
                AboutLinks = _aboutLinks,
                AboutTexts = _aboutTexts
            };

            CreateFileEntryInZip(stream, aboutPage);
            PutPageToFile(stream, aboutPage);

            _content.AddXHTMLTextItem(aboutPage);
        }


        /// <summary>
        /// Adds actual book "context" 
        /// </summary>
        /// <param name="stream"></param>
        protected void AddBookData(ZipOutputStream stream)
        {
            if (InjectLKRLicense)
            {
                AddLicenseFile(stream);
            }
            AddImages(stream);
            AddFontFiles(stream);
            AddAdditionalFiles(stream);
            AddNavigation(stream);
            AddContentFile(stream);
        }

        private void AddNavigation(ZipOutputStream stream)
        {
            _navigationManager.SetupBookNavigation(_title.Identifiers[0].ID, Rus2Lat.Instance.Translate(_title.BookTitles[0].TitleName, TranslitMode));

            if (GenerateCompatibleTOC)
            {
                AddTOCFile(stream);
            }
            AddNavigationFile(stream);
        }

        protected void AddTOCFile(ZipOutputStream stream)
        {
            stream.SetLevel(9);
            CreateFileEntryInZip(stream, _navigationManager.TableOfContentFile);
            _navigationManager.TableOfContentFile.Write(stream);
            _content.AddTOC();
        }

        protected void AddNavigationFile(ZipOutputStream stream)
        {
            stream.SetLevel(9);
            CreateFileEntryInZip(stream, _navigationManager.NavigationDocument);
            _navigationManager.NavigationDocument.StyleFiles.Add(_mainCss);
            _navigationManager.NavigationDocument.Write(stream);
            _content.AddNavigationDocument(_navigationManager.NavigationDocument);
        }

        /// <summary>
        /// Adds different additional generated files like cover, annotation etc.
        /// </summary>
        /// <param name="stream"></param>
        protected void AddAdditionalFiles(ZipOutputStream stream)
        {
            AddCSSFiles(stream);
            AddCover(stream);
            AddTitle(stream);
            AddAnnotation(stream);
            AddBookContent(stream);
            if (_aboutTexts.Count > 0 || _aboutLinks.Count > 0)
            {
                AddAbout(stream);
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
        /// Adds title page file
        /// </summary>
        /// <param name="stream"></param>
        private void AddTitle(ZipOutputStream stream)
        {
            if (TitlePage != null)
            {
                stream.SetLevel(9);
                CreateFileEntryInZip(stream, TitlePage);
                PutPageToFile(stream, TitlePage);
                _content.AddXHTMLTextItem(TitlePage);
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
                    var fontFile = new FontOnStorage(embededFileLocation, ConvertFontToMediaType(_fontSettings.GetFontFormat(embededFileLocation)));
                    CreateFileEntryInZip(stream, fontFile);
                    try
                    {
                        using (var reader = new BinaryReader(File.OpenRead(embededFileLocation)))
                        {
                            int iCount;
                            var buffer = new Byte[2048];
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

        private EPubCoreMediaType ConvertFontToMediaType(FontFormat fontFormat)
        {
            if (fontFormat == FontFormat.WOFF)
            {
                return EPubCoreMediaType.ApplicationFontWoff;
            }
            return EPubCoreMediaType.ApplicationFontMSOpen;
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
                var imageFile = new ImageOnStorage(epubImage.Value) { FileName = epubImage.Value.ID };
                CreateFileEntryInZip(stream, imageFile);
                stream.Write(epubImage.Value.ImageData, 0, epubImage.Value.ImageData.Length);
                _content.AddImage(imageFile);
            }

        }


        protected void AddCover(ZipOutputStream stream)
        {
            if (string.IsNullOrEmpty(_coverImage))
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

            var cover = new CoverPageFile(HTMLElementType.HTML5)
            {
                CoverFileName = GetCoverImageName(eImage),
            };

            CreateFileEntryInZip(stream, cover);
            PutPageToFile(stream, cover);

            if (!string.IsNullOrEmpty(eImage.ID))
            {
                _content.CoverId = eImage.ID;
            }
            _content.AddXHTMLTextItem(cover);
        }

        protected ImageOnStorage GetCoverImageName(EPUBImage eImage)
        {
            if (_images.Any(image => image.Key == _coverImage))
            {
                return new ImageOnStorage(eImage) { FileName = eImage.ID };
            }
            return new ImageOnStorage(eImage) { FileName = "cover.jpg" };
        }

        /// <summary>
        /// Writes book content to the stream
        /// </summary>
        /// <param name="stream">stream to write to</param>
        protected void AddBookContent(ZipOutputStream stream)
        {
            int count = 1;

            stream.SetLevel(9);

            foreach (var section in _sections)
            {
                section.FlatStructure = FlatStructure;
                section.EmbedStyles = EmbedStyles;
                section.MaxSize = ContentFileLimit;

                if (string.IsNullOrEmpty(section.FileName)) // if file name not defined yet create our own (not converter case)
                {
                    section.FileName = string.Format(@"section{0}.xhtml", count);
                }

                if (section.MaxSize != 0)
                {
                    count = SplitAndAddSubSections(stream,section, count);
                }
                else
                {
                    CreateFileEntryInZip(stream, section);
                    section.Write(stream);
                    AddBookContentSection(section, count, 0);
                    count++;                   
                }

            }

            // remove navigation leaf end points with empty names
            _navigationManager.Consolidate();

            // to be valid we need at least one NAVPoint
            if (_navigationManager.TableOfContentFile.IsNavMapEmpty() && (_sections.Count > 0))
            {
                _navigationManager.AddBookSubsection(_sections[0], TranliterateToc ?Rus2Lat.Instance.Translate(_title.BookTitles[0].TitleName,  _translitMode ):_title.BookTitles[0].TitleName);
            }
        }

        private int SplitAndAddSubSections(ZipOutputStream stream,BookDocument section, int count)
        {
            int newCount = count;
            XDocument document = section.Generate();
            ulong docSize = document.EstimateSize();
            if (docSize >= section.MaxSize)
            {
                // This case is not for converter
                // after converter the files should be in right size already
                int subCount = 0;
                foreach (var subsection in section.Split())
                {
                    subsection.FlatStructure = FlatStructure;
                    subsection.EmbedStyles = EmbedStyles;
                    subsection.FileName = string.Format("{0}_{1}.xhtml",
                        Path.GetFileNameWithoutExtension(section.FileName), subCount);
                    CreateFileEntryInZip(stream, subsection);
                    subsection.Write(stream);
                    AddBookContentSection(subsection, count, subCount);
                    subCount++;
                }
                newCount++;
            }
            else
            {
                CreateFileEntryInZip(stream, section);
                section.Write(stream);
                AddBookContentSection(section, count, 0);
                newCount++;
            }
            return newCount;
        }


        protected void AddBookContentSection(BookDocument subsection, int count, int subcount)
        {
            subsection.Id = string.Format(@"bookcontent{0}_{1}", count, subcount); // generate unique ID
            _content.AddXHTMLTextItem(subsection);
            _navigationManager.AddBookSubsection(subsection, TranliterateToc ? Rus2Lat.Instance.Translate(subsection.PageTitle, _translitMode ):subsection.PageTitle);
        }

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
        /// Get/Set embedding styles into xHTML files instead of referencing style files
        /// </summary>
        public bool EmbedStyles { get; set; }

        /// <summary>
        /// Controls if Lord Kiron's license need to be added to file
        /// </summary>
        public bool InjectLKRLicense { get; set; }

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
        /// Set/Get title page object
        /// </summary>
        public TitlePageFile TitlePage { get; set; }

        /// <summary>
        /// Set/get Annotation object
        /// </summary>
        public AnnotationPageFile AnnotationPage { get; set; }

        // All sequences in the book
        public List<string> AllSequences { get { return _allSequences; } }


        /// <summary>
        /// Get access to book's title data
        /// </summary>
        public EPubTitleSettings Title
        {
            get { return _title; }
        }

        
        /// <summary>
        /// Used to set creator software string
        /// </summary>
        public string CreatorSoftwareString
        {
            set { _content.CreatorSoftwareString = value; }
        }

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
        /// Return reference to the list of images
        /// </summary>
        public Dictionary<string, EPUBImage> Images
        {
            get { return _images; }
        }

        /// <summary>
        /// Create a file in the ZIP stream
        /// </summary>
        /// <param name="stream"></param>
        /// <param name="pathObject"></param>
        protected void CreateFileEntryInZip(ZipOutputStream stream, IEPubPath pathObject)
        {
            ZipEntry file = _zipFactory.MakeFileEntry(pathObject.PathInEPUB.GetFilePathInZip(_flatStructure), false);
            file.CompressionMethod = CompressionMethod.Deflated; // as defined by ePub stndard
            stream.PutNextEntry(file);
        }

        protected void PutPageToFile(ZipOutputStream stream, IBaseXHTMLFile xhtmlFile)
        {
            xhtmlFile.FlatStructure = FlatStructure;
            xhtmlFile.EmbedStyles = EmbedStyles;
            xhtmlFile.StyleFiles.Add(_mainCss);
            xhtmlFile.Write(stream);
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
                throw new InvalidDataException("File data not valid. Can't generate.");
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
                throw;
            }
        }

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

        private void AddMimeTypeEntry(ZipOutputStream s)
        {
            s.SetLevel(0);
            ZipEntry entry = _zipFactory.MakeFileEntry("mimetype", false);
            entry.CompressionMethod = CompressionMethod.Stored;
            entry.IsUnicodeText = false;
            entry.ZipFileIndex = 0;
            s.PutNextEntry(entry);
            const string mimetype = "application/epub+zip";
            var encoding = new ASCIIEncoding();
            byte[] buffer = encoding.GetBytes(mimetype);
            s.Write(buffer, 0, buffer.Length);
            s.CloseEntry();
        }

        /// <summary>
        /// Adds content of META-INF subfolder
        /// </summary>
        /// <param name="stream"></param>
        private void AddMetaData(ZipOutputStream stream)
        {
            AddMetaDataFile(stream);
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
        /// <summary>
        /// Assign ePub fonts from set of Fonts settings
        /// </summary>
        /// <param name="fonts">font settings to use</param>
        /// <param name="resourcesPath">path to program's "resources"</param>
        /// <param name="decorateFontNames">add or not random decoration to the font names</param>
        public void SetEPubFonts(IEPubFontSettings fonts, string resourcesPath, bool decorateFontNames)
        {
            _fontSettings.ResourceMask = resourcesPath;
            _fontSettings.Load(fonts, decorateFontNames ? _title.Identifiers[0].IdentifierName : string.Empty);

            AddFontsToCSS(_fontSettings.Fonts);
            AddCssElementsToCSS(_fontSettings.CssElements);
        }

        private void AddCssElementsToCSS(Dictionary<string, Dictionary<string, List<ICSSFontFamily>>> cssElements)
        {
            // Now add the elements
            foreach (var elementName in cssElements.Keys)
            {
                foreach (var elementClass in cssElements[elementName].Keys)
                {
                    var cssItem = new BaseCSSItem();
                    var sb = new StringBuilder();
                    sb.Append(elementName);
                    if (!string.IsNullOrEmpty(elementClass))
                    {
                        sb.AppendFormat(".{0}", elementClass);
                    }
                    cssItem.Name = sb.ToString();

                    // now build a list of fonts
                    sb.Clear();
                    int counter = 0;
                    if (elementClass != null)
                    {
                        foreach (var fontFamily in cssElements[elementName][elementClass])
                        {
                            sb.AppendFormat("\"{0}\"", fontFamily.Name);
                            if (counter != 0)
                            {
                                sb.Append(", ");
                            }
                            counter++;
                        }
                    }
                    cssItem.Parameters.Add("font-family", sb.ToString());
                    _mainCss.AddTarget(cssItem);
                }
            }
        }


        private void AddFontsToCSS(Dictionary<string, ICSSFontFamily> fontsFamilies)
        {
            // Add the fonts to CSS
            foreach (var cssFontFamily in fontsFamilies)
            {
                foreach (var subFont in cssFontFamily.Value.Fonts)
                {
                    var cssFont = new CssFontDefinition
                    {
                        Family = cssFontFamily.Key,
                        FontStyle = CssFontDefinition.FromStyle(subFont.FontStyle),
                        FontWidth = CssFontDefinition.FromWidth(subFont.FontWidth)
                    };
                    var sources = subFont.Sources.Select(fontSource => CssFontDefinition.ConvertToSourceString(fontSource, EmbedStyles, FlatStructure)).ToList();
                    cssFont.FontSrcs = sources;
                    _mainCss.AddFont(cssFont);
                }
            }
        }

    }

}
