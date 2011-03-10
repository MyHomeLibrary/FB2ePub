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
    internal class AnnotationConverter : BaseElementConverter
    {
        public AnnotationItem Item { get; set; }

        public Div Convert(int level)
        {
            if (Item == null)
            {
                throw new NullReferenceException("Item");
            }
            Div resAnnotation = new Div();

            foreach (var element in Item.Content)
            {
                if (element is ParagraphItem)
                {
                    ParagraphConverter paragraphConverter = new ParagraphConverter
                                                                {
                                                                    Item = element as ParagraphItem,
                                                                    Settings = Settings
                                                                };
                    resAnnotation.Add(paragraphConverter.Convert(ParagraphConvTargetEnum.Paragraph));
                }
                else if (element is PoemItem)
                {
                    PoemConverter poemConverter = new PoemConverter
                                                      {
                                                          Item = element as PoemItem,
                                                          Settings = Settings
                                                      };
                    resAnnotation.Add(poemConverter.Convert(level + 1));
                }
                else if (element is CiteItem)
                {
                    CitationConverter citationConverter = new CitationConverter
                                                              {
                                                                  Item = element as CiteItem,
                                                                  Settings = Settings
                                                              };
                    resAnnotation.Add(citationConverter.Convert(level + 1));
                }
                else if (element is SubTitleItem)
                {
                    SubtitleConverter subtitleConverter = new SubtitleConverter
                                                              {
                                                                  Item = element as SubTitleItem,
                                                                  Settings = Settings
                                                              };
                    resAnnotation.Add(subtitleConverter.Convert(level + 1));
                }
                else if (element is TableItem)
                {
                    TableConverter tableConverter = new TableConverter
                                                        {
                                                            Item = element as TableItem,
                                                            Settings = Settings
                                                        };
                    resAnnotation.Add(tableConverter.Convert());
                }
                else if (element is EmptyLineItem)
                {
                    EmptyLineConverter emptyLineConverter = new EmptyLineConverter();
                    resAnnotation.Add(emptyLineConverter.Convert());
                }
            }

            resAnnotation.ID.Value = Settings.ReferencesManager.AddIdUsed(Item.ID, resAnnotation);

            resAnnotation.Class.Value = "annotation";
            return resAnnotation;
        }

    }
}
