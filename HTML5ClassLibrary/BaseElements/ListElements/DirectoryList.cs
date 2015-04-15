using XHTMLClassLibrary.Attributes;
using XHTMLClassLibrary.BaseElements.BlockElements;

namespace XHTMLClassLibrary.BaseElements.ListElements
{
    [HTMLItemAttribute(ElementName = "dir", SupportedStandards = HTMLElementType.Transitional | HTMLElementType.FrameSet)]    
    public class DirectoryList : HTMLItem, IBlockElement
    {
        [AttributeTypeAttributeMember(SupportedStandards = HTMLElementType.Transitional | HTMLElementType.FrameSet)]
        private readonly FlagTypeAttribute _compactAttribute = new FlagTypeAttribute("compact");


        public DirectoryList(HTMLElementType htmlStandard) : base(htmlStandard)
        {
        }

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

        public override object Clone()
        {
            var item = new DirectoryList(HTMLStandard);
            item.CloneAttributes(this);
            foreach (var htmlItem in Subitems)
            {
                item.Add(htmlItem.Clone() as IHTMLItem);
            }
            item.TextContent = TextContent;
            return item;
        }


    }
}
