using System.Collections.Generic;
using System.ComponentModel;
using XHTMLClassLibrary.AttributeDataTypes;
using XHTMLClassLibrary.Attributes;

namespace XHTMLClassLibrary.BaseElements.InlineElements
{
    /// <summary>
    /// The "input" tag specifies an input field where the user can enter data.
    /// "input" elements are used within a "form" element to declare input controls that allow users to input data.
    /// An input field can vary in many ways, depending on the type attribute.
    /// </summary>
    [HTMLItemAttribute(ElementName = "input", SupportedStandards = HTMLElementType.HTML5 |  HTMLElementType.XHTML5 | HTMLElementType.XHTML11 | HTMLElementType.Transitional | HTMLElementType.Strict | HTMLElementType.FrameSet)]    
    public class Input : HTMLItem, IInlineItem
    {

        #region Attribute_Values_Enums


        /// <summary>
        /// "align" attribute possible values
        /// </summary>
        public enum AlignAttributeOptions
        {
            [Description("baseline")]
            Baseline,

            [Description("bottom")]
            Bottom,

            [Description("left")]
            Left,

            [Description("middle")]
            Middle,

            [Description("right")]
            Right,

            [Description("top")]
            Top,
        }


        /// <summary>
        /// "autocomplete" attribute possible values
        /// </summary>
        public enum AutocompleteAttributeOptions
        {
            [Description("on")]
            On,

            [Description("off")]
            Off,
        }


        /// <summary>
        /// "enctype" attribute possible values
        /// </summary>
        public enum EncodingTypeAttributeOptions
        {
            [Description(@"application/x-www-form-urlencoded")]
            ApplicationURLEnocoded,

            [Description(@"multipart/form-data")]
            MultipartFormData,

            [Description(@"text/plain")]
            TextPlain,           
        }

        /// <summary>
        /// "formmethod" attribute possible values
        /// </summary>
        public enum FormMethodAttributeOptions
        {
            [Description("get")]
            Get,

            [Description("post")]
            Post,
        }

        /// <summary>
        /// "type" attribute possible values
        /// </summary>
        public enum InputTypeAttributeOptions
        {
            [Description("button")]
            Button,

            [Description("checkbox")]
            Checkbox,

            [Description("color")]
            Color,

            [Description("date")]
            Date,

            [Description("datetime")]
            DateTime,

            [Description("datetime-local")]
            DateTimeLocal,

            [Description("email")]
            EMail,

            [Description("file")]
            File,

            [Description("hidden")]
            Hidden,

            [Description("image")]
            Image,

            [Description("month")]
            Month,

            [Description("number")]
            Number,

            [Description("password")]
            Password,

            [Description("radio")]
            Radio,

            [Description("reset")]
            Reset,

            [Description("range")]
            Range,

            [Description("submit")]
            Submit,

            [Description("search")]
            Search,

            [Description("text")]
            Text,

            [Description("tel")]
            Tel,

            [Description("time")]
            Time,

            [Description("url")]
            Url,

            [Description("week")]
            Week,
        }


        #endregion


        [AttributeTypeAttributeMember(Name = "accept", SupportedStandards = HTMLElementType.HTML5 | HTMLElementType.XHTML5 | HTMLElementType.XHTML11 | HTMLElementType.Transitional | HTMLElementType.Strict | HTMLElementType.FrameSet)]
        private readonly SimpleSingleTypeAttribute<ContentType> _acceptAttribute = new SimpleSingleTypeAttribute<ContentType>();

         [AttributeTypeAttributeMember(Name = "align", SupportedStandards = HTMLElementType.XHTML11 | HTMLElementType.Transitional | HTMLElementType.Strict | HTMLElementType.FrameSet)]
        private readonly ValuesSelectionTypeAttribute<Text> _alignAttribute = new ValuesSelectionTypeAttribute<Text>(typeof(AlignAttributeOptions));

