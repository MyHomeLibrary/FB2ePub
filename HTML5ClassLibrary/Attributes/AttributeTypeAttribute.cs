using System;
using XHTMLClassLibrary.BaseElements;

namespace XHTMLClassLibrary.Attributes
{
    /// <summary>
    /// This is a class marking any member as HTML attribute and providing some properties like name etc
    /// </summary>
    [AttributeUsage(AttributeTargets.Field, Inherited = false, AllowMultiple = false)]
    public class AttributeTypeAttributeMember : Attribute
    {
        private HTMLElementType _standard = HTMLElementType.UnknownType;

        /// <summary>
        /// Mask containing set of standards element supports
        /// </summary>
        public HTMLElementType SupportedStandards
        {
            get { return _standard; }
            set { _standard = value; }
        }
    }

    /// <summary>
    /// marks a field as containing members of attribute type
    /// </summary>
    public sealed class AttributeBlockAttribute : Attribute
    {
        
    }
}
