using System.Xml.Linq;
using System;
using EPubLibrary.Content.Bindings;
using EPubLibrary.Content.Collections;
using EPubLibrary.Content.Guide;
using EPubLibrary.Content.Manifest;
using EPubLibrary.Content.NavigationDocument;
using EPubLibrary.Content.NavigationManagement;
using EPubLibrary.Content.Spine;
using EPubLibrary.CSS_Items;
using EPubLibrary.ReferenceUtils;
using EPubLibrary.XHTML_Items;
using EPubLibrary.TOC;
using EPubLibrary.PathUtils;
using System.IO;
using System.Text;
using System.Xml;


namespace EPubLibrary.Content
{
    public class ContentFileV3 : IEPubPath
    {

        private readonly V3Standard _standard;   
        private readonly ManifestSectionV3 _manifest;
        private readonly BindingsV3 _bindings = new BindingsV3();
        private readonly  EPubSeriesCollections _seriesCollections  = new EPubSeriesCollections();
        private readonly NavigationManagerV3 _navigationManager =   new NavigationManagerV3();


        private readonly SpineSectionV3 _spine;

        private bool _flatStructure;


        /// <summary>
        /// Get /Set if flat structure used
        /// </summary>
        public bool FlatStructure
        {
            get { return _flatStructure; }
            set { _flatStructure = value; }
        }
        
        public ContentFileV3(V3Standard standard)
        {
            _standard = standard;
            _manifest   =   new ManifestSectionV3(standard);
            _spine = new SpineSectionV3(standard);
        }

        public bool GenerateCompatibleTOC { get; set; }

        public EPubSeriesCollections SeriesCollections { get { return _seriesCollections; }}

        /// <summary>
        /// Returns epub version to write into a package
        /// </summary>
        /// <returns></returns>
        protected string GetEPubVersion()
        {
            return "3.0";
        }

        protected void AddPackageData(XDocument document)
        {
            var packagedata = new XElement(EPubNamespaces.OpfNameSpace + "package");
            packagedata.Add(new XAttribute("version", GetEPubVersion()));
            // we use ID of the first identifier
            packagedata.Add(new XAttribute("unique-identifier", Title.Identifiers[0].IdentifierName));
            packagedata.Add(new XAttribute("xmlns", EPubNamespaces.OpfNameSpace.NamespaceName));
            packagedata.Add(new XAttribute(XNamespace.Xml + "lang",Title.Languages[0]));
            document.Add(packagedata);
        }

