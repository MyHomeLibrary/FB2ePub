using System.Xml.Linq;

namespace HTML5ClassLibrary.Attributes.AttributeGroups.HTMLGlobal
{
    /// <summary>
    /// The translate attribute specifies whether the content of an element should be translated or not.
    /// </summary>
    public class TranslateAttribute : BaseAttribute
    {
        private enum TranslateValues
        {
            Invalid,
            Yes,
            No,
        }

        private TranslateValues _translate = TranslateValues.Invalid;

        private const string AttributeName = "translate";

        #region Overrides of BaseAttribute


        public override string Value
        {
            get
            {
                switch (_translate)
                {
                    case TranslateValues.Yes:
                        return "yes";
                    case TranslateValues.No:
                        return "no";
                }
                return string.Empty;
            }

            set
            {
                switch (value.ToLower())
                {
                    case "yes":
                        _translate = TranslateValues.Yes;
                        break;
                    case "no":
                        _translate = TranslateValues.No;
                        break;
                    default:
                        _translate = TranslateValues.Invalid;
                        break;
                }
            }
        }


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
