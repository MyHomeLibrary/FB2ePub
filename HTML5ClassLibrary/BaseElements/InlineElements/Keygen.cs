using System;
using System.Collections.Generic;
using System.Xml;
using System.Xml.Linq;
using HTMLClassLibrary.Attributes;
using HTMLClassLibrary.Attributes.FlaggedAttributes;

namespace HTMLClassLibrary.BaseElements.InlineElements
{
    /// <summary>
    /// The "keygen" tag specifies a key-pair generator field used for forms.
    /// When the form is submitted, the private key is stored locally, and the public key is sent to the server.
    /// </summary>
    [HTMLItemAttribute(ElementName = "keygen", SupportedStandards = HTMLElementType.HTML5 | HTMLElementType.XHTML11 | HTMLElementType.Transitional | HTMLElementType.Strict | HTMLElementType.FrameSet)]    
    public class Keygen : HTMLItem, IInlineItem
    {
        public Keygen()
        {
            RegisterAttribute(_autoFocusAttribute);
            RegisterAttribute(_challengeAttribute);
            RegisterAttribute(_disabledAttribute);
            RegisterAttribute(_formIdAttribute);
            RegisterAttribute(_keyTypeAttribute);
            RegisterAttribute(_nameAttribute);
        }

        private readonly AutoFocusAttribute _autoFocusAttribute = new AutoFocusAttribute();
        private readonly ChallengeAttribute _challengeAttribute = new ChallengeAttribute();
        private readonly DisabledAttribute _disabledAttribute = new DisabledAttribute();
        private readonly FormIdAttribute _formIdAttribute = new FormIdAttribute();
        private readonly KeyTypeAttribute _keyTypeAttribute = new KeyTypeAttribute();
        private readonly NameAttribute _nameAttribute = new NameAttribute();



        /// <summary>
        /// Specifies that a "keygen" element should automatically get focus when the page loads
        /// </summary>
        public AutoFocusAttribute Autofocus { get { return _autoFocusAttribute; }}

        /// <summary>
        /// Specifies that the value of the "keygen" element should be challenged when submitted
        /// </summary>
        public ChallengeAttribute Challenge { get { return _challengeAttribute; }}

        /// <summary>
        /// Specifies that a "keygen" element should be disabled
        /// </summary>
        public DisabledAttribute Disable { get { return _disabledAttribute; }}

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
