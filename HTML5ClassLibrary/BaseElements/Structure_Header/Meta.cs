using System;
using System.Collections.Generic;
using System.Xml;
using System.Xml.Linq;
using HTMLClassLibrary.Attributes;
using HTMLClassLibrary.Attributes.AttributeGroups.FormEvents;
using HTMLClassLibrary.Attributes.AttributeGroups.HTMLGlobal;
using HTMLClassLibrary.Attributes.AttributeGroups.KeyboardEvents;
using HTMLClassLibrary.Attributes.AttributeGroups.MediaEvents;
using HTMLClassLibrary.Attributes.AttributeGroups.MouseEvents;
using HTMLClassLibrary.Attributes.AttributeGroups.WindowEventAttributes;

namespace HTMLClassLibrary.BaseElements.Structure_Header
{
    /// <summary>
    /// The meta element is a generic mechanism for specifying metadata for a Web page. 
    /// Some search engines use this information.
    /// </summary>
    [HTMLItemAttribute(ElementName = "meta", SupportedStandards = HTMLElementType.HTML5 | HTMLElementType.Transitional | HTMLElementType.Strict | HTMLElementType.FrameSet | HTMLElementType.XHTML11)]
    public class Meta : HTMLItem
    {
        public Meta()
        {
            RegisterAttribute(_contentAttribute);
            RegisterAttribute(_nameAttribute);
            RegisterAttribute(_httpEqvAttribute);
            RegisterAttribute(_charsetAttribute);
        }

        private readonly ContentAttribute _contentAttribute = new ContentAttribute();
        private readonly TokenNameAttribute _nameAttribute = new TokenNameAttribute();
        private readonly HTTPEquivAttribute _httpEqvAttribute = new HTTPEquivAttribute();
        private readonly CharsetAttribute _charsetAttribute = new CharsetAttribute();




        /// <summary>
        /// Specifies the character encoding for the HTML document 
        /// </summary>
        public CharsetAttribute Charset { get { return _charsetAttribute; }}

        /// <summary>
        /// This attribute may be used in place of the name attribute. 
        /// Web servers use this attribute to gather information for HTTP response message headers.
        /// </summary>
        public HTTPEquivAttribute HTTPEquvalent { get { return _httpEqvAttribute; } }

        /// <summary>
        /// The property's value.
        /// </summary>
        public ContentAttribute Content { get { return _contentAttribute; } }


        /// <summary>
        /// Property name. 
        /// This attribute is required.
        /// </summary>
        public TokenNameAttribute Name { get { return _nameAttribute; } }

     
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
