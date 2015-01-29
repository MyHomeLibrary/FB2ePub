using System;
using FB2EPubConverter.ElementConvertersV2.Epigraph;
using FB2Library.Elements;
using FB2Library.Elements.Poem;
using XHTMLClassLibrary.BaseElements;
using XHTMLClassLibrary.BaseElements.BlockElements;

namespace FB2EPubConverter.ElementConvertersV2.Poem
{
    internal class PoemConverterParamsV2
    {
        public int Level { get; set; }
        public ConverterOptionsV2 Settings { get; set; }  
    }

    internal class PoemConverterV2 : BaseElementConverterV2
    {
        /// <summary>
        /// Convert poem FB2 element
        /// </summary>
        /// <param name="poemItem">item to convert</param>
        /// <param name="poemConverterParams"></param>
        /// <returns>XHTML representation</returns>
        public  IHTMLItem Convert(PoemItem poemItem,PoemConverterParamsV2 poemConverterParams)
        {
            if (poemItem == null)
            {
                throw new ArgumentNullException("poemItem");
            }
            var poemContent = new Div(HTMLElementType.XHTML11);

            if (poemItem.Title != null)
            {
                var titleConverter = new PoemTitleConverterV2();
                poemContent.Add(titleConverter.Convert(poemItem.Title,
                    new TitleConverterParamsV2{ TitleLevel = poemConverterParams.Level,Settings = poemConverterParams.Settings}));
            }

            foreach (var epigraph in poemItem.Epigraphs)
            {
                var epigraphConverter = new PoemEpigraphConverterV2();
                poemContent.Add(epigraphConverter.Convert(epigraph,
                    new EpigraphConverterParamsV2{Level = poemConverterParams.Level+ 1,Settings = poemConverterParams.Settings}
                    ));
            }

            foreach (var poemElement in poemItem.Content)
            {
                if (poemElement is StanzaItem)
                {
                    var stanzaConverter = new StanzaConverterV2();
                    poemContent.Add(stanzaConverter.Convert(poemElement as StanzaItem, 
                        new StanzaConverterParamsV2 {Level = poemConverterParams.Level + 1,Settings = poemConverterParams.Settings}));
                }
                else if (poemElement is SubTitleItem)
                {
                    var subtitleConverter = new PoemSubtitleConverterV2();
                    poemContent.Add(subtitleConverter.Convert(poemElement as SubTitleItem,
                        new SubtitleConverterParamsV2 { Settings = poemConverterParams.Settings}));
                }
            }


            foreach (var author in poemItem.Authors)
            {
                var poemAuthorConverter = new PoemAuthorConverterV2();
                poemContent.Add(poemAuthorConverter.Convert(author,
                    new PoemAuthorConverterParamsV2 { Settings = poemConverterParams .Settings}));
            }

            if (poemItem.Date != null)
            {
                var poemDateConverter = new PoemDateConverterV2();
                poemContent.Add(poemDateConverter.Convert(poemItem.Date));
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
