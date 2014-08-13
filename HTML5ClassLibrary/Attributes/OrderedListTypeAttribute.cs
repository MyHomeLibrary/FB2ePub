using System.Xml.Linq;

namespace HTML5ClassLibrary.Attributes
{
    /// <summary>
    /// The type attribute specifies the kind of marker to use in the list (letters or numbers).
    /// 1   Default. Decimal numbers (1, 2, 3, 4)
    /// a   Alphabetically ordered list, lowercase (a, b, c, d)
    /// A   Alphabetically ordered list, uppercase (A, B, C, D)
    /// i   Roman numbers, lowercase (i, ii, iii, iv)
    /// I   Roman numbers, uppercase (I, II, III, IV)
    /// </summary>
    public class OrderedListTypeAttribute : BaseAttribute
    {
        private enum OrderedListType
        {
            Invalid,
            Number,
            CapitalA,
            SmallA,
            CapitalI,
            SmallI,
        }

        private OrderedListType _type = OrderedListType.Invalid;

        private const string AttributeName = "type";

        #region Overrides of BaseAttribute

        public override string Value
        {
            get
            {
                switch (_type)
                {
                    case OrderedListType.Number:
                        return "1";
                    case OrderedListType.CapitalA:
                        return "A";
                    case OrderedListType.SmallA:
                        return "a";
                    case OrderedListType.CapitalI:
                        return "I";
                    case OrderedListType.SmallI:
                        return "i";
                }
                return string.Empty;
            }

            set
            {
                switch (value.ToLower())
                {
                    case "1":
                        _type = OrderedListType.Number;
                        break;
                    case "A":
                        _type = OrderedListType.CapitalA;
                        break;
                    case "a":
                        _type = OrderedListType.SmallA;
                        break;
                    case "I":
                        _type = OrderedListType.CapitalI;
                        break;
                    case "i":
                        _type = OrderedListType.SmallI;
                        break;
                    default:
                        _type = OrderedListType.Invalid;
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
