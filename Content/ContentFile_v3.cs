using System.Globalization;
using System.Linq.Expressions;
using System.Xml.Linq;
using EPubLibrary.Container;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using EPubLibrary.ReferenceUtils;


namespace EPubLibrary.Content
{
    public class ContentFileV3 : ContentFile
    {

        /// <summary>
        /// Returns epub version to write into a package
        /// </summary>
        /// <returns></returns>
        protected override string GetEPubVersion()
        {
            return "3.0";
        }

        protected override void AddPackageData(XDocument document)
        {
            XElement packagedata = new XElement(_opfNameSpace + "package");
            packagedata.Add(new XAttribute("version", GetEPubVersion()));
            // we use ID of the first identifier
            packagedata.Add(new XAttribute("unique-identifier", Title.Identifiers[0].IdentifierName));
            packagedata.Add(new XAttribute("xmlns", _opfNameSpace.NamespaceName));
            packagedata.Add(new XAttribute(XNamespace.Xml + "lang",Title.Languages[0]));
            document.Add(packagedata);
        }

        protected override void AddMetaDataToContentDocument(XElement document)
        {
            XElement metadata = new XElement(_opfNameSpace + "metadata");
            XNamespace dc = @"http://purl.org/dc/elements/1.1/";
            XNamespace xsi = @"http://www.w3.org/2001/XMLSchema-instance";
            XNamespace dcterms = @"http://purl.org/dc/terms/";
            metadata.Add(new XAttribute(XNamespace.Xmlns + "dc", dc.NamespaceName));
            metadata.Add(new XAttribute(XNamespace.Xmlns + "xsi", xsi.NamespaceName));
            metadata.Add(new XAttribute(XNamespace.Xmlns + "dcterms", dcterms.NamespaceName));
            metadata.Add(new XAttribute(XNamespace.Xmlns + "opf", _fakeOpf.NamespaceName));
            if (CalibreData != null)
            {
                CalibreData.InjectNamespace(metadata);
            }

            Onix5SchemaConverter source = null;
            foreach (var identifierItem in Title.Identifiers)
            {
                Onix5SchemaConverter schemaConverter = new Onix5SchemaConverter(identifierItem);
                XElement identifier = new XElement(dc + "identifier", schemaConverter.GetIdentifier());
                identifier.Add(new XAttribute("id", identifierItem.IdentifierName));
                metadata.Add(identifier);

                XElement metaRefine = new XElement(_fakeOpf + "meta", schemaConverter.GetIdentifierType());
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
                var titleElement = new XElement(dc + "title", titleItem.TitleName);
                titleElement.Add(new XAttribute("id",idString));
                metadata.Add(titleElement);

                XElement metaRefineType = new XElement(_fakeOpf + "meta",GetTitleType(titleItem));
                metaRefineType.Add(new XAttribute("refines", "#" + idString));
                metaRefineType.Add(new XAttribute("property", "title-type"));
                metadata.Add(metaRefineType);

                XElement metaRefineDisplay= new XElement(_fakeOpf + "meta", titleIdCounter);
                metaRefineDisplay.Add(new XAttribute("refines", "#" + idString));
                metaRefineDisplay.Add(new XAttribute("property", "display-seq"));
                metadata.Add(metaRefineDisplay);

            }

            foreach (var languageItem in Title.Languages)
            {
                XElement language = new XElement(dc + "language", languageItem);
                metadata.Add(language);
            }



            foreach (var creatorItem in Title.Creators)
            {
                int creatorCounter = 0;
                if (!string.IsNullOrEmpty(creatorItem.PersonName))
                {
                    string creatorId = string.Format("creator{0}", ++creatorCounter);
                    var creator = new XElement(dc + "creator", creatorItem.PersonName);
                    creator.Add(new XAttribute("id", creatorId));
                    metadata.Add(creator);

                    XElement metaRefineRole= new XElement(_fakeOpf + "meta",EPubRoles.ConvertEnumToAttribute(creatorItem.Role) );
                    metaRefineRole.Add(new XAttribute("refines", "#" + creatorId));
                    metaRefineRole.Add(new XAttribute("property", "role"));
                    metaRefineRole.Add(new XAttribute("scheme", "marc:relators"));
                    metadata.Add(metaRefineRole);

                    if (!string.IsNullOrEmpty(creatorItem.FileAs))
                    {
                        XElement metaRefineFileAs= new XElement(_fakeOpf + "meta",creatorItem.FileAs );
                        metaRefineFileAs.Add(new XAttribute("refines", "#" + creatorId));
                        metaRefineFileAs.Add(new XAttribute("property", "file-as"));
                        metadata.Add(metaRefineFileAs);
                    }

                    XElement metaRefineDisplay = new XElement(_fakeOpf + "meta", creatorCounter);
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
                    var contributor = new XElement(dc + "contributor", contributorItem.PersonName);
                    contributor.Add(new XAttribute("id", contributorId));
                    metadata.Add(contributor);


                    XElement metaRefineRole = new XElement(_fakeOpf + "meta", EPubRoles.ConvertEnumToAttribute(contributorItem.Role));
                    metaRefineRole.Add(new XAttribute("refines", "#" + contributorId));
                    metaRefineRole.Add(new XAttribute("property", "role"));
                    metaRefineRole.Add(new XAttribute("scheme", "marc:relators"));
                    metadata.Add(metaRefineRole);


                    if (!string.IsNullOrEmpty(contributorItem.FileAs))
                    {
                        XElement metaRefineFileAs = new XElement(_fakeOpf + "meta", contributorItem.FileAs);
                        metaRefineFileAs.Add(new XAttribute("refines", "#" + contributorId));
                        metaRefineFileAs.Add(new XAttribute("property", "file-as"));
                        metadata.Add(metaRefineFileAs);
                    }

                    XElement metaRefineDisplay = new XElement(_fakeOpf + "meta", contributorCounter);
                    metaRefineDisplay.Add(new XAttribute("refines", "#" + contributorId));
                    metaRefineDisplay.Add(new XAttribute("property", "display-seq"));
                    metadata.Add(metaRefineDisplay);

                }
            }
            if (CreatorSoftwareString != null)
            {
                string contributorId = string.Format("contributor{0}", ++contributorCounter);
                var maker = new XElement(dc + "contributor", CreatorSoftwareString);
                maker.Add(new XAttribute("id", contributorId));
                metadata.Add(maker);

                XElement metaRefineRole = new XElement(_fakeOpf + "meta", EPubRoles.ConvertEnumToAttribute(RolesEnum.BookProducer));
                metaRefineRole.Add(new XAttribute("refines", "#" + contributorId));
                metaRefineRole.Add(new XAttribute("property", "role"));
                metaRefineRole.Add(new XAttribute("scheme", "marc:relators"));
                metadata.Add(metaRefineRole);

                XElement metaRefineDisplay = new XElement(_fakeOpf + "meta", contributorCounter);
                metaRefineDisplay.Add(new XAttribute("refines", "#" + contributorId));
                metaRefineDisplay.Add(new XAttribute("property", "display-seq"));
                metadata.Add(metaRefineDisplay);

            }


            // date
            if (Title.DatePublished.HasValue)
            {
                XElement xDate = new XElement(dc + "date", Title.DatePublished.Value.Year);
                metadata.Add(xDate);
            }


            // source
            if (source != null)
            {
                var sourceElm = new XElement(dc + "source", source.GetIdentifier());
                sourceElm.Add(new XAttribute("id","src_id"));
                metadata.Add(sourceElm);

                XElement metaRefine = new XElement(_fakeOpf + "meta", source.GetIdentifierType());
                metaRefine.Add(new XAttribute("refines", "#" + "src_id"));
                metaRefine.Add(new XAttribute("property", "identifier-type"));
                metaRefine.Add(new XAttribute("scheme", Onix5SchemaConverter.GetScheme()));
                metadata.Add(metaRefine);

            }

            // description
            if (!string.IsNullOrEmpty(Title.Description))
            {

                var publisher = new XElement(dc + "description", Title.Description);
                if (Title.Languages.Count > 0 && !string.IsNullOrEmpty(Title.Languages[0]))
                {
                    publisher.Add(new XAttribute(XNamespace.Xml + "lang", Title.Languages[0]));
                }
                publisher.Add(new XAttribute("id", "id_desc"));
                metadata.Add(publisher);

                XElement metaRefineDisplay = new XElement(_fakeOpf + "meta",1);
                metaRefineDisplay.Add(new XAttribute("refines", "#id_desc"));
                metaRefineDisplay.Add(new XAttribute("property", "display-seq"));
                metadata.Add(metaRefineDisplay);

            }


            // publisher
            if (!string.IsNullOrEmpty(Title.Publisher.PublisherName))
            {
                var publisher = new XElement(dc + "publisher", Title.Publisher.PublisherName);
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
                    var subject = new XElement(dc + "subject", subjectItem.SubjectInfo);
                    subject.Add(new XAttribute("id", subjectID));
                    if (!string.IsNullOrEmpty(subjectItem.Language))
                    {
                        subject.Add(new XAttribute(XNamespace.Xml + "lang", subjectItem.Language));
                    }
                    metadata.Add(subject);

                    XElement metaRefineDisplay = new XElement(_fakeOpf + "meta", subjectCount);
                    metaRefineDisplay.Add(new XAttribute("refines", "#" + subjectID));
                    metaRefineDisplay.Add(new XAttribute("property", "display-seq"));
                    metadata.Add(metaRefineDisplay);
                }
            }

