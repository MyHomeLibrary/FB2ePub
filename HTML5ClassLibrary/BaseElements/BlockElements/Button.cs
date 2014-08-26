using System.Linq;
using XHTMLClassLibrary.AttributeDataTypes;
using XHTMLClassLibrary.Attributes;
using XHTMLClassLibrary.BaseElements.InlineElements;

namespace XHTMLClassLibrary.BaseElements.BlockElements
{
    /// <summary>
    /// The button element is used to create button controls for forms. 
    /// Buttons created using the button element are similar in functionality to buttons created using the input element, 
    /// but offer greater rendering options.
    /// </summary>
    [HTMLItemAttribute(ElementName = "button", SupportedStandards = HTMLElementType.HTML5 |  HTMLElementType.XHTML5 | HTMLElementType.XHTML11 | HTMLElementType.Transitional | HTMLElementType.Strict | HTMLElementType.FrameSet)]
    public class Button : HTMLItem, IBlockElement
    {
        [AttributeTypeAttributeMember(Name = "autofocus", SupportedStandards = HTMLElementType.HTML5 | HTMLElementType.XHTML5)]
        private readonly FlagTypeAttribute _autofocus = new FlagTypeAttribute();

        [AttributeTypeAttributeMember(Name = "disabled", SupportedStandards = HTMLElementType.HTML5 | HTMLElementType.XHTML5 | HTMLElementType.XHTML11 | HTMLElementType.Transitional | HTMLElementType.Strict | HTMLElementType.FrameSet)]
        private readonly FlagTypeAttribute _disabled = new FlagTypeAttribute();

        [AttributeTypeAttributeMember(Name = "form", SupportedStandards = HTMLElementType.HTML5 | HTMLElementType.XHTML5)]
        private readonly URITypeAttribute _form = new URITypeAttribute();

        [AttributeTypeAttributeMember(Name = "formaction", SupportedStandards = HTMLElementType.HTML5 | HTMLElementType.XHTML5)]
        private readonly URITypeAttribute _formAction = new URITypeAttribute();

        [AttributeTypeAttributeMember(Name = "enctype", SupportedStandards = HTMLElementType.HTML5 | HTMLElementType.XHTML5)]
        private readonly ValuesSelectionTypeAttribute<Text> _formEncoding = new ValuesSelectionTypeAttribute<Text>(@"application/x-www-form-urlencoded;multipart/form-data;text/plain");

        [AttributeTypeAttributeMember(Name = "formmethod", SupportedStandards = HTMLElementType.HTML5 | HTMLElementType.XHTML5)]
        private readonly ValuesSelectionTypeAttribute<Text> _formMethod = new ValuesSelectionTypeAttribute<Text>("get;post");

        [AttributeTypeAttributeMember(Name = "formnovalidate", SupportedStandards = HTMLElementType.HTML5 | HTMLElementType.XHTML5)]
        private readonly FlagTypeAttribute _formNoValidate = new FlagTypeAttribute();

        [AttributeTypeAttributeMember(Name = "formtarget", SupportedStandards = HTMLElementType.HTML5 | HTMLElementType.XHTML5)]
        private readonly FormTargetTypeAttribute _formTarget = new FormTargetTypeAttribute();

        [AttributeTypeAttributeMember(Name = "name", SupportedStandards = HTMLElementType.HTML5 | HTMLElementType.XHTML5 | HTMLElementType.XHTML11 | HTMLElementType.Transitional | HTMLElementType.Strict | HTMLElementType.FrameSet)]
        private readonly TextValueTypeAttribute _name = new TextValueTypeAttribute();

        [AttributeTypeAttributeMember(Name = "type", SupportedStandards = HTMLElementType.HTML5 | HTMLElementType.XHTML5 | HTMLElementType.XHTML11 | HTMLElementType.Transitional | HTMLElementType.Strict | HTMLElementType.FrameSet)]
        private readonly ValuesSelectionTypeAttribute<ContentType> _type = new ValuesSelectionTypeAttribute<ContentType>("reset;button;submit");

        [AttributeTypeAttributeMember(Name = "value", SupportedStandards = HTMLElementType.HTML5 | HTMLElementType.XHTML5 | HTMLElementType.XHTML11 | HTMLElementType.Transitional | HTMLElementType.Strict | HTMLElementType.FrameSet)]
        private readonly TextValueTypeAttribute _value = new TextValueTypeAttribute();



        public IAttributeDataAccess Type
        {
            get { return _type; }
        }

        public IAttributeDataAccess Name
        {
            get { return _name; }
        }

        public IAttributeDataAccess Value
        {
            get { return _value; }
        }

        public IAttributeDataAccess Disabled
        {
            get { return _disabled; }
        }

        public IAttributeDataAccess Autofocus
        {
            get { return _autofocus; }
        }

        public IAttributeDataAccess Form
        {
            get { return _form; }
        }

        public IAttributeDataAccess FormAction
        {
            get { return _formAction; }
        }

        public IAttributeDataAccess FormEncoding
        {
            get { return _formEncoding; }
        }

        public IAttributeDataAccess FormMethod
        {
            get { return _formMethod; }
        }

        public IAttributeDataAccess FormNoValidate
        {
            get { return _formNoValidate; }
        }

        public IAttributeDataAccess FormTarget
        {
            get { return _formTarget; }
        }


        public override bool IsValid()
        {
            return Subitems.All(item => item.IsValid());
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
