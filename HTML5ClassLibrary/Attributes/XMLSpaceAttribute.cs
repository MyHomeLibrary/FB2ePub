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
                _hasValue = true;
            }
            else
            {
                _attrObject.Value = string.Empty;
                _hasValue = false;
            }
        }

        public override void AddAttribute(XElement xElement)
        {
            if (!_hasValue)
            {
                return;
            }
            xElement.Add(new XAttribute(XNamespace.Xml + AttributeName, _attrObject.Value));
        }

        public override void ReadAttribute(XElement element)
        {
            _hasValue = false;
            _attrObject = null;
            XAttribute xObject = element.Attribute(XNamespace.Xml + AttributeName);
            if (xObject != null)
            {
                _attrObject = new Text {Value = xObject.Value};
                _hasValue = true;
            }
        }

        public override string Value
        {
            get { return _attrObject.Value; }
            set
            {
                _attrObject.Value = value;
                _hasValue = (value != string.Empty);
            }
        }
    }
}
