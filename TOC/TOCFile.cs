using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Xml;
using System.Xml.Linq;
using EPubLibrary.PathUtils;
using EPubLibrary.TOC.NavMap;
using EPubLibrary.XHTML_Items;

namespace EPubLibrary.TOC
{
    internal class TOCFile
    {
        public static readonly EPubInternalPath TOCFilePath = new EPubInternalPath(EPubInternalPath.DefaultOebpsFolder + "/toc.ncx");

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
            XDocument tocDocument = new XDocument();

            CreateTOCDocument(tocDocument);

            XmlWriterSettings settings = new XmlWriterSettings();
            settings.CloseOutput = false;
            settings.Encoding = Encoding.UTF8;
            settings.Indent = true;
            using (var writer = XmlWriter.Create(s, settings))
            {
                tocDocument.WriteTo(writer);
            }
            
        }

        public void AddNavPoint(BookDocument content, string name)
        {
            NavPoint bookPoint = new NavPoint { Content = content.PathInEPUB.GetRelativePath(TOCFilePath, content.FlatStructure), Name = name };
            _navMap.Add(bookPoint);
        }

        public void AddSubNavPoint(BookDocument content, BookDocument subcontent, string name)
        {
            var point = _navMap.Find(x => (x.Content == content.PathInEPUB.GetRelativePath(TOCFilePath, content.FlatStructure)));
            if (point != null)
            {
                point.SubPoints.Add(new NavPoint { Content = subcontent.PathInEPUB.GetRelativePath(TOCFilePath, subcontent.FlatStructure), Name = name });
            }
            else
            {
                foreach (var element in _navMap)
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
            var point = _navMap.Find(x => (x.Content == content.PathInEPUB.GetRelativePath(TOCFilePath,content.FlatStructure)));
            if (point != null)
            {
                point.SubPoints.Add(new NavPoint { Content = string.Format("{0}#{1}", content, subcontent), Name = name });
            }
            else
            {
                throw new Exception("no such point to add sub point");
            }

        }


        private void CreateTOCDocument(XDocument document)
        {
            if (ID == null)
            {
                throw new NullReferenceException("ID need to be set first");
            }
            XNamespace ncxNamespace = @"http://www.daisy.org/z3986/2005/ncx/";
            XElement ncxElement = new XElement(ncxNamespace + "ncx");
            ncxElement.Add(new XAttribute("version", "2005-1"));


            // Add head block
            XElement headElement = new XElement(ncxNamespace + "head");

            XElement metaID = new XElement(ncxNamespace + "meta");
            metaID.Add(new XAttribute("name", "dtb:uid"));
            metaID.Add(new XAttribute("content", ID));
            headElement.Add(metaID);

            XElement metaDepth = new XElement(ncxNamespace + "meta");
            metaDepth.Add(new XAttribute("name", "dtb:depth"));
            metaDepth.Add(new XAttribute("content", _navMap.GetDepth()));
            headElement.Add(metaDepth);

            XElement metaTotalPageCount = new XElement(ncxNamespace + "meta");
            metaTotalPageCount.Add(new XAttribute("name", "dtb:totalPageCount"));
            metaTotalPageCount.Add(new XAttribute("content", "0"));
            headElement.Add(metaTotalPageCount);

            XElement metaMaxPageNumber = new XElement(ncxNamespace + "meta");
            metaMaxPageNumber.Add(new XAttribute("name", "dtb:maxPageNumber"));
            metaMaxPageNumber.Add(new XAttribute("content", "0"));
            headElement.Add(metaMaxPageNumber);

            ncxElement.Add(headElement);

            // Add DocTitle block
            XElement docTitleElement = new XElement(ncxNamespace + "docTitle");
            XElement textElement = new XElement(ncxNamespace + "text", Title);
            docTitleElement.Add(textElement);
            ncxElement.Add(docTitleElement);

            ncxElement.Add(_navMap.GenerateXMLMap());

            document.Add(new XDocumentType("ncx", @"-//NISO//DTD ncx 2005-1//EN", @"http://www.daisy.org/z3986/2005/ncx-2005-1.dtd", null));
            document.Add(ncxElement);
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
    }
}