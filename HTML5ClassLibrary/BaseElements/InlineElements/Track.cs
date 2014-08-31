using System.Collections.Generic;
using System.ComponentModel;
using XHTMLClassLibrary.AttributeDataTypes;
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
        #region Attribute_Values_Enums

        /// <summary>
        /// "kind" attribute possible values
        /// </summary>
        public enum KindAttributeOptions
        {
            [Description("captions")]
            Captions,

            [Description("chapters")]
            Chapters,

            [Description("descriptions")]
            Descriptions,

            [Description("metadata")]
            Metadata,

            [Description("subtitles")]
            Subtitles,
        }

        #endregion

        [AttributeTypeAttributeMember(Name = "default", SupportedStandards = HTMLElementType.HTML5 | HTMLElementType.XHTML5)]
        private readonly FlagTypeAttribute _defaultAttribute = new FlagTypeAttribute();

        [AttributeTypeAttributeMember(Name = "label", SupportedStandards = HTMLElementType.HTML5 | HTMLElementType.XHTML5)]
        private readonly SimpleSingleTypeAttribute<Text> _labelAttribute = new SimpleSingleTypeAttribute<Text>();

        [AttributeTypeAttributeMember(Name = "src", SupportedStandards = HTMLElementType.HTML5 | HTMLElementType.XHTML5)]
        private readonly SimpleSingleTypeAttribute<URI> _sourceAttribute = new SimpleSingleTypeAttribute<URI>();

        [AttributeTypeAttributeMember(Name = "kind", SupportedStandards = HTMLElementType.HTML5 | HTMLElementType.XHTML5)]
        private readonly ValuesSelectionTypeAttribute<Text> _trackKindAttribute = new ValuesSelectionTypeAttribute<Text>(typeof(KindAttributeOptions));

        [AttributeTypeAttributeMember(Name = "srclang", SupportedStandards = HTMLElementType.HTML5 | HTMLElementType.XHTML5)]
        private readonly SimpleSingleTypeAttribute<LanguageCode> _sourceLanguageAttribute = new SimpleSingleTypeAttribute<LanguageCode>();


        /// <summary>
        /// Specifies the language of the track text data (required if kind="subtitles")
        /// </summary>
        public IAttributeDataAccess SourceLanguage { get { return _sourceLanguageAttribute; }}

        /// <summary>
        /// Specifies the kind of text track
        /// </summary>
        public IAttributeDataAccess Kind { get { return _trackKindAttribute; } }

        /// <summary>
        /// Required. Specifies the URL of the track file
        /// </summary>
        public IAttributeDataAccess Source { get { return _sourceAttribute; } }

        /// <summary>
        /// Specifies the title of the text track
        /// </summary>
        public IAttributeDataAccess Label { get { return _labelAttribute; } }

        /// <summary>
        /// Specifies that the track is to be enabled if the user's preferences do not indicate that another track would be more appropriate
        /// </summary>
        public IAttributeDataAccess Default { get { return _defaultAttribute; } }

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
