using System;

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
        public  IHTMLItem Convert(PoemItem poemItem,int level)
        {
            if (poemItem == null)
            {
                throw new ArgumentNullException("poemItem");
            }
            var poemContent = new Div();

            if (poemItem.Title != null)
            {
                var titleConverter = new PoemTitleConverter { Settings = Settings };
                poemContent.Add(titleConverter.Convert(poemItem.Title, level));
            }

            foreach (var epigraph in poemItem.Epigraphs)
            {
                var epigraphConverter = new PoemEpigraphConverter { Settings = Settings };
                poemContent.Add(epigraphConverter.Convert(epigraph,level + 1));
            }

            foreach (var poemElement in poemItem.Content)
            {
                if (poemElement is StanzaItem)
                {
                    var stanzaConverter = new StanzaConverter {Settings = Settings};
                    poemContent.Add(stanzaConverter.Convert(poemElement as StanzaItem, level + 1));
                }
                else if (poemElement is SubTitleItem)
                {
                    var subtitleConverter = new PoemSubtitleConverter { Settings = Settings };
                    poemContent.Add(subtitleConverter.Convert(poemElement as SubTitleItem));
                }
            }


            foreach (var author in poemItem.Authors)
            {
                var poemAuthorConverter = new PoemAuthorConverter { Settings = Settings };
                poemContent.Add(poemAuthorConverter.Convert(author));
            }

            if (poemItem.Date != null)
            {
                var poemDateConverter = new PoemDateConverter {Settings = Settings};
                poemContent.Add(poemDateConverter.Convert(poemItem.Date));
            }

            poemContent.GlobalAttributes.ID.Value = Settings.ReferencesManager.AddIdUsed(poemItem.ID, poemContent);

            if (poemItem.Lang != null)
            {
                poemContent.GlobalAttributes.Language.Value = poemItem.Lang;
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
