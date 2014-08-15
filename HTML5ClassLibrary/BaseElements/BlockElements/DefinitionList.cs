using System;
using System.Collections.Generic;
using System.Xml;
using System.Xml.Linq;
using HTMLClassLibrary.BaseElements.InlineElements.TextBasedElements;
using HTMLClassLibrary.BaseElements.ListElements;

namespace HTMLClassLibrary.BaseElements.BlockElements
{
    /// <summary>
    /// The dl element is used to create a list where each item in the list comprises two parts: a term and a description. 
    /// A glossary of terms is a typical example of a definition list, 
    /// where each item consists of the term being defined and a definition of the term.
    /// </summary>
    [HTMLItemAttribute(ElementName = "dl", SupportedStandards = HTMLElementType.HTML5 | HTMLElementType.XHTML11 | HTMLElementType.Transitional | HTMLElementType.Strict | HTMLElementType.FrameSet)]
    public class DefinitionList : HTMLItem, IBlockElement
    {

        protected override bool IsValidSubType(IHTMLItem item)
        {
            if (item is DefinitionDescription)
            {
                return item.IsValid();
            }
            if (item is DefinitionTerms)
            {
                return item.IsValid();
            }
            return false;
        }


        public override bool IsValid()
        {
            return (Subitems.Count > 0);
        }
    }
}
