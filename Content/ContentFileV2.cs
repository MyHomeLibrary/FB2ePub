using System;
using System.IO;
using System.Text;
using System.Xml;
using System.Xml.Linq;
using EPubLibrary.Content.CalibreMetadata;
using EPubLibrary.Content.Collections;
using EPubLibrary.Content.Guide;
using EPubLibrary.Content.Manifest;
using EPubLibrary.Content.Spine;
using EPubLibrary.CSS_Items;
using EPubLibrary.PathUtils;
using EPubLibrary.Template;
using EPubLibrary.TOC;
using EPubLibrary.XHTML_Items;

namespace EPubLibrary.Content
{
    public class ContentFileV2 : IEPubPath
    {
        public static readonly EPubInternalPath ContentFilePath = new EPubInternalPath(EPubInternalPath.DefaultOebpsFolder + "/content.opf");

        private readonly GuideSection _guide = new GuideSection();

        private readonly SpineSectionV2 _spine = new SpineSectionV2();

        private bool _flatStructure;
        private readonly ManifestSectionV2 _manifest = new ManifestSectionV2();


        /// <summary>
        /// Get/Set CalibreData object containing calibre's metadata to add to file
        /// </summary>
        public CalibreMetadataObject CalibreData { get; set; }


        /// <summary>
        /// Get /Set if flat structure used
        /// </summary>
        public bool FlatStructure
        {
            get { return _flatStructure; }
            set { _flatStructure = value; }
        }

        /// <summary>
        /// Returns epub version to write into a package
        /// </summary>
        /// <returns></returns>
        protected virtual string GetEPubVersion()
        {
            return "2.0";
        }

        private void CreateContentDocument(XDocument document)
        {
            if (Title == null)
            {
                throw new NullReferenceException("Please set Title first!");
            }
            AddPackageData(document);
            AddMetaDataToContentDocument(document.Root);
            AddManifestToContentDocument(document.Root);
            AddSpineToContentDocument(document.Root);
            AddGuideToContentDocument(document.Root);
        }

        protected virtual void AddPackageData(XDocument document)
        {
            var packagedata = new XElement(EPubNamespaces.OpfNameSpace + "package");
            packagedata.Add(new XAttribute("version", GetEPubVersion()));
            // we use ID of the first identifier
            packagedata.Add(new XAttribute("unique-identifier", Title.Identifiers[0].IdentifierName));
            packagedata.Add(new XAttribute("xmlns", EPubNamespaces.OpfNameSpace.NamespaceName));
            document.Add(packagedata);
        }

        private void AddManifestToContentDocument(XElement document)
        {
            XElement manifestElement = _manifest.GenerateManifestElement();
            document.Add(manifestElement);
        }


        private void AddGuideToContentDocument(XElement xElement)
        {
            if (_guide.HasData())
            {
                xElement.Add(_guide.GenerateGuide());
            }
        }

        private void AddSpineToContentDocument(XElement xElement)
        {
            XElement spineElement = _spine.GenerateSpineElement();
            xElement.Add(spineElement);
        }

