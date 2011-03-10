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
        public CiteItem Item { get; set; }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="level"></param>
        /// <returns></returns>
        public Div Convert(int level)
        {
            if (Item == null)
            {
                throw new NullReferenceException("Item");
            }
            Div citation = new Div();
            foreach (var item in Item.CiteData)
            {
                if (item is SubTitleItem)
                {
                    SubtitleConverter subtitleConverter = new SubtitleConverter
                                                              {
                                                                  Item = item as SubTitleItem,
                                                                  Settings = Settings
                                                              };
                    citation.Add(subtitleConverter.Convert(level));
                }
                else if (item is ParagraphItem)
                {
                    ParagraphConverter paragraphConverter = new ParagraphConverter
                                                            {
                                                                Item = item as ParagraphItem,
                                                                Settings = Settings
                                                            };

                    citation.Add(paragraphConverter.Convert(ParagraphConvTargetEnum.Paragraph));
                }
                else if (item is PoemItem)
                {
                    PoemConverter poemConverter = new PoemConverter
                                                      {
                                                          Item = item as PoemItem,
                                                          Settings = Settings
                                                      };
                    citation.Add(poemConverter.Convert(level + 1));
                }
                else if (item is EmptyLineItem)
                {
                    EmptyLineConverter emptyLineConverter = new EmptyLineConverter();
                    citation.Add(emptyLineConverter.Convert());
                }
                else if (item is TableItem)
                {
                    TableConverter tableConverter = new TableConverter
                                                        {
                                                            Item = item as TableItem,
                                                            Settings = Settings
                                                        };
                    citation.Add(tableConverter.Convert());
                }
            }

            foreach (var author in Item.TextAuthors)
            {
                CitationAuthorConverter citationAuthorConverter = new CitationAuthorConverter {Item = author};
                citation.Add(citationAuthorConverter.Convert());
            }

            citation.ID.Value = Settings.ReferencesManager.AddIdUsed(Item.ID, citation);

            if (Item.Lang != null)
            {
                citation.Language.Value = Item.Lang;
            }
            citation.Class.Value = "citation";
            return citation;
        }


    }
}
