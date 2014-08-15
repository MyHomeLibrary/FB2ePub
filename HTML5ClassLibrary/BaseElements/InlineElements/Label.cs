using System;
using System.Collections.Generic;
using System.Xml;
using System.Xml.Linq;
using HTMLClassLibrary.Attributes;
using HTMLClassLibrary.Attributes.AttributeGroups.FormEvents;
using HTMLClassLibrary.Attributes.AttributeGroups.HTMLGlobal;
using HTMLClassLibrary.Attributes.AttributeGroups.KeyboardEvents;
using HTMLClassLibrary.Attributes.AttributeGroups.MouseEvents;
using HTMLClassLibrary.Attributes.Events;
using HTMLClassLibrary.Exceptions;

namespace HTMLClassLibrary.BaseElements.InlineElements
{
    /// <summary>
    /// The label element associates a label with form controls such as input, textarea, select and object. 
    /// This association enhances the usability of forms. For example, when users of visual Web browsers click in a label, 
    /// focus is automatically set in the associated form control. For users of assistive technology, 
    /// establishing associations between labels and controls helps clarify the spatial relationships found in forms and makes them easier to navigate.
    /// </summary>
   [HTMLItemAttribute(ElementName = "label", SupportedStandards = HTMLElementType.HTML5 | HTMLElementType.XHTML11 | HTMLElementType.Transitional | HTMLElementType.Strict | HTMLElementType.FrameSet)]
    public class Label : HTMLItem, IInlineItem
    {

        public Label()
        {
            RegisterAttribute(_forAttribute);
            RegisterAttribute(_formIdAttribute);
        }

        /// <summary>
        /// Internal content of the element
        /// </summary>
        private readonly List<IHTMLItem> _content = new List<IHTMLItem>();

        private readonly ForAttribute _forAttribute = new ForAttribute();
        private readonly FormIdAttribute _formIdAttribute = new FormIdAttribute();



        /// <summary>
        /// This attribute explicitly associates the label with a form control. 
        /// When present, the value of this attribute must be the same as the value of the id attribute of the form control in the same document. When absent, the label being defined is associated with the control inside the label element.
        /// </summary>
        public ForAttribute For { get { return _forAttribute; } }

        /// <summary>
        /// Specifies one or more forms the label belongs to
        /// </summary>
        public FormIdAttribute Form { get { return _formIdAttribute; }}



        protected override bool IsValidSubType(IHTMLItem item)
        {
            if (item is IInlineItem)
            {
                // TODO: check for label presence at depth
                if (item is Label)
                {
                    return false;
                }
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
        /// <returns>
        /// true if valid
        /// </returns>
        public override bool IsValid()
        {
            return true;
        }

        public override List<IHTMLItem> SubElements()
        {
            return _content;
        }
    }
}
