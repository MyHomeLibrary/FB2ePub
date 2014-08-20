using XHTMLClassLibrary.Attributes;
using XHTMLClassLibrary.Attributes.FlaggedAttributes;
using XHTMLClassLibrary.BaseElements.BlockElements;

namespace XHTMLClassLibrary.BaseElements.ListElements
{
    /// <summary>
    /// The ul element is used to create unordered lists. 
    /// An unordered list is a grouping of items whose sequence in the list is not important. 
    /// For example, the order in which ingredients for a recipe are presented will not affect the outcome of the recipe.
    /// </summary>
    [HTMLItemAttribute(ElementName = "ul", SupportedStandards = HTMLElementType.HTML5 |  HTMLElementType.XHTML5 | HTMLElementType.Transitional | HTMLElementType.Strict | HTMLElementType.FrameSet | HTMLElementType.XHTML11)]
    public class UnorderedList : HTMLItem, IBlockElement
    {
        [AttributeTypeAttributeMember(Name = "compact", SupportedStandards = HTMLElementType.XHTML11 | HTMLElementType.Transitional | HTMLElementType.Strict | HTMLElementType.FrameSet)]
        private readonly FlagAttribute _compactAttribute = new FlagAttribute();

        [AttributeTypeAttributeMember(SupportedStandards = HTMLElementType.XHTML11 | HTMLElementType.Transitional | HTMLElementType.Strict | HTMLElementType.FrameSet)]
        private readonly ListItemTypeAtttribute _listItemTypeAtttribute = new ListItemTypeAtttribute();





        /// <summary>
        ///  Specifies that the list should render smaller than normal
        /// Not supported in HTML5.
        /// </summary>
        public FlagAttribute Compact { get { return _compactAttribute; }}


        /// <summary>
        ///  Specifies the kind of marker to use in the list
        /// Not supported in HTML5.
        /// </summary>
        public ListItemTypeAtttribute Type { get { return _listItemTypeAtttribute; }}

        protected override bool IsValidSubType(IHTMLItem item)
        {
            if (item is ListItem)
            {
                return item.IsValid();
            }
            return false;
        }

        public override bool IsValid()
        {
            return (Subitems.Count > 0);
        }
    }
}
