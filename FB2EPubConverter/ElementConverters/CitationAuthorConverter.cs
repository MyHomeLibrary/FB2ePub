using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using FB2Library.Elements;
using XHTMLClassLibrary.BaseElements;
using XHTMLClassLibrary.BaseElements.InlineElements;

namespace FB2EPubConverter.ElementConverters
{
    internal class CitationAuthorConverter : BaseElementConverter
    {
        public ParagraphItem Item { get; set; }

        public IXHTMLItem Convert()
        {
            if (Item == null)
            {
                throw new NullReferenceException("Item");
            }
            Citation cite = new Citation();

            cite.Add(new SimpleEPubText { Text = Item.ToString() });

            cite.Class.Value = "citation_author";
            return cite;
        }

    }
}
