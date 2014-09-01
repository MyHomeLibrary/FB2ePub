using System.ComponentModel;
using XHTMLClassLibrary.AttributeDataTypes;
using XHTMLClassLibrary.Attributes;
using XHTMLClassLibrary.BaseElements.InlineElements;

namespace XHTMLClassLibrary.BaseElements.BlockElements
{
    [HTMLItemAttribute(ElementName = "iframe", SupportedStandards = HTMLElementType.HTML5 |  HTMLElementType.XHTML5 | HTMLElementType.Transitional | HTMLElementType.FrameSet)]
    public class InlineFrame : HTMLItem, IBlockElement
    {

#region Attribute_Values_Enums

        /// <summary>
        /// "align" attribute possible values
        /// </summary>
        public enum AlignAttributeOptions
        {
            [Description("baseline")]
            Baseline,

            [Description("bottom")]
            Bottom,

            [Description("left")]
            Left,

            [Description("middle")]
            Middle,

            [Description("right")]
            Right,

            [Description("top")]
            Top,
        }


        /// <summary>
        /// "frameborder" attribute possible values
        /// </summary>
        public enum FrameBorderAttributeOptions
        {
            [Description("0")]
            Disabled,

            [Description("1")]
            Enabled,
        }


        /// <summary>
        /// "sandbox" attribute possible values
        /// </summary>
        public enum SendboxAttributeOptions
        {
            [Description("allow-forms")]
            AllowForms,

            [Description("allow-same-origins")]
            AllowSameOrigins,

            [Description("allow-scripts")]
            AllowScripts,

            [Description("allow-top-navigation")]
            AllowTopNavigation,
        }


        /// <summary>
        /// "scrolling" attribute possible values
        /// </summary>
        public enum ScrollingAttributeOptions
        {
            [Description("auto")]
            Auto,

            [Description("yes")]
            Yes,

            [Description("no")]
            No,
        }

#endregion

        [AttributeTypeAttributeMember(SupportedStandards = HTMLElementType.Transitional | HTMLElementType.Strict | HTMLElementType.FrameSet)]
        private readonly ValuesSelectionTypeAttribute<Text> _alignAttribute = new ValuesSelectionTypeAttribute<Text>("align",typeof(AlignAttributeOptions));

        [AttributeTypeAttributeMember(SupportedStandards = HTMLElementType.Transitional | HTMLElementType.Strict | HTMLElementType.FrameSet)]
        private readonly ValuesSelectionTypeAttribute<Text> _frameBorderAttribute = new ValuesSelectionTypeAttribute<Text>("frameborder",typeof(FrameBorderAttributeOptions));

        [AttributeTypeAttributeMember(SupportedStandards = HTMLElementType.HTML5 | HTMLElementType.XHTML5 | HTMLElementType.Transitional | HTMLElementType.Strict | HTMLElementType.FrameSet)]
        private readonly SimpleSingleTypeAttribute<Length> _heightAttribute = new SimpleSingleTypeAttribute<Length>("height");

        [AttributeTypeAttributeMember(SupportedStandards = HTMLElementType.Transitional | HTMLElementType.Strict | HTMLElementType.FrameSet)]
        private readonly SimpleSingleTypeAttribute<URI> _longDescriptionAttribute = new SimpleSingleTypeAttribute<URI>("longdesc");

        [AttributeTypeAttributeMember(SupportedStandards = HTMLElementType.Transitional | HTMLElementType.Strict | HTMLElementType.FrameSet)]
        private readonly SimpleSingleTypeAttribute<Pixels> _marginHeightAttribute = new SimpleSingleTypeAttribute<Pixels>("marginheight");

        [AttributeTypeAttributeMember(SupportedStandards = HTMLElementType.Transitional | HTMLElementType.Strict | HTMLElementType.FrameSet)]
        private readonly SimpleSingleTypeAttribute<Pixels> _marginWidthAttribute = new SimpleSingleTypeAttribute<Pixels>("marginwidth");

        [AttributeTypeAttributeMember(SupportedStandards = HTMLElementType.HTML5 | HTMLElementType.XHTML5 | HTMLElementType.Transitional | HTMLElementType.Strict | HTMLElementType.FrameSet)]
        private readonly SimpleSingleTypeAttribute<Text> _nameAttribute = new SimpleSingleTypeAttribute<Text>("name");

