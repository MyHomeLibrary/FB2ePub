using XHTMLClassLibrary.AttributeDataTypes;
using XHTMLClassLibrary.BaseElements;

namespace XHTMLClassLibrary.Attributes.AttributeGroups
{
    /// <summary>
    /// This class contain a set of HTML (HTML5) global attributes
    /// </summary>
    public class HTMLGlobalAttributes
    {
        [AttributeTypeAttributeMember(Name = "accesskey", SupportedStandards = HTMLElementType.HTML5 | HTMLElementType.XHTML5 | HTMLElementType.XHTML11 | HTMLElementType.Transitional | HTMLElementType.Strict | HTMLElementType.FrameSet)]
        private readonly SimpleSingleTypeAttribute<Character> _accessKeyAttribute = new SimpleSingleTypeAttribute<Character>();
       
        [AttributeTypeAttributeMember(Name = "class", SupportedStandards = HTMLElementType.HTML5 | HTMLElementType.XHTML5 | HTMLElementType.XHTML11 | HTMLElementType.Transitional | HTMLElementType.Strict | HTMLElementType.FrameSet)]
        private readonly SimpleSingleTypeAttribute<NameTokens> _classAttribute = new SimpleSingleTypeAttribute<NameTokens>();
        
        [AttributeTypeAttributeMember(Name = "contenteditable", SupportedStandards = HTMLElementType.HTML5 | HTMLElementType.XHTML5)]
        private readonly ValuesSelectionTypeAttribute<Text> _contentEditableAttribute = new ValuesSelectionTypeAttribute<Text>("true;false");

        [AttributeTypeAttributeMember(Name = "contextmenu", SupportedStandards = HTMLElementType.HTML5 | HTMLElementType.XHTML5)]
        private readonly SimpleSingleTypeAttribute<Id> _contextMenuAttribute = new SimpleSingleTypeAttribute<Id>();
        
        //[AttributeTypeAttributeMember(SupportedStandards = HTMLElementType.HTML5 | HTMLElementType.XHTML5)]
        //private readonly AnyDataAttribute _anyDataAttribute = new AnyDataAttribute(); // not really global but contain custom elements that can appear on any element so we put it here 
        
        [AttributeTypeAttributeMember(Name = "dir", SupportedStandards = HTMLElementType.HTML5 | HTMLElementType.XHTML5 | HTMLElementType.XHTML11 | HTMLElementType.Transitional | HTMLElementType.Strict | HTMLElementType.FrameSet)]
        private readonly ValuesSelectionTypeAttribute<Text> _directionAttribute = new ValuesSelectionTypeAttribute<Text>("ltr;rtl;auto");
        
        [AttributeTypeAttributeMember(Name = "draggable", SupportedStandards = HTMLElementType.HTML5 | HTMLElementType.XHTML5)]
        private readonly ValuesSelectionTypeAttribute<Text> _draggable = new ValuesSelectionTypeAttribute<Text>("true;false;auto");

        [AttributeTypeAttributeMember(Name = "dropzone", SupportedStandards = HTMLElementType.HTML5 | HTMLElementType.XHTML5)]
        private readonly ValuesSelectionTypeAttribute<Text> _dropZoneAttribure = new ValuesSelectionTypeAttribute<Text>("copy;move;link");
        
        [AttributeTypeAttributeMember(Name = "hidden", SupportedStandards = HTMLElementType.HTML5 | HTMLElementType.XHTML5)]
        private readonly FlagTypeAttribute _hiddenAttribute = new FlagTypeAttribute();

        [AttributeTypeAttributeMember(Name = "id", SupportedStandards = HTMLElementType.HTML5 | HTMLElementType.XHTML5 | HTMLElementType.XHTML11 | HTMLElementType.Transitional | HTMLElementType.Strict | HTMLElementType.FrameSet)]
        private readonly SimpleSingleTypeAttribute<Id> _idAttribute = new SimpleSingleTypeAttribute<Id>();
        
        [AttributeTypeAttributeMember(Name = "lang", SupportedStandards = HTMLElementType.HTML5 | HTMLElementType.XHTML5 | HTMLElementType.XHTML11 | HTMLElementType.Transitional | HTMLElementType.Strict | HTMLElementType.FrameSet)]
        private readonly SimpleSingleTypeAttribute<LanguageCode> _languageAttribute = new SimpleSingleTypeAttribute<LanguageCode>();
        
        [AttributeTypeAttributeMember(Name = "spellcheck", SupportedStandards = HTMLElementType.HTML5 | HTMLElementType.XHTML5)]
        private readonly ValuesSelectionTypeAttribute<Text> _spellCheckAttribute = new ValuesSelectionTypeAttribute<Text>("true;false");

