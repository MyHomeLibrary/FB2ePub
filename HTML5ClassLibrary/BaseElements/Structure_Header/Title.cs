using System;
using System.Collections.Generic;
using System.Xml;
using System.Xml.Linq;
using XHTMLClassLibrary.Attributes.AttributeGroups.FormEvents;
using XHTMLClassLibrary.Attributes.AttributeGroups.HTMLGlobal;
using XHTMLClassLibrary.Attributes.AttributeGroups.KeyboardEvents;
using XHTMLClassLibrary.Attributes.AttributeGroups.MediaEvents;
using XHTMLClassLibrary.Attributes.AttributeGroups.MouseEvents;
using XHTMLClassLibrary.Attributes.AttributeGroups.WindowEventAttributes;

namespace XHTMLClassLibrary.BaseElements.Structure_Header
{
    /// <summary>
    /// The "title" element is used to identify the document.
    /// </summary>
    [HTMLItemAttribute(ElementName = "title", SupportedStandards = HTMLElementType.HTML5 |  HTMLElementType.XHTML5 | HTMLElementType.Transitional | HTMLElementType.Strict | HTMLElementType.FrameSet | HTMLElementType.XHTML11)]
    public class Title : HTMLItem
    {
        public Title()
        {
            TextContent = new SimpleHTML5Text();
        }


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
