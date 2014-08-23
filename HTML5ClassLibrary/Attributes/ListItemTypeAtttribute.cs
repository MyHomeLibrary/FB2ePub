using System.Xml.Linq;

namespace XHTMLClassLibrary.Attributes
{
    /// <summary>
    /// For ordered lists
    /// The type attribute specifies the kind of marker to use in the list (letters or numbers).
    /// 1   Default. Decimal numbers (1, 2, 3, 4)
    /// a   Alphabetically ordered list, lowercase (a, b, c, d)
    /// A   Alphabetically ordered list, uppercase (A, B, C, D)
    /// i   Roman numbers, lowercase (i, ii, iii, iv)
    /// I   Roman numbers, uppercase (I, II, III, IV)
    /// For unordered lists
    /// disc    Default. A filled circle
    /// circle  An unfilled circle
    /// square  A filled square
    /// </summary>
    public class ListItemTypeAtttribute : BaseAttribute
    {
        private enum ListItemType
        {
            Invalid,
            Number,
            CapitalA,
            SmallA,
            CapitalI,
            SmallI,
            Disc,
            Square,
            Circle,
        }

        private ListItemType _type = ListItemType.Invalid;

        public override string Value
        {
            get
            {
                switch (_type)
                {
                    case ListItemType.Number:
                        return "1";
                    case ListItemType.CapitalA:
                        return "A";
                    case ListItemType.SmallA:
                        return "a";
                    case ListItemType.CapitalI:
                        return "I";
                    case ListItemType.SmallI:
                        return "i";
                    case ListItemType.Disc:
                        return "disc";
                    case ListItemType.Square:
                        return "square";
                    case ListItemType.Circle:
                        return "circle";
                }
                return string.Empty;
            }

            set
            {
                switch (value.ToLower())
                {
                    case "1":
                        _type = ListItemType.Number;
                        break;
                    case "A":
                        _type = ListItemType.CapitalA;
                        break;
                    case "a":
                        _type = ListItemType.SmallA;
                        break;
                    case "I":
                        _type = ListItemType.CapitalI;
                        break;
                    case "i":
                        _type = ListItemType.SmallI;
                        break;
                    case "disc":
                        _type = ListItemType.Disc;
                        break;
                    case "square":
                        _type = ListItemType.Square;
                        break;
                    case "circle":
                        _type = ListItemType.Circle;
                        break;
                    default:
                        _type = ListItemType.Invalid;
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
