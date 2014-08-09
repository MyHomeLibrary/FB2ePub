using System;
using System.Collections.Generic;
using System.Xml;
using System.Xml.Linq;
using HTML5ClassLibrary.Attributes;
using HTML5ClassLibrary.Attributes.FlaggedAttributes;

namespace HTML5ClassLibrary.BaseElements.InlineElements
{
    /// <summary>
    /// The "keygen" tag specifies a key-pair generator field used for forms.
    /// When the form is submitted, the private key is stored locally, and the public key is sent to the server.
    /// </summary>
    public class Keygen : BaseInlineItem
    {
        public const string ElementName = "keygen";

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

        public override void Load(XNode xNode)
        {
            if (xNode.NodeType != XmlNodeType.Element)
            {
                throw new Exception("xNode is not of element type");
            }
            var xElement = (XElement)xNode;
            if (xElement.Name.LocalName != ElementName)
            {
                throw new Exception(string.Format("xNode is not {0} element", ElementName));
            }
            ReadAttributes(xElement);
        }

        public override XNode Generate()
        {
            var xElement = new XElement(XhtmlNameSpace + ElementName);

            AddAttributes(xElement);
            return xElement;
        }

        public override bool IsValid()
        {
            return true;
        }

        public override void Add(IHTML5Item item)
        {
            throw new Exception("This element does not contain sub-items");
        }

        public override void Remove(IHTML5Item item)
        {
            throw new Exception("This element does not contain sub-items");
        }

        public override List<IHTML5Item> SubElements()
        {
            return null;
        }

        protected override void AddAttributes(XElement xElement)
        {
            base.AddAttributes(xElement);
            _autoFocusAttribute.AddAttribute(xElement);
            _challengeAttribute.AddAttribute(xElement);
            _disabledAttribute.AddAttribute(xElement);
            _formIdAttribute.AddAttribute(xElement);
            _keyTypeAttribute.AddAttribute(xElement);
            _nameAttribute.AddAttribute(xElement);
        }

        protected override void ReadAttributes(XElement xElement)
        {
            base.ReadAttributes(xElement);
            _autoFocusAttribute.ReadAttribute(xElement);
            _challengeAttribute.ReadAttribute(xElement);
            _disabledAttribute.ReadAttribute(xElement);
            _formIdAttribute.ReadAttribute(xElement);
            _keyTypeAttribute.ReadAttribute(xElement);
            _nameAttribute.ReadAttribute(xElement);
        }
    }
}
