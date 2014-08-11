using HTML5ClassLibrary.Attributes;
using HTML5ClassLibrary.Attributes.AttributeGroups.HTMLGlobal;
using HTML5ClassLibrary.Attributes.AttributeGroups.KeyboardEvents;
using HTML5ClassLibrary.Attributes.AttributeGroups.MouseEvents;
using HTML5ClassLibrary.Attributes.Events;

namespace HTML5ClassLibrary.BaseElements.BlockElements
{
    public interface IBlockElement : IHTML5Item
    {
        /// <summary>
        /// This attribute specifies the base direction of text. 
        /// Possible values:
        /// ltr: Left-to-right 
        /// rtl: Right-to-left
        /// </summary>
        DirectionAttribute Direction { get; }

        /// <summary>
        /// A client-side script event that occurs when a pointing device button is clicked over an element.
        /// </summary>
        OnClickEventAttribute OnClick { get; }


        /// <summary>
        /// A client-side script event that occurs when a pointing device button is double-clicked over an element.
        /// </summary>
        OnDblClickEventAttribute OnDblClick { get; }


        /// <summary>
        /// A client-side script event that occurs when a pointing device button is pressed down over an element.
        /// </summary>
        OnMouseDownEventAttribute OnMouseDown { get; }

        /// <summary>
        /// A client-side script event that occurs when a pointing device button is released over an element.
        /// </summary>
        OnMouseUpEventAttribute OnMouseUp { get; }


        /// <summary>
        /// A client-side script event that occurs when a pointing device is moved onto an element.
        /// </summary>
        OnMouseOverEventAttribute OnMouseOver { get; }

        /// <summary>
        /// A client-side script event that occurs when a pointing device is moved within an element.
        /// </summary>
        OnMouseMoveEventAttribute OnMouseMove { get; }


        /// <summary>
        /// A client-side script event that occurs when a pointing device is moved away from an element.
        /// </summary>
        OnMouseOutEventAttribute OnMouseOut { get; }

        /// <summary>
        /// A client-side script event that occurs when a key is pressed down over an element then released.
        /// </summary>
        OnKeyPressEventAttribute OnKeyPress { get; }

        /// <summary>
        /// A client-side script event that occurs when a key is pressed down over an element.
        /// </summary>
        OnKeyDownEventAttribute OnKeyDown { get; }

        /// <summary>
        /// A client-side script event that occurs when a key is released over an element.
        /// </summary>
        OnKeyUpEventAttribute OnKeyUp { get; }

        /// <summary>
        /// This attribute specifies the base language of an element's attribute values and text content.
        /// </summary>
        LanguageAttribute Language { get; }

        /// <summary>
        /// This attribute assigns a class name or set of class names to an element. 
        /// Any number of elements may be assigned the same class name or set of class names. 
        /// Multiple class names must be separated by white space characters. 
        /// Class names are typically used to apply CSS formatting rules to an element.
        /// </summary>
        ClassAttr Class { get; }


        /// <summary>
        /// This attribute assigns an ID to an element. 
        /// This ID must be unique in a document. 
        /// This ID can be used by client-side scripts (such as JavaScript) to select elements, apply CSS formatting rules, or to build relationships between elements.
        /// </summary>
        IdAttribute ID { get; }

        /// <summary>
        /// This attribute offers advisory information. 
        /// Some Web browsers will display this information as tooltips. 
        /// Assistive technologies may make this information available to users as additional information about the element.
        /// </summary>
        TitleAttribute Title { get; }


        /// <summary>
        /// This attribute specifies formatting style information for the current element. 
        /// The content of this attribute is called inline CSS. The style attribute is deprecated (considered outdated), 
        /// because it fuses together content and formatting.
        /// </summary>
        StyleAttribute Style { get; }

    }
}
