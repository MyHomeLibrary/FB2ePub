using System;
using System.Collections.Generic;
using System.Xml.Linq;
using HTML5ClassLibrary.Attributes;
using HTML5ClassLibrary.Attributes.AttributeGroups.FormEvents;
using HTML5ClassLibrary.Attributes.AttributeGroups.HTMLGlobal;
using HTML5ClassLibrary.Attributes.AttributeGroups.KeyboardEvents;
using HTML5ClassLibrary.Attributes.AttributeGroups.MediaEvents;
using HTML5ClassLibrary.Attributes.AttributeGroups.MouseEvents;
using HTML5ClassLibrary.Attributes.AttributeGroups.WindowEventAttributes;

namespace HTML5ClassLibrary.BaseElements
{
    /// <summary>
    /// A base class for any HTML item
    /// </summary>
    abstract public class HTML5Item : IHTML5Item
    {

        protected readonly List<IBaseAttribute> Attributes = new List<IBaseAttribute>();

        // General common attributes and event attributes for any HTML element

        private readonly HTMLGlobalAttributes _globalAttributes = new HTMLGlobalAttributes();
        private readonly FormEvents _formEvents = new FormEvents();
        private readonly KeyboardEvents _keyboardEvents = new KeyboardEvents();
        private readonly MediaEvents _mediaEvents = new MediaEvents();
        private readonly MouseEvents _mouseEvents = new MouseEvents();
        private readonly WindowEventAttributes _windowEventAttributes = new WindowEventAttributes();

        protected HTML5Item()
        {
            _globalAttributes.AddAttributes(Attributes);
            _formEvents.AddAttributes(Attributes);
            _keyboardEvents.AddAttributes(Attributes);
            _mediaEvents.AddAttributes(Attributes);
            _mouseEvents.AddAttributes(Attributes);
            _windowEventAttributes.AddAttributes(Attributes);
        }

        public abstract void Load(XNode xNode);
        public abstract XNode Generate();


        /// <summary>
        /// Check if element valid 
        /// </summary>
        /// <returns></returns>
        public virtual bool IsValid()
        {
            return false;
        }

        /// <summary>
        /// Adds sub-item to the item , only if 
        /// allowed by the rules and element can accept content
        /// </summary>
        /// <param name="item">sub-item to add</param>
        public virtual void Add(IHTML5Item item)
        {
            throw new Exception("This element does not contain sub-items");
        }

        /// <summary>
        /// Remove sub-item 
        /// </summary>
        /// <param name="item"></param>
        public virtual void Remove(IHTML5Item item)
        {
            throw new Exception("This element does not contain sub-items");
        }

        /// <summary>
        /// Retutns list of sub-items
        /// </summary>
        /// <returns></returns>
        public virtual List<IHTML5Item> SubElements()
        {
            return null;
        }


        /// <summary>
        /// Get/Set item parent in the HTML "tree"
        /// </summary>
        public IHTML5Item Parent { get; set; }


        /// <summary>
        /// Global attributes 
        /// </summary>
        public HTMLGlobalAttributes GlobalAttributes { get { return _globalAttributes; } }

        /// <summary>
        /// Form related events
        /// </summary>
        public FormEvents FormEvents { get { return _formEvents; } }

        /// <summary>
        /// Keyboard related events
        /// </summary>
        public KeyboardEvents KeyboardEvents { get { return _keyboardEvents; } }

        /// <summary>
        /// Media related events (can include few that not 100% media related in all cases)
        /// </summary>
        public MediaEvents MediaEvents { get { return _mediaEvents; } }

        /// <summary>
        /// Mouse global events
        /// </summary>
        public MouseEvents MouseEvents { get { return _mouseEvents; } }

        /// <summary>
        /// Window events , not always working for all types of elements, mostrl for windows like "body"
        /// </summary>
        public WindowEventAttributes WindowEvents { get { return _windowEventAttributes; } }

        /// <summary>
        /// Used to add attributes to any element when generatig it
        /// </summary>
        /// <param name="xElement">XElement to add aattributes to </param>
        protected void AddAttributes(XElement xElement)
        {
            foreach (var attribute in Attributes)
            {
                attribute.AddAttribute(xElement);
            }
        }

        /// <summary>
        /// Used to read out attributes from the element passed, when readin/loading it
        /// </summary>
        /// <param name="xElement">XElement to load attributes from</param>
        protected void ReadAttributes(XElement xElement)
        {
            foreach (var attribute in Attributes)
            {
                attribute.ReadAttribute(xElement);
            }
        }
    }
}
