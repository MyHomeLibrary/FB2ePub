using System;
using System.Collections.Generic;
using System.Xml;
using System.Xml.Linq;
using HTMLClassLibrary.Attributes;
using HTMLClassLibrary.Attributes.AttributeGroups.FormEvents;
using HTMLClassLibrary.Attributes.AttributeGroups.HTMLGlobal;
using HTMLClassLibrary.Attributes.AttributeGroups.KeyboardEvents;
using HTMLClassLibrary.Attributes.AttributeGroups.MediaEvents;
using HTMLClassLibrary.Attributes.AttributeGroups.MouseEvents;
using HTMLClassLibrary.Attributes.AttributeGroups.WindowEventAttributes;

namespace HTMLClassLibrary.BaseElements.ObjectParameters
{
    /// <summary>
    /// The param element is used to customize embedded objects that are loaded into a Web browser via the object element. 
    /// The param element is a generic way of passing data to embedded objects in the form of name/value pairs. 
    /// The need for param elements and the number of param elements depends on the embedded object.
    /// </summary>
    [HTMLItemAttribute(ElementName = "param", SupportedStandards = HTMLElementType.HTML5 | HTMLElementType.Transitional | HTMLElementType.Strict | HTMLElementType.FrameSet | HTMLElementType.XHTML11)]
    public class Param : HTMLItem
    {
        public Param()
        {
            RegisterAttribute(_nameAttribute);
            RegisterAttribute(_valueAttribute);
        }

        private readonly NameAttribute _nameAttribute = new NameAttribute();
        private readonly ValueAttribute _valueAttribute = new ValueAttribute();


        
        #region public_properties


        /// <summary>
        /// his attribute defines the name of a run-time parameter, assumed to be known by the inserted object. 
        /// Whether the property name is case-sensitive or not depends on the specific object implementation. 
        /// This attribute is required.
        /// </summary>
        public NameAttribute Name { get { return _nameAttribute; } }

        /// <summary>
        /// This attribute specifies the value of a run-time parameter specified by the name attribute. 
        /// Property values have no meaning in XHTML; their meaning is determined by the object in question.
        /// </summary>
        public ValueAttribute Value { get { return _valueAttribute; } }


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