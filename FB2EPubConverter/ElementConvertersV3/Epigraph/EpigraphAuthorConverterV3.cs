using System;
using ConverterContracts.ConversionElementsStyles;
using FB2Library.Elements;
using XHTMLClassLibrary.BaseElements;
using XHTMLClassLibrary.BaseElements.BlockElements;

namespace FB2EPubConverter.ElementConvertersV3.Epigraph
{
    internal class EpigraphAuthorConverterParamsV3
    {
        public ConverterOptionsV3 Settings { get; set; }
    }

    internal class EpigraphAuthorConverterV3 : BaseElementConverterV3
    {
        /// <summary>
        /// Convert epigraph author FB2 element
        /// </summary>
        /// <param name="textAuthorItem">item to convert</param>
        /// <param name="epigraphAuthorConverterParams"></param>
        /// <returns>XHTML representation</returns>
        public IHTMLItem Convert(TextAuthorItem textAuthorItem, EpigraphAuthorConverterParamsV3 epigraphAuthorConverterParams)
        {
            if (textAuthorItem == null)
            {
                throw new ArgumentNullException("textAuthorItem");
            }
            var epigraphAuthor = new Div(HTMLElementType.HTML5);
            var paragraphConverter = new ParagraphConverterV3();
            epigraphAuthor.Add(paragraphConverter.Convert(textAuthorItem,
                new ParagraphConverterParamsV3 { ResultType = ParagraphConvTargetEnumV3.Paragraph, Settings = epigraphAuthorConverterParams.Settings, StartSection = false }));
            SetClassType(epigraphAuthor, ElementStylesV3.EpigraphAuthor);
            return epigraphAuthor;
        }

    }
}