        protected void AddMetaDataToContentDocument(XElement document)
        {
            var metadata = new XElement(EPubNamespaces.OpfNameSpace + "metadata");
            metadata.Add(new XAttribute(XNamespace.Xmlns + "dc", PURLNamespaces.DCElements));
            metadata.Add(new XAttribute(XNamespace.Xmlns + "xsi", WWWNamespaces.XSI));
            metadata.Add(new XAttribute(XNamespace.Xmlns + "dcterms", PURLNamespaces.DCTerms));
            metadata.Add(new XAttribute(XNamespace.Xmlns + "opf", EPubNamespaces.FakeOpf.NamespaceName));

            Onix5SchemaConverter source = null;
            foreach (var identifierItem in Title.Identifiers)
            {
                var schemaConverter = new Onix5SchemaConverter(identifierItem);
                var identifier = new XElement(PURLNamespaces.DCElements + "identifier", schemaConverter.GetIdentifier());
                identifier.Add(new XAttribute("id", identifierItem.IdentifierName));
                metadata.Add(identifier);

                var metaRefine = new XElement(EPubNamespaces.FakeOpf + "meta", schemaConverter.GetIdentifierType());
                metaRefine.Add(new XAttribute("refines", "#" + identifierItem.IdentifierName));
                metaRefine.Add(new XAttribute("property", "identifier-type"));
                metaRefine.Add(new XAttribute("scheme", Onix5SchemaConverter.GetScheme()));
                metadata.Add(metaRefine);

                if (source == null && schemaConverter.IsISBN())
                {
                    source = schemaConverter;
                }
            }


            int titleIdCounter = 0;
            foreach (var titleItem in Title.BookTitles)
            {
                string idString = string.Format("t{0}",++titleIdCounter);
                var titleElement = new XElement(PURLNamespaces.DCElements + "title", titleItem.TitleName);
                titleElement.Add(new XAttribute("id",idString));
                metadata.Add(titleElement);

                var metaRefineType = new XElement(EPubNamespaces.FakeOpf + "meta",GetTitleType(titleItem));
                metaRefineType.Add(new XAttribute("refines", "#" + idString));
                metaRefineType.Add(new XAttribute("property", "title-type"));
                metadata.Add(metaRefineType);

                var metaRefineDisplay= new XElement(EPubNamespaces.FakeOpf + "meta", titleIdCounter);
                metaRefineDisplay.Add(new XAttribute("refines", "#" + idString));
                metaRefineDisplay.Add(new XAttribute("property", "display-seq"));
                metadata.Add(metaRefineDisplay);

            }

            foreach (var languageItem in Title.Languages)
            {
                var language = new XElement(PURLNamespaces.DCElements + "language", languageItem);
                metadata.Add(language);
            }



            foreach (var creatorItem in Title.Creators)
            {
                int creatorCounter = 0;
                if (!string.IsNullOrEmpty(creatorItem.PersonName))
                {
                    string creatorId = string.Format("creator{0}", ++creatorCounter);
                    var creator = new XElement(PURLNamespaces.DCElements + "creator", creatorItem.PersonName);
                    creator.Add(new XAttribute("id", creatorId));
                    metadata.Add(creator);

                    var metaRefineRole= new XElement(EPubNamespaces.FakeOpf + "meta",EPubRoles.ConvertEnumToAttribute(creatorItem.Role) );
                    metaRefineRole.Add(new XAttribute("refines", "#" + creatorId));
                    metaRefineRole.Add(new XAttribute("property", "role"));
                    metaRefineRole.Add(new XAttribute("scheme", "marc:relators"));
                    metadata.Add(metaRefineRole);

                    if (!string.IsNullOrEmpty(creatorItem.FileAs))
                    {
                        var metaRefineFileAs= new XElement(EPubNamespaces.FakeOpf + "meta",creatorItem.FileAs );
                        metaRefineFileAs.Add(new XAttribute("refines", "#" + creatorId));
                        metaRefineFileAs.Add(new XAttribute("property", "file-as"));
                        metadata.Add(metaRefineFileAs);
                    }

                    var metaRefineDisplay = new XElement(EPubNamespaces.FakeOpf + "meta", creatorCounter);
                    metaRefineDisplay.Add(new XAttribute("refines", "#" + creatorId));
                    metaRefineDisplay.Add(new XAttribute("property", "display-seq"));
                    metadata.Add(metaRefineDisplay);

                }
            }

            int contributorCounter = 0;
            foreach (var contributorItem in Title.Contributors)
            {
                if (!string.IsNullOrEmpty(contributorItem.PersonName))
                {
                    string contributorId = string.Format("contributor{0}", ++contributorCounter);
                    var contributor = new XElement(PURLNamespaces.DCElements + "contributor", contributorItem.PersonName);
                    contributor.Add(new XAttribute("id", contributorId));
                    metadata.Add(contributor);


                    var metaRefineRole = new XElement(EPubNamespaces.FakeOpf + "meta", EPubRoles.ConvertEnumToAttribute(contributorItem.Role));
                    metaRefineRole.Add(new XAttribute("refines", "#" + contributorId));
                    metaRefineRole.Add(new XAttribute("property", "role"));
                    metaRefineRole.Add(new XAttribute("scheme", "marc:relators"));
                    metadata.Add(metaRefineRole);


                    if (!string.IsNullOrEmpty(contributorItem.FileAs))
                    {
                        var metaRefineFileAs = new XElement(EPubNamespaces.FakeOpf + "meta", contributorItem.FileAs);
                        metaRefineFileAs.Add(new XAttribute("refines", "#" + contributorId));
                        metaRefineFileAs.Add(new XAttribute("property", "file-as"));
                        metadata.Add(metaRefineFileAs);
                    }

                    var metaRefineDisplay = new XElement(EPubNamespaces.FakeOpf + "meta", contributorCounter);
                    metaRefineDisplay.Add(new XAttribute("refines", "#" + contributorId));
                    metaRefineDisplay.Add(new XAttribute("property", "display-seq"));
                    metadata.Add(metaRefineDisplay);

                }
            }
            if (CreatorSoftwareString != null)
            {
                string contributorId = string.Format("contributor{0}", ++contributorCounter);
                var maker = new XElement(PURLNamespaces.DCElements + "contributor", CreatorSoftwareString);
                maker.Add(new XAttribute("id", contributorId));
                metadata.Add(maker);

                var metaRefineRole = new XElement(EPubNamespaces.FakeOpf + "meta", EPubRoles.ConvertEnumToAttribute(RolesEnum.BookProducer));
                metaRefineRole.Add(new XAttribute("refines", "#" + contributorId));
                metaRefineRole.Add(new XAttribute("property", "role"));
                metaRefineRole.Add(new XAttribute("scheme", "marc:relators"));
                metadata.Add(metaRefineRole);

                var metaRefineDisplay = new XElement(EPubNamespaces.FakeOpf + "meta", contributorCounter);
                metaRefineDisplay.Add(new XAttribute("refines", "#" + contributorId));
                metaRefineDisplay.Add(new XAttribute("property", "display-seq"));
                metadata.Add(metaRefineDisplay);

            }


            // date
            if (Title.DatePublished.HasValue)
            {
                var xDate = new XElement(PURLNamespaces.DCElements + "date", Title.DatePublished.Value.Year);
                metadata.Add(xDate);
            }


            // source
            if (source != null)
            {
                var sourceElm = new XElement(PURLNamespaces.DCElements + "source", source.GetIdentifier());
                sourceElm.Add(new XAttribute("id","src_id"));
                metadata.Add(sourceElm);

                var metaRefine = new XElement(EPubNamespaces.FakeOpf + "meta", source.GetIdentifierType());
                metaRefine.Add(new XAttribute("refines", "#" + "src_id"));
                metaRefine.Add(new XAttribute("property", "identifier-type"));
                metaRefine.Add(new XAttribute("scheme", Onix5SchemaConverter.GetScheme()));
                metadata.Add(metaRefine);

            }

            // description
            if (!string.IsNullOrEmpty(Title.Description))
            {

                var publisher = new XElement(PURLNamespaces.DCElements + "description", Title.Description);
                if (Title.Languages.Count > 0 && !string.IsNullOrEmpty(Title.Languages[0]))
                {
                    publisher.Add(new XAttribute(XNamespace.Xml + "lang", Title.Languages[0]));
                }
                publisher.Add(new XAttribute("id", "id_desc"));
                metadata.Add(publisher);

                var metaRefineDisplay = new XElement(EPubNamespaces.FakeOpf + "meta",1);
                metaRefineDisplay.Add(new XAttribute("refines", "#id_desc"));
                metaRefineDisplay.Add(new XAttribute("property", "display-seq"));
                metadata.Add(metaRefineDisplay);

            }


            // publisher
            if (!string.IsNullOrEmpty(Title.Publisher.PublisherName))
            {
                var publisher = new XElement(PURLNamespaces.DCElements + "publisher", Title.Publisher.PublisherName);
                if (!string.IsNullOrEmpty(Title.Publisher.Language))
                {
                    publisher.Add(new XAttribute(XNamespace.Xml + "lang", Title.Publisher.Language));
                }
                metadata.Add(publisher);
            }


            // subject
            int subjectCount = 0;
            foreach (var subjectItem in Title.Subjects)
            {
                if (!string.IsNullOrEmpty(subjectItem.SubjectInfo))
                {
                    string subjectID = string.Format("subj_{0}", ++subjectCount);
                    var subject = new XElement(PURLNamespaces.DCElements + "subject", subjectItem.SubjectInfo);
                    subject.Add(new XAttribute("id", subjectID));
                    if (!string.IsNullOrEmpty(subjectItem.Language))
                    {
                        subject.Add(new XAttribute(XNamespace.Xml + "lang", subjectItem.Language));
                    }
                    metadata.Add(subject);

                    var metaRefineDisplay = new XElement(EPubNamespaces.FakeOpf + "meta", subjectCount);
                    metaRefineDisplay.Add(new XAttribute("refines", "#" + subjectID));
                    metaRefineDisplay.Add(new XAttribute("property", "display-seq"));
                    metadata.Add(metaRefineDisplay);
                }
            }

            // meta modified
            string modifiedDate = DateTime.UtcNow.ToString("s")+"Z";
            if (Title.DataFileModification.HasValue)
            {
                modifiedDate = Title.DataFileModification.Value.ToUniversalTime().ToString("s")+"Z";
            }
            var metaModified = new XElement(EPubNamespaces.FakeOpf + "meta", modifiedDate);
            metaModified.Add(new XAttribute("property", "dcterms:modified"));
            metadata.Add(metaModified);

            // series
            if (V3StandardChecker.IsCollectionsAllowedByStandard(_standard))
            {
                _seriesCollections.AddCollectionsToElement(metadata);
            }
            document.Add(metadata);
        }


