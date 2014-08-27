using XHTMLClassLibrary.AttributeDataTypes;
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
        private readonly ValuesSelectionTypeAttribute<Text> _alignAttribute = new ValuesSelectionTypeAttribute<Text>("center;justify;right;left;char");

        [AttributeTypeAttributeMember(Name = "char", SupportedStandards = HTMLElementType.XHTML11 | HTMLElementType.Transitional | HTMLElementType.Strict | HTMLElementType.FrameSet)]
        private readonly SimpleSingleTypeAttribute<Character> _charAttribute = new SimpleSingleTypeAttribute<Character>();

        [AttributeTypeAttributeMember(Name = "charoff",SupportedStandards = HTMLElementType.XHTML11 | HTMLElementType.Transitional | HTMLElementType.Strict | HTMLElementType.FrameSet)]
        private readonly SimpleSingleTypeAttribute<Length> _charOffAttribute = new SimpleSingleTypeAttribute<Length>();

        [AttributeTypeAttributeMember(Name = "valign", SupportedStandards = HTMLElementType.XHTML11 | HTMLElementType.Transitional | HTMLElementType.Strict | HTMLElementType.FrameSet)]
        private readonly ValuesSelectionTypeAttribute<Text> _vAlignAttribute = new ValuesSelectionTypeAttribute<Text>("middle;baseline;bottom;top");




        /// <summary>
        /// Aligns the content inside the "tbody" element
        /// Not supported in HTML5.
        /// </summary>
        public IAttributeDataAccess Align { get { return _alignAttribute; } }


        /// <summary>
        /// Aligns the content inside the "tbody" element to a character
        /// Not supported in HTML5.
        /// </summary>
        public IAttributeDataAccess Char { get { return _charAttribute; } }



        /// <summary>
        /// Sets the number of characters the content inside the "tbody" element will be aligned from the character specified by the char attribute
        /// Not supported in HTML5.
        /// </summary>
        public IAttributeDataAccess CharOff { get { return _charOffAttribute; } }


        /// <summary>
        /// Vertical aligns the content inside the "tbody" element
        /// Not supported in HTML5.
        /// </summary>
        public IAttributeDataAccess VAlign { get { return _vAlignAttribute; } }

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
