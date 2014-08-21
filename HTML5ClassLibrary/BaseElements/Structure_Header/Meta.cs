using System.Collections.Generic;
using XHTMLClassLibrary.Attributes;

namespace XHTMLClassLibrary.BaseElements.Structure_Header
{
    /// <summary>
    /// The meta element is a generic mechanism for specifying metadata for a Web page. 
    /// Some search engines use this information.
    /// </summary>
    [HTMLItemAttribute(ElementName = "meta", SupportedStandards = HTMLElementType.HTML5 |  HTMLElementType.XHTML5 | HTMLElementType.Transitional | HTMLElementType.Strict | HTMLElementType.FrameSet | HTMLElementType.XHTML11)]
    public class Meta : HTMLItem
    {
        [AttributeTypeAttributeMember(Name = "charset", SupportedStandards = HTMLElementType.HTML5 | HTMLElementType.XHTML5)]
        private readonly CharsetAttribute _charsetAttribute = new CharsetAttribute();

        [AttributeTypeAttributeMember(Name = "content", SupportedStandards = HTMLElementType.HTML5 | HTMLElementType.XHTML5 | HTMLElementType.XHTML11 | HTMLElementType.Transitional | HTMLElementType.Strict | HTMLElementType.FrameSet)]
        private readonly TextValueAttribute _contentAttribute = new TextValueAttribute();

        [AttributeTypeAttributeMember(Name = "http-equiv", SupportedStandards = HTMLElementType.HTML5 | HTMLElementType.XHTML5 | HTMLElementType.XHTML11 | HTMLElementType.Transitional | HTMLElementType.Strict | HTMLElementType.FrameSet)]
        private readonly NameTokenAttribute _httpEqvAttribute = new NameTokenAttribute();

        [AttributeTypeAttributeMember(Name = "name", SupportedStandards = HTMLElementType.HTML5 | HTMLElementType.XHTML5 | HTMLElementType.XHTML11 | HTMLElementType.Transitional | HTMLElementType.Strict | HTMLElementType.FrameSet)]
        private readonly NameTokenAttribute _nameAttribute = new NameTokenAttribute();

        [AttributeTypeAttributeMember(Name = "scheme", SupportedStandards = HTMLElementType.XHTML11 | HTMLElementType.Transitional | HTMLElementType.Strict | HTMLElementType.FrameSet)]
        private readonly TextValueAttribute _schemeAttribute = new TextValueAttribute();



        /// <summary>
        /// Specifies the character encoding for the HTML document 
        /// </summary>
        public CharsetAttribute Charset { get { return _charsetAttribute; }}

        /// <summary>
        /// This attribute may be used in place of the name attribute. 
        /// Web servers use this attribute to gather information for HTTP response message headers.
        /// </summary>
        public NameTokenAttribute HTTPEquvalent { get { return _httpEqvAttribute; } }

        /// <summary>
        /// The property's value.
        /// </summary>
        public TextValueAttribute Content { get { return _contentAttribute; } }


        /// <summary>
        /// Specifies a scheme to be used to interpret the value of the content attribute
        /// Not supported in HTML5.
        /// </summary>
        public TextValueAttribute Scheme { get { return _schemeAttribute; }}


        /// <summary>
        /// Property name. 
        /// This attribute is required.
        /// </summary>
        public NameTokenAttribute Name { get { return _nameAttribute; } }

     
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
