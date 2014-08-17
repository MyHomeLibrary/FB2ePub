using System;

namespace XHTMLClassLibrary.BaseElements
{
    [Flags]
    public enum  HTMLElementType 
    {
        UnknownType = 0,
        HTML5 = 1,
        XHTML11 =   2,
        Transitional = 4 ,
        Strict = 8,
        FrameSet = 16,
        XHTML5 = 32,
    }

    /// <summary>
    /// This is a class marking any HTML item as such and providing some properties like name etc
    /// </summary>
    [AttributeUsage(AttributeTargets.Class, Inherited = false)]
    public sealed class HTMLItemAttribute : Attribute
    {
        private HTMLElementType _standard = HTMLElementType.UnknownType;

        /// <summary>
        /// Name of the element in XML/HTML file
        /// </summary>
        public string ElementName { get; set; }

        /// <summary>
        /// Mask containing set of standards element supports
        /// </summary>
        public HTMLElementType SupportedStandards
        {
            get { return _standard; }
            set { _standard = value; }
        }
    }
}
