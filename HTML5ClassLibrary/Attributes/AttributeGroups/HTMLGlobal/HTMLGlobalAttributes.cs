using System.Collections.Generic;
using System.Xml.Linq;
using HTMLClassLibrary.Attributes.FlaggedAttributes;
using HTMLClassLibrary.BaseElements;

namespace HTMLClassLibrary.Attributes.AttributeGroups.HTMLGlobal
{
    /// <summary>
    /// This class contain a set of HTML (HTML5) global attributes
    /// </summary>
    public class HTMLGlobalAttributes
    {
        private readonly AccessKeyAttribute _accessKeyAttribute = new AccessKeyAttribute();
        private readonly ClassAttr _classAttribute = new ClassAttr();
        private readonly ContentEditableAttribute _contentEditableAttribute = new ContentEditableAttribute();
        private readonly DirectionAttribute _directionAttribute = new DirectionAttribute();
        private readonly DraggableAttribute _draggable = new DraggableAttribute();
        private readonly DropZoneAttribute _dropZoneAttribure = new DropZoneAttribute();
        private readonly ContextMenuAttribute _contextMenuAttribute = new ContextMenuAttribute();
        //private readonly AnyDataAttribute _anyDataAttribute = new AnyDataAttribute(); // not really global but contain custom elements that can appear on any element so we put it here 
        private readonly HiddenAttribute _hiddenAttribute = new HiddenAttribute();
        private readonly IdAttribute _idAttribute = new IdAttribute();
        private readonly LanguageAttribute _languageAttribute = new LanguageAttribute();
        private readonly SpellCheckAttribute _spellCheckAttribute = new SpellCheckAttribute();
        private readonly StyleAttribute _styleAttribute = new StyleAttribute();
        private readonly TabIndexAttribute _tabIndexAttribute = new TabIndexAttribute();
        private readonly TitleAttribute _titleAttribute = new TitleAttribute();
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
        public HiddenAttribute Hidden { get { return _hiddenAttribute; }}

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
        public StyleAttribute Style { get { return _styleAttribute; }}


        /// <summary>
        /// Specifies the tabbing order of an element
        /// </summary>
        public TabIndexAttribute TabIndex { get { return _tabIndexAttribute; }}

        /// <summary>C:\Project\GoogleCode\fb2epub\HTML5ClassLibrary\Attributes\XMLSpaceAttribute.cs
        /// Specifies extra information about an element
        /// </summary>
        public TitleAttribute Title { get { return _titleAttribute; }}


        /// <summary>
        /// Specifies whether the content of an element should be translated or not
        /// </summary>
        public TranslateAttribute Translate { get { return _translateAttribute; }}


        /// <summary>
        /// Add all global attributes set to specified xElement
        /// </summary>
        /// <param name="attributesList"></param>
        public void AddAttributes(List<IBaseAttribute> attributesList)
        {
            attributesList.Add(_accessKeyAttribute);
            attributesList.Add(_classAttribute);
            attributesList.Add(_contentEditableAttribute);
            attributesList.Add(_contextMenuAttribute);
            //attributesList.Add(_anyDataAttribute);
            attributesList.Add(_directionAttribute);
            attributesList.Add(_draggable);
            attributesList.Add(_dropZoneAttribure);
            attributesList.Add(_hiddenAttribute);
            attributesList.Add(_idAttribute);
            attributesList.Add(_languageAttribute);
            attributesList.Add(_spellCheckAttribute);
            attributesList.Add(_styleAttribute);
            attributesList.Add(_tabIndexAttribute);
            attributesList.Add(_titleAttribute);
            attributesList.Add(_translateAttribute);
        }

    }
}
