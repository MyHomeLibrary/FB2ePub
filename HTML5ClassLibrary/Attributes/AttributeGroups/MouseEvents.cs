using XHTMLClassLibrary.Attributes.Events;
using XHTMLClassLibrary.BaseElements;

namespace XHTMLClassLibrary.Attributes.AttributeGroups.MouseEvents
{
    /// <summary>
    /// Mouse global events
    /// </summary>
    public class MouseEvents
    {
        [AttributeTypeAttributeMember(Name = "onclick", SupportedStandards = HTMLElementType.HTML5 | HTMLElementType.XHTML5 | HTMLElementType.XHTML11 | HTMLElementType.Transitional | HTMLElementType.Strict | HTMLElementType.FrameSet)]
        private readonly OnEventAttribute _onClickEventAttribute = new OnEventAttribute();

        [AttributeTypeAttributeMember(Name = "ondblclick", SupportedStandards = HTMLElementType.HTML5 | HTMLElementType.XHTML5 | HTMLElementType.XHTML11 | HTMLElementType.Transitional | HTMLElementType.Strict | HTMLElementType.FrameSet)]
        private readonly OnEventAttribute _onDblClickEventAttribute = new OnEventAttribute();

        [AttributeTypeAttributeMember(Name = "ondrag", SupportedStandards = HTMLElementType.HTML5 | HTMLElementType.XHTML5)]
        private readonly OnEventAttribute _onDragEventAttribute = new OnEventAttribute();

        [AttributeTypeAttributeMember(Name = "ondragend", SupportedStandards = HTMLElementType.HTML5 | HTMLElementType.XHTML5)]
        private readonly OnEventAttribute _onDragEndEventAttribute = new OnEventAttribute();

        [AttributeTypeAttributeMember(Name = "ondragenter", SupportedStandards = HTMLElementType.HTML5 | HTMLElementType.XHTML5)]
        private readonly OnEventAttribute _onDragEnterEventAttribute = new OnEventAttribute();

        [AttributeTypeAttributeMember(Name = "ondragleave", SupportedStandards = HTMLElementType.HTML5 | HTMLElementType.XHTML5)]
        private readonly OnEventAttribute _onDragLeaveEventAttribute = new OnEventAttribute();

        [AttributeTypeAttributeMember(Name = "ondragover", SupportedStandards = HTMLElementType.HTML5 | HTMLElementType.XHTML5)]
        private readonly OnEventAttribute _onDragOverEventAttribute = new OnEventAttribute();

        [AttributeTypeAttributeMember(Name = "ondragstart", SupportedStandards = HTMLElementType.HTML5 | HTMLElementType.XHTML5)]
        private readonly OnEventAttribute _onDragStartEventAttribute = new OnEventAttribute();

        [AttributeTypeAttributeMember(Name = "ondrop", SupportedStandards = HTMLElementType.HTML5 | HTMLElementType.XHTML5)]
        private readonly OnEventAttribute _onDropEventAttribute = new OnEventAttribute();

        [AttributeTypeAttributeMember(Name = "onmousedown", SupportedStandards = HTMLElementType.HTML5 | HTMLElementType.XHTML5 | HTMLElementType.XHTML11 | HTMLElementType.Transitional | HTMLElementType.Strict | HTMLElementType.FrameSet)]
        private readonly OnEventAttribute _onMouseDownEventAttribute = new OnEventAttribute();

        [AttributeTypeAttributeMember(Name = "onmousemove", SupportedStandards = HTMLElementType.HTML5 | HTMLElementType.XHTML5 | HTMLElementType.XHTML11 | HTMLElementType.Transitional | HTMLElementType.Strict | HTMLElementType.FrameSet)]
        private readonly OnEventAttribute _onMouseMoveEventAttribute = new OnEventAttribute();

        [AttributeTypeAttributeMember(Name = "onmouseout", SupportedStandards = HTMLElementType.HTML5 | HTMLElementType.XHTML5 | HTMLElementType.XHTML11 | HTMLElementType.Transitional | HTMLElementType.Strict | HTMLElementType.FrameSet)]
        private readonly OnEventAttribute _onMouseOutEventAttribute = new OnEventAttribute();

        [AttributeTypeAttributeMember(Name = "onmouseover", SupportedStandards = HTMLElementType.HTML5 | HTMLElementType.XHTML5 | HTMLElementType.XHTML11 | HTMLElementType.Transitional | HTMLElementType.Strict | HTMLElementType.FrameSet)]
        private readonly OnEventAttribute _onMouseOverEventAttribute = new OnEventAttribute();

        [AttributeTypeAttributeMember(Name = "onmouseup", SupportedStandards = HTMLElementType.HTML5 | HTMLElementType.XHTML5 | HTMLElementType.XHTML11 | HTMLElementType.Transitional | HTMLElementType.Strict | HTMLElementType.FrameSet)]
        private readonly OnEventAttribute _onMouseUpEventAttribute = new OnEventAttribute();

