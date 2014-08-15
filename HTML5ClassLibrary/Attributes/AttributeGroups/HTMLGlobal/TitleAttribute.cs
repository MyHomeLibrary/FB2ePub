using System.Xml.Linq;
using HTMLClassLibrary.AttributeDataTypes;

namespace HTMLClassLibrary.Attributes.AttributeGroups.HTMLGlobal
{
    /// <summary>
    /// The title attribute specifies extra information about an element.
    /// The information is most often shown as a tooltip text when the mouse moves over the element.
    /// </summary>
    public class TitleAttribute : BaseAttribute
    {
        private Text _attrObject = new Text();

        private const string AttributeName = "title";

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
        #endregion
    }
}
