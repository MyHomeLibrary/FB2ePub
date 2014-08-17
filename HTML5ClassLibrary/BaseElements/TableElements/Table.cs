using XHTMLClassLibrary.Attributes.FlaggedAttributes;
using XHTMLClassLibrary.BaseElements.BlockElements;

namespace XHTMLClassLibrary.BaseElements.TableElements
{
    /// <summary>
    /// The table element is used to define a table. 
    /// A table is a construct where data is organized into rows and columns of cells.
    /// </summary>
    [HTMLItemAttribute(ElementName = "table", SupportedStandards = HTMLElementType.HTML5 |  HTMLElementType.XHTML5 | HTMLElementType.Transitional | HTMLElementType.Strict | HTMLElementType.FrameSet | HTMLElementType.XHTML11)]
    public class Table : HTMLItem, IBlockElement
    {
        public Table()
        {
            RegisterAttribute(_sortableAttribute);
        }

        private readonly SortableAttribute _sortableAttribute = new SortableAttribute();


        /// <summary>
        /// Specifies that the table should be sortable
        /// </summary>
        public SortableAttribute Sortable { get { return _sortableAttribute; }}

        protected override bool IsValidSubType(IHTMLItem item)
        {
            // may appear only once and only as first element
            if (item is TableCaption)
            {
                if (Subitems.Count > 0)
                {
                    return false;
                }
                IHTMLItem seekItem = Subitems.Find(x => x is TableCaption);
                if (seekItem != null)
                {
                    return false;
                }
                return item.IsValid();
            }
            if (item is ColElement)
            {
                return item.IsValid();
            }
            if (item is ColGroup)
            {
                return item.IsValid();
            }
            if (item is TableBody)
            {
                IHTMLItem seekItem = Subitems.Find(x => x is TableBody);
                if (seekItem != null)
                {
                    return false;
                }
                seekItem = Subitems.Find(x => x is TableRow);
                if (seekItem != null)
                {
                    return false;
                }
                return item.IsValid();
            }
            if (item is TableRow)
            {
                IHTMLItem seekItem = Subitems.Find(x => x is TableBody);
                if (seekItem != null)
                {
                    return false;
                }
                seekItem = Subitems.Find(x => x is TableHead);
                if (seekItem != null)
                {
                    return false;
                }
                seekItem = Subitems.Find(x => x is TableFooter);
                if (seekItem != null)
                {
                    return false;
                }
                return item.IsValid();
            }
            return false;
        }



        public override bool IsValid()
        {
            // TODO: perform full validation based on:
            //
            // The following element may appear only as the first one inside table :
            // * caption may appear at most once
            // Either one or the other or neither of the following two elements may then appear:
            //
            // * col may appear any number of times or not at all
            // * colgroup may appear any number of times or not at all
            //
            // Finally, one or more of the following elements must then appear in the order listed:
            // * thead may appear at most once, and only if tbody appears
            // * tfoot may appear at most once, and only if tbody appears
            // * tbody must appear at least once if, and only if, tr does not appear
            // * tr must appear at least once if, and only if, tbody does not appear
            return true;
        }
    }
}
