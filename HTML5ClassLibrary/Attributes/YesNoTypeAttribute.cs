using System.Xml.Linq;

namespace XHTMLClassLibrary.Attributes
{
    public class YesNoTypeAttribute : BaseAttribute
    {
        private enum SelectionValues
        {
            Invalid,
            Yes,
            No,
        }

        private SelectionValues _selection = SelectionValues.Invalid;

        public override string Value
        {
            get
            {
                switch (_selection)
                {
                    case SelectionValues.Yes:
                        return "yes";
                    case SelectionValues.No:
                        return "no";
                }
                return string.Empty;
            }

            set
            {
                switch (value.ToLower())
                {
                    case "yes":
                        _selection = SelectionValues.Yes;
                        break;
                    case "no":
                        _selection = SelectionValues.No;
                        break;
                    default:
                        _selection = SelectionValues.Invalid;
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
