using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Xml.Linq;
using XHTMLClassLibrary.AttributeDataTypes;
using XHTMLClassLibrary.Attributes;


namespace XHTMLClassLibrary.BaseElements.Structure_Header
{
    /// <summary>
    /// The link element conveys relationship information that can be used by Web browsers and search engines. 
    /// You can have multiple link elements that link to different resources or describe different relationships. 
    /// The link elements can be contained in the head element.
    /// </summary>
    [HTMLItemAttribute(ElementName = "link", SupportedStandards = HTMLElementType.HTML5 |  HTMLElementType.XHTML5 | HTMLElementType.XHTML11 | HTMLElementType.Transitional | HTMLElementType.Strict | HTMLElementType.FrameSet)]    
    public class Link : HTMLItem
    {
        #region Attribute_Values_Enums

        /// <summary>
        /// "rel" attribute possible values
        /// </summary>
        public enum RelAttributeOptions
        {
            [Description("alternate")]
            Alternate,

            [Description("archives")]
            Archives,

            [Description("author")]
            Author,

            [Description("bookmark")]
            Bookmark,

            [Description("external")]
            External,

            [Description("first")]
            First,

            [Description("help")]
            Help,

            [Description("icon")]
            Icon,

            [Description("last")]
            Last,

            [Description("license")]
            License,

            [Description("next")]
            Next,

            [Description("nofollow")]
            Nofollow,

            [Description("noreferrer")]
            Noreferrer,

            [Description("pingback")]
            Pingback,

            [Description("prefetch")]
            Prefetch,

            [Description("prev")]
            Prev,

            [Description("search")]
            Search,

            [Description("sidebar")]
            Sidebar,

            [Description("stylesheet")]
            Stylesheet,

            [Description("tag")]
            Tag,

            [Description("up")]
            Up,
        }

        /// <summary>
        /// "rev" attribute possible values
        /// </summary>
        public enum RevAttributeOptions
        {
            [Description("alternate")]
            Alternate, //	An alternate version of the document (i.e. print page, translated or mirror)

            [Description("stylesheet")]
            Stylesheet, 	//An external style sheet for the document

            [Description("start")]
            Start, 	//The first document in a selection

            [Description("next")]
            Next, 	//The next document in a selection

            [Description("prev")]
            Prev, 	//The previous document in a selection

            [Description("contents")]
            Contents, 	//A table of contents for the document

            [Description("index")]
            Index, 	//An index for the document

            [Description("glossary")]
            Glossary, 	//A glossary (explanation) of words used in the document

            [Description("copyright")]
            Copyright, 	//A document containing copyright information

            [Description("chapter")]
            Chapter, 	//A chapter of the document

            [Description("section")]
            Section, 	//A section of the document

            [Description("subsection")]
            Subsection, 	//A subsection of the document

            [Description("appendix")]
            Appendix, 	//An appendix for the document

            [Description("help")]
            Help, 	//A help document

            [Description("bookmark")]
            Bookmark,
        }

        #endregion

        [AttributeTypeAttributeMember(SupportedStandards = HTMLElementType.XHTML11 | HTMLElementType.Transitional | HTMLElementType.Strict | HTMLElementType.FrameSet)]
        private readonly SimpleSingleTypeAttribute<Charset> _charsetAttribute = new SimpleSingleTypeAttribute<Charset>("charset");

        [AttributeTypeAttributeMember(SupportedStandards = HTMLElementType.HTML5 | HTMLElementType.XHTML5 | HTMLElementType.XHTML11 | HTMLElementType.Transitional | HTMLElementType.Strict | HTMLElementType.FrameSet)]
        private readonly SimpleSingleTypeAttribute<URI> _hrefAttribute = new SimpleSingleTypeAttribute<URI>("href");

        [AttributeTypeAttributeMember(SupportedStandards = HTMLElementType.HTML5 | HTMLElementType.XHTML5 | HTMLElementType.XHTML11 | HTMLElementType.Transitional | HTMLElementType.Strict | HTMLElementType.FrameSet)]
        private readonly SimpleSingleTypeAttribute<LanguageCode> _hrefLangAttribute = new SimpleSingleTypeAttribute<LanguageCode>("hreflang");

