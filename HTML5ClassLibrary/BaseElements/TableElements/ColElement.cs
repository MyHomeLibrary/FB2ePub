using System;
using System.Collections.Generic;
using System.Xml;
using System.Xml.Linq;
using HTMLClassLibrary.Attributes;

namespace HTMLClassLibrary.BaseElements.TableElements
{
    /// <summary>
    /// In XHTML, tables are physically constructed from rows, rather than columns. 
    /// Table rows contain table cells. 
    /// In visual Web browsers, when cells line up beneath each other, they are perceived as columns.
    /// </summary>
    [HTMLItemAttribute(ElementName = "col", SupportedStandards = HTMLElementType.HTML5 | HTMLElementType.Transitional | HTMLElementType.Strict | HTMLElementType.FrameSet)]
    public class ColElement : HTMLItem
    {
        public ColElement()
        {
            RegisterAttribute(_spanAttribute);
        }

        // Basic attributes
        private readonly SpanAttribute _spanAttribute = new SpanAttribute();


        /// <summary>
        /// A single col element can represent (or "span") multiple columns. 
        /// This attribute contains a number of columns "spanned" by the col element.
        /// </summary>
        public SpanAttribute Span { get { return _spanAttribute; } }


        public override bool IsValid()
        {
            return true;
        }

        public override List<IHTMLItem> SubElements()
        {
            return null;
        }
    }
}
