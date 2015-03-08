using ConverterContracts.ConversionElementsStyles;
using EPubLibrary;
using EPubLibrary.Content.Guide;
using EPubLibrary.XHTML_Items;
using EPubLibraryContracts.Settings;
using FB2EPubConverter.ElementConvertersV3.Epigraph;
using FB2Library;
using FB2Library.Elements;
using XHTMLClassLibrary.BaseElements;
using XHTMLClassLibrary.BaseElements.BlockElements;

namespace FB2EPubConverter.ElementConvertersV3
{
    internal class Fb2EPubTextConverterV3 : BaseElementConverterV3
    {
        private readonly IEPubCommonSettings _commonSettings;
        private readonly ImageManager _images;
        private readonly HRefManagerV3 _referencesManager;
        private readonly ulong _maxSize;

        private int _sectionCounter;

        public Fb2EPubTextConverterV3(IEPubCommonSettings commonSettings, ImageManager images, HRefManagerV3 referencesManager, ulong maxSize)
        {
            _commonSettings = commonSettings;
            _images = images;
            _referencesManager = referencesManager;
            _maxSize = maxSize;
        }

        public void Convert(EPubFileV3 epubFile, FB2File fb2File)
        {
            // create second title page
            if ((fb2File.MainBody.Title != null) && (!string.IsNullOrEmpty(fb2File.MainBody.Title.ToString())))
            {
                string docTitle = fb2File.MainBody.Title.ToString();
                Logger.Log.DebugFormat("Adding section : {0}", docTitle);
                BookDocument addTitlePage = epubFile.AddDocument(docTitle);
                addTitlePage.DocumentType = GuideTypeEnum.TitlePage;
                addTitlePage.Content = new Div(HTMLElementType.HTML5);
                var converterSettings = new ConverterOptionsV3
                {
                    CapitalDrop = _commonSettings.CapitalDrop,
                    Images = _images,
                    MaxSize = _maxSize,
                    ReferencesManager = _referencesManager,
                };
                var titleConverter = new TitleConverterV3();
                addTitlePage.Content.Add(titleConverter.Convert(fb2File.MainBody.Title,
                    new TitleConverterParamsV3 { Settings = converterSettings, TitleLevel = 2 }));
                addTitlePage.NavigationParent = null;
                addTitlePage.FileName = string.Format("section{0}.xhtml", ++_sectionCounter);
                addTitlePage.NotPartOfNavigation = true;
            }

            BookDocument mainDocument = null;
            if (!string.IsNullOrEmpty(fb2File.MainBody.Name))
            {
                string docTitle = fb2File.MainBody.Name;
                Logger.Log.DebugFormat("Adding section : {0}", docTitle);
                mainDocument = epubFile.AddDocument(docTitle);
                mainDocument.DocumentType = GuideTypeEnum.Text;
                mainDocument.Content = new Div(HTMLElementType.HTML5);
                mainDocument.NavigationParent = null;
                mainDocument.FileName = string.Format("section{0}.xhtml", ++_sectionCounter);
            }

            if ((fb2File.MainBody.ImageName != null) && !string.IsNullOrEmpty(fb2File.MainBody.ImageName.HRef))
            {
                if (mainDocument == null)
                {
                    string newDocTitle = ((fb2File.MainBody.Title != null) && (!string.IsNullOrEmpty(fb2File.MainBody.Title.ToString()))) ? fb2File.MainBody.Title.ToString() : "main";
                    mainDocument = epubFile.AddDocument(newDocTitle);
                    mainDocument.DocumentType = GuideTypeEnum.Text;
                    mainDocument.Content = new Div(HTMLElementType.HTML5);
                    mainDocument.NavigationParent = null;
                    mainDocument.FileName = string.Format("section{0}.xhtml", ++_sectionCounter);
                }
                if (_images.IsImageIdReal(fb2File.MainBody.ImageName.HRef))
                {
                    var enclosing = new Div(HTMLElementType.HTML5); // we use the enclosing so the user can style center it
                    var converterSettings = new ConverterOptionsV3
                    {
                        CapitalDrop = _commonSettings.CapitalDrop,
                        Images = _images,
                        MaxSize = _maxSize,
                        ReferencesManager = _referencesManager,
                    };

                    var imageConverter = new ImageConverterV3();
                    enclosing.Add(imageConverter.Convert(fb2File.MainBody.ImageName, new ImageConverterParamsV3 { Settings = converterSettings }));
                    SetClassType(enclosing, ElementStylesV3.BodyImage);
                    mainDocument.Content.Add(enclosing);
                }


            }
            foreach (var ep in fb2File.MainBody.Epigraphs)
            {
                if (mainDocument == null)
                {
                    string newDocTitle = ((fb2File.MainBody.Title != null) && (!string.IsNullOrEmpty(fb2File.MainBody.Title.ToString()))) ? fb2File.MainBody.Title.ToString() : "main";
                    mainDocument = epubFile.AddDocument(newDocTitle);
                    mainDocument.DocumentType = GuideTypeEnum.Text;
                    mainDocument.Content = new Div(HTMLElementType.HTML5);
                    mainDocument.NavigationParent = null;
                    mainDocument.FileName = string.Format("section{0}.xhtml", ++_sectionCounter);
                }
                var converterSettings = new ConverterOptionsV3
                {
                    CapitalDrop = _commonSettings.CapitalDrop,
                    Images = _images,
                    MaxSize = _maxSize,
                    ReferencesManager = _referencesManager,
                };

                var epigraphConverter = new MainEpigraphConverterV3();
                mainDocument.Content.Add(epigraphConverter.Convert(ep,
                    new EpigraphConverterParamsV3 { Settings = converterSettings, Level = 1 }));
            }

            Logger.Log.Debug("Adding main sections");
            foreach (var section in fb2File.MainBody.Sections)
            {
                AddSection(epubFile, section, mainDocument, false);
            }

            Logger.Log.Debug("Adding secondary bodies");
            foreach (var bodyItem in fb2File.Bodies)
            {
                if (bodyItem == fb2File.MainBody)
                {
                    continue;
                }
                bool fbeNotesSection = FBENotesSection(bodyItem.Name);
                if (fbeNotesSection)
                {
                    AddFbeNotesBody(epubFile, bodyItem);
                }
                else
                {
                    AddSecondaryBody(epubFile, bodyItem);
                }
            }
        }

