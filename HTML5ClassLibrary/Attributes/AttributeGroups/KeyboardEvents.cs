using XHTMLClassLibrary.Attributes.Events;
using XHTMLClassLibrary.BaseElements;

namespace XHTMLClassLibrary.Attributes.AttributeGroups.KeyboardEvents
{
    /// <summary>
    /// 
    /// </summary>
    public class KeyboardEvents
    {
        [AttributeTypeAttributeMember(Name = "onkeydown", SupportedStandards = HTMLElementType.HTML5 | HTMLElementType.XHTML5 | HTMLElementType.XHTML11 | HTMLElementType.Transitional | HTMLElementType.Strict | HTMLElementType.FrameSet)]
        private readonly OnEventAttribute _onKeyDownEventAttribute = new OnEventAttribute();

        [AttributeTypeAttributeMember(Name = "onkeypress", SupportedStandards = HTMLElementType.HTML5 | HTMLElementType.XHTML5 | HTMLElementType.XHTML11 | HTMLElementType.Transitional | HTMLElementType.Strict | HTMLElementType.FrameSet)]
        private readonly OnEventAttribute _onKeyPressEventAttribute = new OnEventAttribute();

        [AttributeTypeAttributeMember(Name = "onkeyup", SupportedStandards = HTMLElementType.HTML5 | HTMLElementType.XHTML5 | HTMLElementType.XHTML11 | HTMLElementType.Transitional | HTMLElementType.Strict | HTMLElementType.FrameSet)]
        private readonly OnEventAttribute _onKeyUpEventAttribute = new OnEventAttribute(); 



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
