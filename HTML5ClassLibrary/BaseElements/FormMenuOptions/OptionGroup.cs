using System;
using System.Collections.Generic;
using System.Xml;
using System.Xml.Linq;
using HTML5ClassLibrary.Attributes;
using HTML5ClassLibrary.Attributes.FlaggedAttributes;
using HTML5ClassLibrary.Exceptions;

namespace HTML5ClassLibrary.BaseElements.FormMenuOptions
{
    /// <summary>
    /// The optgroup element is used to group the choices offered in select form controls. 
    /// Users find it easier to work with long lists if related sections are grouped together. 
    /// </summary>
    public class OptionGroup : BaseOptionItem
    {
        public const string ElementName = "optgroup";

        /// <summary>
        /// Internal content of the element
        /// </summary>
        private readonly List<IHTML5Item> _content = new List<IHTML5Item>();


        // Basic attributes
        private readonly LabelAttribute _labelAttribute = new LabelAttribute();

        // Advanced attributes
        private readonly DisabledAttribute _disabledAttribute = new DisabledAttribute();



        /// <summary>
        /// Label for the option group.
        /// </summary>
        public LabelAttribute Label { get { return _labelAttribute; } }


        /// <summary>
        /// Disables the control for user input. 
        /// Possible value is "disabled".
        /// </summary>
        public DisabledAttribute Disabled { get { return _disabledAttribute; } }


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

            // Basic attributes
            _labelAttribute.ReadAttribute(xElement);

            // Advanced attributes
            _disabledAttribute.ReadAttribute(xElement);

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

            // Basic attributes
            _labelAttribute.AddAttribute(xElement);

            // Advanced attributes
            _disabledAttribute.AddAttribute(xElement);

            if (_content.Count > 0)
            {
                foreach (var item in _content)
                {
                    xElement.Add(item.Generate());
                }
            }
            

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
            if ((item != null) && IsValidSubType(item))
            {
                _content.Add(item);
                item.Parent = this;
            }
            else
            {
                throw new HTML5ViolationException(item,"");
            }
        }

        public override void Remove(IHTML5Item item)
        {
            if ( _content.Remove(item) )
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
            if (item is Option)
            {
                return item.IsValid();
            }
            return false;
        }
    }
}
