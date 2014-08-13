using System;
using System.Collections.Generic;
using System.Xml;
using System.Xml.Linq;
using HTML5ClassLibrary.Attributes;
using HTML5ClassLibrary.Attributes.FlaggedAttributes;

namespace HTML5ClassLibrary.BaseElements.InlineElements
{
    /// <summary>
    /// The "track" tag specifies text tracks for media elements ("audio" and "video").
    /// This element is used to specify subtitles, caption files or other files containing text, that should be visible when the media is playing.
    /// </summary>
    public class Track : BaseInlineItem
    {
        public const string ElementName = "track";

        public Track()
        {
            RegisterAttribute(_defaultAttribute);
            RegisterAttribute(_labelAttribute);
            RegisterAttribute(_sourceAttribute);
            RegisterAttribute(_trackKindAttribute);
            RegisterAttribute(_sourceLanguageAttribute);
        }

        private readonly DefaultAttribute _defaultAttribute = new DefaultAttribute();
        private readonly LabelAttribute _labelAttribute = new LabelAttribute();
        private readonly SourceAttribute _sourceAttribute = new SourceAttribute();
        private readonly TrackKindAttribute _trackKindAttribute = new TrackKindAttribute();
        private readonly SourceLanguageAttribute _sourceLanguageAttribute = new SourceLanguageAttribute();


        /// <summary>
        /// Specifies the language of the track text data (required if kind="subtitles")
        /// </summary>
        public SourceLanguageAttribute SourceLanguage { get { return _sourceLanguageAttribute; }}

        /// <summary>
        /// Specifies the kind of text track
        /// </summary>
        public TrackKindAttribute Kind { get { return _trackKindAttribute; }}

        /// <summary>
        /// Required. Specifies the URL of the track file
        /// </summary>
        public SourceAttribute Source { get { return _sourceAttribute; }}

        /// <summary>
        /// Specifies the title of the text track
        /// </summary>
        public LabelAttribute Label { get { return _labelAttribute; }}

        /// <summary>
        /// Specifies that the track is to be enabled if the user's preferences do not indicate that another track would be more appropriate
        /// </summary>
        public DefaultAttribute Default { get { return _defaultAttribute; }}

        public override void Load(XNode xNode)
        {
            if (xNode.NodeType != XmlNodeType.Element)
            {
                throw new Exception("xNode is not of element type");
            }
            var xElement = (XElement)xNode;
            if (xElement.Name.LocalName != ElementName)
            {
                throw new Exception("xNode is not empty line element");
            }
            ReadAttributes(xElement);
        }

        public override XNode Generate()
        {
            var xElement = new XElement(XhtmlNameSpace + ElementName);
            AddAttributes(xElement);
            return xElement;
        }

        public override bool IsValid()
        {
            return _sourceAttribute.HasValue();
        }

        /// <summary>
        /// Adds sub-item to the item , only if 
        /// allowed by the rules and element can accept content
        /// </summary>
        /// <param name="item">sub-item to add</param>
        public override void Add(IHTML5Item item)
        {
            throw new Exception("This element does not contain sub-items");
        }

        public override void Remove(IHTML5Item item)
        {
            throw new Exception("This element does not contain sub-items");
        }

        public override List<IHTML5Item> SubElements()
        {
            return null;
        }
    }
}
