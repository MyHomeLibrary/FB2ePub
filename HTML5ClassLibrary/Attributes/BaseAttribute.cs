using System.Xml.Linq;

namespace HTML5ClassLibrary.Attributes
{
    internal interface IBaseAttribute
    {
        void AddAttribute(XElement xElement);
        void ReadAttribute(XElement element);
        bool HasValue();
        string Value { get; set;}
    }

    public  abstract class BaseAttribute : IBaseAttribute
    {
        protected bool _hasValue = false;

        #region Implementation of IBaseAttribute

        public abstract void AddAttribute(XElement xElement);
        public abstract void ReadAttribute(XElement element);
        
        public bool HasValue()
        {
            return _hasValue;
        }

        public abstract string Value { get; set; }

        #endregion
    }
}
