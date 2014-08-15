using System.Xml.Linq;
using HTMLClassLibrary.Attributes;

namespace HTMLClassLibrary.BaseElements.InlineElements.TextBasedElements
{
    /// <summary>
    /// The "pre" tag defines preformatted text.
    ///Text in a "pre" element is displayed in a fixed-width font (usually Courier), and it preserves both spaces and line breaks.
    /// </summary>
    [HTMLItemAttribute(ElementName = "pre", SupportedStandards = HTMLElementType.HTML5 | HTMLElementType.Transitional | HTMLElementType.Strict | HTMLElementType.FrameSet | HTMLElementType.XHTML11)]
    public class PreFormated : TextBasedElement
    {
        public PreFormated()
        {
                            
        }

        protected override bool IsValidSubType(IHTMLItem item)
        {
            if (!base.IsValidSubType(item))
            {
                return false;
            }
            if (item is Image || 
                item is SmallText || 
                item is Sub ||
                item is Sup)
            {
                return false;
            }
            if (item.SubElements().FindAll(x => x is Image).Count > 0)
            {
                return false;
            }
            if (item.SubElements().FindAll(x => x is SmallText).Count > 0)
            {
                return false;
            }
            if (item.SubElements().FindAll(x => x is Sub).Count > 0)
            {
                return false;
            }
            if (item.SubElements().FindAll(x => x is Sup).Count > 0)
            {
                return false;
            }
            return true;
        }
    }
}
