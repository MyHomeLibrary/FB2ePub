using System.Xml.Linq;

namespace XHTMLClassLibrary.Attributes
{
    /// <summary>
    /// The frameborder attribute specifies whether or not to display a border around a frame.
    /// Tip: For practical reasons, it may be better not to specify borders, and use CSS to apply border styles and color instead
    /// </summary>
    public class FrameBorderAttribute : BaseAttribute
    {
        private enum BorderPresence
        {
            Invalid,
            Absent,
            Present,
        }

        private BorderPresence _borderPresence = BorderPresence.Invalid;

        private const string AttributeName = "frameborder";

        public override string Value
        {
            get
            {
                switch (_borderPresence)
                {
                    case BorderPresence.Absent:
                        return "0";
                    case BorderPresence.Present:
                        return "1";
                }
                return string.Empty;
            }

            set
            {
                switch (value.ToLower())
                {
                    case "0":
                        _borderPresence = BorderPresence.Absent;
                        break;
                    case "1":
                        _borderPresence = BorderPresence.Present;
                        break;
                    default:
                        _borderPresence = BorderPresence.Invalid;
                        break;
                }
            }
        }


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
