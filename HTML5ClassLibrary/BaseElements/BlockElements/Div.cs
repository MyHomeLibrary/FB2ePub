using System.ComponentModel;
using XHTMLClassLibrary.AttributeDataTypes;
using XHTMLClassLibrary.Attributes;
using XHTMLClassLibrary.BaseElements.InlineElements;

namespace XHTMLClassLibrary.BaseElements.BlockElements
{
    /// <summary>
    /// The div element offers a generic way of grouping areas of content.
    /// </summary>
    [HTMLItemAttribute(ElementName = "div", SupportedStandards = HTMLElementType.HTML5 |  HTMLElementType.XHTML5 | HTMLElementType.XHTML11 | HTMLElementType.Transitional | HTMLElementType.Strict | HTMLElementType.FrameSet)]
    public class Div : HTMLItem, IBlockElement
    {
        #region Attribute_Values_Enums

        /// <summary>
        /// "align" attribute possible values
        /// </summary>
        public enum AlignAttributePossibleValues
        {
            [Description("center")]
            Center,

            [Description("justify")]
            Justify,

            [Description("right")]
            Right,

            [Description("left")]
            Left,

            [Description("char")]
            Char,
        }

        #endregion


        [AttributeTypeAttributeMember(SupportedStandards = HTMLElementType.XHTML11 | HTMLElementType.Transitional | HTMLElementType.Strict | HTMLElementType.FrameSet)]
        private readonly ValuesSelectionTypeAttribute<Text> _alignAttribute = new ValuesSelectionTypeAttribute<Text>("align",typeof(AlignAttributePossibleValues));


        /// <summary>
        /// Specifies the alignment of the content inside a "div" element
        /// Not supported in HTML5.
        /// </summary>
        public IAttributeDataAccess Align { get { return _alignAttribute; } }

        protected override bool IsValidSubType(IHTMLItem item)
        {
            if (item is IInlineItem ||
                item is IBlockElement ||
                item is SimpleHTML5Text)
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