        private void AddSection(EPubFileV3 epubFile, SectionItem section, BookDocument navParent, bool fbeNotesSection)
        {
            string docTitle = string.Empty;
            if (section.Title != null)
            {
                docTitle = section.Title.ToString();
            }
            Logger.Log.DebugFormat("Adding section : {0}", docTitle);
            BookDocument sectionDocument = null;
            bool firstDocumentOfSplit = true;
            var converterSettings = new ConverterOptionsV3
            {
                CapitalDrop = !fbeNotesSection && _commonSettings.CapitalDrop,
                Images = _images,
                MaxSize = _maxSize,
                ReferencesManager = _referencesManager,
            };
            var sectionConverter = new SectionConverterV3
            {
                LinkSection = fbeNotesSection,
                RecursionLevel = GetRecursionLevel(navParent),
                Settings = converterSettings
            };
            foreach (var subitem in sectionConverter.Convert(section))
            {
                sectionDocument = epubFile.AddDocument(docTitle);
                sectionDocument.DocumentType = (navParent == null) ? GuideTypeEnum.Text : navParent.DocumentType;
                sectionDocument.Type = (navParent == null) ? SectionTypeEnum.Text : navParent.Type;
                sectionDocument.Content = subitem;
                sectionDocument.NavigationParent = navParent;
                sectionDocument.FileName = string.Format("section{0}.xhtml", ++_sectionCounter);
                if (!firstDocumentOfSplit || (sectionDocument.Type == SectionTypeEnum.Links))
                {
                    sectionDocument.NotPartOfNavigation = true;
                }
                firstDocumentOfSplit = false;
            }
            Logger.Log.Debug("Adding sub-sections");
            foreach (var subSection in section.SubSections)
            {
                AddSection(epubFile, subSection, sectionDocument, fbeNotesSection);
            }
        }

