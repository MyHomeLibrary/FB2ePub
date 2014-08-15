namespace HTMLClassLibrary.BaseElements.TableElements
{
    /// <summary>
    /// The tr element defines a table row.
    /// </summary>
    [HTMLItemAttribute(ElementName = "tr", SupportedStandards = HTMLElementType.HTML5 | HTMLElementType.Transitional | HTMLElementType.Strict | HTMLElementType.FrameSet | HTMLElementType.XHTML11)]
    public class TableRow : HTMLItem
    {
        protected override bool IsValidSubType(IHTMLItem item)
        {
            if (item is TableData)
            {
                return item.IsValid();
            }
            if (item is TableHeaderCell)
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
