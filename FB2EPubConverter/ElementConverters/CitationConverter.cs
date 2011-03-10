using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using FB2Library.Elements;
using FB2Library.Elements.Poem;
using FB2Library.Elements.Table;
using XHTMLClassLibrary.BaseElements.BlockElements;

namespace FB2EPubConverter.ElementConverters
{
    internal class CitationConverter : BaseElementConverter
    {

        /// <summary>
        /// Convert FB2 citation element
        /// </summary>
        /// <param name="citeItem">item to convert</param>
        /// <param name="level">"deepness" level, affects representation</param>
        /// <returns>XHTML representation</returns>
        public Div Convert(CiteItem citeItem,int level)
        {
            if (citeItem == null)
            {
                throw new ArgumentNullException("citeItem");
            }
            Div citation = new Div();
            foreach (var item in citeItem.CiteData)
            {
                if (item is SubTitleItem)
                {
                    SubtitleConverter subtitleConverter = new SubtitleConverter {Settings = Settings};
                    citation.Add(subtitleConverter.Convert(item as SubTitleItem));
                }
                else if (item is ParagraphItem)
                {
                    ParagraphConverter paragraphConverter = new ParagraphConverter {Settings = Settings};
                    citation.Add(paragraphConverter.Convert(item as ParagraphItem,ParagraphConvTargetEnum.Paragraph));
                }
                else if (item is PoemItem)
                {
                    PoemConverter poemConverter = new PoemConverter {Settings = Settings};
                    citation.Add(poemConverter.Convert(item as PoemItem,level + 1));
                }
                else if (item is EmptyLineItem)
                {
                    EmptyLineConverter emptyLineConverter = new EmptyLineConverter();
                    citation.Add(emptyLineConverter.Convert());
                }
                else if (item is TableItem)
                {
                    TableConverter tableConverter = new TableConverter {Settings = Settings};
                    citation.Add(tableConverter.Convert(item as TableItem));
                }
            }

            foreach (var author in citeItem.TextAuthors)
            {
                CitationAuthorConverter citationAuthorConverter = new CitationAuthorConverter();
                citation.Add(citationAuthorConverter.Convert(author));
            }

            citation.ID.Value = Settings.ReferencesManager.AddIdUsed(citeItem.ID, citation);

            if (citeItem.Lang != null)
            {
                citation.Language.Value = citeItem.Lang;
            }
            SetClassType(citation);
            return citation;
        }


        public override string GetElementType()
        {
            return "citation";
        }
    }
}
