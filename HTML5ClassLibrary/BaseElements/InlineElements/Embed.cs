using System;
using System.Collections.Generic;
using System.Xml;
using System.Xml.Linq;
using HTMLClassLibrary.Exceptions;

namespace HTMLClassLibrary.BaseElements.InlineElements
{
    /// <summary>
    /// The "embed" tag defines a container for an external application or interactive content (a plug-in).
    ///</summary>
    [HTMLItemAttribute(ElementName = "embed", SupportedStandards = HTMLElementType.HTML5)]
    public class Embed : HTMLItem, IInlineItem
    {      
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
