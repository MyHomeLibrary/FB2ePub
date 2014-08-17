using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using XHTMLClassLibrary.BaseElements;

namespace XHTMLClassLibrary
{
    /// <summary>
    /// Represent one element creator 
    /// To be used for implementation of single standard (HTM5, XHTML, etc.) mapping
    /// </summary>
    internal class ElementConvertor
    {
        private readonly Dictionary<string, Type> _elementsMap = new Dictionary<string, Type>();

        /// <summary>
        /// Adds an element to the map,
        /// Should be called only once per each element name
        /// </summary>
        /// <param name="elementName">name of the element</param>
        /// <param name="elementType">type of the element</param>
        public void AddElement(string elementName, Type elementType)
        {
            if (_elementsMap.ContainsKey(elementName))
            {
                throw new ArgumentException(
                    string.Format(
                        "Element <{0}> already present in a map with type {1}, can't add it again with type {2} - should be unique",
                        elementName, _elementsMap[elementName].ToString(), elementType.ToString()), "elementType");
            }
            if (string.IsNullOrEmpty(elementName))
            {
                throw new ArgumentException("Element can't be empty","elementName");
            }

            if (!elementType.GetInterfaces().Contains(typeof(IHTMLItem)))
            {
                throw new ArgumentException("The element type to register should implement IHTMLItem interface","elementType");
            }
            _elementsMap.Add(elementName,elementType);
        }

        /// <summary>
        /// Create one IHTMLItem element, according to HTML element name passed
        /// </summary>
        /// <param name="elementName">name of the element to create</param>
        /// <returns>IHTMLItem element</returns>
        public IHTMLItem CreateHTMLItem(string elementName)
        {
            if (_elementsMap.ContainsKey(elementName))
            {
                Object theObj = Activator.CreateInstance(_elementsMap[elementName]);
                var item = theObj as IHTMLItem;
                return item; 
            }
            return null; // no such element registered
        }
    }
}
