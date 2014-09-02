using System.Collections.Generic;
using System.ComponentModel;
using XHTMLClassLibrary.AttributeDataTypes;
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
        
        #region Attribute_Values_Enums

        /// <summary>
        /// "valuetype" attribute possible values
        /// </summary>
        public enum TypeAttributeOptions
        {
            [Description("data")]
            Data,

            [Description("object")]
            Object,

            [Description("ref")]
            Ref,
        }
        #endregion


        [AttributeTypeAttributeMember(SupportedStandards = HTMLElementType.HTML5 | HTMLElementType.XHTML5 | HTMLElementType.XHTML11 | HTMLElementType.Transitional | HTMLElementType.Strict | HTMLElementType.FrameSet)]
        private readonly SimpleSingleTypeAttribute<Text> _nameAttribute = new SimpleSingleTypeAttribute<Text>("name");

        [AttributeTypeAttributeMember(SupportedStandards = HTMLElementType.XHTML11 | HTMLElementType.Transitional | HTMLElementType.Strict | HTMLElementType.FrameSet)]
        private readonly SimpleSingleTypeAttribute<MIME_Type> _typeAttribute = new SimpleSingleTypeAttribute<MIME_Type>("type");

        [AttributeTypeAttributeMember(SupportedStandards = HTMLElementType.HTML5 | HTMLElementType.XHTML5 | HTMLElementType.XHTML11 | HTMLElementType.Transitional | HTMLElementType.Strict | HTMLElementType.FrameSet)]
        private readonly SimpleSingleTypeAttribute<Text> _valueAttribute = new SimpleSingleTypeAttribute<Text>("value");

        [AttributeTypeAttributeMember(SupportedStandards = HTMLElementType.XHTML11 | HTMLElementType.Transitional | HTMLElementType.Strict | HTMLElementType.FrameSet)]
        private readonly ValuesSelectionTypeAttribute<Text> _valueTypeAttribute = new ValuesSelectionTypeAttribute<Text>("valuetype",typeof(TypeAttributeOptions));


        
        #region public_properties

        public Param(HTMLElementType htmlStandard) : base(htmlStandard)
        {
        }

        /// <summary>
        /// his attribute defines the name of a run-time parameter, assumed to be known by the inserted object. 
        /// Whether the property name is case-sensitive or not depends on the specific object implementation. 
        /// This attribute is required.
        /// </summary>
        public IAttributeDataAccess Name { get { return _nameAttribute; } }


        /// <summary>
        /// Specifies the media type of the parameter
        /// Not supported in HTML5.
        /// </summary>
        public IAttributeDataAccess Type { get { return _typeAttribute; } }

        /// <summary>
        /// This attribute specifies the value of a run-time parameter specified by the name attribute. 
        /// Property values have no meaning in XHTML; their meaning is determined by the object in question.
        /// </summary>
        public IAttributeDataAccess Value { get { return _valueAttribute; } }


        /// <summary>
        /// Specifies the type of the value
        /// Not supported in HTML5.
        /// </summary>
        public IAttributeDataAccess ValueType { get { return _valueTypeAttribute; } }


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