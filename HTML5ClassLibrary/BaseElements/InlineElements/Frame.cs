using System.Collections.Generic;

namespace XHTMLClassLibrary.BaseElements.InlineElements
{
    /// <summary>
    /// The "frame" tag defines one particular window (frame) within a "frameset".
    /// Each "frame" in a "frameset" can have different attributes, such as border, scrolling, the ability to resize, etc.
    /// Note: If you want to validate a page containing frames, be sure the "!DOCTYPE" is set to either "HTML Frameset DTD" or "XHTML Frameset DTD".
    /// </summary>
    [HTMLItemAttribute(ElementName = "frame", SupportedStandards = HTMLElementType.FrameSet)]
    public class Frame : HTMLItem, IInlineItem
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
