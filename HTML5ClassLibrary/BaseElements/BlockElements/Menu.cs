using XHTMLClassLibrary.Attributes;
using XHTMLClassLibrary.BaseElements.InlineElements;

namespace XHTMLClassLibrary.BaseElements.BlockElements
{
    /// <summary>
    /// The "menu" tag defines a list/menu of commands.
    /// The "menu" tag is used for context menus, toolbars and for listing form controls and commands.
    /// 
    /// </summary>
    [HTMLItemAttribute(ElementName = "menu", SupportedStandards = HTMLElementType.HTML5 |  HTMLElementType.XHTML5 | HTMLElementType.Transitional | HTMLElementType.FrameSet)]
    public class Menu : HTMLItem, IBlockElement
    {
        [AttributeTypeAttributeMember(SupportedStandards = HTMLElementType.HTML5 | HTMLElementType.XHTML5)]
        private readonly LabelAttribute _label = new LabelAttribute();

        [AttributeTypeAttributeMember(SupportedStandards = HTMLElementType.HTML5 | HTMLElementType.XHTML5)]
        private readonly MenuTypeAttribute _menuTypeAttribute = new MenuTypeAttribute();


        /// <summary>
        /// Specifies a visible label for the menu
        /// </summary>
        public LabelAttribute Label { get { return _label; }}

        /// <summary>
        /// Specifies which type of menu to display
        /// </summary>
        public MenuTypeAttribute Type { get { return _menuTypeAttribute; }}

        protected override bool IsValidSubType(IHTMLItem item)
        {
            return (item is MenuItem);
        }

        public override bool IsValid()
        {
            return true;
        }
    }
}