        protected virtual void AddMetaDataToContentDocument(XElement document)
        {
            var metadata = new XElement(EPubNamespaces.OpfNameSpace + "metadata");
            metadata.Add(new XAttribute(XNamespace.Xmlns + "dc", PURLNamespaces.DCElements.NamespaceName));
            metadata.Add(new XAttribute(XNamespace.Xmlns + "xsi", WWWNamespaces.XSI.NamespaceName));
            metadata.Add(new XAttribute(XNamespace.Xmlns + "dcterms", PURLNamespaces.DCTerms.NamespaceName));
            metadata.Add(new XAttribute(XNamespace.Xmlns + "opf", EPubNamespaces.FakeOpf.NamespaceName));
            if (CalibreData!= null)
            {
                CalibreData.InjectNamespace(metadata);
            }

            foreach (var titleItem in Title.BookTitles)
            {
                var titleElement = new XElement(PURLNamespaces.DCElements + "title", titleItem.TitleName);
                if (!string.IsNullOrEmpty(titleItem.Language))
                {
                    // need to add writing language in "xml:lang — use RFC-3066 format"
                    // will add when will find an example since unclear               
                }
                metadata.Add(titleElement);
            }

            foreach (var languageItem in Title.Languages)
            {
                var language = new XElement(PURLNamespaces.DCElements + "language", languageItem);
                language.Add(new XAttribute(WWWNamespaces.XSI + "type", "dcterms:RFC3066"));
                metadata.Add(language);
            }

            foreach (var identifierItem in Title.Identifiers)
            {
                var identifier = new XElement(PURLNamespaces.DCElements + "identifier", identifierItem.ID);
                identifier.Add(new XAttribute("id", identifierItem.IdentifierName));
                identifier.Add(new XAttribute(EPubNamespaces.FakeOpf + "scheme", identifierItem.Scheme));
                metadata.Add(identifier);
            }

            if ( Title.DatePublished.HasValue)
            {
                var xDate = new XElement(PURLNamespaces.DCElements + "date", Title.DatePublished.Value.Year);
                xDate.Add(new XAttribute(EPubNamespaces.FakeOpf + "event", "original-publication"));
                metadata.Add(xDate);
            }

            foreach (var creatorItem in Title.Creators)
            {
                if (!string.IsNullOrEmpty(creatorItem.PersonName))
                {
                    var creator = new XElement(PURLNamespaces.DCElements + "creator", creatorItem.PersonName);
                    creator.Add(new XAttribute(EPubNamespaces.FakeOpf + "role", EPubRoles.ConvertEnumToAttribute(creatorItem.Role)));
                    if (!string.IsNullOrEmpty(creatorItem.FileAs))
                    {
                        creator.Add(new XAttribute(EPubNamespaces.FakeOpf + "file-as",creatorItem.FileAs));
                    }
                    if (!string.IsNullOrEmpty(creatorItem.Language))
                    {
                        // need to add writing language in "xml:lang — use RFC-3066 format"
                        // will add when will find an example since unclear
                    }
                    metadata.Add(creator);
                }
            }

            foreach (var contributorItem in Title.Contributors)
            {
                if (!string.IsNullOrEmpty(contributorItem.PersonName))
                {
                    var contributor = new XElement(PURLNamespaces.DCElements + "contributor", contributorItem.PersonName);
                    contributor.Add(new XAttribute(EPubNamespaces.FakeOpf + "role", EPubRoles.ConvertEnumToAttribute(contributorItem.Role)));
                    if (!string.IsNullOrEmpty(contributorItem.FileAs))
                    {
                        contributor.Add(new XAttribute(EPubNamespaces.FakeOpf + "file-as", contributorItem.FileAs));
                    }
                    if (!string.IsNullOrEmpty(contributorItem.Language))
                    {
                        // need to add writing language in "xml:lang — use RFC-3066 format"
                        // will add when will find an example since unclear
                    }
                    metadata.Add(contributor);
                }
            }
            if (CreatorSoftwareString != null)
            {
                var maker = new XElement(PURLNamespaces.DCElements + "contributor", CreatorSoftwareString);
                maker.Add(new XAttribute(EPubNamespaces.FakeOpf + "role",
                    EPubRoles.ConvertEnumToAttribute(RolesEnum.BookProducer)));
                metadata.Add(maker);
            }

            if (!string.IsNullOrEmpty(Title.Publisher.PublisherName))
            {
                var publisher = new XElement(PURLNamespaces.DCElements + "publisher", Title.Publisher.PublisherName);
                if (!string.IsNullOrEmpty(Title.Publisher.Language))
                {
                    // need to add writing language in "xml:lang — use RFC-3066 format"
                    // will add when will find an example since unclear
                }
                metadata.Add(publisher);
            }

            if (!string.IsNullOrEmpty(Title.Description))
            {

                var publisher = new XElement(PURLNamespaces.DCElements + "description", Title.Description);
                metadata.Add(publisher);                
            }


            foreach (var subjectItem in Title.Subjects)
            {
                if (!string.IsNullOrEmpty(subjectItem.SubjectInfo))
                {
                    var contributor = new XElement(PURLNamespaces.DCElements + "subject", subjectItem.SubjectInfo);
                    if (!string.IsNullOrEmpty(subjectItem.Language))
                    {
                        // need to add writing language in "xml:lang — use RFC-3066 format"
                        // will add when will find an example since unclear
                    }
                    metadata.Add(contributor);
                }
            }



            // This needed for iTunes and other apple made devices/programs to display the cover
            if (!string.IsNullOrEmpty(CoverId))
            {
                // <meta name="cover" content="cover.jpg"/>
                var cover = new XElement(EPubNamespaces.OpfNameSpace + "meta");
                cover.Add(new XAttribute("name","cover"));
                cover.Add(new XAttribute("content", CoverId));
                metadata.Add(cover);
            }

            if (CalibreData != null)
            {
                CalibreData.InjectData(metadata);
            }

            document.Add(metadata);
        }



