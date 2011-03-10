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
        /// <summary>
        /// Converts FB2 citation author element
        /// </summary>
        /// <param name="paragraphItem">item to convert</param>
        /// <returns>XHTML representation</returns>
        public IXHTMLItem Convert(ParagraphItem paragraphItem)
        {
            if (paragraphItem == null)
            {
                throw new ArgumentNullException("paragraphItem");
            }
            Citation cite = new Citation();

            cite.Add(new SimpleEPubText { Text = paragraphItem.ToString() });

            cite.Class.Value = "citation_author";
            return cite;
        }

        public override string GetElementType()
        {
            return "citation_author";
        }
    }
}
