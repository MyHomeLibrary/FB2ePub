using System.Collections.Generic;
using XHTMLClassLibrary.Attributes.FlaggedAttributes;
using XHTMLClassLibrary.BaseElements;

namespace XHTMLClassLibrary.Attributes.AttributeGroups.HTMLGlobal
{
    /// <summary>
    /// This class contain a set of HTML (HTML5) global attributes
    /// </summary>
    public class HTMLGlobalAttributes
    {
        [AttributeTypeAttributeMember(SupportedStandards = HTMLElementType.HTML5 |  HTMLElementType.XHTML5 | HTMLElementType.XHTML11 | HTMLElementType.Transitional | HTMLElementType.Strict | HTMLElementType.FrameSet)]
        private readonly AccessKeyAttribute _accessKeyAttribute = new AccessKeyAttribute();
       
        [AttributeTypeAttributeMember(SupportedStandards = HTMLElementType.HTML5 | HTMLElementType.XHTML5 | HTMLElementType.XHTML11 | HTMLElementType.Transitional | HTMLElementType.Strict | HTMLElementType.FrameSet)]
        private readonly ClassAttr _classAttribute = new ClassAttr();
        
        [AttributeTypeAttributeMember(SupportedStandards = HTMLElementType.HTML5 | HTMLElementType.XHTML5)]
        private readonly ContentEditableAttribute _contentEditableAttribute = new ContentEditableAttribute();
        
        [AttributeTypeAttributeMember(SupportedStandards = HTMLElementType.HTML5 | HTMLElementType.XHTML5)]
        private readonly ContextMenuAttribute _contextMenuAttribute = new ContextMenuAttribute();
        
        //[AttributeTypeAttributeMember(SupportedStandards = HTMLElementType.HTML5 | HTMLElementType.XHTML5)]
        //private readonly AnyDataAttribute _anyDataAttribute = new AnyDataAttribute(); // not really global but contain custom elements that can appear on any element so we put it here 
        
        [AttributeTypeAttributeMember(SupportedStandards = HTMLElementType.HTML5 | HTMLElementType.XHTML5 | HTMLElementType.XHTML11 | HTMLElementType.Transitional | HTMLElementType.Strict | HTMLElementType.FrameSet)]
        private readonly DirectionAttribute _directionAttribute = new DirectionAttribute();
        
        [AttributeTypeAttributeMember(SupportedStandards = HTMLElementType.HTML5 | HTMLElementType.XHTML5)]
        private readonly DraggableAttribute _draggable = new DraggableAttribute();
        
        [AttributeTypeAttributeMember(SupportedStandards = HTMLElementType.HTML5 | HTMLElementType.XHTML5)]
        private readonly DropZoneAttribute _dropZoneAttribure = new DropZoneAttribute();
        
        [AttributeTypeAttributeMember(Name = "hidden", SupportedStandards = HTMLElementType.HTML5 | HTMLElementType.XHTML5)]
        private readonly FlagAttribute _hiddenAttribute = new FlagAttribute();
        
        [AttributeTypeAttributeMember(SupportedStandards = HTMLElementType.HTML5 | HTMLElementType.XHTML5 | HTMLElementType.XHTML11 | HTMLElementType.Transitional | HTMLElementType.Strict | HTMLElementType.FrameSet)]
        private readonly IdAttribute _idAttribute = new IdAttribute();
        
        [AttributeTypeAttributeMember(SupportedStandards = HTMLElementType.HTML5 | HTMLElementType.XHTML5 | HTMLElementType.XHTML11 | HTMLElementType.Transitional | HTMLElementType.Strict | HTMLElementType.FrameSet)]
        private readonly LanguageAttribute _languageAttribute = new LanguageAttribute();
        
        [AttributeTypeAttributeMember(SupportedStandards = HTMLElementType.HTML5 | HTMLElementType.XHTML5)]
        private readonly SpellCheckAttribute _spellCheckAttribute = new SpellCheckAttribute();

