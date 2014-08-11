using System;
using System.Collections.Generic;
using System.Xml;
using System.Xml.Linq;
using HTML5ClassLibrary.Attributes;
using HTML5ClassLibrary.Attributes.AttributeGroups.HTMLGlobal;
using HTML5ClassLibrary.Attributes.AttributeGroups.KeyboardEvents;
using HTML5ClassLibrary.Attributes.AttributeGroups.MouseEvents;
using HTML5ClassLibrary.Attributes.Events;
using HTML5ClassLibrary.Exceptions;

namespace HTML5ClassLibrary.BaseElements.InlineElements
{
    public abstract class TextBasedElement : BaseInlineItem
    {
        /// <summary>
        /// Internal content of the element
        /// </summary>
        protected readonly List<IHTML5Item> Content = new List<IHTML5Item>();

        protected abstract string GetElementName();

        protected readonly LanguageAttribute LanguageAttr = new LanguageAttribute();
        protected readonly DirectionAttribute DirectionAttr = new DirectionAttribute();

        // Common event attributes
        protected readonly OnClickEventAttribute OnClickEvent = new OnClickEventAttribute();
        protected readonly OnDblClickEventAttribute OnDblClickEvent = new OnDblClickEventAttribute();
        protected readonly OnMouseDownEventAttribute OnMouseDownEvent = new OnMouseDownEventAttribute();
        protected readonly OnMouseUpEventAttribute OnMouseUpEvent = new OnMouseUpEventAttribute();
        protected readonly OnMouseOverEventAttribute OnMouseOverEvent = new OnMouseOverEventAttribute();
        protected readonly OnMouseMoveEventAttribute OnMouseMoveEvent = new OnMouseMoveEventAttribute();
        protected readonly OnMouseOutEventAttribute OnMouseOutEvent = new OnMouseOutEventAttribute();
        protected readonly OnKeyPressEventAttribute OnKeyPressEvent = new OnKeyPressEventAttribute();
        protected readonly OnKeyDownEventAttribute OnKeyDownEvent = new OnKeyDownEventAttribute();
        protected readonly OnKeyUpEventAttribute OnKeyUpEvent = new OnKeyUpEventAttribute();



        /// <summary>
        /// This attribute specifies the base direction of text. 
        /// Possible values:
        /// ltr: Left-to-right 
        /// rtl: Right-to-left
        /// </summary>
        public DirectionAttribute Direction
        {
            get { return DirectionAttr; }
        }

        /// <summary>
        /// A client-side script event that occurs when a pointing device button is clicked over an element.
        /// </summary>
        public OnClickEventAttribute OnClick
        {
            get { return OnClickEvent; }
        }


        /// <summary>
        /// A client-side script event that occurs when a pointing device button is double-clicked over an element.
        /// </summary>
        public OnDblClickEventAttribute OnDblClick { get { return OnDblClickEvent; } }


        /// <summary>
        /// A client-side script event that occurs when a pointing device button is pressed down over an element.
        /// </summary>
        public OnMouseDownEventAttribute OnMouseDown { get { return OnMouseDownEvent; } }

        /// <summary>
        /// A client-side script event that occurs when a pointing device button is released over an element.
        /// </summary>
        public OnMouseUpEventAttribute OnMouseUp { get { return OnMouseUpEvent; } }


        /// <summary>
        /// A client-side script event that occurs when a pointing device is moved onto an element.
        /// </summary>
        public OnMouseOverEventAttribute OnMouseOver { get { return OnMouseOverEvent; } }

        /// <summary>
        /// A client-side script event that occurs when a pointing device is moved within an element.
        /// </summary>
        public OnMouseMoveEventAttribute OnMouseMove { get { return OnMouseMoveEvent; } }


        /// <summary>
        /// A client-side script event that occurs when a pointing device is moved away from an element.
        /// </summary>
        public OnMouseOutEventAttribute OnMouseOut { get { return OnMouseOutEvent; } }

        /// <summary>
        /// A client-side script event that occurs when a key is pressed down over an element then released.
        /// </summary>
        public OnKeyPressEventAttribute OnKeyPress { get { return OnKeyPressEvent; } }

        /// <summary>
        /// A client-side script event that occurs when a key is pressed down over an element.
        /// </summary>
        public OnKeyDownEventAttribute OnKeyDown { get { return OnKeyDownEvent; } }

