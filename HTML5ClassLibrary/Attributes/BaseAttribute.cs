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

        #region Implementation of IBaseAttribute

        public abstract void AddAttribute(XElement xElement);
        public abstract void ReadAttribute(XElement element);

        public bool HasValue()
        {
            return AttributeHasValue;
        }

        public abstract string Value { get; set; }

        #endregion
    }
}
