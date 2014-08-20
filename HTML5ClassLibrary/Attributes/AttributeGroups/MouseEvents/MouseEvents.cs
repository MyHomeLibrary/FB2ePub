using XHTMLClassLibrary.BaseElements;

namespace XHTMLClassLibrary.Attributes.AttributeGroups.MouseEvents
{
    /// <summary>
    /// Mouse global events
    /// </summary>
    public class MouseEvents
    {
        [AttributeTypeAttributeMember(SupportedStandards = HTMLElementType.HTML5 | HTMLElementType.XHTML5 | HTMLElementType.XHTML11 | HTMLElementType.Transitional | HTMLElementType.Strict | HTMLElementType.FrameSet)]
        private readonly OnClickEventAttribute _onClickEventAttribute = new OnClickEventAttribute();

        [AttributeTypeAttributeMember(SupportedStandards = HTMLElementType.HTML5 | HTMLElementType.XHTML5 | HTMLElementType.XHTML11 | HTMLElementType.Transitional | HTMLElementType.Strict | HTMLElementType.FrameSet)]
        private readonly OnDblClickEventAttribute _onDblClickEventAttribute = new OnDblClickEventAttribute();

        [AttributeTypeAttributeMember(SupportedStandards = HTMLElementType.HTML5 | HTMLElementType.XHTML5)]
        private readonly OnDragEventAttribute _onDragEventAttribute = new OnDragEventAttribute();

        [AttributeTypeAttributeMember(SupportedStandards = HTMLElementType.HTML5 | HTMLElementType.XHTML5)]
        private readonly OnDragEndEventAttribute _onDragEndEventAttribute = new OnDragEndEventAttribute();

        [AttributeTypeAttributeMember(SupportedStandards = HTMLElementType.HTML5 | HTMLElementType.XHTML5)]
        private readonly OnDragEnterEventAttribute _onDragEnterEventAttribute = new OnDragEnterEventAttribute();

        [AttributeTypeAttributeMember(SupportedStandards = HTMLElementType.HTML5 | HTMLElementType.XHTML5)]
        private readonly OnDragLeaveEventAttribute _onDragLeaveEventAttribute = new OnDragLeaveEventAttribute();

        [AttributeTypeAttributeMember(SupportedStandards = HTMLElementType.HTML5 | HTMLElementType.XHTML5)]
        private readonly OnDragOverEventAttribute _onDragOverEventAttribute = new OnDragOverEventAttribute();

        [AttributeTypeAttributeMember(SupportedStandards = HTMLElementType.HTML5 | HTMLElementType.XHTML5)]
        private readonly OnDragStartEventAttribute _onDragStartEventAttribute = new OnDragStartEventAttribute();

        [AttributeTypeAttributeMember(SupportedStandards = HTMLElementType.HTML5 | HTMLElementType.XHTML5)]
        private readonly OnDropEventAttribute _onDropEventAttribute = new OnDropEventAttribute();

        [AttributeTypeAttributeMember(SupportedStandards = HTMLElementType.HTML5 | HTMLElementType.XHTML5 | HTMLElementType.XHTML11 | HTMLElementType.Transitional | HTMLElementType.Strict | HTMLElementType.FrameSet)]
        private readonly OnMouseDownEventAttribute _onMouseDownEventAttribute = new OnMouseDownEventAttribute();

        [AttributeTypeAttributeMember(SupportedStandards = HTMLElementType.HTML5 | HTMLElementType.XHTML5 | HTMLElementType.XHTML11 | HTMLElementType.Transitional | HTMLElementType.Strict | HTMLElementType.FrameSet)]
        private readonly OnMouseMoveEventAttribute _onMouseMoveEventAttribute = new OnMouseMoveEventAttribute();

        [AttributeTypeAttributeMember(SupportedStandards = HTMLElementType.HTML5 | HTMLElementType.XHTML5 | HTMLElementType.XHTML11 | HTMLElementType.Transitional | HTMLElementType.Strict | HTMLElementType.FrameSet)]
        private readonly OnMouseOutEventAttribute _onMouseOutEventAttribute = new OnMouseOutEventAttribute();

        [AttributeTypeAttributeMember(SupportedStandards = HTMLElementType.HTML5 | HTMLElementType.XHTML5 | HTMLElementType.XHTML11 | HTMLElementType.Transitional | HTMLElementType.Strict | HTMLElementType.FrameSet)]
        private readonly OnMouseOverEventAttribute _onMouseOverEventAttribute = new OnMouseOverEventAttribute();

        [AttributeTypeAttributeMember(SupportedStandards = HTMLElementType.HTML5 | HTMLElementType.XHTML5 | HTMLElementType.XHTML11 | HTMLElementType.Transitional | HTMLElementType.Strict | HTMLElementType.FrameSet)]
        private readonly OnMouseUpEventAttribute _onMouseUpEventAttribute = new OnMouseUpEventAttribute();