        /// <summary>
        /// A client-side script event that occurs when a key is released over an element.
        /// </summary>
        public OnKeyUpEventAttribute OnKeyUp { get { return OnKeyUpEvent; } }

        /// <summary>
        /// This attribute specifies the base language of an element's attribute values and text content.
        /// </summary>
        public LanguageAttribute Language
        {
            get { return LanguageAttr; }
        }

        #region Overrides of BaseInlineItem

        /// <summary>
        /// Loads the element from XNode
        /// </summary>
        /// <param name="xNode">node to load element from</param>
        public override void Load(XNode xNode)
        {
            if (xNode.NodeType != XmlNodeType.Element)
            {
                throw new Exception("xNode is not of element type");
            }
            var xElement = (XElement)xNode;
            if (xElement.Name.LocalName != GetElementName())
            {
                throw new Exception(string.Format("xNode is not {0} element", GetElementName()));
            }

            ReadAttributes(xElement);

            LanguageAttr.ReadAttribute(xElement);
            DirectionAttr.ReadAttribute(xElement);

            OnClickEvent.ReadAttribute(xElement);
            OnDblClickEvent.ReadAttribute(xElement);
            OnMouseDownEvent.ReadAttribute(xElement);
            OnMouseUpEvent.ReadAttribute(xElement);
            OnMouseOverEvent.ReadAttribute(xElement);
            OnMouseMoveEvent.ReadAttribute(xElement);
            OnMouseOutEvent.ReadAttribute(xElement);
            OnKeyPressEvent.ReadAttribute(xElement);
            OnKeyDownEvent.ReadAttribute(xElement);
            OnKeyUpEvent.ReadAttribute(xElement);

            Content.Clear();
            IEnumerable<XNode> descendants = xElement.Nodes();
            foreach (var node in descendants)
            {
                IHTML5Item item = ElementFactory.CreateElement(node);
                if ((item != null) && IsValidSubType(item))
                {
                    try
                    {
                        item.Load(node);
                    }
                    catch (Exception)
                    {
                        continue;
                    }
                    Content.Add(item);
                }
            }

        }

        protected virtual bool IsValidSubType(IHTML5Item item)
        {
            if (item is SimpleHTML5Text)
            {
                return item.IsValid();
            }
            if (item is IInlineItem)
            {
                return item.IsValid();
            }
            return false;
        }

        /// <summary>
        /// Generates element to XNode from data
        /// </summary>
        /// <returns>
        /// generated XNode
        /// </returns>
        public override XNode Generate()
        {
            var xElement = new XElement(XhtmlNameSpace + GetElementName());

            AddAttributes(xElement);

            LanguageAttr.AddAttribute(xElement);
            DirectionAttr.AddAttribute(xElement);

            OnClickEvent.AddAttribute(xElement);
            OnDblClickEvent.AddAttribute(xElement);
            OnMouseDownEvent.AddAttribute(xElement);
            OnMouseUpEvent.AddAttribute(xElement);
            OnMouseOverEvent.AddAttribute(xElement);
            OnMouseMoveEvent.AddAttribute(xElement);
            OnMouseOutEvent.AddAttribute(xElement);
            OnKeyPressEvent.AddAttribute(xElement);
            OnKeyDownEvent.AddAttribute(xElement);
            OnKeyUpEvent.AddAttribute(xElement);

            foreach (var item in Content)
            {
                xElement.Add(item.Generate());
            }
            return xElement;
        }

        /// <summary>
        /// Checks it element data is valid
        /// </summary>
        /// <returns>
        /// true if valid
        /// </returns>
        public override bool IsValid()
        {
            return true;
        }

        /// <summary>
        /// Adds sub-item to the item , only if 
        /// allowed by the rules and element can accept content
        /// </summary>
        /// <param name="item">sub-item to add</param>
        public override void Add(IHTML5Item item)
        {
            if ((item != null) && IsValidSubType(item))
            {
                Content.Add(item);
                item.Parent = this;
            }
            else
            {
                throw new HTML5ViolationException(item,"");
            }
        }

        public override void Remove(IHTML5Item item)
        {
            if(Content.Remove(item))
            {
                item.Parent = null;
            }
        }

        public override List<IHTML5Item> SubElements()
        {
            return Content;
        }
        #endregion

    }
}
