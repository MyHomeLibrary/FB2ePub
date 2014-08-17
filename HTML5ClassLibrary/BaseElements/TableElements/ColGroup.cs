using System;
using System.Collections.Generic;
using System.Xml;
using System.Xml.Linq;
using XHTMLClassLibrary.Attributes;
using XHTMLClassLibrary.Exceptions;

namespace XHTMLClassLibrary.BaseElements.TableElements
{
    /// <summary>
    /// In XHTML, tables are physically constructed from rows, rather than columns. 
    /// Table rows contain table cells. In visual Web browsers, when cells line up beneath each other, they are perceived as columns.
    /// 
    /// The colgroup element provides a mechanism to apply attributes to a logical conception of a column. 
    /// The colgroup element is most commonly used to apply table cell alignment using the align and valign attributes, to apply column width using the width attribute, 
    /// and CSS formatting using the class attribute.
    /// 
    /// The colgroup element contains col elements that represent individual columns.
    /// </summary>
    [HTMLItemAttribute(ElementName = "colgroup", SupportedStandards = HTMLElementType.HTML5 |  HTMLElementType.XHTML5 | HTMLElementType.Transitional | HTMLElementType.Strict | HTMLElementType.FrameSet)]
    public class ColGroup : HTMLItem
    {
        [AttributeTypeAttributeMember(SupportedStandards = HTMLElementType.Transitional | HTMLElementType.Strict | HTMLElementType.FrameSet)]
        private readonly AlignAttribute _alignAttribute = new AlignAttribute();

        [AttributeTypeAttributeMember(SupportedStandards = HTMLElementType.Transitional | HTMLElementType.Strict | HTMLElementType.FrameSet)]
        private readonly CharAttribute _charAttribute = new CharAttribute();

        [AttributeTypeAttributeMember(SupportedStandards = HTMLElementType.Transitional | HTMLElementType.Strict | HTMLElementType.FrameSet)]
        private readonly CharOffAttribute _charOffAttribute = new CharOffAttribute();
        
        [AttributeTypeAttributeMember(SupportedStandards = HTMLElementType.HTML5 | HTMLElementType.XHTML5 | HTMLElementType.Transitional | HTMLElementType.Strict | HTMLElementType.FrameSet)]
        private readonly SpanAttribute _spanAttribute = new SpanAttribute();

        [AttributeTypeAttributeMember(SupportedStandards = HTMLElementType.Transitional | HTMLElementType.Strict | HTMLElementType.FrameSet)]
        private readonly VAlignAttribute _vAlignAttribute = new VAlignAttribute();

        [AttributeTypeAttributeMember(SupportedStandards = HTMLElementType.Transitional | HTMLElementType.Strict | HTMLElementType.FrameSet)]
        private readonly WidthAttribute _widthAttribute = new WidthAttribute();

        /// <summary>
        /// A single col element can represent (or "span") multiple columns. 
        /// This attribute contains a number of columns "spanned" by the col element.
        /// </summary>
        public SpanAttribute Span { get { return _spanAttribute; } }

        /// <summary>
        /// Not supported in HTML5.
        /// Aligns the content in a column group
        /// </summary>
        public AlignAttribute Align { get { return _alignAttribute; } }


        /// <summary>
        /// Aligns the content in a column group to a character
        /// Not supported in HTML5.
        /// </summary>
        public CharAttribute Char { get { return _charAttribute; } }


        /// <summary>
        /// Sets the number of characters the content will be aligned from the character specified by the char attribute
        /// Not supported in HTML5.
        /// </summary>
        public CharOffAttribute CharOff { get { return _charOffAttribute; } }


        /// <summary>
        /// Vertical aligns the content in a column group
        /// Not supported in HTML5.
        /// </summary>
        public VAlignAttribute VAlign { get { return _vAlignAttribute; } }


        /// <summary>
        /// Specifies the width of a column group
        /// Not supported in HTML5.
        /// </summary>
        public WidthAttribute Width { get { return _widthAttribute; } }


        protected override bool IsValidSubType(IHTMLItem item)
        {
            if (item is ColElement)
            {
                return item.IsValid();
            }
            return false;
        }

        public override bool IsValid()
        {
            return true;
        }
    }
}
