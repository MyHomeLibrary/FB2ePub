using System;
using System.Collections.Generic;
using System.Xml;
using System.Xml.Linq;
using XHTMLClassLibrary.Attributes;
using XHTMLClassLibrary.Attributes.FlaggedAttributes;

namespace XHTMLClassLibrary.BaseElements.InlineElements
{
    /// <summary>
    /// 
    /// </summary>
    [HTMLItemAttribute(ElementName = "menuitem", SupportedStandards = HTMLElementType.HTML5 )]
    public class MenuItem : HTMLItem, IInlineItem
    {
        public MenuItem()
        {
            RegisterAttribute(_checkedAttribute);
            RegisterAttribute(_disabledAttribute);
            RegisterAttribute(_labelAttribute);
            RegisterAttribute(_commandAttribute);
            RegisterAttribute(_defaultAttribute);
            RegisterAttribute(_iconAttribute);
            RegisterAttribute(_radioGroupAttribute);
            RegisterAttribute(_menuItemTypeAttribute);
        }

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
