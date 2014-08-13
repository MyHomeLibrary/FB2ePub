using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml.Linq;

namespace HTML5ClassLibrary.Attributes
{
    public class SortedAttribute : BaseAttribute
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

        private const string AttributeName = "sorted";

        #region Overrides of BaseAttribute

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
            xElement.Add(new XAttribute(AttributeName, Value));
        }

        public override void ReadAttribute(XElement element)
        {
            AttributeHasValue = false;
            XAttribute xObject = element.Attribute(AttributeName);
            if (xObject != null)
            {
                Value = xObject.Value;
            }
        }

        #endregion

    }
}
