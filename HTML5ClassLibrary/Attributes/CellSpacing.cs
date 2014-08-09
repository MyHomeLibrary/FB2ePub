using System.Xml.Linq;
using HTML5ClassLibrary.AttributeDataTypes;

namespace HTML5ClassLibrary.Attributes
{
    public class CellSpacingAttribute : BaseAttribute
    {
        private Length _attrObject = new Length();

        private const string AttributeName = "cellspacing";

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
            XAttribute xObject = element.Attribute(AttributeName);
            if (xObject != null)
            {
                _attrObject = new Length {Value = xObject.Value};
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
