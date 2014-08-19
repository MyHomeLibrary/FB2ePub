using System.Collections.Generic;
using XHTMLClassLibrary.Attributes;

namespace XHTMLClassLibrary.BaseElements.ObjectParameters
{
    /// <summary>
    /// The param element is used to customize embedded objects that are loaded into a Web browser via the object element. 
    /// The param element is a generic way of passing data to embedded objects in the form of name/value pairs. 
    /// The need for param elements and the number of param elements depends on the embedded object.
    /// </summary>
    [HTMLItemAttribute(ElementName = "param", SupportedStandards = HTMLElementType.HTML5 |  HTMLElementType.XHTML5 | HTMLElementType.Transitional | HTMLElementType.Strict | HTMLElementType.FrameSet | HTMLElementType.XHTML11)]
    public class Param : HTMLItem
    {
        [AttributeTypeAttributeMember(SupportedStandards = HTMLElementType.HTML5 | HTMLElementType.XHTML5 | HTMLElementType.XHTML11 | HTMLElementType.Transitional | HTMLElementType.Strict | HTMLElementType.FrameSet)]
        private readonly NameAttribute _nameAttribute = new NameAttribute();

        [AttributeTypeAttributeMember(SupportedStandards = HTMLElementType.XHTML11 | HTMLElementType.Transitional | HTMLElementType.Strict | HTMLElementType.FrameSet)]
        private readonly MIMETypeAttribute _typeAttribute = new MIMETypeAttribute();

        [AttributeTypeAttributeMember(SupportedStandards = HTMLElementType.HTML5 | HTMLElementType.XHTML5 | HTMLElementType.XHTML11 | HTMLElementType.Transitional | HTMLElementType.Strict | HTMLElementType.FrameSet)]
        private readonly ValueAttribute _valueAttribute = new ValueAttribute();

        [AttributeTypeAttributeMember(SupportedStandards = HTMLElementType.XHTML11 | HTMLElementType.Transitional | HTMLElementType.Strict | HTMLElementType.FrameSet)]
        private readonly ValueTypeAttribute _valueTypeAttribute = new ValueTypeAttribute();


        
        #region public_properties


        /// <summary>
        /// his attribute defines the name of a run-time parameter, assumed to be known by the inserted object. 
        /// Whether the property name is case-sensitive or not depends on the specific object implementation. 
        /// This attribute is required.
        /// </summary>
        public NameAttribute Name { get { return _nameAttribute; } }


        /// <summary>
        /// Specifies the media type of the parameter
        /// Not supported in HTML5.
        /// </summary>
        public MIMETypeAttribute Type { get { return _typeAttribute; }}

        /// <summary>
        /// This attribute specifies the value of a run-time parameter specified by the name attribute. 
        /// Property values have no meaning in XHTML; their meaning is determined by the object in question.
        /// </summary>
        public ValueAttribute Value { get { return _valueAttribute; } }


        /// <summary>
        /// Specifies the type of the value
        /// Not supported in HTML5.
        /// </summary>
        public ValueTypeAttribute ValueType { get { return _valueTypeAttribute; }}


        #endregion

 
        /// <summary>
        /// Checks it element data is valid
        /// </summary>
        /// <returns>true if valid</returns>
        public override bool IsValid()
        {
            return (_nameAttribute.HasValue());
        }

        public override List<IHTMLItem> SubElements()
        {
            return null;
        }
    }
}