            // meta modifyed
            string modifiedDate = DateTime.UtcNow.ToUniversalTime().ToString("s")+"Z";
            if (Title.DataFileModification.HasValue)
            {
                modifiedDate = Title.DataFileModification.Value.ToUniversalTime().ToString("s")+"Z";
            }
            XElement metaModified = new XElement(_fakeOpf + "meta", modifiedDate);
            metaModified.Add(new XAttribute("property", "dcterms:modified"));
            metadata.Add(metaModified);




            // TODO: This need to be updated in future when we know how it's implemented

            // This needed for iTunes and other apple made devices/programs to display the cover
            //if (!string.IsNullOrEmpty(CoverId))
            //{
            //    // <meta name="cover" content="cover.jpg"/>
            //    var cover = new XElement(_fakeOpf + "meta",CoverId);
            //    cover.Add(new XAttribute("property", "cover"));
            //    metadata.Add(cover);
            //}

            //if (CalibreData != null)
            //{
            //    CalibreData.InjectData(metadata);
            //}

            document.Add(metadata);
        }


        private string GetTitleType(Title titleItem)
        {
            switch (titleItem.TitleType)
            {
                case TitleType.Main:
                    return "main";
                case TitleType.SourceInfo:
                    return "edition";
                case TitleType.PublishInfo:
                    return "collection";
            }
            return "expanded";
        }



    }
}
