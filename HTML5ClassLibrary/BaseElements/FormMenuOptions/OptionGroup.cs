using System;
using System.Collections.Generic;
using System.Xml;
using System.Xml.Linq;
using XHTMLClassLibrary.Attributes;
using XHTMLClassLibrary.Attributes.FlaggedAttributes;
using XHTMLClassLibrary.Exceptions;

namespace XHTMLClassLibrary.BaseElements.FormMenuOptions
{
    /// <summary>
    /// The "optgroup" element is used to group the choices offered in select form controls. 
    /// Users find it easier to work with long lists if related sections are grouped together. 
    /// </summary>
    [HTMLItemAttribute(ElementName = "optgroup", SupportedStandards = HTMLElementType.HTML5 |  HTMLElementType.XHTML5 | HTMLElementType.Transitional | HTMLElementType.Strict | HTMLElementType.FrameSet | HTMLElementType.XHTML11)]
    public class OptionGroup : HTMLItem, IOptionItem
    {
        public OptionGroup()
        {
            RegisterAttribute(_labelAttribute);
            RegisterAttribute(_disabledAttribute);
        }


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



        public override bool IsValid()
        {
            return true;
        }


        protected override bool IsValidSubType(IHTMLItem item)
        {
            if (item is Option)
            {
                return item.IsValid();
            }
            return false;
        }
    }
}
