using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Xml.Linq;
using EPubLibrary.Container;
using EPubLibrary.Content;
using EPubLibrary.Content.NavigationDocument;
using EPubLibrary.TOC;
using EPubLibrary.XHTML_Items;
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
    public class EPubFileV3 : EPubFile
    {
        protected V3Standard _standard  = V3Standard.V301;
        private readonly NavigationDocumentFile _navigationDocument = new NavigationDocumentFile();


        /// <summary>
        /// Get/Set if adobe template XPGT file should be added to resulting file
        /// </summary>
        public override bool UseAdobeTemplate
        {
            get { return false; }
        }

        public V3Standard EPubV3Standard
        {
            get { return _standard; }
        }

        public EPubFileV3(V3Standard standard)
        {
            _standard = standard;
            _content = new ContentFileV3(standard);
            _tableOfContentFile = new TOCFileV3Transitional();
        }


        protected bool _generateCompatibleTOC = false;

        public bool GenerateCompatibleTOC
        {
            get { return _generateCompatibleTOC; }
            set { ((ContentFileV3)_content).GenerateCompatibleTOC = _generateCompatibleTOC = value; }
        }


        protected override void CreateContainer(out ContainerFile container)
        {
            container = new ContainerFileV3 { FlatStructure = _flatStructure, ContentFilePath = _content };
        }


        protected override void AddContentFile(ZipOutputStream stream)
        {
            stream.SetLevel(9);
            CreateFileEntryInZip(stream, _content);
            _content.Title = _title;
            _content.Collections = _collections;
            _content.Write(stream);
        }

        /// <summary>
        /// Adds (creates) a new empty document in a list of book content documents
        /// </summary>
        /// <param name="id">id - title to assign to the new document</param>
        /// <returns></returns>
        public override BookDocument AddDocument(string id)
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
        protected override void AddLicenseFile(ZipOutputStream stream)
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
        protected override void AddAbout(ZipOutputStream stream)
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
        protected override void AddBookData(ZipOutputStream stream)
        {
            if (InjectLKRLicense)
            {
                AddLicenseFile(stream);
            }
            AddImages(stream);
            AddFontFiles(stream);
            AddAdditionalFiles(stream);
            if (GenerateCompatibleTOC)
            {
                AddTOCFile(stream);
            }
            AddNavigationFile(stream);
            AddContentFile(stream);
        }

        protected void AddNavigationFile(ZipOutputStream stream)
        {
            stream.SetLevel(9);
            CreateFileEntryInZip(stream, _navigationDocument);
            _navigationDocument.PageTitle = _title.BookTitles[0].TitleName;
            _navigationDocument.StyleFiles.Add(_mainCss);
            _navigationDocument.Write(stream);
            var contentFileV3 = _content as ContentFileV3;
            if (contentFileV3 != null) contentFileV3.AddNavigationDocument(_navigationDocument);
        }

        protected override void AddCover(ZipOutputStream stream)
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

        /// <summary>
        /// Writes book content to the stream
        /// </summary>
        /// <param name="stream">stream to write to</param>
        protected override void AddBookContent(ZipOutputStream stream)
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
                if (docSize >= BookDocument.MaxSize)
                {
                    // This case is not for converter
                    // after converter the files should be in right size already
                    int subCount = 0;
                    foreach (var subsection in section.Split())
                    {
                        subsection.FlatStructure = FlatStructure;
                        subsection.EmbedStyles = EmbedStyles;
                        subsection.FileName = string.Format("{0}_{1}.xhtml", Path.GetFileNameWithoutExtension(section.FileName), subCount);
                        CreateFileEntryInZip(stream, subsection);
                        subsection.Write(stream);
                        AddBookContentSection(subsection, count, subCount);
                        subCount++;
                    }
                    count++;
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
            _tableOfContentFile.Consolidate();

            // to be valid we need at least one NAVPoint
            if (_tableOfContentFile.IsNavMapEmpty() && (_sections.Count > 0))
            {
                _tableOfContentFile.AddNavPoint(_sections[0], _rule.Translate(_title.BookTitles[0].TitleName, TranliterateToc ? TranslitMode : TranslitModeEnum.None));
                _navigationDocument.AddNavPoint(_sections[0], _rule.Translate(_title.BookTitles[0].TitleName, TranliterateToc ? TranslitMode : TranslitModeEnum.None));
            }
        }


        protected override void AddBookContentSection(BookDocument subsection, int count, int subcount)
        {
            subsection.Id = string.Format("bookcontent{0}_{1}", count, subcount); // generate unique ID
            _content.AddXHTMLTextItem(subsection);
            if (!subsection.NotPartOfNavigation)
            {
                if (subsection.NavigationLevel <= 1)
                {
                    _tableOfContentFile.AddNavPoint(subsection, _rule.Translate(subsection.PageTitle, TranliterateToc ? TranslitMode : TranslitModeEnum.None));
                    _navigationDocument.AddNavPoint(subsection, _rule.Translate(subsection.PageTitle, TranliterateToc ? TranslitMode : TranslitModeEnum.None));
                }
                else
                {
                    _tableOfContentFile.AddSubNavPoint(subsection.NavigationParent, subsection, _rule.Translate(subsection.PageTitle, TranliterateToc ? TranslitMode : TranslitModeEnum.None));
                    _navigationDocument.AddSubNavPoint(subsection.NavigationParent, subsection, _rule.Translate(subsection.PageTitle, TranliterateToc ? TranslitMode : TranslitModeEnum.None));
                }
            }
        }


    }
}
