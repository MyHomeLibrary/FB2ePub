using System.Xml.Linq;
using XHTMLClassLibrary.AttributeDataTypes;

namespace XHTMLClassLibrary.Attributes
{
    public class CharsetAttribute : BaseAttribute
    {
        private Charset _attrObject = new Charset();

        private const string AttributeName = "charset";

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
                _attrObject = new Charset {Value = xObject.Value};
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