        [AttributeTypeAttributeMember(Name = "alt", SupportedStandards = HTMLElementType.HTML5 | HTMLElementType.XHTML5 | HTMLElementType.XHTML11 | HTMLElementType.Transitional | HTMLElementType.Strict | HTMLElementType.FrameSet)]
         private readonly SimpleSingleTypeAttribute<Text> _altAttribute = new SimpleSingleTypeAttribute<Text>();

        [AttributeTypeAttributeMember(Name = "autocomplete", SupportedStandards = HTMLElementType.HTML5 | HTMLElementType.XHTML5)]
        private readonly ValuesSelectionTypeAttribute<Text> _autocompleteAttribute = new ValuesSelectionTypeAttribute<Text>(typeof(AutocompleteAttributeOptions));

        [AttributeTypeAttributeMember(Name = "autofocus", SupportedStandards = HTMLElementType.HTML5 | HTMLElementType.XHTML5)]
        private readonly FlagTypeAttribute _autoFocusAttribute = new FlagTypeAttribute();

        [AttributeTypeAttributeMember(Name = "checked", SupportedStandards = HTMLElementType.HTML5 | HTMLElementType.XHTML5 | HTMLElementType.XHTML11 | HTMLElementType.Transitional | HTMLElementType.Strict | HTMLElementType.FrameSet)]
        private readonly FlagTypeAttribute _checkedAttribute = new FlagTypeAttribute();

        [AttributeTypeAttributeMember(Name = "disabled", SupportedStandards = HTMLElementType.HTML5 | HTMLElementType.XHTML5 | HTMLElementType.XHTML11 | HTMLElementType.Transitional | HTMLElementType.Strict | HTMLElementType.FrameSet)]
        private readonly FlagTypeAttribute _disabledAttribute = new FlagTypeAttribute();

        [AttributeTypeAttributeMember(Name = "form", SupportedStandards = HTMLElementType.HTML5 | HTMLElementType.XHTML5)]
        private readonly SimpleSingleTypeAttribute<URI> _formIdAttribute = new SimpleSingleTypeAttribute<URI>();

        [AttributeTypeAttributeMember(Name = "formaction", SupportedStandards = HTMLElementType.HTML5 | HTMLElementType.XHTML5)]
        private readonly SimpleSingleTypeAttribute<URI> _formActionAttribute = new SimpleSingleTypeAttribute<URI>();

        [AttributeTypeAttributeMember(Name = "enctype", SupportedStandards = HTMLElementType.HTML5 | HTMLElementType.XHTML5)]
        private readonly ValuesSelectionTypeAttribute<Text> _formEncodingTypeAttribute = new ValuesSelectionTypeAttribute<Text>(typeof(EncodingTypeAttributeOptions));

        [AttributeTypeAttributeMember(Name = "formmethod", SupportedStandards = HTMLElementType.HTML5 | HTMLElementType.XHTML5)]
        private readonly ValuesSelectionTypeAttribute<Text> _formMethodAttribute = new ValuesSelectionTypeAttribute<Text>(typeof(FormMethodAttributeOptions));

        [AttributeTypeAttributeMember(Name = "formnovalidate", SupportedStandards = HTMLElementType.HTML5 | HTMLElementType.XHTML5)]
        private readonly FlagTypeAttribute _formNoValidateAttribute = new FlagTypeAttribute();

        [AttributeTypeAttributeMember(Name = "formtarget", SupportedStandards = HTMLElementType.HTML5 | HTMLElementType.XHTML5)]
        private readonly SimpleSingleTypeAttribute<TargetType> _formTargetAttribute = new SimpleSingleTypeAttribute<TargetType>();

        [AttributeTypeAttributeMember(Name = "height", SupportedStandards = HTMLElementType.HTML5 | HTMLElementType.XHTML5)]
        private readonly SimpleSingleTypeAttribute<Length> _heightAttribute = new SimpleSingleTypeAttribute<Length>();
      
