using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using FB2Library.Elements;
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
                PoemTitleConverter titleConverter = new PoemTitleConverter { Settings = Settings };
                poemContent.Add(titleConverter.Convert(poemItem.Title, level));
            }

            foreach (var epigraph in poemItem.Epigraphs)
            {
                PoemEpigraphConverter epigraphConverter = new PoemEpigraphConverter { Settings = Settings };
                poemContent.Add(epigraphConverter.Convert(epigraph,level + 1));
            }

            foreach (var poemElement in poemItem.Content)
            {
                if (poemElement is StanzaItem)
                {
                    StanzaConverter stanzaConverter = new StanzaConverter {Settings = Settings};
                    poemContent.Add(stanzaConverter.Convert(poemElement as StanzaItem, level + 1));
                }
                else if (poemElement is SubTitleItem)
                {
                    PoemSubtitleConverter subtitleConverter = new PoemSubtitleConverter { Settings = Settings };
                    poemContent.Add(subtitleConverter.Convert(poemElement as SubTitleItem));
                }
            }


            foreach (var author in poemItem.Authors)
            {
                PoemAuthorConverter poemAuthorConverter = new PoemAuthorConverter() { Settings = Settings };
                poemContent.Add(poemAuthorConverter.Convert(author));
            }

            if (poemItem.Date != null)
            {
                PoemDateConverter poemDateConverter = new PoemDateConverter {Settings = Settings};
                poemContent.Add(poemDateConverter.Convert(poemItem.Date));
            }

            poemContent.ID.Value = Settings.ReferencesManager.AddIdUsed(poemItem.ID, poemContent);

            if (poemItem.Lang != null)
            {
                poemContent.Language.Value = poemItem.Lang;
            }
            SetClassType(poemContent);
            return poemContent;
        }

        public override string GetElementType()
        {
            return "poem";
        }
    }
}