        [AttributeTypeAttributeMember(Name = "style", SupportedStandards = HTMLElementType.HTML5 | HTMLElementType.XHTML5 | HTMLElementType.XHTML11 | HTMLElementType.Transitional | HTMLElementType.Strict | HTMLElementType.FrameSet)]
        private readonly TextValueAttribute _styleAttribute = new TextValueAttribute();
        
        [AttributeTypeAttributeMember(SupportedStandards = HTMLElementType.HTML5 | HTMLElementType.XHTML5 | HTMLElementType.XHTML11 | HTMLElementType.Transitional | HTMLElementType.Strict | HTMLElementType.FrameSet)]
        private readonly TabIndexAttribute _tabIndexAttribute = new TabIndexAttribute();

        [AttributeTypeAttributeMember(Name = "title", SupportedStandards = HTMLElementType.HTML5 | HTMLElementType.XHTML5 | HTMLElementType.XHTML11 | HTMLElementType.Transitional | HTMLElementType.Strict | HTMLElementType.FrameSet)]
        private readonly TextValueAttribute _titleAttribute = new TextValueAttribute();
        
        [AttributeTypeAttributeMember(SupportedStandards = HTMLElementType.HTML5 | HTMLElementType.XHTML5)]
        private readonly TranslateAttribute _translateAttribute = new TranslateAttribute();


        /// <summary>
        /// Specifies a shortcut key to activate/focus an element
        /// </summary>
        public AccessKeyAttribute AccessKey { get { return _accessKeyAttribute; }}

        /// <summary>
        /// Specifies one or more classnames for an element (refers to a class in a style sheet)
        /// </summary>
        public ClassAttr Class { get { return _classAttribute; }}

        /// <summary>
        /// Specifies whether the content of an element is editable or not
        /// </summary>
        public ContentEditableAttribute ContentEditable { get { return _contentEditableAttribute; }}

        /// <summary>
        /// Specifies a context menu for an element. The context menu appears when a user right-clicks on the element 
        /// </summary>
        public ContextMenuAttribute ContextMenu {get { return _contextMenuAttribute; }}

        ///// <summary>
        ///// Used to store custom data private to the page or application
        ///// </summary>
        //public AnyDataAttribute DataAll { get { return _anyDataAttribute; }}

        /// <summary>
        /// Specifies the text direction for the content in an element
        /// </summary>
        public DirectionAttribute Direction { get { return _directionAttribute; }}

        /// <summary>
        /// Specifies whether an element is draggable or not
        /// </summary>
        public DraggableAttribute Draggable { get { return _draggable; }}


        /// <summary>
        /// Specifies whether the dragged data is copied, moved, or linked, when dropped
        /// </summary>
        public DropZoneAttribute DropZone { get { return _dropZoneAttribure; }}

        /// <summary>
        /// Specifies that an element is not yet, or is no longer, relevant
        /// </summary>
        public FlagAttribute Hidden { get { return _hiddenAttribute; }}

        /// <summary>
        /// Specifies a unique id for an element
        /// </summary>
        public IdAttribute ID { get { return _idAttribute; }}

        /// <summary>
        /// Specifies the language of the element's content
        /// </summary>
        public LanguageAttribute Language { get { return _languageAttribute; }}

        /// <summary>
        /// Specifies whether the element is to have its spelling and grammar checked or not
        /// </summary>
        public SpellCheckAttribute SpellCheck { get { return _spellCheckAttribute; }}

        /// <summary>
        /// Specifies an inline CSS style for an element
        /// </summary>
        public TextValueAttribute Style { get { return _styleAttribute; }}


        /// <summary>
        /// Specifies the tabbing order of an element
        /// </summary>
        public TabIndexAttribute TabIndex { get { return _tabIndexAttribute; }}

        /// <summary>C:\Project\GoogleCode\fb2epub\HTML5ClassLibrary\Attributes\XMLSpaceAttribute.cs
        /// Specifies extra information about an element
        /// </summary>
        public TextValueAttribute Title { get { return _titleAttribute; }}


        /// <summary>
        /// Specifies whether the content of an element should be translated or not
        /// </summary>
        public TranslateAttribute Translate { get { return _translateAttribute; }}

    }
}