        private string GetTitleType(Title titleItem)
        {
            switch (titleItem.TitleType)
            {
                case TitleType.Main:
                    return "main";
                case TitleType.SourceInfo:
                    return "collection";
                case TitleType.PublishInfo:
                    return "collection";
            }
            return "collection";
        }


        public void AddXHTMLTextItem(BaseXHTMLFile baseXhtmlFile)
        {
            var bookItem = new ManifestItemV3
            {
                HRef = baseXhtmlFile.HRef, 
                ID = baseXhtmlFile.Id, 
                MediaType = EPubCoreMediaType.ApplicationXhtmlXml
            };
            _manifest.Add(bookItem);

            if (baseXhtmlFile.DocumentType != GuideTypeEnum.Ignore) // we do not add objects that to be ignored 
            {
                var bookSpine = new SpineItemV3 { Name = baseXhtmlFile.Id };
                if (V3StandardChecker.IsRenditionFlowAllowedByStandard(_standard))
                {
                    bookSpine.Flow = SpineItemV3.FlowOptions.Auto;
                       //TODO: make this optional, based on settings to define look and find best properties for defaults
                }
                _spine.Add(bookSpine);
            }
            _navigationManager.AddDocumentToNavigation(baseXhtmlFile);
        }

        public void AddFontFile(FontOnStorage fontFile)
        {
            _manifest.Add(new ManifestItemV3 { HRef = fontFile.PathInEPUB.GetRelativePath(DefaultInternalPaths.ContentFilePath, _flatStructure), ID = fontFile.ID, MediaType = fontFile.MediaType });
        }


