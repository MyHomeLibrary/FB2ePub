using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
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
    public class BaseXHTMLFile : IEPubPath , IBaseXHTMLFile
    {
        protected Head HeadElement = null;
        protected Body BodyElement = null;
        protected XNamespace XhtmlNamespace = @"http://www.w3.org/1999/xhtml";
        protected string pageTitle;
        protected bool Durty = true;
        protected XHTMRulesEnum Compatibility = XHTMRulesEnum.EPUBCompatible;

        public BaseXHTMLFile(XHTMRulesEnum compatibility)
        {
            Compatibility = compatibility;
        }
        

        protected EPubInternalPath FileEPubInternalPath = null;

        private readonly List<StyleElement> _styles = new List<StyleElement>();
        private XDocument _generatedCodeXDocument;
        private bool _embeddStyles;

        public virtual void GenerateHead()
        {
            HeadElement = new Head();
        }

        public GuideTypeEnum DocumentType { get; set; }

        public bool NotPartOfNavigation{get; set;}

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
        public bool EmbedStyles
        {
            get { return _embeddStyles; }
            set
            {
                _embeddStyles = value;
                Durty = true;
            }
        }

        /// <summary>
        /// Document title (meaningless in EPUB , usually used by browsers)
        /// </summary>
        public string PageTitle
        {
            get { return pageTitle; }
            set
            {
                pageTitle = value;
                Durty = true;
            }
        }

        /// <summary>
        /// Get access to list of CSS files
        /// </summary>
        public List<StyleElement> StyleFiles { get { return _styles; } }


        public void Write(Stream stream)
        {
            XmlWriterSettings settings = new XmlWriterSettings();
            settings.CloseOutput = false;
            settings.Encoding = Encoding.UTF8;
            settings.Indent = true;


            XDocument document = _generatedCodeXDocument;
            if (document == null || Durty)
            {
                document = Generate();
            }


            using (var writer = XmlWriter.Create(stream, settings))
            {
                document.WriteTo(writer);
            }
            
        }

        public virtual XDocument Generate()
        {
            XHTMLDocument mainDocument = new XHTMLDocument(Compatibility);
            GenerateHead();
            GenerateBody();
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

            mainDocument.RootHTML.Add(HeadElement);

            mainDocument.RootHTML.Add(BodyElement);

            if (!mainDocument.RootHTML.IsValid())
            {
               throw new Exception("Document content is not valid");
            }


            var titleElm = new XHTMLClassLibrary.BaseElements.Structure_Header.Title();
            titleElm.Content.Text = pageTitle;
            HeadElement.Add(titleElm);
            

            _generatedCodeXDocument =  mainDocument.Generate();
            Durty = false;
            return _generatedCodeXDocument;
        }


        public virtual void GenerateBody()
        {
            BodyElement = new Body();
            BodyElement.Class.Value = "epub";           
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
