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
        [AttributeTypeAttributeMember(Name = "autoplay",SupportedStandards = HTMLElementType.HTML5 | HTMLElementType.XHTML5)]
        private readonly FlagTypeAttribute _autoplay = new FlagTypeAttribute();

        [AttributeTypeAttributeMember(Name = "controls", SupportedStandards = HTMLElementType.HTML5 | HTMLElementType.XHTML5)]
        private readonly FlagTypeAttribute _controls = new FlagTypeAttribute();

        [AttributeTypeAttributeMember(Name = "height", SupportedStandards = HTMLElementType.HTML5 | HTMLElementType.XHTML5)]
        private readonly LengthTypeAttribute _heightAttribute = new LengthTypeAttribute();

        [AttributeTypeAttributeMember(Name = "loop", SupportedStandards = HTMLElementType.HTML5 | HTMLElementType.XHTML5)]
        private readonly FlagTypeAttribute _loop = new FlagTypeAttribute();

        [AttributeTypeAttributeMember(Name = "muted", SupportedStandards = HTMLElementType.HTML5 | HTMLElementType.XHTML5)]
        private readonly FlagTypeAttribute _muted = new FlagTypeAttribute();

        [AttributeTypeAttributeMember(Name = "poster", SupportedStandards = HTMLElementType.HTML5 | HTMLElementType.XHTML5)]
        private readonly  URITypeAttribute _posterAttribute = new URITypeAttribute();

        [AttributeTypeAttributeMember(Name = "preload", SupportedStandards = HTMLElementType.HTML5 | HTMLElementType.XHTML5)]
        private readonly PreloadTypeAttribute _preload = new PreloadTypeAttribute();

        [AttributeTypeAttributeMember(Name = "src", SupportedStandards = HTMLElementType.HTML5 | HTMLElementType.XHTML5)]
        private readonly URITypeAttribute _src = new URITypeAttribute();

        [AttributeTypeAttributeMember(Name = "width", SupportedStandards = HTMLElementType.HTML5 | HTMLElementType.XHTML5)]
        private readonly LengthTypeAttribute _widthAttribute = new LengthTypeAttribute();



        /// <summary>
        /// Specifies an image to be shown while the video is downloading, or until the user hits the play button
        /// </summary>
        public URITypeAttribute Poster { get { return _posterAttribute; }}

        /// <summary>
        /// Sets the width of the video player
        /// </summary>
        public LengthTypeAttribute  Width   { get { return _widthAttribute; }}

        /// <summary>
        /// Sets the height of the video player
        /// </summary>
        public LengthTypeAttribute LengthAttribute
        {
            get { return _heightAttribute; }
        }

        /// <summary>
        /// Specifies the URL of the video file
        /// </summary>
        public URITypeAttribute Src
        {
            get { return _src; }
        }

        /// <summary>
        /// Specifies that the video will start playing as soon as it is ready
        /// </summary>
        public FlagTypeAttribute AutoPlay
        {
            get { return _autoplay; }
        }

        /// <summary>
        /// Specifies that video controls should be displayed (such as a play/pause button etc).
        /// </summary>
        public FlagTypeAttribute Controls
        {
            get { return _controls; }
        }

        /// <summary>
        /// Specifies that the video will start over again, every time it is finished
        /// </summary>
        public FlagTypeAttribute Loop
        {
            get { return _loop; }
        }

        /// <summary>
        /// Specifies that the audio output of the video should be muted
        /// </summary>
        public FlagTypeAttribute Muted
        {
            get { return _muted; }
        }

        /// <summary>
        /// Specifies if and how the author thinks the video should be loaded when the page loads
        /// </summary>
        public PreloadTypeAttribute Preload
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
