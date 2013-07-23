using System.IO;
using System.Text;
using System.Xml;
using System.Xml.Linq;
using EPubLibrary.PathUtils;

namespace EPubLibrary.Container
{
    internal class ContainerFile : IEPubPath
    {
        private readonly XNamespace _localNameSpace = "urn:oasis:names:tc:opendocument:xmlns:container";

        public static readonly EPubInternalPath DefaultContainerPath = new EPubInternalPath("META-INF/container.xml")
        {
            SupportFlatStructure = false
        };

        private static readonly EPubInternalPath _ePubDataRoot = new EPubInternalPath("");

        public bool FlatStructure { get; set; }

        public IEPubPath ContentFilePath { set; get; }



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
            XElement containerElement = new XElement(_localNameSpace + "container");
            containerElement.Add(new XAttribute("version", "1.0"));
            XElement rootFilesElement = new XElement(_localNameSpace + "rootfiles");
            XElement rootFileElement = new XElement(_localNameSpace + "rootfile");
            rootFileElement.Add(new XAttribute("full-path", ContentFilePath.PathInEPUB.GetRelativePath(_ePubDataRoot,FlatStructure)));
            rootFileElement.Add(new XAttribute("media-type", @"application/oebps-package+xml"));
            rootFilesElement.Add(rootFileElement);
            containerElement.Add(rootFilesElement);
            document.Add(containerElement);
        }


        public EPubInternalPath PathInEPUB
        {
            get
            {
                return DefaultContainerPath;
            }
            
        }
    }
}
