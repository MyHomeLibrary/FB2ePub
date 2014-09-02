using System.ComponentModel;
using XHTMLClassLibrary.AttributeDataTypes;
using XHTMLClassLibrary.Attributes;

namespace XHTMLClassLibrary.BaseElements.InlineElements.TextBasedElements
{
    /// <summary>
    /// Hyperlink
    /// </summary>
    [HTMLItemAttribute(ElementName = "a", SupportedStandards = HTMLElementType.HTML5 |  HTMLElementType.XHTML5 | HTMLElementType.Transitional | HTMLElementType.Strict | HTMLElementType.FrameSet | HTMLElementType.XHTML11)]
    public class Anchor : TextBasedElement
    {
        #region Attribute_Values_Enums

        /// <summary>
        /// "rel" attribute possible values
        /// </summary>
        public enum RelAttributeOptions
        {
            [Description("alternate")]
            Alternate,

            [Description("author")]
            Author,

            [Description("bookmark")]
            Bookmark,

            [Description("help")]
            Help,

            [Description("license")]
            License,

            [Description("nofollow")]
            NoFollow,

            [Description("next")]
            Next,

            [Description("norefferer")]
            NoRefferer,

            [Description("prefetch")]
            Prefetch,

            [Description("prev")]
            Prev,

            [Description("search")]
            Search,

            [Description("tag")]
            Tag,
        }


        /// <summary>
        /// "shape" attribute possible values
        /// </summary>
        public enum ShapeAttributeOptions
        {
            [Description("circle")]
            Circle,

            [Description("default")]
            Default,

            [Description("poly")]
            Poly,

            [Description("rect")]
            Rect,
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
            Bookmark, // A related document

            [Description("nofollow")]
            Nofollow, // "nofollow" is used by Google, to specify that the Google search spider should not follow that link (mostly used for paid links)

            [Description("licence")]
            Licence,

            [Description("tag")]
            Tag,

            [Description("friend")]
            Friend,
        }


        #endregion


        [AttributeTypeAttributeMember(SupportedStandards = HTMLElementType.XHTML11 | HTMLElementType.Transitional | HTMLElementType.Strict | HTMLElementType.FrameSet)]
        private readonly SimpleSingleTypeAttribute<Charset> _charsetAttribute = new SimpleSingleTypeAttribute<Charset>("charset");

        [AttributeTypeAttributeMember(SupportedStandards = HTMLElementType.XHTML11 | HTMLElementType.Transitional | HTMLElementType.Strict | HTMLElementType.FrameSet)]
        private readonly SimpleSingleTypeAttribute<Coords> _coordinatesAttribute = new SimpleSingleTypeAttribute<Coords>("coords");

        [AttributeTypeAttributeMember(SupportedStandards = HTMLElementType.HTML5 | HTMLElementType.XHTML5)]
        private readonly SimpleSingleTypeAttribute<URI> _downloadAttrib = new SimpleSingleTypeAttribute<URI>("download");

        [AttributeTypeAttributeMember(SupportedStandards = HTMLElementType.HTML5 | HTMLElementType.XHTML5 | HTMLElementType.XHTML11 | HTMLElementType.Transitional | HTMLElementType.Strict | HTMLElementType.FrameSet)]
        private readonly SimpleSingleTypeAttribute<URI> _hrefAttrib = new SimpleSingleTypeAttribute<URI>("href");

        [AttributeTypeAttributeMember(SupportedStandards = HTMLElementType.HTML5 | HTMLElementType.XHTML5 | HTMLElementType.XHTML11 | HTMLElementType.Transitional | HTMLElementType.Strict | HTMLElementType.FrameSet)]
        private readonly SimpleSingleTypeAttribute<LanguageCode> _hrefLangAttrib = new SimpleSingleTypeAttribute<LanguageCode>("hreflang");

        [AttributeTypeAttributeMember(SupportedStandards = HTMLElementType.HTML5 | HTMLElementType.XHTML5)]
        private readonly SimpleSingleTypeAttribute<MediaDescriptions> _mediaAttr = new SimpleSingleTypeAttribute<MediaDescriptions>("media");

        [AttributeTypeAttributeMember(SupportedStandards = HTMLElementType.XHTML11 | HTMLElementType.Transitional | HTMLElementType.Strict | HTMLElementType.FrameSet)]
        private readonly SimpleSingleTypeAttribute<Text> _nameAttribute = new SimpleSingleTypeAttribute<Text>("name");

