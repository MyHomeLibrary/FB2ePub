using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Xml;
using System.Xml.Linq;
using EPubLibrary.CSS_Items;
using EPubLibrary.PathUtils;
using EPubLibraryContracts;
using XHTMLClassLibrary;
using XHTMLClassLibrary.BaseElements;
using XHTMLClassLibrary.BaseElements.BlockElements;
using XHTMLClassLibrary.BaseElements.InlineElements;
using XHTMLClassLibrary.BaseElements.Structure_Header;

namespace EPubLibrary.XHTML_Items
{
    public class BaseXHTMLFileV3 : IEPubPath, IBaseXHTMLFile
    {
        protected Head HeadElement;
        protected Body BodyElement;
        protected string InternalPageTitle;
        protected bool Durty = true;
        protected const HTMLElementType Compatibility = HTMLElementType.HTML5;
        private IHTMLItem _content;



        public EPubInternalPath FileEPubInternalPath;

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

        public IHTMLItem Content
        {
            get { return _content; }
            set
            {
                _content = value;
                Durty = true;
            }
        }



        /// <summary>
        /// Get/Set max document size in bytes
        /// </summary>
        public ulong MaxSize { get; set; }

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

        /// <summary>
        /// Get navigation level (depth) of the element
        /// </summary>
        public int NavigationLevel
        {
            get
            {
                if (NavigationParent == null)
                {
                    return 1;
                }
                return NavigationParent.NavigationLevel + 1;
            }
        }


        /// <summary>
        /// Get / Set document parent
        /// if null - means top level element
        /// </summary>
        public IBaseXHTMLFile NavigationParent { get; set; }

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

            mainDocument.RootHTML.ItemNamespaces.Add(new CustomNamespace(XNamespace.Xmlns + "epub",EPubNamespaces.OpsNamespace));

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
            BodyElement.Add(_content ?? new EmptyLine(Compatibility));
        }

        /// <summary>
        /// Checks if XHTML element is part of current document
        /// </summary>
        /// <param name="value">element to check</param>
        /// <returns>true if part of this document, false otherwise</returns>
        public bool PartOfDocument(IHTMLItem value)
        {
            if (Content == null)
            {
                return false;
            }
            IHTMLItem parent = value;
            while (parent.Parent != null)
            {
                parent = parent.Parent;
            }
            if (parent == Content)
            {
                return true;
            }
            return false;
        }

        public List<BaseXHTMLFileV3> Split()
        {
            var list = new List<BaseXHTMLFileV3>();
            BaseXHTMLFileV3 newDoc = null;
            var listToRemove = new List<IHTMLItem>();
            ulong totlaSize = 0;
            IHTMLItem oldContent = _content;
            var newContent = new Div(Compatibility);
            if (_content != null)
            {
                foreach (var subElement in _content.SubElements())
                {
                    ulong itemSize = EstimateSize(subElement);
                    if (totlaSize + itemSize > MaxSize)
                    {
                        Content = newContent;
                        newDoc = new BaseXHTMLFileV3()
                        {
                            Content = oldContent,
                            PageTitle = PageTitle,
                            NotPartOfNavigation = true
                        };
                        newDoc.StyleFiles.AddRange(StyleFiles);
                        newDoc.GuideRole = GuideRole;
                        newDoc.NavigationParent = NavigationParent;
                        break;
                    }
                    if (itemSize <= MaxSize)
                    {
                        totlaSize += itemSize;
                        newContent.Add(subElement);
                    }
                    listToRemove.Add(subElement);
                }
                foreach (var item in listToRemove)
                {
                    oldContent.Remove(item);
                }
                if (newDoc != null)
                {
                    list.Add(newDoc);
                    if (EstimateSize(newDoc.Content) > MaxSize)
                    {
                        if ((newDoc.Content.SubElements() != null) && (newDoc.Content.SubElements().Count > 1)) // in case we have only one sub-element we can't split
                        {
                            var subList = newDoc.Split();
                            list.AddRange(subList);
                            list.Remove(newDoc);
                        }
                        else
                        {
                            if (newDoc.Content.SubElements()[0] is Paragraph) // in case element we about to split is paragraph
                            {
                                List<BaseXHTMLFileV3> subList = SplitParagraph(newDoc.Content.SubElements()[0] as Paragraph);
                                list.AddRange(subList);
                            }
                            else if (newDoc.Content.SubElements()[0] is Div)
                            {
                                newDoc.Content = newDoc.Content.SubElements()[0];
                                List<BaseXHTMLFileV3> subList = newDoc.Split();
                                list.AddRange(subList);
                            }
                        }
                    }
                }
            }
            return list;
        }

        private List<BaseXHTMLFileV3> SplitParagraph(Paragraph paragraph)
        {
            var list = new List<BaseXHTMLFileV3>();
            foreach (var subElement in paragraph.SubElements())
            {
                var newParagraph = new Paragraph(Compatibility);
                newParagraph.Add(subElement);
                ulong itemSize = EstimateSize(newParagraph);
                if (itemSize > MaxSize)
                {
                    if (Content.SubElements() != null)
                    {
                        List<BaseXHTMLFileV3> subList = null;
                        if (subElement.GetType() == typeof(SimpleHTML5Text))
                        {
                            subList = SplitSimpleText(subElement as SimpleHTML5Text);
                        }
                        if (subList != null)
                        {
                            list.AddRange(subList);
                        }
                    }
                }
                else
                {
                    Content.Add(newParagraph);
                }
            }
            return list;
        }

        private List<BaseXHTMLFileV3> SplitSimpleText(SimpleHTML5Text simpleEPubText)
        {
            var list = new List<BaseXHTMLFileV3>();
            var newDoc = new BaseXHTMLFileV3 { PageTitle = PageTitle, NotPartOfNavigation = true };
            newDoc.StyleFiles.AddRange(StyleFiles);
            newDoc.GuideRole = GuideRole;
            newDoc.NavigationParent = NavigationParent;
            newDoc.Content = new Div(Compatibility);
            var newParagraph = new Paragraph(Compatibility);
            newDoc.Content.Add(newParagraph);
            var newText = new SimpleHTML5Text(Compatibility) { Text = "" };
            newParagraph.Add(newText);
            foreach (var word in simpleEPubText.Text.Split(' '))
            {
                newText.Text += ' ';
                newText.Text += word;
                ulong itemSize = EstimateSize(newParagraph);
                if (itemSize >= MaxSize)
                {
                    list.Add(newDoc);
                    newDoc = new BaseXHTMLFileV3 { PageTitle = PageTitle, NotPartOfNavigation = true };
                    newDoc.StyleFiles.AddRange(StyleFiles);
                    newDoc.GuideRole = GuideRole;
                    newDoc.NavigationParent = NavigationParent;
                    newDoc.Content = new Div(Compatibility);
                    newParagraph = new Paragraph(Compatibility);
                    newDoc.Content.Add(newParagraph);
                    newText = new SimpleHTML5Text(Compatibility) { Text = "" };
                    newParagraph.Add(newText);
                }
            }
            list.Add(newDoc);

            return list;
        }

        private static ulong EstimateSize(IHTMLItem item)
        {
            var stream = new MemoryStream();
            XNode node = item.Generate();
            using (var writer = XmlWriter.Create(stream))
            {
                node.WriteTo(writer);
            }
            return (ulong)stream.Length;
        }


    }
}
