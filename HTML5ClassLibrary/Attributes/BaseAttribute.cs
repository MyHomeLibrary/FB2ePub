﻿using System;
using System.Reflection;
using System.Xml.Linq;
using XHTMLClassLibrary.BaseElements;

namespace XHTMLClassLibrary.Attributes
{
    public interface IAttributeDataAccess
    {
        bool HasValue();
        object Value { get; set; }       
    }

    public interface IBaseAttribute : IAttributeDataAccess , ICloneable
    {

        void AddAttribute(XElement xElement,XNamespace ns);
        void ReadAttribute(XElement element);
        string GetAttributeName();
    }

    public  abstract class BaseAttribute : IBaseAttribute
    {
        protected BaseAttribute(string name)
        {
            _attributeName = name;
        }

        protected bool AttributeHasValue = false;
        private readonly string _attributeName;

        
        #region Implementation of IBaseAttribute

        public abstract void AddAttribute(XElement xElement, XNamespace ns);
        public abstract void ReadAttribute(XElement element);

        public bool HasValue()
        {
            return AttributeHasValue;
        }

        public abstract object Value { get; set; }

        #endregion


        /// <summary>
        ///  Extracts attribute name from custom attributes
        /// </summary>
        /// <returns>name of the attribute as it in XML/HTML</returns>
        public string GetAttributeName()
        {
            return _attributeName;
        }

        public virtual object Clone()
        {
            BaseAttribute clonedAttribute = (BaseAttribute)Activator.CreateInstance(GetType(), _attributeName);
            if (AttributeHasValue)
            {
                clonedAttribute.Value = Value;
            }
            return clonedAttribute;
        }
    }
}
