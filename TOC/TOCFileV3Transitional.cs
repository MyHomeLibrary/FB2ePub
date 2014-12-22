using System;
using System.Xml.Linq;

namespace EPubLibrary.TOC
{
    public class TOCFileV3Transitional : TOCFile
    {
        protected override void AddNamespaces(XDocument document)
        {
            // external namespaces not allowed in V3
            //document.Add(new XDocumentType("ncx", @"-//NISO//DTD ncx 2005-1//EN", @"http://www.daisy.org/z3986/2005/ncx-2005-1.dtd", null));
        }


    }
}
