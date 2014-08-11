using System.Collections.Generic;
using System.Xml.Linq;
using HTML5ClassLibrary.Attributes;
using HTML5ClassLibrary.Attributes.AttributeGroups.HTMLGlobal;
using HTML5ClassLibrary.Attributes.AttributeGroups.KeyboardEvents;
using HTML5ClassLibrary.Attributes.AttributeGroups.MouseEvents;
using HTML5ClassLibrary.Attributes.Events;

namespace HTML5ClassLibrary.BaseElements.FormMenuOptions
{
    public interface IOptionItem : IHTML5Item
    {

    }

    public abstract class BaseOptionItem : IOptionItem, ICommonAttributes
    {
        // Common core attributes
        private readonly ClassAttr _classattr = new ClassAttr();
        private readonly IdAttribute _idattr = new IdAttribute();
        private readonly TitleAttribute _titleattr = new TitleAttribute();


        private readonly LanguageAttribute _language = new LanguageAttribute();
        private readonly DirectionAttribute _direction = new DirectionAttribute();

        // Common event attributes
        private readonly OnClickEventAttribute _onClick = new OnClickEventAttribute();
        private readonly OnDblClickEventAttribute _onDblClick = new OnDblClickEventAttribute();
        private readonly OnMouseDownEventAttribute _onMouseDown = new OnMouseDownEventAttribute();
        private readonly OnMouseUpEventAttribute _onMouseUp = new OnMouseUpEventAttribute();
        private readonly OnMouseOverEventAttribute _onMouseOver = new OnMouseOverEventAttribute();
        private readonly OnMouseMoveEventAttribute _onMouseMove = new OnMouseMoveEventAttribute();
        private readonly OnMouseOutEventAttribute _onMouseOut = new OnMouseOutEventAttribute();
        private readonly OnKeyPressEventAttribute _onKeyPress = new OnKeyPressEventAttribute();
        private readonly OnKeyDownEventAttribute _onKeyDown = new OnKeyDownEventAttribute();
        private readonly OnKeyUpEventAttribute _onKeyUp = new OnKeyUpEventAttribute();

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


        /// <summary>
        /// This attribute specifies the base direction of text. 
        /// Possible values:
        /// ltr: Left-to-right 
        /// rtl: Right-to-left
        /// </summary>
        public DirectionAttribute Direction
        {
            get { return _direction; }
        }

        /// <summary>
        /// A client-side script event that occurs when a pointing device button is clicked over an element.
        /// </summary>
        public OnClickEventAttribute OnClick
        {
            get { return _onClick; }
        }


        /// <summary>
        /// A client-side script event that occurs when a pointing device button is double-clicked over an element.
        /// </summary>
        public OnDblClickEventAttribute OnDblClick { get { return _onDblClick; } }


        /// <summary>
        /// A client-side script event that occurs when a pointing device button is pressed down over an element.
        /// </summary>
        public OnMouseDownEventAttribute OnMouseDown { get { return _onMouseDown; } }

        /// <summary>
        /// A client-side script event that occurs when a pointing device button is released over an element.
        /// </summary>
        public OnMouseUpEventAttribute OnMouseUp { get { return _onMouseUp; } }


        /// <summary>
        /// A client-side script event that occurs when a pointing device is moved onto an element.
        /// </summary>
        public OnMouseOverEventAttribute OnMouseOver { get { return _onMouseOver; } }

        /// <summary>
        /// A client-side script event that occurs when a pointing device is moved within an element.
        /// </summary>
        public OnMouseMoveEventAttribute OnMouseMove { get { return _onMouseMove; } }


        /// <summary>
        /// A client-side script event that occurs when a pointing device is moved away from an element.
        /// </summary>
        public OnMouseOutEventAttribute OnMouseOut { get { return _onMouseOut; } }

        /// <summary>
        /// A client-side script event that occurs when a key is pressed down over an element then released.
        /// </summary>
        public OnKeyPressEventAttribute OnKeyPress { get { return _onKeyPress; } }

        /// <summary>
        /// A client-side script event that occurs when a key is pressed down over an element.
        /// </summary>
        public OnKeyDownEventAttribute OnKeyDown { get { return _onKeyDown; } }

        /// <summary>
        /// A client-side script event that occurs when a key is released over an element.
        /// </summary>
        public OnKeyUpEventAttribute OnKeyUp { get { return _onKeyUp; } }

        /// <summary>
        /// This attribute specifies the base language of an element's attribute values and text content.
        /// </summary>
        public LanguageAttribute Language
        {
            get { return _language; }
        }

        #endregion


        protected virtual void AddAttributes(XElement xElement)
        {
            _classattr.AddAttribute(xElement);
            _idattr.AddAttribute(xElement);
            _titleattr.AddAttribute(xElement);

            _language.AddAttribute(xElement);
            _direction.AddAttribute(xElement);

            _onClick.AddAttribute(xElement);
            _onDblClick.AddAttribute(xElement);
            _onMouseDown.AddAttribute(xElement);
            _onMouseUp.AddAttribute(xElement);
            _onMouseOver.AddAttribute(xElement);
            _onMouseMove.AddAttribute(xElement);
            _onMouseOut.AddAttribute(xElement);
            _onKeyPress.AddAttribute(xElement);
            _onKeyDown.AddAttribute(xElement);
            _onKeyUp.AddAttribute(xElement);


            _styleAttr.AddAttribute(xElement);

        }

        protected virtual void ReadAttributes(XElement xElement)
        {
            _classattr.ReadAttribute(xElement);
            _idattr.ReadAttribute(xElement);
            _titleattr.ReadAttribute(xElement);

            _language.ReadAttribute(xElement);
            _direction.ReadAttribute(xElement);

            _onClick.ReadAttribute(xElement);
            _onDblClick.ReadAttribute(xElement);
            _onMouseDown.ReadAttribute(xElement);
            _onMouseUp.ReadAttribute(xElement);
            _onMouseOver.ReadAttribute(xElement);
            _onMouseMove.ReadAttribute(xElement);
            _onMouseOut.ReadAttribute(xElement);
            _onKeyPress.ReadAttribute(xElement);
            _onKeyDown.ReadAttribute(xElement);
            _onKeyUp.ReadAttribute(xElement);

            _styleAttr.ReadAttribute(xElement);
        }

        public abstract void Load(XNode xNode);
        public abstract XNode Generate();
        public abstract bool IsValid();

        /// <summary>
        /// Adds subitem to the item , only if 
        /// allowed by the rules and element can accept content
        /// </summary>
        /// <param name="item">subitem to add</param>
        public abstract void Add(IHTML5Item item);

        public abstract void Remove(IHTML5Item item);

        public abstract List<IHTML5Item> SubElements();

        /// <summary>
        /// Get/Set item parent in the XHTML "tree"
        /// </summary>
        public IHTML5Item Parent { get; set; }
    }
}
