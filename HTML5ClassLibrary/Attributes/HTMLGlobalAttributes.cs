using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml.Linq;

namespace HTML5ClassLibrary.Attributes
{
    /// <summary>
    /// This class contain a set of HTML (HTML5) global attributes
    /// </summary>
    public class HTMLGlobalAttributes
    {
        private readonly AccessKeyAttribute _accessKeyAttribute = new AccessKeyAttribute();
        private readonly ClassAttr _classAttribute = new ClassAttr();
        private readonly ContentEditableAttribute _contentEditableAttribute = new ContentEditableAttribute();
        private readonly ContextMenuAttribute _contextMenuAttribute = new ContextMenuAttribute();
        private readonly AnyDataAttribute _anyDataAttribute = new AnyDataAttribute();



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

        /// <summary>
        /// Used to store custom data private to the page or application
        /// </summary>
        public AnyDataAttribute DataAll { get { return _anyDataAttribute; }}


        /// <summary>
        /// Add all global attributes set to specified xElement
        /// </summary>
        /// <param name="xElement">element to store/write attributes to</param>
        public void AddAttributes(XElement xElement)
        {
            _accessKeyAttribute.AddAttribute(xElement);
            _classAttribute.AddAttribute(xElement);
            _contentEditableAttribute.AddAttribute(xElement);
            _contextMenuAttribute.AddAttribute(xElement);
            _anyDataAttribute.AddAttribute(xElement);
        }

        /// <summary>
        /// Loads all global attributes from provided xElement
        /// </summary>
        /// <param name="xElement">element to load attributes from</param>
        public void ReadAttributes(XElement xElement)
        {
            _accessKeyAttribute.ReadAttribute(xElement);
            _classAttribute.ReadAttribute(xElement);
            _contentEditableAttribute.ReadAttribute(xElement);
            _contextMenuAttribute.ReadAttribute(xElement);
            _anyDataAttribute.ReadAttribute(xElement);
        }
    }
}
