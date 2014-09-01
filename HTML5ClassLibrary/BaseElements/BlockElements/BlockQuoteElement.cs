using XHTMLClassLibrary.AttributeDataTypes;
using XHTMLClassLibrary.Attributes;

namespace XHTMLClassLibrary.BaseElements.BlockElements
{
    /// <summary>
    /// The blockquote element is used to identify larger amounts of quoted text.
    /// </summary>
    [HTMLItemAttribute(ElementName = "blockquote", SupportedStandards = HTMLElementType.HTML5 |  HTMLElementType.XHTML5 | HTMLElementType.XHTML11 | HTMLElementType.Transitional | HTMLElementType.Strict | HTMLElementType.FrameSet)]
    public class BlockQuoteElement : HTMLItem, IBlockElement 
    {
        [AttributeTypeAttributeMember(SupportedStandards = HTMLElementType.HTML5 | HTMLElementType.XHTML5 | HTMLElementType.XHTML11 | HTMLElementType.Transitional | HTMLElementType.Strict | HTMLElementType.FrameSet)]
        private readonly SimpleSingleTypeAttribute<URI> _citeAttribute = new SimpleSingleTypeAttribute<URI>("cite");

        public IAttributeDataAccess Cite { get { return _citeAttribute; } }

        #region Overrides of IBlockElement


        protected override bool IsValidSubType(IHTMLItem item)
        {
            if (item is IBlockElement)
            {
                if (item is IHeader)
                {
                    return false;
                }
                return item.IsValid();
            }
            return false;
        }


        /// <summary>
        /// Checks it element data is valid
        /// </summary>
        /// <returns>true if valid</returns>
        public override bool IsValid()
        {
            return true;
        }

        #endregion
    }
}