using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml.Linq;
using HTML5ClassLibrary.AttributeDataTypes;

namespace HTML5ClassLibrary.Attributes
{
    /// <summary>
    /// The srcdoc attribute specifies the HTML content of the page to show in the inline frame.
    /// Tip: This attribute is expected to be used together with the sandbox and seamless attributes.
    /// If a browser supports the srcdoc attribute, it will override the content specified in the src attribute (if present).
    /// If a browser does NOT support the srcdoc attribute, it will show the file specified in the src attribute instead (if present).
    /// </summary>
    public class SourceDocAttribute : BaseAttribute
    {
        private HTMLCode _attrObject = new HTMLCode();

        private const string AttributeName = "srcdoc";

        #region Overrides of BaseAttribute

        public override void AddAttribute(XElement xElement)
        {
            if (!AttributeHasValue)
            {
                return;
            }
            xElement.Add(new XAttribute(AttributeName, _attrObject.Value));
        }

        public override void ReadAttribute(XElement element)
        {
            AttributeHasValue = false;
            _attrObject = null;
            XAttribute xObject = element.Attribute(AttributeName);
            if (xObject != null)
            {
                _attrObject = new HTMLCode { Value = xObject.Value };
                AttributeHasValue = true;
            }
        }

        public override string Value
        {
            get { return _attrObject.Value; }
            set
            {
                _attrObject.Value = value;
                AttributeHasValue = (value != string.Empty);
            }
        }
        #endregion

    }
}
