using ConverterContracts.ConversionElementsStyles;
using EPubLibrary;
using EPubLibrary.PathUtils;
using EPubLibrary.XHTML_Items;
using EPubLibraryContracts;
using EPubLibraryContracts.Settings;
using FB2EPubConverter.ElementConvertersV2.Epigraph;
using FB2Library;
using FB2Library.Elements;
using XHTMLClassLibrary.BaseElements;
using XHTMLClassLibrary.BaseElements.BlockElements;

namespace FB2EPubConverter.ElementConvertersV2
{
    internal class Fb2EPubTextConverterV2 : BaseElementConverterV2
    {
        private readonly IEPubCommonSettings _commonSettings;
        private readonly ImageManager _images;
        private readonly HRefManagerV2 _referencesManager;
        private readonly ulong _maxSize;

        private int _sectionCounter;

        internal Fb2EPubTextConverterV2(IEPubCommonSettings commonSettings, ImageManager images, HRefManagerV2 referencesManager, ulong maxSize)
        {
            _commonSettings = commonSettings;
            _images = images;
            _referencesManager = referencesManager;
            _maxSize = maxSize;
        }


        public void Convert(EPubFileV2 epubFile, FB2File fb2File)
        {
            // create second title page
            if ((fb2File.MainBody.Title != null) && (!string.IsNullOrEmpty(fb2File.MainBody.Title.ToString())))
            {
                string docTitle = fb2File.MainBody.Title.ToString();
                Logger.Log.DebugFormat("Adding section : {0}", docTitle);
                BaseXHTMLFileV2 addTitlePage = new BaseXHTMLFileV2
                {
                    GuideRole = GuideTypeEnum.TitlePage,
                    Content = new Div(BaseXHTMLFileV2.Compatibility),
                    Type = SectionTypeEnum.Text,
                    FileEPubInternalPath = EPubInternalPath.GetDefaultTextFilesFolder(),
                    NavigationParent = null,
                    FileName = string.Format("section{0}.xhtml", ++_sectionCounter),
                    NotPartOfNavigation = true,
                    PageTitle = docTitle,
                };
                var converterSettings = new ConverterOptionsV2
                {
                    CapitalDrop = _commonSettings.CapitalDrop,
                    Images = _images,
                    MaxSize = _maxSize,
                    ReferencesManager = _referencesManager,
                };
                var titleConverter = new TitleConverterV2();
                addTitlePage.Content.Add(titleConverter.Convert(fb2File.MainBody.Title,
                    new TitleConverterParamsV2 { Settings = converterSettings, TitleLevel = 2 }));
                epubFile.AddXHTMLFile(addTitlePage);
            }

            BaseXHTMLFileV2 mainDocument = null;
            if (!string.IsNullOrEmpty(fb2File.MainBody.Name))
            {
                string docTitle = fb2File.MainBody.Name;
                Logger.Log.DebugFormat("Adding section : {0}", docTitle);
                mainDocument = new BaseXHTMLFileV2
                {
                    PageTitle = docTitle,
                    GuideRole = GuideTypeEnum.Text,
                    Content = new Div(BaseXHTMLFileV2.Compatibility),
                    Type = SectionTypeEnum.Text,
                    FileEPubInternalPath = EPubInternalPath.GetDefaultTextFilesFolder(),
                    NavigationParent = null,
                    FileName = string.Format("section{0}.xhtml", ++_sectionCounter)
                };
                epubFile.AddXHTMLFile(mainDocument);
            }

            if ((fb2File.MainBody.ImageName != null) && !string.IsNullOrEmpty(fb2File.MainBody.ImageName.HRef))
            {
                if (mainDocument == null)
                {
                    string newDocTitle = ((fb2File.MainBody.Title != null) && (!string.IsNullOrEmpty(fb2File.MainBody.Title.ToString()))) ? fb2File.MainBody.Title.ToString() : "main";
                    mainDocument = new BaseXHTMLFileV2
                    {
                        PageTitle = newDocTitle,
                        GuideRole = GuideTypeEnum.Text,
                        Content = new Div(BaseXHTMLFileV2.Compatibility),
                        NavigationParent = null,
                        Type = SectionTypeEnum.Text,
                        FileEPubInternalPath = EPubInternalPath.GetDefaultTextFilesFolder(),
                        FileName = string.Format("section{0}.xhtml", ++_sectionCounter)
                    };
                    epubFile.AddXHTMLFile(mainDocument);
                }
                if (_images.IsImageIdReal(fb2File.MainBody.ImageName.HRef))
                {
                    var enclosing = new Div(HTMLElementType.XHTML11); // we use the enclosing so the user can style center it
                    var converterSettings = new ConverterOptionsV2
                    {
                        CapitalDrop = _commonSettings.CapitalDrop,
                        Images = _images,
                        MaxSize = _maxSize,
                        ReferencesManager = _referencesManager,
                    };

                    var imageConverter = new ImageConverterV2();
                    enclosing.Add(imageConverter.Convert(fb2File.MainBody.ImageName, new ImageConverterParamsV2 { Settings = converterSettings }));
                    SetClassType(enclosing, ElementStylesV2.BodyImage);
                    mainDocument.Content.Add(enclosing);
                }


            }
            foreach (var ep in fb2File.MainBody.Epigraphs)
            {
                if (mainDocument == null)
                {
                    string newDocTitle = ((fb2File.MainBody.Title != null) && (!string.IsNullOrEmpty(fb2File.MainBody.Title.ToString()))) ? fb2File.MainBody.Title.ToString() : "main";
                    mainDocument = new BaseXHTMLFileV2
                    {
                        PageTitle = newDocTitle,
                        GuideRole = GuideTypeEnum.Text,
                        Content = new Div(BaseXHTMLFileV2.Compatibility),
                        NavigationParent = null,
                        Type = SectionTypeEnum.Text,
                        FileEPubInternalPath = EPubInternalPath.GetDefaultTextFilesFolder(),
                        FileName = string.Format("section{0}.xhtml", ++_sectionCounter)
                    };
                }
                var converterSettings = new ConverterOptionsV2
                {
                    CapitalDrop = _commonSettings.CapitalDrop,
                    Images = _images,
                    MaxSize = _maxSize,
                    ReferencesManager = _referencesManager,
                };

                var epigraphConverter = new MainEpigraphConverterV2();
                mainDocument.Content.Add(epigraphConverter.Convert(ep,
                    new EpigraphConverterParamsV2 { Settings = converterSettings, Level = 1 }));
                epubFile.AddXHTMLFile(mainDocument);
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

        private void AddSection(EPubFileV2 epubFile, SectionItem section, BaseXHTMLFileV2 navParent, bool fbeNotesSection)
        {
            string docTitle = string.Empty;
            if (section.Title != null)
            {
                docTitle = section.Title.ToString();
            }
            Logger.Log.DebugFormat("Adding section : {0}", docTitle);
            BaseXHTMLFileV2 sectionDocument = null;
            bool firstDocumentOfSplit = true;
            var converterSettings = new ConverterOptionsV2
            {
                CapitalDrop = !fbeNotesSection && _commonSettings.CapitalDrop,
                Images = _images,
                MaxSize = _maxSize,
                ReferencesManager = _referencesManager,
            };
            var sectionConverter = new SectionConverterV2
            {
                LinkSection = fbeNotesSection,
                RecursionLevel = GetRecursionLevel(navParent),
                Settings = converterSettings
            };
            foreach (var subitem in sectionConverter.Convert(section))
            {
                sectionDocument = new BaseXHTMLFileV2
                {
                    PageTitle = docTitle,
                    FileEPubInternalPath = EPubInternalPath.GetDefaultTextFilesFolder(),
                    GuideRole = (navParent == null) ? GuideTypeEnum.Text : navParent.GuideRole,
                    Type = (navParent == null) ? SectionTypeEnum.Text : navParent.Type,
                    Content = subitem,
                    NavigationParent = navParent,
                    FileName = string.Format("section{0}.xhtml", ++_sectionCounter)
                };
                if (!firstDocumentOfSplit || (sectionDocument.Type == SectionTypeEnum.Links))
                {
                    sectionDocument.NotPartOfNavigation = true;
                }
                firstDocumentOfSplit = false;
                epubFile.AddXHTMLFile(sectionDocument);
            }
            Logger.Log.Debug("Adding sub-sections");
            foreach (var subSection in section.SubSections)
            {
                AddSection(epubFile, subSection, sectionDocument, fbeNotesSection);
            }
        }

        private static int GetRecursionLevel(BaseXHTMLFileV2 navParent)
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
        private void AddFbeNotesBody(EPubFileV2 epubFile, BodyItem bodyItem)
        {
            string docTitle = bodyItem.Name;
            Logger.Log.DebugFormat("Adding section : {0}", docTitle);
            var sectionDocument = new BaseXHTMLFileV2
            {
                PageTitle = docTitle,
                FileEPubInternalPath = EPubInternalPath.GetDefaultTextFilesFolder(),
                GuideRole = GuideTypeEnum.Glossary,
                Type = SectionTypeEnum.Links,
                Content = new Div(BaseXHTMLFileV2.Compatibility),
                NavigationParent = null,
                NotPartOfNavigation = true,
                FileName = string.Format("section{0}.xhtml", ++_sectionCounter),

            };
            if (bodyItem.Title != null)
            {
                var converterSettings = new ConverterOptionsV2
                {
                    CapitalDrop = false,
                    Images = _images,
                    MaxSize = _maxSize,
                    ReferencesManager = _referencesManager,
                };
                var titleConverter = new TitleConverterV2();
                sectionDocument.Content.Add(titleConverter.Convert(bodyItem.Title,
                    new TitleConverterParamsV2 { Settings = converterSettings, TitleLevel = 1 }));
            }
            epubFile.AddXHTMLFile(sectionDocument);

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
        private void AddSecondaryBody(EPubFileV2 epubFile, BodyItem bodyItem)
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
            var sectionDocument = new BaseXHTMLFileV2
            {
                Id = docTitle,
                GuideRole = GuideTypeEnum.Text,
                Type = SectionTypeEnum.Text,
                FileEPubInternalPath = EPubInternalPath.GetDefaultTextFilesFolder(),
                Content = new Div(BaseXHTMLFileV2.Compatibility),
                NavigationParent = null,
                NotPartOfNavigation = false,
                FileName = string.Format("section{0}.xhtml", ++_sectionCounter)
            };

            if (bodyItem.Title != null)
            {
                var converterSettings = new ConverterOptionsV2
                {
                    CapitalDrop = _commonSettings.CapitalDrop,
                    Images = _images,
                    MaxSize = _maxSize,
                    ReferencesManager = _referencesManager,
                };
                var titleConverter = new TitleConverterV2();
                sectionDocument.Content.Add(titleConverter.Convert(bodyItem.Title,
                    new TitleConverterParamsV2 { Settings = converterSettings, TitleLevel = 1 }));
            }
            epubFile.AddXHTMLFile(sectionDocument);


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
