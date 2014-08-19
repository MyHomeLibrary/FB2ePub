using System.Xml.Linq;

namespace XHTMLClassLibrary.Attributes
{
    /// <summary>
    /// The noresize attribute specifies that a "frame" element cannot be resized by the user.
    /// By default, each "frame" in a "frameset" can be resized by dragging the border between the frames. However, this attribute locks the size of a frame.
    /// </summary>
    public class NoResizeAttribute : BaseAttribute
    {
        private enum NoResizeType
        {
            Invalid,
            Present,
        }

        private NoResizeType _noresizeValue = NoResizeType.Invalid;

        private const string AttributeName = "noresize";

        public override string Value
        {
            get
            {
                switch (_noresizeValue)
                {
                    case NoResizeType.Present:
                        return "noresize";
                }
                return string.Empty;
            }

            set
            {
                switch (value.ToLower())
                {
                    case "noresize":
                        _noresizeValue = NoResizeType.Present;
                        break;
                    default:
                        _noresizeValue = NoResizeType.Invalid;
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
