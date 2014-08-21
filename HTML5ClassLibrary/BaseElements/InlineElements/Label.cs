using XHTMLClassLibrary.Attributes;

namespace XHTMLClassLibrary.BaseElements.InlineElements
{
    /// <summary>
    /// The label element associates a label with form controls such as input, textarea, select and object. 
    /// This association enhances the usability of forms. For example, when users of visual Web browsers click in a label, 
    /// focus is automatically set in the associated form control. For users of assistive technology, 
    /// establishing associations between labels and controls helps clarify the spatial relationships found in forms and makes them easier to navigate.
    /// </summary>
   [HTMLItemAttribute(ElementName = "label", SupportedStandards = HTMLElementType.HTML5 |  HTMLElementType.XHTML5 | HTMLElementType.XHTML11 | HTMLElementType.Transitional | HTMLElementType.Strict | HTMLElementType.FrameSet)]
    public class Label : HTMLItem, IInlineItem
    {
        [AttributeTypeAttributeMember(Name = "for", SupportedStandards = HTMLElementType.HTML5 | HTMLElementType.XHTML5 | HTMLElementType.XHTML11 | HTMLElementType.Transitional | HTMLElementType.Strict | HTMLElementType.FrameSet)]
        private readonly IdReferenceAttribute _forAttribute = new IdReferenceAttribute();

        [AttributeTypeAttributeMember(Name = "form", SupportedStandards = HTMLElementType.HTML5 | HTMLElementType.XHTML5)]
        private readonly URITypeAttribute _formIdAttribute = new URITypeAttribute();



        /// <summary>
        /// This attribute explicitly associates the label with a form control. 
        /// When present, the value of this attribute must be the same as the value of the id attribute of the form control in the same document. When absent, the label being defined is associated with the control inside the label element.
        /// </summary>
        public IdReferenceAttribute For { get { return _forAttribute; } }

        /// <summary>
        /// Specifies one or more forms the label belongs to
        /// </summary>
        public URITypeAttribute Form { get { return _formIdAttribute; }}



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
    }
}
