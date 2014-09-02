using XHTMLClassLibrary.AttributeDataTypes;
using XHTMLClassLibrary.Attributes;
using XHTMLClassLibrary.BaseElements.FormMenuOptions;

namespace XHTMLClassLibrary.BaseElements.InlineElements
{
    /// <summary>
    /// The select element is used to create an option selector form control which most Web browsers render as a listbox control. 
    /// The list of values for this control is created using option elements. 
    /// These values can be grouped together using the optgroup element.
    /// </summary>
    [HTMLItemAttribute(ElementName = "select", SupportedStandards = HTMLElementType.HTML5 |  HTMLElementType.XHTML5 | HTMLElementType.Transitional | HTMLElementType.Strict | HTMLElementType.FrameSet | HTMLElementType.XHTML11)]    
    public class Select : HTMLItem, IInlineItem
    {
        [AttributeTypeAttributeMember(SupportedStandards = HTMLElementType.HTML5 | HTMLElementType.XHTML5)]
        private readonly FlagTypeAttribute _autoFocusAttribute = new FlagTypeAttribute("autofocus");

        [AttributeTypeAttributeMember(SupportedStandards = HTMLElementType.HTML5 | HTMLElementType.XHTML5 | HTMLElementType.XHTML11 | HTMLElementType.Transitional | HTMLElementType.Strict | HTMLElementType.FrameSet)]
        private readonly FlagTypeAttribute _disabledAttribute = new FlagTypeAttribute("disabled");

        [AttributeTypeAttributeMember(SupportedStandards = HTMLElementType.HTML5 | HTMLElementType.XHTML5)]
        private readonly SimpleSingleTypeAttribute<URI> _formIdAttribute = new SimpleSingleTypeAttribute<URI>("form");

        [AttributeTypeAttributeMember(SupportedStandards = HTMLElementType.HTML5 | HTMLElementType.XHTML5 | HTMLElementType.XHTML11 | HTMLElementType.Transitional | HTMLElementType.Strict | HTMLElementType.FrameSet)]
        private readonly FlagTypeAttribute _multipleAttribute = new FlagTypeAttribute("multiple");

        [AttributeTypeAttributeMember(SupportedStandards = HTMLElementType.HTML5 | HTMLElementType.XHTML5 | HTMLElementType.XHTML11 | HTMLElementType.Transitional | HTMLElementType.Strict | HTMLElementType.FrameSet)]
        private readonly SimpleSingleTypeAttribute<Text> _nameAttribute = new SimpleSingleTypeAttribute<Text>("name");

        [AttributeTypeAttributeMember(SupportedStandards = HTMLElementType.HTML5 | HTMLElementType.XHTML5)]
        private readonly FlagTypeAttribute _requiredAttribute = new FlagTypeAttribute("required");

        [AttributeTypeAttributeMember(SupportedStandards = HTMLElementType.HTML5 | HTMLElementType.XHTML5 | HTMLElementType.XHTML11 | HTMLElementType.Transitional | HTMLElementType.Strict | HTMLElementType.FrameSet)]
        private readonly SimpleSingleTypeAttribute<Number> _sizeAttribute = new SimpleSingleTypeAttribute<Number>("size");


        public Select(HTMLElementType htmlStandard) : base(htmlStandard)
        {
        }

        /// <summary>
        /// If set, this attribute allows multiple selections. 
        /// Possible value is multiple.
        /// </summary>
        public IAttributeDataAccess Multiple { get { return _multipleAttribute; } }

        /// <summary>
        /// Form control name.
        /// </summary>
        public IAttributeDataAccess Name { get { return _nameAttribute; } }


        /// <summary>
        /// Number of rows in the list that should be visible at the same time.
        /// </summary>
        public IAttributeDataAccess Size { get { return _sizeAttribute; } }

        /// <summary>
        /// Disables the control for user input. 
        /// Possible value is disabled.
        /// </summary>
        public IAttributeDataAccess Disabled { get { return _disabledAttribute; } }

        /// <summary>
        /// Specifies that the drop-down list should automatically get focus when the page loads
        /// </summary>
        public IAttributeDataAccess Autofocus { get { return _autoFocusAttribute; }}

        /// <summary>
        /// Defines one or more forms the select field belongs to
        /// </summary>
        public IAttributeDataAccess Form { get { return _formIdAttribute; }}

        /// <summary>
        /// Specifies that the user is required to select a value before submitting the form
        /// </summary>
        public IAttributeDataAccess Required { get { return _requiredAttribute; }}



        public override bool IsValid()
        {
            // at least one of sub elements have to appear
            return (Subitems.Count > 0);
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
