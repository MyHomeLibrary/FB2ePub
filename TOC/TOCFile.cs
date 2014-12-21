using System;
using System.IO;
using System.Text;
using System.Xml;
using System.Xml.Linq;
using EPubLibrary.PathUtils;
using EPubLibrary.TOC.NavMap;
using EPubLibrary.XHTML_Items;

namespace EPubLibrary.TOC
{
    public class TOCFile : IEPubPath
    {
        public static readonly EPubInternalPath TOCFilePath = new EPubInternalPath(EPubInternalPath.DefaultOebpsFolder + "/toc.ncx");

        protected readonly NavMapElement NavMap = new NavMapElement();
        private string _title;


        public bool IsNavMapEmpty()
        {
            return (NavMap.Count == 0);
        }

        public string Title
        {
            get
            {
                if(_title == null)
                {
                    return string.Empty;
                }
                return _title;
            }
            set { _title = value;}
        }

        public string ID { get; set; }

        public void Write(Stream s)
        {
            var tocDocument = new XDocument();

            CreateTOCDocument(tocDocument);

            var settings = new XmlWriterSettings {CloseOutput = false, Encoding = Encoding.UTF8, Indent = true};
            using (var writer = XmlWriter.Create(s, settings))
            {
                tocDocument.WriteTo(writer);
            }
            
        }

        public void AddNavPoint(BookDocument content, string name)
        {
            var bookPoint = new NavPoint { Content = content.PathInEPUB.GetRelativePath(TOCFilePath, content.FlatStructure), Name = name };
            NavMap.Add(bookPoint);
        }

        public void AddSubNavPoint(BookDocument content, BookDocument subcontent, string name)
        {
            var point = NavMap.Find(x => (x.Content == content.PathInEPUB.GetRelativePath(TOCFilePath, content.FlatStructure)));
            if (point != null)
            {
                point.SubPoints.Add(new NavPoint { Content = subcontent.PathInEPUB.GetRelativePath(TOCFilePath, subcontent.FlatStructure), Name = name });
            }
            else
            {
                foreach (var element in NavMap)
                {
                    point = element.AllContent().Find(x => (x.Content == content.PathInEPUB.GetRelativePath(TOCFilePath, content.FlatStructure)));
                    if (point != null)
                    {
                        point.SubPoints.Add(new NavPoint { Content = subcontent.PathInEPUB.GetRelativePath(TOCFilePath, subcontent.FlatStructure), Name = name });
                        return;
                    }
                }
                throw new Exception("no such point to add sub point");
            }
            
        }


        public void AddSubLink(BookDocument content, BookDocument subcontent, string name)
        {
            var point = NavMap.Find(x => (x.Content == content.PathInEPUB.GetRelativePath(TOCFilePath,content.FlatStructure)));
            if (point != null)
            {
                point.SubPoints.Add(new NavPoint { Content = string.Format("{0}#{1}", content, subcontent), Name = name });
            }
            else
            {
                throw new Exception("no such point to add sub point");
            }

        }


        protected virtual void CreateTOCDocument(XDocument document)
        {
            if (ID == null)
            {
                throw new NullReferenceException("ID need to be set first");
            }
            var ncxElement = new XElement(DaisyNamespaces.NCXNamespace + "ncx");
            ncxElement.Add(new XAttribute("version", "2005-1"));


            // Add head block
            var headElement = new XElement(DaisyNamespaces.NCXNamespace + "head");

            var metaID = new XElement(DaisyNamespaces.NCXNamespace + "meta");
            metaID.Add(new XAttribute("name", "dtb:uid"));
            metaID.Add(new XAttribute("content", ID));
            headElement.Add(metaID);

            var metaDepth = new XElement(DaisyNamespaces.NCXNamespace + "meta");
            metaDepth.Add(new XAttribute("name", "dtb:depth"));
            metaDepth.Add(new XAttribute("content", NavMap.GetDepth()));
            headElement.Add(metaDepth);

            var metaTotalPageCount = new XElement(DaisyNamespaces.NCXNamespace + "meta");
            metaTotalPageCount.Add(new XAttribute("name", "dtb:totalPageCount"));
            metaTotalPageCount.Add(new XAttribute("content", "0"));
            headElement.Add(metaTotalPageCount);

            var metaMaxPageNumber = new XElement(DaisyNamespaces.NCXNamespace + "meta");
            metaMaxPageNumber.Add(new XAttribute("name", "dtb:maxPageNumber"));
            metaMaxPageNumber.Add(new XAttribute("content", "0"));
            headElement.Add(metaMaxPageNumber);

            ncxElement.Add(headElement);

            // Add DocTitle block
            var docTitleElement = new XElement(DaisyNamespaces.NCXNamespace + "docTitle");
            var textElement = new XElement(DaisyNamespaces.NCXNamespace + "text", Title);
            docTitleElement.Add(textElement);
            ncxElement.Add(docTitleElement);

            ncxElement.Add(NavMap.GenerateXMLMap());

            document.Add(new XDocumentType("ncx", @"-//NISO//DTD ncx 2005-1//EN", @"http://www.daisy.org/z3986/2005/ncx-2005-1.dtd", null));
            document.Add(ncxElement);
        }


        internal void Consolidate()
        {
            foreach (var point in NavMap)
            {
                point.RemoveDeadEnds();
            }

            var points = NavMap.FindAll(x => (string.IsNullOrEmpty(x.Name)));
            foreach (var navPoint in points)
            {
                if (navPoint.SubPoints.Count == 0)
                {
                    NavMap.Remove(navPoint);
                }
            }
        }

        public EPubInternalPath PathInEPUB
        {
            get { return TOCFilePath; }
        }
    }
}