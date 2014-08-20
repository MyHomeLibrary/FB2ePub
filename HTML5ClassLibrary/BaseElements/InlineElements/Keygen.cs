using System;
using System.Collections.Generic;
using System.Xml;
using System.Xml.Linq;
using XHTMLClassLibrary.Attributes;
using XHTMLClassLibrary.Attributes.FlaggedAttributes;

namespace XHTMLClassLibrary.BaseElements.InlineElements
{
    /// <summary>
    /// The "keygen" tag specifies a key-pair generator field used for forms.
    /// When the form is submitted, the private key is stored locally, and the public key is sent to the server.
    /// </summary>
    [HTMLItemAttribute(ElementName = "keygen", SupportedStandards = HTMLElementType.HTML5 |  HTMLElementType.XHTML5 | HTMLElementType.XHTML11 | HTMLElementType.Transitional | HTMLElementType.Strict | HTMLElementType.FrameSet)]    
    public class Keygen : HTMLItem, IInlineItem
    {
        [AttributeTypeAttributeMember(Name = "autofocus", SupportedStandards = HTMLElementType.HTML5 | HTMLElementType.XHTML5)]
        private readonly FlagAttribute _autoFocusAttribute = new FlagAttribute();

        [AttributeTypeAttributeMember(Name = "challenge", SupportedStandards = HTMLElementType.HTML5 | HTMLElementType.XHTML5)]
        private readonly FlagAttribute _challengeAttribute = new FlagAttribute();

        [AttributeTypeAttributeMember(Name = "disabled", SupportedStandards = HTMLElementType.HTML5 | HTMLElementType.XHTML5)]
        private readonly FlagAttribute _disabledAttribute = new FlagAttribute();

        [AttributeTypeAttributeMember(SupportedStandards = HTMLElementType.HTML5 | HTMLElementType.XHTML5)]
        private readonly FormIdAttribute _formIdAttribute = new FormIdAttribute();

        [AttributeTypeAttributeMember(SupportedStandards = HTMLElementType.HTML5 | HTMLElementType.XHTML5)]
        private readonly KeyTypeAttribute _keyTypeAttribute = new KeyTypeAttribute();

        [AttributeTypeAttributeMember(SupportedStandards = HTMLElementType.HTML5 | HTMLElementType.XHTML5)]
        private readonly NameAttribute _nameAttribute = new NameAttribute();



        /// <summary>
        /// Specifies that a "keygen" element should automatically get focus when the page loads
        /// </summary>
        public FlagAttribute Autofocus { get { return _autoFocusAttribute; }}

        /// <summary>
        /// Specifies that the value of the "keygen" element should be challenged when submitted
        /// </summary>
        public FlagAttribute Challenge { get { return _challengeAttribute; }}

        /// <summary>
        /// Specifies that a "keygen" element should be disabled
        /// </summary>
        public FlagAttribute Disable { get { return _disabledAttribute; }}

        /// <summary>
        /// Specifies one or more forms the keygen element belongs to
        /// </summary>
        public FormIdAttribute Form { get { return _formIdAttribute; }}

        /// <summary>
        /// Specifies the security algorithm of the key
        /// </summary>
        public KeyTypeAttribute Keytype { get { return _keyTypeAttribute; }}

        /// <summary>
        /// Defines a name for the "keygen" element
        /// </summary>
        public NameAttribute Name { get { return _nameAttribute; }}


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
