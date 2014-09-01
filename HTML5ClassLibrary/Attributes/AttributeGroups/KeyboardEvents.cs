using XHTMLClassLibrary.Attributes.Events;
using XHTMLClassLibrary.BaseElements;

namespace XHTMLClassLibrary.Attributes.AttributeGroups
{
    /// <summary>
    /// 
    /// </summary>
    public class KeyboardEvents
    {
        [AttributeTypeAttributeMember(SupportedStandards = HTMLElementType.HTML5 | HTMLElementType.XHTML5 | HTMLElementType.XHTML11 | HTMLElementType.Transitional | HTMLElementType.Strict | HTMLElementType.FrameSet)]
        private readonly OnEventAttribute _onKeyDownEventAttribute = new OnEventAttribute("onkeydown");

        [AttributeTypeAttributeMember(SupportedStandards = HTMLElementType.HTML5 | HTMLElementType.XHTML5 | HTMLElementType.XHTML11 | HTMLElementType.Transitional | HTMLElementType.Strict | HTMLElementType.FrameSet)]
        private readonly OnEventAttribute _onKeyPressEventAttribute = new OnEventAttribute("onkeypress");

        [AttributeTypeAttributeMember(SupportedStandards = HTMLElementType.HTML5 | HTMLElementType.XHTML5 | HTMLElementType.XHTML11 | HTMLElementType.Transitional | HTMLElementType.Strict | HTMLElementType.FrameSet)]
        private readonly OnEventAttribute _onKeyUpEventAttribute = new OnEventAttribute("onkeyup"); 



        /// <summary>
        /// Fires when a user is pressing a key
        /// </summary>
        public OnEventAttribute OnKeyDownEvent { get { return _onKeyDownEventAttribute; }}

        /// <summary>
        /// Fires when a user presses a key
        /// </summary>
        public OnEventAttribute OnKeyPressEvent { get { return _onKeyPressEventAttribute; }}


        /// <summary>
        /// Fires when a user releases a key
        /// </summary>
        public OnEventAttribute OnKeyUpEvent   { get { return _onKeyUpEventAttribute; }}

    }
}