        [AttributeTypeAttributeMember(Name = "list", SupportedStandards = HTMLElementType.HTML5 | HTMLElementType.XHTML5)]
        private readonly SimpleSingleTypeAttribute<URI> _listAttribute = new SimpleSingleTypeAttribute<URI>();

        [AttributeTypeAttributeMember(Name = "max", SupportedStandards = HTMLElementType.HTML5 | HTMLElementType.XHTML5)]
        private readonly SimpleSingleTypeAttribute<Text> _maxAttribute = new SimpleSingleTypeAttribute<Text>();

        [AttributeTypeAttributeMember(Name = "maxlength", SupportedStandards = HTMLElementType.HTML5 | HTMLElementType.XHTML5)]
        private readonly SimpleSingleTypeAttribute<Number> _maxLenghtAttribute = new SimpleSingleTypeAttribute<Number>();

        [AttributeTypeAttributeMember(Name = "min", SupportedStandards = HTMLElementType.HTML5 | HTMLElementType.XHTML5)]
        private readonly SimpleSingleTypeAttribute<Text> _minAttribute = new SimpleSingleTypeAttribute<Text>();

        [AttributeTypeAttributeMember(Name = "multiple", SupportedStandards = HTMLElementType.HTML5 | HTMLElementType.XHTML5)]
        private readonly FlagTypeAttribute _multipleAttribute = new FlagTypeAttribute();

        [AttributeTypeAttributeMember(Name = "name", SupportedStandards = HTMLElementType.HTML5 | HTMLElementType.XHTML5 | HTMLElementType.XHTML11 | HTMLElementType.Transitional | HTMLElementType.Strict | HTMLElementType.FrameSet)]
        private readonly SimpleSingleTypeAttribute<Text> _nameAttribute = new SimpleSingleTypeAttribute<Text>();

        [AttributeTypeAttributeMember(Name = "pattern", SupportedStandards = HTMLElementType.HTML5 | HTMLElementType.XHTML5)]
        private readonly SimpleSingleTypeAttribute<PatternAttributeType> _pattern = new SimpleSingleTypeAttribute<PatternAttributeType>();

        [AttributeTypeAttributeMember(Name = "placeholder", SupportedStandards = HTMLElementType.HTML5 | HTMLElementType.XHTML5)]
        private readonly SimpleSingleTypeAttribute<Text> _placeHolderAttribute = new SimpleSingleTypeAttribute<Text>();

        [AttributeTypeAttributeMember(Name = "readonly", SupportedStandards = HTMLElementType.HTML5 | HTMLElementType.XHTML5 | HTMLElementType.XHTML11 | HTMLElementType.Transitional | HTMLElementType.Strict | HTMLElementType.FrameSet)]
        private readonly FlagTypeAttribute _readonlyAttribute = new FlagTypeAttribute();

        [AttributeTypeAttributeMember(Name = "required", SupportedStandards = HTMLElementType.HTML5 | HTMLElementType.XHTML5)]
        private readonly FlagTypeAttribute _requiredAttribute = new FlagTypeAttribute();

        [AttributeTypeAttributeMember(Name = "size", SupportedStandards = HTMLElementType.HTML5 | HTMLElementType.XHTML5 | HTMLElementType.XHTML11 | HTMLElementType.Transitional | HTMLElementType.Strict | HTMLElementType.FrameSet)]
        private readonly SimpleSingleTypeAttribute<Number> _sizeAttribute = new SimpleSingleTypeAttribute<Number>();

        [AttributeTypeAttributeMember(Name = "src", SupportedStandards = HTMLElementType.HTML5 | HTMLElementType.XHTML5 | HTMLElementType.XHTML11 | HTMLElementType.Transitional | HTMLElementType.Strict | HTMLElementType.FrameSet)]
        private readonly SimpleSingleTypeAttribute<URI> _srcAttribute = new SimpleSingleTypeAttribute<URI>();

