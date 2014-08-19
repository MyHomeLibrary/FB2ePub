using System.Xml.Linq;
using XHTMLClassLibrary.AttributeDataTypes;

namespace XHTMLClassLibrary.Attributes
{
    /// <summary>
    /// The marginwidth attribute specifies the width between the content and the left and right sides of the frame, in pixels.
    /// </summary>
    public class MarginWidthAttribute : BaseAttribute
    {
        private Pixels _attrObject = new Pixels();

        private const string AttributeName = "marginwidth";

        #region Overrides of BaseAttribute

        public override void AddAttribute(XElement xElement)
        {
            if (!AttributeHasValue)
            {
                return;
            }
            xElement.Add(new XAttribute(AttributeName, _attrObject.Value));
        }

        public override void ReadAttribute(XElement element)
        {
            AttributeHasValue = false;
            _attrObject = null;
            XAttribute xObject = element.Attribute(AttributeName);
            if (xObject != null)
            {
                _attrObject = new Pixels { Value = xObject.Value };
                AttributeHasValue = true;
            }

        }

        public override string Value
        {
            get { return _attrObject.Value; }
            set
            {
                _attrObject.Value = value;
                AttributeHasValue = (value != string.Empty);
            }
        }
        #endregion

    }
}
