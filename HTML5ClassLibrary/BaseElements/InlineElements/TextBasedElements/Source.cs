using System.Xml.Linq;
using HTMLClassLibrary.Attributes;

namespace HTMLClassLibrary.BaseElements.InlineElements.TextBasedElements
{
    /// <summary>
    /// 
    /// </summary>
    [HTMLItemAttribute(ElementName = "source", SupportedStandards = HTMLElementType.HTML5)]
    public class Source : TextBasedElement
    {
        public Source()
        {
            RegisterAttribute(_srcAttrib);
            RegisterAttribute(_mediaAttrib);
            RegisterAttribute(_mimeType);
        }

        private readonly SourceAttribute _srcAttrib = new SourceAttribute();
        private readonly MediaAttribute _mediaAttrib = new MediaAttribute();
        private  readonly MIMETypeAttribute _mimeType = new MIMETypeAttribute();

        public SourceAttribute Src
        {
            get { return _srcAttrib; }
        }

        public MediaAttribute Media
        {
            get { return _mediaAttrib; }
        }

        public MIMETypeAttribute Type
        {
            get { return _mimeType; }
        }
    }
}
