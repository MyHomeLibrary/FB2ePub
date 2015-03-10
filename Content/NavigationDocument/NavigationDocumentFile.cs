using System;
using System.Collections.Generic;
using System.IO;
using System.Net;
using System.Text;
using System.Xml;
using System.Xml.Linq;
using EPubLibrary.CSS_Items;
using EPubLibrary.PathUtils;
using EPubLibrary.XHTML_Items;
using EPubLibraryContracts;
using XHTMLClassLibrary.BaseElements;
using XHTMLClassLibrary.BaseElements.Structure_Header;

namespace EPubLibrary.Content.NavigationDocument
{
    public class NavigationDocumentFile : IEPubPath
    {
        private const string HeadingTOC = "Table of Contents";

        private readonly List<IStyleElement> _styles = new List<IStyleElement>();
        private readonly NavMapElementV3 _documentNavigationMap = new NavMapElementV3
        {
            Type = NavigationTableType.TOC,
            Heading = HeadingTOC,
        };

        private readonly NavMapElementV3 _landmarks = new NavMapElementV3
        {
            Type = NavigationTableType.Landmarks
        };

        private static readonly EPubInternalPath NAVFilePath = new EPubInternalPath(EPubInternalPath.DefaultOebpsFolder + "/nav.xhtml");

        public IEPubInternalPath PathInEPUB
        {
            get { return NAVFilePath; }
        }

        public bool FlatStructure { get; set; }

        /// <summary>
        /// Get access to list of CSS files
        /// </summary>
        public List<IStyleElement> StyleFiles { get { return _styles; } }

        /// <summary>
        /// Document title (meaningless in EPUB , usually used by browsers)
        /// </summary>
        public string PageTitle { get; set; }

        /// <summary>
        /// Writes content to stream
        /// </summary>
        /// <param name="s"></param>
        public void Write(Stream s)
        {
            var contentDocument = new XDocument();
            CreateNAVDocument(contentDocument);
            var settings = new XmlWriterSettings {CloseOutput = false, Encoding = Encoding.UTF8, Indent = true};
            using (var writer = XmlWriter.Create(s, settings))
            {
                contentDocument.WriteTo(writer);
            }


        }

        private void CreateNAVDocument(XDocument contentDocument)
        {
            var html = new XElement(WWWNamespaces.XHTML + "html");
            html.Add(new XAttribute(XNamespace.Xmlns + "epub", EPubNamespaces.OpsNamespace));
            contentDocument.Add(html);

            var head = new XElement(WWWNamespaces.XHTML + "head");
            html.Add(head);
            var meta = new XElement(WWWNamespaces.XHTML + "meta");
            meta.Add(new XAttribute("charset","utf-8"));
            head.Add(meta);
            var title = new XElement(WWWNamespaces.XHTML + "title");
            if (string.IsNullOrEmpty(PageTitle))
            {
                title.Value = "Table of Contents";
            }
            else
            {
                title.Value = WebUtility.HtmlEncode(PageTitle) + " - Table of Contents";
            }
            head.Add(title);
            foreach (var file in _styles)
            {
                var cssStyleSheet = new Link(HTMLElementType.HTML5);
                cssStyleSheet.Relation.Value = "stylesheet";
                cssStyleSheet.Type.Value = file.GetMediaType().GetAsSerializableString();
                cssStyleSheet.HRef.Value = file.PathInEPUB.GetRelativePath(NAVFilePath, FlatStructure);
                head.Add(cssStyleSheet.Generate());
            }

            var body = new XElement(WWWNamespaces.XHTML + "body");
            body.Add(new XAttribute("class","nav_body"));
            html.Add(body);

            var navElement = _documentNavigationMap.GenerateXMLMap();
            if (navElement != null)
            {
                body.Add(navElement);
            }

            var landmarksElement = _landmarks.GenerateXMLMap();
            if (landmarksElement != null)
            {
                body.Add(landmarksElement);
            }
        }

        public void AddTOCNavPoint(BaseXHTMLFileV3 content, string name)
        {
            if (string.IsNullOrEmpty(name))
            {
                return;
            }
            var bookPoint = new NavPointV3 { Content = content.PathInEPUB.GetRelativePath(NAVFilePath, content.FlatStructure), 
                Name = name,
            Id =  content.Id};
            _documentNavigationMap.Add(bookPoint);
        }

        public void AddSubNavPoint(BaseXHTMLFileV3 subcontent, string name)
        {
            if (subcontent.NavigationParent == null)
            {
                throw new ArgumentException(@"Can't add subpoint without a parent","subcontent");
            }
            var newPoint = new NavPointV3
            {
                Content = subcontent.PathInEPUB.GetRelativePath(NAVFilePath, subcontent.FlatStructure),
                Name = name,
                Id = subcontent.Id
            };
            var parentPoint = _documentNavigationMap.Find(x => (x.Id == subcontent.NavigationParent.Id));
            if (parentPoint != null)
            {
                parentPoint.SubPoints.Add(newPoint);
            }
            else
            {
                // iterate all top level content
                foreach (var element in _documentNavigationMap)
                {
                    // here "AllContent) will return all the sub elements (any depth) of given top level element
                    // so we look for parent in all this elements
                    parentPoint = element.AllContent().Find(x => (x.Id == subcontent.NavigationParent.Id));
                    if (parentPoint != null)
                    {
                        parentPoint.SubPoints.Add(newPoint);
                        return;
                    }
                }
                throw new Exception("no such point to add sub point");
            }

        }

    }
}
