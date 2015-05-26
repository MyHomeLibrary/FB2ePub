using System.Collections.Generic;
using EPubLibrary.PathUtils;
using EPubLibraryContracts;
using EPubLibraryContracts.Settings;
using FB2EPubConverter.ElementConvertersV3;
using FB2EPubConverter.PrepearedHTMLFiles;
using FB2Library.Elements;
using XHTMLClassLibrary.BaseElements;
using XHTMLClassLibrary.BaseElements.BlockElements;
using EPubLibrary.XHTML_Items;

namespace FB2EPubConverter
{
    internal class FBNotesManager
    {
        private readonly List<BodyItem>  _bodyItems = new List<BodyItem>();
        private readonly IEPubV3Settings _v3Settings;
        private readonly ImageManager _images;
        private readonly HRefManagerV3 _referencesManager;

        private int _sectionCounter;

        private const string EPubNotesFileNameFormat = "notes_section{0}.xhtml";

        private string BuildSectionFileName()
        {
            return string.Format(EPubNotesFileNameFormat, ++_sectionCounter);
        }


        public FBNotesManager(IEPubV3Settings v3Settings, ImageManager images, HRefManagerV3 referencesManager)
        {
            _v3Settings = v3Settings;
            _images = images;
            _referencesManager = referencesManager;

        }

        public void AddNotesBody(BodyItem bodyItem)
        {
            if (!_bodyItems.Contains(bodyItem))
            {
                _bodyItems.Add(bodyItem);
            }
        }

        public List<FB2NotesPageSectionFile> GetFootNotesAdditionalDocuments()
        {
            List<FB2NotesPageSectionFile> documents = new List<FB2NotesPageSectionFile>();

            if (_v3Settings.FootnotesCreationMode == FootnotesGenerationMode.V2StyleSections ||
                _v3Settings.FootnotesCreationMode == FootnotesGenerationMode.Combined)
            {
                foreach (var bodyItem in _bodyItems)
                {
                    documents.AddRange(AddV2StyleFbeNotesBody(bodyItem));
                }
            }
            return documents;
        }

        private List<FB2NotesPageSectionFile> AddV2StyleFbeNotesBody(BodyItem bodyItem)
        {
            List<FB2NotesPageSectionFile> documents = new List<FB2NotesPageSectionFile>();
            string docTitle = bodyItem.Name;
            Logger.Log.DebugFormat("Adding notes section : {0}", docTitle);
            var sectionDocument = new FB2NotesPageSectionFile
            {
                PageTitle = docTitle,
                FileEPubInternalPath = EPubInternalPath.GetDefaultLocation(DefaultLocations.DefaultTextFolder),
                GuideRole = GuideTypeEnum.Glossary,
                Content = new Div(HTMLElementType.HTML5),
                NavigationParent = null,
                NotPartOfNavigation = true,
                FileName = BuildSectionFileName()
            };
            if (bodyItem.Title != null)
            {
                var converterSettings = new ConverterOptionsV3
                {
                    CapitalDrop = false,
                    Images = _images,
                    MaxSize = _v3Settings.HTMLFileMaxSize,
                    ReferencesManager = _referencesManager,
                };
                var titleConverter = new TitleConverterV3();
                sectionDocument.Content.Add(titleConverter.Convert(bodyItem.Title,
                    new TitleConverterParamsV3 { Settings = converterSettings, TitleLevel = 1 }));
            }
            documents.Add(sectionDocument);

            Logger.Log.Debug("Adding sub-sections");
            foreach (var section in bodyItem.Sections)
            {
                documents.AddRange(AddSection(section, sectionDocument));
            }
            return documents;
        }

        private List<FB2NotesPageSectionFile> AddSection(SectionItem section, BaseXHTMLFileV3 navParent)
        {
            List<FB2NotesPageSectionFile> documents = new List<FB2NotesPageSectionFile>();

            string docTitle = string.Empty;
            if (section.Title != null)
            {
                docTitle = section.Title.ToString();
            }
            Logger.Log.DebugFormat("Adding notes section : {0}", docTitle);
            FB2NotesPageSectionFile sectionDocument = null;
            bool firstDocumentOfSplit = true;
            var converterSettings = new ConverterOptionsV3
            {
                CapitalDrop = false,
                Images = _images,
                MaxSize = _v3Settings.HTMLFileMaxSize,
                ReferencesManager = _referencesManager,
            };
            var sectionConverter = new SectionConverterV3
            {
                LinkSection = true,
                RecursionLevel = GetRecursionLevel(navParent),
                Settings = converterSettings
            };
            foreach (var subitem in sectionConverter.Convert(section))
            {
                sectionDocument = new FB2NotesPageSectionFile
                {
                    PageTitle = docTitle,
                    FileEPubInternalPath = EPubInternalPath.GetDefaultLocation(DefaultLocations.DefaultTextFolder),
                    GuideRole = (navParent == null) ? GuideTypeEnum.Text : navParent.GuideRole,
                    Content = subitem,
                    NavigationParent = navParent,
                    FileName = BuildSectionFileName()
                };

                if (!firstDocumentOfSplit ||
                    ((navParent != null) && navParent.NotPartOfNavigation))
                {
                    sectionDocument.NotPartOfNavigation = true;
                }
                firstDocumentOfSplit = false;
                documents.Add(sectionDocument);
            }

            Logger.Log.Debug("Adding sub-sections");
            foreach (var subSection in section.SubSections)
            {
                documents.AddRange(AddSection(subSection, sectionDocument));
            }
            return documents;
        }

        private static int GetRecursionLevel(BaseXHTMLFileV3 navParent)
        {
            if (navParent == null)
            {
                return 1;
            }
            return navParent.NavigationLevel + 1;
        }

    }
}
