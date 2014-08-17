using System;
using System.Collections.Generic;
using System.Xml;
using System.Xml.Linq;
using XHTMLClassLibrary.Attributes;


namespace XHTMLClassLibrary.BaseElements.Structure_Header
{
    /// <summary>
    /// The link element conveys relationship information that can be used by Web browsers and search engines. 
    /// You can have multiple link elements that link to different resources or describe different relationships. 
    /// The link elements can be contained in the head element.
    /// </summary>
    [HTMLItemAttribute(ElementName = "link", SupportedStandards = HTMLElementType.HTML5 |  HTMLElementType.XHTML5 | HTMLElementType.XHTML11 | HTMLElementType.Transitional | HTMLElementType.Strict | HTMLElementType.FrameSet)]    
    public class Link : HTMLItem
    {
        public Link()
        {
            RegisterAttribute(_hrefAttribute);
            RegisterAttribute(_mediaAttribute);
            RegisterAttribute(_typeAttribute);
            RegisterAttribute(_sizesAttribute);
            RegisterAttribute(_hrefLangAttribute);
            RegisterAttribute(_relAttribute);          
        }

        private readonly HrefAttribute _hrefAttribute = new HrefAttribute();
        private readonly HRefLanguageAttribute _hrefLangAttribute = new HRefLanguageAttribute();
        private readonly MediaAttribute _mediaAttribute = new MediaAttribute();
        private readonly LinkRelationAttribute _relAttribute = new LinkRelationAttribute();
        private readonly MIMETypeAttribute _typeAttribute = new MIMETypeAttribute();
        private readonly SizesAttribute _sizesAttribute = new SizesAttribute();


        public static XNamespace XhtmlNameSpace = @"http://www.w3.org/1999/xhtml";

        #region public_properties


        /// <summary>
        /// Specifies the primary language of the resource designated by href and may only be used when href is specified.
        /// </summary>
        public HRefLanguageAttribute RefLanguage { get { return _hrefLangAttribute;}}

        /// <summary>
        /// Describes the forward relationship from the current document to the resource specified by the href attribute. 
        /// The value of this attribute is a space-separated list of link types.
        /// </summary>
        public LinkRelationAttribute Relation { get { return _relAttribute; } }

        /// <summary>
        /// This attribute specifies the location of a Web resource.
        /// </summary>
        public HrefAttribute HRef { get { return _hrefAttribute; } }

        /// <summary>
        /// his attribute specifies the intended destination medium for style information. 
        /// It may be a single media descriptor or a comma-separated list. 
        /// The default value for this attribute is "screen".
        /// </summary>
        public MediaAttribute Media { get { return _mediaAttribute; } }

        /// <summary>
        /// Style sheet language. 
        /// For example: text/css.
        /// </summary>
        public MIMETypeAttribute Type { get { return _typeAttribute; } }


        /// <summary>
        /// Specifies the size of the linked resource. Only for rel="icon"
        /// </summary>
        public SizesAttribute Sizes { get { return _sizesAttribute; }}


        #endregion


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
