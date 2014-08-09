using System.Xml.Linq;

namespace HTML5ClassLibrary.Attributes
{
    public class ScopeAttribute : BaseAttribute
    {
        private enum ScopeEnum
        {
            Invalid,
            Row,
            Col,
            RowGroup,
            ColGroup,
        }

        public override string Value
        {
            get
            {
                switch (_scope)
                {
                    case ScopeEnum.Col:
                        return "col";
                    case ScopeEnum.ColGroup:
                        return "colgroup";
                    case ScopeEnum.RowGroup:
                        return "row";
                    case ScopeEnum.Row:
                        return "rowgroup";
                }
                return string.Empty;
            }

            set
            {
                switch (value.ToLower())
                {
                    case "col":
                        _scope = ScopeEnum.Col;
                        break;
                    case "colgroup":
                        _scope = ScopeEnum.ColGroup;
                        break;
                    case "rowgroup":
                        _scope = ScopeEnum.RowGroup;
                        break;
                    case "row":
                        _scope = ScopeEnum.Row;
                        break;
                    default:
                        _scope = ScopeEnum.Invalid;
                        break;
                }
            }
        }


        private ScopeEnum _scope = ScopeEnum.Invalid;

        private const string AttributeName = "scope";

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
