using XHTMLClassLibrary.Attributes;
using System;
using System.Collections.Generic;
using System.Xml;
using System.Xml.Linq;
using XHTMLClassLibrary.Exceptions;

namespace XHTMLClassLibrary.BaseElements.Structure_Header
{
    /// <summary>
    /// To resolve relative URLs, Web browsers will use the base URL from where the Web page was downloaded. 
    /// In some circumstances, it is necessary to instruct the Web browser to use a different base URL, 
    /// in which case the base element is used.
    /// </summary>
    [HTMLItemAttribute(ElementName = "base", SupportedStandards = HTMLElementType.HTML5 |  HTMLElementType.XHTML5 | HTMLElementType.XHTML11 | HTMLElementType.Transitional | HTMLElementType.Strict | HTMLElementType.FrameSet)]
    public class Base : HTMLItem
    {
        [AttributeTypeAttributeMember(SupportedStandards = HTMLElementType.HTML5 | HTMLElementType.XHTML5 | HTMLElementType.XHTML11 | HTMLElementType.Transitional | HTMLElementType.Strict | HTMLElementType.FrameSet)]
        private readonly HrefAttribute _hrefAttribute = new HrefAttribute();

        [AttributeTypeAttributeMember(SupportedStandards = HTMLElementType.HTML5 | HTMLElementType.XHTML5 | HTMLElementType.XHTML11 | HTMLElementType.Transitional | HTMLElementType.Strict | HTMLElementType.FrameSet)]
        private readonly FormTargetAttribute _targetAttribute = new FormTargetAttribute();


        /// <summary>
        /// Specifies the base URL for all relative URLs in the page
        /// </summary>
        public HrefAttribute HRef { get { return _hrefAttribute; } }

        /// <summary>
        /// Specifies the default target for all hyperlinks and forms in the page
        /// </summary>
        public FormTargetAttribute Target { get { return _targetAttribute; }}
        

        public override bool IsValid()
        {
            return true;
        }

    }
}
