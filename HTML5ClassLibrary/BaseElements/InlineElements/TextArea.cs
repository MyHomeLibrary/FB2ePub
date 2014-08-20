using System;
using System.Collections.Generic;
using System.Xml;
using System.Xml.Linq;
using XHTMLClassLibrary.Attributes;
using XHTMLClassLibrary.Attributes.FlaggedAttributes;

namespace XHTMLClassLibrary.BaseElements.InlineElements
{
    /// <summary>
    /// The textarea element is used to create a multi-line text input form control.
    /// </summary>
    [HTMLItemAttribute(ElementName = "textarea", SupportedStandards = HTMLElementType.HTML5 |  HTMLElementType.XHTML5 | HTMLElementType.Transitional | HTMLElementType.Strict | HTMLElementType.FrameSet | HTMLElementType.XHTML11)]
    public class TextArea : HTMLItem, IInlineItem
    {
        [AttributeTypeAttributeMember(Name = "autofocus", SupportedStandards = HTMLElementType.HTML5 | HTMLElementType.XHTML5)]
        private readonly FlagAttribute _autoFocusAttribute = new FlagAttribute();

        [AttributeTypeAttributeMember(SupportedStandards = HTMLElementType.HTML5 | HTMLElementType.XHTML5 | HTMLElementType.XHTML11 | HTMLElementType.Transitional | HTMLElementType.Strict | HTMLElementType.FrameSet)]
        private readonly ColsAttribute _colsAttribute = new ColsAttribute();

        [AttributeTypeAttributeMember(Name = "disabled", SupportedStandards = HTMLElementType.HTML5 | HTMLElementType.XHTML5 | HTMLElementType.XHTML11 | HTMLElementType.Transitional | HTMLElementType.Strict | HTMLElementType.FrameSet)]
        private readonly FlagAttribute _disabledAttribute = new FlagAttribute();

        [AttributeTypeAttributeMember(SupportedStandards = HTMLElementType.HTML5 | HTMLElementType.XHTML5)]
        private readonly FormIdAttribute _formIdAttribute = new FormIdAttribute();

        [AttributeTypeAttributeMember(SupportedStandards = HTMLElementType.HTML5 | HTMLElementType.XHTML5)]
        private readonly MaxLengthAttribute _maxLengthAttribute = new MaxLengthAttribute();

        [AttributeTypeAttributeMember(SupportedStandards = HTMLElementType.HTML5 | HTMLElementType.XHTML5 | HTMLElementType.XHTML11 | HTMLElementType.Transitional | HTMLElementType.Strict | HTMLElementType.FrameSet)]
        private readonly NameAttribute _nameAttribute = new NameAttribute();

        [AttributeTypeAttributeMember(SupportedStandards = HTMLElementType.HTML5 | HTMLElementType.XHTML5)]
        private readonly PlaceHolderAttribute _placeHolderAttribute = new PlaceHolderAttribute();

        [AttributeTypeAttributeMember(Name = "readonly", SupportedStandards = HTMLElementType.HTML5 | HTMLElementType.XHTML5 | HTMLElementType.XHTML11 | HTMLElementType.Transitional | HTMLElementType.Strict | HTMLElementType.FrameSet)]
        private readonly FlagAttribute _readOnlyAttribute = new FlagAttribute();

        [AttributeTypeAttributeMember(Name = "required", SupportedStandards = HTMLElementType.HTML5 | HTMLElementType.XHTML5)]
        private readonly FlagAttribute _requiredAttribute = new FlagAttribute();

        [AttributeTypeAttributeMember(SupportedStandards = HTMLElementType.HTML5 | HTMLElementType.XHTML5 | HTMLElementType.XHTML11 | HTMLElementType.Transitional | HTMLElementType.Strict | HTMLElementType.FrameSet)]
        private readonly RowsAttribute _rowsAttribute = new RowsAttribute();

        [AttributeTypeAttributeMember(SupportedStandards = HTMLElementType.HTML5 | HTMLElementType.XHTML5)]
        private readonly WrapAttribute _wrapAttribute = new WrapAttribute();




        /// <summary>
        /// Specifies that a text area should automatically get focus when the page loads
        /// </summary>
        public FlagAttribute AutoFocus { get { return _autoFocusAttribute; }}

        /// <summary>
        /// Specifies that a text area should be disabled
        /// </summary>
        public FlagAttribute Disabled { get { return _disabledAttribute; }}

        /// <summary>
        /// Specifies one or more forms the text area belongs to
        /// </summary>
        public FormIdAttribute Form { get { return _formIdAttribute; }}

        /// <summary>
        /// Specifies the maximum number of characters allowed in the text area
        /// </summary>
        public MaxLengthAttribute MaxLength { get { return _maxLengthAttribute; }}

        /// <summary>
        /// Specifies a short hint that describes the expected value of a text area
        /// </summary>
        public PlaceHolderAttribute PlaceHolder { get { return _placeHolderAttribute; }}

        /// <summary>
        /// Specifies that a text area should be read-only
        /// </summary>
        public FlagAttribute ReadOnly { get { return _readOnlyAttribute; }}

        /// <summary>
        /// Specifies that a text area is required/must be filled out
        /// </summary>
        public FlagAttribute Required { get { return _requiredAttribute; }}

        /// <summary>
        /// Specifies how the text in a text area is to be wrapped when submitted in a form
        /// </summary>
        public WrapAttribute Wrap { get { return _wrapAttribute; }}

        /// <summary>
        /// This attribute specifies the visible width in average character widths. 
        /// This attribute is required.
        /// </summary>
        public ColsAttribute Cols { get { return _colsAttribute; } }

        /// <summary>
        /// Form control name.
        /// </summary>
        public NameAttribute Name { get { return _nameAttribute; } }

        /// <summary>
        /// This attribute specifies the number of visible text lines. 
        /// This attribute is required.
        /// </summary>
        public RowsAttribute Rows { get { return _rowsAttribute; } }

 
        public override bool IsValid()
        {
            return (_colsAttribute.HasValue() && _rowsAttribute.HasValue());
        }

        public override List<IHTMLItem> SubElements()
        {
            return null;
        }
    }
}
