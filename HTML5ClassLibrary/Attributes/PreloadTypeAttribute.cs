using System.Xml.Linq;

namespace XHTMLClassLibrary.Attributes
{
    public class PreloadTypeAttribute : BaseAttribute
    {
        private enum PreloadValues
        {
            Invalid,
            Auto,
            Metadata,
            None,
        }

        private PreloadValues _preload = PreloadValues.Invalid;

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

        public override string Value
        {
            get
            {
                switch (_preload)
                {
                    case PreloadValues.Auto:
                        return "auto";
                    case PreloadValues.Metadata:
                        return "metadata";
                    case PreloadValues.None:
                        return "none";
                }
                return string.Empty;
            }

            set
            {
                switch (value.ToLower())
                {
                    case "auto":
                        _preload = PreloadValues.Auto;
                        break;
                    case "metadata":
                        _preload = PreloadValues.Metadata;
                        break;
                    case "none":
                        _preload = PreloadValues.None;
                        break;
                    default:
                        _preload = PreloadValues.Invalid;
                        break;
                }
            }
        }

    }
}
