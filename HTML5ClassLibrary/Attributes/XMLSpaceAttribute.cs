using System.Xml.Linq;
using HTML5ClassLibrary.AttributeDataTypes;

namespace HTML5ClassLibrary.Attributes
{
    public class XMLSpaceAttribute : BaseAttribute
    {
        private const string AttributeName = "space";

        private Text _attrObject = new Text();

        /// <summary>
        /// Set the attribute to on/off (disabled or not)
        /// </summary>
        /// <param name="flag"></param>
        public void SetFlag(bool flag)
        {
            if (flag)
            {
                _attrObject.Value = "preserve";
                AttributeHasValue = true;
            }
            else
            {
                _attrObject.Value = string.Empty;
                AttributeHasValue = false;
            }
        }

        public override void AddAttribute(XElement xElement)
        {
            if (!AttributeHasValue)
            {
                return;
            }
            xElement.Add(new XAttribute(XNamespace.Xml + AttributeName, _attrObject.Value));
        }

        public override void ReadAttribute(XElement element)
        {
            AttributeHasValue = false;
            _attrObject = null;
            XAttribute xObject = element.Attribute(XNamespace.Xml + AttributeName);
            if (xObject != null)
            {
                _attrObject = new Text {Value = xObject.Value};
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
    }
}
