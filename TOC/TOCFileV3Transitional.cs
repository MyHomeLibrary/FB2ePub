using System;
using System.Xml.Linq;

namespace EPubLibrary.TOC
{
    public class TOCFileV3Transitional : TOCFile
    {
        protected override void CreateTOCDocument(XDocument document)
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

            // external namespaces not allowed in V3
            //document.Add(new XDocumentType("ncx", @"-//NISO//DTD ncx 2005-1//EN", @"http://www.daisy.org/z3986/2005/ncx-2005-1.dtd", null));
            document.Add(ncxElement);
        }


    }
}
