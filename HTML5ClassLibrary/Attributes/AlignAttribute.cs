using System.Xml.Linq;

namespace XHTMLClassLibrary.Attributes
{
    public class AlignAttribute : BaseAttribute
    {
        private enum Align
        {
            Invalid,
            Left,
            Center,
            Right,
            Justify,
            Char,
        }

        public override string Value
        {
            get
            {
                switch (_align)
                {
                    case Align.Center:
                        return "center";
                    case Align.Justify:
                        return "justify";
                    case Align.Right:
                        return "right";
                    case Align.Left:
                        return "left";
                    case Align.Char:
                        return "char";
                }
                return string.Empty;
            }

            set
            {
                switch (value.ToLower())
                {
                    case "center":
                        _align = Align.Center;
                        break;
                    case "justify":
                        _align = Align.Justify;
                        break;
                    case "right":
                        _align = Align.Right;
                        break;
                    case "left":
                        _align = Align.Left;
                        break;
                    case "char":
                        _align = Align.Char;
                        break;
                    default:
                        _align = Align.Invalid;
                        break;
                }
            }
        }


        private Align _align = Align.Invalid;

        private  const string AttributeName = "align";

        #region Overrides of BaseAttribute

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
