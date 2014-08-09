using System;
using System.Collections.Generic;
using System.Xml;
using System.Xml.Linq;
using HTML5ClassLibrary.Attributes;
using HTML5ClassLibrary.Attributes.FlaggedAttributes;
using HTML5ClassLibrary.BaseElements;
using HTML5ClassLibrary.BaseElements.InlineElements;

namespace HTML5ClassLibrary
{
    public class MenuItem : BaseInlineItem
    {
        public const string ElementName = "menuitem";

        private readonly CheckedAttribute _checkedAttribute = new CheckedAttribute();
        private readonly DisabledAttribute _disabledAttribute = new DisabledAttribute();
        private readonly LabelAttribute _labelAttribute = new LabelAttribute();
        private readonly CommandAttribute _commandAttribute = new CommandAttribute();
        private readonly DefaultAttribute _defaultAttribute = new DefaultAttribute();
        private readonly IconAttribute _iconAttribute = new IconAttribute();
        private readonly RadioGroupAttribute _radioGroupAttribute = new RadioGroupAttribute();
        private readonly MenuItemTypeAttribute _menuItemTypeAttribute = new MenuItemTypeAttribute();

        /// <summary>
        /// Specifies that the command/menu item should be checked when the page loads. Only for type="radio" or type="checkbox"
        /// </summary>
        public CheckedAttribute Checked { get { return _checkedAttribute; }}

        /// <summary>
        /// Specifies that the command/menu item should be disabled
        /// </summary>
        public DisabledAttribute Disabled { get { return _disabledAttribute; }}

        /// <summary>
        /// Required. Specifies the name of the command/menu item, as shown to the user
        /// </summary>
        public LabelAttribute Label {get { return _labelAttribute; }}

        /// <summary>
        /// 
        /// </summary>
        public CommandAttribute Command { get { return _commandAttribute; }}


        /// <summary>
        /// Marks the command/menu item as being a default command
        /// </summary>
        public DefaultAttribute Default { get { return _defaultAttribute; }}

        /// <summary>
        /// Specifies an icon for the command/menu item
        /// </summary>
        public IconAttribute Icon { get { return _iconAttribute; }}

        /// <summary>
        /// Specifies the name of the group of commands that will be toggled when the command/menu item itself is toggled. Only for type="radio"
        /// </summary>
        public RadioGroupAttribute RadioGroup{ get { return _radioGroupAttribute; }}

        /// <summary>
        /// Specifies the type of command/menu item. Default is "command"
        /// </summary>
        public MenuItemTypeAttribute Type { get { return _menuItemTypeAttribute; }}

        #region Overrides of BaseInlineItem


        /// <summary>
        /// Loads the element from XNode
        /// </summary>
        /// <param name="xNode">node to load element from</param>
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

        /// <summary>
        /// Generates element to XNode from data
        /// </summary>
        /// <returns>
        /// generated XNode
        /// </returns>
        public override XNode Generate()
        {
            var xElement = new XElement(XhtmlNameSpace + ElementName);

            AddAttributes(xElement);
            return xElement;
        }

        /// <summary>
        /// Checks it element data is valid
        /// </summary>
        /// <returns>
        /// true if valid
        /// </returns>
        public override bool IsValid()
        {
            return _labelAttribute.HasValue();
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
            _checkedAttribute.AddAttribute(xElement);
            _disabledAttribute.AddAttribute(xElement);
            _labelAttribute.AddAttribute(xElement);
            _commandAttribute.AddAttribute(xElement);
            _defaultAttribute.AddAttribute(xElement);
            _iconAttribute.AddAttribute(xElement);
            _radioGroupAttribute.AddAttribute(xElement);
            _menuItemTypeAttribute.AddAttribute(xElement);
        }

        protected override void ReadAttributes(XElement xElement)
        {
            base.ReadAttributes(xElement);
            _checkedAttribute.ReadAttribute(xElement);
            _disabledAttribute.ReadAttribute(xElement);
            _labelAttribute.ReadAttribute(xElement);
            _commandAttribute.ReadAttribute(xElement);
            _defaultAttribute.ReadAttribute(xElement);
            _iconAttribute.ReadAttribute(xElement);
            _radioGroupAttribute.ReadAttribute(xElement);
            _menuItemTypeAttribute.ReadAttribute(xElement);
        }

        #endregion

    }
}
