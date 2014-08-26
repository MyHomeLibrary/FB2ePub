using XHTMLClassLibrary.AttributeDataTypes;
using XHTMLClassLibrary.Attributes;
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
        private readonly FlagTypeAttribute _compactAttribute = new FlagTypeAttribute();

        [AttributeTypeAttributeMember(Name = "type", SupportedStandards = HTMLElementType.XHTML11 | HTMLElementType.Transitional | HTMLElementType.Strict | HTMLElementType.FrameSet)]
        private readonly ValuesSelectionTypeAttribute<Text> _listItemTypeAtttribute = new ValuesSelectionTypeAttribute<Text>("1;A;a;I;i;disc;square;circle");





        /// <summary>
        ///  Specifies that the list should render smaller than normal
        /// Not supported in HTML5.
        /// </summary>
        public IAttributeDataAccess Compact { get { return _compactAttribute; }}


        /// <summary>
        ///  Specifies the kind of marker to use in the list
        /// Not supported in HTML5.
        /// </summary>
        public IAttributeDataAccess Type { get { return _listItemTypeAtttribute; }}

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
