﻿using System.Collections.Generic;
using XHTMLClassLibrary.AttributeDataTypes;
using XHTMLClassLibrary.Attributes;

namespace XHTMLClassLibrary.BaseElements.Structure_Header
{
    /// <summary>
    /// The style element can contain CSS rules (called embedded CSS) or 
    /// a URL that leads to a file containing CSS rules (called external CSS).
    /// </summary>
    [HTMLItemAttribute(ElementName = "style", SupportedStandards = HTMLElementType.HTML5 |  HTMLElementType.XHTML5 | HTMLElementType.Transitional | HTMLElementType.Strict | HTMLElementType.FrameSet | HTMLElementType.XHTML11)]
    public class Style : HTMLItem
    {
        [AttributeTypeAttributeMember(SupportedStandards = HTMLElementType.HTML5 | HTMLElementType.XHTML5 | HTMLElementType.XHTML11 | HTMLElementType.Transitional | HTMLElementType.Strict | HTMLElementType.FrameSet)]
        private readonly SimpleSingleTypeAttribute<MediaDescriptions> _mediaAttribute = new SimpleSingleTypeAttribute<MediaDescriptions>("media");

        [AttributeTypeAttributeMember(SupportedStandards = HTMLElementType.HTML5 | HTMLElementType.XHTML5)]
        private readonly FlagTypeAttribute _scopedAttribute = new FlagTypeAttribute("scoped");

        [AttributeTypeAttributeMember(SupportedStandards = HTMLElementType.HTML5 | HTMLElementType.XHTML5 | HTMLElementType.XHTML11 | HTMLElementType.Transitional | HTMLElementType.Strict | HTMLElementType.FrameSet)]
        private readonly SimpleSingleTypeAttribute<ContentType> _typeAttribute = new SimpleSingleTypeAttribute<ContentType>("type");


        public Style(HTMLElementType htmlStandard) : base(htmlStandard)
        {
        }

        /// <summary>
        /// This attribute specifies the intended destination medium for style information. 
        /// It may be a single media descriptor or a comma-separated list. 
        /// The default value for this attribute is screen.
        /// </summary>
        public IAttributeDataAccess Media { get { return _mediaAttribute; } }

        /// <summary>
        /// This attribute specifies the style sheet language of the element's contents. 
        /// The style sheet language is specified as a content type. 
        /// For example: text/css. 
        /// This attribute is required.
        /// </summary>
        public IAttributeDataAccess Type { get { return _typeAttribute; } }

        /// <summary>
        /// Specifies that the styles only apply to this element's parent element and that element's child elements
        /// </summary>
        public IAttributeDataAccess Scoped { get { return _scopedAttribute; } }


        public override bool IsValid()
        {
            return _typeAttribute.HasValue();
        }

        public override List<IHTMLItem> SubElements()
        {
            return null;
        }

    }
}
