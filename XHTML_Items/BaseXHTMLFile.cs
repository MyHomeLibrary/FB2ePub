using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Xml;
using System.Xml.Linq;
using EPubLibrary.Content;
using EPubLibrary.CSS_Items;
using EPubLibrary.PathUtils;
using EPubLibraryContracts;
using XHTMLClassLibrary;
using XHTMLClassLibrary.BaseElements;
using XHTMLClassLibrary.BaseElements.Structure_Header;

namespace EPubLibrary.XHTML_Items
{
    public class BaseXHTMLFile : IEPubPath , IBaseXHTMLFile
    {
        protected Head HeadElement;
        protected Body BodyElement;
        protected string InternalPageTitle;
        protected bool Durty = true;
        protected readonly HTMLElementType Compatibility;

        public BaseXHTMLFile(HTMLElementType compatibility)
        {
            Compatibility = compatibility;
        }
        

        protected EPubInternalPath FileEPubInternalPath;

        private readonly List<IStyleElement> _styles = new List<IStyleElement>();
        private XDocument _generatedCodeXDocument;
        private bool _embeddStyles;

        public virtual void GenerateHead()
        {
            HeadElement = new Head(Compatibility);
        }

        public GuideTypeEnum GuideRole { get; set; }

        public bool NotPartOfNavigation{get; set;}

        public bool FlatStructure { get; set; }

        public string Id { get; set; }


        public IEPubInternalPath PathInEPUB
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

        public string HRef
        {
            get { return PathInEPUB.GetRelativePath(DefaultInternalPaths.ContentFilePath, FlatStructure); }
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
            get { return InternalPageTitle; }
            set
            {
                InternalPageTitle = value;
                Durty = true;
            }
        }

        /// <summary>
        /// Get access to list of CSS files
        /// </summary>
        public List<IStyleElement> StyleFiles { get { return _styles; } }


        public void Write(Stream stream)
        {
            var settings = new XmlWriterSettings {CloseOutput = false, Encoding = Encoding.UTF8, Indent = true};


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
            var mainDocument = new HTMLDocument(Compatibility);
            GenerateHead();
            GenerateBody();
            var encoding = new UTF8Encoding();
            foreach (var file in _styles)
            {
                IHTMLItem styleElement;
                if (EmbedStyles)
                {
                    var styleElementEntry = new Style(Compatibility);
                    styleElement = styleElementEntry;
                    styleElementEntry.Type.Value = CSSFile.MediaType.GetAsSerializableString();
                    try
                    {
                        using (var outStream = new MemoryStream())
                        {
                            file.Write(outStream);
                            styleElementEntry.InternalTextItem.Text = encoding.GetString(outStream.ToArray());
                        }
                    }
                    catch
                    {
                        // ignored
                    }
                }
                else
                {
                    var cssStyleSheet = new Link(Compatibility);
                    styleElement = cssStyleSheet;
                    cssStyleSheet.Relation.Value = "stylesheet";
                    cssStyleSheet.Type.Value = file.GetMediaType().GetAsSerializableString();
                    cssStyleSheet.HRef.Value = file.PathInEPUB.GetRelativePath(FileEPubInternalPath, FlatStructure);
                }
                HeadElement.Add(styleElement);
            }

            mainDocument.RootHTML.Add(HeadElement);

            mainDocument.RootHTML.Add(BodyElement);

            if (Compatibility == HTMLElementType.HTML5 ||
                Compatibility == HTMLElementType.XHTML5) // basically in ePub v3 cases
            {
                mainDocument.RootHTML.ItemNamespaces.Add(new CustomNamespace(XNamespace.Xmlns + "epub",EPubNamespaces.OpsNamespace));
            }

            if (!mainDocument.RootHTML.IsValid())
            {
               throw new Exception("Document content is not valid");
            }


            var titleElm = new Title(Compatibility);
            titleElm.InternalTextItem.Text = InternalPageTitle;
            HeadElement.Add(titleElm);
            

            _generatedCodeXDocument =  mainDocument.Generate();
            Durty = false;
            return _generatedCodeXDocument;
        }


        public virtual void GenerateBody()
        {
            BodyElement = new Body(Compatibility);
            BodyElement.GlobalAttributes.Class.Value = "epub";           
        }

        /// <summary>
        /// Checks if XHTML element is part of current document
        /// </summary>
        /// <param name="value">element to check</param>
        /// <returns>true if part of this document, false otherwise</returns>
        public virtual  bool PartOfDocument(IHTMLItem value)
        {
            return false;
        }
    }
}
