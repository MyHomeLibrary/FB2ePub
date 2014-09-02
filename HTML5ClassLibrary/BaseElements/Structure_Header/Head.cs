using System.Linq;
using XHTMLClassLibrary.AttributeDataTypes;
using XHTMLClassLibrary.Attributes;
using XHTMLClassLibrary.BaseElements.BlockElements;
using Script = XHTMLClassLibrary.BaseElements.InlineElements.Script;

namespace XHTMLClassLibrary.BaseElements.Structure_Header
{
    /// <summary>
    /// The head element contains information about the current document, such as its title, 
    /// keywords that may be useful to search engines, 
    /// and other data that is not considered to be document content. 
    /// This information is usually not displayed by browsers.
    /// </summary>
    [HTMLItemAttribute(ElementName = "head", SupportedStandards = HTMLElementType.HTML5 |  HTMLElementType.XHTML5 | HTMLElementType.XHTML11 | HTMLElementType.Transitional | HTMLElementType.Strict | HTMLElementType.FrameSet)]
    public class Head : HTMLItem
    {
        [AttributeTypeAttributeMember(SupportedStandards = HTMLElementType.XHTML11 | HTMLElementType.Transitional | HTMLElementType.Strict | HTMLElementType.FrameSet)]
        private readonly SimpleSingleTypeAttribute<URIs> _profileAttribute = new SimpleSingleTypeAttribute<URIs>("profile");


        public Head(HTMLElementType htmlStandard) : base(htmlStandard)
        {
        }

        /// <summary>
        /// Specifies a URL to a document that contains a set of rules. The rules can be read by browsers to clearly understand the information in the "meta" tag's content attribute
        /// Not supported in HTML5.
        /// </summary>
        public IAttributeDataAccess Profile { get { return _profileAttribute; }}

        protected override bool IsValidSubType(IHTMLItem item)
        {
            if (item is Title   || 
                item is Base    ||
                item is Link    ||
                item is Meta    ||
                item is Script  ||
                item is Style   ||
                item is NoScript
                )
            {
                return item.IsValid();
            }
            return false;
        }

        public override bool IsValid()
        {
            return (Subitems.Count(x => x is Title) <= 1);
        }

    }
}
