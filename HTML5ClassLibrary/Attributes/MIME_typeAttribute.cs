using System.Xml.Linq;

namespace XHTMLClassLibrary.Attributes
{
    // TODO: make it hold real mime types, not string
    public class MIMETypeAttribute : BaseAttribute
    {
        public override void AddAttribute(XElement xElement)
        {
            if (!AttributeHasValue)
            {
                return;
            }
            xElement.Add(new XAttribute(GetAttributeName(), Value));
        }

        public override void ReadAttribute(XElement element)
        {
            AttributeHasValue = false;
            XAttribute xObject = element.Attribute(GetAttributeName());
            if (xObject != null)
            {
                Value = xObject.Value;
            }
        }

        public override string Value { get; set; }

    }
}
