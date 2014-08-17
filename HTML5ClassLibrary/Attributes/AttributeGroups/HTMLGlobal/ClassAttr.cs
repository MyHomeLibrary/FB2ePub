using System.Xml.Linq;
using XHTMLClassLibrary.AttributeDataTypes;

namespace XHTMLClassLibrary.Attributes.AttributeGroups.HTMLGlobal
{
    /// <summary>
    /// The class attribute specifies one or more classnames for an element.
    /// The class attribute is mostly used to point to a class in a style sheet. However, it can also be used by a JavaScript (via the HTML DOM) to make changes to HTML elements with a specified class.
    /// </summary>
    public class ClassAttr : BaseAttribute
    {
        private NameTokens _attrObject  = new NameTokens();

        private const string AttributeName = "class";

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
            XAttribute xClass = element.Attribute(AttributeName);
            if (xClass != null)
            {
                _attrObject = new NameTokens {Value = xClass.Value};
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
