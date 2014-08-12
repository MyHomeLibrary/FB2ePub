using System.Collections.Generic;
using System.Xml.Linq;
using HTML5ClassLibrary.Attributes.AttributeGroups.FormEvents;
using HTML5ClassLibrary.Attributes.AttributeGroups.HTMLGlobal;
using HTML5ClassLibrary.Attributes.AttributeGroups.KeyboardEvents;
using HTML5ClassLibrary.Attributes.AttributeGroups.MediaEvents;
using HTML5ClassLibrary.Attributes.AttributeGroups.MouseEvents;
using HTML5ClassLibrary.Attributes.AttributeGroups.WindowEventAttributes;

namespace HTML5ClassLibrary.BaseElements.TableElements
{
    public interface ITableElement 
    {

    }

    /// <summary>
    /// Base class for all table elements
    /// </summary>
    public abstract class BaseTableElement : HTML5Item, ITableElement
    {

        public static XNamespace XhtmlNameSpace = @"http://www.w3.org/1999/xhtml";

    }
}
