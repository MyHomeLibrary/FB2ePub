using System.Collections.Generic;
using XHTMLClassLibrary.AttributeDataTypes;
using XHTMLClassLibrary.Attributes;

namespace XHTMLClassLibrary.BaseElements.InlineElements
{
    /// <summary>
    /// The "embed" tag defines a container for an external application or interactive content (a plug-in).
    ///</summary>
    [HTMLItemAttribute(ElementName = "embed", SupportedStandards = HTMLElementType.HTML5)]
    public class Embed : HTMLItem, IInlineItem
    {
        [AttributeTypeAttributeMember(SupportedStandards = HTMLElementType.HTML5 | HTMLElementType.XHTML5)]
        private readonly SimpleSingleTypeAttribute<Length> _heightAttribute = new SimpleSingleTypeAttribute<Length>("height");

        [AttributeTypeAttributeMember(SupportedStandards = HTMLElementType.HTML5 | HTMLElementType.XHTML5)]
        private readonly SimpleSingleTypeAttribute<URI> _sourceAttribute = new SimpleSingleTypeAttribute<URI>("src");

        [AttributeTypeAttributeMember(SupportedStandards = HTMLElementType.HTML5 | HTMLElementType.XHTML5)]
        private readonly SimpleSingleTypeAttribute<MIME_Type> _typeAttribute = new SimpleSingleTypeAttribute<MIME_Type>("type");

        [AttributeTypeAttributeMember(SupportedStandards = HTMLElementType.HTML5 | HTMLElementType.XHTML5)]
        private readonly SimpleSingleTypeAttribute<Length> _widthAttribute = new SimpleSingleTypeAttribute<Length>("width");


        public Embed(HTMLElementType htmlStandard) : base(htmlStandard)
        {
        }

        /// <summary>
        /// Specifies the height of the embedded content
        /// </summary>
        public IAttributeDataAccess Height { get { return _heightAttribute; }}


        /// <summary>
        /// Specifies the address of the external file to embed
        /// </summary>
        public IAttributeDataAccess Src { get { return _sourceAttribute; } }


        /// <summary>
        /// Specifies the media type of the embedded content
        /// </summary>
        public IAttributeDataAccess Type { get { return _typeAttribute; } }


        /// <summary>
        /// Specifies the width of the embedded content
        /// </summary>
        public IAttributeDataAccess Width { get { return _widthAttribute; } }

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
