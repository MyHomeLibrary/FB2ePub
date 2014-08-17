using System;
using System.Collections.Generic;
using System.Xml;
using System.Xml.Linq;
using XHTMLClassLibrary.Attributes;
using XHTMLClassLibrary.Attributes.FlaggedAttributes;

namespace XHTMLClassLibrary.BaseElements.InlineElements
{
    /// <summary>
    /// The "track" tag specifies text tracks for media elements ("audio" and "video").
    /// This element is used to specify subtitles, caption files or other files containing text, that should be visible when the media is playing.
    /// </summary>
    [HTMLItemAttribute(ElementName = "track", SupportedStandards = HTMLElementType.HTML5)]
    public class Track : HTMLItem, IInlineItem
    {
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

        public override bool IsValid()
        {
            return _sourceAttribute.HasValue();
        }

        public override List<IHTMLItem> SubElements()
        {
            return null;
        }
    }
}
