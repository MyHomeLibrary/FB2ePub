using System;
using System.Collections.Generic;
using System.Xml;
using System.Xml.Linq;
using HTML5ClassLibrary.Attributes;
using HTML5ClassLibrary.Exceptions;

namespace HTML5ClassLibrary.BaseElements.InlineElements
{
    /// <summary>
    /// Unlike English, which is written from left-to-right (LTR), some languages, such as Arabic and Hebrew, 
    /// are written from right-to-left (RTL). When the same paragraph contains both RTL and LTR text, 
    /// this is known as bidirectional text or "bidi" text for short
    /// Most Web browsers will correctly display bidirectional text. 
    /// However, situations may arise when the browser's bidirectional algorithm results in incorrect presentation. 
    /// To correct this, the bdo element allows authors to turn off the bidirectional algorithm for selected fragments of text.
    /// </summary>
    public class BiDirectionalOverride : TextBasedElement
    {
        public const string ElementName = "bdo";


        protected override string GetElementName()
        {
            return ElementName;
        }

        protected override void AddAttributes(XElement xElement)
        {
            if (!DirectionAttr.HasValue())
            {
                throw new HTML5ViolationException(this,"The BDO element have to have direction set");
            }
            base.AddAttributes(xElement);
        }
    }
}
