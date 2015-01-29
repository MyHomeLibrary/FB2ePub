using System;
using FB2EPubConverter.ElementConvertersV3.Poem;
using FB2EPubConverter.ElementConvertersV3.Tables;
using FB2Library.Elements;
using FB2Library.Elements.Poem;
using FB2Library.Elements.Table;
using XHTMLClassLibrary.BaseElements;
using XHTMLClassLibrary.BaseElements.BlockElements;

namespace FB2EPubConverter.ElementConvertersV3
{
    internal class CitationConverterParamsV3
    {
        public ConverterOptionsV3 Settings { get; set; }
        public int Level { get; set; }
    }

    internal class CitationConverterV3 : BaseElementConverterV3
    {
        /// <summary>
        /// Convert FB2 citation element
        /// </summary>
        /// <param name="citeItem">item to convert</param>
        /// <param name="citationConverterParams"></param>
        /// <returns>XHTML representation</returns>
        public Div Convert(CiteItem citeItem, CitationConverterParamsV3 citationConverterParams)
        {
            if (citeItem == null)
            {
                throw new ArgumentNullException("citeItem");
            }

            if (citationConverterParams == null)
            {
                throw new ArgumentNullException("citationConverterParams");
            }

            var citation = new Div(HTMLElementType.HTML5);
            foreach (var item in citeItem.CiteData)
            {
                if (item is SubTitleItem)
                {
                    var subtitleConverter = new SubtitleConverterV3();
                    citation.Add(subtitleConverter.Convert(item as SubTitleItem,
                        new SubtitleConverterParamsV3 { Settings = citationConverterParams.Settings }));
                }
                else if (item is ParagraphItem)
                {
                    var paragraphConverter = new ParagraphConverterV3();
                    citation.Add(paragraphConverter.Convert(item as ParagraphItem,
                        new ParagraphConverterParamsV3 { ResultType = ParagraphConvTargetEnumV3.Paragraph, Settings = citationConverterParams.Settings, StartSection = false }));
                }
                else if (item is PoemItem)
                {
                    var poemConverter = new PoemConverterV3();
                    citation.Add(poemConverter.Convert(item as PoemItem,
                        new PoemConverterParamsV3 { Settings = citationConverterParams.Settings, Level = citationConverterParams.Level + 1 }
                        ));
                }
                else if (item is EmptyLineItem)
                {
                    var emptyLineConverter = new EmptyLineConverterV3();
                    citation.Add(emptyLineConverter.Convert());
                }
                else if (item is TableItem)
                {
                    var tableConverter = new TableConverterV3();
                    citation.Add(tableConverter.Convert(item as TableItem,
                        new TableConverterParamsV3 { Settings = citationConverterParams.Settings }));
                }
            }

            foreach (var author in citeItem.TextAuthors)
            {
                var citationAuthorConverter = new CitationAuthorConverterV3();
                citation.Add(citationAuthorConverter.Convert(author, new CitationAuthorConverterParamsV3 { Settings = citationConverterParams.Settings }));
            }

            citation.GlobalAttributes.ID.Value = citationConverterParams.Settings.ReferencesManager.AddIdUsed(citeItem.ID, citation);

            if (citeItem.Lang != null)
            {
                citation.GlobalAttributes.Language.Value = citeItem.Lang;
            }
            SetClassType(citation, "citation");
            return citation;
        }
    }
}
