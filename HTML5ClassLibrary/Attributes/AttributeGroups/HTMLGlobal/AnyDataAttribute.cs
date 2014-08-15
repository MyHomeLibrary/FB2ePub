using System.Collections.Generic;
using System.Xml.Linq;

namespace HTMLClassLibrary.Attributes.AttributeGroups.HTMLGlobal
{
    /// <summary>
    /// The data-* attributes is used to store custom data private to the page or application.
    /// The data-* attributes gives us the ability to embed custom data attributes on all HTML elements.
    /// The stored (custom) data can then be used in the page's JavaScript to create a more engaging user experience (without any Ajax calls or server-side database queries).
    /// The data-* attributes consist of two parts:
    ///     The attribute name should not contain any uppercase letters, and must be at least one character long after the prefix "data-"
    ///     The attribute value can be any string
    /// Note: Custom attributes prefixed with "data-" will be completely ignored by the user agent.
    /// </summary>
    public class AnyDataAttribute
    {
        private const string DataPreffix = "data-";
        private bool _hasValue;
        private  readonly Dictionary<string,string>  _dataObjects = new Dictionary<string, string>();

        public bool HasValue()
        {
            return _hasValue;
        }

        public void AddAttribute(XElement xElement)
        {
            if (_hasValue)
            {
                foreach (var dataObject in _dataObjects)
                {
                    xElement.Add(new XAttribute(DataPreffix+dataObject.Key, dataObject.Value));
                }
            }
        }

        public void ReadAttribute(XElement element)
        {
            _hasValue = false;
            _dataObjects.Clear();
            foreach (var xAttribute in element.Attributes())
            {
                if (xAttribute.Name.LocalName.StartsWith(DataPreffix) &&
                    xAttribute.Name.LocalName.Length > DataPreffix.Length) // make sure not empty
                {
                    _hasValue = true;
                    _dataObjects[xAttribute.Name.LocalName.Substring(DataPreffix.Length-1)] = xAttribute.Value;
                }
            }
        }

        public Dictionary<string,string> ValueObjects
        {
            get { return _dataObjects; }
        }

        

    }
}
