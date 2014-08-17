using System.Collections.Generic;
using XHTMLClassLibrary.BaseElements;

namespace XHTMLClassLibrary.Attributes.AttributeGroups.KeyboardEvents
{
    /// <summary>
    /// 
    /// </summary>
    public class KeyboardEvents
    {
        [AttributeTypeAttributeMember(SupportedStandards = HTMLElementType.HTML5 | HTMLElementType.XHTML5 | HTMLElementType.XHTML11 | HTMLElementType.Transitional | HTMLElementType.Strict | HTMLElementType.FrameSet)]
        private readonly OnKeyDownEventAttribute _onKeyDownEventAttribute = new OnKeyDownEventAttribute(); 

        [AttributeTypeAttributeMember(SupportedStandards = HTMLElementType.HTML5 | HTMLElementType.XHTML5 | HTMLElementType.XHTML11 | HTMLElementType.Transitional | HTMLElementType.Strict | HTMLElementType.FrameSet)]
        private readonly OnKeyPressEventAttribute _onKeyPressEventAttribute = new OnKeyPressEventAttribute(); 

        [AttributeTypeAttributeMember(SupportedStandards = HTMLElementType.HTML5 | HTMLElementType.XHTML5 | HTMLElementType.XHTML11 | HTMLElementType.Transitional | HTMLElementType.Strict | HTMLElementType.FrameSet)]
        private readonly OnKeyUpEventAttribute _onKeyUpEventAttribute = new OnKeyUpEventAttribute(); 



        /// <summary>
        /// Fires when a user is pressing a key
        /// </summary>
        public OnKeyDownEventAttribute OnKeyDownEvent { get { return _onKeyDownEventAttribute; }}

        /// <summary>
        /// Fires when a user presses a key
        /// </summary>
        public OnKeyPressEventAttribute OnKeyPressEvent { get { return _onKeyPressEventAttribute; }}


        /// <summary>
        /// Fires when a user releases a key
        /// </summary>
        public OnKeyUpEventAttribute OnKeyUpEvent   { get { return _onKeyUpEventAttribute; }}




        /// <summary>
        /// Add all attributes set to specified xElement
        /// </summary>
        public void AddAttributes(List<IBaseAttribute> attributesList)
        {
            attributesList.Add(_onKeyDownEventAttribute);
            attributesList.Add(_onKeyPressEventAttribute);
            attributesList.Add(_onKeyUpEventAttribute);
        }

    }
}
