using System.Collections.Generic;
using System.Xml.Linq;
using HTML5ClassLibrary.Attributes;
using HTML5ClassLibrary.Attributes.AttributeGroups.HTMLGlobal;
using HTML5ClassLibrary.Attributes.AttributeGroups.KeyboardEvents;
using HTML5ClassLibrary.Attributes.AttributeGroups.MouseEvents;
using HTML5ClassLibrary.Attributes.Events;

namespace HTML5ClassLibrary.BaseElements.Ruby
{
    public interface IRubyItem : IHTML5Item
    {

    }

    public abstract class BaseRubyItem : IRubyItem
    {
        public static XNamespace XhtmlNameSpace = @"http://www.w3.org/1999/xhtml";
        private HTMLGlobalAttributes _globalAttributes = new HTMLGlobalAttributes();


        #region public_properties

        public HTMLGlobalAttributes GlobalAttributes { get { return _globalAttributes; }}

        #endregion

        protected virtual void AddAtributes(XElement xElement)
        {
            _globalAttributes.AddAttributes(xElement);
        }

        protected virtual void ReadAttributes(XElement xElement)
        {
            _globalAttributes.ReadAttributes(xElement);
        }


        public abstract void Load(XNode xNode);
        public abstract XNode Generate();
        public abstract bool IsValid();

        /// <summary>
        /// Adds subitem to the item , only if 
        /// allowed by the rules and element can accept content
        /// </summary>
        /// <param name="item">subitem to add</param>
        public abstract void Add(IHTML5Item item);

        public abstract void Remove(IHTML5Item item);

        public abstract List<IHTML5Item> SubElements();

        /// <summary>
        /// Get/Set item parent in the XHTML "tree"
        /// </summary>
        public IHTML5Item Parent { get; set; }
    }
}
