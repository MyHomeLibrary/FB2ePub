using System;
using FB2Library.Elements;
using XHTMLClassLibrary.BaseElements;
using XHTMLClassLibrary.BaseElements.BlockElements;

namespace FB2EPubConverter.ElementConvertersV2
{
    internal class CitationAuthorConverterParams
    {
        public ConverterOptionsV2 Settings { get; set; }    
    }

    internal class CitationAuthorConverterV2 : BaseElementConverterV2
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

            var paragraphConverter = new ParagraphConverterV2();
            cite.Add(paragraphConverter.Convert(paragraphItem,compatibility,
                new ParagraphConverterParams {Settings =citationAuthorConverterParams.Settings, ResultType = ParagraphConvTargetEnum.Paragraph, StartSection = false}));

            SetClassType(cite, "citation_author");
            return cite;
        }

    }
}
