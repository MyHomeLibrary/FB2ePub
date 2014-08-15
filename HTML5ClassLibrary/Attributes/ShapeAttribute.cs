using System.Xml.Linq;

namespace HTMLClassLibrary.Attributes
{
    public class ShapeAttribute : BaseAttribute
    {
        private enum ShapeEnum
        {
            Invalid,
            Rect,
            Circle,
            Poly,
            Default,
        }

        private ShapeEnum _shape = ShapeEnum.Invalid;

        private const string AttributeName = "shape";

        #region Overrides of BaseAttribute

        public override string Value
        {
            get
            {
                switch (_shape)
                {
                    case ShapeEnum.Circle:
                        return "circle";
                    case ShapeEnum.Default:
                        return "default";
                    case ShapeEnum.Poly:
                        return "poly";
                    case ShapeEnum.Rect:
                        return "rect";
                }
                return string.Empty;
            }

            set
            {
                switch (value.ToLower())
                {
                    case "circle":
                        _shape = ShapeEnum.Circle;
                        break;
                    case "default":
                        _shape = ShapeEnum.Default;
                        break;
                    case "poly":
                        _shape = ShapeEnum.Poly;
                        break;
                    case "rect":
                        _shape = ShapeEnum.Rect;
                        break;
                    default:
                        _shape = ShapeEnum.Invalid;
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
