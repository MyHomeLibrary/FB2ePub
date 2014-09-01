using System.ComponentModel;
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

        #region Attribute_Values_Enums

        /// <summary>
        /// "type" attribute possible values
        /// </summary>
        public enum ListItemTypeAttributeOptions
        {
            [Description("1")]
            Diggit,

            [Description("A")]
            CapitalA,

            [Description("a")]
            SmallA,

            [Description("I")]
            CapitalI,

            [Description("i")]
            SmallI,

            [Description("disc")]
            Disc,

            [Description("square")]
            Square,

            [Description("circle")]
            Circle,
        }

        #endregion

        [AttributeTypeAttributeMember(SupportedStandards = HTMLElementType.XHTML11 | HTMLElementType.Transitional | HTMLElementType.Strict | HTMLElementType.FrameSet)]
        private readonly FlagTypeAttribute _compactAttribute = new FlagTypeAttribute("compact");

        [AttributeTypeAttributeMember(SupportedStandards = HTMLElementType.XHTML11 | HTMLElementType.Transitional | HTMLElementType.Strict | HTMLElementType.FrameSet)]
        private readonly ValuesSelectionTypeAttribute<Text> _listItemTypeAttribute = new ValuesSelectionTypeAttribute<Text>("type", typeof(ListItemTypeAttributeOptions));





        /// <summary>
        ///  Specifies that the list should render smaller than normal
        /// Not supported in HTML5.
        /// </summary>
        public IAttributeDataAccess Compact { get { return _compactAttribute; }}


        /// <summary>
        ///  Specifies the kind of marker to use in the list
        /// Not supported in HTML5.
        /// </summary>
        public IAttributeDataAccess Type { get { return _listItemTypeAttribute; }}

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
