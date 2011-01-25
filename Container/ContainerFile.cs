using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Xml;
using System.Xml.Linq;

namespace EPubLibrary.Container
{
    internal class ContainerFile
    {
        public bool FlatStructure { get; set; }

        public void Write(Stream s)
        {
            XDocument metaDataDocument = new XDocument();
            FillMetaDataDocument(metaDataDocument);
            XmlWriterSettings settings = new XmlWriterSettings();
            settings.CloseOutput = false;
            settings.Encoding = Encoding.UTF8;
            settings.Indent = true;
            using (var writer = XmlWriter.Create(s, settings))
            {
                metaDataDocument.WriteTo(writer);
            }
            
        }

        private void FillMetaDataDocument(XDocument document)
        {
            XNamespace localNameSpace = "urn:oasis:names:tc:opendocument:xmlns:container";
            XElement containerElement = new XElement(localNameSpace + "container");
            containerElement.Add(new XAttribute("version", "1.0"));
            XElement rootFilesElement = new XElement(localNameSpace + "rootfiles");
            XElement rootFileElement = new XElement(localNameSpace + "rootfile");
            rootFileElement.Add(new XAttribute("full-path", FlatStructure ? "Content.opf" : @"OEBPS/Content.opf"));
            rootFileElement.Add(new XAttribute("media-type", @"application/oebps-package+xml"));
            rootFilesElement.Add(rootFileElement);
            containerElement.Add(rootFilesElement);
            document.Add(containerElement);
        }

    }
}
