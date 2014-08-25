using System.Reflection;
using System.Xml.Linq;

namespace XHTMLClassLibrary.Attributes
{
    public interface IBaseAttribute
    {
        bool HasValue();
        string Value { get; set;}

        void AddAttribute(XElement xElement);
        void ReadAttribute(XElement element);
    }

    public  abstract class BaseAttribute : IBaseAttribute
    {
        protected bool AttributeHasValue = false;

        private AttributeTypeAttributeMember _attributeTypeAttribute = null;

        #region Implementation of IBaseAttribute

        public abstract void AddAttribute(XElement xElement);
        public abstract void ReadAttribute(XElement element);

        public bool HasValue()
        {
            return AttributeHasValue;
        }

        public abstract string Value { get; set; }

        #endregion

        /// <summary>
        /// Get the data from the attached attribute (cached)
        /// </summary>
        /// <returns></returns>
        private AttributeTypeAttributeMember GetAttributeTypeAttribute()
        {
            if (_attributeTypeAttribute != null)
            {
                return _attributeTypeAttribute;
            }
            var attributes = (AttributeTypeAttributeMember[])GetType().GetCustomAttributes(typeof(AttributeTypeAttributeMember), false);
            if (attributes.Length < 1) // check the we havr attribute set
            {
                throw new CustomAttributeFormatException(string.Format("The {0} object does not have AttributeTypeAttributeMember attribute set", GetType()));
            }
            if (string.IsNullOrEmpty(attributes[0].Name)) // check that we have Name set for attribute
            {
                throw new CustomAttributeFormatException(string.Format("The {0} object does not have AttributeTypeAttributeMember attribute Name set", GetType()));
            }
            return _attributeTypeAttribute = attributes[0];            
        }

        /// <summary>
        ///  Extracts attribute name from custom attributes
        /// </summary>
        /// <returns>name of the attribute as it in XML/HTML</returns>
        protected string GetAttributeName()
        {
            return GetAttributeTypeAttribute().Name;
        }
    }
}
