using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml;
using System.Xml.Linq;
using XHTMLClassLibrary.BaseElements.InlineElements;
using XHTMLClassLibrary.BaseElements.InlineElements.TextBasedElements;

namespace XHTMLClassLibrary.BaseElements.BlockElements
{
    public class Figure : HTMLItem, IBlockElement
    {
        public const string ElementName = "figure";


        public override bool IsValid()
        {
            IEnumerable<IHTMLItem> captions = Subitems.FindAll(x => x is FigCaption);
            foreach (var figCaption in captions) // figcaption can be only first or last element
            {
                if (figCaption != Subitems.First() && figCaption != Subitems.Last())
                {
                    return false;
                }
            }
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
