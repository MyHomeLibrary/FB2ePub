using System.Xml.Linq;

namespace XHTMLClassLibrary.Attributes
{
    public class OnOffTypeAttribute : BaseAttribute
    {
        private enum PossibleValues
        {
            Invalid,
            On,
            Off,
        }

        private PossibleValues _value = PossibleValues.Invalid;

        public override string Value
        {
            get
            {
                switch (_value)
                {
                    case PossibleValues.On:
                        return "on";
                    case PossibleValues.Off:
                        return "off";
                }
                return string.Empty;
            }

            set
            {
                switch (value.ToLower())
                {
                    case "on":
                        _value = PossibleValues.On;
                        break;
                    case "off":
                        _value = PossibleValues.Off;
                        break;
                    default:
                        _value = PossibleValues.Invalid;
                        break;
                }
            }
        }


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
    }
}
