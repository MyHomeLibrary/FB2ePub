using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using FB2Library.Elements.Poem;
using XHTMLClassLibrary.BaseElements;
using XHTMLClassLibrary.BaseElements.BlockElements;

namespace FB2EPubConverter.ElementConverters
{
    internal class StanzaConverter : BaseElementConverter
    {
        public StanzaItem Item { get; set; }


        public IXHTMLItem Convert(int level)
        {
            if (Item == null)
            {
                throw new NullReferenceException("Item");
            }
            Div stanzaSection = new Div();

            if (Item.Title != null)
            {
                TitleConverter titleConverter = new TitleConverter
                                                    {
                                                        Item = Item.Title,
                                                        Settings = Settings
                                                    };
                stanzaSection.Add(titleConverter.Convert(level));
            }

            if (Item.SubTitle != null)
            {
                SubtitleConverter subtitleConverter = new SubtitleConverter
                {
                    Item = Item.SubTitle,
                    Settings = Settings
                };
                stanzaSection.Add(subtitleConverter.Convert(level + 1));
            }

            foreach (var line in Item.Lines)
            {
                VElementConverter vElementConverter = new VElementConverter
                                                          {
                                                              Item = line,
                                                              Settings = Settings
                                                          };
                stanzaSection.Add(vElementConverter.Convert());
            }

            if (Item.Lang != null)
            {
                stanzaSection.Language.Value = Item.Lang;
            }

            stanzaSection.Class.Value = "stanza";
            return stanzaSection;
        }

    }
}
