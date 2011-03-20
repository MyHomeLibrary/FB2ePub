using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Xml;
using System.Xml.Linq;
using XHTMLClassLibrary.BaseElements;
using XHTMLClassLibrary.BaseElements.BlockElements;
using XHTMLClassLibrary.BaseElements.InlineElements;

namespace EPubLibrary.XHTML_Items
{
    public enum SectionTypeEnum
    {
        Text,
        Links,
    }

    public class BookDocument : BaseXHTMLFile
    {
        private readonly Dictionary<Anchor,ICommonAttributes> references = new Dictionary<Anchor, ICommonAttributes>();
        

        public Dictionary<Anchor,ICommonAttributes> Refrences
        {
            get { return references; }    
        }


        public BookDocument()
        {
            // real limit is 300k but just to be sure
            MaxSize = 300 * 1024;
            Type = SectionTypeEnum.Text;
        }


        public bool NeedSplit()
        {
            return (EstimateSize(Content)>MaxSize);
        }

        /// <summary>
        /// Get/Set section type
        /// </summary>
        public SectionTypeEnum Type { get; set; }

        /// <summary>
        /// Get/Set max document size in bytes
        /// </summary>
        public static long MaxSize { get; set; }

        
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

        /// <summary>
        /// Get/Set document title (which is also content menu entry)
        /// </summary>
        public new string PageTitle { get { return pageTitle; } set { pageTitle = value; } }


        public IXHTMLItem Content { get; set; }


        public override XDocument Generate()
        {
            if (Content != null)
            {
                bodyElement.Add(Content);                    
            }
            else // just to make sure it's valid element
            {
                bodyElement.Add(new EmptyLine());
            }

            return base.Generate();
        }


        public List<BookDocument> Split()
        {
            List<BookDocument> list = new List<BookDocument>();
            BookDocument newDoc = null;
            List<IXHTMLItem> listToRemove = new List<IXHTMLItem>();
            long totlaSize = 0;
            IXHTMLItem oldContent = Content;
            IXHTMLItem newContent = new Div();
            if (Content != null)
            {
                foreach (var subElement in Content.SubElements())
                {
                    long itemSize = EstimateSize(subElement);
                    if (totlaSize + itemSize > MaxSize)
                    {
                        Content = newContent;
                        newDoc = new BookDocument();
                        newDoc.Content = oldContent;
                        newDoc.PageTitle = PageTitle;
                        newDoc.NotPartOfNavigation = true;
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
                            List<BookDocument> subList = newDoc.Split();
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
            List<BookDocument> list = new List<BookDocument>();
            foreach (var subElement in paragraph.SubElements())
            {
                Paragraph newParagraph = new Paragraph();
                newParagraph.Add(subElement);
                long itemSize = EstimateSize(newParagraph);
                if (itemSize > MaxSize)
                {
                    if (Content.SubElements() != null )
                    {
                        List<BookDocument> subList = null;
                        if (subElement.GetType() == typeof(SimpleEPubText))
                        {
                            subList = SplitSimpleText(subElement as SimpleEPubText);
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

        private List<BookDocument> SplitSimpleText(SimpleEPubText simpleEPubText)
        {
            List<BookDocument> list = new List<BookDocument>();
            BookDocument newDoc = new BookDocument();
            newDoc.PageTitle = PageTitle;
            newDoc.NotPartOfNavigation = true;
            newDoc.StyleFiles.AddRange(StyleFiles);
            newDoc.DocumentType = DocumentType;
            newDoc.NavigationParent = NavigationParent;
            newDoc.Content = new Div();
            Paragraph newParagraph = new Paragraph();
            newDoc.Content.Add(newParagraph);
            SimpleEPubText newText = new SimpleEPubText{Text = ""};
            newParagraph.Add(newText);
            foreach (var word in simpleEPubText.Text.Split(' '))
            {
                newText.Text += ' ';
                newText.Text+= word;
                long itemSize = EstimateSize(newParagraph);
                if (itemSize >= MaxSize)
                {
                    list.Add(newDoc);
                    newDoc = new BookDocument();
                    newDoc.PageTitle = PageTitle;
                    newDoc.NotPartOfNavigation = true;
                    newDoc.StyleFiles.AddRange(StyleFiles);
                    newDoc.DocumentType = DocumentType;
                    newDoc.NavigationParent = NavigationParent;
                    newDoc.Content = new Div();
                    newParagraph = new Paragraph();
                    newDoc.Content.Add(newParagraph);
                    newText = new SimpleEPubText { Text = "" };
                    newParagraph.Add(newText);
                }
            }
            list.Add(newDoc);

            return list;
        }

        private static long EstimateSize(IXHTMLItem item)
        {
            MemoryStream stream = new MemoryStream();
            XNode node = item.Generate();
            using (var writer = XmlWriter.Create(stream))
            {
                node.WriteTo(writer);
            }
            return stream.Length;
        }

        /// <summary>
        /// Checks if XHTML element is part of current document
        /// </summary>
        /// <param name="value">elemenrt to check</param>
        /// <returns>true idf part of this document, false otherwise</returns>
        public new bool PartOfDocument(IXHTMLItem value)
        {
            if (Content == null)
            {
                return false;
            }
            IXHTMLItem parent = value;
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
