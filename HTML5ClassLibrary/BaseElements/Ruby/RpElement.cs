namespace HTMLClassLibrary.BaseElements.Ruby
{
    /// <summary>
    /// The "rp" tag defines what to show if a browser does NOT support ruby annotations.
    /// Ruby annotations are used for East Asian typography, to show the pronunciation of East Asian characters.
    /// Use the "rp" tag together with the "ruby" and the "rt" tags: The "ruby" element consists of one or more characters that needs an explanation/pronunciation, and an "rt" element that gives that information, and an optional "rp" element that defines what to show for browsers that not support ruby annotations.
    /// </summary>
    [HTMLItemAttribute(ElementName = "rp", SupportedStandards = HTMLElementType.HTML5 )]
    public class RpElement : HTMLItem , IRubyItem
    {

        protected override bool IsValidSubType(IHTMLItem item)
        {
            if (item is SimpleHTML5Text)
            {
                return item.IsValid();
            }
            return false;
        }


        /// <summary>
        /// Checks it element data is valid
        /// </summary>
        /// <returns>
        /// true if valid
        /// </returns>
        public override bool IsValid()
        {
            return true;
        }
    }
}
