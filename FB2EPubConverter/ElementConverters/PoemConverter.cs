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
        /// <summary>
        /// Convert poem FB2 element
        /// </summary>
        /// <param name="poemItem">item to convert</param>
        /// <param name="level">"deepness" level - affects representation</param>
        /// <returns>XHTML representation</returns>
        public  IXHTMLItem Convert(PoemItem poemItem,int level)
        {
            if (poemItem == null)
            {
                throw new ArgumentNullException("poemItem");
            }
            Div poemContent = new Div();

            if (poemItem.Title != null)
            {
                TitleConverter titleConverter = new TitleConverter {Settings = Settings};
                poemContent.Add(titleConverter.Convert(poemItem.Title, level));
            }

            foreach (var epigraph in poemItem.Epigraphs)
            {
                EpigraphConverter epigraphConverter = new EpigraphConverter {Settings = Settings};
                poemContent.Add(epigraphConverter.Convert(epigraph,level + 1, false));
            }

            foreach (var stanza in poemItem.Content)
            {
                if (stanza is StanzaItem)
                {
                    StanzaConverter stanzaConverter = new StanzaConverter {Settings = Settings};
                    poemContent.Add(stanzaConverter.Convert(stanza as StanzaItem,level + 1));
                }
            }

            foreach (var author in poemItem.Authors)
            {
                CitationAuthorConverter citationAuthorConverter = new CitationAuthorConverter();
                poemContent.Add(citationAuthorConverter.Convert(author));
            }

            if (poemItem.Date != null)
            {
                Paragraph date = new Paragraph();
                date.Add(new SimpleEPubText { Text = poemItem.Date.Text });
                date.Class.Value = "poemdate";
                poemContent.Add(date);
            }

            poemContent.ID.Value = Settings.ReferencesManager.AddIdUsed(poemItem.ID, poemContent);

            if (poemItem.Lang != null)
            {
                poemContent.Language.Value = poemItem.Lang;
            }
            poemContent.Class.Value = "poem";
            return poemContent;
        }

    }
}