        public void AddTOC()
        {

            if (!GenerateCompatibleTOC)
            {
               return;
            }
            var tocItem = new ManifestItemV3
            {
                HRef = TOCFile.TOCFilePath.GetRelativePath(DefaultInternalPaths.ContentFilePath, _flatStructure), 
                ID = "ncx", 
                MediaType = EPubCoreMediaType.ApplicationNCX,
            };
            _spine.TOCId = tocItem.ID;
            _manifest.Add(tocItem);
        }

        public void AddNavigationDocument(NavigationDocumentFile navigationDocument)
        {
            var navItem = new ManifestItemV3
            {
                HRef = navigationDocument.PathInEPUB.GetRelativePath(DefaultInternalPaths.ContentFilePath, _flatStructure),
                ID = "nav",
                MediaType = EPubCoreMediaType.ApplicationXhtmlXml,
                Nav = true,
            };
            _manifest.Add(navItem);
        }

        public void AddImage(ImageOnStorage image)
        {
            var item = new ManifestItemV3
            {
                HRef = image.PathInEPUB.GetRelativePath(DefaultInternalPaths.ContentFilePath, _flatStructure),
                ID = image.ID,
                MediaType = EPUBImage.ConvertImageTypeToMediaType(image.ImageType)
            };
            if (CoverId == image.ID)
            {
                item.CoverImage = true;
            }
            _manifest.Add(item);
        }

        public void AddCSS(CSSFile cssFile)
        {
            var maincss = new ManifestItemV3 { HRef = cssFile.PathInEPUB.GetRelativePath(DefaultInternalPaths.ContentFilePath, _flatStructure), ID = cssFile.ID, MediaType = CSSFile.MediaType };
            _manifest.Add(maincss);
        }

        /// <summary>
        /// Writes content to stream
        /// </summary>
        /// <param name="s"></param>
        public void Write(Stream s)
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
            var settings = new XmlWriterSettings { CloseOutput = false, Encoding = Encoding.UTF8, Indent = true };
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
            AddBindingsToContentDocument(document.Root);
            AddCollectionsToContentDocument(document.Root);
        }

// ReSharper disable once UnusedParameter.Local
        private void AddCollectionsToContentDocument(XElement document)
        {
            // not implemented for now, we not support ePub v3 collections yet
        }

        private void AddBindingsToContentDocument(XElement document)
        {
            XElement bindingsElement = _bindings.GenerateBindingsElement();
            if (bindingsElement != null)
            {
                document.Add(bindingsElement);
            }
        }

        private void AddManifestToContentDocument(XElement document)
        {
            XElement manifestElement = _manifest.GenerateManifestElement();
            document.Add(manifestElement);
        }


        private void AddGuideToContentDocument(XElement document)
        {
            _navigationManager.WriteNavigationItemsToContentDocumentElement(document);
        }

        private void AddSpineToContentDocument(XElement xElement)
        {
            XElement spineElement = _spine.GenerateSpineElement();
            xElement.Add(spineElement);
        }


        /// <summary>
        /// Returns path in ePub 
        /// </summary>
        public EPubInternalPath PathInEPUB
        {
            get { return DefaultInternalPaths.ContentFilePath; }
        }

        /// <summary>
        /// Get/Set book title
        /// </summary>
        public EPubTitleSettings Title { get; set; }

        /// <summary>
        /// get/set Id of the cover image file
        /// </summary>
        public string CoverId { get; set; }

        /// <summary>
        /// Get/Set string by software creator to identify creator software
        /// </summary>
        public string CreatorSoftwareString { get; set; }

    }
}
