﻿using System.Collections.Generic;
using System.Xml.Linq;
using HTML5ClassLibrary.Attributes.AttributeGroups.FormEvents;
using HTML5ClassLibrary.Attributes.AttributeGroups.HTMLGlobal;
using HTML5ClassLibrary.Attributes.AttributeGroups.KeyboardEvents;
using HTML5ClassLibrary.Attributes.AttributeGroups.MediaEvents;
using HTML5ClassLibrary.Attributes.AttributeGroups.MouseEvents;
using HTML5ClassLibrary.Attributes.AttributeGroups.WindowEventAttributes;
using HTML5ClassLibrary.Exceptions;

namespace HTML5ClassLibrary.BaseElements
{
    /// <summary>
    /// Base element that can contain any other element
    /// </summary>
    public abstract class BaseContainingElement : IHTML5Item
    {
        protected readonly List<IHTML5Item> Content = new List<IHTML5Item>();
        private readonly HTMLGlobalAttributes _globalAttributes = new HTMLGlobalAttributes();
        private readonly FormEvents _formEvents = new FormEvents();
        private readonly KeyboardEvents _keyboardEvents = new KeyboardEvents();
        private readonly MediaEvents _mediaEvents = new MediaEvents();
        private readonly MouseEvents _mouseEvents = new MouseEvents();
        private readonly WindowEventAttributes _windowEventAttributes = new WindowEventAttributes();


        public HTMLGlobalAttributes GlobalAttributes
        {
            get { return _globalAttributes; }
        }

        public FormEvents FormEvents { get { return _formEvents; }}

        public KeyboardEvents KeyboardEvents { get { return _keyboardEvents; }}

        public MediaEvents MediaEvents { get { return _mediaEvents; }}

        public MouseEvents MouseEvents { get { return _mouseEvents; }}

        public WindowEventAttributes WindowEvents { get { return _windowEventAttributes; }}

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
        public virtual void Add(IHTML5Item item)
        {
            if ((item != null) && IsValidSubType(item))
            {
                Content.Add(item);
                item.Parent = this;
            }
            else
            {
                throw new HTML5ViolationException();
            }

        }

        /// <summary>
        /// Removes sub item 
        /// </summary>
        /// <param name="item">sub item to remove</param>
        public virtual void Remove(IHTML5Item item)
        {
            if (Content.Remove(item))
            {
                item.Parent = null;
            }

        }

        /// <summary>
        /// Get list of all sub elements
        /// </summary>
        /// <returns></returns>
        public virtual List<IHTML5Item> SubElements()
        {
            return Content;
        }


        /// <summary>
        /// Get/Set item parent in the XHTML "tree"
        /// </summary>
        public IHTML5Item Parent { get; set; }


        #endregion

        /// <summary>
        /// Check if element can be sub element of this element (according to XHTML rules)
        /// </summary>
        /// <param name="item">element to check</param>
        /// <returns>true if it can be sub element, false otherwise</returns>
        protected abstract bool IsValidSubType(IHTML5Item item);


    }
}