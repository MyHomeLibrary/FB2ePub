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
        /// <summary>
        /// Converts FB2 Stanza element into XHTML representation
        /// </summary>
        /// <param name="stanzaItem">item to convert</param>
        /// <param name="level">"deepness" level</param>
        /// <returns>XHTML representation</returns>
        public IHTMLItem Convert(StanzaItem stanzaItem,int level)
        {
            if (stanzaItem == null)
            {
                throw new ArgumentNullException("stanzaItem");
            }
            Div stanzaSection = new Div();

            if (stanzaItem.Title != null)
            {
                TitleConverter titleConverter = new TitleConverter {Settings = Settings};
                stanzaSection.Add(titleConverter.Convert(stanzaItem.Title, level));
            }

            if (stanzaItem.SubTitle != null)
            {
                SubtitleConverter subtitleConverter = new SubtitleConverter {Settings = Settings};
                stanzaSection.Add(subtitleConverter.Convert(stanzaItem.SubTitle));
            }

            foreach (var line in stanzaItem.Lines)
            {
                VElementConverter vElementConverter = new VElementConverter {Settings = Settings};
                stanzaSection.Add(vElementConverter.Convert(line));
            }

            if (stanzaItem.Lang != null)
            {
                stanzaSection.GlobalAttributes.Language.Value = stanzaItem.Lang;
            }

            SetClassType(stanzaSection);
            return stanzaSection;
        }

        public override string GetElementType()
        {
            return "stanza";
        }
    }
}
