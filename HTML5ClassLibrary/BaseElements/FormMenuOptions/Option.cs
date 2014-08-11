using System;
using System.Collections.Generic;
using System.Xml;
using System.Xml.Linq;
using HTML5ClassLibrary.Attributes;
using HTML5ClassLibrary.Attributes.FlaggedAttributes;

namespace HTML5ClassLibrary.BaseElements.FormMenuOptions
{
    /// <summary>
    /// The option element represents a choice offered by select form controls.
    /// </summary>
    public class Option : BaseOptionItem
    {
        public const string ElementName = "option";

        private readonly SimpleHTML5Text _optionText = new SimpleHTML5Text();

        private readonly SelectedAttribute  _selectedAttribute = new SelectedAttribute();
        private readonly ValueAttribute _valueAttribute = new ValueAttribute();
        private readonly DisabledAttribute _disabledAttribute = new DisabledAttribute();
        private readonly LabelAttribute _labelAttribute = new LabelAttribute();

        /// <summary>
        /// The script text itself
        /// </summary>
        public SimpleHTML5Text OptionText { get { return _optionText; } }


        /// <summary>
        /// When set, this attribute specifies that an option is pre-selected. 
        /// Possible value is "selected".
        /// </summary>
        public SelectedAttribute Selected { get { return _selectedAttribute; } }

        /// <summary>
        /// Value associated with a selection option.
        /// </summary>
        public ValueAttribute Value { get { return _valueAttribute; } }

        /// <summary>
        /// Disables an option in a list of selectable options. 
        /// Possible value is "disabled".
        /// </summary>
        public DisabledAttribute Disabled { get { return _disabledAttribute; } }

        /// <summary>
        /// Shorter label.
        /// </summary>
        public LabelAttribute Label { get { return _labelAttribute; } }

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

            _optionText.Load(xNode);
        }

        public override XNode Generate()
        {
            var xElement = new XElement(XhtmlNameSpace + ElementName);

            AddAttributes(xElement);

            xElement.Add(_optionText.Generate());
            
            return xElement;
        }

        public override bool IsValid()
        {
            return true;
        }

        /// <summary>
        /// Adds sub-item to the item , only if 
        /// allowed by the rules and element can accept content
        /// </summary>
        /// <param name="item">sub-item to add</param>
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
            _selectedAttribute.AddAttribute(xElement);
            _valueAttribute.AddAttribute(xElement);
            _disabledAttribute.AddAttribute(xElement);
            _labelAttribute.AddAttribute(xElement);

        }

        protected override void ReadAttributes(XElement xElement)
        {
            base.ReadAttributes(xElement);
            _selectedAttribute.ReadAttribute(xElement);
            _valueAttribute.ReadAttribute(xElement);
            _disabledAttribute.ReadAttribute(xElement);
            _labelAttribute.ReadAttribute(xElement);

        }
    }
}
