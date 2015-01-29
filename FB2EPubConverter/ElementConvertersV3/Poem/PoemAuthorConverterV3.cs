using System;
using FB2Library.Elements;
using XHTMLClassLibrary.BaseElements;

namespace FB2EPubConverter.ElementConvertersV3.Poem
{
    internal class PoemAuthorConverterParamsV3
    {
        public ConverterOptionsV3 Settings { get; set; }
    }

    internal class PoemAuthorConverterV3 : BaseElementConverterV3
    {
        /// <summary>
        /// Converts FB2 poem  author element
        /// </summary>
        /// <param name="paragraphItem">item to convert</param>
        /// <param name="poemAuthorConverterParams"></param>
        /// <returns>XHTML representation</returns>
        public IHTMLItem Convert(ParagraphItem paragraphItem, PoemAuthorConverterParamsV3 poemAuthorConverterParams)
        {
            if (paragraphItem == null)
            {
                throw new ArgumentNullException("paragraphItem");
            }

            var paragraphConverter = new ParagraphConverterV3();
            var item = paragraphConverter.Convert(paragraphItem, new ParagraphConverterParamsV3 { ResultType = ParagraphConvTargetEnumV3.Paragraph, StartSection = false, Settings = poemAuthorConverterParams.Settings });

            SetClassType(item, "poem_author");
            return item;
        }

    }
}
