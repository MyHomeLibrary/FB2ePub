using XHTMLClassLibrary.Attributes;
using XHTMLClassLibrary.BaseElements.BlockElements;

namespace XHTMLClassLibrary.BaseElements.ListElements
{
    [HTMLItemAttribute(ElementName = "dir", SupportedStandards = HTMLElementType.Transitional | HTMLElementType.FrameSet)]    
    public class DirectoryList : HTMLItem, IBlockElement
    {
        [AttributeTypeAttributeMember(SupportedStandards = HTMLElementType.Transitional | HTMLElementType.FrameSet)]
        private readonly FlagTypeAttribute _compactAttribute = new FlagTypeAttribute("compact");


        /// <summary>
        /// Specifies that the list should render smaller than normal
        /// Not supported in HTML5.
        /// </summary>
        public FlagTypeAttribute Compact { get { return _compactAttribute; }}

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
