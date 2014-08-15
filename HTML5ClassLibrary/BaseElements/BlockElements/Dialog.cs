using System;
using System.Collections.Generic;
using System.Xml;
using System.Xml.Linq;
using HTMLClassLibrary.Attributes;
using HTMLClassLibrary.BaseElements.InlineElements;

namespace HTMLClassLibrary.BaseElements.BlockElements
{
    /// <summary>
    /// The "dialog" tag defines a dialog box or window.
    ///The "dialog" element makes it easy to create popup dialogs and modals on a web page.
    ///</summary>
    [HTMLItemAttribute(ElementName = "dialog", SupportedStandards = HTMLElementType.HTML5 )]
    public class Dialog: HTMLItem, IBlockElement
    {
        public Dialog()
        {
            RegisterAttribute(_openAttribute);
        }

        private readonly OpenAttribute _openAttribute = new OpenAttribute();


        /// <summary>
        /// Specifies that the dialog should be visible (open) to the user
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
