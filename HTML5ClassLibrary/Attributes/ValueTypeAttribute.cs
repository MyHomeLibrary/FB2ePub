using System.Xml.Linq;

namespace HTML5ClassLibrary.Attributes
{
    public class ValueTypeAttribute : BaseAttribute
    {
        private enum ValueTypeEnum
        {
            Invalid,
            Data,
            Ref,
            Object,
        }

        public override string Value
        {
            get
            {
                switch (_type)
                {
                    case ValueTypeEnum.Ref:
                        return "ref";
                    case ValueTypeEnum.Object:
                        return "object";
                    case ValueTypeEnum.Data:
                        return "data";
                }
                return string.Empty;
            }

            set
            {
                switch (value.ToLower())
                {
                    case "ref":
                        _type = ValueTypeEnum.Ref;
                        break;
                    case "object":
                        _type = ValueTypeEnum.Object;
                        break;
                    case "data":
                        _type = ValueTypeEnum.Data;
                        break;
                    default:
                        _type = ValueTypeEnum.Invalid;
                        break;
                }
            }
        }


        private ValueTypeEnum _type = ValueTypeEnum.Invalid;

        private const string AttributeName = "valuetype";

        #region Overrides of BaseAttribute

        public override void AddAttribute(XElement xElement)
        {
            if (!_hasValue)
            {
                return;
            }
            xElement.Add(new XAttribute(AttributeName, Value));
        }

        public override void ReadAttribute(XElement element)
        {
            _hasValue = false;
            XAttribute xObject = element.Attribute(AttributeName);
            if (xObject != null)
            {
                Value = xObject.Value;
            }
        }

        #endregion

    }
}
