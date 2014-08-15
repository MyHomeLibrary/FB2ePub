using System;
using System.Collections.Generic;
using System.Xml;
using System.Xml.Linq;
using HTMLClassLibrary.Attributes;
using HTMLClassLibrary.Exceptions;

namespace HTMLClassLibrary.BaseElements.TableElements
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
    [HTMLItemAttribute(ElementName = "colgroup", SupportedStandards = HTMLElementType.HTML5 | HTMLElementType.Transitional | HTMLElementType.Strict | HTMLElementType.FrameSet)]
    public class ColGroup : HTMLItem
    {
        private readonly List<IHTMLItem> _content = new List<IHTMLItem>();

        public ColGroup()
        {
            RegisterAttribute(_spanAttribute);
        }

        private readonly SpanAttribute _spanAttribute = new SpanAttribute();

        /// <summary>
        /// A single col element can represent (or "span") multiple columns. 
        /// This attribute contains a number of columns "spanned" by the col element.
        /// </summary>
        public SpanAttribute Span { get { return _spanAttribute; } }


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