        [AttributeTypeAttributeMember(Name = "step", SupportedStandards = HTMLElementType.HTML5 | HTMLElementType.XHTML5)]
        private readonly SimpleSingleTypeAttribute<Text> _stepAttribute = new SimpleSingleTypeAttribute<Text>();

        [AttributeTypeAttributeMember(Name = "type", SupportedStandards = HTMLElementType.HTML5 | HTMLElementType.XHTML5 | HTMLElementType.XHTML11 | HTMLElementType.Transitional | HTMLElementType.Strict | HTMLElementType.FrameSet)]
        private readonly ValuesSelectionTypeAttribute<Text> _inputTypeAttribute = new ValuesSelectionTypeAttribute<Text>(typeof(InputTypeAttributeOptions));

        [AttributeTypeAttributeMember(Name = "value", SupportedStandards = HTMLElementType.HTML5 | HTMLElementType.XHTML5 | HTMLElementType.XHTML11 | HTMLElementType.Transitional | HTMLElementType.Strict | HTMLElementType.FrameSet)]
        private readonly SimpleSingleTypeAttribute<Text> _valueAttribute = new SimpleSingleTypeAttribute<Text>();

        [AttributeTypeAttributeMember(Name = "width", SupportedStandards = HTMLElementType.HTML5 | HTMLElementType.XHTML5)]
        private readonly SimpleSingleTypeAttribute<Length> _widthAttribute = new SimpleSingleTypeAttribute<Length>();


        /// <summary>
        /// Alternate text for controls of the type image.
        /// </summary>
        public IAttributeDataAccess Alt
        {
            get { return _altAttribute; }
        }

        /// <summary>
        /// Specifies whether an "input" element should have autocomplete enabled
        /// </summary>
        public IAttributeDataAccess Autocomplete
        {
            get { return _autocompleteAttribute; }
        }

        /// <summary>
        /// Specifies that an "input" element should automatically get focus when the page loads
        /// </summary>
        public IAttributeDataAccess Autofocus
        {
            get { return _autoFocusAttribute; }
        }

        /// <summary>
        /// Form control name.
        /// </summary>
        public IAttributeDataAccess Name
        {
            get { return _nameAttribute; }
        }

        /// <summary>
        /// Specifies a regular expression that an "input" element's value is checked against
        /// </summary>
        public IAttributeDataAccess Pattern { get { return _pattern; } }

        /// <summary>
        /// Specifies a short hint that describes the expected value of an "input" element
        /// </summary>
        public IAttributeDataAccess Placeholder { get { return _placeHolderAttribute; } }

        /// <summary>
        /// When the type attribute has the value radio or checkbox, 
        /// this attribute specifies that the radio/checkbox is selected.
        /// </summary>
        public IAttributeDataAccess Checked
        {
            get { return _checkedAttribute; }
        }

        /// <summary>
        /// When the type attribute has the value text or password, this attribute specifies the maximum number of characters the user may enter. 
        /// This number should not exceed the value specified in the size attribute.
        /// </summary>
        public IAttributeDataAccess MaxLength
        {
            get { return _maxLenghtAttribute; }
        }

        /// <summary>
        /// Specifies a minimum value for an "input" element
        /// </summary>
        public IAttributeDataAccess Min { get { return _minAttribute; } }


        /// <summary>
        /// Specifies that a user can enter more than one value in an "input" element
        /// </summary>
        public IAttributeDataAccess Multiple { get { return _multipleAttribute; } }

        /// <summary>
        /// This attribute tells the Web browser the initial width of the control. 
        /// The width is given in pixels except when the type attribute has the value text or password. 
        /// In such cases, its value is the number of characters.
        /// </summary>
        public IAttributeDataAccess Size
        {
            get { return _sizeAttribute; }
        }

