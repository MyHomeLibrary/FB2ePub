using System.Xml.Linq;

namespace HTMLClassLibrary.Attributes
{
    public class ControlsAttribute : BaseAttribute
    {
        private const string AttributeName = "controls";
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
