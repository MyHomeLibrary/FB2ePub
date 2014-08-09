using System.Collections.Generic;
using System.Xml.Linq;
using HTML5ClassLibrary.Attributes;

namespace HTML5ClassLibrary.BaseElements.InlineElements
{
    abstract public class BaseInlineItem : IInlineItem, ICommonAttributes
    {
        // Common core attributes
        private readonly ClassAttr _classattr = new ClassAttr();
        private readonly IdAttribute _idattr = new IdAttribute();
        private readonly TitleAttribute _titleattr = new TitleAttribute();

        private readonly StyleAttribute _styleAttr = new StyleAttribute();

        public static XNamespace XhtmlNameSpace = @"http://www.w3.org/1999/xhtml";

        #region public_properties

        /// <summary>
        /// This attribute assigns a class name or set of class names to an element. 
        /// Any number of elements may be assigned the same class name or set of class names. 
        /// Multiple class names must be separated by white space characters. 
        /// Class names are typically used to apply CSS formatting rules to an element.
        /// </summary>
        public ClassAttr Class
        {
            get { return _classattr; }
        }


        /// <summary>
        /// This attribute assigns an ID to an element. 
        /// This ID must be unique in a document. 
        /// This ID can be used by client-side scripts (such as JavaScript) to select elements, apply CSS formatting rules, or to build relationships between elements.
        /// </summary>
        public IdAttribute ID
        {
            get { return _idattr; }
        }

        /// <summary>
        /// This attribute offers advisory information. 
        /// Some Web browsers will display this information as tooltips. 
        /// Assistive technologies may make this information available to users as additional information about the element.
        /// </summary>
        public TitleAttribute Title
        {
            get { return _titleattr; }
        }


        /// <summary>
        /// This attribute specifies formatting style information for the current element. 
        /// The content of this attribute is called inline CSS. The style attribute is deprecated (considered outdated), 
        /// because it fuses together content and formatting.
        /// </summary>
        public StyleAttribute Style { get { return _styleAttr; } }

        #endregion

        protected virtual void AddAttributes(XElement xElement)
        {
            _classattr.AddAttribute(xElement);
            _idattr.AddAttribute(xElement);
            _titleattr.AddAttribute(xElement);


            _styleAttr.AddAttribute(xElement);

        }

        protected virtual void ReadAttributes(XElement xElement)
        {
            _classattr.ReadAttribute(xElement);
            _idattr.ReadAttribute(xElement);
            _titleattr.ReadAttribute(xElement);

            _styleAttr.ReadAttribute(xElement);
        }


        public abstract void Load(XNode xNode);
        public abstract XNode Generate();
        public abstract bool IsValid();

        /// <summary>
        /// Adds sub item to the item , only if 
        /// allowed by the rules and element can accept content
        /// </summary>
        /// <param name="item">sub item to add</param>
        public abstract void Add(IHTML5Item item);

        public abstract void Remove(IHTML5Item item);

        public abstract List<IHTML5Item> SubElements();

        /// <summary>
        /// Get/Set item parent in the XHTML "tree"
        /// </summary>
        public IHTML5Item Parent { get; set; }
    }
}
