using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
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

            // external namespaces not allowed in V3
            //document.Add(new XDocumentType("ncx", @"-//NISO//DTD ncx 2005-1//EN", @"http://www.daisy.org/z3986/2005/ncx-2005-1.dtd", null));
            document.Add(ncxElement);
        }


    }
}
