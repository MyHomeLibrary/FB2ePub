using System.Collections.Generic;
using System.Xml.Linq;
using HTML5ClassLibrary.Attributes.Events;

namespace HTML5ClassLibrary.Attributes.AttributeGroups.MouseEvents
{
    /// <summary>
    /// 
    /// </summary>
    public class MouseEvents
    {
        private readonly OnClickEventAttribute _onClickEventAttribute = new OnClickEventAttribute();  
        private readonly OnDblClickEventAttribute _onDblClickEventAttribute = new OnDblClickEventAttribute(); 
        private readonly OnDragEventAttribute _onDragEventAttribute = new OnDragEventAttribute(); 
        private readonly OnDragEndEventAttribute _onDragEndEventAttribute = new OnDragEndEventAttribute(); 
        private readonly OnDragEnterEventAttribute _onDragEnterEventAttribute = new OnDragEnterEventAttribute(); 
        private readonly OnDragLeaveEventAttribute _onDragLeaveEventAttribute = new OnDragLeaveEventAttribute(); 
        private readonly OnDragOverEventAttribute _onDragOverEventAttribute = new OnDragOverEventAttribute(); 
        private readonly OnDragStartEventAttribute _onDragStartEventAttribute = new OnDragStartEventAttribute(); 
        private readonly OnDropEventAttribute _onDropEventAttribute = new OnDropEventAttribute(); 
        private readonly OnMouseDownEventAttribute _onMouseDownEventAttribute = new OnMouseDownEventAttribute();
        private readonly OnMouseMoveEventAttribute _onMouseMoveEventAttribute = new OnMouseMoveEventAttribute(); 
        private readonly OnMouseOutEventAttribute _onMouseOutEventAttribute = new OnMouseOutEventAttribute(); 
        private readonly OnMouseUpEventAttribute _onMouseUpEventAttribute = new OnMouseUpEventAttribute(); 
        private readonly OnMouseWheelEventAttribute _onMouseWheelEventAttribute = new OnMouseWheelEventAttribute(); 
        private readonly OnScrollEventAttribute _onScrollEventAttribute = new OnScrollEventAttribute(); 
        private readonly OnMouseEnterEventAttribute _onMouseEnterEventAttribute =new OnMouseEnterEventAttribute();
        private readonly OnMouseLeaveEventAttribute _onMouseLeaveEventAttribute = new OnMouseLeaveEventAttribute();
        private readonly OnMouseOverEventAttribute _onMouseOverEventAttribute = new OnMouseOverEventAttribute();





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


        /// <summary>
        /// Add all attributes set to specified xElement
        /// </summary>
        /// <param name="attributesList"></param>
        public void AddAttributes(List<IBaseAttribute> attributesList)
        {
            attributesList.Add(_onClickEventAttribute);
            attributesList.Add(_onDblClickEventAttribute);
            attributesList.Add(_onDragEventAttribute);
            attributesList.Add(_onDragEndEventAttribute);
            attributesList.Add(_onDragEnterEventAttribute);
            attributesList.Add(_onDragLeaveEventAttribute);
            attributesList.Add(_onDragOverEventAttribute);
            attributesList.Add(_onDragStartEventAttribute);
            attributesList.Add(_onDropEventAttribute);
            attributesList.Add(_onMouseDownEventAttribute);
            attributesList.Add(_onMouseMoveEventAttribute);
            attributesList.Add(_onMouseOutEventAttribute);
            attributesList.Add(_onMouseUpEventAttribute);
            attributesList.Add(_onMouseWheelEventAttribute);
            attributesList.Add(_onScrollEventAttribute);
            attributesList.Add(_onMouseEnterEventAttribute);
            attributesList.Add(_onMouseLeaveEventAttribute);
            attributesList.Add(_onMouseOverEventAttribute);
        }
    }
}