        [AttributeTypeAttributeMember(SupportedStandards = HTMLElementType.HTML5 | HTMLElementType.XHTML5)]
        private readonly ValuesSelectionTypeAttribute<Text> _sandboxAttribute = new ValuesSelectionTypeAttribute<Text>("sandbox",typeof(SendboxAttributeOptions));

        [AttributeTypeAttributeMember(SupportedStandards = HTMLElementType.Transitional | HTMLElementType.Strict | HTMLElementType.FrameSet)]
        private readonly ValuesSelectionTypeAttribute<Text> _scrollingAttribute = new ValuesSelectionTypeAttribute<Text>("scrolling",typeof(ScrollingAttributeOptions));

        [AttributeTypeAttributeMember(SupportedStandards = HTMLElementType.HTML5 | HTMLElementType.XHTML5)]
        private readonly FlagTypeAttribute _seamlessAttribute = new FlagTypeAttribute("seamless");

        [AttributeTypeAttributeMember(SupportedStandards = HTMLElementType.HTML5 | HTMLElementType.XHTML5 | HTMLElementType.Transitional | HTMLElementType.Strict | HTMLElementType.FrameSet)]
        private readonly SimpleSingleTypeAttribute<URI> _sourceAttribute = new SimpleSingleTypeAttribute<URI>("src");

        [AttributeTypeAttributeMember(SupportedStandards = HTMLElementType.HTML5 | HTMLElementType.XHTML5)]
        private readonly SimpleSingleTypeAttribute<HTMLCode> _sourceDocAttribute = new SimpleSingleTypeAttribute<HTMLCode>("srcdoc");

        [AttributeTypeAttributeMember(SupportedStandards = HTMLElementType.HTML5 | HTMLElementType.XHTML5 | HTMLElementType.Transitional | HTMLElementType.Strict | HTMLElementType.FrameSet)]
        private readonly SimpleSingleTypeAttribute<Length> _widthAttribute = new SimpleSingleTypeAttribute<Length>("width");

        




        /// <summary>
        ///  Specifies the alignment of an "iframe" according to surrounding elements
        /// Not supported in HTML5.
        /// </summary>
        public IAttributeDataAccess Align { get { return _alignAttribute; }}


        /// <summary>
        /// Specifies whether or not to display a border around an "iframe"
        /// Not supported in HTML5.
        /// </summary>
        public IAttributeDataAccess FrameBorder { get { return _frameBorderAttribute; } }


        /// <summary>
        /// Specifies the height of an "iframe"
        /// </summary>
        public IAttributeDataAccess Height { get { return _heightAttribute; } }


        /// <summary>
        /// Specifies a page that contains a long description of the content of an "iframe"
        /// Not supported in HTML5.
        /// </summary>
        public IAttributeDataAccess LongDescription { get { return _longDescriptionAttribute; } }


        /// <summary>
        /// Specifies the top and bottom margins of the content of an "iframe"
        /// Not supported in HTML5.
        /// </summary>
        public IAttributeDataAccess MarginHeight { get { return _marginHeightAttribute; } }


        /// <summary>
        /// Specifies the left and right margins of the content of an "iframe"
        /// Not supported in HTML5.
        /// </summary>
        public IAttributeDataAccess MarginWidth { get { return _marginWidthAttribute; } }

        /// <summary>
        /// Specifies the width of an "iframe"
        /// </summary>
        public IAttributeDataAccess Width { get { return _widthAttribute; } }

        /// <summary>
        /// Specifies the address of the document to embed in the "iframe"
        /// </summary>
        public IAttributeDataAccess Src { get { return _sourceAttribute; } }

        /// <summary>
        /// Specifies the name of an "iframe"
        /// </summary>
        public IAttributeDataAccess Name { get { return _nameAttribute; } }

        /// <summary>
        /// Enables a set of extra restrictions for the content in the "iframe"
        /// </summary>
        public IAttributeDataAccess Sandbox { get { return _sandboxAttribute; } }


        /// <summary>
        /// Specifies whether or not to display scrollbars in an "iframe"
        /// Not supported in HTML5.
        /// </summary>
        public IAttributeDataAccess Scrolling { get { return _scrollingAttribute; } }

        /// <summary>
        /// Specifies that the "iframe" should look like it is a part of the containing document
        /// </summary>
        public IAttributeDataAccess Seamless { get { return _seamlessAttribute; } }

        /// <summary>
        /// Specifies the HTML content of the page to show in the "iframe"
        /// </summary>
        public IAttributeDataAccess SrcDoc { get { return _sourceDocAttribute; } }


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


        public override bool IsValid()
        {
            return true;
        }

    }
}
