using System;
using FB2Library.Elements;
using FB2Library.Elements.Poem;
using FB2Library.Elements.Table;
using XHTMLClassLibrary.BaseElements;
using XHTMLClassLibrary.BaseElements.BlockElements;

namespace FB2EPubConverter.ElementConverters
{

    internal class CitationConverterParams
    {
        public ConverterOptions Settings { get; set; }
        public int Level { get; set; }       
    }

    internal class CitationConverter : BaseElementConverter
    {
        /// <summary>
        /// Convert FB2 citation element
        /// </summary>
        /// <param name="citeItem">item to convert</param>
        /// <param name="compatibility"></param>
        /// <param name="citationConverterParams"></param>
        /// <returns>XHTML representation</returns>
        public Div Convert(CiteItem citeItem, HTMLElementType compatibility, CitationConverterParams citationConverterParams)
        {
            if (citeItem == null)
            {
                throw new ArgumentNullException("citeItem");
            }

            if (citationConverterParams == null)
            {
                throw new ArgumentNullException("citationConverterParams");
            }

            var citation = new Div(compatibility);
            foreach (var item in citeItem.CiteData)
            {
                if (item is SubTitleItem)
                {
                    var subtitleConverter = new SubtitleConverter();
                    citation.Add(subtitleConverter.Convert(item as SubTitleItem,compatibility,
                        new SubtitleConverterParams { Settings = citationConverterParams.Settings}));
                }
                else if (item is ParagraphItem)
                {
                    var paragraphConverter = new ParagraphConverter();
                    citation.Add(paragraphConverter.Convert(item as ParagraphItem,compatibility, 
                        new ParagraphConverterParams { ResultType = ParagraphConvTargetEnum.Paragraph, Settings = citationConverterParams.Settings, StartSection = false}));
                }
                else if (item is PoemItem)
                {
                    var poemConverter = new PoemConverter();
                    citation.Add(poemConverter.Convert(item as PoemItem, compatibility,
                        new PoemConverterParams { Settings = citationConverterParams.Settings, Level = citationConverterParams.Level + 1 } 
                        ));
                }
                else if (item is EmptyLineItem)
                {
                    var emptyLineConverter = new EmptyLineConverter();
                    citation.Add(emptyLineConverter.Convert(compatibility));
                }
                else if (item is TableItem)
                {
                    var tableConverter = new TableConverter();
                    citation.Add(tableConverter.Convert(item as TableItem,compatibility,
                        new TableConverterParams { Settings = citationConverterParams.Settings}));
                }
            }

            foreach (var author in citeItem.TextAuthors)
            {
                var citationAuthorConverter = new CitationAuthorConverter();
                citation.Add(citationAuthorConverter.Convert(author,compatibility,new CitationAuthorConverterParams { Settings = citationConverterParams.Settings}));
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
