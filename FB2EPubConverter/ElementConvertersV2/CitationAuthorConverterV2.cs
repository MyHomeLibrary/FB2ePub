using System;
using ConverterContracts.ConversionElementsStyles;
using FB2Library.Elements;
using XHTMLClassLibrary.BaseElements;
using XHTMLClassLibrary.BaseElements.BlockElements;

namespace FB2EPubConverter.ElementConvertersV2
{
    internal class CitationAuthorConverterParamsV2
    {
        public ConverterOptionsV2 Settings { get; set; }    
    }

    internal class CitationAuthorConverterV2 : BaseElementConverterV2
    {
        /// <summary>
        /// Converts FB2 citation author element
        /// </summary>
        /// <param name="paragraphItem">item to convert</param>
        /// <param name="citationAuthorConverterParams"></param>
        /// <returns>XHTML representation</returns>
        public IHTMLItem Convert(ParagraphItem paragraphItem, CitationAuthorConverterParamsV2 citationAuthorConverterParams)
        {
            if (paragraphItem == null)
            {
                throw new ArgumentNullException("paragraphItem");
            }
            var cite = new Div(HTMLElementType.XHTML11);

            var paragraphConverter = new ParagraphConverterV2();
            cite.Add(paragraphConverter.Convert(paragraphItem,
                new ParagraphConverterParamsV2 {Settings =citationAuthorConverterParams.Settings, ResultType = ParagraphConvTargetEnumV2.Paragraph, StartSection = false}));

            SetClassType(cite, ElementStylesV2.CitationAuthor);
            return cite;
        }

    }
}
