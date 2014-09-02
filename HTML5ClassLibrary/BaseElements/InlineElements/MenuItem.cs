using System;
using System.Collections.Generic;
using System.Xml;
using System.Xml.Linq;
using XHTMLClassLibrary.AttributeDataTypes;
using XHTMLClassLibrary.Attributes;

namespace XHTMLClassLibrary.BaseElements.InlineElements
{
    /// <summary>
    /// 
    /// </summary>
    [HTMLItemAttribute(ElementName = "menuitem", SupportedStandards = HTMLElementType.HTML5 )]
    public class MenuItem : HTMLItem, IInlineItem
    {
        [AttributeTypeAttributeMember(SupportedStandards = HTMLElementType.HTML5 | HTMLElementType.XHTML5)]
        private readonly FlagTypeAttribute _checkedAttribute = new FlagTypeAttribute("checked");

        [AttributeTypeAttributeMember(SupportedStandards = HTMLElementType.HTML5 | HTMLElementType.XHTML5)]
        private readonly SimpleSingleTypeAttribute<Text> _commandAttribute = new SimpleSingleTypeAttribute<Text>("command");

        [AttributeTypeAttributeMember(SupportedStandards = HTMLElementType.HTML5 | HTMLElementType.XHTML5)]
        private readonly FlagTypeAttribute _defaultAttribute = new FlagTypeAttribute("default");

        [AttributeTypeAttributeMember(SupportedStandards = HTMLElementType.HTML5 | HTMLElementType.XHTML5)]
        private readonly FlagTypeAttribute _disabledAttribute = new FlagTypeAttribute("disabled");

        [AttributeTypeAttributeMember(SupportedStandards = HTMLElementType.HTML5 | HTMLElementType.XHTML5)]
        private readonly SimpleSingleTypeAttribute<URI> _iconAttribute = new SimpleSingleTypeAttribute<URI>("icon");

        [AttributeTypeAttributeMember(SupportedStandards = HTMLElementType.HTML5 | HTMLElementType.XHTML5)]
        private readonly SimpleSingleTypeAttribute<Text> _labelAttribute = new SimpleSingleTypeAttribute<Text>("label");

        [AttributeTypeAttributeMember(SupportedStandards = HTMLElementType.HTML5 | HTMLElementType.XHTML5)]
        private readonly SimpleSingleTypeAttribute<URI> _radioGroupAttribute = new SimpleSingleTypeAttribute<URI>("radiogroup");

        [AttributeTypeAttributeMember(SupportedStandards = HTMLElementType.HTML5 | HTMLElementType.XHTML5)]
        private readonly SimpleSingleTypeAttribute<ContentType> _menuItemTypeAttribute = new SimpleSingleTypeAttribute<ContentType>("type");

        public MenuItem(HTMLElementType htmlStandard) : base(htmlStandard)
        {
        }

        /// <summary>
        /// Specifies that the command/menu item should be checked when the page loads. Only for type="radio" or type="checkbox"
        /// </summary>
        public IAttributeDataAccess Checked { get { return _checkedAttribute; }}

        /// <summary>
        /// Specifies that the command/menu item should be disabled
        /// </summary>
        public IAttributeDataAccess Disabled { get { return _disabledAttribute; }}

        /// <summary>
        /// Required. Specifies the name of the command/menu item, as shown to the user
        /// </summary>
        public IAttributeDataAccess Label {get { return _labelAttribute; }}

        /// <summary>
        /// 
        /// </summary>
        public IAttributeDataAccess Command { get { return _commandAttribute; }}


        /// <summary>
        /// Marks the command/menu item as being a default command
        /// </summary>
        public IAttributeDataAccess Default { get { return _defaultAttribute; }}

        /// <summary>
        /// Specifies an icon for the command/menu item
        /// </summary>
        public IAttributeDataAccess Icon { get { return _iconAttribute; }}

        /// <summary>
        /// Specifies the name of the group of commands that will be toggled when the command/menu item itself is toggled. Only for type="radio"
        /// </summary>
        public IAttributeDataAccess RadioGroup{ get { return _radioGroupAttribute; }}

        /// <summary>
        /// Specifies the type of command/menu item. Default is "command"
        /// </summary>
        public IAttributeDataAccess Type { get { return _menuItemTypeAttribute; }}

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

        public override List<IHTMLItem> SubElements()
        {
            return null;
        }
    }
}
