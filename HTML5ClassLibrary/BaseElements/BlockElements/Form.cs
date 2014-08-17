using System;
using System.Collections.Generic;
using System.Xml;
using System.Xml.Linq;
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
        [AttributeTypeAttributeMember(SupportedStandards = HTMLElementType.XHTML11 | HTMLElementType.Transitional | HTMLElementType.Strict | HTMLElementType.FrameSet)]
        private readonly AcceptAttribute _acceptAttribute = new AcceptAttribute();

        [AttributeTypeAttributeMember(SupportedStandards = HTMLElementType.HTML5 | HTMLElementType.XHTML5 | HTMLElementType.XHTML11 | HTMLElementType.Transitional | HTMLElementType.Strict | HTMLElementType.FrameSet)]
        private readonly AcceptCharsetsAttribute _acceptCharsetsAttribute = new AcceptCharsetsAttribute();

        [AttributeTypeAttributeMember(SupportedStandards = HTMLElementType.HTML5 | HTMLElementType.XHTML5 | HTMLElementType.XHTML11 | HTMLElementType.Transitional | HTMLElementType.Strict | HTMLElementType.FrameSet)]
        private readonly ActionAttribute _actionAttribute = new ActionAttribute();

        [AttributeTypeAttributeMember(SupportedStandards = HTMLElementType.HTML5 | HTMLElementType.XHTML5)]
        private readonly AutocompleteAttribute _autocompleteAttribute = new AutocompleteAttribute();

        [AttributeTypeAttributeMember(SupportedStandards = HTMLElementType.HTML5 | HTMLElementType.XHTML5 | HTMLElementType.XHTML11 | HTMLElementType.Transitional | HTMLElementType.Strict | HTMLElementType.FrameSet)]
        private readonly FormEncodingTypeAttribute _encTypeAttribute = new FormEncodingTypeAttribute();

        [AttributeTypeAttributeMember(SupportedStandards = HTMLElementType.HTML5 | HTMLElementType.XHTML5 | HTMLElementType.XHTML11 | HTMLElementType.Transitional | HTMLElementType.Strict | HTMLElementType.FrameSet)]
        private readonly MethodAttribute _methodAttribute = new MethodAttribute();

        [AttributeTypeAttributeMember(SupportedStandards = HTMLElementType.HTML5 | HTMLElementType.XHTML5 | HTMLElementType.XHTML11 | HTMLElementType.Transitional | HTMLElementType.Strict | HTMLElementType.FrameSet)]
        private readonly NameAttribute _nameAttribute = new NameAttribute();

        [AttributeTypeAttributeMember(SupportedStandards = HTMLElementType.HTML5 | HTMLElementType.XHTML5)]
        private readonly NoValidateAttribute _noValidateAttribute = new NoValidateAttribute();

        [AttributeTypeAttributeMember(SupportedStandards = HTMLElementType.HTML5 | HTMLElementType.XHTML5 | HTMLElementType.XHTML11 | HTMLElementType.Transitional | HTMLElementType.Strict | HTMLElementType.FrameSet)]
        private readonly FormTargetAttribute _formTargetAttribute = new FormTargetAttribute();




        /// <summary>
        /// Specifies a comma-separated list of file types  that the server accepts (that can be submitted through the file upload)
        /// Not supported in HTML5.
        /// </summary>
        public AcceptAttribute Accept { get { return _acceptAttribute; }}

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
