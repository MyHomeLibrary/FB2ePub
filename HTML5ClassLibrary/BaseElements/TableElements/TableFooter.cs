namespace XHTMLClassLibrary.BaseElements.TableElements
{
    /// <summary>
    /// The tfoot element can be used to group table rows that contain table footer information. 
    /// This may be useful when printing longer tables that span several printed pages, since the data in tfoot is repeated on each page. 
    /// The tfoot element should appear before tbody elements.
    /// </summary>
    [HTMLItemAttribute(ElementName = "tfoot", SupportedStandards = HTMLElementType.HTML5 |  HTMLElementType.XHTML5 | HTMLElementType.Transitional | HTMLElementType.Strict | HTMLElementType.FrameSet)]
    public class TableFooter : HTMLItem
    {
        protected override bool IsValidSubType(IHTMLItem item)
        {
            if (item is TableRow)
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
