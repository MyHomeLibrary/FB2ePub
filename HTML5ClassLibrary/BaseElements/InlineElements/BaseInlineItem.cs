using System.Collections.Generic;
using System.Xml.Linq;
using HTML5ClassLibrary.Attributes;
using HTML5ClassLibrary.Attributes.AttributeGroups.FormEvents;
using HTML5ClassLibrary.Attributes.AttributeGroups.HTMLGlobal;
using HTML5ClassLibrary.Attributes.AttributeGroups.KeyboardEvents;
using HTML5ClassLibrary.Attributes.AttributeGroups.MediaEvents;
using HTML5ClassLibrary.Attributes.AttributeGroups.MouseEvents;
using HTML5ClassLibrary.Attributes.AttributeGroups.WindowEventAttributes;

namespace HTML5ClassLibrary.BaseElements.InlineElements
{
    abstract public class BaseInlineItem : HTML5Item, IInlineItem
    {
        public static XNamespace XhtmlNameSpace = @"http://www.w3.org/1999/xhtml";

    }
}
