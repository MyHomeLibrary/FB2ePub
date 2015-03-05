using System;
using ConverterContracts.ConversionElementsStyles;
using FB2Library.Elements.Poem;
using XHTMLClassLibrary.BaseElements;
using XHTMLClassLibrary.BaseElements.BlockElements;

namespace FB2EPubConverter.ElementConvertersV3.Poem
{
    internal class StanzaConverterParamsV3
    {
        public ConverterOptionsV3 Settings { get; set; }
        public int Level { get; set; }
    }

    internal class StanzaConverterV3 : BaseElementConverterV3
    {
        /// <summary>
        /// Converts FB2 Stanza element into XHTML representation
        /// </summary>
        /// <param name="stanzaItem">item to convert</param>
        /// <param name="stanzaConverterParams"></param>
        /// <returns>XHTML representation</returns>
        public IHTMLItem Convert(StanzaItem stanzaItem, StanzaConverterParamsV3 stanzaConverterParams)
        {
            if (stanzaItem == null)
            {
                throw new ArgumentNullException("stanzaItem");
            }
            var stanzaSection = new Div(HTMLElementType.HTML5);

            if (stanzaItem.Title != null)
            {
                var titleConverter = new TitleConverterV3();
                stanzaSection.Add(titleConverter.Convert(stanzaItem.Title, new TitleConverterParamsV3 { TitleLevel = stanzaConverterParams.Level, Settings = stanzaConverterParams.Settings }));
            }

            if (stanzaItem.SubTitle != null)
            {
                var subtitleConverter = new SubtitleConverterV3();
                stanzaSection.Add(subtitleConverter.Convert(stanzaItem.SubTitle, new SubtitleConverterParamsV3 { Settings = stanzaConverterParams.Settings }));
            }

            foreach (var line in stanzaItem.Lines)
            {
                var vElementConverter = new VElementConverterV3();
                stanzaSection.Add(vElementConverter.Convert(line, new VElementConverterParamsV3 { Settings = stanzaConverterParams.Settings }));
            }

            if (stanzaItem.Lang != null)
            {
                stanzaSection.GlobalAttributes.Language.Value = stanzaItem.Lang;
            }

            SetClassType(stanzaSection, ElementStylesV3.Stanza);
            return stanzaSection;
        }

    }
}
