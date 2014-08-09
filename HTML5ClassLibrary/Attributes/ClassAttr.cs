using System.Xml.Linq;
using HTML5ClassLibrary.AttributeDataTypes;

namespace HTML5ClassLibrary.Attributes
{
    public class ClassAttr : BaseAttribute
    {
        private NameTokens _attrObject  = new NameTokens();

        private const string AttributeName = "class";

        #region Overrides of BaseAttribute

        public override void AddAttribute(XElement xElement)
        {
            if (!_hasValue)
            {
                return;
            }
            xElement.Add(new XAttribute(AttributeName, _attrObject.Value));
        }

        public override void ReadAttribute(XElement element)
        {
            _hasValue = false;
            _attrObject = null;
            XAttribute xClass = element.Attribute(AttributeName);
            if (xClass != null)
            {
                _attrObject = new NameTokens {Value = xClass.Value};
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
        #endregion
    }
}
