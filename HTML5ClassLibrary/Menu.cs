using System;
using System.Collections.Generic;
using System.Xml;
using System.Xml.Linq;
using HTML5ClassLibrary.Attributes;
using HTML5ClassLibrary.BaseElements;
using HTML5ClassLibrary.BaseElements.BlockElements;
using HTML5ClassLibrary.Exceptions;

namespace HTML5ClassLibrary
{
    /// <summary>
    /// The "menu" tag defines a list/menu of commands.
    /// The "menu" tag is used for context menus, toolbars and for listing form controls and commands.
    /// 
    /// </summary>
    public class Menu : BaseBlockElement
    {
        public const string ElementName = "menu";

        private readonly LabelAttribute _label = new LabelAttribute();
        private readonly MenuTypeAttribute _menuTypeAttribute = new MenuTypeAttribute();


        /// <summary>
        /// Specifies a visible label for the menu
        /// </summary>
        public LabelAttribute Label { get { return _label; }}

        /// <summary>
        /// Specifies which type of menu to display
        /// </summary>
        public MenuTypeAttribute Type { get { return _menuTypeAttribute; }}

        public override void Load(XNode xNode)
        {
            if (xNode.NodeType != XmlNodeType.Element)
            {
                throw new Exception("xNode is not of element type");
            }
            var xElement = (XElement)xNode;
            if (xElement.Name.LocalName != ElementName)
            {
                throw new Exception("xNode is not empty line element");
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

        protected override bool IsValidSubType(IHTML5Item item)
        {
            return (item is MenuItem);
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
            return true;
        }

        protected override void AddAttributes(XElement xElement)
        {
            base.AddAttributes(xElement);
            _label.AddAttribute(xElement);
            _menuTypeAttribute.AddAttribute(xElement);
        }

        protected override void ReadAttributes(XElement xElement)
        {
            base.ReadAttributes(xElement);
            _label.ReadAttribute(xElement);
            _menuTypeAttribute.ReadAttribute(xElement);
        }
    }
}
