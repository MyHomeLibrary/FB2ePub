using System.Collections.Generic;
using XHTMLClassLibrary.Attributes;

namespace XHTMLClassLibrary.BaseElements.InlineElements
{
    /// <summary>
    /// The "track" tag specifies text tracks for media elements ("audio" and "video").
    /// This element is used to specify subtitles, caption files or other files containing text, that should be visible when the media is playing.
    /// </summary>
    [HTMLItemAttribute(ElementName = "track", SupportedStandards = HTMLElementType.HTML5)]
    public class Track : HTMLItem, IInlineItem
    {
        [AttributeTypeAttributeMember(Name = "default", SupportedStandards = HTMLElementType.HTML5 | HTMLElementType.XHTML5)]
        private readonly FlagTypeAttribute _defaultAttribute = new FlagTypeAttribute();

        [AttributeTypeAttributeMember(Name = "label", SupportedStandards = HTMLElementType.HTML5 | HTMLElementType.XHTML5)]
        private readonly TextValueTypeAttribute _labelAttribute = new TextValueTypeAttribute();

        [AttributeTypeAttributeMember(Name = "src", SupportedStandards = HTMLElementType.HTML5 | HTMLElementType.XHTML5)]
        private readonly URITypeAttribute _sourceAttribute = new URITypeAttribute();

        [AttributeTypeAttributeMember(Name = "kind", SupportedStandards = HTMLElementType.HTML5 | HTMLElementType.XHTML5)]
        private readonly TrackKindTypeAttribute _trackKindAttribute = new TrackKindTypeAttribute();

        [AttributeTypeAttributeMember(Name = "srclang", SupportedStandards = HTMLElementType.HTML5 | HTMLElementType.XHTML5)]
        private readonly LanguageTypeAttribute _sourceLanguageAttribute = new LanguageTypeAttribute();


        /// <summary>
        /// Specifies the language of the track text data (required if kind="subtitles")
        /// </summary>
        public LanguageTypeAttribute SourceLanguage { get { return _sourceLanguageAttribute; }}

        /// <summary>
        /// Specifies the kind of text track
        /// </summary>
        public TrackKindTypeAttribute Kind { get { return _trackKindAttribute; }}

        /// <summary>
        /// Required. Specifies the URL of the track file
        /// </summary>
        public URITypeAttribute Source { get { return _sourceAttribute; }}

        /// <summary>
        /// Specifies the title of the text track
        /// </summary>
        public TextValueTypeAttribute Label { get { return _labelAttribute; }}

        /// <summary>
        /// Specifies that the track is to be enabled if the user's preferences do not indicate that another track would be more appropriate
        /// </summary>
        public FlagTypeAttribute Default { get { return _defaultAttribute; }}

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
