using System;
using FB2Library.Elements;
using FB2Library.Elements.Poem;
using XHTMLClassLibrary.BaseElements;
using XHTMLClassLibrary.BaseElements.BlockElements;

namespace FB2EPubConverter.ElementConverters
{
    internal class PoemEpigraphConverter : BaseElementConverter
    {
        /// <summary>
        /// Converts FB2 epigraph element
        /// </summary>
        /// <param name="epigraphItem"></param>
        /// <param name="compatibility"></param>
        /// <param name="epigraphConverterParams"></param>
        /// <returns>XHTML representation</returns>
        public Div Convert(EpigraphItem epigraphItem, HTMLElementType compatibility, EpigraphConverterParams epigraphConverterParams)
        {
            if (epigraphItem == null)
            {
                throw new ArgumentNullException("epigraphItem");
            }
            var content = new Div(compatibility);

            foreach (var element in epigraphItem.EpigraphData)
            {
                if (element is ParagraphItem)
                {
                    var paragraphConverter = new ParagraphConverter();
                    content.Add(paragraphConverter.Convert(element as ParagraphItem, compatibility,
                        new ParagraphConverterParams { ResultType = ParagraphConvTargetEnum.Paragraph, Settings = epigraphConverterParams.Settings, StartSection = false }));
                }
                if (element is PoemItem)
                {
                    var poemConverter = new PoemConverter();
                    content.Add(poemConverter.Convert(element as PoemItem, compatibility,
                    new PoemConverterParams { Level = epigraphConverterParams.Level + 1, Settings = epigraphConverterParams.Settings }
                    ));
                }
                if (element is CiteItem)
                {
                    var citationConverter = new CitationConverter();
                    content.Add(citationConverter.Convert(element as CiteItem, compatibility,
                        new CitationConverterParams { Level = epigraphConverterParams.Level + 1, Settings = epigraphConverterParams.Settings }));
                }
                if (element is EmptyLineItem)
                {
                    var emptyLineConverter = new EmptyLineConverter();
                    content.Add(emptyLineConverter.Convert(compatibility));
                }
            }

            foreach (var author in epigraphItem.TextAuthors)
            {
                var epigraphAuthorConverter = new EpigraphAuthorConverter();
                content.Add(epigraphAuthorConverter.Convert(author as TextAuthorItem, compatibility, new EpigraphAuthorConverterParams { Settings = epigraphConverterParams.Settings }));
            }

            SetClassType(content, "poem_epigraph");

            content.GlobalAttributes.ID.Value = epigraphConverterParams.Settings.ReferencesManager.AddIdUsed(epigraphItem.ID, content);

            return content;
        }
    }
}
