using System;
using FB2Library.Elements;
using XHTMLClassLibrary.BaseElements;
using XHTMLClassLibrary.BaseElements.BlockElements;

namespace FB2EPubConverter.ElementConvertersV2
{
    internal class EpigraphAuthorConverterParams
    {
        public ConverterOptionsV2 Settings { get; set; }  
    }

    internal class EpigraphAuthorConverterV2 : BaseElementConverterV2
    {
        /// <summary>
        /// Convert epigraph author FB2 element
        /// </summary>
        /// <param name="textAuthorItem">item to convert</param>
        /// <param name="compatibility"></param>
        /// <param name="epigraphAuthorConverterParams"></param>
        /// <returns>XHTML representation</returns>
        public IHTMLItem Convert(TextAuthorItem textAuthorItem,HTMLElementType compatibility,EpigraphAuthorConverterParams epigraphAuthorConverterParams)
        {
            if (textAuthorItem == null)
            {
                throw new ArgumentNullException("textAuthorItem");
            }
            var epigraphAuthor = new Div(compatibility);
            var paragraphConverter = new ParagraphConverterV2();
            epigraphAuthor.Add(paragraphConverter.Convert(textAuthorItem,compatibility,
                new ParagraphConverterParams { ResultType = ParagraphConvTargetEnum.Paragraph, Settings = epigraphAuthorConverterParams.Settings, StartSection = false }));
            SetClassType(epigraphAuthor,"epigraph_author");
            return epigraphAuthor;
        }
    }
}
