using System;
using System.Collections.Generic;
using System.Xml;
using System.Xml.Linq;
using XHTMLClassLibrary.Attributes;
using XHTMLClassLibrary.Attributes.FlaggedAttributes;
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
        public Video()
        {
            RegisterAttribute(_autoplay);
            RegisterAttribute(_controls);
            RegisterAttribute(_heightAttribute);
            RegisterAttribute(_loop);
            RegisterAttribute(_muted);
            RegisterAttribute(_posterAttribute);
            RegisterAttribute(_preload);
            RegisterAttribute(_src);
            RegisterAttribute(_widthAttribute);
        }

        private readonly AutoPlayAttribute _autoplay = new AutoPlayAttribute();
        private readonly ControlsAttribute _controls = new ControlsAttribute();
        private readonly HeightAttribute _heightAttribute = new HeightAttribute();
        private readonly LoopAttribute _loop = new LoopAttribute();
        private readonly MutedAttribute _muted = new MutedAttribute();
        private readonly  PosterAttribute _posterAttribute = new PosterAttribute();
        private readonly PreloadAttribute _preload = new PreloadAttribute();
        private readonly SourceAttribute _src = new SourceAttribute();
        private readonly WidthAttribute _widthAttribute = new WidthAttribute();



        /// <summary>
        /// Specifies an image to be shown while the video is downloading, or until the user hits the play button
        /// </summary>
        public PosterAttribute Poster { get { return _posterAttribute; }}

        /// <summary>
        /// Sets the width of the video player
        /// </summary>
        public WidthAttribute  Width   { get { return _widthAttribute; }}

        /// <summary>
        /// Sets the height of the video player
        /// </summary>
        public HeightAttribute HeightAttribute
        {
            get { return _heightAttribute; }
        }

        /// <summary>
        /// Specifies the URL of the video file
        /// </summary>
        public SourceAttribute Src
        {
            get { return _src; }
        }

        /// <summary>
        /// Specifies that the video will start playing as soon as it is ready
        /// </summary>
        public AutoPlayAttribute AutoPlay
        {
            get { return _autoplay; }
        }

        /// <summary>
        /// Specifies that video controls should be displayed (such as a play/pause button etc).
        /// </summary>
        public ControlsAttribute Controls
        {
            get { return _controls; }
        }

        /// <summary>
        /// Specifies that the video will start over again, every time it is finished
        /// </summary>
        public LoopAttribute Loop
        {
            get { return _loop; }
        }

        /// <summary>
        /// Specifies that the audio output of the video should be muted
        /// </summary>
        public MutedAttribute Muted
        {
            get { return _muted; }
        }

        /// <summary>
        /// Specifies if and how the author thinks the video should be loaded when the page loads
        /// </summary>
        public PreloadAttribute Preload
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
