using System;
using ConverterContracts.ConversionElementsStyles;
using FB2EPubConverter.ElementConvertersV3.Epigraph;
using FB2Library.Elements;
using FB2Library.Elements.Poem;
using XHTMLClassLibrary.BaseElements;
using XHTMLClassLibrary.BaseElements.BlockElements;

namespace FB2EPubConverter.ElementConvertersV3.Poem
{
    internal class PoemConverterParamsV3
    {
        public int Level { get; set; }
        public ConverterOptionsV3 Settings { get; set; }
    }

    internal class PoemConverterV3 : BaseElementConverterV3
    {
        /// <summary>
        /// Convert poem FB2 element
        /// </summary>
        /// <param name="poemItem">item to convert</param>
        /// <param name="poemConverterParams"></param>
        /// <returns>XHTML representation</returns>
        public IHTMLItem Convert(PoemItem poemItem, PoemConverterParamsV3 poemConverterParams)
        {
            if (poemItem == null)
            {
                throw new ArgumentNullException("poemItem");
            }
            var poemContent = new Div(HTMLElementType.HTML5);

            if (poemItem.Title != null)
            {
                var titleConverter = new PoemTitleConverterV3();
                poemContent.Add(titleConverter.Convert(poemItem.Title,
                    new TitleConverterParamsV3 { TitleLevel = poemConverterParams.Level, Settings = poemConverterParams.Settings }));
            }

            foreach (var epigraph in poemItem.Epigraphs)
            {
                var epigraphConverter = new PoemEpigraphConverterV3();
                poemContent.Add(epigraphConverter.Convert(epigraph,
                    new EpigraphConverterParamsV3 { Level = poemConverterParams.Level + 1, Settings = poemConverterParams.Settings }
                    ));
            }

            foreach (var poemElement in poemItem.Content)
            {
                if (poemElement is StanzaItem)
                {
                    var stanzaConverter = new StanzaConverterV3();
                    poemContent.Add(stanzaConverter.Convert(poemElement as StanzaItem,
                        new StanzaConverterParamsV3 { Level = poemConverterParams.Level + 1, Settings = poemConverterParams.Settings }));
                }
                else if (poemElement is SubTitleItem)
                {
                    var subtitleConverter = new PoemSubtitleConverterV3();
                    poemContent.Add(subtitleConverter.Convert(poemElement as SubTitleItem,
                        new SubtitleConverterParamsV3 { Settings = poemConverterParams.Settings }));
                }
            }


            foreach (var author in poemItem.Authors)
            {
                var poemAuthorConverter = new PoemAuthorConverterV3();
                poemContent.Add(poemAuthorConverter.Convert(author,
                    new PoemAuthorConverterParamsV3 { Settings = poemConverterParams.Settings }));
            }

            if (poemItem.Date != null)
            {
                var poemDateConverter = new PoemDateConverterV3();
                poemContent.Add(poemDateConverter.Convert(poemItem.Date));
            }

            poemContent.GlobalAttributes.ID.Value = poemConverterParams.Settings.ReferencesManager.AddIdUsed(poemItem.ID, poemContent);

            if (poemItem.Lang != null)
            {
                poemContent.GlobalAttributes.Language.Value = poemItem.Lang;
            }
            SetClassType(poemContent, ElementStylesV3.Poem);
            return poemContent;
        }

    }
}
