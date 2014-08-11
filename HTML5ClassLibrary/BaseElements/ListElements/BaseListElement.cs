using System.Collections.Generic;
using System.Xml.Linq;
using HTML5ClassLibrary.Attributes;
using HTML5ClassLibrary.Attributes.AttributeGroups.FormEvents;
using HTML5ClassLibrary.Attributes.AttributeGroups.HTMLGlobal;
using HTML5ClassLibrary.Attributes.AttributeGroups.KeyboardEvents;
using HTML5ClassLibrary.Attributes.AttributeGroups.MediaEvents;
using HTML5ClassLibrary.Attributes.AttributeGroups.MouseEvents;
using HTML5ClassLibrary.Attributes.AttributeGroups.WindowEventAttributes;
using HTML5ClassLibrary.Attributes.Events;

namespace HTML5ClassLibrary.BaseElements.ListElements
{
    public interface IListElement : IHTML5Item
    {

    }

    public abstract class BaseListElement : IListElement
    {
        public static XNamespace XhtmlNameSpace = @"http://www.w3.org/1999/xhtml";

        private  readonly HTMLGlobalAttributes _globalAttributes = new HTMLGlobalAttributes();
        private readonly FormEvents _formEvents = new FormEvents();
        private readonly KeyboardEvents _keyboardEvents = new KeyboardEvents();
        private readonly MediaEvents _mediaEvents = new MediaEvents();
        private readonly MouseEvents _mouseEvents = new MouseEvents();
        private readonly WindowEventAttributes _windowEventAttributes = new WindowEventAttributes();


        #region public_properties

        public HTMLGlobalAttributes GlobalAttributes { get { return _globalAttributes; }}

        public FormEvents FormEvents { get { return _formEvents; } }

        public KeyboardEvents KeyboardEvents { get { return _keyboardEvents; } }

        public MediaEvents MediaEvents { get { return _mediaEvents; } }

        public MouseEvents MouseEvents { get { return _mouseEvents; } }

        public WindowEventAttributes WindowEvents { get { return _windowEventAttributes; } }

        #endregion

        #region Implementation of IHTML5Item

        /// <summary>
        /// Loads the element from XNode
        /// </summary>
        /// <param name="xNode">node to load element from</param>
        public abstract void Load(XNode xNode);

        /// <summary>
        /// Generates element to XNode from data
        /// </summary>
        /// <returns>generated XNode</returns>
        public abstract XNode Generate();

        /// <summary>
        /// Checks it element data is valid
        /// </summary>
        /// <returns>true if valid</returns>
        public abstract bool IsValid();

        /// <summary>
        /// Adds sub-item to the item , only if 
        /// allowed by the rules and element can accept content
        /// </summary>
        /// <param name="item">sub-item to add</param>
        public abstract void Add(IHTML5Item item);

        public abstract void Remove(IHTML5Item item);

        public abstract List<IHTML5Item> SubElements();

        /// <summary>
        /// Get/Set item parent in the XHTML "tree"
        /// </summary>
        public IHTML5Item Parent { get; set; }

        #endregion

        protected virtual void AddAtributes(XElement xElement)
        {
            _globalAttributes.AddAttributes(xElement);
            _formEvents.AddAttributes(xElement);
            _keyboardEvents.AddAttributes(xElement);
            _mediaEvents.AddAttributes(xElement);
            _mouseEvents.AddAttributes(xElement);
            _windowEventAttributes.AddAttributes(xElement);
        }

        protected virtual void ReadAttributes(XElement xElement)
        {
            _globalAttributes.ReadAttributes(xElement);
            _formEvents.ReadAttributes(xElement);
            _keyboardEvents.ReadAttributes(xElement);
            _mediaEvents.ReadAttributes(xElement);
            _mouseEvents.ReadAttributes(xElement);
            _windowEventAttributes.ReadAttributes(xElement);
        }

    }
}
