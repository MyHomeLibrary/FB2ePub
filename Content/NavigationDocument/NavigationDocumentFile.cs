using System;
using System.Collections.Generic;
using System.IO;
using System.Net;
using System.Text;
using System.Xml;
using System.Xml.Linq;
using EPubLibrary.CSS_Items;
using EPubLibrary.PathUtils;
using EPubLibrary.TOC.NavMap;
using EPubLibrary.XHTML_Items;
using XHTMLClassLibrary.BaseElements;
using XHTMLClassLibrary.BaseElements.Structure_Header;

namespace EPubLibrary.Content.NavigationDocument
{
    public class NavigationDocumentFile : IEPubPath
    {
        private readonly List<StyleElement> _styles = new List<StyleElement>();
        protected readonly NavMapElementV3 _tocNav = new NavMapElementV3
        {
            Type = NavigationTableType.TOC,
            NavHeading = "Table of Contents",
        };

        public static readonly EPubInternalPath NAVFilePath = new EPubInternalPath(EPubInternalPath.DefaultOebpsFolder + "/text/nav.xhtml");

        public EPubInternalPath PathInEPUB
        {
            get { return NAVFilePath; }
        }

        public bool FlatStructure { get; set; }

        /// <summary>
        /// Get access to list of CSS files
        /// </summary>
        public List<StyleElement> StyleFiles { get { return _styles; } }

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
            XDocument contentDocument = new XDocument();
            CreateNAVDocument(contentDocument);
            XmlWriterSettings settings = new XmlWriterSettings();
            settings.CloseOutput = false;
            settings.Encoding = Encoding.UTF8;
            settings.Indent = true;
            using (var writer = XmlWriter.Create(s, settings))
            {
                contentDocument.WriteTo(writer);
            }


        }

        private void CreateNAVDocument(XDocument contentDocument)
        {
            XNamespace xhtmlNamespace = @"http://www.w3.org/1999/xhtml";
            XNamespace ePubNamespace = @"http://www.idpf.org/2007/ops";

            var html = new XElement(xhtmlNamespace + "html");
            html.Add(new XAttribute(XNamespace.Xmlns + "epub", ePubNamespace));
            contentDocument.Add(html);

            var head = new XElement(xhtmlNamespace + "head");
            html.Add(head);
            var meta = new XElement(xhtmlNamespace + "meta");
            meta.Add(new XAttribute("charset","utf-8"));
            head.Add(meta);
            var title = new XElement(xhtmlNamespace + "title");
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
                var cssStyleShit = new Link(HTMLElementType.HTML5);
                cssStyleShit.Relation.Value = "stylesheet";
                cssStyleShit.Type.Value = file.GetMediaType();
                cssStyleShit.HRef.Value = file.PathInEPUB.GetRelativePath(NAVFilePath, FlatStructure);
                head.Add(cssStyleShit.Generate());
            }

            XElement body = new XElement(xhtmlNamespace + "body");
            html.Add(body);

            XElement navElement = _tocNav.GenerateXMLMap();
            body.Add(navElement);          
        }

        public void AddNavPoint(BookDocument content, string name)
        {
            if (string.IsNullOrEmpty(name))
            {
                return;
            }
            NavPointV3 bookPoint = new NavPointV3 { Content = content.PathInEPUB.GetRelativePath(NAVFilePath, content.FlatStructure), 
                Name = name,
            Id =  content.Id};
            _tocNav.Add(bookPoint);
        }

        public void AddSubNavPoint(BookDocument content, BookDocument subcontent, string name)
        {
            if (string.IsNullOrEmpty(name))
            {
                return;
            }
            var point = _tocNav.Find(x => (x.Content == content.PathInEPUB.GetRelativePath(NAVFilePath, content.FlatStructure)));
            if (point != null)
            {
                point.SubPoints.Add(new NavPointV3 { Content = subcontent.PathInEPUB.GetRelativePath(NAVFilePath, subcontent.FlatStructure), Name = name });
            }
            else
            {
                foreach (var element in _tocNav)
                {
                    point = element.AllContent().Find(x => (x.Content == content.PathInEPUB.GetRelativePath(NAVFilePath, content.FlatStructure)));
                    if (point != null)
                    {
                        point.SubPoints.Add(new NavPointV3 { Content = subcontent.PathInEPUB.GetRelativePath(NAVFilePath, subcontent.FlatStructure), Name = name });
                        return;
                    }
                }
                throw new Exception("no such point to add sub point");
            }

        }


        public void AddSubLink(BookDocument content, BookDocument subcontent, string name)
        {
            var point = _tocNav.Find(x => (x.Content == content.PathInEPUB.GetRelativePath(NAVFilePath, content.FlatStructure)));
            if (point != null)
            {
                point.SubPoints.Add(new NavPointV3 { Content = string.Format("{0}#{1}", content, subcontent), Name = name });
            }
            else
            {
                throw new Exception("no such point to add sub point");
            }

        }

    }
}
