using XHTMLClassLibrary.AttributeDataTypes;
using XHTMLClassLibrary.Attributes;

namespace XHTMLClassLibrary.BaseElements.Structure_Header
{
    /// <summary>
    /// The "html" tag tells the browser that this is an HTML document.
    ///The "html" tag represents the root of an HTML document.
    ///The "html" tag is the container for all other HTML elements (except for the "!DOCTYPE" tag).
    /// </summary>
    [HTMLItemAttribute(ElementName = "html", SupportedStandards = HTMLElementType.HTML5 |  HTMLElementType.XHTML5 | HTMLElementType.XHTML11 | HTMLElementType.Transitional | HTMLElementType.Strict | HTMLElementType.FrameSet)]
    public class HTML : HTMLItem
    {
        [AttributeTypeAttributeMember(SupportedStandards = HTMLElementType.XHTML5 | HTMLElementType.XHTML11)]
        private readonly SimpleSingleTypeAttribute<Text> _namespaceAttribute = new SimpleSingleTypeAttribute<Text>("xmlns");
        
            
        [AttributeTypeAttributeMember(SupportedStandards = HTMLElementType.Transitional | HTMLElementType.FrameSet | HTMLElementType.XHTML11)]
        private readonly SimpleSingleTypeAttribute<URI> _manifestAttribute = new SimpleSingleTypeAttribute<URI>("manifest");


        public HTML(HTMLElementType htmlStandard) : base(htmlStandard)
        {
            //_namespaceAttribute.Value = @"http://www.w3.org/1999/xhtml";
        }

        /// <summary>
        /// This attribute has been deprecated (made outdated). 
        /// It is redundant, because version information is now provided by the DOCTYPE.
        /// </summary>
        public IAttributeDataAccess Manifest { get {return _manifestAttribute;}}


        protected override bool IsValidSubType(IHTMLItem item)
        {
            if (Subitems.Count >= 2) // no more than two sub elements
            {
                return false;
            }
            if (Subitems.Count == 0) // head have to be first
            {
                if (!(item is Head)  )
                {
                    return false;
                }
            }
            if (Subitems.Count == 1) // body have to be second
            {
                if (!(item is Body)  )
                {
                    return false;
                }
            }

            return item.IsValid();
        }


        public override bool IsValid()
        {
            if (Subitems.Count != 2)
            {
                return false;
            }
            if (!(Subitems[0] is Head))
            {
                return false;
            }
            if (!(Subitems[1] is Body))
            {
                return false;
            }
            return (Subitems[0].IsValid() && Subitems[1].IsValid());
        }
    }
}
