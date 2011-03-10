using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using FB2Library.Elements.Poem;
using XHTMLClassLibrary.BaseElements;
using XHTMLClassLibrary.BaseElements.BlockElements;

namespace FB2EPubConverter.ElementConverters
{
    internal class PoemConverter : BaseElementConverter
    {
        public PoemItem Item { get; set; }

        public  IXHTMLItem Convert(int level)
        {
            if (Item == null)
            {
                throw new NullReferenceException("Item");
            }
            Div poemContent = new Div();

            if (Item.Title != null)
            {
                TitleConverter titleConverter = new TitleConverter
                                                    {
                                                        Item = Item.Title,
                                                        Settings = Settings
                                                    };
                poemContent.Add(titleConverter.Convert(level));
            }

            foreach (var epigraph in Item.Epigraphs)
            {
                EpigraphConverter epigraphAuthorConverter = new EpigraphConverter
                                                                {
                                                                    Item = epigraph,
                                                                    Settings = Settings
                                                                };
                poemContent.Add(epigraphAuthorConverter.Convert(level + 1, false));
            }

            foreach (var stanza in Item.Content)
            {
                if (stanza is StanzaItem)
                {
                    StanzaConverter stanzaConverter = new StanzaConverter
                                                          {
                                                              Item = stanza as StanzaItem,
                                                              Settings = Settings
                                                          };
                    poemContent.Add(stanzaConverter.Convert(level + 1));
                }
            }

            foreach (var author in Item.Authors)
            {
                CitationAuthorConverter citationAuthorConverter = new CitationAuthorConverter {Item = author};
                poemContent.Add(citationAuthorConverter.Convert());
            }

            if (Item.Date != null)
            {
                Paragraph date = new Paragraph();
                date.Add(new SimpleEPubText { Text = Item.Date.Text });
                date.Class.Value = "poemdate";
                poemContent.Add(date);
            }

            poemContent.ID.Value = Settings.ReferencesManager.AddIdUsed(Item.ID, poemContent);

            if (Item.Lang != null)
            {
                poemContent.Language.Value = Item.Lang;
            }
            poemContent.Class.Value = "poem";
            return poemContent;
        }

    }
}
