using System.ComponentModel;
using XHTMLClassLibrary.AttributeDataTypes;
using XHTMLClassLibrary.Attributes;
using XHTMLClassLibrary.BaseElements.InlineElements;

namespace XHTMLClassLibrary.BaseElements.BlockElements
{
    /// <summary>
    /// The "video" tag specifies video, such as a movie clip or other video streams.
    /// Currently, there are 3 supported video formats for the "video" element: MP4, WebM, and Ogg
    /// </summary>
    [HTMLItemAttribute(ElementName = "video", SupportedStandards = HTMLElementType.HTML5)]
    public class Video : HTMLItem, IBlockElement
    {

        #region Attribute_Values_Enums

        /// <summary>
        /// "preload" attribute possible values
        /// </summary>
        public enum PreloadAttributeOptions
        {
            [Description("auto")]
            Auto,

            [Description("metadata")]
            Metadata,

            [Description("none")]
            None,
        }

        #endregion


        [AttributeTypeAttributeMember(SupportedStandards = HTMLElementType.HTML5 | HTMLElementType.XHTML5)]
        private readonly FlagTypeAttribute _autoplay = new FlagTypeAttribute("autoplay");

        [AttributeTypeAttributeMember(SupportedStandards = HTMLElementType.HTML5 | HTMLElementType.XHTML5)]
        private readonly FlagTypeAttribute _controls = new FlagTypeAttribute("controls");

        [AttributeTypeAttributeMember(SupportedStandards = HTMLElementType.HTML5 | HTMLElementType.XHTML5)]
        private readonly SimpleSingleTypeAttribute<Length> _heightAttribute = new SimpleSingleTypeAttribute<Length>("height");

        [AttributeTypeAttributeMember(SupportedStandards = HTMLElementType.HTML5 | HTMLElementType.XHTML5)]
        private readonly FlagTypeAttribute _loop = new FlagTypeAttribute("loop");

        [AttributeTypeAttributeMember(SupportedStandards = HTMLElementType.HTML5 | HTMLElementType.XHTML5)]
        private readonly FlagTypeAttribute _muted = new FlagTypeAttribute("muted");

        [AttributeTypeAttributeMember(SupportedStandards = HTMLElementType.HTML5 | HTMLElementType.XHTML5)]
        private readonly SimpleSingleTypeAttribute<URI> _posterAttribute = new SimpleSingleTypeAttribute<URI>("poster");

        [AttributeTypeAttributeMember(SupportedStandards = HTMLElementType.HTML5 | HTMLElementType.XHTML5)]
        private readonly ValuesSelectionTypeAttribute<Text> _preload = new ValuesSelectionTypeAttribute<Text>("preload",typeof(PreloadAttributeOptions));

        [AttributeTypeAttributeMember(SupportedStandards = HTMLElementType.HTML5 | HTMLElementType.XHTML5)]
        private readonly SimpleSingleTypeAttribute<URI> _src = new SimpleSingleTypeAttribute<URI>("src");

        [AttributeTypeAttributeMember(SupportedStandards = HTMLElementType.HTML5 | HTMLElementType.XHTML5)]
        private readonly SimpleSingleTypeAttribute<Length> _widthAttribute = new SimpleSingleTypeAttribute<Length>("width");



        /// <summary>
        /// Specifies an image to be shown while the video is downloading, or until the user hits the play button
        /// </summary>
        public IAttributeDataAccess Poster { get { return _posterAttribute; }}

        /// <summary>
        /// Sets the width of the video player
        /// </summary>
        public IAttributeDataAccess Width { get { return _widthAttribute; } }

        /// <summary>
        /// Sets the height of the video player
        /// </summary>
        public IAttributeDataAccess LengthAttribute
        {
            get { return _heightAttribute; }
        }

        /// <summary>
        /// Specifies the URL of the video file
        /// </summary>
        public IAttributeDataAccess Src
        {
            get { return _src; }
        }

        /// <summary>
        /// Specifies that the video will start playing as soon as it is ready
        /// </summary>
        public IAttributeDataAccess AutoPlay
        {
            get { return _autoplay; }
        }

        /// <summary>
        /// Specifies that video controls should be displayed (such as a play/pause button etc).
        /// </summary>
        public IAttributeDataAccess Controls
        {
            get { return _controls; }
        }

        /// <summary>
        /// Specifies that the video will start over again, every time it is finished
        /// </summary>
        public IAttributeDataAccess Loop
        {
            get { return _loop; }
        }

        /// <summary>
        /// Specifies that the audio output of the video should be muted
        /// </summary>
        public IAttributeDataAccess Muted
        {
            get { return _muted; }
        }

        /// <summary>
        /// Specifies if and how the author thinks the video should be loaded when the page loads
        /// </summary>
        public IAttributeDataAccess Preload
        {
            get { return _preload; }
        }

        public override bool IsValid()
        {
            return true;
        }

        protected override bool IsValidSubType(IHTMLItem item)
        {
            if (item is IInlineItem ||
                item is IBlockElement ||
                item is SimpleHTML5Text)
            {
                return item.IsValid();
            }
            return false;
        }
    }
}
