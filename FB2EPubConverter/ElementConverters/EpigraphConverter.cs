using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using FB2Library.Elements;
using FB2Library.Elements.Poem;
using XHTMLClassLibrary.BaseElements.BlockElements;

namespace FB2EPubConverter.ElementConverters
{
    internal class EpigraphConverter : BaseElementConverter
    {
        public EpigraphItem Item { set; get; }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="level">"recursion" level</param>
        /// <param name="fromMain">If this is epigraph from Main section (book) or other level</param>
        /// <returns></returns>
        public Div Convert(int level, bool fromMain)
        {
            if (Item == null)
            {
                throw new NullReferenceException("Item");
            }
            Div content = new Div();

            foreach (var element in Item.EpigraphData)
            {
                if (element is ParagraphItem)
                {
                    ParagraphConverter paragraphConverter = new ParagraphConverter
                                                                {
                                                                    Item = element as ParagraphItem,
                                                                    Settings = Settings
                                                                };
                    content.Add(paragraphConverter.Convert(ParagraphConvTargetEnum.Paragraph));
                }
                if (element is PoemItem)
                {
                    PoemConverter poemConverter = new PoemConverter
                                                      {
                                                          Item = element as PoemItem,
                                                          Settings = Settings
                                                      };
                    content.Add(poemConverter.Convert(level + 1));
                }
                if (element is CiteItem)
                {
                    CitationConverter citationAuthorConverter = new CitationConverter
                                                                          {Item = element as CiteItem};
                    content.Add(citationAuthorConverter.Convert(level + 1));
                }
                if (element is EmptyLineItem)
                {
                    EmptyLineConverter emptyLineConverter = new EmptyLineConverter();
                    content.Add(emptyLineConverter.Convert());
                }
            }

            foreach (var author in Item.TextAuthors)
            {
                EpigraphAuthorConverter epigraphAuthorConverter = new EpigraphAuthorConverter
                                                                      {
                                                                          Item = author as TextAuthorItem,
                                                                          Settings = Settings
                                                                      };
                IBlockElement epAuthor = epigraphAuthorConverter.Convert();
                epAuthor.Class.Value = "epigraph_author";
                content.Add(epAuthor);
            }

            if (fromMain)
            {
                content.Class.Value = "epigraph_main";
            }
            else
            {
                content.Class.Value = "epigraph";
            }

            content.ID.Value = Settings.ReferencesManager.AddIdUsed(Item.ID, content);

            return content;
        }

    }
}
