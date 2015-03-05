using System;
using ConverterContracts.ConversionElementsStyles;
using FB2Library.Elements;
using XHTMLClassLibrary.BaseElements;
using XHTMLClassLibrary.BaseElements.BlockElements;

namespace FB2EPubConverter.ElementConvertersV2.Epigraph
{
    internal class EpigraphAuthorConverterParamsV2
    {
        public ConverterOptionsV2 Settings { get; set; }  
    }

    internal class EpigraphAuthorConverterV2 : BaseElementConverterV2
    {
        /// <summary>
        /// Convert epigraph author FB2 element
        /// </summary>
        /// <param name="textAuthorItem">item to convert</param>
        /// <param name="epigraphAuthorConverterParams"></param>
        /// <returns>XHTML representation</returns>
        public IHTMLItem Convert(TextAuthorItem textAuthorItem,EpigraphAuthorConverterParamsV2 epigraphAuthorConverterParams)
        {
            if (textAuthorItem == null)
            {
                throw new ArgumentNullException("textAuthorItem");
            }
            var epigraphAuthor = new Div(HTMLElementType.XHTML11);
            var paragraphConverter = new ParagraphConverterV2();
            epigraphAuthor.Add(paragraphConverter.Convert(textAuthorItem,
                new ParagraphConverterParamsV2 { ResultType = ParagraphConvTargetEnumV2.Paragraph, Settings = epigraphAuthorConverterParams.Settings, StartSection = false }));
            SetClassType(epigraphAuthor, ElementStylesV2.EpigraphAuthor);
            return epigraphAuthor;
        }
    }
}
