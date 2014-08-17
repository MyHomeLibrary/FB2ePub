namespace XHTMLClassLibrary.BaseElements.TableElements
{
    /// <summary>
    /// The thead element can be used to group table rows that contain table header information. 
    /// This can be useful when printing long tables that span several printed pages, since the data in thead will be repeated on each page.
    /// </summary>
    [HTMLItemAttribute(ElementName = "thead", SupportedStandards = HTMLElementType.HTML5 |  HTMLElementType.XHTML5 | HTMLElementType.Transitional | HTMLElementType.Strict | HTMLElementType.FrameSet)]
    public class TableHead : HTMLItem
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
