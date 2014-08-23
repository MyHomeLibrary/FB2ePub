using System;
using System.Collections.Generic;
using System.Xml;
using System.Xml.Linq;
using XHTMLClassLibrary.Attributes;

namespace XHTMLClassLibrary.BaseElements.InlineElements
{
    /// <summary>
    /// 
    /// </summary>
    [HTMLItemAttribute(ElementName = "menuitem", SupportedStandards = HTMLElementType.HTML5 )]
    public class MenuItem : HTMLItem, IInlineItem
    {
        [AttributeTypeAttributeMember(Name = "checked", SupportedStandards = HTMLElementType.HTML5 | HTMLElementType.XHTML5)]
        private readonly FlagTypeAttribute _checkedAttribute = new FlagTypeAttribute();

        [AttributeTypeAttributeMember(Name = "command", SupportedStandards = HTMLElementType.HTML5 | HTMLElementType.XHTML5)]
        private readonly TextValueTypeAttribute _commandAttribute = new TextValueTypeAttribute();

        [AttributeTypeAttributeMember(Name = "default", SupportedStandards = HTMLElementType.HTML5 | HTMLElementType.XHTML5)]
        private readonly FlagTypeAttribute _defaultAttribute = new FlagTypeAttribute();

        [AttributeTypeAttributeMember(Name = "disabled", SupportedStandards = HTMLElementType.HTML5 | HTMLElementType.XHTML5)]
        private readonly FlagTypeAttribute _disabledAttribute = new FlagTypeAttribute();

        [AttributeTypeAttributeMember(Name = "icon", SupportedStandards = HTMLElementType.HTML5 | HTMLElementType.XHTML5)]
        private readonly URITypeAttribute _iconAttribute = new URITypeAttribute();

        [AttributeTypeAttributeMember(Name = "label", SupportedStandards = HTMLElementType.HTML5 | HTMLElementType.XHTML5)]
        private readonly TextValueTypeAttribute _labelAttribute = new TextValueTypeAttribute();

        [AttributeTypeAttributeMember(Name = "radiogroup", SupportedStandards = HTMLElementType.HTML5 | HTMLElementType.XHTML5)]
        private readonly URITypeAttribute _radioGroupAttribute = new URITypeAttribute();

        [AttributeTypeAttributeMember(Name = "type", SupportedStandards = HTMLElementType.HTML5 | HTMLElementType.XHTML5)]
        private readonly ContentTypeAttribute _menuItemTypeAttribute = new ContentTypeAttribute();

        /// <summary>
        /// Specifies that the command/menu item should be checked when the page loads. Only for type="radio" or type="checkbox"
        /// </summary>
        public FlagTypeAttribute Checked { get { return _checkedAttribute; }}

        /// <summary>
        /// Specifies that the command/menu item should be disabled
        /// </summary>
        public FlagTypeAttribute Disabled { get { return _disabledAttribute; }}

        /// <summary>
        /// Required. Specifies the name of the command/menu item, as shown to the user
        /// </summary>
        public TextValueTypeAttribute Label {get { return _labelAttribute; }}

        /// <summary>
        /// 
        /// </summary>
        public TextValueTypeAttribute Command { get { return _commandAttribute; }}


        /// <summary>
        /// Marks the command/menu item as being a default command
        /// </summary>
        public FlagTypeAttribute Default { get { return _defaultAttribute; }}

        /// <summary>
        /// Specifies an icon for the command/menu item
        /// </summary>
        public URITypeAttribute Icon { get { return _iconAttribute; }}

        /// <summary>
        /// Specifies the name of the group of commands that will be toggled when the command/menu item itself is toggled. Only for type="radio"
        /// </summary>
        public URITypeAttribute RadioGroup{ get { return _radioGroupAttribute; }}

        /// <summary>
        /// Specifies the type of command/menu item. Default is "command"
        /// </summary>
        public ContentTypeAttribute Type { get { return _menuItemTypeAttribute; }}

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
