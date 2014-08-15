using System;
using System.Collections.Generic;
using System.Xml;
using System.Xml.Linq;
using HTMLClassLibrary.Attributes;
using HTMLClassLibrary.Attributes.FlaggedAttributes;
using HTMLClassLibrary.BaseElements.InlineElements;
using HTMLClassLibrary.BaseElements.Legends;

namespace HTMLClassLibrary.BaseElements.BlockElements
{
    /// <summary>
    /// The fieldset element adds structure to forms by grouping together related controls and labels.
    /// </summary>
    [HTMLItemAttribute(ElementName = "fieldset", SupportedStandards = HTMLElementType.HTML5 | HTMLElementType.XHTML11 | HTMLElementType.Transitional | HTMLElementType.Strict | HTMLElementType.FrameSet)]
    public class FieldSet : HTMLItem, IBlockElement
    {
        public FieldSet()
        {
            RegisterAttribute(_disabledAttribute);
            RegisterAttribute(_formIdAttribute);
            RegisterAttribute(_nameAttribute);
        }

        private readonly DisabledAttribute _disabledAttribute = new DisabledAttribute();
        private readonly FormIdAttribute _formIdAttribute = new FormIdAttribute();
        private readonly NameAttribute _nameAttribute = new NameAttribute();

        /// <summary>
        /// Specifies that a group of related form elements should be disabled
        /// </summary>
        public DisabledAttribute Disabled { get { return _disabledAttribute; }}

        /// <summary>
        /// Specifies one or more forms the fieldset belongs to
        /// </summary>
        public FormIdAttribute Form { get { return _formIdAttribute; }}

        /// <summary>
        /// Specifies a name for the fieldset
        /// </summary>
        public NameAttribute Name { get { return _nameAttribute; }}

        protected override bool IsValidSubType(IHTMLItem item)
        {
            if (item is IInlineItem)
            {
                return item.IsValid();
            }
            if (item is IBlockElement)
            {
                return item.IsValid();
            }
            if (item is Legend)
            {
                return item.IsValid();
            }
            if (item is SimpleHTML5Text)
            {
                return item.IsValid();
            }
            return false;
        }

        public override bool IsValid()
        {
            return true;
        }
    }
}
