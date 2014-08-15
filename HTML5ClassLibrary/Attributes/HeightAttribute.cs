using System.Xml.Linq;
using HTMLClassLibrary.AttributeDataTypes;

namespace HTMLClassLibrary.Attributes
{
    public class HeightAttribute : BaseAttribute
    {
        private Length _attrObject = new Length();

        private const string AttributeName = "height";

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
                _attrObject = new Length {Value = xObject.Value};
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
