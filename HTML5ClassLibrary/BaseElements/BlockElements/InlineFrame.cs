using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml;
using System.Xml.Linq;
using XHTMLClassLibrary.Attributes;
using XHTMLClassLibrary.BaseElements.InlineElements;

namespace XHTMLClassLibrary.BaseElements.BlockElements
{
    [HTMLItemAttribute(ElementName = "iframe", SupportedStandards = HTMLElementType.HTML5 |  HTMLElementType.XHTML5 | HTMLElementType.Transitional | HTMLElementType.FrameSet)]
    public class InlineFrame : HTMLItem, IBlockElement
    {
        public InlineFrame()
        {
            RegisterAttribute(_heightAttribute);
            RegisterAttribute(_widthAttribute);
            RegisterAttribute(_sourceAttribute);
            RegisterAttribute(_nameAttribute);
            RegisterAttribute(_sandboxAttribute);
            RegisterAttribute(_seamlessAttribute);
            RegisterAttribute(_sourceDocAttribute);
        }

        private readonly HeightAttribute _heightAttribute = new HeightAttribute();
        private readonly WidthAttribute _widthAttribute = new WidthAttribute();
        private readonly SourceAttribute _sourceAttribute = new SourceAttribute();
        private readonly NameAttribute _nameAttribute = new NameAttribute();
        private readonly SandboxAttribute _sandboxAttribute = new SandboxAttribute();
        private readonly SeamlessAttribute _seamlessAttribute = new SeamlessAttribute();
        private readonly SourceDocAttribute _sourceDocAttribute = new SourceDocAttribute();
        
        /// <summary>
        /// Specifies the height of an "iframe"
        /// </summary>
        public HeightAttribute Height { get { return _heightAttribute; }}

        /// <summary>
        /// Specifies the width of an "iframe"
        /// </summary>
        public WidthAttribute Width { get { return _widthAttribute; }}

        /// <summary>
        /// Specifies the address of the document to embed in the "iframe"
        /// </summary>
        public SourceAttribute Src { get { return _sourceAttribute; }}

        /// <summary>
        /// Specifies the name of an "iframe"
        /// </summary>
        public NameAttribute Name { get { return _nameAttribute; }}

        /// <summary>
        /// Enables a set of extra restrictions for the content in the "iframe"
        /// </summary>
        public SandboxAttribute Sandbox { get { return _sandboxAttribute; }}

        /// <summary>
        /// Specifies that the "iframe" should look like it is a part of the containing document
        /// </summary>
        public SeamlessAttribute Seamless { get { return _seamlessAttribute; }}

        /// <summary>
        /// Specifies the HTML content of the page to show in the "iframe"
        /// </summary>
        public SourceDocAttribute SrcDoc { get { return _sourceDocAttribute; }}


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
