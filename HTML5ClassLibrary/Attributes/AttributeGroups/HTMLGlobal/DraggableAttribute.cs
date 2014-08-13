using System.Xml.Linq;

namespace HTML5ClassLibrary.Attributes.AttributeGroups.HTMLGlobal
{
    /// <summary>
    /// The draggable attribute specifies whether an element is draggable or not.
    /// Tip: Links and images are draggable by default.
    /// </summary>
    public class DraggableAttribute : BaseAttribute
    {
        private enum DraggableValues
        {
            Invalid,
            True,
            False,
            Auto,
        }

        private DraggableValues _draggable = DraggableValues.Invalid;

        private const string AttributeName = "draggable";

        #region Overrides of BaseAttribute


        public override string Value
        {
            get
            {
                switch (_draggable)
                {
                    case DraggableValues.True:
                        return "true";
                    case DraggableValues.False:
                        return "false";
                    case DraggableValues.Auto:
                        return "auto";
                }
                return string.Empty;
            }

            set
            {
                switch (value.ToLower())
                {
                    case "true":
                        _draggable = DraggableValues.True;
                        break;
                    case "false":
                        _draggable = DraggableValues.False;
                        break;
                    case "auto":
                        _draggable = DraggableValues.Auto;
                        break;
                    default:
                        _draggable = DraggableValues.Invalid;
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
