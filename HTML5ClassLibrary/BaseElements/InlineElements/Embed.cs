using System.Collections.Generic;
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
        private readonly HeightAttribute _heightAttribute = new HeightAttribute();

        [AttributeTypeAttributeMember(SupportedStandards = HTMLElementType.HTML5 | HTMLElementType.XHTML5)]
        private readonly SourceAttribute _sourceAttribute = new SourceAttribute();

        [AttributeTypeAttributeMember(SupportedStandards = HTMLElementType.HTML5 | HTMLElementType.XHTML5)]
        private readonly MIMETypeAttribute _typeAttribute = new MIMETypeAttribute();

        [AttributeTypeAttributeMember(SupportedStandards = HTMLElementType.HTML5 | HTMLElementType.XHTML5)]
        private readonly WidthAttribute _widthAttribute = new WidthAttribute();


        /// <summary>
        /// Specifies the height of the embedded content
        /// </summary>
        public HeightAttribute Height { get { return _heightAttribute; }}


        /// <summary>
        /// Specifies the address of the external file to embed
        /// </summary>
        public SourceAttribute Src { get { return _sourceAttribute; }}


        /// <summary>
        /// Specifies the media type of the embedded content
        /// </summary>
        public MIMETypeAttribute Type { get { return _typeAttribute; }}


        /// <summary>
        /// Specifies the width of the embedded content
        /// </summary>
        public WidthAttribute Width { get { return _widthAttribute; }}

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