        [AttributeTypeAttributeMember(SupportedStandards = HTMLElementType.HTML5 | HTMLElementType.XHTML5 | HTMLElementType.XHTML11 | HTMLElementType.Transitional | HTMLElementType.Strict | HTMLElementType.FrameSet)]
        private readonly SimpleSingleTypeAttribute<MediaDescriptions> _mediaAttribute = new SimpleSingleTypeAttribute<MediaDescriptions>("media");

        [AttributeTypeAttributeMember(SupportedStandards = HTMLElementType.HTML5 | HTMLElementType.XHTML5 | HTMLElementType.XHTML11 | HTMLElementType.Transitional | HTMLElementType.Strict | HTMLElementType.FrameSet)]
        private readonly ValuesSelectionTypeAttribute<Text> _relAttribute = new ValuesSelectionTypeAttribute<Text>("rel",typeof(RelAttributeOptions));

        [AttributeTypeAttributeMember(SupportedStandards = HTMLElementType.XHTML11 | HTMLElementType.Transitional | HTMLElementType.Strict | HTMLElementType.FrameSet)]
        private readonly ValuesSelectionTypeAttribute<Text> _reverseRelationAttribute = new ValuesSelectionTypeAttribute<Text>("rev",typeof(RevAttributeOptions));

        [AttributeTypeAttributeMember(SupportedStandards = HTMLElementType.HTML5 | HTMLElementType.XHTML5)]
        private readonly SimpleSingleTypeAttribute<Sizes> _sizesAttribute = new SimpleSingleTypeAttribute<Sizes>("sizes");

        [AttributeTypeAttributeMember(SupportedStandards = HTMLElementType.HTML5 | HTMLElementType.XHTML5 | HTMLElementType.XHTML11 | HTMLElementType.Transitional | HTMLElementType.Strict | HTMLElementType.FrameSet)]
        private readonly SimpleSingleTypeAttribute<MIME_Type> _typeAttribute = new SimpleSingleTypeAttribute<MIME_Type>("type");


        #region public_properties

        public Link(HTMLElementType htmlStandard) : base(htmlStandard)
        {
        }

        /// <summary>
        /// Specifies the character encoding of the linked document
        /// Not supported in HTML5.
        /// </summary>
        public IAttributeDataAccess Charset { get { return _charsetAttribute; }}

        /// <summary>
        /// Specifies the primary language of the resource designated by href and may only be used when href is specified.
        /// </summary>
        public IAttributeDataAccess RefLanguage { get { return _hrefLangAttribute; } }

        /// <summary>
        /// Describes the forward relationship from the current document to the resource specified by the href attribute. 
        /// The value of this attribute is a space-separated list of link types.
        /// </summary>
        public IAttributeDataAccess Relation { get { return _relAttribute; } }


        /// <summary>
        /// Specifies the relationship between the linked document and the current document
        /// Not supported in HTML5.
        /// </summary>
        public IAttributeDataAccess Rev { get { return _reverseRelationAttribute; } }

        /// <summary>
        /// This attribute specifies the location of a Web resource.
        /// </summary>
        public IAttributeDataAccess HRef { get { return _hrefAttribute; } }

        /// <summary>
        /// his attribute specifies the intended destination medium for style information. 
        /// It may be a single media descriptor or a comma-separated list. 
        /// The default value for this attribute is "screen".
        /// </summary>
        public IAttributeDataAccess Media { get { return _mediaAttribute; } }

        /// <summary>
        /// Style sheet language. 
        /// For example: text/css.
        /// </summary>
        public IAttributeDataAccess Type { get { return _typeAttribute; } }


        /// <summary>
        /// Specifies the size of the linked resource. Only for rel="icon"
        /// </summary>
        public IAttributeDataAccess Sizes { get { return _sizesAttribute; } }


        #endregion


        public override bool IsValid()
        {
            return true;
        }

        public override List<IHTMLItem> SubElements()
        {
            return null;
        }

    }
}
