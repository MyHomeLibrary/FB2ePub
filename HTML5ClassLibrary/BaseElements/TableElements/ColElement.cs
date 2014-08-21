﻿using System.Collections.Generic;
using XHTMLClassLibrary.Attributes;

namespace XHTMLClassLibrary.BaseElements.TableElements
{
    /// <summary>
    /// In XHTML, tables are physically constructed from rows, rather than columns. 
    /// Table rows contain table cells. 
    /// In visual Web browsers, when cells line up beneath each other, they are perceived as columns.
    /// </summary>
    [HTMLItemAttribute(ElementName = "col", SupportedStandards = HTMLElementType.HTML5 |  HTMLElementType.XHTML5 | HTMLElementType.Transitional | HTMLElementType.Strict | HTMLElementType.FrameSet)]
    public class ColElement : HTMLItem
    {
        [AttributeTypeAttributeMember(Name = "align", SupportedStandards = HTMLElementType.Transitional | HTMLElementType.Strict | HTMLElementType.FrameSet)]
        private readonly AlignTypeAttribute _alignAttribute = new AlignTypeAttribute();

        [AttributeTypeAttributeMember(Name = "char", SupportedStandards = HTMLElementType.Transitional | HTMLElementType.Strict | HTMLElementType.FrameSet)]
        private readonly CharAttribute _charAttribute = new CharAttribute();

        [AttributeTypeAttributeMember(SupportedStandards =  HTMLElementType.Transitional | HTMLElementType.Strict | HTMLElementType.FrameSet)]
        private readonly CharOffAttribute _charOffAttribute = new CharOffAttribute();
        
        [AttributeTypeAttributeMember(Name = "span", SupportedStandards = HTMLElementType.HTML5 | HTMLElementType.XHTML5 | HTMLElementType.Transitional | HTMLElementType.Strict | HTMLElementType.FrameSet)]
        private readonly NumberAttribute _spanAttribute = new NumberAttribute();

        [AttributeTypeAttributeMember(SupportedStandards = HTMLElementType.Transitional | HTMLElementType.Strict | HTMLElementType.FrameSet)]
        private readonly VAlignAttribute _vAlignAttribute = new VAlignAttribute();

        [AttributeTypeAttributeMember(Name = "width", SupportedStandards = HTMLElementType.Transitional | HTMLElementType.Strict | HTMLElementType.FrameSet)]
        private readonly LengthAttribute _widthAttribute = new LengthAttribute();


        /// <summary>
        /// A single col element can represent (or "span") multiple columns. 
        /// This attribute contains a number of columns "spanned" by the col element.
        /// </summary>
        public NumberAttribute Span { get { return _spanAttribute; } }


        /// <summary>
        /// Not supported in HTML5.
        /// Specifies the alignment of the content related to a "col" element
        /// </summary>
        public AlignTypeAttribute Align { get { return _alignAttribute; }}


        /// <summary>
        /// Specifies the alignment of the content related to a "col" element to a character
        /// Not supported in HTML5.
        /// </summary>
        public CharAttribute Char { get { return _charAttribute; }}


        /// <summary>
        /// Specifies the number of characters the content will be aligned from the character specified by the char attribute
        /// Not supported in HTML5.
        /// </summary>
        public CharOffAttribute CharOff { get { return _charOffAttribute; }}


        /// <summary>
        /// Specifies the vertical alignment of the content related to a "col" element
        /// Not supported in HTML5.
        /// </summary>
        public VAlignAttribute VAlign { get { return _vAlignAttribute; }}


        /// <summary>
        /// Specifies the width of a "col" element
        /// Not supported in HTML5.
        /// </summary>
        public LengthAttribute Width { get { return _widthAttribute; }}




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
