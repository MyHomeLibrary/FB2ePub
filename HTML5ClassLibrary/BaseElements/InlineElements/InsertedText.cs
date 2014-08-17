using System;
using System.Collections.Generic;
using System.Xml;
using System.Xml.Linq;
using XHTMLClassLibrary.Attributes;
using XHTMLClassLibrary.BaseElements.BlockElements;
using XHTMLClassLibrary.Exceptions;

namespace XHTMLClassLibrary.BaseElements.InlineElements
{
    /// <summary>
    /// The ins element is used to mark up content that has been inserted into the current version of a document. 
    /// The ins element indicates that content in the previous version of the document has been changed, 
    /// and that the changes are found inside the ins element.
    /// </summary>
    [HTMLItemAttribute(ElementName = "ins", SupportedStandards = HTMLElementType.HTML5 |  HTMLElementType.XHTML5 |  HTMLElementType.Transitional | HTMLElementType.Strict | HTMLElementType.FrameSet)]
    public class InsertedText : HTMLItem, IInlineItem, IBlockElement
    {

        public InsertedText()
        {
            RegisterAttribute(_cite);
            RegisterAttribute(_datetime);
        }

        /// <summary>
        /// Internal content of the element
        /// </summary>
        private readonly List<IHTMLItem> _content = new List<IHTMLItem>();


        private readonly CiteAttribute _cite = new CiteAttribute();
        private readonly DateTimeAttribute _datetime = new DateTimeAttribute();

        /// <summary>
        /// This attribute is intended to point to information explaining why content was changed. 
        /// For example, this can be a URL leading to a Web page that contains such an explanation.
        /// </summary>
        public CiteAttribute Cite
        {
            get { return _cite; }
        }


        public DateTimeAttribute DateTime
        {
            get { return _datetime; }
        }

        protected override bool IsValidSubType(IHTMLItem item)
        {
            if (item is SimpleHTML5Text)
            {
                return item.IsValid();
            }
            if (item is IInlineItem)
            {
                return item.IsValid();
            }
            if (item is IBlockElement)
            {
                return item.IsValid();
            }
            return false;
        }


        /// <summary>
        /// Checks it element data is valid
        /// </summary>
        /// <returns>
        /// true if valid
        /// </returns>
        public override bool IsValid()
        {
            return true;
        }

        public override List<IHTMLItem> SubElements()
        {
            return _content;
        }
    }
}
