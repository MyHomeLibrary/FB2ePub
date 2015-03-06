using System.Collections.Generic;
using System.IO;
using System.Xml;
using System.Xml.Linq;
using EPubLibrary.PathUtils;
using XHTMLClassLibrary.Attributes;
using XHTMLClassLibrary.BaseElements;
using XHTMLClassLibrary.BaseElements.BlockElements;
using XHTMLClassLibrary.BaseElements.InlineElements;
using XHTMLClassLibrary.BaseElements.InlineElements.TextBasedElements;

namespace EPubLibrary.XHTML_Items
{
    public enum SectionTypeEnum
    {
        Text,
        Links,
    }

    public class BookDocument : BaseXHTMLFile
    {
        public readonly static EPubInternalPath DefaultTextFilesFolder= new EPubInternalPath(EPubInternalPath.DefaultOebpsFolder + "/text/");
        private readonly Dictionary<Anchor, IAttributeDataAccess> _references = new Dictionary<Anchor, IAttributeDataAccess>();
        private IHTMLItem _content;


        public Dictionary<Anchor, IAttributeDataAccess> Refrences
        {
            get { return _references; }    
        }

        public BookDocument(HTMLElementType compatibility)
            : base(compatibility)
        {
            // real limit is 300k but just to be sure
            MaxSize = 300 * 1024;
            Type = SectionTypeEnum.Text;
            FileEPubInternalPath = DefaultTextFilesFolder;
        }


        /// <summary>
        /// Get/Set section type
        /// </summary>
        public SectionTypeEnum Type { get; set; }

        /// <summary>
        /// Get/Set max document size in bytes
        /// </summary>
        public ulong MaxSize { get; set; }

        
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
        public BookDocument NavigationParent { get; set; }


        public IHTMLItem Content
        {
            get { return _content; }
            set
            {
                _content = value;
                Durty = true;
            }
        }


        public override void GenerateBody()
        {
            base.GenerateBody();
            BodyElement.Add(_content ?? new EmptyLine(Compatibility));
        }



        public List<BookDocument> Split()
        {
            var list = new List<BookDocument>();
            BookDocument newDoc = null;
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
                        newDoc = new BookDocument(Compatibility)
                        {
                            Content = oldContent,
                            PageTitle = PageTitle,
                            NotPartOfNavigation = true
                        };
                        newDoc.StyleFiles.AddRange(StyleFiles);
                        newDoc.DocumentType = DocumentType;
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
                    if (EstimateSize(newDoc.Content) > MaxSize )
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
                                List<BookDocument> subList = SplitParagraph(newDoc.Content.SubElements()[0] as Paragraph);
                                list.AddRange(subList);                                                                                        
                            }
                            else if (newDoc.Content.SubElements()[0] is Div)
                            {
                                newDoc.Content = newDoc.Content.SubElements()[0];
                                List<BookDocument> subList = newDoc.Split();
                                list.AddRange(subList);                                                           
                            }
                        }
                    }
                }
            }
            return list;
        }

        private List<BookDocument> SplitParagraph(Paragraph paragraph)
        {
            var list = new List<BookDocument>();
            foreach (var subElement in paragraph.SubElements())
            {
                var newParagraph = new Paragraph(Compatibility);
                newParagraph.Add(subElement);
                ulong itemSize = EstimateSize(newParagraph);
                if (itemSize > MaxSize)
                {
                    if (Content.SubElements() != null )
                    {
                        List<BookDocument> subList = null;
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

        private List<BookDocument> SplitSimpleText(SimpleHTML5Text simpleEPubText)
        {
            var list = new List<BookDocument>();
            var newDoc = new BookDocument(Compatibility) {PageTitle = PageTitle, NotPartOfNavigation = true};
            newDoc.StyleFiles.AddRange(StyleFiles);
            newDoc.DocumentType = DocumentType;
            newDoc.NavigationParent = NavigationParent;
            newDoc.Content = new Div(Compatibility);
            var newParagraph = new Paragraph(Compatibility);
            newDoc.Content.Add(newParagraph);
            var newText = new SimpleHTML5Text(Compatibility) { Text = "" };
            newParagraph.Add(newText);
            foreach (var word in simpleEPubText.Text.Split(' '))
            {
                newText.Text += ' ';
                newText.Text+= word;
                ulong itemSize = EstimateSize(newParagraph);
                if (itemSize >= MaxSize)
                {
                    list.Add(newDoc);
                    newDoc = new BookDocument(Compatibility) {PageTitle = PageTitle, NotPartOfNavigation = true};
                    newDoc.StyleFiles.AddRange(StyleFiles);
                    newDoc.DocumentType = DocumentType;
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

        /// <summary>
        /// Checks if XHTML element is part of current document
        /// </summary>
        /// <param name="value">element to check</param>
        /// <returns>true if part of this document, false otherwise</returns>
        public override bool PartOfDocument(IHTMLItem value)
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
    }
}
