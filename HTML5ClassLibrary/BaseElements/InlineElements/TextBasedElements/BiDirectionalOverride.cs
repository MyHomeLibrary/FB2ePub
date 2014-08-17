namespace XHTMLClassLibrary.BaseElements.InlineElements.TextBasedElements
{
    /// <summary>
    /// Unlike English, which is written from left-to-right (LTR), some languages, such as Arabic and Hebrew, 
    /// are written from right-to-left (RTL). When the same paragraph contains both RTL and LTR text, 
    /// this is known as bidirectional text or "bidi" text for short
    /// Most Web browsers will correctly display bidirectional text. 
    /// However, situations may arise when the browser's bidirectional algorithm results in incorrect presentation. 
    /// To correct this, the bdo element allows authors to turn off the bidirectional algorithm for selected fragments of text.
    /// </summary>
    [HTMLItemAttribute(ElementName = "bdo", SupportedStandards = HTMLElementType.HTML5 |  HTMLElementType.XHTML5 | HTMLElementType.XHTML11 | HTMLElementType.Transitional | HTMLElementType.Strict | HTMLElementType.FrameSet)]
    public class BiDirectionalOverride : TextBasedElement
    {
        public override bool IsValid()
        {
            if(!GlobalAttributes.Direction.HasValue())
            {
                return false; //The BDO element have to have direction set
            }
            return base.IsValid();
        }
    }
}
