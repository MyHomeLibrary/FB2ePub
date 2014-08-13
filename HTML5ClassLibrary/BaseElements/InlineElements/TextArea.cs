using System;
using System.Collections.Generic;
using System.Xml;
using System.Xml.Linq;
using HTML5ClassLibrary.Attributes;
using HTML5ClassLibrary.Attributes.FlaggedAttributes;

namespace HTML5ClassLibrary.BaseElements.InlineElements
{
    /// <summary>
    /// The textarea element is used to create a multi-line text input form control.
    /// </summary>
    public class TextArea : BaseInlineItem
    {
        internal const string ElementName = "textarea";

        private readonly SimpleHTML5Text _scriptText = new SimpleHTML5Text();

        public TextArea()
        {
            Attributes.Add(_colsAttribute);
            Attributes.Add(_nameAttribute);
            Attributes.Add(_rowsAttribute);
            Attributes.Add(_autoFocusAttribute);
            Attributes.Add(_disabledAttribute);
            Attributes.Add(_formIdAttribute);
            Attributes.Add(_maxLengthAttribute);
            Attributes.Add(_placeHolderAttribute);
            Attributes.Add(_readOnlyAttribute);
            Attributes.Add(_requiredAttribute);
            Attributes.Add(_wrapAttribute);
           
        }

        private readonly ColsAttribute _colsAttribute = new ColsAttribute();
        private readonly NameAttribute _nameAttribute = new NameAttribute();
        private readonly RowsAttribute _rowsAttribute = new RowsAttribute();
        private readonly AutoFocusAttribute _autoFocusAttribute = new AutoFocusAttribute();
        private readonly DisabledAttribute _disabledAttribute = new DisabledAttribute();
        private readonly FormIdAttribute _formIdAttribute = new FormIdAttribute();
        private readonly MaxLengthAttribute _maxLengthAttribute = new MaxLengthAttribute();
        private readonly PlaceHolderAttribute _placeHolderAttribute = new PlaceHolderAttribute();
        private readonly ReadOnlyAttribute _readOnlyAttribute = new ReadOnlyAttribute();
        private readonly RequiredAttribute _requiredAttribute = new RequiredAttribute();
        private readonly WrapAttribute _wrapAttribute = new WrapAttribute();




        /// <summary>
        /// Specifies that a text area should automatically get focus when the page loads
        /// </summary>
        public AutoFocusAttribute AutoFocus { get { return _autoFocusAttribute; }}

        /// <summary>
        /// Specifies that a text area should be disabled
        /// </summary>
        public DisabledAttribute Disabled { get { return _disabledAttribute; }}

        /// <summary>
        /// Specifies one or more forms the text area belongs to
        /// </summary>
        public FormIdAttribute Form { get { return _formIdAttribute; }}

        /// <summary>
        /// Specifies the maximum number of characters allowed in the text area
        /// </summary>
        public MaxLengthAttribute MaxLength { get { return _maxLengthAttribute; }}

        /// <summary>
        /// Specifies a short hint that describes the expected value of a text area
        /// </summary>
        public PlaceHolderAttribute PlaceHolder { get { return _placeHolderAttribute; }}

        /// <summary>
        /// Specifies that a text area should be read-only
        /// </summary>
        public ReadOnlyAttribute ReadOnly { get { return _readOnlyAttribute; }}

        /// <summary>
        /// Specifies that a text area is required/must be filled out
        /// </summary>
        public RequiredAttribute Required { get { return _requiredAttribute; }}

        /// <summary>
        /// Specifies how the text in a text area is to be wrapped when submitted in a form
        /// </summary>
        public WrapAttribute Wrap { get { return _wrapAttribute; }}

        /// <summary>
        /// The script text itself
        /// </summary>
        public SimpleHTML5Text ScriptText { get { return _scriptText; } }

        /// <summary>
        /// This attribute specifies the visible width in average character widths. 
        /// This attribute is required.
        /// </summary>
        public ColsAttribute Cols { get { return _colsAttribute; } }

        /// <summary>
        /// Form control name.
        /// </summary>
        public NameAttribute Name { get { return _nameAttribute; } }

        /// <summary>
        /// This attribute specifies the number of visible text lines. 
        /// This attribute is required.
        /// </summary>
        public RowsAttribute Rows { get { return _rowsAttribute; } }



        public override void Load(XNode xNode)
        {
            if (xNode.NodeType != XmlNodeType.Element)
            {
                throw new Exception("xNode is not of element type");
            }
            var xElement = (XElement)xNode;
            if (xElement.Name.LocalName != ElementName)
            {
                throw new Exception(string.Format("xNode is not {0} element", ElementName));
            }

            ReadAttributes(xElement);

            _scriptText.Load(xNode);
        }

        public override XNode Generate()
        {
            var xElement = new XElement(XhtmlNameSpace + ElementName);

            AddAttributes(xElement);

            xElement.Add(_scriptText.Generate());

            return xElement;

        }

    
        public override bool IsValid()
        {
            return (_colsAttribute.HasValue() && _rowsAttribute.HasValue());
        }

        /// <summary>
        /// Adds sub-item to the item , only if 
        /// allowed by the rules and element can accept content
        /// </summary>
        /// <param name="item">sub-item to add</param>
        public override void Add(IHTML5Item item)
        {
            throw new Exception("This element does not contain sub-items");
        }

        public override void Remove(IHTML5Item item)
        {
            throw new Exception("This element does not contain sub-items");
        }

        public override List<IHTML5Item> SubElements()
        {
            return null;
        }
    }
}
