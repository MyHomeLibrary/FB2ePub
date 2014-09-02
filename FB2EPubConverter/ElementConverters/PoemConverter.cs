using System;
using FB2Library.Elements;
using FB2Library.Elements.Poem;
using XHTMLClassLibrary.BaseElements;
using XHTMLClassLibrary.BaseElements.BlockElements;


namespace FB2EPubConverter.ElementConverters
{
    internal class PoemConverterParams
    {
        public int Level { get; set; }
        public ConverterOptions Settings { get; set; }  
    }

    internal class PoemConverter : BaseElementConverter
    {
        /// <summary>
        /// Convert poem FB2 element
        /// </summary>
        /// <param name="poemItem">item to convert</param>
        /// <param name="compatibility"></param>
        /// <param name="poemConverterParams"></param>
        /// <returns>XHTML representation</returns>
        public  IHTMLItem Convert(PoemItem poemItem,HTMLElementType compatibility,PoemConverterParams poemConverterParams)
        {
            if (poemItem == null)
            {
                throw new ArgumentNullException("poemItem");
            }
            var poemContent = new Div(compatibility);

            if (poemItem.Title != null)
            {
                var titleConverter = new PoemTitleConverter();
                poemContent.Add(titleConverter.Convert(poemItem.Title,compatibility,
                    new TitleConverterParams{ TitleLevel = poemConverterParams.Level,Settings = poemConverterParams.Settings}));
            }

            foreach (var epigraph in poemItem.Epigraphs)
            {
                var epigraphConverter = new PoemEpigraphConverter();
                poemContent.Add(epigraphConverter.Convert(epigraph,compatibility,
                    new EpigraphConverterParams{Level = poemConverterParams.Level+ 1,Settings = poemConverterParams.Settings}
                    ));
            }

            foreach (var poemElement in poemItem.Content)
            {
                if (poemElement is StanzaItem)
                {
                    var stanzaConverter = new StanzaConverter();
                    poemContent.Add(stanzaConverter.Convert(poemElement as StanzaItem,compatibility, 
                        new StanzaConverterParams {Level = poemConverterParams.Level + 1,Settings = poemConverterParams.Settings}));
                }
                else if (poemElement is SubTitleItem)
                {
                    var subtitleConverter = new PoemSubtitleConverter();
                    poemContent.Add(subtitleConverter.Convert(poemElement as SubTitleItem,compatibility,
                        new SubtitleConverterParams { Settings = poemConverterParams.Settings}));
                }
            }


            foreach (var author in poemItem.Authors)
            {
                var poemAuthorConverter = new PoemAuthorConverter();
                poemContent.Add(poemAuthorConverter.Convert(author,compatibility,
                    new PoemAuthorConverterParams { Settings = poemConverterParams .Settings}));
            }

            if (poemItem.Date != null)
            {
                var poemDateConverter = new PoemDateConverter();
                poemContent.Add(poemDateConverter.Convert(poemItem.Date,compatibility));
            }

            poemContent.GlobalAttributes.ID.Value = poemConverterParams.Settings.ReferencesManager.AddIdUsed(poemItem.ID, poemContent);

            if (poemItem.Lang != null)
            {
                poemContent.GlobalAttributes.Language.Value = poemItem.Lang;
            }
            SetClassType(poemContent,"poem");
            return poemContent;
        }
    }
}
