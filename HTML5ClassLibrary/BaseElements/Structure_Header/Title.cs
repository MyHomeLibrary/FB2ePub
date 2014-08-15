using System;
using System.Collections.Generic;
using System.Xml;
using System.Xml.Linq;
using HTMLClassLibrary.Attributes.AttributeGroups.FormEvents;
using HTMLClassLibrary.Attributes.AttributeGroups.HTMLGlobal;
using HTMLClassLibrary.Attributes.AttributeGroups.KeyboardEvents;
using HTMLClassLibrary.Attributes.AttributeGroups.MediaEvents;
using HTMLClassLibrary.Attributes.AttributeGroups.MouseEvents;
using HTMLClassLibrary.Attributes.AttributeGroups.WindowEventAttributes;

namespace HTMLClassLibrary.BaseElements.Structure_Header
{
    /// <summary>
    /// The "title" element is used to identify the document.
    /// </summary>
    [HTMLItemAttribute(ElementName = "title", SupportedStandards = HTMLElementType.HTML5 | HTMLElementType.Transitional | HTMLElementType.Strict | HTMLElementType.FrameSet | HTMLElementType.XHTML11)]
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
