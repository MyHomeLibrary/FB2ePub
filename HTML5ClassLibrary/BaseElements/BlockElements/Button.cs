using System;
using System.Collections.Generic;
using System.Xml;
using System.Xml.Linq;
using HTML5ClassLibrary.Attributes;
using HTML5ClassLibrary.Attributes.FlaggedAttributes;
using HTML5ClassLibrary.BaseElements.InlineElements;

namespace HTML5ClassLibrary.BaseElements.BlockElements
{
    /// <summary>
    /// The button element is used to create button controls for forms. 
    /// Buttons created using the button element are similar in functionality to buttons created using the input element, 
    /// but offer greater rendering options.
    /// </summary>
    public class Button : BaseBlockElement
    {
        public const string ElementName = "button";

        public Button()
        {
            Attributes.Add(_type);
            Attributes.Add(_name);
            Attributes.Add(_value);
            Attributes.Add(_disabled);
            Attributes.Add(_autofocus);
            Attributes.Add(_form);
            Attributes.Add(_formAction);
            Attributes.Add(_formEncoding);
            Attributes.Add(_formMethod);
            Attributes.Add(_formNoValidate);
            Attributes.Add(_formTarget);
        }

        private readonly ButtonTypeAttribute _type = new ButtonTypeAttribute();
        private readonly NameAttribute      _name = new NameAttribute();
        private readonly ValueAttribute     _value = new ValueAttribute();
        private readonly DisabledAttribute  _disabled = new DisabledAttribute();
        private readonly AutoFocusAttribute _autofocus = new AutoFocusAttribute();
        private readonly FormIdAttribute _form = new FormIdAttribute();
        private readonly FormActionAttribute _formAction = new FormActionAttribute();
        private readonly FormEncodingTypeAttribute _formEncoding = new FormEncodingTypeAttribute();
        private readonly FormMethodAttribute _formMethod = new FormMethodAttribute();
        private readonly FormNoValidateAttribute _formNoValidate = new FormNoValidateAttribute();
        private readonly FormTargetAttribute _formTarget = new FormTargetAttribute();


        public ButtonTypeAttribute Type
        {
            get { return _type; }
        }

        public NameAttribute Name
        {
            get { return _name; }
        }

        public ValueAttribute Value
        {
            get { return _value; }
        }

        public DisabledAttribute Disabled
        {
            get { return _disabled; }
        }

        public AutoFocusAttribute Autofocus
        {
            get { return _autofocus; }
        }

        public FormIdAttribute Form
        {
            get { return _form; }
        }

        public FormActionAttribute FormAction
        {
            get { return _formAction; }
        }

        public FormEncodingTypeAttribute FormEncoding
        {
            get { return _formEncoding; }
        }

        public FormMethodAttribute FormMethod
        {
            get { return _formMethod; }
        }

        public FormNoValidateAttribute FormNoValidate
        {
            get { return _formNoValidate; }
        }

        public FormTargetAttribute FormTarget
        {
            get { return _formTarget; }
        }

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

            Content.Clear();
            IEnumerable<XNode> descendants = xElement.Nodes();
            foreach (var node in descendants)
            {
                IHTML5Item item = ElementFactory.CreateElement(node);
                if ((item != null) && IsValidSubType(item))
                {
                    try
                    {
                        item.Load(node);
                    }
                    catch (Exception)
                    {
                        continue;
                    }
                    Content.Add(item);
                }
            }

        }

        public override XNode Generate()
        {
            var xElement = new XElement(XhtmlNameSpace + ElementName);

            AddAttributes(xElement);

            foreach (var item in Content)
            {
                xElement.Add(item.Generate());
            }
            return xElement;

        }

        public override bool IsValid()
        {
            foreach (var item in Content)
            {
                if (!item.IsValid())
                {
                    return false;
                }
            }
            return true;
        }

        protected override bool IsValidSubType(IHTML5Item item)
        {
            if (item is IInlineItem ||
                item is IBlockElement ||
                item is SimpleHTML5Text)
            {
                return item.IsValid();
            }
            return false;
        }
    }
}
