using System.Xml.Linq;

namespace XHTMLClassLibrary.Attributes
{
    public class BooleanAutoTypeAttribute : BaseAttribute
    {
        private enum PossibleValues
        {
            Invalid,
            True,
            False,
            Auto,
        }

        private PossibleValues _value = PossibleValues.Invalid;


        public override string Value
        {
            get
            {
                switch (_value)
                {
                    case PossibleValues.True:
                        return "true";
                    case PossibleValues.False:
                        return "false";
                    case PossibleValues.Auto:
                        return "auto";
                }
                return string.Empty;
            }

            set
            {
                switch (value.ToLower())
                {
                    case "true":
                        _value = PossibleValues.True;
                        break;
                    case "false":
                        _value = PossibleValues.False;
                        break;
                    case "auto":
                        _value = PossibleValues.Auto;
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