        [AttributeTypeAttributeMember(Name = "style", SupportedStandards = HTMLElementType.HTML5 | HTMLElementType.XHTML5 | HTMLElementType.XHTML11 | HTMLElementType.Transitional | HTMLElementType.Strict | HTMLElementType.FrameSet)]
        private readonly SimpleSingleTypeAttribute<Text> _styleAttribute = new SimpleSingleTypeAttribute<Text>();
        
        [AttributeTypeAttributeMember(Name = "tabindex", SupportedStandards = HTMLElementType.HTML5 | HTMLElementType.XHTML5 | HTMLElementType.XHTML11 | HTMLElementType.Transitional | HTMLElementType.Strict | HTMLElementType.FrameSet)]
        private readonly SimpleSingleTypeAttribute<Number> _tabIndexAttribute = new SimpleSingleTypeAttribute<Number>();

        [AttributeTypeAttributeMember(Name = "title", SupportedStandards = HTMLElementType.HTML5 | HTMLElementType.XHTML5 | HTMLElementType.XHTML11 | HTMLElementType.Transitional | HTMLElementType.Strict | HTMLElementType.FrameSet)]
        private readonly SimpleSingleTypeAttribute<Text> _titleAttribute = new SimpleSingleTypeAttribute<Text>();
        
        [AttributeTypeAttributeMember(Name = "translate", SupportedStandards = HTMLElementType.HTML5 | HTMLElementType.XHTML5)]
        private readonly ValuesSelectionTypeAttribute<Text> _translateAttribute = new ValuesSelectionTypeAttribute<Text>("yes;no");


        /// <summary>
        /// Specifies a shortcut key to activate/focus an element
        /// </summary>
        public IAttributeDataAccess AccessKey { get { return _accessKeyAttribute; }}

        /// <summary>
        /// Specifies one or more classnames for an element (refers to a class in a style sheet)
        /// </summary>
        public IAttributeDataAccess Class { get { return _classAttribute; } }

        /// <summary>
        /// Specifies whether the content of an element is editable or not
        /// </summary>
        public IAttributeDataAccess ContentEditable { get { return _contentEditableAttribute; } }

        /// <summary>
        /// Specifies a context menu for an element. The context menu appears when a user right-clicks on the element 
        /// </summary>
        public IAttributeDataAccess ContextMenu { get { return _contextMenuAttribute; } }

        ///// <summary>
        ///// Used to store custom data private to the page or application
        ///// </summary>
        //public IAttributeDataAccess DataAll { get { return _anyDataAttribute; }}

        /// <summary>
        /// Specifies the text direction for the content in an element
        /// </summary>
        public IAttributeDataAccess Direction { get { return _directionAttribute; } }

        /// <summary>
        /// Specifies whether an element is draggable or not
        /// </summary>
        public IAttributeDataAccess Draggable { get { return _draggable; } }


        /// <summary>
        /// Specifies whether the dragged data is copied, moved, or linked, when dropped
        /// </summary>
        public IAttributeDataAccess DropZone { get { return _dropZoneAttribure; } }

        /// <summary>
        /// Specifies that an element is not yet, or is no longer, relevant
        /// </summary>
        public IAttributeDataAccess Hidden { get { return _hiddenAttribute; } }

        /// <summary>
        /// Specifies a unique id for an element
        /// </summary>
        public IAttributeDataAccess ID { get { return _idAttribute; } }

        /// <summary>
        /// Specifies the language of the element's content
        /// </summary>
        public IAttributeDataAccess Language { get { return _languageAttribute; } }

        /// <summary>
        /// Specifies whether the element is to have its spelling and grammar checked or not
        /// </summary>
        public IAttributeDataAccess SpellCheck { get { return _spellCheckAttribute; } }

        /// <summary>
        /// Specifies an inline CSS style for an element
        /// </summary>
        public IAttributeDataAccess Style { get { return _styleAttribute; } }


        /// <summary>
        /// Specifies the tabbing order of an element
        /// </summary>
        public IAttributeDataAccess TabIndex { get { return _tabIndexAttribute; } }

        /// <summary>C:\Project\GoogleCode\fb2epub\HTML5ClassLibrary\Attributes\XMLSpaceAttribute.cs
        /// Specifies extra information about an element
        /// </summary>
        public IAttributeDataAccess Title { get { return _titleAttribute; } }


        /// <summary>
        /// Specifies whether the content of an element should be translated or not
        /// </summary>
        public IAttributeDataAccess Translate { get { return _translateAttribute; } }

    }
}
