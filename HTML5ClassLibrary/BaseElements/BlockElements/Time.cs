using System;
using System.Collections.Generic;
using System.Xml;
using System.Xml.Linq;
using XHTMLClassLibrary.Attributes;
using XHTMLClassLibrary.BaseElements.InlineElements;

namespace XHTMLClassLibrary.BaseElements.BlockElements
{
    /// <summary>
    /// The "time" tag defines either a time (24 hour clock), or a date in the Gregorian calendar, optionally with a time and a time-zone offset.
    /// This element can be used as a way to encode dates and times in a machine-readable way so that, for example, user agents can offer to add birthday reminders or scheduled events to the user's calendar, and search engines can produce smarter search results.
    /// </summary>
    [HTMLItemAttribute(ElementName = "time", SupportedStandards = HTMLElementType.HTML5)]
    public class Time : HTMLItem, IBlockElement
    {
        public Time()
        {
            RegisterAttribute(_dateTimeAttribute);
        }

        private readonly DateTimeAttribute _dateTimeAttribute = new DateTimeAttribute();


        /// <summary>
        /// Gives the date/time being specified. Otherwise, the date/time is given by the element's contents
        /// </summary>
        public DateTimeAttribute DateTime { get { return _dateTimeAttribute; }}

        protected override bool IsValidSubType(IHTMLItem item)
        {
            if (item is IInlineItem)
            {
                return item.IsValid();
            }
            if (item is SimpleHTML5Text)
            {
                return item.IsValid();
            }
            return false;
        }


        /// <summary>
        /// Checks it element data is valid
        /// </summary>
        /// <returns>true if valid</returns>
        public override bool IsValid()
        {
            return true;
        }

    }
}