        [AttributeTypeAttributeMember(SupportedStandards = HTMLElementType.HTML5 | HTMLElementType.XHTML5)]
        private readonly OnMouseWheelEventAttribute _onMouseWheelEventAttribute = new OnMouseWheelEventAttribute();

        [AttributeTypeAttributeMember(SupportedStandards = HTMLElementType.HTML5 | HTMLElementType.XHTML5)]
        private readonly OnScrollEventAttribute _onScrollEventAttribute = new OnScrollEventAttribute();

        [AttributeTypeAttributeMember(SupportedStandards = HTMLElementType.HTML5 | HTMLElementType.XHTML5 | HTMLElementType.XHTML11 | HTMLElementType.Transitional | HTMLElementType.Strict | HTMLElementType.FrameSet)]
        private readonly OnMouseEnterEventAttribute _onMouseEnterEventAttribute =new OnMouseEnterEventAttribute();

        [AttributeTypeAttributeMember(SupportedStandards = HTMLElementType.HTML5 | HTMLElementType.XHTML5 | HTMLElementType.XHTML11 | HTMLElementType.Transitional | HTMLElementType.Strict | HTMLElementType.FrameSet)]
        private readonly OnMouseLeaveEventAttribute _onMouseLeaveEventAttribute = new OnMouseLeaveEventAttribute();





        /// <summary>
        /// Fires on a mouse click on the element
        /// </summary>
        public OnClickEventAttribute OnClickEvent { get { return _onClickEventAttribute; }}

        /// <summary>
        /// Fires on a mouse double-click on the element
        /// </summary>
        public OnDblClickEventAttribute OnDblClickEvent { get { return _onDblClickEventAttribute; }}


        /// <summary>
        /// Script to be run when an element is dragged
        /// </summary>
        public OnDragEventAttribute OnDragEvent { get { return _onDragEventAttribute; }}


        /// <summary>
        /// Script to be run at the end of a drag operation
        /// </summary>
        public OnDragEndEventAttribute OnDragEndEvent { get { return _onDragEndEventAttribute; }}


        /// <summary>
        /// Script to be run when an element has been dragged to a valid drop target
        /// </summary>
        public OnDragEnterEventAttribute OnDragEnterEvent { get { return _onDragEnterEventAttribute; }}


        /// <summary>
        /// Script to be run when an element leaves a valid drop target
        /// </summary>
        public OnDragLeaveEventAttribute OnDragLeaveEvent { get { return _onDragLeaveEventAttribute; }}


        /// <summary>
        /// Script to be run when an element is being dragged over a valid drop target
        /// </summary>
        public OnDragOverEventAttribute OnDragOverEvent { get { return _onDragOverEventAttribute; }}



        /// <summary>
        /// Script to be run at the start of a drag operation
        /// </summary>
        public OnDragStartEventAttribute OnDragStartEvent { get { return _onDragStartEventAttribute;}}


        /// <summary>
        /// Script to be run when dragged element is being dropped
        /// </summary>
        public OnDropEventAttribute OnDropEvent { get { return _onDropEventAttribute; }}


        /// <summary>
        /// Fires when a mouse button is pressed down on an element
        /// </summary>
        public OnMouseDownEventAttribute OnMouseDownEvent { get { return _onMouseDownEventAttribute; }}


        /// <summary>
        /// Fires when the mouse pointer moves over an element
        /// </summary>
        public OnMouseMoveEventAttribute OnMouseMoveEvent { get { return _onMouseMoveEventAttribute; }}


        /// <summary>
        /// Fires when the mouse pointer moves out of an element
        /// </summary>
        public OnMouseOutEventAttribute OnMouseOutEvent { get { return _onMouseOutEventAttribute; }}


        /// <summary>
        /// Fires when a mouse button is released over an element
        /// </summary>
        public OnMouseUpEventAttribute OnMouseUpEvent { get { return _onMouseUpEventAttribute; }}


        /// <summary>
        /// Script to be run when the mouse wheel is being rotated
        /// </summary>
        public OnMouseWheelEventAttribute OnMouseWheelEvent { get { return _onMouseWheelEventAttribute; }}


        /// <summary>
        /// Script to be run when an element's scrollbar is being scrolled
        /// </summary>
        public OnScrollEventAttribute OnScrollEvent { get { return _onScrollEventAttribute; }}

        /// <summary>
        /// 
        /// </summary>
        public OnMouseEnterEventAttribute OnMouseEnterEvent { get { return _onMouseEnterEventAttribute; }}

        /// <summary>
        /// 
        /// </summary>
        public OnMouseLeaveEventAttribute OnMouseLeaveEvent { get { return _onMouseLeaveEventAttribute; }}

        /// <summary>
        /// 
        /// </summary>
        public OnMouseOverEventAttribute OnMouseOverEvent { get { return _onMouseOverEventAttribute; }}

    }
}
