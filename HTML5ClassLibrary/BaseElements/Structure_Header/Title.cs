using System.Collections.Generic;

namespace XHTMLClassLibrary.BaseElements.Structure_Header
{
    /// <summary>
    /// The "title" element is used to identify the document.
    /// </summary>
    [HTMLItemAttribute(ElementName = "title", SupportedStandards = HTMLElementType.HTML5 |  HTMLElementType.XHTML5 | HTMLElementType.Transitional | HTMLElementType.Strict | HTMLElementType.FrameSet | HTMLElementType.XHTML11)]
    public class Title : HTMLItem
    {
        public Title(HTMLElementType htmlStandard)
            : base(htmlStandard)
        {
            TextContent = new SimpleHTML5Text(htmlStandard);
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
