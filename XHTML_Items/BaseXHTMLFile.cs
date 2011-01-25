using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Xml;
using System.Xml.Linq;
using EPubLibrary.Content.Guide;
using EPubLibrary.CSS_Items;
using XHTMLClassLibrary;
using XHTMLClassLibrary.BaseElements;
using XHTMLClassLibrary.BaseElements.Structure_Header;

namespace EPubLibrary.XHTML_Items
{
    public class BaseXHTMLFile 
    {
        protected Head headElement = null;
        protected Body bodyElement = null;
        protected XNamespace xhtmlNamespace = @"http://www.w3.org/1999/xhtml";
        protected string pageTitle;

        private readonly List<CSSFile> styles = new List<CSSFile>();

        private XHTMLDocument mainDocument = new XHTMLDocument(XHTMRulesEnum.EPUBCompatible);

        public GuideTypeEnum DocumentType { get; set; }

        public bool NotPartOfNavigation { get; set; }

        public bool FlatStructure { get; set; }

        /// <summary>
        /// Get/Set file name to be used when saving into EPUB
        /// </summary>
        public string FileName { get; set; }

        /// <summary>
        /// Get/Set embeding styles into xHTML files instead of referencing style files
        /// </summary>
        public bool EmbedStyles { get; set; }

        /// <summary>
        /// Document title (meaningless in EPUB , usualy used by browsers)
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
        public List<CSSFile> StyleFiles { get { return styles; } }


        public BaseXHTMLFile()
        {
            headElement = new Head();

            bodyElement = new Body();
            bodyElement.Class.Value = "epub";

            
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
            foreach (var file in StyleFiles)
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
                            //styleElementEntry.Add(new SimpleEPubText { Text = outStream.ToString() });
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
                    cssStyleShit.Type.Value = CSSFile.MediaType;
                    cssStyleShit.HRef.Value = FlatStructure
                                                  ? string.Format("../{0}", file.EPubFilePath)
                                                  : file.EPubFilePath;
                }
                headElement.Add(styleElement);
            }

            mainDocument.RootHTML.Add(headElement);

            mainDocument.RootHTML.Add(bodyElement);

            if (!mainDocument.RootHTML.IsValid())
            {
               throw new Exception("Document content is not valid");
            }


            var titleElm = new XHTMLClassLibrary.BaseElements.Structure_Header.Title();
            titleElm.Content.Text = pageTitle;
            headElement.Add(titleElm);
            

            return mainDocument.Generate();
        }

        /// <summary>
        /// Checks if XHTML element is part of current document
        /// </summary>
        /// <param name="value">elemenrt to check</param>
        /// <returns>true idf part of this document, false otherwise</returns>
        public virtual  bool PartOfDocument(IXHTMLItem value)
        {
            return false;
        }
    }
}
