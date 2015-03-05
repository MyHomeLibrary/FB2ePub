using System;
using ConverterContracts.ConversionElementsStyles;
using FB2Library.Elements;
using XHTMLClassLibrary.BaseElements;

namespace FB2EPubConverter.ElementConvertersV2.Poem
{
    internal class PoemAuthorConverterParamsV2
    {
        public ConverterOptionsV2 Settings { get; set; }  
    }

    internal class PoemAuthorConverterV2 : BaseElementConverterV2
    {
        /// <summary>
        /// Converts FB2 poem  author element
        /// </summary>
        /// <param name="paragraphItem">item to convert</param>
        /// <param name="poemAuthorConverterParams"></param>
        /// <returns>XHTML representation</returns>
        public IHTMLItem Convert(ParagraphItem paragraphItem,PoemAuthorConverterParamsV2 poemAuthorConverterParams)
        {
            if (paragraphItem == null)
            {
                throw new ArgumentNullException("paragraphItem");
            }

            var paragraphConverter = new ParagraphConverterV2();
            var item = paragraphConverter.Convert(paragraphItem,  new ParagraphConverterParamsV2 { ResultType = ParagraphConvTargetEnumV2.Paragraph, StartSection = false, Settings = poemAuthorConverterParams .Settings});

            SetClassType(item, ElementStylesV2.PoemAuthor);
            return item;
        }
    }
}
