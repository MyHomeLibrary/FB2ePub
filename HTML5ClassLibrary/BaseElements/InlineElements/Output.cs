using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml;
using System.Xml.Linq;
using HTML5ClassLibrary.Attributes;

namespace HTML5ClassLibrary.BaseElements.InlineElements
{
    /// <summary>
    /// The "output" tag represents the result of a calculation (like one performed by a script).
    /// </summary>
    public class Output : BaseInlineItem
    {
        public const string ElementName = "output";

        private readonly OutputForAttribute _forAttribute = new OutputForAttribute();
        private readonly FormIdAttribute _formIdAttribute = new FormIdAttribute();
        private readonly NameAttribute _nameAttribute = new NameAttribute();


        /// <summary>
        /// Specifies the relationship between the result of the calculation, and the elements used in the calculation
        /// </summary>
        public OutputForAttribute For { get { return _forAttribute; } }

        /// <summary>
        /// Specifies one or more forms the output element belongs to
        /// </summary>
        public FormIdAttribute Form { get { return _formIdAttribute; }}

        /// <summary>
        /// Specifies a name for the output element
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
            _forAttribute.AddAttribute(xElement);
            _formIdAttribute.AddAttribute(xElement);
            _nameAttribute.AddAttribute(xElement);
        }

        protected override void ReadAttributes(XElement xElement)
        {
            base.ReadAttributes(xElement);
            _forAttribute.ReadAttribute(xElement);
            _formIdAttribute.ReadAttribute(xElement);
            _nameAttribute.ReadAttribute(xElement);
        }
    }
}
