using System;
using FB2Library.Elements;
using XHTMLClassLibrary.BaseElements;
using XHTMLClassLibrary.BaseElements.BlockElements;

namespace FB2EPubConverter.ElementConverters
{
    internal class CitationAuthorConverterParams
    {
        public ConverterOptions Settings { get; set; }    
    }

    internal class CitationAuthorConverter : BaseElementConverter
    {
        /// <summary>
        /// Converts FB2 citation author element
        /// </summary>
        /// <param name="paragraphItem">item to convert</param>
        /// <param name="compatibility"></param>
        /// <param name="citationAuthorConverterParams"></param>
        /// <returns>XHTML representation</returns>
        public IHTMLItem Convert(ParagraphItem paragraphItem, HTMLElementType compatibility,CitationAuthorConverterParams citationAuthorConverterParams)
        {
            if (paragraphItem == null)
            {
                throw new ArgumentNullException("paragraphItem");
            }
            var cite = new Div(compatibility);

            var paragraphConverter = new ParagraphConverter();
            cite.Add(paragraphConverter.Convert(paragraphItem,compatibility,
                new ParagraphConverterParams {Settings =citationAuthorConverterParams.Settings, ResultType = ParagraphConvTargetEnum.Paragraph, StartSection = false}));

            SetClassType(cite, "citation_author");
            return cite;
        }

    }
}
