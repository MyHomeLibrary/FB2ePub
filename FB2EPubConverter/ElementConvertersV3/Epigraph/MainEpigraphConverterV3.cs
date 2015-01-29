using System;
using FB2EPubConverter.ElementConvertersV3.Poem;
using FB2Library.Elements;
using FB2Library.Elements.Poem;
using XHTMLClassLibrary.BaseElements;
using XHTMLClassLibrary.BaseElements.BlockElements;

namespace FB2EPubConverter.ElementConvertersV3.Epigraph
{
    internal class MainEpigraphConverterV3 : BaseElementConverterV3
    {
        /// <summary>
        /// Converts FB2 epigraph element
        /// </summary>
        /// <param name="epigraphItem"></param>
        /// <param name="epigraphConverterParams"></param>
        /// <returns>XHTML representation</returns>
        public Div Convert(EpigraphItem epigraphItem, EpigraphConverterParamsV3 epigraphConverterParams)
        {
            if (epigraphItem == null)
            {
                throw new ArgumentNullException("epigraphItem");
            }
            var content = new Div(HTMLElementType.HTML5);

            foreach (var element in epigraphItem.EpigraphData)
            {
                if (element is ParagraphItem)
                {
                    var paragraphConverter = new ParagraphConverterV3();
                    content.Add(paragraphConverter.Convert(element as ParagraphItem,
                        new ParagraphConverterParamsV3 { ResultType = ParagraphConvTargetEnumV3.Paragraph, Settings = epigraphConverterParams.Settings, StartSection = false }));
                }
                if (element is PoemItem)
                {
                    var poemConverter = new PoemConverterV3();
                    content.Add(poemConverter.Convert(element as PoemItem,
                    new PoemConverterParamsV3 { Level = epigraphConverterParams.Level + 1, Settings = epigraphConverterParams.Settings }
                    ));
                }
                if (element is CiteItem)
                {
                    var citationConverter = new CitationConverterV3();
                    content.Add(citationConverter.Convert(element as CiteItem,
                        new CitationConverterParamsV3 { Level = epigraphConverterParams.Level + 1, Settings = epigraphConverterParams.Settings }));
                }
                if (element is EmptyLineItem)
                {
                    var emptyLineConverter = new EmptyLineConverterV3();
                    content.Add(emptyLineConverter.Convert());
                }
            }

            foreach (var author in epigraphItem.TextAuthors)
            {
                var epigraphAuthorConverter = new EpigraphAuthorConverterV3();
                content.Add(epigraphAuthorConverter.Convert(author as TextAuthorItem, new EpigraphAuthorConverterParamsV3 { Settings = epigraphConverterParams.Settings }));
            }

            SetClassType(content, "epigraph_main");

            content.GlobalAttributes.ID.Value = epigraphConverterParams.Settings.ReferencesManager.AddIdUsed(epigraphItem.ID, content);

            return content;
        }

    }
}