        /// <summary>
        /// Type of control to create.
        /// </summary>
        public IAttributeDataAccess InputType
        {
            get { return _inputTypeAttribute; }
        }


        /// <summary>
        /// This attribute specifies a comma-separated list of content types that a server processing this form will handle correctly.
        /// </summary>
        public IAttributeDataAccess Accept { get { return _acceptAttribute; } }



        /// <summary>
        ///  Specifies the alignment of an image input (only for type="image")
        /// Not supported in HTML5.
        /// </summary>
        public IAttributeDataAccess Align { get { return _alignAttribute; } }


        /// <summary>
        /// Disables the control for user input. Possible value is disabled.
        /// </summary>
        public IAttributeDataAccess Disabled { get { return _disabledAttribute; } }


        /// <summary>
        /// Specifies one or more forms the "input" element belongs to
        /// </summary>
        public IAttributeDataAccess Form { get { return _formIdAttribute; } }


        /// <summary>
        /// Specifies the URL of the file that will process the input control when the form is submitted (for type="submit" and type="image")
        /// </summary>
        public IAttributeDataAccess FormAction { get { return _formActionAttribute; } }


        /// <summary>
        /// Specifies how the form-data should be encoded when submitting it to the server (for type="submit" and type="image")
        /// </summary>
        public IAttributeDataAccess FormEncType { get { return _formEncodingTypeAttribute; } }


        /// <summary>
        /// Defines that form elements should not be validated when submitted
        /// </summary>
        public IAttributeDataAccess FormNoValidate { get { return _formNoValidateAttribute; } }

        /// <summary>
        /// Specifies where to display the response that is received after submitting the form (for type="submit" and type="image")
        /// </summary>
        public IAttributeDataAccess FormTarget { get { return _formTargetAttribute; } }

        /// <summary>
        /// Defines the HTTP method for sending data to the action URL (for type="submit" and type="image")
        /// </summary>
        public IAttributeDataAccess FormMethod { get { return _formMethodAttribute; } }

        /// <summary>
        /// Specifies the height of an "input" element (only for type="image")
        /// </summary>
        public IAttributeDataAccess Height { get { return _heightAttribute; } }

        /// <summary>
        /// Specifies the width of an "input" element (only for type="image")
        /// </summary>
        public IAttributeDataAccess Width { get { return _widthAttribute; } }


        /// <summary>
        /// Refers to a "datalist" element that contains pre-defined options for an "input" element
        /// </summary>
        public IAttributeDataAccess List { get { return _listAttribute; } } 

        /// <summary>
        /// Specifies the maximum value for an "input" element
        /// </summary>
        public IAttributeDataAccess Max { get { return _maxAttribute; } }

        /// <summary>
        /// If present, this attribute prohibits changes to the value in the control. 
        /// Possible value is "readonly".
        /// </summary>
        public IAttributeDataAccess ReadOnly { get { return _readonlyAttribute; } }

        /// <summary>
        /// Specifies that an input field must be filled out before submitting the form
        /// </summary>
        public IAttributeDataAccess Required { get { return _requiredAttribute; } }

        /// <summary>
        /// When the type attribute has the value image, 
        /// this attribute specifies the location of the image to be used to decorate the graphical submit button.
        /// </summary>
        public IAttributeDataAccess Src { get { return _srcAttribute; } }

        /// <summary>
        /// Specifies the legal number intervals for an input field
        /// </summary>
        public IAttributeDataAccess Step { get { return _stepAttribute; } }

        /// <summary>
        /// Value associated with a control.
        /// </summary>
        public IAttributeDataAccess Value { get { return _valueAttribute; } }


        /// <summary>
        /// Checks it element data is valid
        /// </summary>
        /// <returns>
        /// true if valid
        /// </returns>
        public override bool IsValid()
        {
            return true;
        }

        public override List<IHTMLItem> SubElements()
        {
            return null;
        }
    }
}
