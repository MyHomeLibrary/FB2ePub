using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Xml;
using System.Xml.Linq;
using EPubLibrary.Content.Guide;
using EPubLibrary.CSS_Items;
using EPubLibrary.PathUtils;
using XHTMLClassLibrary;
using XHTMLClassLibrary.BaseElements;
using XHTMLClassLibrary.BaseElements.Structure_Header;

namespace EPubLibrary.XHTML_Items
{
    public class BaseXHTMLFile : IEPubPath
    {
        protected Head HeadElement = null;
        protected Body BodyElement = null;
        protected XNamespace XhtmlNamespace = @"http://www.w3.org/1999/xhtml";
        protected string pageTitle;

        protected EPubInternalPath FileEPubInternalPath= null;

        private readonly List<StyleElement> _styles = new List<StyleElement>();

        private readonly XHTMLDocument _mainDocument = new XHTMLDocument(XHTMRulesEnum.EPUBCompatible);

        public GuideTypeEnum DocumentType { get; set; }

        public bool NotPartOfNavigation { get; set; }

        public bool FlatStructure { get; set; }

        public string Id { get; set; }

        public EPubInternalPath PathInEPUB
        {
            get
            {
                if (string.IsNullOrEmpty(FileName))
                {
                    throw new NullReferenceException("FileName property has to be set");
                }
                return new EPubInternalPath(FileEPubInternalPath, FileName);
            }
            
        }

        /// <summary>
        /// Get/Set file name to be used when saving into EPUB
        /// </summary>
        public string FileName { get; set; }

        /// <summary>
        /// Get/Set embedding styles into xHTML files instead of referencing style files
        /// </summary>
        public bool EmbedStyles { get; set; }

        /// <summary>
        /// Document title (meaningless in EPUB , usually used by browsers)
        /// </summary>
        public string PageTitle
        {
            get { return pageTitle; }
            set
            {
                pageTitle = value;
            }
        }

        /// <summary>
        /// Get access to list of CSS files
        /// </summary>
        public List<StyleElement> StyleFiles { get { return _styles; } }


        public BaseXHTMLFile()
        {
            HeadElement = new Head();

            BodyElement = new Body();
            BodyElement.Class.Value = "epub";

            
        }

        public void Write(Stream stream)
        {
            XmlWriterSettings settings = new XmlWriterSettings();
            settings.CloseOutput = false;
            settings.Encoding = Encoding.UTF8;
            settings.Indent = true;

            XDocument document = Generate();


            using (var writer = XmlWriter.Create(stream, settings))
            {
                document.WriteTo(writer);
            }
            
        }

        public virtual XDocument Generate()
        {
            UTF8Encoding encoding = new UTF8Encoding();
            foreach (var file in _styles)
            {
                IXHTMLItem styleElement;
                if (EmbedStyles)
                {
                    Style styleElementEntry = new Style();
                    styleElement = styleElementEntry;
                    styleElementEntry.Type.Value = CSSFile.MediaType;
                    try
                    {
                        using (MemoryStream outStream = new MemoryStream())
                        {
                            file.Write(outStream);
                            styleElementEntry.Content.Text = encoding.GetString(outStream.ToArray());
                        }
                    }
                    catch (Exception)
                    {
                    }
                }
                else
                {
                    Link cssStyleShit = new Link();
                    styleElement = cssStyleShit;
                    cssStyleShit.Relation.Value = "stylesheet";
                    cssStyleShit.Type.Value = file.GetMediaType();
                    cssStyleShit.HRef.Value = file.PathInEPUB.GetRelativePath(FileEPubInternalPath, FlatStructure);
                }
                HeadElement.Add(styleElement);
            }

            _mainDocument.RootHTML.Add(HeadElement);

            _mainDocument.RootHTML.Add(BodyElement);

            if (!_mainDocument.RootHTML.IsValid())
            {
               throw new Exception("Document content is not valid");
            }


            var titleElm = new XHTMLClassLibrary.BaseElements.Structure_Header.Title();
            titleElm.Content.Text = pageTitle;
            HeadElement.Add(titleElm);
            

            return _mainDocument.Generate();
        }

        /// <summary>
        /// Checks if XHTML element is part of current document
        /// </summary>
        /// <param name="value">element to check</param>
        /// <returns>true if part of this document, false otherwise</returns>
        public virtual  bool PartOfDocument(IXHTMLItem value)
        {
            return false;
        }
    }
}
