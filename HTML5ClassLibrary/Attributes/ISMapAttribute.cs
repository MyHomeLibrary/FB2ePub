using System.Xml.Linq;
using HTML5ClassLibrary.AttributeDataTypes;

namespace HTML5ClassLibrary.Attributes
{
    public class ISMapAttribute : BaseAttribute
    {
        private Text _attrObject = new Text();

        private const string AttributeName = "ismap";

        #region Overrides of BaseAttribute

        /// <summary>
        /// Set the attribute to on/off (ismap or not)
        /// </summary>
        /// <param name="disabled"></param>
        public void SetISMap(bool disabled)
        {
            if (disabled)
            {
                _attrObject.Value = "ismap";
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
            xElement.Add(new XAttribute(AttributeName, _attrObject.Value));
        }

        public override void ReadAttribute(XElement element)
        {
            _hasValue = false;
            _attrObject = null;
            XAttribute xObject = element.Attribute(AttributeName);
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
        #endregion
    }
}
