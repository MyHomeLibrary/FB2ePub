﻿using System;
using System.Collections.Generic;
using System.Xml;
using System.Xml.Linq;
using HTMLClassLibrary.Attributes;
using HTMLClassLibrary.BaseElements.InlineElements;

namespace HTMLClassLibrary.BaseElements.BlockElements
{
    /// <summary>
    /// The "details" tag specifies additional details that the user can view or hide on demand.
    /// The "details" tag can be used to create an interactive widget that the user can open and close. Any sort of content can be put inside the "details" tag.
    /// The content of a "details" element should not be visible unless the open attribute is set.
    /// </summary>
    [HTMLItemAttribute(ElementName = "details", SupportedStandards = HTMLElementType.HTML5)]
    public class Details : HTMLItem, IBlockElement 
    {
        public Details()
        {
            RegisterAttribute(_openAttribute);
        }

        private readonly OpenAttribute _openAttribute = new OpenAttribute();


        /// <summary>
        /// Specifies that the details should be visible (open) to the user
        /// </summary>
        public OpenAttribute Open { get { return _openAttribute; }}

        public override bool IsValid()
        {
            return true;
        }

        protected override bool IsValidSubType(IHTMLItem item)
        {
            if (item is IInlineItem ||
                item is IBlockElement ||
                item is SimpleHTML5Text)
            {
                return item.IsValid();
            }
            return false;
        }

    }
}