        /// <summary>
        /// Get/Set book title
        /// </summary>
        public EPubTitleSettings Title { get; set; }

        /// <summary>
        /// get/set Id of the cover image file
        /// </summary>
        public string CoverId{ get; set; }

        /// <summary>
        /// Writes content to stream
        /// </summary>
        /// <param name="s"></param>
        public void Write (Stream s)
        {
            var contentDocument = new XDocument();
            CreateContentDocument(contentDocument);
            string str = FixDocument(contentDocument);
            var encoding = new UTF8Encoding();
            s.Write(encoding.GetBytes(str), 0, encoding.GetByteCount(str));            

        }


        /// <summary>
        /// Fixes namespace issue
        /// we need this since .Net add namespace (opf) before "root" (metadata) element name 
        /// if we set Xmlns and opf to same id
        /// and some readers do not like it
        /// so we fake the opf namespace with "template" and then replace it here 
        /// </summary>
        /// <param name="contentDocument"></param>
        /// <returns></returns>
        private string FixDocument(XDocument contentDocument)
        {
            var settings = new XmlWriterSettings {CloseOutput = false, Encoding = Encoding.UTF8, Indent = true};
            var ms = new MemoryStream();
            using (var writer = XmlWriter.Create(ms, settings))
            {
                contentDocument.WriteTo(writer);
            }
            ms.Seek(0, SeekOrigin.Begin);
            var buffer = new byte[ms.Length];
            ms.Read(buffer, 0, buffer.Length);
            var encoding = new UTF8Encoding();
            string str = encoding.GetString(buffer);
            str = str.Replace(EPubNamespaces.FakeOpf.NamespaceName, EPubNamespaces.OpfNameSpace.NamespaceName);
            return str;
        }

        public void AddXHTMLTextItem(BaseXHTMLFile baseXhtmlFile)
        {
            var bookItem = new ManifestItemV2 { HRef = baseXhtmlFile.PathInEPUB.GetRelativePath(ContentFilePath, _flatStructure), ID = baseXhtmlFile.Id, MediaType = EPubCoreMediaType.ApplicationXhtmlXml };
            _manifest.Add(bookItem);

            if (baseXhtmlFile.DocumentType != GuideTypeEnum.Ignore) // we do not add objects that to be ignored 
            {
                var bookSpine = new SpineItemV2 {Name = baseXhtmlFile.Id};
                _spine.Add(bookSpine);
            }

            _guide.AddGuideItem(bookItem.HRef, baseXhtmlFile.Id, baseXhtmlFile.DocumentType);                
        }

        public void AddTOC()
        {
            var tocItem = new ManifestItemV2 { HRef = TOCFile.TOCFilePath.GetRelativePath(ContentFilePath, _flatStructure), ID = "ncx", MediaType = EPubCoreMediaType.ApplicationNCX };
            _manifest.Add(tocItem);                     
        }

        public void AddImage(ImageOnStorage image)
        {
            _manifest.Add(new ManifestItemV2 { HRef = image.PathInEPUB.GetRelativePath(ContentFilePath, _flatStructure), ID = image.ID, MediaType = EPUBImage.ConvertImageTypeToMediaType(image.ImageType) });
        }

        public void AddCSS(CSSFile cssFile)
        {
            var maincss = new ManifestItemV2 { HRef = cssFile.PathInEPUB.GetRelativePath(ContentFilePath,_flatStructure), ID = cssFile.ID, MediaType = CSSFile.MediaType };
            _manifest.Add(maincss);
        }

        public void AddXPGTTemplate(AdobeTemplate template)
        {
            var maincss = new ManifestItemV2 { HRef = template.PathInEPUB.GetRelativePath(ContentFilePath, _flatStructure), ID = template.ID, MediaType = template.GetMediaType() };
            _manifest.Add(maincss);
        }


        public void AddFontFile(FontOnStorage fontFile)
        {
            _manifest.Add(new ManifestItemV2 { HRef = fontFile.PathInEPUB.GetRelativePath(ContentFilePath, _flatStructure), ID = fontFile.ID, MediaType =fontFile.MediaType });
        }


        /// <summary>
        /// Returns path in ePub 
        /// </summary>
        public EPubInternalPath PathInEPUB
        {
            get { return ContentFilePath; }
        }

        /// <summary>
        /// Get/Set string by software creator to identify creator software
        /// </summary>
        public string CreatorSoftwareString { get; set; }

    }
}
