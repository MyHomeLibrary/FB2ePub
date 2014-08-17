using System.Xml.Linq;
using XHTMLClassLibrary.AttributeDataTypes;

namespace XHTMLClassLibrary.Attributes
{
    public class DownloadAttribute : BaseAttribute
    {
        private URI _attrObject = new URI();

        private const string AttributeName = "download";

        #region Overrides of BaseAttribute

        public override void AddAttribute(XElement xElement)
        {
            if (!AttributeHasValue)
            {
                return;
            }
            xElement.Add(_attrObject.Value != null
                ? new XAttribute(AttributeName, _attrObject.Value)
                : new XAttribute(AttributeName, ""));
        }

        public override void ReadAttribute(XElement element)
        {
            AttributeHasValue = false;
            _attrObject = null;
            XAttribute xObject = element.Attribute(AttributeName);
            if (xObject != null)
            {
                _attrObject = new URI { Value = xObject.Value };
                AttributeHasValue = true;
            }
        }

        public override string Value
        {
            get { return _attrObject.Value; }
            set
            {
                _attrObject.Value = value;
                AttributeHasValue = (value != null);
            }
        }
        #endregion

    }
}
