using System.Xml.Linq;

namespace XHTMLClassLibrary.Attributes
{
 public class YesNoAutoTypeAttribute : BaseAttribute
    {
        private enum ValuesTypes
        {
            Invalid,
            Auto,
            Yes,
            No
        }

        private ValuesTypes _value = ValuesTypes.Invalid;

        public override string Value
        {
            get
            {
                switch (_value)
                {
                    case ValuesTypes.Auto:
                        return "auto";
                    case ValuesTypes.Yes:
                        return "yes";
                    case ValuesTypes.No:
                        return "no ";
                }
                return string.Empty;
            }

            set
            {
                switch (value.ToLower())
                {
                    case "auto":
                        _value = ValuesTypes.Auto;
                        break;
                    case "yes":
                        _value = ValuesTypes.Yes;
                        break;
                    case "no":
                        _value = ValuesTypes.No;
                        break;
                    default:
                        _value = ValuesTypes.Invalid;
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
