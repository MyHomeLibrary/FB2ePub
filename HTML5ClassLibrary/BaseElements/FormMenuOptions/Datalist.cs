using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml;
using System.Xml.Linq;
using XHTMLClassLibrary.Attributes;
using XHTMLClassLibrary.Exceptions;

namespace XHTMLClassLibrary.BaseElements.FormMenuOptions
{
    /// <summary>
    /// The "datalist" tag specifies a list of pre-defined options for an "input" element.
    /// The "datalist" tag is used to provide an "autocomplete" feature on "input" elements. Users will see a drop-down list of pre-defined options as they input data.
    /// Use the "input" element's list attribute to bind it together with a "datalist" element.
    /// </summary>
    [HTMLItemAttribute(ElementName = "datalist", SupportedStandards = HTMLElementType.HTML5)]
    public class Datalist : HTMLItem, IOptionItem
    {
        /// <summary>
        /// Internal content of the element
        /// </summary>
        private readonly List<IHTMLItem> _content = new List<IHTMLItem>();


        public override bool IsValid()
        {
            return true;
        }


        protected override bool IsValidSubType(IHTMLItem item)
        {
            if (item is Option)
            {
                return item.IsValid();
            }
            return false;
        }
    }
}
