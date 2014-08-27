using System.ComponentModel;
using XHTMLClassLibrary.AttributeDataTypes;
using XHTMLClassLibrary.Attributes;
using XHTMLClassLibrary.BaseElements.FormMenuOptions;
using XHTMLClassLibrary.BaseElements.InlineElements;

namespace XHTMLClassLibrary.BaseElements.BlockElements
{
    /// <summary>
    /// The form element is used to create data entry forms. 
    /// Data collected in the form is sent to the server for processing by server-side scripts such as PHP, ASP, etc.
    /// </summary>
    [HTMLItemAttribute(ElementName = "form", SupportedStandards = HTMLElementType.HTML5 |  HTMLElementType.XHTML5 | HTMLElementType.XHTML11 | HTMLElementType.Transitional | HTMLElementType.Strict | HTMLElementType.FrameSet)]
    public class Form : HTMLItem, IBlockElement
    {

        #region Attribute_Values_Enums

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
            Application,

            [Description(@"multipart/form-data")]
            Multipart,

            [Description(@"text/plain")]
            Text,
        }

        /// <summary>
        /// "method" attribute possible values
        /// </summary>
        public enum MethodAttributeOptions
        {
            [Description("get")]
            Get,

            [Description("post")]
            Post,
        }

        #endregion


        [AttributeTypeAttributeMember(Name = "accept", SupportedStandards = HTMLElementType.XHTML11 | HTMLElementType.Transitional | HTMLElementType.Strict | HTMLElementType.FrameSet)]
        private readonly SimpleSingleTypeAttribute<ContentType> _acceptAttribute = new SimpleSingleTypeAttribute<ContentType>();

        [AttributeTypeAttributeMember(Name = "accept-charsets", SupportedStandards = HTMLElementType.HTML5 | HTMLElementType.XHTML5 | HTMLElementType.XHTML11 | HTMLElementType.Transitional | HTMLElementType.Strict | HTMLElementType.FrameSet)]
        private readonly SimpleSingleTypeAttribute<Charsets> _acceptCharsetsAttribute = new SimpleSingleTypeAttribute<Charsets>();

        [AttributeTypeAttributeMember(Name = "action", SupportedStandards = HTMLElementType.HTML5 | HTMLElementType.XHTML5 | HTMLElementType.XHTML11 | HTMLElementType.Transitional | HTMLElementType.Strict | HTMLElementType.FrameSet)]
        private readonly SimpleSingleTypeAttribute<URI> _actionAttribute = new SimpleSingleTypeAttribute<URI>();

        [AttributeTypeAttributeMember(Name = "autocomplete", SupportedStandards = HTMLElementType.HTML5 | HTMLElementType.XHTML5)]
        private readonly ValuesSelectionTypeAttribute<Text> _autocompleteAttribute = new ValuesSelectionTypeAttribute<Text>(typeof(AutocompleteAttributeOptions));

        [AttributeTypeAttributeMember(Name = "enctype", SupportedStandards = HTMLElementType.HTML5 | HTMLElementType.XHTML5 | HTMLElementType.XHTML11 | HTMLElementType.Transitional | HTMLElementType.Strict | HTMLElementType.FrameSet)]
        private readonly ValuesSelectionTypeAttribute<Text> _encTypeAttribute = new ValuesSelectionTypeAttribute<Text>(typeof(EncodingTypeAttributeOptions));

        [AttributeTypeAttributeMember(Name = "method", SupportedStandards = HTMLElementType.HTML5 | HTMLElementType.XHTML5 | HTMLElementType.XHTML11 | HTMLElementType.Transitional | HTMLElementType.Strict | HTMLElementType.FrameSet)]
        private readonly ValuesSelectionTypeAttribute<Text> _methodAttribute = new ValuesSelectionTypeAttribute<Text>(typeof(MethodAttributeOptions));

        [AttributeTypeAttributeMember(Name = "name", SupportedStandards = HTMLElementType.HTML5 | HTMLElementType.XHTML5 | HTMLElementType.XHTML11 | HTMLElementType.Transitional | HTMLElementType.Strict | HTMLElementType.FrameSet)]
        private readonly SimpleSingleTypeAttribute<Text> _nameAttribute = new SimpleSingleTypeAttribute<Text>();

        [AttributeTypeAttributeMember(Name = "novalidate", SupportedStandards = HTMLElementType.HTML5 | HTMLElementType.XHTML5)]
        private readonly FlagTypeAttribute _noValidateAttribute = new FlagTypeAttribute();

        [AttributeTypeAttributeMember(Name = "target", SupportedStandards = HTMLElementType.HTML5 | HTMLElementType.XHTML5 | HTMLElementType.XHTML11 | HTMLElementType.Transitional | HTMLElementType.Strict | HTMLElementType.FrameSet)]
        private readonly SimpleSingleTypeAttribute<TargetType> _formTargetAttribute = new SimpleSingleTypeAttribute<TargetType>();




        /// <summary>
        /// Specifies a comma-separated list of file types  that the server accepts (that can be submitted through the file upload)
        /// Not supported in HTML5.
        /// </summary>
        public IAttributeDataAccess Accept { get { return _acceptAttribute; }}

        /// <summary>
        /// Specifies the location of the server-side script used to process data collected in the form.
        /// </summary>
        public IAttributeDataAccess Action { get { return _actionAttribute; } }

        /// <summary>
        /// Specifies whether a form should have autocomplete on or off
        /// </summary>
        public IAttributeDataAccess Autocomplete { get { return _autocompleteAttribute; } }

        /// <summary>
        /// Specifies the type of HTTP method used to send data to the server. 
        /// The default is get when the form data is sent to the server encoded into the URL specified in the action attribute. 
        /// Most forms use post when form data is sent to the server in the body of the HTTP message.
        /// </summary>
        public IAttributeDataAccess Method { get { return _methodAttribute; } }

        /// <summary>
        /// Specifies the name of a form
        /// </summary>
        public IAttributeDataAccess Name { get { return _nameAttribute; } }

        /// <summary>
        /// This attribute specifies the list of character encodings for input data that are accepted by the server processing the form.
        /// </summary>
        public IAttributeDataAccess AcceptCharsets { get { return _acceptCharsetsAttribute; } }


        /// <summary>
        /// This attribute specifies the content type used to send form data to the server when the value of method is post. 
        /// The default value for this attribute is "application/x-www-form-urlencoded". 
        /// If a form contains a file upload control (input element with type value of file), then this attribute value should be "multipart/form-data".
        /// </summary>
        public IAttributeDataAccess EncType { get { return _encTypeAttribute; } }

        /// <summary>
        /// Specifies that the form should not be validated when submitted
        /// </summary>
        public IAttributeDataAccess NoValidate { get { return _noValidateAttribute; } }

        /// <summary>
        /// Specifies where to display the response that is received after submitting the form
        /// </summary>
        public IAttributeDataAccess Target { get { return _formTargetAttribute; } }

        #region Overrides of IBlockElement

        protected override bool IsValidSubType(IHTMLItem item)
        {
            if (
                !(item is Input) &&
                !(item is TextArea) &&
                !(item is Button) &&
                !(item is Select) &&
                !(item is Option) &&
                !(item is OptionGroup) &&
                !(item is Label) &&
                !(item is FieldSet))
            {
                return false;
            }
            return item.IsValid();
        }


        /// <summary>
        /// Checks it element data is valid
        /// </summary>
        /// <returns>true if valid</returns>
        public override bool IsValid()
        {
            return true;
        }

        #endregion
    }
}
