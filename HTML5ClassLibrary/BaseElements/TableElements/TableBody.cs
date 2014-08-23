using XHTMLClassLibrary.Attributes;

namespace XHTMLClassLibrary.BaseElements.TableElements
{
    /// <summary>
    /// The tbody element can be used to group table data rows. 
    /// This can be useful when a Web browser supports scrolling of table rows in longer tables. 
    /// Multiple tbody elements can be used for independent scrolling.
    /// </summary>
    [HTMLItemAttribute(ElementName = "tbody", SupportedStandards = HTMLElementType.HTML5 |  HTMLElementType.XHTML5 | HTMLElementType.Transitional | HTMLElementType.Strict | HTMLElementType.FrameSet)]    
    public class TableBody : HTMLItem
    {
        [AttributeTypeAttributeMember(Name = "align", SupportedStandards = HTMLElementType.XHTML11 | HTMLElementType.Transitional | HTMLElementType.Strict | HTMLElementType.FrameSet)]
        private readonly AlignTypeAttribute _alignAttribute = new AlignTypeAttribute();

        [AttributeTypeAttributeMember(Name = "char", SupportedStandards = HTMLElementType.XHTML11 | HTMLElementType.Transitional | HTMLElementType.Strict | HTMLElementType.FrameSet)]
        private readonly CharAttribute _charAttribute = new CharAttribute();

        [AttributeTypeAttributeMember(Name = "charoff",SupportedStandards = HTMLElementType.XHTML11 | HTMLElementType.Transitional | HTMLElementType.Strict | HTMLElementType.FrameSet)]
        private readonly LengthTypeAttribute _charOffAttribute = new LengthTypeAttribute();

        [AttributeTypeAttributeMember(Name = "valign", SupportedStandards = HTMLElementType.XHTML11 | HTMLElementType.Transitional | HTMLElementType.Strict | HTMLElementType.FrameSet)]
        private readonly VAlignTypeAttribute _vAlignAttribute = new VAlignTypeAttribute();




        /// <summary>
        /// Aligns the content inside the "tbody" element
        /// Not supported in HTML5.
        /// </summary>
        public AlignTypeAttribute Align { get { return _alignAttribute; }}


        /// <summary>
        /// Aligns the content inside the "tbody" element to a character
        /// Not supported in HTML5.
        /// </summary>
        public CharAttribute Char { get { return _charAttribute; }}



        /// <summary>
        /// Sets the number of characters the content inside the "tbody" element will be aligned from the character specified by the char attribute
        /// Not supported in HTML5.
        /// </summary>
        public LengthTypeAttribute CharOff { get { return _charOffAttribute; }}


        /// <summary>
        /// Vertical aligns the content inside the "tbody" element
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
