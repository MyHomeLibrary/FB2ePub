using System.Xml.Linq;
using HTML5ClassLibrary.Attributes;


namespace HTML5ClassLibrary.BaseElements.InlineElements
{
    /// <summary>
    /// Hyperlink
    /// </summary>
    public class Anchor : TextBasedElement
    {

        private readonly HrefAttribute _hrefAttrib = new HrefAttribute();


        // advanced attributes
        private readonly HRefLanguageAttribute _hrefLangAttrib = new HRefLanguageAttribute();
        private readonly AreaRelationAttribute _relAttrib = new AreaRelationAttribute();
        private readonly DownloadAttribute _downloadAttrib = new DownloadAttribute();
        private readonly MediaAttribute _mediaAttr = new MediaAttribute();
        private readonly FormTargetAttribute _targetAttr = new FormTargetAttribute();
        private readonly MIMETypeAttribute _typeAttr = new MIMETypeAttribute();


#region public_attributes

        protected override string GetElementName()
        {
            return ElementName;
        }

        /// <summary>
        /// This attribute specifies the location of a Web resource. 
        /// For example: http://xhtml.com/ or mailto:info@xhtml.com.
        /// </summary>
        public HrefAttribute HRef { get { return _hrefAttrib; } }

        /// <summary>
        /// Specifies the primary language of the resource designated by href and may only be used when href is specified.
        /// </summary>
        public HRefLanguageAttribute HrefLanguage { get { return _hrefLangAttrib; } }

        /// <summary>
        /// Describes the relationship from the current document to the resource specified by the href attribute. The value of this attribute is a space-separated list of link types. 
        /// For example: appendix.
        /// </summary>
        public AreaRelationAttribute Rel { get { return _relAttrib; } }

        /// <summary>
        /// Specifies that the target will be downloaded when a user clicks on the hyperlink
        /// </summary>
        public DownloadAttribute Download {get { return _downloadAttrib; }}

        /// <summary>
        /// Specifies what media/device the linked document is optimized for
        /// </summary>
        public MediaAttribute Media { get { return _mediaAttr; }}

        /// <summary>
        /// Specifies where to open the linked document
        /// </summary>
        public FormTargetAttribute Target { get { return _targetAttr; }}

        /// <summary>
        /// Specifies the MIME type of the linked document
        /// </summary>
        public MIMETypeAttribute Type { get { return _typeAttr; }}

#endregion

        public const string ElementName = "a";


        protected override  bool IsValidSubType(IHTML5Item item)
        {

            if (item is IInlineItem)
            {
                if (item is Anchor)
                {
                    return false;
                }
                return item.IsValid();
            }
            if (item is SimpleHTML5Text)
            {
                return true;
            }
            return false;
        }


        public override bool IsValid()
        {
            if (HRef == null)
            {
                return false;
            }
            return true;
        }


        protected override void ReadAttributes(XElement xElement)
        {
            base.ReadAttributes(xElement);

            // Basic attributes
            _hrefAttrib.ReadAttribute(xElement);

            //Advanced attributes
            _hrefLangAttrib.ReadAttribute(xElement);
            _relAttrib.ReadAttribute(xElement);
            _downloadAttrib.ReadAttribute(xElement);
            _mediaAttr.ReadAttribute(xElement);
            _targetAttr.ReadAttribute(xElement);
            _typeAttr.ReadAttribute(xElement);
        }


        protected override void AddAttributes(XElement xElement)
        {
            base.AddAttributes(xElement);

            // basic attributes
            _hrefAttrib.AddAttribute(xElement);

            // advanced attributes
            _hrefLangAttrib.AddAttribute(xElement);
            _relAttrib.AddAttribute(xElement);
            _downloadAttrib.AddAttribute(xElement);
            _mediaAttr.AddAttribute(xElement);
            _targetAttr.AddAttribute(xElement);
            _typeAttr.ReadAttribute(xElement);
        }
    }
}