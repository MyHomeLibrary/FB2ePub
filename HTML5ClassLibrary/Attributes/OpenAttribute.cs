using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml.Linq;

namespace XHTMLClassLibrary.Attributes
{
    public class OpenAttribute : BaseAttribute
    {
        private const string AttributeName = "open";

        public override void AddAttribute(XElement xElement)
        {
            if (!AttributeHasValue)
            {
                return;
            }
            xElement.Add(new XAttribute(AttributeName, ""));
        }

        public override void ReadAttribute(XElement element)
        {
            AttributeHasValue = false;
            XAttribute xObject = element.Attribute(AttributeName);
            if (xObject != null)
            {
                AttributeHasValue = true;
            }

        }

        public override string Value
        {
            get { return AttributeHasValue ? AttributeName : string.Empty; }
            set
            {
                AttributeHasValue = (value == string.Empty);
            }
        }

    }
}
