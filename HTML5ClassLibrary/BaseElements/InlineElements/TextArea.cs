using System.Collections.Generic;
using System.ComponentModel;
using XHTMLClassLibrary.AttributeDataTypes;
using XHTMLClassLibrary.Attributes;

namespace XHTMLClassLibrary.BaseElements.InlineElements
{
    /// <summary>
    /// The textarea element is used to create a multi-line text input form control.
    /// </summary>
    [HTMLItemAttribute(ElementName = "textarea", SupportedStandards = HTMLElementType.HTML5 |  HTMLElementType.XHTML5 | HTMLElementType.Transitional | HTMLElementType.Strict | HTMLElementType.FrameSet | HTMLElementType.XHTML11)]
    public class TextArea : HTMLItem, IInlineItem
    {
        #region Attribute_Values_Enums

        /// <summary>
        /// "wrap" attribute possible values
        /// </summary>
        public enum WrapAttributeOptions
        {
            [Description("hard")]
            Hard,

            [Description("soft")]
            Soft,
        }

        #endregion 


        [AttributeTypeAttributeMember(Name = "autofocus", SupportedStandards = HTMLElementType.HTML5 | HTMLElementType.XHTML5)]
        private readonly FlagTypeAttribute _autoFocusAttribute = new FlagTypeAttribute();

        [AttributeTypeAttributeMember(Name = "cols", SupportedStandards = HTMLElementType.HTML5 | HTMLElementType.XHTML5 | HTMLElementType.XHTML11 | HTMLElementType.Transitional | HTMLElementType.Strict | HTMLElementType.FrameSet)]
        private readonly SimpleSingleTypeAttribute<Number> _colsAttribute = new SimpleSingleTypeAttribute<Number>();

        [AttributeTypeAttributeMember(Name = "disabled", SupportedStandards = HTMLElementType.HTML5 | HTMLElementType.XHTML5 | HTMLElementType.XHTML11 | HTMLElementType.Transitional | HTMLElementType.Strict | HTMLElementType.FrameSet)]
        private readonly FlagTypeAttribute _disabledAttribute = new FlagTypeAttribute();

        [AttributeTypeAttributeMember(Name = "form", SupportedStandards = HTMLElementType.HTML5 | HTMLElementType.XHTML5)]
        private readonly SimpleSingleTypeAttribute<URI> _formIdAttribute = new SimpleSingleTypeAttribute<URI>();

        [AttributeTypeAttributeMember(Name = "maxlength", SupportedStandards = HTMLElementType.HTML5 | HTMLElementType.XHTML5)]
        private readonly SimpleSingleTypeAttribute<Number> _maxLengthAttribute = new SimpleSingleTypeAttribute<Number>();

        [AttributeTypeAttributeMember(Name = "name", SupportedStandards = HTMLElementType.HTML5 | HTMLElementType.XHTML5 | HTMLElementType.XHTML11 | HTMLElementType.Transitional | HTMLElementType.Strict | HTMLElementType.FrameSet)]
        private readonly SimpleSingleTypeAttribute<Text> _nameAttribute = new SimpleSingleTypeAttribute<Text>();

        [AttributeTypeAttributeMember(Name = "placeholder", SupportedStandards = HTMLElementType.HTML5 | HTMLElementType.XHTML5)]
        private readonly SimpleSingleTypeAttribute<Text> _placeHolderAttribute = new SimpleSingleTypeAttribute<Text>();

        [AttributeTypeAttributeMember(Name = "readonly", SupportedStandards = HTMLElementType.HTML5 | HTMLElementType.XHTML5 | HTMLElementType.XHTML11 | HTMLElementType.Transitional | HTMLElementType.Strict | HTMLElementType.FrameSet)]
        private readonly FlagTypeAttribute _readOnlyAttribute = new FlagTypeAttribute();

        [AttributeTypeAttributeMember(Name = "required", SupportedStandards = HTMLElementType.HTML5 | HTMLElementType.XHTML5)]
        private readonly FlagTypeAttribute _requiredAttribute = new FlagTypeAttribute();

        [AttributeTypeAttributeMember(Name = "rows", SupportedStandards = HTMLElementType.HTML5 | HTMLElementType.XHTML5 | HTMLElementType.XHTML11 | HTMLElementType.Transitional | HTMLElementType.Strict | HTMLElementType.FrameSet)]
        private readonly SimpleSingleTypeAttribute<Number> _rowsAttribute = new SimpleSingleTypeAttribute<Number>();

        [AttributeTypeAttributeMember(Name = "wrap", SupportedStandards = HTMLElementType.HTML5 | HTMLElementType.XHTML5)]
        private readonly ValuesSelectionTypeAttribute<Text> _wrapAttribute = new ValuesSelectionTypeAttribute<Text>(typeof(WrapAttributeOptions));




        /// <summary>
        /// Specifies that a text area should automatically get focus when the page loads
        /// </summary>
        public IAttributeDataAccess AutoFocus { get { return _autoFocusAttribute; }}

        /// <summary>
        /// Specifies that a text area should be disabled
        /// </summary>
        public IAttributeDataAccess Disabled { get { return _disabledAttribute; } }

        /// <summary>
        /// Specifies one or more forms the text area belongs to
        /// </summary>
        public IAttributeDataAccess Form { get { return _formIdAttribute; } }

        /// <summary>
        /// Specifies the maximum number of characters allowed in the text area
        /// </summary>
        public IAttributeDataAccess MaxLength { get { return _maxLengthAttribute; } }

        /// <summary>
        /// Specifies a short hint that describes the expected value of a text area
        /// </summary>
        public IAttributeDataAccess PlaceHolder { get { return _placeHolderAttribute; } }

        /// <summary>
        /// Specifies that a text area should be read-only
        /// </summary>
        public IAttributeDataAccess ReadOnly { get { return _readOnlyAttribute; } }

        /// <summary>
        /// Specifies that a text area is required/must be filled out
        /// </summary>
        public IAttributeDataAccess Required { get { return _requiredAttribute; } }

        /// <summary>
        /// Specifies how the text in a text area is to be wrapped when submitted in a form
        /// </summary>
        public IAttributeDataAccess Wrap { get { return _wrapAttribute; } }

        /// <summary>
        /// This attribute specifies the visible width in average character widths. 
        /// This attribute is required.
        /// </summary>
        public IAttributeDataAccess Cols { get { return _colsAttribute; } }

        /// <summary>
        /// Form control name.
        /// </summary>
        public IAttributeDataAccess Name { get { return _nameAttribute; } }

        /// <summary>
        /// This attribute specifies the number of visible text lines. 
        /// This attribute is required.
        /// </summary>
        public IAttributeDataAccess Rows { get { return _rowsAttribute; } }

 
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
