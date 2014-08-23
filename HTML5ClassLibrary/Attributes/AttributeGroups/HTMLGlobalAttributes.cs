﻿using XHTMLClassLibrary.BaseElements;

namespace XHTMLClassLibrary.Attributes.AttributeGroups
{
    /// <summary>
    /// This class contain a set of HTML (HTML5) global attributes
    /// </summary>
    public class HTMLGlobalAttributes
    {
        [AttributeTypeAttributeMember(Name = "accesskey", SupportedStandards = HTMLElementType.HTML5 | HTMLElementType.XHTML5 | HTMLElementType.XHTML11 | HTMLElementType.Transitional | HTMLElementType.Strict | HTMLElementType.FrameSet)]
        private readonly CharAttribute _accessKeyAttribute = new CharAttribute();
       
        [AttributeTypeAttributeMember(Name = "class", SupportedStandards = HTMLElementType.HTML5 | HTMLElementType.XHTML5 | HTMLElementType.XHTML11 | HTMLElementType.Transitional | HTMLElementType.Strict | HTMLElementType.FrameSet)]
        private readonly NameTokensTypeAttribute _classAttribute = new NameTokensTypeAttribute();
        
        [AttributeTypeAttributeMember(Name = "contenteditable", SupportedStandards = HTMLElementType.HTML5 | HTMLElementType.XHTML5)]
        private readonly BooleanSimpleTypeAttribute _contentEditableAttribute = new BooleanSimpleTypeAttribute();

        [AttributeTypeAttributeMember(Name = "contextmenu", SupportedStandards = HTMLElementType.HTML5 | HTMLElementType.XHTML5)]
        private readonly IdTypeAttribute _contextMenuAttribute = new IdTypeAttribute();
        
        //[AttributeTypeAttributeMember(SupportedStandards = HTMLElementType.HTML5 | HTMLElementType.XHTML5)]
        //private readonly AnyDataAttribute _anyDataAttribute = new AnyDataAttribute(); // not really global but contain custom elements that can appear on any element so we put it here 
        
        [AttributeTypeAttributeMember(Name = "dir", SupportedStandards = HTMLElementType.HTML5 | HTMLElementType.XHTML5 | HTMLElementType.XHTML11 | HTMLElementType.Transitional | HTMLElementType.Strict | HTMLElementType.FrameSet)]
        private readonly DirectionTypeAttribute _directionAttribute = new DirectionTypeAttribute();
        
        [AttributeTypeAttributeMember(Name = "draggable", SupportedStandards = HTMLElementType.HTML5 | HTMLElementType.XHTML5)]
        private readonly BooleanAutoTypeAttribute _draggable = new BooleanAutoTypeAttribute();

        [AttributeTypeAttributeMember(Name = "dropzone", SupportedStandards = HTMLElementType.HTML5 | HTMLElementType.XHTML5)]
        private readonly DropZoneTypeAttribute _dropZoneAttribure = new DropZoneTypeAttribute();
        
        [AttributeTypeAttributeMember(Name = "hidden", SupportedStandards = HTMLElementType.HTML5 | HTMLElementType.XHTML5)]
        private readonly FlagTypeAttribute _hiddenAttribute = new FlagTypeAttribute();

        [AttributeTypeAttributeMember(Name = "id", SupportedStandards = HTMLElementType.HTML5 | HTMLElementType.XHTML5 | HTMLElementType.XHTML11 | HTMLElementType.Transitional | HTMLElementType.Strict | HTMLElementType.FrameSet)]
        private readonly IdTypeAttribute _idAttribute = new IdTypeAttribute();
        
        [AttributeTypeAttributeMember(Name = "lang", SupportedStandards = HTMLElementType.HTML5 | HTMLElementType.XHTML5 | HTMLElementType.XHTML11 | HTMLElementType.Transitional | HTMLElementType.Strict | HTMLElementType.FrameSet)]
        private readonly LanguagesTypeAttribute _languageAttribute = new LanguagesTypeAttribute();
        
        [AttributeTypeAttributeMember(Name = "spellcheck", SupportedStandards = HTMLElementType.HTML5 | HTMLElementType.XHTML5)]
        private readonly BooleanSimpleTypeAttribute _spellCheckAttribute = new BooleanSimpleTypeAttribute();

