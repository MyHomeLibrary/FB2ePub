using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Xml;
using System.Xml.Linq;
using EPubLibrary.Content.CalibreMetadata;
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
    public class ContentFile : IEPubPath
    {
        public static readonly EPubInternalPath ContentFilePath = new EPubInternalPath(EPubInternalPath.DefaultOebpsFolder + "/content.opf");

        protected readonly XNamespace _opfNameSpace = @"http://www.idpf.org/2007/opf";
        protected readonly XNamespace _fakeOpf = @"http://www.idpf.org/2007/xxx";


        protected readonly GuideSection _guide = new GuideSection();

        protected ManifestSection _manifest = new ManifestSection();

        protected SpineSection _spine = new SpineSection();

        protected bool _flatStructure = false;


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
            XElement packagedata = new XElement(_opfNameSpace + "package");
            packagedata.Add(new XAttribute("version", GetEPubVersion()));
            // we use ID of the first identifier
            packagedata.Add(new XAttribute("unique-identifier", Title.Identifiers[0].IdentifierName));
            packagedata.Add(new XAttribute("xmlns", _opfNameSpace.NamespaceName));
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
            XElement metadata = new XElement(_opfNameSpace + "metadata");
            XNamespace dc = @"http://purl.org/dc/elements/1.1/";
            XNamespace xsi = @"http://www.w3.org/2001/XMLSchema-instance";
            XNamespace dcterms = @"http://purl.org/dc/terms/";
            metadata.Add(new XAttribute(XNamespace.Xmlns + "dc", dc.NamespaceName));
            metadata.Add(new XAttribute(XNamespace.Xmlns + "xsi", xsi.NamespaceName));
            metadata.Add(new XAttribute(XNamespace.Xmlns + "dcterms", dcterms.NamespaceName));
            metadata.Add(new XAttribute(XNamespace.Xmlns + "opf", _fakeOpf.NamespaceName));
            if (CalibreData!= null)
            {
                CalibreData.InjectNamespace(metadata);
            }

            foreach (var titleItem in Title.BookTitles)
            {
                var titleElement = new XElement(dc + "title", titleItem.TitleName);
                if (!string.IsNullOrEmpty(titleItem.Language))
                {
                    // need to add writing language in "xml:lang — use RFC-3066 format"
                    // will add when will find an example since unclear               
                }
                metadata.Add(titleElement);
            }

            foreach (var languageItem in Title.Languages)
            {
                XElement language = new XElement(dc + "language", languageItem);
                language.Add(new XAttribute(xsi + "type", "dcterms:RFC3066"));
                metadata.Add(language);
            }

            foreach (var identifierItem in Title.Identifiers)
            {
                XElement identifier = new XElement(dc + "identifier", identifierItem.ID);
                identifier.Add(new XAttribute("id", identifierItem.IdentifierName));
                identifier.Add(new XAttribute(_fakeOpf + "scheme", identifierItem.Scheme));
                metadata.Add(identifier);
            }

            if ( Title.DatePublished.HasValue)
            {
                XElement xDate = new XElement(dc + "date",Title.DatePublished.Value.Year);
                xDate.Add(new XAttribute(_fakeOpf + "event", "original-publication"));
                metadata.Add(xDate);
            }

            foreach (var creatorItem in Title.Creators)
            {
                if (!string.IsNullOrEmpty(creatorItem.PersonName))
                {
                    var creator = new XElement(dc + "creator", creatorItem.PersonName);
                    creator.Add(new XAttribute(_fakeOpf + "role", EPubRoles.ConvertEnumToAttribute(creatorItem.Role)));
                    if (!string.IsNullOrEmpty(creatorItem.FileAs))
                    {
                        creator.Add(new XAttribute(_fakeOpf + "file-as",creatorItem.FileAs));
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
                    var contributor = new XElement(dc + "contributor", contributorItem.PersonName);
                    contributor.Add(new XAttribute(_fakeOpf + "role", EPubRoles.ConvertEnumToAttribute(contributorItem.Role)));
                    if (!string.IsNullOrEmpty(contributorItem.FileAs))
                    {
                        contributor.Add(new XAttribute(_fakeOpf + "file-as", contributorItem.FileAs));
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
                var maker = new XElement(dc + "contributor", CreatorSoftwareString);
                maker.Add(new XAttribute(_fakeOpf + "role",
                    EPubRoles.ConvertEnumToAttribute(RolesEnum.BookProducer)));
                metadata.Add(maker);
            }

            if (!string.IsNullOrEmpty(Title.Publisher.PublisherName))
            {
                var publisher = new XElement(dc + "publisher", Title.Publisher.PublisherName);
                if (!string.IsNullOrEmpty(Title.Publisher.Language))
                {
                    // need to add writing language in "xml:lang — use RFC-3066 format"
                    // will add when will find an example since unclear
                }
                metadata.Add(publisher);
            }

            if (!string.IsNullOrEmpty(Title.Description))
            {

                var publisher = new XElement(dc + "description", Title.Description);
                metadata.Add(publisher);                
            }


            foreach (var subjectItem in Title.Subjects)
            {
                if (!string.IsNullOrEmpty(subjectItem.SubjectInfo))
                {
                    var contributor = new XElement(dc + "subject", subjectItem.SubjectInfo);
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
                var cover = new XElement(_opfNameSpace + "meta");
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
        /// Get/set collections
        /// </summary>
        public EPubCollections Collections { get; set; }

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
            XDocument contentDocument = new XDocument();
            CreateContentDocument(contentDocument);
            string str = FixDocument(contentDocument);
            UTF8Encoding encoding = new UTF8Encoding();
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
            XmlWriterSettings settings = new XmlWriterSettings();
            settings.CloseOutput = false;
            settings.Encoding = Encoding.UTF8;
            settings.Indent = true;
            MemoryStream ms = new MemoryStream();
            using (var writer = XmlWriter.Create(ms, settings))
            {
                contentDocument.WriteTo(writer);
            }
            ms.Seek(0, SeekOrigin.Begin);
            byte[] buffer = new byte[ms.Length];
            ms.Read(buffer, 0, buffer.Length);
            UTF8Encoding encoding = new UTF8Encoding();
            string str = encoding.GetString(buffer);
            str = str.Replace(_fakeOpf.NamespaceName, _opfNameSpace.NamespaceName);
            return str;
        }

        public virtual void AddXHTMLTextItem(BaseXHTMLFile baseXhtmlFile)
        {
            ManifestItem bookItem = new ManifestItem { HRef = baseXhtmlFile.PathInEPUB.GetRelativePath(ContentFilePath, _flatStructure), ID = baseXhtmlFile.Id, MediaType = @"application/xhtml+xml" };
            _manifest.Add(bookItem);

            if (baseXhtmlFile.DocumentType != GuideTypeEnum.Ignore) // we do not add objects that to be ignored 
            {
                SpineItem bookSpine = new SpineItem {Name = baseXhtmlFile.Id};
                _spine.Add(bookSpine);
            }

            _guide.AddGuideItem(bookItem.HRef, baseXhtmlFile.Id, baseXhtmlFile.DocumentType);                
        }

        public virtual void AddTOC()
        {
            ManifestItem TOCItem = new ManifestItem { HRef = TOCFile.TOCFilePath.GetRelativePath(ContentFilePath, _flatStructure), ID = "ncx", MediaType = @"application/x-dtbncx+xml" };
            _manifest.Add(TOCItem);                     
        }

        public virtual void AddImage(ImageOnStorage image)
        {
            _manifest.Add(new ManifestItem { HRef = image.PathInEPUB.GetRelativePath(ContentFilePath, _flatStructure), ID = image.ID, MediaType = EPUBImage.ConvertImageTypeToMediaType(image.ImageType) });
        }

        public virtual void AddCSS(CSSFile cssFile)
        {
            ManifestItem maincss = new ManifestItem { HRef = cssFile.PathInEPUB.GetRelativePath(ContentFilePath,_flatStructure), ID = cssFile.ID, MediaType = CSSFile.MediaType };
            _manifest.Add(maincss);
        }

        public virtual void AddXPGTTemplate(AdobeTemplate template)
        {
            ManifestItem maincss = new ManifestItem { HRef = template.PathInEPUB.GetRelativePath(ContentFilePath, _flatStructure), ID = template.ID, MediaType = template.GetMediaType() };
            _manifest.Add(maincss);
        }


        public virtual void AddFontFile(FontOnStorage fontFile)
        {
            _manifest.Add(new ManifestItem() { HRef = fontFile.PathInEPUB.GetRelativePath(ContentFilePath, _flatStructure), ID = fontFile.ID, MediaType =fontFile.MediaType });
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
