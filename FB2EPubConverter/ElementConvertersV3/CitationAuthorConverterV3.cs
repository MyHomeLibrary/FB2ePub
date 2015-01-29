using System;
using FB2Library.Elements;
using XHTMLClassLibrary.BaseElements;
using XHTMLClassLibrary.BaseElements.BlockElements;

namespace FB2EPubConverter.ElementConvertersV3
{
    internal class CitationAuthorConverterParamsV3
    {
        public ConverterOptionsV3 Settings { get; set; }
    }

    internal class CitationAuthorConverterV3 : BaseElementConverterV3
    {
        /// <summary>
        /// Converts FB2 citation author element
        /// </summary>
        /// <param name="paragraphItem">item to convert</param>
        /// <param name="citationAuthorConverterParams"></param>
        /// <returns>XHTML representation</returns>
        public IHTMLItem Convert(ParagraphItem paragraphItem, CitationAuthorConverterParamsV3 citationAuthorConverterParams)
        {
            if (paragraphItem == null)
            {
                throw new ArgumentNullException("paragraphItem");
            }
            var cite = new Div(HTMLElementType.HTML5);

            var paragraphConverter = new ParagraphConverterV3();
            cite.Add(paragraphConverter.Convert(paragraphItem,
                new ParagraphConverterParamsV3 { Settings = citationAuthorConverterParams.Settings, ResultType = ParagraphConvTargetEnumV3.Paragraph, StartSection = false }));

            SetClassType(cite, "citation_author");
            return cite;
        }
    }
}