        [AttributeTypeAttributeMember(Name = "onmousewheel", SupportedStandards = HTMLElementType.HTML5 | HTMLElementType.XHTML5)]
        private readonly OnEventAttribute _onMouseWheelEventAttribute = new OnEventAttribute();

        [AttributeTypeAttributeMember(Name = "onscroll", SupportedStandards = HTMLElementType.HTML5 | HTMLElementType.XHTML5)]
        private readonly OnEventAttribute _onScrollEventAttribute = new OnEventAttribute();

        [AttributeTypeAttributeMember(Name = "mouseenter", SupportedStandards = HTMLElementType.HTML5 | HTMLElementType.XHTML5 | HTMLElementType.XHTML11 | HTMLElementType.Transitional | HTMLElementType.Strict | HTMLElementType.FrameSet)]
        private readonly OnEventAttribute _onMouseEnterEventAttribute =new OnEventAttribute();

        [AttributeTypeAttributeMember(Name = "mouseleave", SupportedStandards = HTMLElementType.HTML5 | HTMLElementType.XHTML5 | HTMLElementType.XHTML11 | HTMLElementType.Transitional | HTMLElementType.Strict | HTMLElementType.FrameSet)]
        private readonly OnEventAttribute _onMouseLeaveEventAttribute = new OnEventAttribute();





        /// <summary>
        /// Fires on a mouse click on the element
        /// </summary>
        public OnEventAttribute OnClickEvent { get { return _onClickEventAttribute; }}

        /// <summary>
        /// Fires on a mouse double-click on the element
        /// </summary>
        public OnEventAttribute OnDblClickEvent { get { return _onDblClickEventAttribute; }}


        /// <summary>
        /// Script to be run when an element is dragged
        /// </summary>
        public OnEventAttribute OnDragEvent { get { return _onDragEventAttribute; }}


        /// <summary>
        /// Script to be run at the end of a drag operation
        /// </summary>
        public OnEventAttribute OnDragEndEvent { get { return _onDragEndEventAttribute; }}


        /// <summary>
        /// Script to be run when an element has been dragged to a valid drop target
        /// </summary>
        public OnEventAttribute OnDragEnterEvent { get { return _onDragEnterEventAttribute; }}


        /// <summary>
        /// Script to be run when an element leaves a valid drop target
        /// </summary>
        public OnEventAttribute OnDragLeaveEvent { get { return _onDragLeaveEventAttribute; }}


        /// <summary>
        /// Script to be run when an element is being dragged over a valid drop target
        /// </summary>
        public OnEventAttribute OnDragOverEvent { get { return _onDragOverEventAttribute; }}



        /// <summary>
        /// Script to be run at the start of a drag operation
        /// </summary>
        public OnEventAttribute OnDragStartEvent { get { return _onDragStartEventAttribute;}}


        /// <summary>
        /// Script to be run when dragged element is being dropped
        /// </summary>
        public OnEventAttribute OnDropEvent { get { return _onDropEventAttribute; }}


        /// <summary>
        /// Fires when a mouse button is pressed down on an element
        /// </summary>
        public OnEventAttribute OnMouseDownEvent { get { return _onMouseDownEventAttribute; }}


        /// <summary>
        /// Fires when the mouse pointer moves over an element
        /// </summary>
        public OnEventAttribute OnMouseMoveEvent { get { return _onMouseMoveEventAttribute; }}


        /// <summary>
        /// Fires when the mouse pointer moves out of an element
        /// </summary>
        public OnEventAttribute OnMouseOutEvent { get { return _onMouseOutEventAttribute; }}


        /// <summary>
        /// Fires when a mouse button is released over an element
        /// </summary>
        public OnEventAttribute OnMouseUpEvent { get { return _onMouseUpEventAttribute; }}


        /// <summary>
        /// Script to be run when the mouse wheel is being rotated
        /// </summary>
        public OnEventAttribute OnMouseWheelEvent { get { return _onMouseWheelEventAttribute; }}


        /// <summary>
        /// Script to be run when an element's scrollbar is being scrolled
        /// </summary>
        public OnEventAttribute OnScrollEvent { get { return _onScrollEventAttribute; }}

        /// <summary>
        /// 
        /// </summary>
        public OnEventAttribute OnMouseEnterEvent { get { return _onMouseEnterEventAttribute; }}

        /// <summary>
        /// 
        /// </summary>
        public OnEventAttribute OnMouseLeaveEvent { get { return _onMouseLeaveEventAttribute; }}

        /// <summary>
        /// 
        /// </summary>
        public OnEventAttribute OnMouseOverEvent { get { return _onMouseOverEventAttribute; }}

    }
}
