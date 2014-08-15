using System;
using System.Collections.Generic;
using System.Xml;
using System.Xml.Linq;
using HTMLClassLibrary.Attributes;
using HTMLClassLibrary.BaseElements.FormMenuOptions;
using HTMLClassLibrary.BaseElements.InlineElements;

namespace HTMLClassLibrary.BaseElements.BlockElements
{
    /// <summary>
    /// The form element is used to create data entry forms. 
    /// Data collected in the form is sent to the server for processing by server-side scripts such as PHP, ASP, etc.
    /// </summary>
    [HTMLItemAttribute(ElementName = "form", SupportedStandards = HTMLElementType.HTML5 | HTMLElementType.XHTML11 | HTMLElementType.Transitional | HTMLElementType.Strict | HTMLElementType.FrameSet)]
    public class Form : HTMLItem, IBlockElement
    {
        public Form()
        {
            RegisterAttribute(_acceptCharsetsAttribute);
            RegisterAttribute(_actionAttribute);
            RegisterAttribute(_autocompleteAttribute);
            RegisterAttribute(_encTypeAttribute);
            RegisterAttribute(_methodAttribute);
            RegisterAttribute(_nameAttribute);
            RegisterAttribute(_noValidateAttribute);
            RegisterAttribute(_formTargetAttribute);
        }

        // Basic attributes
        private readonly AcceptCharsetsAttribute _acceptCharsetsAttribute = new AcceptCharsetsAttribute();
        private readonly ActionAttribute _actionAttribute = new ActionAttribute();
        private readonly AutocompleteAttribute _autocompleteAttribute = new AutocompleteAttribute();
        private readonly FormEncodingTypeAttribute _encTypeAttribute = new FormEncodingTypeAttribute();
        private readonly MethodAttribute _methodAttribute = new MethodAttribute();
        private readonly NameAttribute _nameAttribute = new NameAttribute();
        private readonly NoValidateAttribute _noValidateAttribute = new NoValidateAttribute();
        private readonly FormTargetAttribute _formTargetAttribute = new FormTargetAttribute();



        /// <summary>
        /// Specifies the location of the server-side script used to process data collected in the form.
        /// </summary>
        public ActionAttribute Action { get { return _actionAttribute; } }

        /// <summary>
        /// Specifies whether a form should have autocomplete on or off
        /// </summary>
        public AutocompleteAttribute Autocomplete { get { return _autocompleteAttribute; }}

        /// <summary>
        /// Specifies the type of HTTP method used to send data to the server. 
        /// The default is get when the form data is sent to the server encoded into the URL specified in the action attribute. 
        /// Most forms use post when form data is sent to the server in the body of the HTTP message.
        /// </summary>
        public MethodAttribute Method { get { return _methodAttribute; } }

        /// <summary>
        /// Specifies the name of a form
        /// </summary>
        public NameAttribute Name { get { return _nameAttribute; }}

        /// <summary>
        /// This attribute specifies the list of character encodings for input data that are accepted by the server processing the form.
        /// </summary>
        public AcceptCharsetsAttribute AcceptCharsets { get { return _acceptCharsetsAttribute; } }


        /// <summary>
        /// This attribute specifies the content type used to send form data to the server when the value of method is post. 
        /// The default value for this attribute is "application/x-www-form-urlencoded". 
        /// If a form contains a file upload control (input element with type value of file), then this attribute value should be "multipart/form-data".
        /// </summary>
        public FormEncodingTypeAttribute EncType { get { return _encTypeAttribute; } }

        /// <summary>
        /// Specifies that the form should not be validated when submitted
        /// </summary>
        public NoValidateAttribute NoValidate { get { return _noValidateAttribute; }}

        /// <summary>
        /// Specifies where to display the response that is received after submitting the form
        /// </summary>
        public FormTargetAttribute Target{ get { return _formTargetAttribute; } }

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
