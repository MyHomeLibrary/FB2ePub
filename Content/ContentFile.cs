using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Xml;
using System.Xml.Linq;
using EPubLibrary.Content.Guide;
using EPubLibrary.Content.Manifest;
using EPubLibrary.Content.Spine;
using EPubLibrary.CSS_Items;
using EPubLibrary.Template;

namespace EPubLibrary.Content
{
    internal class ContentFile
    {
        XNamespace opfNameSpace = @"http://www.idpf.org/2007/opf";

        private readonly GuideSection guide = new GuideSection();

        private readonly ManifestSection manifest = new ManifestSection();

        private readonly SpineSection spine = new SpineSection();


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

        private void AddPackageData(XDocument document)
        {
            XElement packagedata = new XElement(opfNameSpace + "package");
            packagedata.Add(new XAttribute("version", "2.0"));
            // we use ID of the first identifier
            packagedata.Add(new XAttribute("unique-identifier", Title.Identifiers[0].ID));
            //packagedata.Add(new XAttribute(XNamespace.Xmlns + "opf", opfNameSpace));
            document.Add(packagedata);
        }

        private void AddManifestToContentDocument(XElement document)
        {
            XElement manifestElement = manifest.GenerateManifestElement();
            document.Add(manifestElement);
        }


        private void AddGuideToContentDocument(XElement xElement)
        {
            if (guide.HasData())
            {
                xElement.Add(guide.GenerateGuide());
            }
        }

        private void AddSpineToContentDocument(XElement xElement)
        {
            XElement spineElement = spine.GenerateSpineElement();
            xElement.Add(spineElement);
        }

        private void AddMetaDataToContentDocument(XElement document)
        {
            XElement metadata = new XElement(opfNameSpace + "metadata", new XAttribute("xmlns", "http://www.idpf.org/2007/opf"));
            XNamespace dc = @"http://purl.org/dc/elements/1.1/";
            XNamespace xsi = @"http://www.w3.org/2001/XMLSchema-instance";
            XNamespace dcterms = @"http://purl.org/dc/terms/";
            metadata.Add(new XAttribute(XNamespace.Xmlns + "dc", dc));
            metadata.Add(new XAttribute(XNamespace.Xmlns + "xsi", xsi));
            metadata.Add(new XAttribute(XNamespace.Xmlns + "dcterms", dcterms));
            //metadata.Add(new XAttribute("opf", opfNameSpace));

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
                string id = id = string.Format("{0}", identifierItem.IdentifierName);
                if (identifierItem.Scheme.ToUpper() == "URI")
                {
                    id = string.Format("urn:uuid:{0}", identifierItem.IdentifierName);

                }
                XElement identifier = new XElement(dc + "identifier", id);
                identifier.Add(new XAttribute("id", identifierItem.ID));
                identifier.Add(new XAttribute(opfNameSpace + "scheme", identifierItem.Scheme));
                metadata.Add(identifier);
            }

            if ( Title.DatePublished.HasValue)
            {
                XElement xDate = new XElement(dc + "date",Title.DatePublished.Value.Year);
                xDate.Add(new XAttribute(opfNameSpace + "event", "original-publication"));
                metadata.Add(xDate);
            }

            foreach (var creatorItem in Title.Creators)
            {
                if (!string.IsNullOrEmpty(creatorItem.PersonName))
                {
                    var creator = new XElement(dc + "creator", creatorItem.PersonName);
                    creator.Add(new XAttribute(opfNameSpace + "role", EPubRoles.ConvertEnumToAttribute(creatorItem.Role)));
                    if (!string.IsNullOrEmpty(creatorItem.FileAs))
                    {
                        creator.Add(new XAttribute(opfNameSpace + "file-as",creatorItem.FileAs));
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
                    contributor.Add(new XAttribute(opfNameSpace + "role", EPubRoles.ConvertEnumToAttribute(contributorItem.Role)));
                    if (!string.IsNullOrEmpty(contributorItem.FileAs))
                    {
                        contributor.Add(new XAttribute(opfNameSpace + "file-as", contributorItem.FileAs));
                    }
                    if (!string.IsNullOrEmpty(contributorItem.Language))
                    {
                        // need to add writing language in "xml:lang — use RFC-3066 format"
                        // will add when will find an example since unclear
                    }
                    metadata.Add(contributor);
                }
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
                var cover = new XElement(opfNameSpace + "meta");
                cover.Add(new XAttribute("name","cover"));
                cover.Add(new XAttribute("content", CoverId));
                metadata.Add(cover);
            }

            document.Add(metadata);
        }



        public EPubTitleSettings Title { get; set; }

        /// <summary>
        /// get/set Id of the cover image file
        /// </summary>
        public string CoverId{ get; set; }

        public void Write (Stream s)
        {
            XDocument contentDocument = new XDocument();
            CreateContentDocument(contentDocument);
            XmlWriterSettings settings = new XmlWriterSettings();
            settings.CloseOutput = false;
            settings.Encoding = Encoding.UTF8;
            settings.Indent = true;
            using (var writer = XmlWriter.Create(s, settings))
            {
                contentDocument.WriteTo(writer);
            }
            
        }

        public void AddXHTMLTextItem(string link, string id, GuideTypeEnum type)
        {
            ManifestItem bookItem = new ManifestItem { HRef = link.Replace('\\', '/'), ID = id, MediaType = @"application/xhtml+xml" };
            manifest.Add(bookItem);

            SpineItem bookSpine = new SpineItem{ Name = id };
            spine.Add(bookSpine);

            guide.AddGuideItem(link, id, type);                
        }

        public void AddTOC(string link, string id)
        {
            ManifestItem TOCItem = new ManifestItem { HRef = link.Replace('\\', '/'), ID = id, MediaType = @"application/x-dtbncx+xml" };
            manifest.Add(TOCItem);                     
        }

        public void AddImage(string link,string id,EPUBImageTypeEnum imageType)
        {
            manifest.Add(new ManifestItem { HRef = link.Replace('\\', '/'), ID = id, MediaType = EPUBImage.ConvertImageTypeToMediaType(imageType) });
        }

        public void AddCSS(string link, string id)
        {
            ManifestItem maincss = new ManifestItem{ HRef = link.Replace('\\','/'), ID = id, MediaType = CSSFile.MediaType };
            manifest.Add(maincss);
        }

        public void AddXPGTTemplate(string link, string id)
        {
            ManifestItem maincss = new ManifestItem { HRef = link.Replace('\\', '/'), ID = id, MediaType = AdobeTemplate.MediaType };
            manifest.Add(maincss);
        }


        public void AddFontFile(string link, string id,string mediaType)
        {
            manifest.Add(new ManifestItem() { HRef = link.Replace('\\', '/'), ID = id, MediaType = mediaType/*"application/x-font-ttf"*/ });
        }


    }
}
