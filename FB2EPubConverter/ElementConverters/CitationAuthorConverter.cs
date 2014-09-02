using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using FB2Library.Elements;
using XHTMLClassLibrary.BaseElements;
using XHTMLClassLibrary.BaseElements.BlockElements;
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
        public IHTMLItem Convert(ParagraphItem paragraphItem)
        {
            if (paragraphItem == null)
            {
                throw new ArgumentNullException("paragraphItem");
            }
            Div cite = new Div();

            ParagraphConverter paragraphConverter = new ParagraphConverter { Settings = Settings };
            cite.Add(paragraphConverter.Convert(paragraphItem, ParagraphConvTargetEnum.Paragraph));

            SetClassType(cite);
            return cite;
        }

        public override string GetElementType()
        {
            return "citation_author";
        }
    }
}
