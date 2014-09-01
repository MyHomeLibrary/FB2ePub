using System.ComponentModel;
using XHTMLClassLibrary.AttributeDataTypes;
using XHTMLClassLibrary.Attributes;
using XHTMLClassLibrary.BaseElements.InlineElements;

namespace XHTMLClassLibrary.BaseElements.BlockElements
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


    /// <summary>
    /// The "audio" tag defines sound, such as music or other audio streams.
    /// Currently, there are 3 supported file formats for the "audio" element: MP3, Wav, and Ogg:
    /// </summary>
    [HTMLItemAttribute(ElementName = "audio", SupportedStandards = HTMLElementType.HTML5)]
    public class Audio : HTMLItem, IBlockElement
    {
        [AttributeTypeAttributeMember(SupportedStandards = HTMLElementType.HTML5 | HTMLElementType.XHTML5)]
        private readonly SimpleSingleTypeAttribute<URI> _src = new SimpleSingleTypeAttribute<URI>("src");

        [AttributeTypeAttributeMember(SupportedStandards = HTMLElementType.HTML5 | HTMLElementType.XHTML5)]
        private readonly FlagTypeAttribute _autoplay = new FlagTypeAttribute("autoplay");

        [AttributeTypeAttributeMember(SupportedStandards = HTMLElementType.HTML5 | HTMLElementType.XHTML5)]
        private readonly FlagTypeAttribute _controls = new FlagTypeAttribute("controls");

        [AttributeTypeAttributeMember(SupportedStandards = HTMLElementType.HTML5 | HTMLElementType.XHTML5)]
        private readonly FlagTypeAttribute  _loop = new FlagTypeAttribute("loop");

        [AttributeTypeAttributeMember(SupportedStandards = HTMLElementType.HTML5 | HTMLElementType.XHTML5)]
        private readonly FlagTypeAttribute _muted = new FlagTypeAttribute("muted");

        [AttributeTypeAttributeMember(SupportedStandards = HTMLElementType.HTML5 | HTMLElementType.XHTML5)]
        private readonly ValuesSelectionTypeAttribute<Text> _preload = new ValuesSelectionTypeAttribute<Text>("preload",typeof(PreloadAttributeOptions));



        /// <summary>
        /// Specifies the URL of the audio file
        /// </summary>
        public IAttributeDataAccess Src
        {
            get { return _src; }
        }

        /// <summary>
        /// Specifies that the audio will start playing as soon as it is ready
        /// </summary>
        public IAttributeDataAccess AutoPlay
        {
            get { return _autoplay; }
        }


        /// <summary>
        /// Specifies that audio controls should be displayed (such as a play/pause button etc)
        /// </summary>
        public IAttributeDataAccess Controls
        {
            get { return _controls; }    
        }


        /// <summary>
        /// Specifies that the audio will start over again, every time it is finished
        /// </summary>
        public IAttributeDataAccess Loop
        {
            get { return _loop; }
        }


        /// <summary>
        /// Specifies that the audio output should be muted
        /// </summary>
        public IAttributeDataAccess Muted
        {
            get { return _muted; }
        }

        /// <summary>
        /// Specifies if and how the author thinks the audio should be loaded when the page loads
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
