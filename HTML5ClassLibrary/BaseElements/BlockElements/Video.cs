using System;
using System.Collections.Generic;
using System.Xml;
using System.Xml.Linq;
using HTML5ClassLibrary.Attributes;
using HTML5ClassLibrary.Attributes.FlaggedAttributes;
using HTML5ClassLibrary.BaseElements.InlineElements;

namespace HTML5ClassLibrary.BaseElements.BlockElements
{
    /// <summary>
    /// The "video" tag specifies video, such as a movie clip or other video streams.
    /// Currently, there are 3 supported video formats for the "video" element: MP4, WebM, and Ogg
    /// </summary>
    public class Video : BaseBlockElement
    {
        public const string ElementName = "video";

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

        public override void Load(XNode xNode)
        {
            if (xNode.NodeType != XmlNodeType.Element)
            {
                throw new Exception("xNode is not of element type");
            }
            var xElement = (XElement)xNode;
            if (xElement.Name.LocalName != ElementName)
            {
                throw new Exception(string.Format("xNode is not {0} element", ElementName));
            }

            ReadAttributes(xElement);

            Content.Clear();
            IEnumerable<XNode> descendants = xElement.Nodes();
            foreach (var node in descendants)
            {
                IHTML5Item item = ElementFactory.CreateElement(node);
                if ((item != null) && IsValidSubType(item))
                {
                    try
                    {
                        item.Load(node);
                    }
                    catch (Exception)
                    {
                        continue;
                    }
                    Content.Add(item);
                }
            }
        }

        public override XNode Generate()
        {
            var xElement = new XElement(XhtmlNameSpace + ElementName);

            AddAttributes(xElement);

            foreach (var item in Content)
            {
                xElement.Add(item.Generate());
            }
            return xElement;
        }

        public override bool IsValid()
        {
            return true;
        }

        protected override bool IsValidSubType(IHTML5Item item)
        {
            if (item is IInlineItem ||
                item is IBlockElement ||
                item is SimpleHTML5Text)
            {
                return item.IsValid();
            }
            return false;
        }

        protected override void AddAttributes(XElement xElement)
        {
            base.AddAttributes(xElement);
            _src.AddAttribute(xElement);
            _autoplay.AddAttribute(xElement);
            _controls.AddAttribute(xElement);
            _loop.AddAttribute(xElement);
            _muted.AddAttribute(xElement);
            _preload.AddAttribute(xElement);
            _heightAttribute.AddAttribute(xElement);
            _widthAttribute.AddAttribute(xElement);
            _posterAttribute.AddAttribute(xElement);
        }

        protected override void ReadAttributes(XElement xElement)
        {
            base.ReadAttributes(xElement);
            _src.ReadAttribute(xElement);
            _autoplay.ReadAttribute(xElement);
            _controls.ReadAttribute(xElement);
            _loop.ReadAttribute(xElement);
            _muted.AddAttribute(xElement);
            _preload.AddAttribute(xElement);
            _heightAttribute.ReadAttribute(xElement);
            _widthAttribute.ReadAttribute(xElement);
            _posterAttribute.ReadAttribute(xElement);
        }

    }
}