        private static int GetRecursionLevel(BookDocument navParent)
        {
            if (navParent == null)
            {
                return 1;
            }
            return navParent.NavigationLevel + 1;
        }

        /// <summary>
        /// Add and convert FBE style generated notes sections
        /// </summary>
        /// <param name="epubFile"></param>
        /// <param name="bodyItem"></param>
        private void AddFbeNotesBody(EPubFileV3 epubFile, BodyItem bodyItem)
        {
            string docTitle = bodyItem.Name;
            Logger.Log.DebugFormat("Adding section : {0}", docTitle);
            var sectionDocument = epubFile.AddDocument(docTitle);
            sectionDocument.DocumentType = GuideTypeEnum.Glossary;
            sectionDocument.Type = SectionTypeEnum.Links;
            sectionDocument.Content = new Div(HTMLElementType.HTML5);
            if (bodyItem.Title != null)
            {
                var converterSettings = new ConverterOptionsV3
                {
                    CapitalDrop = false,
                    Images = _images,
                    MaxSize = _maxSize,
                    ReferencesManager = _referencesManager,
                };
                var titleConverter = new TitleConverterV3();
                sectionDocument.Content.Add(titleConverter.Convert(bodyItem.Title,
                    new TitleConverterParamsV3 { Settings = converterSettings, TitleLevel = 1 }));
            }
            sectionDocument.NavigationParent = null;
            sectionDocument.NotPartOfNavigation = true;
            sectionDocument.FileName = string.Format("section{0}.xhtml", ++_sectionCounter);
            Logger.Log.Debug("Adding sub-sections");
            foreach (var section in bodyItem.Sections)
            {
                AddSection(epubFile, section, sectionDocument, true);
            }
        }

        /// <summary>
        /// Add and convert generic secondary body section
        /// </summary>
        /// <param name="epubFile"></param>
        /// <param name="bodyItem"></param>
        private void AddSecondaryBody(EPubFileV3 epubFile, BodyItem bodyItem)
        {
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
            var sectionDocument = epubFile.AddDocument(docTitle);
            sectionDocument.DocumentType = GuideTypeEnum.Text;
            sectionDocument.Type = SectionTypeEnum.Text;
            sectionDocument.Content = new Div(HTMLElementType.HTML5);
            if (bodyItem.Title != null)
            {
                var converterSettings = new ConverterOptionsV3
                {
                    CapitalDrop = _commonSettings.CapitalDrop,
                    Images = _images,
                    MaxSize = _maxSize,
                    ReferencesManager = _referencesManager,
                };
                var titleConverter = new TitleConverterV3();
                sectionDocument.Content.Add(titleConverter.Convert(bodyItem.Title,
                    new TitleConverterParamsV3 { Settings = converterSettings, TitleLevel = 1 }));
            }
            sectionDocument.NavigationParent = null;
            sectionDocument.NotPartOfNavigation = false;
            sectionDocument.FileName = string.Format("section{0}.xhtml", ++_sectionCounter);
            Logger.Log.Debug("Adding sub-sections");
            foreach (var section in bodyItem.Sections)
            {
                AddSection(epubFile, section, sectionDocument, false);
            }
        }

        /// <summary>
        /// Check if the body is FBE generated notes section
        /// </summary>
        /// <param name="name">body name</param>
        /// <returns>true if it is FBE generated notes/comments body</returns>
        private static bool FBENotesSection(string name)
        {
            switch (name)
            {
                case "comments":
                case "footnotes":
                case "notes":
                    return true;
            }
            return false;
        }

    }
}
