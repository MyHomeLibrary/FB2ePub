using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml.Linq;

namespace HTML5ClassLibrary.Attributes
{
    public class FormNoValidateAttribute : BaseAttribute
    {
        private const string AttributeName = "formnovalidate";
        public override void AddAttribute(XElement xElement)
        {
            if (!_hasValue)
            {
                return;
            }
            xElement.Add(new XAttribute(AttributeName, ""));
        }

        public override void ReadAttribute(XElement element)
        {
            _hasValue = false;
            XAttribute xObject = element.Attribute(AttributeName);
            if (xObject != null)
            {
                _hasValue = true;
            }

        }

        public override string Value
        {
            get { return _hasValue ? AttributeName : string.Empty; }
            set
            {
                _hasValue = (value == string.Empty);
            }
        }

    }
}
