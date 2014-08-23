using XHTMLClassLibrary.Attributes;

namespace XHTMLClassLibrary.BaseElements.TableElements
{
    /// <summary>
    /// The tfoot element can be used to group table rows that contain table footer information. 
    /// This may be useful when printing longer tables that span several printed pages, since the data in tfoot is repeated on each page. 
    /// The tfoot element should appear before tbody elements.
    /// </summary>
    [HTMLItemAttribute(ElementName = "tfoot", SupportedStandards = HTMLElementType.HTML5 |  HTMLElementType.XHTML5 | HTMLElementType.Transitional | HTMLElementType.Strict | HTMLElementType.FrameSet)]
    public class TableFooter : HTMLItem
    {
        [AttributeTypeAttributeMember(Name = "align", SupportedStandards = HTMLElementType.Transitional | HTMLElementType.Strict | HTMLElementType.FrameSet)]
        private readonly AlignTypeAttribute _alignAttribute = new AlignTypeAttribute();

        [AttributeTypeAttributeMember(Name = "char", SupportedStandards = HTMLElementType.Transitional | HTMLElementType.Strict | HTMLElementType.FrameSet)]
        private readonly CharAttribute _charAttribute = new CharAttribute();

        [AttributeTypeAttributeMember(Name = "charoff", SupportedStandards = HTMLElementType.Transitional | HTMLElementType.Strict | HTMLElementType.FrameSet)]
        private readonly LengthTypeAttribute _charOffAttribute = new LengthTypeAttribute();

        [AttributeTypeAttributeMember(Name = "valign", SupportedStandards = HTMLElementType.Transitional | HTMLElementType.Strict | HTMLElementType.FrameSet)]
        private readonly VAlignTypeAttribute _vAlignAttribute = new VAlignTypeAttribute();




        /// <summary>
        /// Aligns the content inside the "tfoot" element
        /// Not supported in HTML5.
        /// </summary>
        public AlignTypeAttribute Align { get { return _alignAttribute; }}


        /// <summary>
        /// Aligns the content inside the "tfoot" element to a character
        /// Not supported in HTML5.
        /// </summary>
        public CharAttribute Char { get { return _charAttribute; }}


        /// <summary>
        /// Sets the number of characters the content inside the "tfoot" element will be aligned from the character specified by the char attribute
        /// Not supported in HTML5.
        /// </summary>
        public LengthTypeAttribute CharOff { get { return _charOffAttribute; }}


        /// <summary>
        /// Vertical aligns the content inside the "tfoot" element
        /// Not supported in HTML5.
        /// </summary>
        public VAlignTypeAttribute VAlign { get { return _vAlignAttribute; }}




        protected override bool IsValidSubType(IHTMLItem item)
        {
            if (item is TableRow)
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
