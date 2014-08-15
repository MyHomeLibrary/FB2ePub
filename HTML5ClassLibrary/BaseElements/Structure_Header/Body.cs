using System;
using System.Collections.Generic;
using System.Xml;
using System.Xml.Linq;

using HTMLClassLibrary.BaseElements.BlockElements;

namespace HTMLClassLibrary.BaseElements.Structure_Header
{
    /// <summary>
    /// The body element contains the contents of a Web page.
    /// </summary>
    [HTMLItemAttribute(ElementName = "body", SupportedStandards = HTMLElementType.HTML5 | HTMLElementType.XHTML11 | HTMLElementType.Transitional | HTMLElementType.Strict | HTMLElementType.FrameSet)]
    public class Body : HTMLItem
    {
        public override bool IsValid()
        {
            // body can't be empty
            return (Subitems.Count > 0);
        }

        protected override bool IsValidSubType(IHTMLItem item)
        {
            if (item is IBlockElement)
            {
                return item.IsValid();
            }
            return false;
        }
    }
}
