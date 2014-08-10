using System.Xml.Linq;
using HTML5ClassLibrary.Attributes;

namespace HTML5ClassLibrary.BaseElements.InlineElements
{
    public class Source : TextBasedElement
    {
        public const string ElementName = "source";

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

        protected override void AddAttributes(XElement xElement)
        {
            base.AddAttributes(xElement);
            _srcAttrib.AddAttribute(xElement);
            _mediaAttrib.AddAttribute(xElement);
            _mimeType.AddAttribute(xElement);
        }

        protected override void ReadAttributes(XElement xElement)
        {
            base.ReadAttributes(xElement);
            _srcAttrib.ReadAttribute(xElement);
            _mediaAttrib.ReadAttribute(xElement);
            _mimeType.ReadAttribute(xElement);
        }
    }
}
