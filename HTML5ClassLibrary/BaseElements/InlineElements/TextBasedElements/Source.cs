using System.Xml.Linq;
using HTML5ClassLibrary.Attributes;

namespace HTML5ClassLibrary.BaseElements.InlineElements.TextBasedElements
{
    public class Source : TextBasedElement
    {
        public const string ElementName = "source";

        public Source()
        {
            Attributes.Add(_srcAttrib);
            Attributes.Add(_mediaAttrib);
            Attributes.Add(_mimeType);
        }

        private readonly SourceAttribute _srcAttrib = new SourceAttribute();
        private readonly MediaAttribute _mediaAttrib = new MediaAttribute();
        private  readonly MIMETypeAttribute _mimeType = new MIMETypeAttribute();

        protected override string GetElementName()
        {
            return ElementName;
        }


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
