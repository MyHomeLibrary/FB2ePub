using System;
using System.IO;
using System.Text;
using System.Xml;
using System.Xml.Linq;
using EPubLibrary.PathUtils;
using EPubLibrary.TOC.NavMap;
using EPubLibraryContracts;

namespace EPubLibrary.TOC
{
    public class TOCFile : IEPubPath
    {
        private readonly NavMapElement _navMap = new NavMapElement();
        private string _title;


        public bool IsNavMapEmpty()
        {
            return (_navMap.Count == 0);
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

        public void AddNavPoint(IBaseXHTMLFile content, string name)
        {
            var bookPoint = new NavPoint { Content = content.PathInEPUB.GetRelativePath(DefaultInternalPaths.TOCFilePath, content.FlatStructure), Name = name };
            _navMap.Add(bookPoint);
        }

        public void AddSubNavPoint(IBaseXHTMLFile subcontent, string name)
        {
            var newPoint = new NavPoint
            {
                Content =
                    subcontent.PathInEPUB.GetRelativePath(DefaultInternalPaths.TOCFilePath, subcontent.FlatStructure),
                Name = name
            };
            var parentPoint = _navMap.Find(x => (x.Content == subcontent.NavigationParent.PathInEPUB.GetRelativePath(DefaultInternalPaths.TOCFilePath, subcontent.NavigationParent.FlatStructure)));
            if (parentPoint != null)
            {
                parentPoint.SubPoints.Add(newPoint);
            }
            else
            {
                foreach (var element in _navMap)
                {
                    parentPoint = element.AllContent().Find(x => (x.Content == subcontent.NavigationParent.PathInEPUB.GetRelativePath(DefaultInternalPaths.TOCFilePath, subcontent.NavigationParent.FlatStructure)));
                    if (parentPoint != null)
                    {
                        parentPoint.SubPoints.Add(newPoint);
                        return;
                    }
                }
                throw new Exception("no such point to add sub point");
            }           
        }


        private void CreateTOCDocument(XDocument document)
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
            metaDepth.Add(new XAttribute("content", _navMap.GetDepth()));
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

            ncxElement.Add(_navMap.GenerateXMLMap());

            AddNamespaces(document);
            document.Add(ncxElement);
        }

        protected virtual void AddNamespaces(XDocument document)
        {
            document.Add(new XDocumentType("ncx", @"-//NISO//DTD ncx 2005-1//EN", @"http://www.daisy.org/z3986/2005/ncx-2005-1.dtd", null));
        }


        internal void Consolidate()
        {
            foreach (var point in _navMap)
            {
                point.RemoveDeadEnds();
            }

            var points = _navMap.FindAll(x => (string.IsNullOrEmpty(x.Name)));
            foreach (var navPoint in points)
            {
                if (navPoint.SubPoints.Count == 0)
                {
                    _navMap.Remove(navPoint);
                }
            }
        }

        public IEPubInternalPath PathInEPUB
        {
            get { return DefaultInternalPaths.TOCFilePath; }
        }
    }
}