        [AttributeTypeAttributeMember(SupportedStandards = HTMLElementType.HTML5 | HTMLElementType.XHTML5 | HTMLElementType.XHTML11 | HTMLElementType.Transitional | HTMLElementType.Strict | HTMLElementType.FrameSet)]
        private readonly ValuesSelectionTypeAttribute<Text> _relAttrib = new ValuesSelectionTypeAttribute<Text>("rel",typeof(RelAttributeOptions));

        [AttributeTypeAttributeMember(SupportedStandards = HTMLElementType.XHTML11 | HTMLElementType.Transitional | HTMLElementType.Strict | HTMLElementType.FrameSet)]
        private readonly ValuesSelectionTypeAttribute<Text> _reverseRelationAttribute = new ValuesSelectionTypeAttribute<Text>("rev",typeof(RevAttributeOptions));

        [AttributeTypeAttributeMember(SupportedStandards = HTMLElementType.XHTML11 | HTMLElementType.Transitional | HTMLElementType.Strict | HTMLElementType.FrameSet)]
        private readonly ValuesSelectionTypeAttribute<Text> _shapeAttribute = new ValuesSelectionTypeAttribute<Text>("shape",typeof(ShapeAttributeOptions));

        [AttributeTypeAttributeMember(SupportedStandards = HTMLElementType.HTML5 | HTMLElementType.XHTML5 | HTMLElementType.XHTML11 | HTMLElementType.Transitional | HTMLElementType.Strict | HTMLElementType.FrameSet)]
        private readonly SimpleSingleTypeAttribute<TargetType> _targetAttr = new SimpleSingleTypeAttribute<TargetType>("target");

        [AttributeTypeAttributeMember(SupportedStandards = HTMLElementType.HTML5 | HTMLElementType.XHTML5)]
        private readonly SimpleSingleTypeAttribute<MIME_Type> _typeAttr = new SimpleSingleTypeAttribute<MIME_Type>("type");
 


#region public_attributes

        public Anchor(HTMLElementType htmlStandard) : base(htmlStandard)
        {
        }

        /// <summary>
        /// Specifies the shape of a link
        /// Not supported in HTML5
        /// </summary>
        public IAttributeDataAccess Shape { get { return _shapeAttribute; }}

        /// <summary>
        /// Specifies the relationship between the linked document and the current document
        /// Not supported in HTML5.
        /// </summary>
        public IAttributeDataAccess ReverseRelation { get { return _reverseRelationAttribute; } }

        /// <summary>
        /// Specifies the name of an anchor
        /// Not supported in HTML5. Use the id attribute instead
        /// </summary>
        public IAttributeDataAccess Name { get { return _nameAttribute; } }

        /// <summary>
        /// Specifies the coordinates of a link
        /// Not supported in HTML5.
        /// </summary>
        public IAttributeDataAccess Coordinates { get { return _coordinatesAttribute; } }

        /// <summary>
        ///  Specifies the character-set of a linked document
        /// Not supported in HTML5.
        /// </summary>
        public IAttributeDataAccess Charset { get { return _charsetAttribute; } }

        /// <summary>
        /// This attribute specifies the location of a Web resource. 
        /// For example: http://xhtml.com/ or mailto:info@xhtml.com.
        /// </summary>
        public IAttributeDataAccess HRef { get { return _hrefAttrib; } }

        /// <summary>
        /// Specifies the primary language of the resource designated by href and may only be used when href is specified.
        /// </summary>
        public IAttributeDataAccess HrefLanguage { get { return _hrefLangAttrib; } }

        /// <summary>
        /// Describes the relationship from the current document to the resource specified by the href attribute. The value of this attribute is a space-separated list of link types. 
        /// For example: appendix.
        /// </summary>
        public IAttributeDataAccess Rel { get { return _relAttrib; } }

        /// <summary>
        /// Specifies that the target will be downloaded when a user clicks on the hyperlink
        /// </summary>
        public IAttributeDataAccess Download { get { return _downloadAttrib; } }

        /// <summary>
        /// Specifies what media/device the linked document is optimized for
        /// </summary>
        public IAttributeDataAccess Media { get { return _mediaAttr; } }

        /// <summary>
        /// Specifies where to open the linked document
        /// </summary>
        public IAttributeDataAccess Target { get { return _targetAttr; } }

        /// <summary>
        /// Specifies the MIME type of the linked document
        /// </summary>
        public IAttributeDataAccess Type { get { return _typeAttr; } }

#endregion


        protected override  bool IsValidSubType(IHTMLItem item)
        {

            if (item is IInlineItem)
            {
                if (item is Anchor)
                {
                    return false;
                }
                return item.IsValid();
            }
            if (item is SimpleHTML5Text)
            {
                return true;
            }
            return false;
        }


        public override bool IsValid()
        {
            return HRef != null;
        }
    }
}