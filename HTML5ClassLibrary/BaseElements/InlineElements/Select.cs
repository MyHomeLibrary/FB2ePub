using System;
using System.Collections.Generic;
using System.Xml;
using System.Xml.Linq;
using HTML5ClassLibrary.Attributes;
using HTML5ClassLibrary.Attributes.FlaggedAttributes;
using HTML5ClassLibrary.BaseElements.FormMenuOptions;
using HTML5ClassLibrary.Exceptions;

namespace HTML5ClassLibrary.BaseElements.InlineElements
{
    /// <summary>
    /// The select element is used to create an option selector form control which most Web browsers render as a listbox control. 
    /// The list of values for this control is created using option elements. 
    /// These values can be grouped together using the optgroup element.
    /// </summary>
    public class Select : BaseInlineItem
    {
        /// <summary>
        /// Internal content of the element
        /// </summary>
        private readonly List<IHTML5Item> _content = new List<IHTML5Item>();

        public Select()
        {
            Attributes.Add(_multipleAttribute);
            Attributes.Add(_nameAttribute);
            Attributes.Add(_sizeAttribute);
            Attributes.Add(_autoFocusAttribute);
            Attributes.Add(_formIdAttribute);
            Attributes.Add(_requiredAttribute);
            Attributes.Add(_disabledAttribute);
         
        }

        private readonly MultipleAttribute _multipleAttribute = new MultipleAttribute();
        private readonly NameAttribute _nameAttribute = new NameAttribute();
        private readonly SizeAttribute _sizeAttribute = new SizeAttribute();
        private readonly DisabledAttribute _disabledAttribute = new DisabledAttribute();
        private readonly AutoFocusAttribute _autoFocusAttribute = new AutoFocusAttribute();
        private readonly FormIdAttribute _formIdAttribute = new FormIdAttribute();
        private readonly RequiredAttribute _requiredAttribute = new RequiredAttribute();


        internal const string ElementName = "select";



        /// <summary>
        /// If set, this attribute allows multiple selections. 
        /// Possible value is multiple.
        /// </summary>
        public MultipleAttribute Multiple { get { return _multipleAttribute; } }

        /// <summary>
        /// Form control name.
        /// </summary>
        public NameAttribute Name { get { return _nameAttribute; } }


        /// <summary>
        /// Number of rows in the list that should be visible at the same time.
        /// </summary>
        public SizeAttribute Size { get { return _sizeAttribute; } }

        /// <summary>
        /// Disables the control for user input. 
        /// Possible value is disabled.
        /// </summary>
        public DisabledAttribute Disabled { get { return _disabledAttribute; } }

        /// <summary>
        /// Specifies that the drop-down list should automatically get focus when the page loads
        /// </summary>
        public AutoFocusAttribute Autofocus { get { return _autoFocusAttribute; }}

        /// <summary>
        /// Defines one or more forms the select field belongs to
        /// </summary>
        public FormIdAttribute Form { get { return _formIdAttribute; }}

        /// <summary>
        /// Specifies that the user is required to select a value before submitting the form
        /// </summary>
        public RequiredAttribute Required { get { return _requiredAttribute; }}



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

            _content.Clear();
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
                    _content.Add(item);
                }
            }
        }

        public override XNode Generate()
        {
            var xElement = new XElement(XhtmlNameSpace + ElementName);

            AddAttributes(xElement);

            foreach (var item in _content)
            {
                xElement.Add(item.Generate());
            }
            return xElement;

        }

        public override bool IsValid()
        {
            // at least one of sub elements have to appear
            return (_content.Count > 0);
        }

        /// <summary>
        /// Adds sub-item to the item , only if 
        /// allowed by the rules and element can accept content
        /// </summary>
        /// <param name="item">sub-item to add</param>
        public override void Add(IHTML5Item item)
        {
            if ((item != null) && IsValidSubType(item))
            {
                _content.Add(item);
                item.Parent = this;
            }
            else
            {
                throw new HTML5ViolationException(this,"");
            }
        }

        public override void Remove(IHTML5Item item)
        {
            if(_content.Remove(item))
            {
                item.Parent = null;
            }
        }

        public override List<IHTML5Item> SubElements()
        {
            return _content;
        }

        private bool IsValidSubType(IHTML5Item item)
        {
            if (item is IOptionItem)
            {
                return item.IsValid();
            }
            return false;
        }
    }
}
