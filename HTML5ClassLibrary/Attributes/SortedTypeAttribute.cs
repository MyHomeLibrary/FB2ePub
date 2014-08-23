using System.Xml.Linq;

namespace XHTMLClassLibrary.Attributes
{
    public class SortedTypeAttribute : BaseAttribute
    {
        private enum SortedValues
        {
            Invalid,
            Reversed,
            Number,
            NumberReversed,
            ReversedNumber,
        }

        private SortedValues _sorted = SortedValues.Invalid;

        public override string Value
        {
            get
            {
                switch (_sorted)
                {
                    case SortedValues.Reversed:
                        return "reversed";
                    case SortedValues.Number:
                        return "number";
                    case SortedValues.NumberReversed:
                        return "number reversed";
                    case SortedValues.ReversedNumber:
                        return "reversed number";
                }
                return string.Empty;
            }

            set
            {
                switch (value.ToLower())
                {
                    case "reversed":
                        _sorted = SortedValues.Reversed;
                        break;
                    case "number":
                        _sorted = SortedValues.Number;
                        break;
                    case "number reversed":
                        _sorted = SortedValues.NumberReversed;
                        break;
                    case "reversed number":
                        _sorted = SortedValues.ReversedNumber;
                        break;
                    default:
                        _sorted = SortedValues.Invalid;
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
