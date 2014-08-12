using System.Collections.Generic;
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
    public abstract class BaseContainingElement : HTML5Item
    {
        protected readonly List<IHTML5Item> Content = new List<IHTML5Item>();



        #region Implementation of IHTML5Item

        /// <summary>
        /// Adds sub-item to the item , only if 
        /// allowed by the rules and element can accept content
        /// </summary>
        /// <param name="item">sub-item to add</param>
        public override void Add(IHTML5Item item)
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
        public override void Remove(IHTML5Item item)
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
        public override List<IHTML5Item> SubElements()
        {
            return Content;
        }



        #endregion

        /// <summary>
        /// Check if element can be sub element of this element (according to XHTML rules)
        /// </summary>
        /// <param name="item">element to check</param>
        /// <returns>true if it can be sub element, false otherwise</returns>
        protected abstract bool IsValidSubType(IHTML5Item item);


    }
}
