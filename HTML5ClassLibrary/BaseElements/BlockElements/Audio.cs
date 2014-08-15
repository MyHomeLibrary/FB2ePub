using System;
using System.Collections.Generic;
using System.Xml;
using System.Xml.Linq;
using HTMLClassLibrary.Attributes;
using HTMLClassLibrary.Attributes.FlaggedAttributes;
using HTMLClassLibrary.BaseElements.InlineElements;

namespace HTMLClassLibrary.BaseElements.BlockElements
{
    /// <summary>
    /// The "audio" tag defines sound, such as music or other audio streams.
    /// Currently, there are 3 supported file formats for the "audio" element: MP3, Wav, and Ogg:
    /// </summary>
    [HTMLItemAttribute(ElementName = "audio", SupportedStandards = HTMLElementType.HTML5)]
    public class Audio : HTMLItem, IBlockElement
    {
        private readonly SourceAttribute _src = new SourceAttribute();
        private readonly AutoPlayAttribute _autoplay = new AutoPlayAttribute();
        private readonly ControlsAttribute _controls = new ControlsAttribute();
        private readonly LoopAttribute  _loop = new LoopAttribute();
        private readonly MutedAttribute _muted = new MutedAttribute();
        private readonly PreloadAttribute _preload = new PreloadAttribute();

        public Audio ()
        {
            RegisterAttribute(_src);
            RegisterAttribute(_autoplay);
            RegisterAttribute(_controls);
            RegisterAttribute(_loop);
            RegisterAttribute(_muted);
            RegisterAttribute(_preload);

        }

        public SourceAttribute Src
        {
            get { return _src; }
        }

        public AutoPlayAttribute AutoPlay
        {
            get { return _autoplay; }
        }

        public ControlsAttribute Controls
        {
            get { return _controls; }    
        }

        public LoopAttribute Loop
        {
            get { return _loop; }
        }

        public MutedAttribute Muted
        {
            get { return _muted; }
        }

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
