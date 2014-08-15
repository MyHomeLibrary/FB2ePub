using System;
using System.Collections.Generic;
using System.Xml;
using System.Xml.Linq;
using HTMLClassLibrary.Attributes;
using HTMLClassLibrary.Attributes.FlaggedAttributes;
using HTMLClassLibrary.BaseElements.FormMenuOptions;
using HTMLClassLibrary.Exceptions;

namespace HTMLClassLibrary.BaseElements.InlineElements
{
    /// <summary>
    /// The select element is used to create an option selector form control which most Web browsers render as a listbox control. 
    /// The list of values for this control is created using option elements. 
    /// These values can be grouped together using the optgroup element.
    /// </summary>
    [HTMLItemAttribute(ElementName = "select", SupportedStandards = HTMLElementType.HTML5 | HTMLElementType.Transitional | HTMLElementType.Strict | HTMLElementType.FrameSet | HTMLElementType.XHTML11)]    
    public class Select : HTMLItem, IInlineItem
    {
        /// <summary>
        /// Internal content of the element
        /// </summary>
        private readonly List<IHTMLItem> _content = new List<IHTMLItem>();

        public Select()
        {
            RegisterAttribute(_multipleAttribute);
            RegisterAttribute(_nameAttribute);
            RegisterAttribute(_sizeAttribute);
            RegisterAttribute(_autoFocusAttribute);
            RegisterAttribute(_formIdAttribute);
            RegisterAttribute(_requiredAttribute);
            RegisterAttribute(_disabledAttribute);
         
        }

        private readonly MultipleAttribute _multipleAttribute = new MultipleAttribute();
        private readonly NameAttribute _nameAttribute = new NameAttribute();
        private readonly SizeAttribute _sizeAttribute = new SizeAttribute();
        private readonly DisabledAttribute _disabledAttribute = new DisabledAttribute();
        private readonly AutoFocusAttribute _autoFocusAttribute = new AutoFocusAttribute();
        private readonly FormIdAttribute _formIdAttribute = new FormIdAttribute();
        private readonly RequiredAttribute _requiredAttribute = new RequiredAttribute();


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



        public override bool IsValid()
        {
            // at least one of sub elements have to appear
            return (_content.Count > 0);
        }

        public override List<IHTMLItem> SubElements()
        {
            return _content;
        }

        protected override bool IsValidSubType(IHTMLItem item)
        {
            if (item is IOptionItem)
            {
                return item.IsValid();
            }
            return false;
        }
    }
}