        [AttributeTypeAttributeMember(Name = "style", SupportedStandards = HTMLElementType.HTML5 | HTMLElementType.XHTML5 | HTMLElementType.XHTML11 | HTMLElementType.Transitional | HTMLElementType.Strict | HTMLElementType.FrameSet)]
        private readonly TextValueTypeAttribute _styleAttribute = new TextValueTypeAttribute();
        
        [AttributeTypeAttributeMember(Name = "tabindex", SupportedStandards = HTMLElementType.HTML5 | HTMLElementType.XHTML5 | HTMLElementType.XHTML11 | HTMLElementType.Transitional | HTMLElementType.Strict | HTMLElementType.FrameSet)]
        private readonly NumberTypeAttribute _tabIndexAttribute = new NumberTypeAttribute();

        [AttributeTypeAttributeMember(Name = "title", SupportedStandards = HTMLElementType.HTML5 | HTMLElementType.XHTML5 | HTMLElementType.XHTML11 | HTMLElementType.Transitional | HTMLElementType.Strict | HTMLElementType.FrameSet)]
        private readonly TextValueTypeAttribute _titleAttribute = new TextValueTypeAttribute();
        
        [AttributeTypeAttributeMember(Name = "translate", SupportedStandards = HTMLElementType.HTML5 | HTMLElementType.XHTML5)]
        private readonly YesNoTypeAttribute _translateAttribute = new YesNoTypeAttribute();


        /// <summary>
        /// Specifies a shortcut key to activate/focus an element
        /// </summary>
        public CharAttribute AccessKey { get { return _accessKeyAttribute; }}

        /// <summary>
        /// Specifies one or more classnames for an element (refers to a class in a style sheet)
        /// </summary>
        public NameTokensTypeAttribute Class { get { return _classAttribute; }}

        /// <summary>
        /// Specifies whether the content of an element is editable or not
        /// </summary>
        public BooleanSimpleTypeAttribute ContentEditable { get { return _contentEditableAttribute; }}

        /// <summary>
        /// Specifies a context menu for an element. The context menu appears when a user right-clicks on the element 
        /// </summary>
        public IdTypeAttribute ContextMenu {get { return _contextMenuAttribute; }}

        ///// <summary>
        ///// Used to store custom data private to the page or application
        ///// </summary>
        //public AnyDataAttribute DataAll { get { return _anyDataAttribute; }}

        /// <summary>
        /// Specifies the text direction for the content in an element
        /// </summary>
        public DirectionTypeAttribute Direction { get { return _directionAttribute; }}

        /// <summary>
        /// Specifies whether an element is draggable or not
        /// </summary>
        public BooleanAutoTypeAttribute Draggable { get { return _draggable; }}


        /// <summary>
        /// Specifies whether the dragged data is copied, moved, or linked, when dropped
        /// </summary>
        public DropZoneTypeAttribute DropZone { get { return _dropZoneAttribure; }}

        /// <summary>
        /// Specifies that an element is not yet, or is no longer, relevant
        /// </summary>
        public FlagTypeAttribute Hidden { get { return _hiddenAttribute; }}

        /// <summary>
        /// Specifies a unique id for an element
        /// </summary>
        public IdTypeAttribute ID { get { return _idAttribute; }}

        /// <summary>
        /// Specifies the language of the element's content
        /// </summary>
        public LanguagesTypeAttribute Language { get { return _languageAttribute; }}

        /// <summary>
        /// Specifies whether the element is to have its spelling and grammar checked or not
        /// </summary>
        public BooleanSimpleTypeAttribute SpellCheck { get { return _spellCheckAttribute; }}

        /// <summary>
        /// Specifies an inline CSS style for an element
        /// </summary>
        public TextValueTypeAttribute Style { get { return _styleAttribute; }}


        /// <summary>
        /// Specifies the tabbing order of an element
        /// </summary>
        public NumberTypeAttribute TabIndex { get { return _tabIndexAttribute; }}

        /// <summary>C:\Project\GoogleCode\fb2epub\HTML5ClassLibrary\Attributes\XMLSpaceAttribute.cs
        /// Specifies extra information about an element
        /// </summary>
        public TextValueTypeAttribute Title { get { return _titleAttribute; }}


        /// <summary>
        /// Specifies whether the content of an element should be translated or not
        /// </summary>
        public YesNoTypeAttribute Translate { get { return _translateAttribute; }}

    }
}