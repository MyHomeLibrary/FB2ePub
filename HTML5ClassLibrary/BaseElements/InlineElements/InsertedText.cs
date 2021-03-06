﻿using XHTMLClassLibrary.AttributeDataTypes;
using XHTMLClassLibrary.Attributes;
using XHTMLClassLibrary.BaseElements.BlockElements;

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
        [AttributeTypeAttributeMember(SupportedStandards = HTMLElementType.HTML5 | HTMLElementType.XHTML5 | HTMLElementType.XHTML11 | HTMLElementType.Transitional | HTMLElementType.Strict | HTMLElementType.FrameSet)]
        private readonly SimpleSingleTypeAttribute<URI> _cite = new SimpleSingleTypeAttribute<URI>("cite");

        [AttributeTypeAttributeMember(SupportedStandards = HTMLElementType.HTML5 | HTMLElementType.XHTML5 | HTMLElementType.XHTML11 | HTMLElementType.Transitional | HTMLElementType.Strict | HTMLElementType.FrameSet)]
        private readonly SimpleSingleTypeAttribute<Datetime> _datetime = new SimpleSingleTypeAttribute<Datetime>("datetime");

        public InsertedText(HTMLElementType htmlStandard) : base(htmlStandard)
        {
        }

        /// <summary>
        /// This attribute is intended to point to information explaining why content was changed. 
        /// For example, this can be a URL leading to a Web page that contains such an explanation.
        /// </summary>
        public IAttributeDataAccess Cite
        {
            get { return _cite; }
        }


        public IAttributeDataAccess DateTime
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
    }
}
